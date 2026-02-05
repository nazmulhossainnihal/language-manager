using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Languages.Queries;

public record GetAllLanguagesQuery : IRequest<Result<IEnumerable<LanguageDto>>>;

public class GetAllLanguagesQueryHandler : IRequestHandler<GetAllLanguagesQuery, Result<IEnumerable<LanguageDto>>>
{
    private readonly ILanguageRepository _languageRepository;

    public GetAllLanguagesQueryHandler(ILanguageRepository languageRepository)
    {
        _languageRepository = languageRepository;
    }

    public async Task<Result<IEnumerable<LanguageDto>>> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken)
    {
        var languages = await _languageRepository.GetAllAsync(cancellationToken);
        var dtos = languages.Select(l => new LanguageDto(l.LanguageId, l.LanguageKey, l.Name));
        return Result<IEnumerable<LanguageDto>>.Success(dtos);
    }
}
