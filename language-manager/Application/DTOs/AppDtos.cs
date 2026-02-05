namespace language_manager.Application.DTOs;

public record AppDto(
    string AppId,
    string Name,
    string Domain,
    string Environment,
    string DefaultLanguageId,
    LanguageDto? DefaultLanguage
);

public record CreateAppRequest(
    string Name,
    string Domain,
    string Environment,
    string DefaultLanguageId
);

public record UpdateAppRequest(
    string? Name,
    string? Domain,
    string? Environment,
    string? DefaultLanguageId
);
