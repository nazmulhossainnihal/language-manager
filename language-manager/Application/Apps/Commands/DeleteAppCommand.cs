using language_manager.Application.Common;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Apps.Commands;

public record DeleteAppCommand(string AppId) : IRequest<Result>;

public class DeleteAppCommandHandler : IRequestHandler<DeleteAppCommand, Result>
{
    private readonly IAppRepository _appRepository;
    private readonly IModuleRepository _moduleRepository;
    private readonly ITranslationRepository _translationRepository;
    private readonly IAppUserRepository _appUserRepository;

    public DeleteAppCommandHandler(
        IAppRepository appRepository,
        IModuleRepository moduleRepository,
        ITranslationRepository translationRepository,
        IAppUserRepository appUserRepository)
    {
        _appRepository = appRepository;
        _moduleRepository = moduleRepository;
        _translationRepository = translationRepository;
        _appUserRepository = appUserRepository;
    }

    public async Task<Result> Handle(DeleteAppCommand request, CancellationToken cancellationToken)
    {
        var app = await _appRepository.GetByIdAsync(request.AppId, cancellationToken);

        if (app == null)
        {
            return Result.NotFound("App not found");
        }

        // Delete all translations for this app
        await _translationRepository.DeleteManyAsync(t => t.AppId == request.AppId, cancellationToken);

        // Delete all modules for this app
        await _moduleRepository.DeleteManyAsync(m => m.AppId == request.AppId, cancellationToken);

        // Delete all app-user relationships
        await _appUserRepository.DeleteManyAsync(au => au.AppId == request.AppId, cancellationToken);

        // Delete the app
        await _appRepository.DeleteAsync(request.AppId, cancellationToken);

        return Result.Success(204);
    }
}
