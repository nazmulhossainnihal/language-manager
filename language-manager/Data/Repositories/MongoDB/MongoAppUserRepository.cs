using language_manager.Data.Context;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Model.Domain;
using MongoDB.Driver;

namespace language_manager.Data.Repositories.MongoDB;

/// <summary>
/// MongoDB implementation of the AppUser repository.
/// </summary>
public class MongoAppUserRepository : MongoRepository<AppUser>, IAppUserRepository
{
    public MongoAppUserRepository(MongoDbContext context)
        : base(context.AppUsers, nameof(AppUser.UserId))
    {
    }

    public override async Task<AppUser?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        // For AppUser, the id is expected to be in format "userId:appId"
        var parts = id.Split(':');
        if (parts.Length != 2) return null;
        return await GetByCompositeKeyAsync(parts[0], parts[1], cancellationToken);
    }

    public async Task<AppUser?> GetByCompositeKeyAsync(string userId, string appId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<AppUser>.Filter.And(
            Builders<AppUser>.Filter.Eq(au => au.UserId, userId),
            Builders<AppUser>.Filter.Eq(au => au.AppId, appId)
        );
        return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<AppUser>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(au => au.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<AppUser>> GetByAppIdAsync(string appId, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(au => au.AppId == appId).ToListAsync(cancellationToken);
    }

    public async Task DeleteByCompositeKeyAsync(string userId, string appId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<AppUser>.Filter.And(
            Builders<AppUser>.Filter.Eq(au => au.UserId, userId),
            Builders<AppUser>.Filter.Eq(au => au.AppId, appId)
        );
        await Collection.DeleteOneAsync(filter, cancellationToken);
    }
}
