param(
  [ValidateSet('Debug', 'Release')]
  [string]$configuration = 'Release',

  [string]$buildFolder = $env:APPVEYOR_BUILD_FOLDER,
  [string]$CodeCovToken = ''
)

if ($buildFolder -eq $null) {
  $scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Definition
  $buildFolder = (Get-Item $scriptPath).Parent.FullName
}

Set-Location -Path $buildFolder

# Locate test runners etc
function Get-FullPath {
  param($SearchRoot,$Name)
  Get-ChildItem $SearchRoot $Name -Recurse | select -First 1 -ExpandProperty FullName
}
$OpenCover = Get-FullPath -SearchRoot "$env:ProgramData\chocolatey" OpenCover.Console.exe
$dotnet = Get-FullPath -SearchRoot "$env:ProgramFiles\dotnet" dotnet.exe
$codecov = Get-FullPath -SearchRoot "$env:ProgramData\chocolatey" codecov.exe
$test_results = Join-Path $(pwd) 'test_results'
$testExitCode = 0

if (!(Test-Path $test_results)) {
  mkdir $test_results
}

Get-ChildItem src\*.Tests `
| % {
  $test = $_.Name
  $coverage = Join-Path $test_results "$test.xml"

  $OpenCoverCmd = "$OpenCover -oldstyle -register:user"
  $OpenCoverCmd += " -target:`"$dotnet`" -targetargs:`""
  $OpenCoverCmd += "test src\\$test --no-build"
  $OpenCoverCmd += " --logger trx;LogFileName=$test.trx --results-directory $test_results"
  $OpenCoverCmd += "`""
  $OpenCoverCmd += " -output:`"$coverage`""

  Write-Host $OpenCoverCmd
  Invoke-Expression $OpenCoverCmd

  $global:testExitCode += $LASTEXITCODE
}

if ($testExitCode -ne 0) { $host.SetShouldExit($testExitCode); throw; }

$uploadExitCode = 0
$wc = New-Object 'System.Net.WebClient'

function Upload-TestResults {
  param([Parameter(ValueFromPipeline=$true)] $results)

  $results `
  | select -ExpandProperty FullName `
  | % {
    $wc.UploadFile("https://ci.appveyor.com/api/testresults/xunit/$($env:APPVEYOR_JOB_ID)", "$_")
    $global:uploadExitCode += $LASTEXITCODE
  }
}

function Upload-CoverageReport {
  param($coverage, $codecov)

  $CodeCovCmd = "$codecov -f $($coverage.FullName)"
  if ("$global:CodeCovToken" -ne '') {
    $CodeCovCmd += " -t $global:CodeCovToken"
  }
  Write-Host $CodeCovCmd
  Invoke-Expression $CodeCovCmd
  $global:uploadExitCode += $LASTEXITCODE
}

if ($env:APPVEYOR -eq 'True') {
  Get-ChildItem $test_results *.trx | Upload-TestResults
} else {
  Write-Host "Not in Appveyor (`$env:APPVEYOR not -eq 'True'); skipping test results upload"
}

Get-ChildItem $test_results *.xml | % { Upload-CoverageReport -coverage $_ -codecov $codecov }

if ($uploadExitCode -ne 0) { $host.SetShouldExit($uploadExitCode); throw; }
