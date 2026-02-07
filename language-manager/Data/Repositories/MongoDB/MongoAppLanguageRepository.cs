using language_manager.Data.Context;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Model.Domain;
using MongoDB.Driver;

namespace language_manager.Data.Repositories.MongoDB;

/// <summary>
/// MongoDB implementation of the AppLanguage repository.
/// </summary>
public class MongoAppLanguageRepository : MongoRepository<AppLanguage>, IAppLanguageRepository
{
    public MongoAppLanguageRepository(MongoDbContext context)
        : base(context.AppLanguages, nameof(AppLanguage.AppId))
    {
    }

    public override async Task<AppLanguage?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        // For AppLanguage, the id is expected to be in format "appId:languageId"
        var parts = id.Split(':');
        if (parts.Length != 2) return null;
        return await GetByCompositeKeyAsync(parts[0], parts[1], cancellationToken);
    }

    public async Task<AppLanguage?> GetByCompositeKeyAsync(string appId, string languageId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<AppLanguage>.Filter.And(
            Builders<AppLanguage>.Filter.Eq(al => al.AppId, appId),
            Builders<AppLanguage>.Filter.Eq(al => al.LanguageId, languageId)
        );
        return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<AppLanguage>> GetByAppIdAsync(string appId, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(al => al.AppId == appId).ToListAsync(cancellationToken);
    }

    public async Task DeleteByCompositeKeyAsync(string appId, string languageId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<AppLanguage>.Filter.And(
            Builders<AppLanguage>.Filter.Eq(al => al.AppId, appId),
            Builders<AppLanguage>.Filter.Eq(al => al.LanguageId, languageId)
        );
        await Collection.DeleteOneAsync(filter, cancellationToken);
    }
}
