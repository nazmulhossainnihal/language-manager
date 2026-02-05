using language_manager.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace language_manager.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return result.StatusCode switch
            {
                201 => Created(string.Empty, result.Data),
                204 => NoContent(),
                _ => Ok(result.Data)
            };
        }

        return result.StatusCode switch
        {
            401 => Unauthorized(new { error = result.Error }),
            404 => NotFound(new { error = result.Error }),
            409 => Conflict(new { error = result.Error }),
            _ => BadRequest(new { error = result.Error })
        };
    }

    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess)
        {
            return result.StatusCode switch
            {
                204 => NoContent(),
                _ => Ok()
            };
        }

        return result.StatusCode switch
        {
            401 => Unauthorized(new { error = result.Error }),
            404 => NotFound(new { error = result.Error }),
            409 => Conflict(new { error = result.Error }),
            _ => BadRequest(new { error = result.Error })
        };
    }
}
