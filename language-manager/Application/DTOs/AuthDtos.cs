namespace language_manager.Application.DTOs;

public record LoginRequest(string Email, string Password);

public record RegisterRequest(string Username, string Email, string Password);

public record AuthResponse(string AccessToken, string RefreshToken, UserDto User);

public record RefreshTokenRequest(string RefreshToken);
