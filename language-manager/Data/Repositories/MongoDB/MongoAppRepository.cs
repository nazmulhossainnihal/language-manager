using language_manager.Data.Context;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Model.Domain;
using MongoDB.Driver;

namespace language_manager.Data.Repositories.MongoDB;

/// <summary>
/// MongoDB implementation of the App repository.
/// </summary>
public class MongoAppRepository : MongoRepository<App>, IAppRepository
{
    public MongoAppRepository(MongoDbContext context)
        : base(context.Apps, nameof(App.AppId))
    {
    }

    public async Task<App?> GetByDomainAsync(string domain, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(a => a.Domain == domain).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<App>> GetByEnvironmentAsync(string environment, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(a => a.Environment == environment).ToListAsync(cancellationToken);
    }
}
