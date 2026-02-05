using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Apps.Queries;

public record GetAllAppsQuery : IRequest<Result<IEnumerable<AppDto>>>;

public class GetAllAppsQueryHandler : IRequestHandler<GetAllAppsQuery, Result<IEnumerable<AppDto>>>
{
    private readonly IAppRepository _appRepository;
    private readonly ILanguageRepository _languageRepository;

    public GetAllAppsQueryHandler(IAppRepository appRepository, ILanguageRepository languageRepository)
    {
        _appRepository = appRepository;
        _languageRepository = languageRepository;
    }

    public async Task<Result<IEnumerable<AppDto>>> Handle(GetAllAppsQuery request, CancellationToken cancellationToken)
    {
        var apps = await _appRepository.GetAllAsync(cancellationToken);
        var languages = await _languageRepository.GetAllAsync(cancellationToken);
        var languageDict = languages.ToDictionary(l => l.LanguageId);

        var dtos = apps.Select(app =>
        {
            LanguageDto? languageDto = null;
            if (!string.IsNullOrEmpty(app.DefaultLanguageId) && languageDict.TryGetValue(app.DefaultLanguageId, out var lang))
            {
                languageDto = new LanguageDto(lang.LanguageId, lang.LanguageKey, lang.Name);
            }
            return new AppDto(app.AppId, app.Name, app.Domain, app.Environment, app.DefaultLanguageId, languageDto);
        });

        return Result<IEnumerable<AppDto>>.Success(dtos);
    }
}
