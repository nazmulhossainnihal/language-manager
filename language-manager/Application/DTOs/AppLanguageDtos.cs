namespace language_manager.Application.DTOs;

public record AppLanguageDto(
    string AppId,
    string LanguageId,
    LanguageDto? Language
);

public record AddAppLanguageRequest(
    string LanguageId
);
