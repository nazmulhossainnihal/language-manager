using language_manager.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MongoDB services
builder.Services.AddMongoDb(builder.Configuration);

var app = builder.Build();

// Run migrations and seeders
await app.RunDatabaseMigrationsAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
