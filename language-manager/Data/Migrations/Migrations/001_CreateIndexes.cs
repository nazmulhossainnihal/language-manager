using language_manager.Data.Context;
using language_manager.Model.Domain;
using MongoDB.Driver;

namespace language_manager.Data.Migrations.Migrations;

/// <summary>
/// Creates indexes for all collections.
/// </summary>
public class CreateIndexesMigration : IMigration
{
    private readonly MongoDbContext _context;

    public CreateIndexesMigration(MongoDbContext context)
    {
        _context = context;
    }

    public string Name => "001_CreateIndexes";
    public int Order => 1;

    public async Task UpAsync(CancellationToken cancellationToken = default)
    {
        // Language indexes
        await _context.Languages.Indexes.CreateManyAsync(new[]
        {
            new CreateIndexModel<Language>(
                Builders<Language>.IndexKeys.Ascending(l => l.LanguageKey),
                new CreateIndexOptions { Unique = true, Name = "idx_language_key" }),
            new CreateIndexModel<Language>(
                Builders<Language>.IndexKeys.Ascending(l => l.Name),
                new CreateIndexOptions { Name = "idx_language_name" })
        }, cancellationToken);

        // User indexes
        await _context.Users.Indexes.CreateManyAsync(new[]
        {
            new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(u => u.Email),
                new CreateIndexOptions { Unique = true, Name = "idx_user_email" })
        }, cancellationToken);

        // App indexes
        await _context.Apps.Indexes.CreateManyAsync(new[]
        {
            new CreateIndexModel<App>(
                Builders<App>.IndexKeys.Ascending(a => a.Domain),
                new CreateIndexOptions { Unique = true, Name = "idx_app_domain" }),
            new CreateIndexModel<App>(
                Builders<App>.IndexKeys.Ascending(a => a.Environment),
                new CreateIndexOptions { Name = "idx_app_environment" }),
            new CreateIndexModel<App>(
                Builders<App>.IndexKeys.Ascending(a => a.DefaultLanguageId),
                new CreateIndexOptions { Name = "idx_app_default_language" })
        }, cancellationToken);

        // AppUser indexes (composite key simulation)
        await _context.AppUsers.Indexes.CreateManyAsync(new[]
        {
            new CreateIndexModel<AppUser>(
                Builders<AppUser>.IndexKeys
                    .Ascending(au => au.UserId)
                    .Ascending(au => au.AppId),
                new CreateIndexOptions { Unique = true, Name = "idx_appuser_composite" }),
            new CreateIndexModel<AppUser>(
                Builders<AppUser>.IndexKeys.Ascending(au => au.UserId),
                new CreateIndexOptions { Name = "idx_appuser_user" }),
            new CreateIndexModel<AppUser>(
                Builders<AppUser>.IndexKeys.Ascending(au => au.AppId),
                new CreateIndexOptions { Name = "idx_appuser_app" })
        }, cancellationToken);

        // Module indexes
        await _context.Modules.Indexes.CreateManyAsync(new[]
        {
            new CreateIndexModel<Module>(
                Builders<Module>.IndexKeys.Ascending(m => m.AppId),
                new CreateIndexOptions { Name = "idx_module_app" }),
            new CreateIndexModel<Module>(
                Builders<Module>.IndexKeys
                    .Ascending(m => m.AppId)
                    .Ascending(m => m.ModuleKey),
                new CreateIndexOptions { Unique = true, Name = "idx_module_app_key" })
        }, cancellationToken);

        // Translation indexes
        await _context.Translations.Indexes.CreateManyAsync(new[]
        {
            new CreateIndexModel<Translation>(
                Builders<Translation>.IndexKeys.Ascending(t => t.AppId),
                new CreateIndexOptions { Name = "idx_translation_app" }),
            new CreateIndexModel<Translation>(
                Builders<Translation>.IndexKeys.Ascending(t => t.ModuleId),
                new CreateIndexOptions { Name = "idx_translation_module" }),
            new CreateIndexModel<Translation>(
                Builders<Translation>.IndexKeys.Ascending(t => t.LanguageId),
                new CreateIndexOptions { Name = "idx_translation_language" }),
            new CreateIndexModel<Translation>(
                Builders<Translation>.IndexKeys
                    .Ascending(t => t.AppId)
                    .Ascending(t => t.ModuleId)
                    .Ascending(t => t.LanguageId)
                    .Ascending(t => t.Key),
                new CreateIndexOptions { Unique = true, Name = "idx_translation_unique_key" }),
            new CreateIndexModel<Translation>(
                Builders<Translation>.IndexKeys
                    .Ascending(t => t.AppId)
                    .Ascending(t => t.LanguageId),
                new CreateIndexOptions { Name = "idx_translation_app_language" })
        }, cancellationToken);
    }

    public async Task DownAsync(CancellationToken cancellationToken = default)
    {
        // Drop all custom indexes
        await _context.Languages.Indexes.DropOneAsync("idx_language_key", cancellationToken);
        await _context.Languages.Indexes.DropOneAsync("idx_language_name", cancellationToken);

        await _context.Users.Indexes.DropOneAsync("idx_user_email", cancellationToken);

        await _context.Apps.Indexes.DropOneAsync("idx_app_domain", cancellationToken);
        await _context.Apps.Indexes.DropOneAsync("idx_app_environment", cancellationToken);
        await _context.Apps.Indexes.DropOneAsync("idx_app_default_language", cancellationToken);

        await _context.AppUsers.Indexes.DropOneAsync("idx_appuser_composite", cancellationToken);
        await _context.AppUsers.Indexes.DropOneAsync("idx_appuser_user", cancellationToken);
        await _context.AppUsers.Indexes.DropOneAsync("idx_appuser_app", cancellationToken);

        await _context.Modules.Indexes.DropOneAsync("idx_module_app", cancellationToken);
        await _context.Modules.Indexes.DropOneAsync("idx_module_app_key", cancellationToken);

        await _context.Translations.Indexes.DropOneAsync("idx_translation_app", cancellationToken);
        await _context.Translations.Indexes.DropOneAsync("idx_translation_module", cancellationToken);
        await _context.Translations.Indexes.DropOneAsync("idx_translation_language", cancellationToken);
        await _context.Translations.Indexes.DropOneAsync("idx_translation_unique_key", cancellationToken);
        await _context.Translations.Indexes.DropOneAsync("idx_translation_app_language", cancellationToken);
    }
}
