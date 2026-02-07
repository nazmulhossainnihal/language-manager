namespace language_manager.Application.DTOs;

public record UserDto(
    string UserId,
    string Email
);

public record CreateUserRequest(
    string Email,
    string Password
);

public record UpdateUserRequest(
    string? Email,
    string? Password
);
