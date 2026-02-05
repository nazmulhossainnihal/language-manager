namespace language_manager.Application.DTOs;

public record LanguageDto(
    string LanguageId,
    string LanguageKey,
    string Name
);

public record CreateLanguageRequest(
    string LanguageKey,
    string Name
);

public record UpdateLanguageRequest(
    string? LanguageKey,
    string? Name
);
