using language_manager.Data.Context;
using language_manager.Data.Migrations;
using language_manager.Data.Migrations.Migrations;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Data.Repositories.MongoDB;
using language_manager.Data.Seeds;
using language_manager.Data.Seeds.Seeders;
using language_manager.Data.Settings;

namespace language_manager.Extensions;

/// <summary>
/// Extension methods for service collection configuration.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds MongoDB services to the dependency injection container.
    /// </summary>
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure MongoDB settings
        services.Configure<MongoDbSettings>(configuration.GetSection(MongoDbSettings.SectionName));

        // Register MongoDB context
        services.AddSingleton<MongoDbContext>();

        // Register repositories
        services.AddScoped<ILanguageRepository, MongoLanguageRepository>();
        services.AddScoped<IUserRepository, MongoUserRepository>();
        services.AddScoped<IAppRepository, MongoAppRepository>();
        services.AddScoped<IAppUserRepository, MongoAppUserRepository>();
        services.AddScoped<IModuleRepository, MongoModuleRepository>();
        services.AddScoped<ITranslationRepository, MongoTranslationRepository>();

        // Register migrations
        services.AddScoped<IMigration, CreateIndexesMigration>();
        services.AddScoped<MigrationRunner>();

        // Register seeders
        services.AddScoped<ISeeder, LanguageSeeder>();
        services.AddScoped<SeederRunner>();

        return services;
    }

    /// <summary>
    /// Runs database migrations and seeders.
    /// </summary>
    public static async Task<IHost> RunDatabaseMigrationsAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();

        try
        {
            logger.LogInformation("Running database migrations...");
            var migrationRunner = services.GetRequiredService<MigrationRunner>();
            await migrationRunner.RunMigrationsAsync();

            logger.LogInformation("Running database seeders...");
            var seederRunner = services.GetRequiredService<SeederRunner>();
            await seederRunner.RunSeedersAsync();

            logger.LogInformation("Database initialization completed successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database");
            throw;
        }

        return host;
    }
}
