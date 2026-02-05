using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace language_manager.Model.Domain;

/// <summary>
/// Represents an application that uses the translation system.
/// </summary>
public class App
{
    /// <summary>
    /// Primary key for the app.
    /// </summary>
    [Key]
    public required string AppId { get; init; }

    public required string Domain { get; set; }

    public required string Name { get; set; }

    /// <summary>
    /// Foreign key to Language - the default language for this app.
    /// </summary>
    [ForeignKey(nameof(DefaultLanguage))]
    public required string DefaultLanguageId { get; set; }

    public required string Environment { get; set; }

    // Navigation properties
    public Language? DefaultLanguage { get; set; }
    public ICollection<AppUser> AppUsers { get; init; } = new List<AppUser>();
    public ICollection<Module> Modules { get; init; } = new List<Module>();
    public ICollection<Translation> Translations { get; init; } = new List<Translation>();
}
