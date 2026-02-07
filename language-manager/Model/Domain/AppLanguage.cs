using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Attributes;

namespace language_manager.Model.Domain;

/// <summary>
/// Junction table representing the languages enabled for a specific app.
/// Composite primary key: (AppId, LanguageId)
/// </summary>
[PrimaryKey(nameof(AppId), nameof(LanguageId))]
public class AppLanguage
{
    /// <summary>
    /// MongoDB document ID (composite of AppId:LanguageId)
    /// </summary>
    [BsonId]
    public string Id => $"{AppId}:{LanguageId}";

    /// <summary>
    /// Primary key (part 1), Foreign key to App.
    /// </summary>
    [ForeignKey(nameof(App))]
    public required string AppId { get; init; }

    /// <summary>
    /// Primary key (part 2), Foreign key to Language.
    /// </summary>
    [ForeignKey(nameof(Language))]
    public required string LanguageId { get; init; }

    // Navigation properties
    [BsonIgnore]
    public App? App { get; set; }
    [BsonIgnore]
    public Language? Language { get; set; }
}
