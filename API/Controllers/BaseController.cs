using API.Errors;
using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator mediator;

        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound(new ApiResponse(404));
            if (result.IsSuccess && result.Value == null) return NotFound(new ApiResponse(404));
            if (result.IsSuccess && result.Value != null) return Ok(result.Value);


            return BadRequest(new ApiResponse(400, result.Error));
        }
    }
}
