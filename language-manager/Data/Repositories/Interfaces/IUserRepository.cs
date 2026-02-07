using language_manager.Model.Domain;

namespace language_manager.Data.Repositories.Interfaces;

/// <summary>
/// Repository interface for User entity operations.
/// </summary>
public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
