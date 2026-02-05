using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Translations.Queries;

public record GetTranslationQuery(string TranslationId) : IRequest<Result<TranslationDto>>;

public class GetTranslationQueryHandler : IRequestHandler<GetTranslationQuery, Result<TranslationDto>>
{
    private readonly ITranslationRepository _translationRepository;

    public GetTranslationQueryHandler(ITranslationRepository translationRepository)
    {
        _translationRepository = translationRepository;
    }

    public async Task<Result<TranslationDto>> Handle(GetTranslationQuery request, CancellationToken cancellationToken)
    {
        var translation = await _translationRepository.GetByIdAsync(request.TranslationId, cancellationToken);

        if (translation == null)
        {
            return Result<TranslationDto>.NotFound("Translation not found");
        }

        var dto = new TranslationDto(
            translation.TranslationId,
            translation.AppId,
            translation.ModuleId,
            translation.LanguageId,
            translation.Key,
            translation.Text,
            translation.Context);

        return Result<TranslationDto>.Success(dto);
    }
}
