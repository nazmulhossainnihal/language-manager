using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;

namespace language_manager.Model.Domain;

/// <summary>
/// Represents a module within an application for organizing translations.
/// </summary>
public class Module
{
    /// <summary>
    /// Primary key for the module.
    /// </summary>
    [Key]
    [BsonId]
    public required string ModuleId { get; init; }

    /// <summary>
    /// Foreign key to App.
    /// </summary>
    [ForeignKey(nameof(App))]
    public required string AppId { get; set; }

    public required string ModuleKey { get; set; }

    public required string Name { get; set; }

    // Navigation properties
    [BsonIgnore]
    public App? App { get; set; }
    [BsonIgnore]
    public ICollection<Translation> Translations { get; init; } = new List<Translation>();
}
