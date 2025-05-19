using ECommerce.Api.Controllers.ActivityLog;
using ECommerce.Api.Controllers.Inventory.ProductCategory;
using ECommerce.Api.Middleware.Authorization;
using ECommerce.Api.Shared;
using ECommerce.Application.CommandQueries.ActivityLog.GetActivityLog;
using ECommerce.Application.CommandQueries.ActivityLog.GetOneActivityLog;
using ECommerce.Application.CommandQueries.Common;
using ECommerce.Application.CommandQueries.Inventory.ProductCategory.GetOneProductCategory;
using ECommerce.Application.CommandQueries.Inventory.ProductCategory.GetProductCategory;
using ECommerce.Application.CommandQueries.Inventory.ProductCategory.GetProductCategoryDropdown;
using ECommerce.Application.CommandQueries.Inventory.ProductCategory.GetProductCategoryStatuses;
using ECommerce.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductCategoryController : ApiBaseController
    {
        #region Fields

        private readonly ISender _sender;

        #endregion Fields

        #region Public Constructors

        public ProductCategoryController(ISender sender)
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
        public async Task<IActionResult> Dropdown([FromQuery] GetProductCategoryRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request.SetQuery<GetProductCategoryDropdownQuery>(), cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("GetProductCategories")]
        [AuthorizePermission(Permissions.UserEnableToViewProductCategory)]
        public async Task<IActionResult> GetListing([FromQuery] GetProductCategoryRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request.SetQuery<GetProductCategoryQuery>(), cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("GetActivityLogs")]
        [AuthorizePermission(Permissions.UserEnableToViewProductCategory)]
        public async Task<IActionResult> GetAllActivityLogs([FromQuery] GenericListingRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request.SetQuery<GetActivityLogQuery>(), cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("GetStatuses")]
        public async Task<IActionResult> GetStatuses([FromQuery] GetProductCategoryStatusesQuery request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request, cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("GetActivityLog/{Id}")]
        [AuthorizePermission(Permissions.UserEnableToViewProductCategory)]
        public async Task<IActionResult> GetActivityLog([FromRoute] string Id, CancellationToken cancellationToken)
        {
            ActivityLogRequest request = new ActivityLogRequest();
            var query = new GetOneActivityLogQuery(Guid.Parse(Id));

            var result = await _sender.Send(query, cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("{Id}")]
        [AuthorizePermission(Permissions.UserEnableToViewProductCategory)]
        public async Task<IActionResult> Show([FromRoute] string Id, CancellationToken cancellationToken)
        {
            ProductCategoryRequest request = new ProductCategoryRequest();
            var query = new GetOneProductCategoryQuery(Guid.Parse(Id));

            var result = await _sender.Send(query, cancellationToken);

            return HandleResponse(result);
        }

        [HttpPost]
        [AuthorizePermission(Permissions.UserEnableToModifyProductCategory)]
        public async Task<IActionResult> Create([FromBody] ProductCategoryRequest request, CancellationToken cancellationToken)
        {
            var command = request.SetAddCommand(UserId);
            var result = await _sender.Send(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("Disable/{Id}")]
        [AuthorizePermission(Permissions.UserEnableToModifyProductCategory)]
        public async Task<IActionResult> Disable([FromRoute] ProductCategoryRequest request, [FromRoute] string Id, CancellationToken cancellationToken)
        {
            var command = request.SetToDisableCommand(Guid.Parse(Id), UserId);
            var result = await _sender.Send(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("Enable/{Id}")]
        [AuthorizePermission(Permissions.UserEnableToModifyProductCategory)]
        public async Task<IActionResult> Enable([FromRoute] ProductCategoryRequest request, [FromRoute] string Id, CancellationToken cancellationToken)
        {
            var command = request.SetToEnableCommand(Guid.Parse(Id), UserId);
            var result = await _sender.Send(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpPut("{Id}")]
        [AuthorizePermission(Permissions.UserEnableToModifyProductCategory)]
        public async Task<IActionResult> Update([FromBody] ProductCategoryRequest request, [FromRoute] string Id, CancellationToken cancellationToken)
        {
            var command = request.SetUpdateCommand(Guid.Parse(Id), UserId);
            var result = await _sender.Send(command, cancellationToken);

            return HandleResponse(result);
        }

        //[HttpDelete("{Id}")]
        //[AuthorizePermission(Permissions.UserEnableToDeleteProductCategory)]
        //public async Task<IActionResult> Delete([FromRoute] string Id, CancellationToken cancellationToken)
        //{
        //    var command = new DeleteProductCategoryTypeCommand(Guid.Parse(Id));
        //    var result = await _sender.Send(command, cancellationToken);

        //    return HandleResponse(result);
        //}

        #endregion Public Methods
    }
}