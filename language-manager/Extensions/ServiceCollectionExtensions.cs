using System.Text;
using language_manager.Data.Context;
using language_manager.Data.Migrations;
using language_manager.Data.Migrations.Migrations;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Data.Repositories.MongoDB;
using language_manager.Data.Seeds;
using language_manager.Data.Seeds.Seeders;
using language_manager.Data.Settings;
using language_manager.Services;
using language_manager.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
        services.AddScoped<ISeeder, UserSeeder>();
        services.AddScoped<SeederRunner>();

        // Register services
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()!;

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization();

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
