using System.Linq.Expressions;
using language_manager.Data.Repositories.Interfaces;
using MongoDB.Driver;

namespace language_manager.Data.Repositories.MongoDB;

/// <summary>
/// Generic MongoDB repository implementation.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public abstract class MongoRepository<T> : IRepository<T> where T : class
{
    protected readonly IMongoCollection<T> Collection;
    protected readonly string IdFieldName;

    protected MongoRepository(IMongoCollection<T> collection, string idFieldName)
    {
        Collection = collection;
        IdFieldName = idFieldName;
    }

    public virtual async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq(IdFieldName, id);
        return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Collection.Find(_ => true).ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(predicate).ToListAsync(cancellationToken);
    }

    public virtual async Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(predicate).AnyAsync(cancellationToken);
    }

    public virtual async Task<long> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        if (predicate == null)
            return await Collection.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken);

        return await Collection.CountDocumentsAsync(predicate, cancellationToken: cancellationToken);
    }

    public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await Collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public virtual async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        var entityList = entities.ToList();
        if (entityList.Count > 0)
        {
            await Collection.InsertManyAsync(entityList, cancellationToken: cancellationToken);
        }
    }

    public virtual async Task UpdateAsync(string id, T entity, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq(IdFieldName, id);
        await Collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
    }

    public virtual async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq(IdFieldName, id);
        await Collection.DeleteOneAsync(filter, cancellationToken);
    }

    public virtual async Task DeleteManyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        await Collection.DeleteManyAsync(predicate, cancellationToken);
    }
}
