namespace language_manager.Data.Migrations;

/// <summary>
/// Interface for database migrations.
/// </summary>
public interface IMigration
{
    string Name { get; }
    int Order { get; }
    Task UpAsync(CancellationToken cancellationToken = default);
    Task DownAsync(CancellationToken cancellationToken = default);
}
