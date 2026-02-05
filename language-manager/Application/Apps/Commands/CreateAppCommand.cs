using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Model.Domain;
using MediatR;

namespace language_manager.Application.Apps.Commands;

public record CreateAppCommand(string Name, string Domain, string Environment, string DefaultLanguageId)
    : IRequest<Result<AppDto>>;

public class CreateAppCommandHandler : IRequestHandler<CreateAppCommand, Result<AppDto>>
{
    private readonly IAppRepository _appRepository;
    private readonly ILanguageRepository _languageRepository;

    public CreateAppCommandHandler(IAppRepository appRepository, ILanguageRepository languageRepository)
    {
        _appRepository = appRepository;
        _languageRepository = languageRepository;
    }

    public async Task<Result<AppDto>> Handle(CreateAppCommand request, CancellationToken cancellationToken)
    {
        var existingApp = await _appRepository.GetByDomainAsync(request.Domain, cancellationToken);
        if (existingApp != null)
        {
            return Result<AppDto>.Conflict("An app with this domain already exists");
        }

        var language = await _languageRepository.GetByIdAsync(request.DefaultLanguageId, cancellationToken);
        if (language == null)
        {
            return Result<AppDto>.Failure("Default language not found", 400);
        }

        var app = new App
        {
            AppId = Guid.NewGuid().ToString(),
            Name = request.Name,
            Domain = request.Domain,
            Environment = request.Environment,
            DefaultLanguageId = request.DefaultLanguageId
        };

        await _appRepository.AddAsync(app, cancellationToken);

        var languageDto = new LanguageDto(language.LanguageId, language.LanguageKey, language.Name);
        var dto = new AppDto(app.AppId, app.Name, app.Domain, app.Environment, app.DefaultLanguageId, languageDto);

        return Result<AppDto>.Success(dto, 201);
    }
}
