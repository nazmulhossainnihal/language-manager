using language_manager.Model.Domain;

namespace language_manager.Data.Repositories.Interfaces;

/// <summary>
/// Repository interface for App entity operations.
/// </summary>
public interface IAppRepository : IRepository<App>
{
    Task<App?> GetByDomainAsync(string domain, CancellationToken cancellationToken = default);
    Task<IEnumerable<App>> GetByEnvironmentAsync(string environment, CancellationToken cancellationToken = default);
}
