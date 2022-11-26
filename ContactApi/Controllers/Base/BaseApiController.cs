using Microsoft.AspNetCore.Mvc;
using Utility;

namespace ContactApi.Controllers.Base
{
    public abstract class BaseApiController : ControllerBase
    {
        protected IActionResult BuildResponse<T>(Request<T> request) where T : class
        {
            if (request.IsSuccessful)
            {
                return Ok(request.Value);
            }
            return StatusCode(400, new
            {
                Errors = request.Errors
            });
        }
    }
}
