using ECommerce.Api.Middleware.Authorization;
using ECommerce.Api.Shared;
using ECommerce.Application.CommandQueries.Common;
using ECommerce.Application.CommandQueries.UserManagement.Module.GetAllModule;
using ECommerce.Application.CommandQueries.UserManagement.Permission.DeleteUserPermission;
using ECommerce.Application.CommandQueries.UserManagement.Permission.GetOneUserPermission;
using ECommerce.Application.CommandQueries.UserManagement.Permission.GetUserPermission;
using ECommerce.Application.CommandQueries.UserManagement.Permission.GetUserPermissionDropdown;
using ECommerce.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.UserManagement.UserPermission
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserPermissionController : ApiBaseController
    {
        #region Fields

        private readonly ISender _sender;

        #endregion Fields

        #region Public Constructors

        public UserPermissionController(ISender sender)
        {
            _sender = sender;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet("GetAllUserPermissions")]
        public async Task<IActionResult> GetAllUserPermissions([FromQuery] GetAllModuleQuery request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request);

            return HandleResponse(result);
        }

        [HttpGet("Dropdown")]
        public async Task<IActionResult> Dropdown([FromQuery] GenericListingRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request.SetQuery<GetUserPermissionDropdownQuery>(), cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("GetUserPermissions")]
        [AuthorizePermission(Permissions.UserEnableToViewUserPermission)]
        public async Task<IActionResult> GetListing([FromQuery] GenericListingRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request.SetQuery<GetUserPermissionQuery>(), cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("{Id}")]
        [AuthorizePermission(Permissions.UserEnableToViewUserPermission)]
        public async Task<IActionResult> Show([FromRoute] string Id, CancellationToken cancellationToken)
        {
            UserPermissionRequest request = new UserPermissionRequest();
            var query = new GetOneUserPermissionQuery(Guid.Parse(Id));

            var result = await _sender.Send(query, cancellationToken);

            return HandleResponse(result);
        }

        [HttpPost]
        [AuthorizePermission(Permissions.UserEnableToModifyUserPermission)]
        public async Task<IActionResult> Create([FromBody] UserPermissionRequest request, CancellationToken cancellationToken)
        {
            var command = request.SetAddCommand(UserId);
            var result = await _sender.Send(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpPut("{Id}")]
        [AuthorizePermission(Permissions.UserEnableToModifyUserPermission)]
        public async Task<IActionResult> Update([FromBody] UserPermissionRequest request, [FromRoute] string Id, CancellationToken cancellationToken)
        {
            var command = request.SetUpdateCommand(Guid.Parse(Id), UserId);
            var result = await _sender.Send(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpDelete("{Id}")]
        [AuthorizePermission(Permissions.UserEnableToDeleteUserPermission)]
        public async Task<IActionResult> Delete([FromRoute] string Id, CancellationToken cancellationToken)
        {
            var command = new DeleteUserPermissionCommand(Guid.Parse(Id));
            var result = await _sender.Send(command, cancellationToken);

            return HandleResponse(result);
        }

        #endregion Public Methods
    }
}