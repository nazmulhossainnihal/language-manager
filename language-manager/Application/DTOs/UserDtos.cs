namespace language_manager.Application.DTOs;

public record UserDto(
    string UserId,
    string Username,
    string Email
);

public record CreateUserRequest(
    string Username,
    string Email,
    string Password
);

public record UpdateUserRequest(
    string? Username,
    string? Email,
    string? Password
);
