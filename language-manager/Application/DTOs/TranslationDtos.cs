namespace language_manager.Application.DTOs;

public record TranslationDto(
    string TranslationId,
    string AppId,
    string ModuleId,
    string LanguageId,
    string Key,
    string Text,
    string? Context
);

public record CreateTranslationRequest(
    string AppId,
    string ModuleId,
    string LanguageId,
    string Key,
    string Text,
    string? Context
);

public record UpdateTranslationRequest(
    string? Key,
    string? Text,
    string? Context
);

public record BulkTranslationItem(
    string ModuleId,
    string LanguageId,
    string Key,
    string Text,
    string? Context
);

public record BulkCreateTranslationsRequest(
    string AppId,
    IEnumerable<BulkTranslationItem> Translations
);

public record BulkTranslationResult(
    int TotalCount,
    int SuccessCount,
    int FailedCount,
    IEnumerable<BulkTranslationError> Errors
);

public record BulkTranslationError(
    string Key,
    string ModuleId,
    string LanguageId,
    string Error
);
