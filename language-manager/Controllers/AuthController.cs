using language_manager.Application.Auth.Commands;
using language_manager.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace language_manager.Controllers;

[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email, request.Password);
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterCommand(request.Email, request.Password);
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }
}
