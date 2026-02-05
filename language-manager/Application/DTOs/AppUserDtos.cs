namespace language_manager.Application.DTOs;

public record AppUserDto(
    string UserId,
    string AppId,
    string Role,
    UserDto? User,
    AppDto? App
);

public record AddUserToAppRequest(
    string UserId,
    string AppId,
    string Role
);

public record UpdateAppUserRoleRequest(
    string Role
);
