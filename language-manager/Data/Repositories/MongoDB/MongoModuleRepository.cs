using language_manager.Data.Context;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Model.Domain;
using MongoDB.Driver;

namespace language_manager.Data.Repositories.MongoDB;

/// <summary>
/// MongoDB implementation of the Module repository.
/// </summary>
public class MongoModuleRepository : MongoRepository<Module>, IModuleRepository
{
    public MongoModuleRepository(MongoDbContext context)
        : base(context.Modules, nameof(Module.ModuleId))
    {
    }

    public async Task<IEnumerable<Module>> GetByAppIdAsync(string appId, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(m => m.AppId == appId).ToListAsync(cancellationToken);
    }

    public async Task<Module?> GetByModuleKeyAsync(string appId, string moduleKey, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Module>.Filter.And(
            Builders<Module>.Filter.Eq(m => m.AppId, appId),
            Builders<Module>.Filter.Eq(m => m.ModuleKey, moduleKey)
        );
        return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }
}
