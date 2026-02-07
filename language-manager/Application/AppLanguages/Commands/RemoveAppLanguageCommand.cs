using language_manager.Application.Common;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.AppLanguages.Commands;

public record RemoveAppLanguageCommand(string AppId, string LanguageId) : IRequest<Result>;

public class RemoveAppLanguageCommandHandler : IRequestHandler<RemoveAppLanguageCommand, Result>
{
    private readonly IAppLanguageRepository _appLanguageRepository;

    public RemoveAppLanguageCommandHandler(IAppLanguageRepository appLanguageRepository)
    {
        _appLanguageRepository = appLanguageRepository;
    }

    public async Task<Result> Handle(RemoveAppLanguageCommand request, CancellationToken cancellationToken)
    {
        var existing = await _appLanguageRepository.GetByCompositeKeyAsync(request.AppId, request.LanguageId, cancellationToken);
        if (existing == null)
        {
            return Result.NotFound("App language not found");
        }

        await _appLanguageRepository.DeleteByCompositeKeyAsync(request.AppId, request.LanguageId, cancellationToken);

        return Result.Success(204);
    }
}
