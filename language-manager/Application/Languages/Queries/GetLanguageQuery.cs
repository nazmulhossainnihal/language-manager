using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Languages.Queries;

public record GetLanguageQuery(string LanguageId) : IRequest<Result<LanguageDto>>;

public class GetLanguageQueryHandler : IRequestHandler<GetLanguageQuery, Result<LanguageDto>>
{
    private readonly ILanguageRepository _languageRepository;

    public GetLanguageQueryHandler(ILanguageRepository languageRepository)
    {
        _languageRepository = languageRepository;
    }

    public async Task<Result<LanguageDto>> Handle(GetLanguageQuery request, CancellationToken cancellationToken)
    {
        var language = await _languageRepository.GetByIdAsync(request.LanguageId, cancellationToken);

        if (language == null)
        {
            return Result<LanguageDto>.NotFound("Language not found");
        }

        var dto = new LanguageDto(language.LanguageId, language.LanguageKey, language.Name);
        return Result<LanguageDto>.Success(dto);
    }
}
