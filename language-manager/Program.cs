using System.Reflection;
using language_manager.Data.Seeds;
using language_manager.Data.Seeds.Seeders;
using language_manager.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Language Manager API",
        Version = "v1",
        Description = "API for managing translations and languages across multiple applications"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Add MongoDB services
builder.Services.AddMongoDb(builder.Configuration);

// Add JWT authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

// Register additional seeders only in development
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddScoped<ISeeder, LanguageSeeder>();
    builder.Services.AddScoped<ISeeder, DummyDataSeeder>();
}

var app = builder.Build();

// Run migrations and seeders
await app.RunDatabaseMigrationsAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
