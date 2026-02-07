using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Model.Domain;
using language_manager.Services.Interfaces;
using MediatR;

namespace language_manager.Application.Auth.Commands;

public record RegisterCommand(string Email, string Password) : IRequest<Result<AuthResponse>>;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IJwtService jwtService,
        IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _passwordService = passwordService;
    }

    public async Task<Result<AuthResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUserByEmail = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingUserByEmail != null)
        {
            return Result<AuthResponse>.Conflict("A user with this email already exists");
        }

        var hashedPassword = _passwordService.HashPassword(request.Password);

        var user = new User
        {
            UserId = Guid.NewGuid().ToString(),
            Email = request.Email,
            Password = hashedPassword
        };

        await _userRepository.AddAsync(user, cancellationToken);

        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        var userDto = new UserDto(user.UserId, user.Email);
        var response = new AuthResponse(accessToken, refreshToken, userDto);

        return Result<AuthResponse>.Success(response, 201);
    }
}
