# tlycken.Extensions

A small library of utility extensions I've used in commercial and OSS projects.

# Extensions on `Task<IEnumerable<T>>`

I've always found it inconvenient to first have to `await` a `Task<IEnumerable<T>>`
before iterating it to a list or other collection. Therefore, there are a few
extension methods that attach continuations that do this.

Given a method with the following signature

    Task<IEnumerable<T>> GetMany<T>();

you can do the following:

    List<T> list = await GetMany<T>().ToList();
    IReadOnlyCollection<T> roc = await GetMany<T>().ToReadOnlyCollection();

# Extensions for function application

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
