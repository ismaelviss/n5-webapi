using AutoMapper;
using n5.webApi.Models.dto;

namespace n5.webApi.Models.Mapper
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, PermissionDto>()
                .ForMember(
                    destinationMember => destinationMember.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    destinationMember => destinationMember.EmployeeName,
                    opt => opt.MapFrom(src => src.EmployeeName)
                )
                .ForMember(
                    destinationMember => destinationMember.EmployeeLastName,
                    opt => opt.MapFrom(src => src.EmployeeLastName)
                )
                .ForMember(
                    destinationMember => destinationMember.PermissionTypeId,
                    opt => opt.MapFrom(src => src.PermissionTypeId)
                )
                .ForMember(
                    destinationMember => destinationMember.PermissionDate,
                    opt => opt.MapFrom(src => src.PermissionDate)
                );

        }
    }
}
