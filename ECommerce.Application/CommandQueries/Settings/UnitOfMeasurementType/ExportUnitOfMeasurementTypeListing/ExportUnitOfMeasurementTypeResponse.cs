using ECommerce.Application.Common.Helpers;
using ECommerce.Domain.Enums;
using System.ComponentModel;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.ExportUnitOfMeasurementTypeListing
{
    public sealed class ExportUnitOfMeasurementTypeResponse
    {
        #region Properties

        [Description("Name")]
        public string Name { get; set; } = string.Empty;

        [Description("Has Decimal?")]
        public string? HasDecimal { get; set; } = string.Empty;

        [Description("Status")]
        public string Status { get; set; } = string.Empty;

        [Description("Created Date")]
        public string? CreatedDate { get; set; }

        [Description("Created By")]
        public string CreatedBy { get; set; } = string.Empty;

        #endregion Properties

        #region Methods

        internal static ExportUnitOfMeasurementTypeResponse MapToResponse(ECommerce.Domain.Entities.Settings.UnitOfMeasurementType unitOfMeasurementType)
        {
            if (unitOfMeasurementType == null)
                throw new ArgumentNullException(nameof(unitOfMeasurementType));

            return new ExportUnitOfMeasurementTypeResponse()
            {
                Name = unitOfMeasurementType.Name,
                Status = EnumExtensions.GetEnumFromDescription<Status>(unitOfMeasurementType.Status).ToString(),
                HasDecimal = unitOfMeasurementType.HasDecimal ? "Yes" : "No",
                CreatedDate = DateHelper.ToFormattedDate(unitOfMeasurementType.CreatedDate!.Value),
                CreatedBy = $"{unitOfMeasurementType.CreatedBy?.FirstName ?? "Unknown"}  {unitOfMeasurementType.CreatedBy?.LastName ?? ""}".Trim(),
            };
        }

        #endregion Methods
    }
}