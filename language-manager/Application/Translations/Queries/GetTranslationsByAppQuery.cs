using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Translations.Queries;

public record GetTranslationsByAppQuery(string AppId, string? LanguageId = null, string? ModuleId = null)
    : IRequest<Result<IEnumerable<TranslationDto>>>;

public class GetTranslationsByAppQueryHandler
    : IRequestHandler<GetTranslationsByAppQuery, Result<IEnumerable<TranslationDto>>>
{
    private readonly ITranslationRepository _translationRepository;
    private readonly IAppRepository _appRepository;

    public GetTranslationsByAppQueryHandler(
        ITranslationRepository translationRepository,
        IAppRepository appRepository)
    {
        _translationRepository = translationRepository;
        _appRepository = appRepository;
    }

    public async Task<Result<IEnumerable<TranslationDto>>> Handle(
        GetTranslationsByAppQuery request,
        CancellationToken cancellationToken)
    {
        var app = await _appRepository.GetByIdAsync(request.AppId, cancellationToken);
        if (app == null)
        {
            return Result<IEnumerable<TranslationDto>>.NotFound("App not found");
        }

        IEnumerable<Model.Domain.Translation> translations;

        if (!string.IsNullOrEmpty(request.ModuleId) && !string.IsNullOrEmpty(request.LanguageId))
        {
            translations = await _translationRepository.GetByModuleAndLanguageAsync(
                request.ModuleId, request.LanguageId, cancellationToken);
        }
        else if (!string.IsNullOrEmpty(request.LanguageId))
        {
            translations = await _translationRepository.GetByAppAndLanguageAsync(
                request.AppId, request.LanguageId, cancellationToken);
        }
        else if (!string.IsNullOrEmpty(request.ModuleId))
        {
            translations = await _translationRepository.GetByModuleIdAsync(request.ModuleId, cancellationToken);
        }
        else
        {
            translations = await _translationRepository.GetByAppIdAsync(request.AppId, cancellationToken);
        }

        var dtos = translations.Select(t => new TranslationDto(
            t.TranslationId,
            t.AppId,
            t.ModuleId,
            t.LanguageId,
            t.Key,
            t.Text,
            t.Context));

        return Result<IEnumerable<TranslationDto>>.Success(dtos);
    }
}
