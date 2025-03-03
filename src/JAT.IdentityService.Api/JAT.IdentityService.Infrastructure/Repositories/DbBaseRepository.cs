using System.Linq.Expressions;
using JAT.Core.Domain.Entities;
using JAT.Core.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JAT.IdentityService.Infrastructure.Repositories;

public class DbBaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly DbContext _dbContext;
    protected readonly DbSet<TEntity> DbSet;

    protected DbBaseRepository(DbContext context)
    {
        _dbContext = context;
        DbSet = _dbContext.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await DbSet.AddAsync(entity, cancellationToken);
    }

    public void Delete(TEntity entity)
    {
        entity.Deleted = true;
        DbSet.Update(entity);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContext.Dispose();
        }
    }

    public async Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await DbSet.AnyAsync(entity => entity.Id == id, cancellationToken);
    }

    public IQueryable<TEntity> GetAllAsync()
    {
        return DbSet;
    }

    public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await DbSet.FindAsync([id], cancellationToken);
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }
}
