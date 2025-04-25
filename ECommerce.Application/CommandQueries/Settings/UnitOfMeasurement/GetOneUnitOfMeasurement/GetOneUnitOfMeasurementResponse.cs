using AutoMapper;
using ECommerce.Application.CommandQueries.Common.Mapping;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementConversion.GetUnitOfMeasurementConversion;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.GetOneUnitOfMeasurement
{
    public sealed class GetOneUnitOfMeasurementResponse
    {
        #region Properties

        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public UnitOfMeasurementTypeFragmentResponse UnitOfMeasurementType { get; set; }
        public bool CanEnable { get; set; }
        public bool CanDisable { get; set; }
        public bool CanDelete { get; set; }
        public bool CanUpdate { get; set; }
        public Guid? Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public UserFragmentResponse CreatedBy { get; set; } = new();
        public UserFragmentResponse ModifiedBy { get; set; } = new();
        public List<GetUnitOfMeasurementConversionResponse> ConversionsFrom { get; set; }
        public List<GetUnitOfMeasurementConversionResponse> ConversionsTo { get; set; }

        #endregion Properties

        #region Internal Methods

        internal static GetOneUnitOfMeasurementResponse MapToResponse(IMapper mapper, Domain.Entities.Settings.UnitOfMeasurement unitOfMeasurement)
        {
            if (unitOfMeasurement == null)
                throw new ArgumentNullException(nameof(unitOfMeasurement));

            return new GetOneUnitOfMeasurementResponse()
            {
                Name = unitOfMeasurement.Name,
                Abbreviation = unitOfMeasurement.Abbreviation,
                ConversionsFrom = mapper.Map<List<GetUnitOfMeasurementConversionResponse>>(unitOfMeasurement.ConvertFroms),
                ConversionsTo = mapper.Map<List<GetUnitOfMeasurementConversionResponse>>(unitOfMeasurement.ConvertTos),
                Id = unitOfMeasurement.Id,
                Status = unitOfMeasurement.Status,
                CanEnable = unitOfMeasurement.Status == Domain.Enums.Status.Disabled.GetDescription(),
                CanDisable = unitOfMeasurement.Status == Domain.Enums.Status.Active.GetDescription(),
                CanUpdate = unitOfMeasurement.Status == Domain.Enums.Status.Active.GetDescription(),
                CanDelete = unitOfMeasurement.Status == Domain.Enums.Status.Active.GetDescription(),
                CreatedDate = unitOfMeasurement.CreatedDate,
                ModifiedDate = unitOfMeasurement.ModifiedDate,
                UnitOfMeasurementType = mapper.Map<UnitOfMeasurementTypeFragmentResponse>(unitOfMeasurement.UnitOfMeasurementType),
                CreatedBy = mapper.Map<UserFragmentResponse>(unitOfMeasurement.CreatedBy),
                ModifiedBy = mapper.Map<UserFragmentResponse>(unitOfMeasurement.ModifiedBy)
            };
        }

        #endregion Internal Methods
    }
}