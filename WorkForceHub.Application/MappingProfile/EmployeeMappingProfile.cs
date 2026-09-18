using AutoMapper;
using WorkForceHub.Application.DTOs.Employee;
using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.MappingProfile
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            //Employee -->CreateEmployeeDto
            CreateMap<Employee, CreateEmployeeDto>();

            //CreateEmployeeDto --> Employee
            CreateMap<CreateEmployeeDto, Employee>();

            //Employee --> UpdateEmployeeDto
            CreateMap<Employee, UpdateEmployeeDto>();

            //UpdateEmployeeDto --> Employee
            CreateMap<UpdateEmployeeDto, Employee>().ForMember(dest => dest.CreatedAt, opt => opt.Ignore()).ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            //Employee --> EmployeeDto
            CreateMap<Employee, EmployeeDto>();

            //EmployeeDto --> Employee
            CreateMap<EmployeeDto, Employee>();

            //Employee --> EmployeeDetailsDto
            CreateMap<Employee, EmployeeDetailsDto>();

            //EmployeeDetailsDto --> Employee
            CreateMap<EmployeeDetailsDto, Employee>();
        }
    }
}
