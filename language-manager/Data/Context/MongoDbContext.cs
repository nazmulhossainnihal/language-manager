using language_manager.Data.Settings;
using language_manager.Model.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace language_manager.Data.Context;

/// <summary>
/// MongoDB database context providing access to all collections.
/// </summary>
public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("users");
    public IMongoCollection<App> Apps => _database.GetCollection<App>("apps");
    public IMongoCollection<AppUser> AppUsers => _database.GetCollection<AppUser>("app_users");
    public IMongoCollection<Language> Languages => _database.GetCollection<Language>("languages");
    public IMongoCollection<Module> Modules => _database.GetCollection<Module>("modules");
    public IMongoCollection<Translation> Translations => _database.GetCollection<Translation>("translations");

    public IMongoDatabase Database => _database;
}
