using language_manager.Application.Common;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.AppUsers.Commands;

public record RemoveUserFromAppCommand(string UserId, string AppId) : IRequest<Result>;

public class RemoveUserFromAppCommandHandler : IRequestHandler<RemoveUserFromAppCommand, Result>
{
    private readonly IAppUserRepository _appUserRepository;

    public RemoveUserFromAppCommandHandler(IAppUserRepository appUserRepository)
    {
        _appUserRepository = appUserRepository;
    }

    public async Task<Result> Handle(RemoveUserFromAppCommand request, CancellationToken cancellationToken)
    {
        var appUser = await _appUserRepository.GetByCompositeKeyAsync(
            request.UserId, request.AppId, cancellationToken);

        if (appUser == null)
        {
            return Result.NotFound("User is not assigned to this app");
        }

        await _appUserRepository.DeleteByCompositeKeyAsync(request.UserId, request.AppId, cancellationToken);

        return Result.Success(204);
    }
}
