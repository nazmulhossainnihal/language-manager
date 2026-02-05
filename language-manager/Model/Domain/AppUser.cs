using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace language_manager.Model.Domain;

/// <summary>
/// Junction table representing the many-to-many relationship between Users and Apps.
/// Composite primary key: (UserId, AppId)
/// </summary>
[PrimaryKey(nameof(UserId), nameof(AppId))]
public class AppUser
{
    /// <summary>
    /// Primary key (part 1), Foreign key to User.
    /// </summary>
    [ForeignKey(nameof(User))]
    public required string UserId { get; init; }

    /// <summary>
    /// Primary key (part 2), Foreign key to App.
    /// </summary>
    [ForeignKey(nameof(App))]
    public required string AppId { get; init; }

    public required string Role { get; set; }

    // Navigation properties
    public User? User { get; set; }
    public App? App { get; set; }
}
