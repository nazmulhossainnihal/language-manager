using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Model.Domain;
using MediatR;

namespace language_manager.Application.Translations.Commands;

public record BulkCreateTranslationsCommand(string AppId, IEnumerable<BulkTranslationItem> Translations)
    : IRequest<Result<BulkTranslationResult>>;

public class BulkCreateTranslationsCommandHandler
    : IRequestHandler<BulkCreateTranslationsCommand, Result<BulkTranslationResult>>
{
    private readonly ITranslationRepository _translationRepository;
    private readonly IAppRepository _appRepository;
    private readonly IModuleRepository _moduleRepository;
    private readonly ILanguageRepository _languageRepository;

    public BulkCreateTranslationsCommandHandler(
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

    public async Task<Result<BulkTranslationResult>> Handle(
        BulkCreateTranslationsCommand request,
        CancellationToken cancellationToken)
    {
        var app = await _appRepository.GetByIdAsync(request.AppId, cancellationToken);
        if (app == null)
        {
            return Result<BulkTranslationResult>.NotFound("App not found");
        }

        var translationsList = request.Translations.ToList();
        var errors = new List<BulkTranslationError>();
        var successfulTranslations = new List<Translation>();

        // Pre-fetch all modules and languages for validation
        var moduleIds = translationsList.Select(t => t.ModuleId).Distinct().ToList();
        var languageIds = translationsList.Select(t => t.LanguageId).Distinct().ToList();

        var modules = await _moduleRepository.FindAsync(m => moduleIds.Contains(m.ModuleId), cancellationToken);
        var moduleDict = modules.ToDictionary(m => m.ModuleId);

        var languages = await _languageRepository.FindAsync(l => languageIds.Contains(l.LanguageId), cancellationToken);
        var languageDict = languages.ToDictionary(l => l.LanguageId);

        // Pre-fetch existing translations to check for duplicates
        var existingTranslations = await _translationRepository.GetByAppIdAsync(request.AppId, cancellationToken);
        var existingKeys = existingTranslations
            .Select(t => $"{t.ModuleId}|{t.LanguageId}|{t.Key}")
            .ToHashSet();

        foreach (var item in translationsList)
        {
            // Validate module
            if (!moduleDict.TryGetValue(item.ModuleId, out var module) || module.AppId != request.AppId)
            {
                errors.Add(new BulkTranslationError(
                    item.Key,
                    item.ModuleId,
                    item.LanguageId,
                    "Module not found or does not belong to this app"));
                continue;
            }

            // Validate language
            if (!languageDict.ContainsKey(item.LanguageId))
            {
                errors.Add(new BulkTranslationError(
                    item.Key,
                    item.ModuleId,
                    item.LanguageId,
                    "Language not found"));
                continue;
            }

            // Check for duplicate key
            var compositeKey = $"{item.ModuleId}|{item.LanguageId}|{item.Key}";
            if (existingKeys.Contains(compositeKey))
            {
                errors.Add(new BulkTranslationError(
                    item.Key,
                    item.ModuleId,
                    item.LanguageId,
                    "Translation with this key already exists"));
                continue;
            }

            // Check for duplicates within the batch
            if (successfulTranslations.Any(t =>
                t.ModuleId == item.ModuleId &&
                t.LanguageId == item.LanguageId &&
                t.Key == item.Key))
            {
                errors.Add(new BulkTranslationError(
                    item.Key,
                    item.ModuleId,
                    item.LanguageId,
                    "Duplicate entry in batch"));
                continue;
            }

            var translation = new Translation
            {
                TranslationId = Guid.NewGuid().ToString(),
                AppId = request.AppId,
                ModuleId = item.ModuleId,
                LanguageId = item.LanguageId,
                Key = item.Key,
                Text = item.Text,
                Context = item.Context
            };

            successfulTranslations.Add(translation);
        }

        // Bulk insert successful translations
        if (successfulTranslations.Count > 0)
        {
            await _translationRepository.AddRangeAsync(successfulTranslations, cancellationToken);
        }

        var result = new BulkTranslationResult(
            translationsList.Count,
            successfulTranslations.Count,
            errors.Count,
            errors);

        return Result<BulkTranslationResult>.Success(result, successfulTranslations.Count > 0 ? 201 : 200);
    }
}
