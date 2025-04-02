using ECommerce.Api.Controllers.ActivityLog;
using ECommerce.Api.Middleware.Authorization;
using ECommerce.Api.Shared;
using ECommerce.Application.CommandQueries.ActivityLog.GetActivityLog;
using ECommerce.Application.CommandQueries.ActivityLog.GetOneActivityLog;
using ECommerce.Application.CommandQueries.Common;
using ECommerce.Application.CommandQueries.UserManagement.User;
using ECommerce.Application.CommandQueries.UserManagement.User.DeleteUser;
using ECommerce.Application.CommandQueries.UserManagement.User.GetOneUser;
using ECommerce.Application.CommandQueries.UserManagement.User.GetUser;
using ECommerce.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.UserManagement.User
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ApiBaseController
    {
        #region Fields

        private readonly ISender _sender;

        #endregion Fields

        #region Public Constructors

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet("GetUsers")]
        [AuthorizePermission(Permissions.UserEnableToViewUser)]
        public async Task<IActionResult> GetListing([FromQuery] UserListingRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request.SetQuery<GetUserQuery>(), cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("{Id}")]
        [AuthorizePermission(Permissions.UserEnableToViewUser)]
        public async Task<IActionResult> Show([FromRoute] string Id, CancellationToken cancellationToken)
        {
            UserRequest request = new UserRequest();
            var query = new GetOneUserQuery(Guid.Parse(Id));

            var result = await _sender.Send(query, cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
        {
            UserRequest request = new UserRequest();
            var query = new GetOneUserQuery(UserId);

            var result = await _sender.Send(query, cancellationToken);

            return HandleResponse(result);
        }

        [HttpPost]
        [AuthorizePermission(Permissions.UserEnableToModifyUser)]
        public async Task<IActionResult> Create([FromBody] UserRequest request, CancellationToken cancellationToken)
        {
            var command = request.SetAddCommand(UserId);
            var result = await _sender.Send(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpPut("{Id}")]
        [AuthorizePermission(Permissions.UserEnableToModifyUser)]
        public async Task<IActionResult> Update([FromBody] UserRequest request, [FromRoute] string Id, CancellationToken cancellationToken)
        {
            var command = request.SetUpdateCommand(Guid.Parse(Id), UserId);
            var result = await _sender.Send(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpPut("UpdateUserProfile")]
        public async Task<IActionResult> UpdateUserProfile([FromForm] UserRequest request, CancellationToken cancellationToken)
        {
            var command = request.SetUpdateProfileCommand(UserId, UserId);
            var result = await _sender.Send(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpDelete("{Id}")]
        [AuthorizePermission(Permissions.UserEnableToDeleteUser)]
        public async Task<IActionResult> Delete([FromRoute] string Id, CancellationToken cancellationToken)
        {
            var command = new DeleteUserCommand(Guid.Parse(Id));
            var result = await _sender.Send(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("GetActivityLogs")]
        [AuthorizePermission(Permissions.UserEnableToViewUser)]
        public async Task<IActionResult> GetAllActivityLogs([FromQuery] GenericListingRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request.SetQuery<GetActivityLogQuery>(), cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("GetActivityLog/{Id}")]
        [AuthorizePermission(Permissions.UserEnableToViewUser)]
        public async Task<IActionResult> GetActivityLog([FromRoute] string Id, CancellationToken cancellationToken)
        {
            ActivityLogRequest request = new ActivityLogRequest();
            var query = new GetOneActivityLogQuery(Guid.Parse(Id));

            var result = await _sender.Send(query, cancellationToken);

            return HandleResponse(result);
        }

        #endregion Public Methods
    }
}