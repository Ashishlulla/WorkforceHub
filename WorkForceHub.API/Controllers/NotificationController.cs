using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkForceHub.Application.DTOs.Notifications;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;

namespace WorkForceHub.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        // Private Fields
        private readonly INotificationService _notificationService;

        //Constructor
        public NotificationController(INotificationService notificationService) {

            _notificationService = notificationService;
        }



        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationDto createNotificationDto)
        {
            NotificationDto notification =
                await _notificationService.CreateNotificationAsync(createNotificationDto);

            return CreatedAtRoute(
                "GetNotificationById",
                new { id = notification.NotificationId },
                notification);
        }

        //[Authorize(Roles ="Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetNotificationsAsync() {

            IEnumerable<NotificationDto> notifications = await _notificationService.GetNotificationsAsync();

            return Ok(notifications);
        }


        //[Authorize(Roles = "Admin,Employee")]
        [HttpGet("id/{id:int}", Name = "GetNotificationById")]
        public async Task<IActionResult> GetNotificationByIdAsync(int id) {

            NotificationDto notification = await _notificationService.GetNotificationByIdAsync(id);

            return Ok(notification);
        }

        //[Authorize(Roles = "Admin,Employee")]
        [HttpGet("employeeId/{employeeId:int}")]
        public async Task<IActionResult> GetNotificationByEmployeeIdAsync(int employeeId)
        {
            IEnumerable<NotificationDto> notification = await _notificationService.GetNotificationsByEmployeeIdAsync(employeeId);

            return Ok(notification);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("unread")]
        public async Task<IActionResult> GetUnreadNotificationsAsync() {

            IEnumerable<NotificationDto> unreadNotifications = await _notificationService.GetUnreadNotificationsAsync();

            return Ok(unreadNotifications);
        }

        //[Authorize(Roles = "Admin,Employee")]
        [HttpGet("unread/employee/{employeeId:int}")]
        public async Task<IActionResult> GetUnreadNotificationByEmployeeId(int employeeId) {

            IEnumerable<NotificationDto> employeeUnreadNotifications = await _notificationService.GetUnreadNotificationsByEmployeeIdAsync(employeeId);

            return Ok(employeeUnreadNotifications);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("unread/admin")]
        public async Task<IActionResult> GetUnreadAdminNotificationsAsync() {

            IEnumerable<NotificationDto> unreadAdminNotifications = await _notificationService.GetUnreadNotificationsByAdminAsync();

            return Ok(unreadAdminNotifications);
        }

        //[Authorize(Roles = "Employee")]
        [HttpPut("{id:int}/read")]
        public async Task<IActionResult> MarkNotificationAsReadAsync(int id) {

             await _notificationService.MarkNotificationAsReadAsync(id);

            return NoContent();
        }

        //[Authorize(Roles = "Employee")]
        [HttpPut("employee/{employeeId:int}/read-all")]
        public async Task<IActionResult> MarkAllAsReadByEmployeeIdAsync(int employeeId) {

            await _notificationService.MarkAllAsReadAsync(employeeId);

            return NoContent();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("read-all")]
        public async Task<IActionResult> MarkAllAsReadByAdminAsync()
        {

            await _notificationService.MarkAllAsReadByAdminAsync();

            return NoContent();
        }

        //[Authorize(Roles ="Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteNotificationAsync(int id) {

            await _notificationService.DeleteNotificationAsync(id);

            return NoContent();
        }
    }
}
