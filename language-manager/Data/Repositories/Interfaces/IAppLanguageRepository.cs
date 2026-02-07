using language_manager.Model.Domain;

namespace language_manager.Data.Repositories.Interfaces;

/// <summary>
/// Repository interface for AppLanguage entity operations.
/// </summary>
public interface IAppLanguageRepository : IRepository<AppLanguage>
{
    Task<AppLanguage?> GetByCompositeKeyAsync(string appId, string languageId, CancellationToken cancellationToken = default);
    Task<IEnumerable<AppLanguage>> GetByAppIdAsync(string appId, CancellationToken cancellationToken = default);
    Task DeleteByCompositeKeyAsync(string appId, string languageId, CancellationToken cancellationToken = default);
}
