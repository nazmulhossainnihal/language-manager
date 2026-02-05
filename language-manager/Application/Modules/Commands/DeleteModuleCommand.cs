using language_manager.Application.Common;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Modules.Commands;

public record DeleteModuleCommand(string ModuleId) : IRequest<Result>;

public class DeleteModuleCommandHandler : IRequestHandler<DeleteModuleCommand, Result>
{
    private readonly IModuleRepository _moduleRepository;
    private readonly ITranslationRepository _translationRepository;

    public DeleteModuleCommandHandler(
        IModuleRepository moduleRepository,
        ITranslationRepository translationRepository)
    {
        _moduleRepository = moduleRepository;
        _translationRepository = translationRepository;
    }

    public async Task<Result> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
    {
        var module = await _moduleRepository.GetByIdAsync(request.ModuleId, cancellationToken);

        if (module == null)
        {
            return Result.NotFound("Module not found");
        }

        // Delete all translations in this module
        await _translationRepository.DeleteManyAsync(t => t.ModuleId == request.ModuleId, cancellationToken);

        // Delete the module
        await _moduleRepository.DeleteAsync(request.ModuleId, cancellationToken);

        return Result.Success(204);
    }
}
