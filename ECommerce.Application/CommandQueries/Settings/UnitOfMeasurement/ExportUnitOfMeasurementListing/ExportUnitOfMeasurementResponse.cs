using System.ComponentModel;

using ECommerce.Application.Common.Helpers;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.ExportUnitOfMeasurementListing
{
    public sealed class ExportUnitOfMeasurementResponse
    {
        #region Properties

        [Description("Name")]
        public string Name { get; set; } = string.Empty;

        [Description("Type")]
        public string? Type { get; set; } = string.Empty;

        [Description("Status")]
        public string Status { get; set; } = string.Empty;

        [Description("Created Date")]
        public string? CreatedDate { get; set; }

        [Description("Created By")]
        public string CreatedBy { get; set; } = string.Empty;

        #endregion Properties

        #region Methods

        internal static ExportUnitOfMeasurementResponse MapToResponse(ECommerce.Domain.Entities.Settings.UnitOfMeasurement unitOfMeasurement)
        {
            if (unitOfMeasurement == null)
                throw new ArgumentNullException(nameof(unitOfMeasurement));

            return new ExportUnitOfMeasurementResponse()
            {
                Name = unitOfMeasurement.Name,
                Status = EnumExtensions.GetEnumFromDescription<Status>(unitOfMeasurement.Status).ToString(),
                Type = unitOfMeasurement.UnitOfMeasurementType.Name,
                CreatedDate = DateHelper.ToFormattedDate(unitOfMeasurement.CreatedDate!.Value),
                CreatedBy = $"{unitOfMeasurement.CreatedBy?.FirstName ?? "Unknown"}  {unitOfMeasurement.CreatedBy?.LastName ?? ""}".Trim(),
            };
        }

        #endregion Methods
    }
}