using AutoMapper;
using WorkForceHub.Application.DTOs.Notifications;
using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.MappingProfile
{
    public class NotificationMappingProfile : Profile
    {
        public NotificationMappingProfile() {

            //CreateNofificationDto ---> Notification
            CreateMap<CreateNotificationDto, Notification>();


            //Notification ---> NotificationDto
            CreateMap<Notification, NotificationDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName));
        }
    }
}
