using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Services.Interfaces;
using MediatR;

namespace language_manager.Application.Auth.Commands;

public record LoginCommand(string Email, string Password) : IRequest<Result<AuthResponse>>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IJwtService jwtService,
        IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _passwordService = passwordService;
    }

    public async Task<Result<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user == null)
        {
            return Result<AuthResponse>.Unauthorized("Invalid email or password");
        }

        if (!_passwordService.VerifyPassword(request.Password, user.Password))
        {
            return Result<AuthResponse>.Unauthorized("Invalid email or password");
        }

        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        var userDto = new UserDto(user.UserId, user.Email);
        var response = new AuthResponse(accessToken, refreshToken, userDto);

        return Result<AuthResponse>.Success(response);
    }
}
