using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Languages.Commands;

public record UpdateLanguageCommand(string LanguageId, string? LanguageKey, string? Name)
    : IRequest<Result<LanguageDto>>;

public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, Result<LanguageDto>>
{
    private readonly ILanguageRepository _languageRepository;

    public UpdateLanguageCommandHandler(ILanguageRepository languageRepository)
    {
        _languageRepository = languageRepository;
    }

    public async Task<Result<LanguageDto>> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
    {
        var language = await _languageRepository.GetByIdAsync(request.LanguageId, cancellationToken);

        if (language == null)
        {
            return Result<LanguageDto>.NotFound("Language not found");
        }

        if (!string.IsNullOrEmpty(request.LanguageKey) && request.LanguageKey != language.LanguageKey)
        {
            var existingLanguage = await _languageRepository.GetByLanguageKeyAsync(request.LanguageKey, cancellationToken);
            if (existingLanguage != null)
            {
                return Result<LanguageDto>.Conflict("A language with this key already exists");
            }
            language.LanguageKey = request.LanguageKey;
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            language.Name = request.Name;
        }

        await _languageRepository.UpdateAsync(language.LanguageId, language, cancellationToken);

        var dto = new LanguageDto(language.LanguageId, language.LanguageKey, language.Name);
        return Result<LanguageDto>.Success(dto);
    }
}
