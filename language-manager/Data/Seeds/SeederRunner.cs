using language_manager.Data.Context;
using MongoDB.Bson;
using MongoDB.Driver;

namespace language_manager.Data.Seeds;

/// <summary>
/// Runs database seeders and tracks applied seeds.
/// </summary>
public class SeederRunner
{
    private readonly MongoDbContext _context;
    private readonly IEnumerable<ISeeder> _seeders;
    private readonly ILogger<SeederRunner> _logger;
    private readonly IMongoCollection<BsonDocument> _seedHistory;

    public SeederRunner(
        MongoDbContext context,
        IEnumerable<ISeeder> seeders,
        ILogger<SeederRunner> logger)
    {
        _context = context;
        _seeders = seeders;
        _logger = logger;
        _seedHistory = context.Database.GetCollection<BsonDocument>("_seeds");
    }

    public async Task RunSeedersAsync(CancellationToken cancellationToken = default)
    {
        var appliedSeeds = await GetAppliedSeedsAsync(cancellationToken);

        foreach (var seeder in _seeders.OrderBy(s => s.Order))
        {
            if (appliedSeeds.Contains(seeder.Name))
            {
                _logger.LogDebug("Seed {SeederName} already applied, skipping", seeder.Name);
                continue;
            }

            _logger.LogInformation("Applying seed: {SeederName}", seeder.Name);

            try
            {
                await seeder.SeedAsync(cancellationToken);
                await RecordSeedAsync(seeder.Name, cancellationToken);
                _logger.LogInformation("Seed {SeederName} applied successfully", seeder.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to apply seed {SeederName}", seeder.Name);
                throw;
            }
        }
    }

    private async Task<HashSet<string>> GetAppliedSeedsAsync(CancellationToken cancellationToken)
    {
        var seeds = await _seedHistory
            .Find(_ => true)
            .ToListAsync(cancellationToken);

        return seeds.Select(s => s["name"].AsString).ToHashSet();
    }

    private async Task RecordSeedAsync(string name, CancellationToken cancellationToken)
    {
        var document = new BsonDocument
        {
            { "name", name },
            { "appliedAt", DateTime.UtcNow }
        };
        await _seedHistory.InsertOneAsync(document, cancellationToken: cancellationToken);
    }
}
