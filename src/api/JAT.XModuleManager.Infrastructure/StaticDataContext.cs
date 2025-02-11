namespace JAT.XModuleManager.Infrastructure;

public class StaticDataContext<T> where T : class
{
    private static readonly List<T> _dataContext = [];

    public async Task AddAsync(T item, CancellationToken cancellationToken)
    {
        _dataContext.Add(item);
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await Task.FromResult(_dataContext);
    }
}
