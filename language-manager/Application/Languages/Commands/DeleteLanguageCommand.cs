using language_manager.Application.Common;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Languages.Commands;

public record DeleteLanguageCommand(string LanguageId) : IRequest<Result>;

public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand, Result>
{
    private readonly ILanguageRepository _languageRepository;
    private readonly IAppRepository _appRepository;
    private readonly ITranslationRepository _translationRepository;

    public DeleteLanguageCommandHandler(
        ILanguageRepository languageRepository,
        IAppRepository appRepository,
        ITranslationRepository translationRepository)
    {
        _languageRepository = languageRepository;
        _appRepository = appRepository;
        _translationRepository = translationRepository;
    }

    public async Task<Result> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
    {
        var language = await _languageRepository.GetByIdAsync(request.LanguageId, cancellationToken);

        if (language == null)
        {
            return Result.NotFound("Language not found");
        }

        // Check if any app uses this as default language
        var appsUsingLanguage = await _appRepository.ExistsAsync(
            a => a.DefaultLanguageId == request.LanguageId, cancellationToken);

        if (appsUsingLanguage)
        {
            return Result.Conflict("Cannot delete language that is used as default language for an app");
        }

        // Delete all translations in this language
        await _translationRepository.DeleteManyAsync(t => t.LanguageId == request.LanguageId, cancellationToken);

        // Delete the language
        await _languageRepository.DeleteAsync(request.LanguageId, cancellationToken);

        return Result.Success(204);
    }
}
