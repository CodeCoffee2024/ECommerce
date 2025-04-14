namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.GetUnitOfMeasurementDropdown
{
    public sealed class GetUnitOfMeasurementTypeDropdownResponse
    {
        #region Properties

        public string Name { get; set; } = string.Empty;
        public Guid? Id { get; set; }

        #endregion Properties

        #region Internal Methods

        internal static GetUnitOfMeasurementTypeDropdownResponse MapToResponse(ECommerce.Domain.Entities.Settings.UnitOfMeasurementType unitOfMeasurementType)
        {
            if (unitOfMeasurementType == null)
                throw new ArgumentNullException(nameof(unitOfMeasurementType));

            return new GetUnitOfMeasurementTypeDropdownResponse()
            {
                Name = unitOfMeasurementType.Name,
                Id = unitOfMeasurementType.Id,
            };
        }

        #endregion Internal Methods
    }
}