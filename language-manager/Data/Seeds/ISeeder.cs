namespace language_manager.Data.Seeds;

/// <summary>
/// Interface for database seeders.
/// </summary>
public interface ISeeder
{
    string Name { get; }
    int Order { get; }
    Task SeedAsync(CancellationToken cancellationToken = default);
}
