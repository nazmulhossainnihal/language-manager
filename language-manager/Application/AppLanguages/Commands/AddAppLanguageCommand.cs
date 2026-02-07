using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Model.Domain;
using MediatR;

namespace language_manager.Application.AppLanguages.Commands;

public record AddAppLanguageCommand(string AppId, string LanguageId)
    : IRequest<Result<AppLanguageDto>>;

public class AddAppLanguageCommandHandler : IRequestHandler<AddAppLanguageCommand, Result<AppLanguageDto>>
{
    private readonly IAppLanguageRepository _appLanguageRepository;
    private readonly IAppRepository _appRepository;
    private readonly ILanguageRepository _languageRepository;

    public AddAppLanguageCommandHandler(
        IAppLanguageRepository appLanguageRepository,
        IAppRepository appRepository,
        ILanguageRepository languageRepository)
    {
        _appLanguageRepository = appLanguageRepository;
        _appRepository = appRepository;
        _languageRepository = languageRepository;
    }

    public async Task<Result<AppLanguageDto>> Handle(AddAppLanguageCommand request, CancellationToken cancellationToken)
    {
        var app = await _appRepository.GetByIdAsync(request.AppId, cancellationToken);
        if (app == null)
        {
            return Result<AppLanguageDto>.NotFound("App not found");
        }

        var language = await _languageRepository.GetByIdAsync(request.LanguageId, cancellationToken);
        if (language == null)
        {
            return Result<AppLanguageDto>.NotFound("Language not found");
        }

        var existing = await _appLanguageRepository.GetByCompositeKeyAsync(request.AppId, request.LanguageId, cancellationToken);
        if (existing != null)
        {
            return Result<AppLanguageDto>.Conflict("This language is already added to the app");
        }

        var appLanguage = new AppLanguage
        {
            AppId = request.AppId,
            LanguageId = request.LanguageId
        };

        await _appLanguageRepository.AddAsync(appLanguage, cancellationToken);

        var languageDto = new LanguageDto(language.LanguageId, language.LanguageKey, language.Name);
        var dto = new AppLanguageDto(appLanguage.AppId, appLanguage.LanguageId, languageDto);

        return Result<AppLanguageDto>.Success(dto, 201);
    }
}
