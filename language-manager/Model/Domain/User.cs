using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace language_manager.Model.Domain;

/// <summary>
/// Represents a user in the system.
/// </summary>
public class User
{
    /// <summary>
    /// Primary key for the user.
    /// </summary>
    [Key]
    [BsonId]
    public required string UserId { get; init; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    // Navigation properties
    [BsonIgnore]
    public ICollection<AppUser> AppUsers { get; init; } = new List<AppUser>();
}
