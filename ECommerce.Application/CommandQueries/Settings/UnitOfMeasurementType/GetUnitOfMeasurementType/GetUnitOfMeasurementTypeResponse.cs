using AutoMapper;
using ECommerce.Application.CommandQueries.Common.Mapping;

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
        public UserFragmentResponse CreatedBy { get; set; } = new();
        public UserFragmentResponse ModifiedBy { get; set; } = new();

        #endregion Properties

        #region Methods

        internal static GetUnitOfMeasurementTypeResponse MapToResponse(IMapper mapper, Domain.Entities.Settings.UnitOfMeasurementType unitOfMeasurementType)
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
                CreatedBy = mapper.Map<UserFragmentResponse>(unitOfMeasurementType.CreatedBy),
                ModifiedBy = mapper.Map<UserFragmentResponse>(unitOfMeasurementType.ModifiedBy)
            };
        }

        #endregion Methods
    }
}