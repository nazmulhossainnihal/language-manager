using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Attributes;

namespace language_manager.Model.Domain;

/// <summary>
/// Junction table representing the many-to-many relationship between Users and Apps.
/// Composite primary key: (UserId, AppId)
/// </summary>
[PrimaryKey(nameof(UserId), nameof(AppId))]
public class AppUser
{
    /// <summary>
    /// MongoDB document ID (composite of UserId:AppId)
    /// </summary>
    [BsonId]
    public string Id => $"{UserId}:{AppId}";

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
    [BsonIgnore]
    public User? User { get; set; }
    [BsonIgnore]
    public App? App { get; set; }
}
