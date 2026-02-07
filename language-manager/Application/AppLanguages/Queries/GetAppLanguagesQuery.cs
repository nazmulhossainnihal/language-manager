using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.AppLanguages.Queries;

public record GetAppLanguagesQuery(string AppId) : IRequest<Result<IEnumerable<AppLanguageDto>>>;

public class GetAppLanguagesQueryHandler : IRequestHandler<GetAppLanguagesQuery, Result<IEnumerable<AppLanguageDto>>>
{
    private readonly IAppLanguageRepository _appLanguageRepository;
    private readonly IAppRepository _appRepository;
    private readonly ILanguageRepository _languageRepository;

    public GetAppLanguagesQueryHandler(
        IAppLanguageRepository appLanguageRepository,
        IAppRepository appRepository,
        ILanguageRepository languageRepository)
    {
        _appLanguageRepository = appLanguageRepository;
        _appRepository = appRepository;
        _languageRepository = languageRepository;
    }

    public async Task<Result<IEnumerable<AppLanguageDto>>> Handle(GetAppLanguagesQuery request, CancellationToken cancellationToken)
    {
        var app = await _appRepository.GetByIdAsync(request.AppId, cancellationToken);
        if (app == null)
        {
            return Result<IEnumerable<AppLanguageDto>>.NotFound("App not found");
        }

        var appLanguages = await _appLanguageRepository.GetByAppIdAsync(request.AppId, cancellationToken);
        var languageIds = appLanguages.Select(al => al.LanguageId).ToList();

        var languages = await _languageRepository.GetAllAsync(cancellationToken);
        var languageDict = languages.ToDictionary(l => l.LanguageId);

        var dtos = appLanguages.Select(al =>
        {
            LanguageDto? languageDto = null;
            if (languageDict.TryGetValue(al.LanguageId, out var lang))
            {
                languageDto = new LanguageDto(lang.LanguageId, lang.LanguageKey, lang.Name);
            }
            return new AppLanguageDto(al.AppId, al.LanguageId, languageDto);
        });

        return Result<IEnumerable<AppLanguageDto>>.Success(dtos);
    }
}
