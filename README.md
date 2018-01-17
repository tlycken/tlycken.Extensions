# tlycken.Extensions

A small library of utility extensions I've used in commercial and OSS projects.

[![AppVeyor](https://img.shields.io/appveyor/ci/tlycken/tlycken-extensions.svg?style=flat-square)](https://ci.appveyor.com/project/tlycken/tlycken-extensions)
[![AppVeyor tests](https://img.shields.io/appveyor/tests/tlycken/tlycken-extensions.svg?style=flat-square)](https://ci.appveyor.com/project/tlycken/tlycken-extensions)
[![CodeCov.io](https://codecov.io/gh/tlycken/tlycken.Extensions/branch/master/graph/badge.svg)](https://codecov.io/gh/tlycken/tlycken.Extensions)
[![NuGet](https://img.shields.io/nuget/v/tlycken.Extensions.svg?style=flat-square)](https://www.nuget.org/packages/tlycken.extensions)
[![NuGet](https://img.shields.io/nuget/dt/tlycken.Extensions.svg?style=flat-square)](http://nuget.org/packages/tlycken.extensions)

## Extensions on `string`

* `"a sample string".ToTitleCase()` -> `"A Sample String"`.

* `"iota ıota".ToTitleCase(new CultureInfo("tr-TR"))` -> `"İota Iota"`.

## Extensions on `DateTime`

* `new DateTime(2017, 03, 13).MonthName()` -> `"March"`

* `new DateTime(2017, 11, 13).MonthName(new CultureInfo("sv-SE"))` -> `"mars"`.

## Extensions on `IEnumerable<T>`

* `Enumerable.Range(0,5).Concat(Enumerable.Range(1,2)).Select(i => new Foo { Bar = i }).Distinct(foo => foo.Bar)`
  -> elements with unique `Bar` values

* `Enumerable.Range(0,5).Select(i => i + 3).Indexed().ToArray()` -> `[(3,0), (4,1), (5,2), (6,3), (7,4)]`,
  i.e. for each element it returns a tuple with the value in the first position
  and the index in the second position.

## Extensions on `Task<IEnumerable<T>>`

I've always found it inconvenient to first have to `await` a `Task<IEnumerable<T>>`
before iterating it to a list or other collection. Therefore, there are a few
extension methods that attach continuations that do this.

Given a method with the following signature

    Task<IEnumerable<T>> GetMany<T>();

you can do the following:

    List<T> list = await GetMany<T>().ToList();
    IReadOnlyCollection<T> roc = await GetMany<T>().ToReadOnlyCollection();

## Extensions for function application

A pattern I've used a couple of times is to accept a projection from
`IQueryable<S>` to `IQueryable<T>` as part of a repository implementation:

    public Task<IReadOnlyCollection<T>> GetMany<T>(Func<IQueryable<S>, IQueryable<T>> projection)
    {
        return _context.Set<T>() // _context is an EF db context
            .Apply(projection)
            .ToListAsync()
            .ToReadOnlyCollection(); // extension from above
    }

However, `Apply` here is a more general concept, so it's implemented to allow any
function application like such:

    Func<S, T> func = s => MakeT(s);
    S s = GetS();
    T t = s.Apply(func);
