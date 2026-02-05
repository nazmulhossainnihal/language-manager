using language_manager.Data.Context;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Model.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace language_manager.Data.Repositories.MongoDB;

/// <summary>
/// MongoDB implementation of the Language repository.
/// </summary>
public class MongoLanguageRepository : MongoRepository<Language>, ILanguageRepository
{
    public MongoLanguageRepository(MongoDbContext context)
        : base(context.Languages, nameof(Language.LanguageId))
    {
    }

    public async Task<Language?> GetByLanguageKeyAsync(string languageKey, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(l => l.LanguageKey == languageKey).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Language>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Language>.Filter.Regex(l => l.Name, new BsonRegularExpression(searchTerm, "i"));
        return await Collection.Find(filter).ToListAsync(cancellationToken);
    }
}
