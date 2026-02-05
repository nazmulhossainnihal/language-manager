using language_manager.Model.Domain;

namespace language_manager.Data.Repositories.Interfaces;

/// <summary>
/// Repository interface for Translation entity operations.
/// </summary>
public interface ITranslationRepository : IRepository<Translation>
{
    Task<IEnumerable<Translation>> GetByAppIdAsync(string appId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Translation>> GetByModuleIdAsync(string moduleId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Translation>> GetByLanguageIdAsync(string languageId, CancellationToken cancellationToken = default);
    Task<Translation?> GetByKeyAsync(string appId, string moduleId, string languageId, string key, CancellationToken cancellationToken = default);
    Task<IEnumerable<Translation>> GetByAppAndLanguageAsync(string appId, string languageId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Translation>> GetByModuleAndLanguageAsync(string moduleId, string languageId, CancellationToken cancellationToken = default);
}
