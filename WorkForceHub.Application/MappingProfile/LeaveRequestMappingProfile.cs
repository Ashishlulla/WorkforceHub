using AutoMapper;
using WorkForceHub.Application.DTOs.LeaveRequest;
using WorkForceHub.Domain.Entities;
using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Application.MappingProfile
{
    public class LeaveRequestMappingProfile : Profile
    {
        public LeaveRequestMappingProfile()
        {

            //LeaveRequest ---> LeaveRequestDto
            CreateMap<LeaveRequest, LeaveRequestDto>().ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName));

            //LeaveRequest ---> LeaveRequestDetailsDto
            CreateMap<LeaveRequest, LeaveRequestDetailsDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName))
                .ForMember(dest => dest.ApprovedByEmployeeName, opt => opt.MapFrom(src => src.ApprovedByEmployee == null ? null :
                src.ApprovedByEmployee.FirstName + " " + src.ApprovedByEmployee.LastName));


            //CreateLeaveRequestDto ---> LeaveRequest
            CreateMap<CreateLeaveRequestDto, LeaveRequest>();

            //UpdateLeaveRequestStatusDto --> LeaveRequestDto
            CreateMap<UpdateLeaveRequestStatusDto, LeaveRequest>();

        }
    }
}