using ECommerce.Application.CommandQueries.Common;
using ECommerce.Application.CommandQueries.Export;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.ExportUnitOfMeasurementTypeListing;
using ECommerce.Application.CommandQueries.UserManagement.Permission.ExportUserPermission;
using ECommerce.Application.CommandQueries.UserManagement.User.ExportUserListing;
using ECommerce.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Export
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        #region Fields

        private readonly ISender _sender;

        #endregion Fields

        #region Public Constructors

        public ExportController(ISender sender)
        {
            _sender = sender;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet("user-permission/{exportType}")]
        public async Task<IActionResult> ExportUserPermissions(string exportType, [FromQuery] ExportListingRequest request)
        {
            request.ReportName = "User Permission List";
            var result = await _sender.Send(request.SetQuery<ExportUserPermissionQuery>());
            if (result.Data.Result == null || !result.Data.Result.Any())
                return BadRequest("No data available for export.");

            // Convert filters to dictionary (or format as needed)
            var filters = new Dictionary<string, string>
            {
                { "Search", request.Search ?? string.Empty },
            };

            var exportQuery = new ExportQuery(result.Data.Result.ToList<object>(), exportType, request.ReportName, filters);
            var fileData = await _sender.Send(exportQuery);

            if (fileData == null || fileData.Length == 0)
                return BadRequest("Failed to generate the export file.");

            string contentType = exportType.ToLower() == "pdf"
                ? "application/pdf"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileExtension = exportType.ToLower() == "pdf" ? "pdf" : "xlsx";

            Console.WriteLine($"Export file generated: {fileData.Length} bytes.");

            return File(fileData, contentType, $"{request.ReportName}.{fileExtension}");
        }

        [HttpGet("user/{exportType}")]
        public async Task<IActionResult> ExportUsers(string exportType, [FromQuery] ExportUserListingRequest request)
        {
            request.ReportName = "User List";
            var result = await _sender.Send(request.SetQuery<ExportUserQuery>());
            if (result.Data.Result == null || !result.Data.Result.Any())
                return BadRequest("No data available for export.");

            // Convert filters to dictionary (or format as needed)
            var filters = new Dictionary<string, string>
            {
                { "Search", request.Search ?? string.Empty },
                { "User Permissions", request.UserPermissions ?? string.Empty },
            };

            var exportQuery = new ExportQuery(result.Data.Result.ToList<object>(), exportType, request.ReportName, filters);
            var fileData = await _sender.Send(exportQuery);

            if (fileData == null || fileData.Length == 0)
                return BadRequest("Failed to generate the export file.");

            string contentType = exportType.ToLower() == "pdf"
                ? "application/pdf"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileExtension = exportType.ToLower() == "pdf" ? "pdf" : "xlsx";

            Console.WriteLine($"Export file generated: {fileData.Length} bytes.");

            return File(fileData, contentType, $"{request.ReportName}.{fileExtension}");
        }

        [HttpGet("unit-of-measurement-type/{exportType}")]
        public async Task<IActionResult> ExportUnitOfMeasurmentTypes(string exportType, [FromQuery] ExportUnitOfMeasurementTypeListingRequest request)
        {
            request.ReportName = "Unit of Measurement Types";
            var result = await _sender.Send(request.SetQuery<ExportUnitOfMeasurementTypeQuery>());
            if (result.Data.Result == null || !result.Data.Result.Any())
                return BadRequest("No data available for export.");

            // Convert filters to dictionary (or format as needed)
            var filters = new Dictionary<string, string>
            {
                { "Search", request.Search ?? string.Empty },
                { "Status", request.Status != null ? EnumExtensions.GetEnumFromDescription<Status>(request.Status!).ToString() : string.Empty},
            };

            var exportQuery = new ExportQuery(result.Data.Result.ToList<object>(), exportType, request.ReportName, filters);
            var fileData = await _sender.Send(exportQuery);

            if (fileData == null || fileData.Length == 0)
                return BadRequest("Failed to generate the export file.");

            string contentType = exportType.ToLower() == "pdf"
                ? "application/pdf"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileExtension = exportType.ToLower() == "pdf" ? "pdf" : "xlsx";

            Console.WriteLine($"Export file generated: {fileData.Length} bytes.");

            return File(fileData, contentType, $"{request.ReportName}.{fileExtension}");
        }

        #endregion Public Methods
    }
}