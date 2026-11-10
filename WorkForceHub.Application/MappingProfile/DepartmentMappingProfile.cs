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
            CreateMap<CreateDepartmentDto, Department>();

            //UpdateDepartmentDto ---> Department
            CreateMap<UpdateDepartmentDto, Department>()
                .ForMember(dest=> dest.CreatedAt, opt=>opt.Ignore())
                .ForMember(dest=>dest.UpdatedAt, opt=>opt.Ignore());

            //Department ---> UpdateDepartmentDto
            CreateMap<Department, UpdateDepartmentDto>();

            
        }
    }
}
