using language_manager.Application.DTOs;
using language_manager.Application.Translations.Commands;
using language_manager.Application.Translations.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace language_manager.Controllers;

[Route("api/[controller]")]
[Authorize]
public class TranslationsController : BaseController
{
    private readonly IMediator _mediator;

    public TranslationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetTranslationQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("app/{appId}")]
    public async Task<IActionResult> GetByAppId(
        string appId,
        [FromQuery] string? languageId,
        [FromQuery] string? moduleId,
        CancellationToken cancellationToken)
    {
        var query = new GetTranslationsByAppQuery(appId, languageId, moduleId);
        var result = await _mediator.Send(query, cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("{appDomain}/{moduleKey}/{languageKey}")]
    public async Task<IActionResult> GetByModuleAndLanguageKey(
        string appDomain,
        string moduleKey,
        string languageKey,
        CancellationToken cancellationToken)
    {
        var query = new GetTranslationsByModuleAndLanguageKeyQuery(appDomain, moduleKey, languageKey);
        var result = await _mediator.Send(query, cancellationToken);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTranslationRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateTranslationCommand(
            request.AppId,
            request.ModuleId,
            request.LanguageId,
            request.Key,
            request.Text,
            request.Context);
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> BulkCreate(
        [FromBody] BulkCreateTranslationsRequest request,
        CancellationToken cancellationToken)
    {
        var command = new BulkCreateTranslationsCommand(request.AppId, request.Translations);
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        string id,
        [FromBody] UpdateTranslationRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateTranslationCommand(id, request.Key, request.Text, request.Context);
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var command = new DeleteTranslationCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }
}
