using ECommerce.Api.Shared;
using ECommerce.Application.CommandQueries.Auth.Login;
using MediatR;
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

        #endregion Public Constructors
    }
}