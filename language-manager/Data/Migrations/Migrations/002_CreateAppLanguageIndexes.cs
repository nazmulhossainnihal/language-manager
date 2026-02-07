using language_manager.Data.Context;
using language_manager.Model.Domain;
using MongoDB.Driver;

namespace language_manager.Data.Migrations.Migrations;

/// <summary>
/// Creates indexes for the app_languages collection.
/// </summary>
public class CreateAppLanguageIndexesMigration : IMigration
{
    private readonly MongoDbContext _context;

    public CreateAppLanguageIndexesMigration(MongoDbContext context)
    {
        _context = context;
    }

    public string Name => "002_CreateAppLanguageIndexes";
    public int Order => 2;

    public async Task UpAsync(CancellationToken cancellationToken = default)
    {
        await _context.AppLanguages.Indexes.CreateManyAsync(new[]
        {
            new CreateIndexModel<AppLanguage>(
                Builders<AppLanguage>.IndexKeys
                    .Ascending(al => al.AppId)
                    .Ascending(al => al.LanguageId),
                new CreateIndexOptions { Unique = true, Name = "idx_applanguage_composite" }),
            new CreateIndexModel<AppLanguage>(
                Builders<AppLanguage>.IndexKeys.Ascending(al => al.AppId),
                new CreateIndexOptions { Name = "idx_applanguage_app" }),
            new CreateIndexModel<AppLanguage>(
                Builders<AppLanguage>.IndexKeys.Ascending(al => al.LanguageId),
                new CreateIndexOptions { Name = "idx_applanguage_language" })
        }, cancellationToken);
    }

    public async Task DownAsync(CancellationToken cancellationToken = default)
    {
        await _context.AppLanguages.Indexes.DropOneAsync("idx_applanguage_composite", cancellationToken);
        await _context.AppLanguages.Indexes.DropOneAsync("idx_applanguage_app", cancellationToken);
        await _context.AppLanguages.Indexes.DropOneAsync("idx_applanguage_language", cancellationToken);
    }
}
