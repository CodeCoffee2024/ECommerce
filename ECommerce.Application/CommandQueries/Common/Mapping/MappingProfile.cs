using AutoMapper;
using ECommerce.Domain.Entities.Settings;

namespace ECommerce.Application.CommandQueries.Common.Mapping
{
    public class MappingProfile : Profile
    {
        #region Public Constructors

        public MappingProfile()
        {
            CreateMap<UnitOfMeasurementType, UnitOfMeasurementTypeFragmentResponse>().ReverseMap();
            CreateMap<ECommerce.Domain.Entities.UserManagement.User, UserFragmentResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                $"{src.FirstName ?? "Unknown"} {src.LastName ?? ""}".Trim()));
        }

        #endregion Public Constructors
    }
}