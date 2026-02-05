using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.AppUsers.Queries;

public record GetUserAppsQuery(string UserId) : IRequest<Result<IEnumerable<AppUserDto>>>;

public class GetUserAppsQueryHandler : IRequestHandler<GetUserAppsQuery, Result<IEnumerable<AppUserDto>>>
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAppRepository _appRepository;
    private readonly ILanguageRepository _languageRepository;

    public GetUserAppsQueryHandler(
        IAppUserRepository appUserRepository,
        IUserRepository userRepository,
        IAppRepository appRepository,
        ILanguageRepository languageRepository)
    {
        _appUserRepository = appUserRepository;
        _userRepository = userRepository;
        _appRepository = appRepository;
        _languageRepository = languageRepository;
    }

    public async Task<Result<IEnumerable<AppUserDto>>> Handle(GetUserAppsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            return Result<IEnumerable<AppUserDto>>.NotFound("User not found");
        }

        var appUsers = await _appUserRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        var appIds = appUsers.Select(au => au.AppId).Distinct().ToList();

        var apps = await _appRepository.FindAsync(a => appIds.Contains(a.AppId), cancellationToken);
        var appDict = apps.ToDictionary(a => a.AppId);

        var languageIds = apps.Select(a => a.DefaultLanguageId).Distinct().ToList();
        var languages = await _languageRepository.FindAsync(l => languageIds.Contains(l.LanguageId), cancellationToken);
        var languageDict = languages.ToDictionary(l => l.LanguageId);

        var dtos = appUsers.Select(au =>
        {
            AppDto? appDto = null;
            if (appDict.TryGetValue(au.AppId, out var app))
            {
                LanguageDto? langDto = null;
                if (languageDict.TryGetValue(app.DefaultLanguageId, out var lang))
                {
                    langDto = new LanguageDto(lang.LanguageId, lang.LanguageKey, lang.Name);
                }
                appDto = new AppDto(app.AppId, app.Name, app.Domain, app.Environment, app.DefaultLanguageId, langDto);
            }
            return new AppUserDto(au.UserId, au.AppId, au.Role, null, appDto);
        });

        return Result<IEnumerable<AppUserDto>>.Success(dtos);
    }
}
