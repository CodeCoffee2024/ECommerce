using AutoMapper;
using ECommerce.Application.CommandQueries.Common.Mapping;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.GetUnitOfMeasurement
{
    public sealed class GetUnitOfMeasurementResponse
    {
        #region Properties

        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public Guid? Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public UnitOfMeasurementTypeFragmentResponse UnitOfMeasurementType { get; set; }
        public UserFragmentResponse CreatedBy { get; set; } = new();
        public UserFragmentResponse ModifiedBy { get; set; } = new();

        #endregion Properties

        #region Methods

        internal static GetUnitOfMeasurementResponse MapToResponse(IMapper mapper, ECommerce.Domain.Entities.Settings.UnitOfMeasurement unitOfMeasurement)
        {
            if (unitOfMeasurement == null)
                throw new ArgumentNullException(nameof(unitOfMeasurement));

            return new GetUnitOfMeasurementResponse()
            {
                Name = unitOfMeasurement.Name,
                Id = unitOfMeasurement.Id,
                Abbreviation = unitOfMeasurement.Abbreviation,
                UnitOfMeasurementType = mapper.Map<UnitOfMeasurementTypeFragmentResponse>(unitOfMeasurement.UnitOfMeasurementType),
                Status = unitOfMeasurement.Status,
                CreatedDate = unitOfMeasurement.CreatedDate,
                ModifiedDate = unitOfMeasurement.ModifiedDate,
                CreatedBy = mapper.Map<UserFragmentResponse>(unitOfMeasurement.CreatedBy),
                ModifiedBy = mapper.Map<UserFragmentResponse>(unitOfMeasurement.ModifiedBy)
            };
        }

        #endregion Methods
    }
}