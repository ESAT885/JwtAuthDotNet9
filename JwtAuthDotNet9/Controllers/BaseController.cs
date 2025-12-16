using JwtAuthDotNet9.Models.BaseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDotNet9.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected IActionResult Success<T>(T data, string? message = null)
        {
            var traceId = HttpContext.TraceIdentifier;
            return Ok(ApiResponse<T>.Ok(data,traceId, message));
        }

   
        protected IActionResult Fail(string message)
        {
            var traceId = HttpContext.TraceIdentifier;
            return BadRequest(ApiResponse<object>.Fail(message,traceId));
        }
    }
}
