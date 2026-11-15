using AutoMapper;
using System.Xml.Serialization;
using WorkForceHub.Application.DTOs.Shift;
using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.MappingProfile
{
    public class ShiftMappingProfile : Profile
    {
        public ShiftMappingProfile() {

            //Shift ---> ShiftDto
            CreateMap<Shift, ShiftDto>();

            //Shift ---> ShiftDetailsDto 
            CreateMap<Shift, ShiftDetailsDto>()
                .ForMember(dest => dest.TotalEmployees, opt => opt.MapFrom(src => src.Employees.Count))
                .ForMember(dest => dest.EmployeeNames, opt => opt.MapFrom(src => src.Employees.Select(e => e.FirstName + " " + e.LastName)));

            //CreateShiftDto ---> Shift
            CreateMap<CreateShiftDto, Shift>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ShiftId, opt => opt.Ignore())
                .ForMember(dest=>dest.Employees, opt=>opt.Ignore());

            //UpdateShiftDto ---> Shift
            CreateMap<UpdateShiftDto, Shift>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest=>dest.CreatedAt, opt=> opt.Ignore())
                .ForMember(dest=>dest.Employees, opt=>opt.Ignore());

        }
    }
}
