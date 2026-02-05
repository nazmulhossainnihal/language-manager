using language_manager.Data.Context;
using MongoDB.Bson;
using MongoDB.Driver;

namespace language_manager.Data.Migrations;

/// <summary>
/// Runs database migrations and tracks applied migrations.
/// </summary>
public class MigrationRunner
{
    private readonly MongoDbContext _context;
    private readonly IEnumerable<IMigration> _migrations;
    private readonly ILogger<MigrationRunner> _logger;
    private readonly IMongoCollection<BsonDocument> _migrationHistory;

    public MigrationRunner(
        MongoDbContext context,
        IEnumerable<IMigration> migrations,
        ILogger<MigrationRunner> logger)
    {
        _context = context;
        _migrations = migrations;
        _logger = logger;
        _migrationHistory = context.Database.GetCollection<BsonDocument>("_migrations");
    }

    public async Task RunMigrationsAsync(CancellationToken cancellationToken = default)
    {
        var appliedMigrations = await GetAppliedMigrationsAsync(cancellationToken);

        foreach (var migration in _migrations.OrderBy(m => m.Order))
        {
            if (appliedMigrations.Contains(migration.Name))
            {
                _logger.LogDebug("Migration {MigrationName} already applied, skipping", migration.Name);
                continue;
            }

            _logger.LogInformation("Applying migration: {MigrationName}", migration.Name);

            try
            {
                await migration.UpAsync(cancellationToken);
                await RecordMigrationAsync(migration.Name, cancellationToken);
                _logger.LogInformation("Migration {MigrationName} applied successfully", migration.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to apply migration {MigrationName}", migration.Name);
                throw;
            }
        }
    }

    private async Task<HashSet<string>> GetAppliedMigrationsAsync(CancellationToken cancellationToken)
    {
        var migrations = await _migrationHistory
            .Find(_ => true)
            .ToListAsync(cancellationToken);

        return migrations.Select(m => m["name"].AsString).ToHashSet();
    }

    private async Task RecordMigrationAsync(string name, CancellationToken cancellationToken)
    {
        var document = new BsonDocument
        {
            { "name", name },
            { "appliedAt", DateTime.UtcNow }
        };
        await _migrationHistory.InsertOneAsync(document, cancellationToken: cancellationToken);
    }
}
