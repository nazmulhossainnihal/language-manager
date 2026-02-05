using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Apps.Commands;

public record UpdateAppCommand(string AppId, string? Name, string? Domain, string? Environment, string? DefaultLanguageId)
    : IRequest<Result<AppDto>>;

public class UpdateAppCommandHandler : IRequestHandler<UpdateAppCommand, Result<AppDto>>
{
    private readonly IAppRepository _appRepository;
    private readonly ILanguageRepository _languageRepository;

    public UpdateAppCommandHandler(IAppRepository appRepository, ILanguageRepository languageRepository)
    {
        _appRepository = appRepository;
        _languageRepository = languageRepository;
    }

    public async Task<Result<AppDto>> Handle(UpdateAppCommand request, CancellationToken cancellationToken)
    {
        var app = await _appRepository.GetByIdAsync(request.AppId, cancellationToken);

        if (app == null)
        {
            return Result<AppDto>.NotFound("App not found");
        }

        if (!string.IsNullOrEmpty(request.Domain) && request.Domain != app.Domain)
        {
            var existingApp = await _appRepository.GetByDomainAsync(request.Domain, cancellationToken);
            if (existingApp != null)
            {
                return Result<AppDto>.Conflict("An app with this domain already exists");
            }
            app.Domain = request.Domain;
        }

        if (!string.IsNullOrEmpty(request.DefaultLanguageId) && request.DefaultLanguageId != app.DefaultLanguageId)
        {
            var language = await _languageRepository.GetByIdAsync(request.DefaultLanguageId, cancellationToken);
            if (language == null)
            {
                return Result<AppDto>.Failure("Default language not found", 400);
            }
            app.DefaultLanguageId = request.DefaultLanguageId;
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            app.Name = request.Name;
        }

        if (!string.IsNullOrEmpty(request.Environment))
        {
            app.Environment = request.Environment;
        }

        await _appRepository.UpdateAsync(app.AppId, app, cancellationToken);

        LanguageDto? languageDto = null;
        var lang = await _languageRepository.GetByIdAsync(app.DefaultLanguageId, cancellationToken);
        if (lang != null)
        {
            languageDto = new LanguageDto(lang.LanguageId, lang.LanguageKey, lang.Name);
        }

        var dto = new AppDto(app.AppId, app.Name, app.Domain, app.Environment, app.DefaultLanguageId, languageDto);
        return Result<AppDto>.Success(dto);
    }
}
