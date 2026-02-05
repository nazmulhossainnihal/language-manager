using language_manager.Model.Domain;

namespace language_manager.Data.Repositories.Interfaces;

/// <summary>
/// Repository interface for Language entity operations.
/// </summary>
public interface ILanguageRepository : IRepository<Language>
{
    Task<Language?> GetByLanguageKeyAsync(string languageKey, CancellationToken cancellationToken = default);
    Task<IEnumerable<Language>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);
}
