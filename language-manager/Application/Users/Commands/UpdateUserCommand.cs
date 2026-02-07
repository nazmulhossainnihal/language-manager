using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Services.Interfaces;
using MediatR;

namespace language_manager.Application.Users.Commands;

public record UpdateUserCommand(string UserId, string? Email, string? Password)
    : IRequest<Result<UserDto>>;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public UpdateUserCommandHandler(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            return Result<UserDto>.NotFound("User not found");
        }

        if (!string.IsNullOrEmpty(request.Email) && request.Email != user.Email)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingUser != null)
            {
                return Result<UserDto>.Conflict("A user with this email already exists");
            }
            user.Email = request.Email;
        }

        if (!string.IsNullOrEmpty(request.Password))
        {
            user.Password = _passwordService.HashPassword(request.Password);
        }

        await _userRepository.UpdateAsync(user.UserId, user, cancellationToken);

        var userDto = new UserDto(user.UserId, user.Email);
        return Result<UserDto>.Success(userDto);
    }
}
