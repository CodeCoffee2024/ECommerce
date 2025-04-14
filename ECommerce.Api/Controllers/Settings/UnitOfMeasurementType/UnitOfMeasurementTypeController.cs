using ECommerce.Api.Controllers.ActivityLog;
using ECommerce.Api.Controllers.Settings.UnitOfMeasurementType;
using ECommerce.Api.Middleware.Authorization;
using ECommerce.Api.Shared;
using ECommerce.Application.CommandQueries.ActivityLog.GetActivityLog;
using ECommerce.Application.CommandQueries.ActivityLog.GetOneActivityLog;
using ECommerce.Application.CommandQueries.Common;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.DeleteUnitOfMeasurementType;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.GetOneUnitOfMeasurentType;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.GetUnitOfMeasurementType;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.GetUnitOfMeasurementTypeStatuses;
using ECommerce.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Settings
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UnitOfMeasurementTypeController : ApiBaseController
    {
        #region Fields

        private readonly ISender _sender;

        #endregion Fields

        #region Public Constructors

        public UnitOfMeasurementTypeController(ISender sender)
        {
            _sender = sender;
        }

        #endregion Public Constructors

        #region Public Methods

        //[HttpGet("GetAllUnitOfMeasuremenTypes")]
        //public async Task<IActionResult> GetAllUserPermissions([FromQuery] GetAllModuleQuery request, CancellationToken cancellationToken)
        //{
        //    var result = await _sender.Send(request);

        //    return HandleResponse(result);
        //}

        [HttpGet("Dropdown")]
        public async Task<IActionResult> Dropdown([FromQuery] GenericListingRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request.SetQuery<GetUnitOfMeasurementTypeDropdownQuery>(), cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("GetUnitOfMeasurementTypes")]
        [AuthorizePermission(Permissions.UserEnableToViewUnitOfMeasurementType)]
        public async Task<IActionResult> GetListing([FromQuery] GetUnitOfMeasurementTypeRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request.SetQuery<GetUnitOfMeasurementTypeQuery>(), cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("GetActivityLogs")]
        [AuthorizePermission(Permissions.UserEnableToViewUnitOfMeasurementType)]
        public async Task<IActionResult> GetAllActivityLogs([FromQuery] GenericListingRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request.SetQuery<GetActivityLogQuery>(), cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("GetStatuses")]
        public async Task<IActionResult> GetStatuses([FromQuery] GetUnitOfMeasurementTypeStatusesQuery request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request, cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("GetActivityLog/{Id}")]
        [AuthorizePermission(Permissions.UserEnableToViewUnitOfMeasurementType)]
        public async Task<IActionResult> GetActivityLog([FromRoute] string Id, CancellationToken cancellationToken)
        {
            ActivityLogRequest request = new ActivityLogRequest();
            var query = new GetOneActivityLogQuery(Guid.Parse(Id));

            var result = await _sender.Send(query, cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("{Id}")]
        [AuthorizePermission(Permissions.UserEnableToViewUnitOfMeasurementType)]
        public async Task<IActionResult> Show([FromRoute] string Id, CancellationToken cancellationToken)
        {
            UnitOfMeasurementTypeRequest request = new UnitOfMeasurementTypeRequest();
            var query = new GetOneUnitOfMeasurementTypeQuery(Guid.Parse(Id));

            var result = await _sender.Send(query, cancellationToken);

            return HandleResponse(result);
        }

        [HttpPost]
        [AuthorizePermission(Permissions.UserEnableToModifyUnitOfMeasurementType)]
        public async Task<IActionResult> Create([FromBody] UnitOfMeasurementTypeRequest request, CancellationToken cancellationToken)
        {
            var command = request.SetAddCommand(UserId);
            var result = await _sender.Send(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("Disable/{Id}")]
        [AuthorizePermission(Permissions.UserEnableToDeleteUnitOfMeasurementType)]
        public async Task<IActionResult> Disable([FromRoute] UnitOfMeasurementTypeRequest request, [FromRoute] string Id, CancellationToken cancellationToken)
        {
            var command = request.SetToDisableCommand(Guid.Parse(Id), UserId);
            var result = await _sender.Send(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("Enable/{Id}")]
        [AuthorizePermission(Permissions.UserEnableToDeleteUnitOfMeasurementType)]
        public async Task<IActionResult> Enable([FromRoute] UnitOfMeasurementTypeRequest request, [FromRoute] string Id, CancellationToken cancellationToken)
        {
            var command = request.SetToEnableCommand(Guid.Parse(Id), UserId);
            var result = await _sender.Send(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpPut("{Id}")]
        [AuthorizePermission(Permissions.UserEnableToDeleteUnitOfMeasurementType)]
        public async Task<IActionResult> Update([FromBody] UnitOfMeasurementTypeRequest request, [FromRoute] string Id, CancellationToken cancellationToken)
        {
            var command = request.SetUpdateCommand(Guid.Parse(Id), UserId);
            var result = await _sender.Send(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpDelete("{Id}")]
        [AuthorizePermission(Permissions.UserEnableToDeleteUnitOfMeasurementType)]
        public async Task<IActionResult> Delete([FromRoute] string Id, CancellationToken cancellationToken)
        {
            var command = new DeleteUnitOfMeasurementTypeCommand(Guid.Parse(Id));
            var result = await _sender.Send(command, cancellationToken);

            return HandleResponse(result);
        }

        #endregion Public Methods
    }
}