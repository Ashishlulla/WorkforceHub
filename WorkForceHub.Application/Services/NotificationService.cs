using AutoMapper;
using FluentValidation;
using WorkForceHub.Application.DTOs.Notifications;
using WorkForceHub.Application.Interfaces.RepositoryInterfaces;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;
using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.Services
{
    public class NotificationService :INotificationService
    {
        // Private Fields
        private readonly INotificationRepository _notificationRepository;
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateNotificationDto> _createNotificationDtoValidator;

        // Constructor
        public NotificationService(INotificationRepository notificationRepository, IEmployeeRepository employeeRepository, 
          IMapper mapper, 
          IValidator<CreateNotificationDto> createNotificationDtoValidator) {

            _notificationRepository = notificationRepository;
            _employeeRepository = employeeRepository;

            _mapper = mapper;

            _createNotificationDtoValidator = createNotificationDtoValidator;
        }

        public async Task<NotificationDto> CreateNotificationAsync(CreateNotificationDto createNotificationDto)
        {
            await _createNotificationDtoValidator.ValidateAndThrowAsync(createNotificationDto);

            Employee employee = await IsEmployeeExists(createNotificationDto.EmployeeId);

           
            Notification notification = _mapper.Map<Notification>(createNotificationDto);
            notification.Employee = employee;
            notification.CreatedAt = DateTime.UtcNow;

            Notification addedNotification = await _notificationRepository.AddNotificationAsync(notification);

            return _mapper.Map<NotificationDto>(addedNotification);
        }

        public async Task DeleteNotificationAsync(int id)
        {
            Notification notification = await IsNotificationExists(id);

            await _notificationRepository.DeleteNotificationAsync(notification);
        }

        public async Task<IEnumerable<NotificationDto>> GetNotificationsByEmployeeIdAsync(int employeeId)
        {
            await IsEmployeeExists(employeeId);

            IEnumerable<Notification> notifications = await _notificationRepository.GetNotificationsByEmployeeIdAsync(employeeId);

           
            return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }

        public async Task<NotificationDto> GetNotificationByIdAsync(int id)
        {
            Notification notification = await IsNotificationExists(id);

            return _mapper.Map<NotificationDto>(notification);
        }

        public async Task<IEnumerable<NotificationDto>> GetNotificationsAsync()
        {
            IEnumerable<Notification> notifications = await _notificationRepository.GetNotificationsAsync();

            return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }

        public async Task<IEnumerable<NotificationDto>> GetUnreadNotificationsAsync()
        {
            IEnumerable<Notification> notifications = await _notificationRepository.GetUnReadNotificationAsync();

            return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }

        public async Task<IEnumerable<NotificationDto>> GetUnreadNotificationsByEmployeeIdAsync(int employeeId)
        {
            await IsEmployeeExists(employeeId);

            IEnumerable<Notification> notifications = await _notificationRepository.GetUnReadNotificationByEmployeeIdAsync(employeeId);

            return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }

        public async Task MarkAllAsReadAsync(int employeeId)
        {
            await IsEmployeeExists(employeeId);

            await _notificationRepository.MarkAllAsReadAsync(employeeId);
        }

        public async Task MarkAllAsReadByAdminAsync()
        {
            await _notificationRepository.MarkAllAsReadByAdminAsync();
        }

        public async Task MarkNotificationAsReadAsync(int id)
        {
            Notification notification = await IsNotificationExists(id);

            if (!notification.IsRead)
            {
                notification.IsRead = true;
                notification.ReadAt = DateTime.UtcNow;

                await _notificationRepository.UpdateNotificationAsync(notification);
            }
        }

        public async Task<IEnumerable<NotificationDto>> GetUnreadNotificationsByAdminAsync()
        {
            IEnumerable<Notification> notifications = await _notificationRepository.GetUnreadNotificationsByAdminAsync();


            return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }



        #region Helper Methods
        private async Task<Employee> IsEmployeeExists(int employeeId) {

            Employee? employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
            {
                throw new KeyNotFoundException($"No employee found with id: {employeeId}");
            }

            return employee;
        }

        private async Task<Notification> IsNotificationExists(int id) {

            Notification? notification = await _notificationRepository.GetNotificationByIdAsync(id);

            if (notification == null)
            {
                throw new KeyNotFoundException($"No notification found with ID: {id}");
            }

            return notification;
        }

        

        #endregion
    }
}
