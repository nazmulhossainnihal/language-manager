using language_manager.Application.AppLanguages.Commands;
using language_manager.Application.AppLanguages.Queries;
using language_manager.Application.Apps.Commands;
using language_manager.Application.Apps.Queries;
using language_manager.Application.AppUsers.Commands;
using language_manager.Application.AppUsers.Queries;
using language_manager.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace language_manager.Controllers;

[Route("api/[controller]")]
[Authorize]
public class AppsController : BaseController
{
    private readonly IMediator _mediator;

    public AppsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllAppsQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetAppQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateAppRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateAppCommand(
            request.Name,
            request.Domain,
            request.Environment,
            request.DefaultLanguageId);
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        string id,
        [FromBody] UpdateAppRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateAppCommand(
            id,
            request.Name,
            request.Domain,
            request.Environment,
            request.DefaultLanguageId);
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var command = new DeleteAppCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("{id}/users")]
    public async Task<IActionResult> GetAppUsers(string id, CancellationToken cancellationToken)
    {
        var query = new GetAppUsersQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return HandleResult(result);
    }

    [HttpPost("{id}/users")]
    public async Task<IActionResult> AddUserToApp(
        string id,
        [FromBody] AddUserToAppRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddUserToAppCommand(request.UserId, id, request.Role);
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpPut("{id}/users/{userId}")]
    public async Task<IActionResult> UpdateAppUserRole(
        string id,
        string userId,
        [FromBody] UpdateAppUserRoleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateAppUserRoleCommand(userId, id, request.Role);
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("{id}/users/{userId}")]
    public async Task<IActionResult> RemoveUserFromApp(
        string id,
        string userId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveUserFromAppCommand(userId, id);
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("{id}/languages")]
    public async Task<IActionResult> GetAppLanguages(string id, CancellationToken cancellationToken)
    {
        var query = new GetAppLanguagesQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return HandleResult(result);
    }

    [HttpPost("{id}/languages")]
    public async Task<IActionResult> AddLanguageToApp(
        string id,
        [FromBody] AddAppLanguageRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddAppLanguageCommand(id, request.LanguageId);
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("{id}/languages/{languageId}")]
    public async Task<IActionResult> RemoveLanguageFromApp(
        string id,
        string languageId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveAppLanguageCommand(id, languageId);
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }
}
