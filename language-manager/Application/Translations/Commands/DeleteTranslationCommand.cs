using language_manager.Application.Common;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Translations.Commands;

public record DeleteTranslationCommand(string TranslationId) : IRequest<Result>;

public class DeleteTranslationCommandHandler : IRequestHandler<DeleteTranslationCommand, Result>
{
    private readonly ITranslationRepository _translationRepository;

    public DeleteTranslationCommandHandler(ITranslationRepository translationRepository)
    {
        _translationRepository = translationRepository;
    }

    public async Task<Result> Handle(DeleteTranslationCommand request, CancellationToken cancellationToken)
    {
        var translation = await _translationRepository.GetByIdAsync(request.TranslationId, cancellationToken);

        if (translation == null)
        {
            return Result.NotFound("Translation not found");
        }

        await _translationRepository.DeleteAsync(request.TranslationId, cancellationToken);

        return Result.Success(204);
    }
}
