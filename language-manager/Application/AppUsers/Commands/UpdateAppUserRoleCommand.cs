using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.AppUsers.Commands;

public record UpdateAppUserRoleCommand(string UserId, string AppId, string Role) : IRequest<Result<AppUserDto>>;

public class UpdateAppUserRoleCommandHandler : IRequestHandler<UpdateAppUserRoleCommand, Result<AppUserDto>>
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly IUserRepository _userRepository;

    public UpdateAppUserRoleCommandHandler(
        IAppUserRepository appUserRepository,
        IUserRepository userRepository)
    {
        _appUserRepository = appUserRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<AppUserDto>> Handle(UpdateAppUserRoleCommand request, CancellationToken cancellationToken)
    {
        var appUser = await _appUserRepository.GetByCompositeKeyAsync(
            request.UserId, request.AppId, cancellationToken);

        if (appUser == null)
        {
            return Result<AppUserDto>.NotFound("User is not assigned to this app");
        }

        appUser.Role = request.Role;

        // For MongoDB with composite key, we need to use the special update method
        var compositeKey = $"{request.UserId}:{request.AppId}";
        await _appUserRepository.UpdateAsync(compositeKey, appUser, cancellationToken);

        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        UserDto? userDto = null;
        if (user != null)
        {
            userDto = new UserDto(user.UserId, user.Email);
        }

        var dto = new AppUserDto(appUser.UserId, appUser.AppId, appUser.Role, userDto, null);
        return Result<AppUserDto>.Success(dto);
    }
}
