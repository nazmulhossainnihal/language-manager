using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Translations.Queries;

public record ModuleTranslationsDto(
    string AppDomain,
    string AppName,
    string ModuleKey,
    string ModuleName,
    string LanguageKey,
    string LanguageName,
    IEnumerable<TranslationItemDto> Translations
);

public record TranslationItemDto(
    string TranslationId,
    string Key,
    string Text,
    string? Context
);

public record GetTranslationsByModuleAndLanguageKeyQuery(string AppDomain, string ModuleKey, string LanguageKey)
    : IRequest<Result<ModuleTranslationsDto>>;

public class GetTranslationsByModuleAndLanguageKeyQueryHandler
    : IRequestHandler<GetTranslationsByModuleAndLanguageKeyQuery, Result<ModuleTranslationsDto>>
{
    private readonly ITranslationRepository _translationRepository;
    private readonly IModuleRepository _moduleRepository;
    private readonly ILanguageRepository _languageRepository;
    private readonly IAppRepository _appRepository;

    public GetTranslationsByModuleAndLanguageKeyQueryHandler(
        ITranslationRepository translationRepository,
        IModuleRepository moduleRepository,
        ILanguageRepository languageRepository,
        IAppRepository appRepository)
    {
        _translationRepository = translationRepository;
        _moduleRepository = moduleRepository;
        _languageRepository = languageRepository;
        _appRepository = appRepository;
    }

    public async Task<Result<ModuleTranslationsDto>> Handle(
        GetTranslationsByModuleAndLanguageKeyQuery request,
        CancellationToken cancellationToken)
    {
        var app = await _appRepository.GetByDomainAsync(request.AppDomain, cancellationToken);
        if (app == null)
        {
            return Result<ModuleTranslationsDto>.NotFound($"App with domain '{request.AppDomain}' not found");
        }

        var module = await _moduleRepository.GetByModuleKeyAsync(app.AppId, request.ModuleKey, cancellationToken);
        if (module == null)
        {
            return Result<ModuleTranslationsDto>.NotFound($"Module with key '{request.ModuleKey}' not found in app '{request.AppDomain}'");
        }

        var language = await _languageRepository.GetByLanguageKeyAsync(request.LanguageKey, cancellationToken);
        if (language == null)
        {
            return Result<ModuleTranslationsDto>.NotFound($"Language with key '{request.LanguageKey}' not found");
        }

        var translations = await _translationRepository.GetByModuleAndLanguageAsync(
            module.ModuleId, language.LanguageId, cancellationToken);

        var translationItems = translations.Select(t => new TranslationItemDto(
            t.TranslationId,
            t.Key,
            t.Text,
            t.Context));

        var result = new ModuleTranslationsDto(
            app.Domain,
            app.Name,
            module.ModuleKey,
            module.Name,
            language.LanguageKey,
            language.Name,
            translationItems);

        return Result<ModuleTranslationsDto>.Success(result);
    }
}
