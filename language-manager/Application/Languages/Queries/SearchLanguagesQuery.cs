using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Languages.Queries;

public record SearchLanguagesQuery(string SearchTerm) : IRequest<Result<IEnumerable<LanguageDto>>>;

public class SearchLanguagesQueryHandler : IRequestHandler<SearchLanguagesQuery, Result<IEnumerable<LanguageDto>>>
{
    private readonly ILanguageRepository _languageRepository;

    public SearchLanguagesQueryHandler(ILanguageRepository languageRepository)
    {
        _languageRepository = languageRepository;
    }

    public async Task<Result<IEnumerable<LanguageDto>>> Handle(SearchLanguagesQuery request, CancellationToken cancellationToken)
    {
        var languages = await _languageRepository.SearchByNameAsync(request.SearchTerm, cancellationToken);
        var dtos = languages.Select(l => new LanguageDto(l.LanguageId, l.LanguageKey, l.Name));
        return Result<IEnumerable<LanguageDto>>.Success(dtos);
    }
}
