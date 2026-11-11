using AutoMapper;
using WorkForceHub.Application.DTOs.Department;
using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.MappingProfile
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile() {

            //Department ---> DepartmentDto
            CreateMap<Department, DepartmentDto>();

            //CreateDepartmentDto ---> Department
            CreateMap<CreateDepartmentDto, Department>()
                .ForMember(dest => dest.DepartmentId, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());


            //UpdateDepartmentDto ---> Department
            CreateMap<UpdateDepartmentDto, Department>()
                .ForMember(dest=> dest.CreatedAt, opt=>opt.Ignore())
                .ForMember(dest=>dest.UpdatedAt, opt=>opt.Ignore());

            //Department ---> UpdateDepartmentDto
            CreateMap<Department, UpdateDepartmentDto>();

            
        }
    }
}
