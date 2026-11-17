using AutoMapper;
using WorkForceHub.Application.DTOs.Payroll;
using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.MappingProfile
{
    public class PayrollMappingProfile : Profile
    {
        public PayrollMappingProfile() {

            //Payroll ---> PayrollDto
            CreateMap<Payroll, PayrollDto>()
                .ForMember(dest=>dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FirstName +" "+ src.Employee.LastName));


            //Payroll ---> PayrollDetailsDto
            CreateMap<Payroll, PayrollDetailsDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName));

            //CreatePayrollDto ---> Payroll
            CreateMap<CreatePayrollDto, Payroll>();

            //UpdatePayrollStatusDto --->  Payroll
            CreateMap<UpdatePayrollStatusDto, Payroll>()
                .ForMember(dest => dest.PayrollId, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeId, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.PayrollYear, opt => opt.Ignore())
                .ForMember(dest => dest.PayrollMonth, opt => opt.Ignore())
                .ForMember(dest => dest.BasicSalary, opt => opt.Ignore())
                .ForMember(dest => dest.Allowances, opt => opt.Ignore())
                .ForMember(dest => dest.Deductions, opt => opt.Ignore())
                .ForMember(dest => dest.GrossSalary, opt => opt.Ignore())
                .ForMember(dest => dest.NetSalary, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        }
    }
}
