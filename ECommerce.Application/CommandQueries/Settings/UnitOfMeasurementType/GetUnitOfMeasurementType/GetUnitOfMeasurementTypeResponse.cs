using ECommerce.Application.CommandQueries.Common;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.GetUnitOfMeasurementType
{
    public sealed class GetUnitOfMeasurementTypeResponse
    {
        #region Properties

        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string HasDecimal { get; set; }
        public Guid? Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public UserFragmentQueryResponse CreatedBy { get; set; } = new();
        public UserFragmentQueryResponse ModifiedBy { get; set; } = new();

        #endregion Properties

        #region Methods

        internal static GetUnitOfMeasurementTypeResponse MapToResponse(ECommerce.Domain.Entities.Settings.UnitOfMeasurementType unitOfMeasurementType)
        {
            if (unitOfMeasurementType == null)
                throw new ArgumentNullException(nameof(unitOfMeasurementType));

            return new GetUnitOfMeasurementTypeResponse()
            {
                Name = unitOfMeasurementType.Name,
                HasDecimal = unitOfMeasurementType.HasDecimal ? "Yes" : "No",
                Id = unitOfMeasurementType.Id,
                CreatedDate = unitOfMeasurementType.CreatedDate,
                ModifiedDate = unitOfMeasurementType.ModifiedDate,
                Status = unitOfMeasurementType.Status,
                CreatedBy = new UserFragmentQueryResponse()
                {
                    Id = unitOfMeasurementType.CreatedBy!.Id.ToString(),
                    Name = $"{unitOfMeasurementType.CreatedBy?.FirstName ?? "Unknown"} {unitOfMeasurementType.CreatedBy?.LastName ?? ""}".Trim()
                },
                ModifiedBy = new UserFragmentQueryResponse()
                {
                    Id = unitOfMeasurementType.CreatedBy!.Id.ToString(),
                    Name = $"{unitOfMeasurementType.ModifiedBy?.FirstName ?? "Unknown"} {unitOfMeasurementType.ModifiedBy?.LastName ?? ""}".Trim()
                }
            };
        }

        #endregion Methods
    }
}