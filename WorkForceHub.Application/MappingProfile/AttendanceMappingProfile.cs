
using AutoMapper;
using WorkForceHub.Application.DTOs.Attendance;
using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.MappingProfile
{
    public class AttendanceMappingProfile : Profile
    {
        public AttendanceMappingProfile()
        {
            // Attendance ---> AttendanceDto
            CreateMap<Attendance, AttendanceDto>();

            // CreateAttendanceDto ---> Attendance
            CreateMap<CreateAttendanceDto, Attendance>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.AttendanceId, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore());

            // UpdateAttendanceDto ---> Attendance
            CreateMap<UpdateAttendanceDto, Attendance>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore());

            // Attendance ---> AttendanceDetailsDto
            CreateMap<Attendance, AttendanceDetailsDto>()
                .ForMember(dest => dest.EmployeeCode,
                    opt => opt.MapFrom(src => src.Employee.EmployeeCode))
                .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName));
        }
    }
}
