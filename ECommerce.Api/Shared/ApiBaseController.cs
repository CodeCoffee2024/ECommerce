using ECommerce.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Shared
{
    [ApiController]
    public abstract class ApiBaseController : ControllerBase
    {
        #region Properties

        protected Guid UserId
        {
            get
            {
                var userIdClaim = User.FindFirst("UserId")?.Value;

                if (Guid.TryParse(userIdClaim, out var userId))
                {
                    return userId;
                }

                throw new UnauthorizedAccessException("User ID claim is missing or invalid.");
            }
        }

        protected IActionResult HandleResponse<T>(Result<T> result)
        {
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return result.Data != null ? Ok(result) : NotFound(result);
        }

        protected IActionResult HandleResponse(Result result)
        {
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        #endregion Properties
    }
}