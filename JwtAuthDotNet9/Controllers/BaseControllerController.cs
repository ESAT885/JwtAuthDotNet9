using JwtAuthDotNet9.Models.BaseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDotNet9.Controllers
{
    
    [ApiController]
    public class BaseControllerController : ControllerBase
    {
        protected IActionResult Success<T>(T data, string? message = null)
        {
            return Ok(ApiResponse<T>.Ok(data, message));
        }

        protected IActionResult Success(string? message = null)
        {
            return Ok(ApiResponse<object>.Ok(null!, message));
        }

        protected IActionResult Fail(string message)
        {
            return BadRequest(ApiResponse<object>.Fail(message));
        }
    }
}
