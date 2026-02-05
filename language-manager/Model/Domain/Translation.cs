using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;

namespace language_manager.Model.Domain;

/// <summary>
/// Represents a translation entry for a specific key in a language and module.
/// </summary>
public class Translation
{
    /// <summary>
    /// Primary key for the translation.
    /// </summary>
    [Key]
    [BsonId]
    public required string TranslationId { get; init; }

    /// <summary>
    /// Foreign key to App.
    /// </summary>
    [ForeignKey(nameof(App))]
    public required string AppId { get; set; }

    /// <summary>
    /// Foreign key to Module.
    /// </summary>
    [ForeignKey(nameof(Module))]
    public required string ModuleId { get; set; }

    /// <summary>
    /// Foreign key to Language.
    /// </summary>
    [ForeignKey(nameof(Language))]
    public required string LanguageId { get; set; }

    public required string Key { get; set; }

    public required string Text { get; set; }

    public string? Context { get; set; }

    // Navigation properties
    [BsonIgnore]
    public App? App { get; set; }
    [BsonIgnore]
    public Module? Module { get; set; }
    [BsonIgnore]
    public Language? Language { get; set; }
}
