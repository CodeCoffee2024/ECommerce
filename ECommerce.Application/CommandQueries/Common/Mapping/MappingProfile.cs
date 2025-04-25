using AutoMapper;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementConversion.GetUnitOfMeasurementConversion;
using ECommerce.Domain.Entities.Settings;

namespace ECommerce.Application.CommandQueries.Common.Mapping
{
    public class MappingProfile : Profile
    {
        #region Public Constructors

        public MappingProfile()
        {
            CreateMap<UnitOfMeasurementType, UnitOfMeasurementTypeFragmentResponse>().ReverseMap();
            CreateMap<UnitOfMeasurementConversion, GetUnitOfMeasurementConversionResponse>()
            .ForMember(dest => dest.UnitOfMeasurementFrom, opt => opt.MapFrom(src => new UnitOfMeasurementFragmentResponse
            {
                Id = src.ConvertFrom.Id,
                Name = src.ConvertFrom.Name,
                Abbreviation = src.ConvertFrom.Abbreviation
            }))
            .ForMember(dest => dest.UnitOfMeasurementTo, opt => opt.MapFrom(src => new UnitOfMeasurementFragmentResponse
            {
                Id = src.ConvertTo.Id,
                Name = src.ConvertTo.Name,
                Abbreviation = src.ConvertTo.Abbreviation
            }));
            CreateMap<ECommerce.Domain.Entities.UserManagement.User, UserFragmentResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                $"{src.FirstName ?? "Unknown"} {src.LastName ?? ""}".Trim()));
        }

        #endregion Public Constructors
    }
}