using language_manager.Data.Context;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Model.Domain;
using MongoDB.Driver;

namespace language_manager.Data.Repositories.MongoDB;

/// <summary>
/// MongoDB implementation of the Translation repository.
/// </summary>
public class MongoTranslationRepository : MongoRepository<Translation>, ITranslationRepository
{
    public MongoTranslationRepository(MongoDbContext context)
        : base(context.Translations, nameof(Translation.TranslationId))
    {
    }

    public async Task<IEnumerable<Translation>> GetByAppIdAsync(string appId, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(t => t.AppId == appId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Translation>> GetByModuleIdAsync(string moduleId, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(t => t.ModuleId == moduleId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Translation>> GetByLanguageIdAsync(string languageId, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(t => t.LanguageId == languageId).ToListAsync(cancellationToken);
    }

    public async Task<Translation?> GetByKeyAsync(string appId, string moduleId, string languageId, string key, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Translation>.Filter.And(
            Builders<Translation>.Filter.Eq(t => t.AppId, appId),
            Builders<Translation>.Filter.Eq(t => t.ModuleId, moduleId),
            Builders<Translation>.Filter.Eq(t => t.LanguageId, languageId),
            Builders<Translation>.Filter.Eq(t => t.Key, key)
        );
        return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Translation>> GetByAppAndLanguageAsync(string appId, string languageId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Translation>.Filter.And(
            Builders<Translation>.Filter.Eq(t => t.AppId, appId),
            Builders<Translation>.Filter.Eq(t => t.LanguageId, languageId)
        );
        return await Collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Translation>> GetByModuleAndLanguageAsync(string moduleId, string languageId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Translation>.Filter.And(
            Builders<Translation>.Filter.Eq(t => t.ModuleId, moduleId),
            Builders<Translation>.Filter.Eq(t => t.LanguageId, languageId)
        );
        return await Collection.Find(filter).ToListAsync(cancellationToken);
    }
}
