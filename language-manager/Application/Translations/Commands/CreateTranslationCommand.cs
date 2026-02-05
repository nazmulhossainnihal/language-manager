using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Model.Domain;
using MediatR;

namespace language_manager.Application.Translations.Commands;

public record CreateTranslationCommand(
    string AppId,
    string ModuleId,
    string LanguageId,
    string Key,
    string Text,
    string? Context) : IRequest<Result<TranslationDto>>;

public class CreateTranslationCommandHandler : IRequestHandler<CreateTranslationCommand, Result<TranslationDto>>
{
    private readonly ITranslationRepository _translationRepository;
    private readonly IAppRepository _appRepository;
    private readonly IModuleRepository _moduleRepository;
    private readonly ILanguageRepository _languageRepository;

    public CreateTranslationCommandHandler(
        ITranslationRepository translationRepository,
        IAppRepository appRepository,
        IModuleRepository moduleRepository,
        ILanguageRepository languageRepository)
    {
        _translationRepository = translationRepository;
        _appRepository = appRepository;
        _moduleRepository = moduleRepository;
        _languageRepository = languageRepository;
    }

    public async Task<Result<TranslationDto>> Handle(CreateTranslationCommand request, CancellationToken cancellationToken)
    {
        var app = await _appRepository.GetByIdAsync(request.AppId, cancellationToken);
        if (app == null)
        {
            return Result<TranslationDto>.NotFound("App not found");
        }

        var module = await _moduleRepository.GetByIdAsync(request.ModuleId, cancellationToken);
        if (module == null || module.AppId != request.AppId)
        {
            return Result<TranslationDto>.NotFound("Module not found or does not belong to this app");
        }

        var language = await _languageRepository.GetByIdAsync(request.LanguageId, cancellationToken);
        if (language == null)
        {
            return Result<TranslationDto>.NotFound("Language not found");
        }

        var existingTranslation = await _translationRepository.GetByKeyAsync(
            request.AppId, request.ModuleId, request.LanguageId, request.Key, cancellationToken);

        if (existingTranslation != null)
        {
            return Result<TranslationDto>.Conflict("A translation with this key already exists for this module and language");
        }

        var translation = new Translation
        {
            TranslationId = Guid.NewGuid().ToString(),
            AppId = request.AppId,
            ModuleId = request.ModuleId,
            LanguageId = request.LanguageId,
            Key = request.Key,
            Text = request.Text,
            Context = request.Context
        };

        await _translationRepository.AddAsync(translation, cancellationToken);

        var dto = new TranslationDto(
            translation.TranslationId,
            translation.AppId,
            translation.ModuleId,
            translation.LanguageId,
            translation.Key,
            translation.Text,
            translation.Context);

        return Result<TranslationDto>.Success(dto, 201);
    }
}
