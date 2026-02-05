namespace language_manager.Application.DTOs;

public record ModuleDto(
    string ModuleId,
    string AppId,
    string ModuleKey,
    string Name
);

public record CreateModuleRequest(
    string AppId,
    string ModuleKey,
    string Name
);

public record UpdateModuleRequest(
    string? ModuleKey,
    string? Name
);
