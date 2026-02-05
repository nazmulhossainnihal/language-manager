using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Translations.Commands;

public record UpdateTranslationCommand(string TranslationId, string? Key, string? Text, string? Context)
    : IRequest<Result<TranslationDto>>;

public class UpdateTranslationCommandHandler : IRequestHandler<UpdateTranslationCommand, Result<TranslationDto>>
{
    private readonly ITranslationRepository _translationRepository;

    public UpdateTranslationCommandHandler(ITranslationRepository translationRepository)
    {
        _translationRepository = translationRepository;
    }

    public async Task<Result<TranslationDto>> Handle(UpdateTranslationCommand request, CancellationToken cancellationToken)
    {
        var translation = await _translationRepository.GetByIdAsync(request.TranslationId, cancellationToken);

        if (translation == null)
        {
            return Result<TranslationDto>.NotFound("Translation not found");
        }

        if (!string.IsNullOrEmpty(request.Key) && request.Key != translation.Key)
        {
            var existingTranslation = await _translationRepository.GetByKeyAsync(
                translation.AppId,
                translation.ModuleId,
                translation.LanguageId,
                request.Key,
                cancellationToken);

            if (existingTranslation != null)
            {
                return Result<TranslationDto>.Conflict(
                    "A translation with this key already exists for this module and language");
            }
            translation.Key = request.Key;
        }

        if (!string.IsNullOrEmpty(request.Text))
        {
            translation.Text = request.Text;
        }

        if (request.Context != null)
        {
            translation.Context = request.Context;
        }

        await _translationRepository.UpdateAsync(translation.TranslationId, translation, cancellationToken);

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
