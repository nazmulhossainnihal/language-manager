using language_manager.Application.DTOs;
using language_manager.Application.Modules.Commands;
using language_manager.Application.Modules.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace language_manager.Controllers;

[Route("api/[controller]")]
[Authorize]
public class ModulesController : BaseController
{
    private readonly IMediator _mediator;

    public ModulesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetModuleQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("app/{appId}")]
    public async Task<IActionResult> GetByAppId(string appId, CancellationToken cancellationToken)
    {
        var query = new GetModulesByAppQuery(appId);
        var result = await _mediator.Send(query, cancellationToken);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateModuleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateModuleCommand(request.AppId, request.ModuleKey, request.Name);
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        string id,
        [FromBody] UpdateModuleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateModuleCommand(id, request.ModuleKey, request.Name);
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var command = new DeleteModuleCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }
}
