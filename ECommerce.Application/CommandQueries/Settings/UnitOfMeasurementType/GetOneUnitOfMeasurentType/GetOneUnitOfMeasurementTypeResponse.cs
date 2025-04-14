using AutoMapper;
using ECommerce.Application.CommandQueries.Common.Mapping;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.GetOneUnitOfMeasurentType
{
    public sealed class GetOneUnitOfMeasurementTypeResponse
    {
        #region Properties

        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string HasDecimal { get; set; }
        public bool CanEnable { get; set; }
        public bool CanDisable { get; set; }
        public bool CanDelete { get; set; }
        public bool CanUpdate { get; set; }
        public Guid? Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public UserFragmentResponse CreatedBy { get; set; } = new();
        public UserFragmentResponse ModifiedBy { get; set; } = new();

        #endregion Properties

        #region Methods

        internal static GetOneUnitOfMeasurementTypeResponse MapToResponse(IMapper mapper, Domain.Entities.Settings.UnitOfMeasurementType unitOfMeasurementType)
        {
            if (unitOfMeasurementType == null)
                throw new ArgumentNullException(nameof(unitOfMeasurementType));

            return new GetOneUnitOfMeasurementTypeResponse()
            {
                Name = unitOfMeasurementType.Name,
                Id = unitOfMeasurementType.Id,
                CreatedDate = unitOfMeasurementType.CreatedDate,
                HasDecimal = unitOfMeasurementType.HasDecimal ? "Yes" : "No",
                ModifiedDate = unitOfMeasurementType.ModifiedDate,
                Status = unitOfMeasurementType.Status,
                CanEnable = unitOfMeasurementType.Status == Domain.Enums.Status.Disabled.GetDescription(),
                CanDisable = unitOfMeasurementType.Status == Domain.Enums.Status.Active.GetDescription(),
                CanUpdate = unitOfMeasurementType.Status == Domain.Enums.Status.Active.GetDescription(),
                CanDelete = unitOfMeasurementType.Status == Domain.Enums.Status.Active.GetDescription(),
                CreatedBy = mapper.Map<UserFragmentResponse>(unitOfMeasurementType.CreatedBy),
                ModifiedBy = mapper.Map<UserFragmentResponse>(unitOfMeasurementType.ModifiedBy)
            };
        }

        #endregion Methods
    }
}