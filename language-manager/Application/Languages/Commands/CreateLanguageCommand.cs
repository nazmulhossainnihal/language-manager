using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Model.Domain;
using MediatR;

namespace language_manager.Application.Languages.Commands;

public record CreateLanguageCommand(string LanguageKey, string Name) : IRequest<Result<LanguageDto>>;

public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, Result<LanguageDto>>
{
    private readonly ILanguageRepository _languageRepository;

    public CreateLanguageCommandHandler(ILanguageRepository languageRepository)
    {
        _languageRepository = languageRepository;
    }

    public async Task<Result<LanguageDto>> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
    {
        var existingLanguage = await _languageRepository.GetByLanguageKeyAsync(request.LanguageKey, cancellationToken);
        if (existingLanguage != null)
        {
            return Result<LanguageDto>.Conflict("A language with this key already exists");
        }

        var language = new Language
        {
            LanguageId = Guid.NewGuid().ToString(),
            LanguageKey = request.LanguageKey,
            Name = request.Name
        };

        await _languageRepository.AddAsync(language, cancellationToken);

        var dto = new LanguageDto(language.LanguageId, language.LanguageKey, language.Name);
        return Result<LanguageDto>.Success(dto, 201);
    }
}
