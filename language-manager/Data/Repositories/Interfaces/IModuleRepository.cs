using language_manager.Model.Domain;

namespace language_manager.Data.Repositories.Interfaces;

/// <summary>
/// Repository interface for Module entity operations.
/// </summary>
public interface IModuleRepository : IRepository<Module>
{
    Task<IEnumerable<Module>> GetByAppIdAsync(string appId, CancellationToken cancellationToken = default);
    Task<Module?> GetByModuleKeyAsync(string appId, string moduleKey, CancellationToken cancellationToken = default);
}
