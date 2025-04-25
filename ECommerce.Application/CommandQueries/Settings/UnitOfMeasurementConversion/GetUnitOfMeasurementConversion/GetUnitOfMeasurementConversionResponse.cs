using AutoMapper;
using ECommerce.Application.CommandQueries.Common.Mapping;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementConversion.GetUnitOfMeasurementConversion
{
    public sealed class GetUnitOfMeasurementConversionResponse
    {
        #region Properties

        public Guid? Id { get; set; }
        public UnitOfMeasurementFragmentResponse? UnitOfMeasurementFrom { get; set; }
        public UnitOfMeasurementFragmentResponse? UnitOfMeasurementTo { get; set; }
        public decimal? Value { get; set; }

        #endregion Properties

        #region Methods

        internal static GetUnitOfMeasurementConversionResponse MapToResponse(IMapper mapper, ECommerce.Domain.Entities.Settings.UnitOfMeasurementConversion conversion)
        {
            if (conversion == null)
                throw new ArgumentNullException(nameof(conversion));
            return new GetUnitOfMeasurementConversionResponse
            {
                Id = conversion.Id,
                UnitOfMeasurementFrom = new UnitOfMeasurementFragmentResponse
                {
                    Id = conversion.ConvertFrom.Id,
                    Name = conversion.ConvertFrom.Name,
                    Abbreviation = conversion.ConvertFrom.Abbreviation,
                },
                UnitOfMeasurementTo = new UnitOfMeasurementFragmentResponse
                {
                    Id = conversion.ConvertTo.Id,
                    Name = conversion.ConvertTo.Name,
                    Abbreviation = conversion.ConvertTo.Abbreviation
                },
                Value = conversion.Value
            };
        }

        #endregion Methods
    }
}