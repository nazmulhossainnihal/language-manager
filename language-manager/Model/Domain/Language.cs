using System.ComponentModel.DataAnnotations;

namespace language_manager.Model.Domain;

/// <summary>
/// Represents a language available for translations.
/// </summary>
public class Language
{
    /// <summary>
    /// Primary key for the language.
    /// </summary>
    [Key]
    public required string LanguageId { get; init; }

    public required string LanguageKey { get; set; }

    public required string Name { get; set; }

    // Navigation properties
    public ICollection<App> AppsWithDefaultLanguage { get; init; } = new List<App>();
    public ICollection<Translation> Translations { get; init; } = new List<Translation>();
}
