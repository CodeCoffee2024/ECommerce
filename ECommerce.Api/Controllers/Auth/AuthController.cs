using ECommerce.Api.Shared;
using ECommerce.Application.CommandQueries.Auth.GetRefreshToken;
using ECommerce.Application.CommandQueries.Auth.GetUserAccessQuery;
using ECommerce.Application.CommandQueries.Auth.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ApiBaseController
    {
        #region Fields

        private readonly ISender _sender;

        #endregion Fields

        #region Public Constructors

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginQuery request, CancellationToken cancellationToken)
        {
            //var query = request.Login();

            var result = await _sender.Send(request);

            return HandleResponse(result);
        }

        [HttpGet("GetUserAccess")]
        [Authorize]
        public async Task<IActionResult> GetUserAccess(CancellationToken cancellationToken)
        {
            //var query = request.Login();
            var userAccessQuery = new GetUserAccessQuery(UserId);
            var result = await _sender.Send(userAccessQuery);

            return HandleResponse(result);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> RefReshToken([FromBody] AuthRefreshTokenRequest refreshToken, CancellationToken cancellationToken)
        {
            //var query = request.Login();
            var userAccessQuery = new RefreshTokenQuery(refreshToken.RefreshToken);
            var result = await _sender.Send(userAccessQuery);

            return HandleResponse(result);
        }

        #endregion Public Constructors
    }
}