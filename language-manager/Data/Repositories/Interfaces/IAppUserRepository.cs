using language_manager.Model.Domain;

namespace language_manager.Data.Repositories.Interfaces;

/// <summary>
/// Repository interface for AppUser entity operations.
/// </summary>
public interface IAppUserRepository : IRepository<AppUser>
{
    Task<AppUser?> GetByCompositeKeyAsync(string userId, string appId, CancellationToken cancellationToken = default);
    Task<IEnumerable<AppUser>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<AppUser>> GetByAppIdAsync(string appId, CancellationToken cancellationToken = default);
    Task DeleteByCompositeKeyAsync(string userId, string appId, CancellationToken cancellationToken = default);
}
