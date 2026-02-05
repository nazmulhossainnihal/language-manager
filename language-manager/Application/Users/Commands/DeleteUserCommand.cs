using language_manager.Application.Common;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Users.Commands;

public record DeleteUserCommand(string UserId) : IRequest<Result>;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IAppUserRepository _appUserRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository, IAppUserRepository appUserRepository)
    {
        _userRepository = userRepository;
        _appUserRepository = appUserRepository;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            return Result.NotFound("User not found");
        }

        // Delete all app-user relationships
        await _appUserRepository.DeleteManyAsync(au => au.UserId == request.UserId, cancellationToken);

        // Delete the user
        await _userRepository.DeleteAsync(request.UserId, cancellationToken);

        return Result.Success(204);
    }
}
