using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Model.Domain;
using MediatR;

namespace language_manager.Application.AppUsers.Commands;

public record AddUserToAppCommand(string UserId, string AppId, string Role) : IRequest<Result<AppUserDto>>;

public class AddUserToAppCommandHandler : IRequestHandler<AddUserToAppCommand, Result<AppUserDto>>
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAppRepository _appRepository;

    public AddUserToAppCommandHandler(
        IAppUserRepository appUserRepository,
        IUserRepository userRepository,
        IAppRepository appRepository)
    {
        _appUserRepository = appUserRepository;
        _userRepository = userRepository;
        _appRepository = appRepository;
    }

    public async Task<Result<AppUserDto>> Handle(AddUserToAppCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            return Result<AppUserDto>.NotFound("User not found");
        }

        var app = await _appRepository.GetByIdAsync(request.AppId, cancellationToken);
        if (app == null)
        {
            return Result<AppUserDto>.NotFound("App not found");
        }

        var existingAppUser = await _appUserRepository.GetByCompositeKeyAsync(
            request.UserId, request.AppId, cancellationToken);

        if (existingAppUser != null)
        {
            return Result<AppUserDto>.Conflict("User is already assigned to this app");
        }

        var appUser = new AppUser
        {
            UserId = request.UserId,
            AppId = request.AppId,
            Role = request.Role
        };

        await _appUserRepository.AddAsync(appUser, cancellationToken);

        var userDto = new UserDto(user.UserId, user.Email);
        var dto = new AppUserDto(appUser.UserId, appUser.AppId, appUser.Role, userDto, null);

        return Result<AppUserDto>.Success(dto, 201);
    }
}
