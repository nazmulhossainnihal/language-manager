using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Apps.Queries;

public record GetAppQuery(string AppId) : IRequest<Result<AppDto>>;

public class GetAppQueryHandler : IRequestHandler<GetAppQuery, Result<AppDto>>
{
    private readonly IAppRepository _appRepository;
    private readonly ILanguageRepository _languageRepository;

    public GetAppQueryHandler(IAppRepository appRepository, ILanguageRepository languageRepository)
    {
        _appRepository = appRepository;
        _languageRepository = languageRepository;
    }

    public async Task<Result<AppDto>> Handle(GetAppQuery request, CancellationToken cancellationToken)
    {
        var app = await _appRepository.GetByIdAsync(request.AppId, cancellationToken);

        if (app == null)
        {
            return Result<AppDto>.NotFound("App not found");
        }

        LanguageDto? languageDto = null;
        if (!string.IsNullOrEmpty(app.DefaultLanguageId))
        {
            var language = await _languageRepository.GetByIdAsync(app.DefaultLanguageId, cancellationToken);
            if (language != null)
            {
                languageDto = new LanguageDto(language.LanguageId, language.LanguageKey, language.Name);
            }
        }

        var dto = new AppDto(app.AppId, app.Name, app.Domain, app.Environment, app.DefaultLanguageId, languageDto);
        return Result<AppDto>.Success(dto);
    }
}
