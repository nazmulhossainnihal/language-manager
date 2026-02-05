namespace language_manager.Data.Settings;

/// <summary>
/// Configuration settings for MongoDB connection.
/// </summary>
public class MongoDbSettings
{
    public const string SectionName = "MongoDb";

    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
}
