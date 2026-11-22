using AutoMapper;
using FluentValidation;
using WorkForceHub.Application.DTOs.Attendance;
using WorkForceHub.Application.Interfaces.RepositoryInterfaces;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;
using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        //Private Fields 
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateAttendanceDto> _createAttendanceDtoValidator;
        private readonly IValidator<UpdateAttendanceDto> _updateAttendanceDtoValidator;

        //Constructor
        public AttendanceService(IEmployeeRepository employeeRepository, IAttendanceRepository attendanceRepository, IMapper mapper, IValidator<CreateAttendanceDto> createAttendanceDtoValidator, IValidator<UpdateAttendanceDto> updateAttendanceDtoValidator) {

            _employeeRepository = employeeRepository;
            _attendanceRepository = attendanceRepository;

            _mapper = mapper;

            _createAttendanceDtoValidator = createAttendanceDtoValidator;
            _updateAttendanceDtoValidator = updateAttendanceDtoValidator;
        }


        public async Task<AttendanceDto> CreateAttendanceAsync(
            CreateAttendanceDto createAttendanceDto)
        {
            
            await _createAttendanceDtoValidator.ValidateAndThrowAsync(
                createAttendanceDto);


            await EnsureEmployeeExistsAsync(createAttendanceDto.EmployeeId);


            EnsureAttendanceDateIsToday(createAttendanceDto.AttendanceDate);

            // 4. Check duplicate attendance
            await EnsureAttendanceDoesNotExistAsync(createAttendanceDto.EmployeeId, createAttendanceDto.AttendanceDate);

            
            var newAttendance = _mapper.Map<Attendance>(createAttendanceDto);

            
            newAttendance.CreatedAt = DateTime.UtcNow;

            
            var attendance = await _attendanceRepository.AddAttendanceAsync(
                newAttendance);

            
            return _mapper.Map<AttendanceDto>(attendance);
        }

        public async Task DeleteAttendanceByIdAsync(int id)
        {
            var attendance = await EnsureAttendanceExistsAsync(id);

            await _attendanceRepository.DeleteAttendanceByIdAsync(attendance);
        }

        public async Task<AttendanceDetailsDto> GetAttendanceDetailsByIdAsync(int id)
        {
            var attendance = await EnsureAttendanceExistsAsync(id);

            return _mapper.Map<AttendanceDetailsDto>(attendance); 
        }

        public async Task<IEnumerable<AttendanceDto>> GetAttendancesAsync()
        {
            var attendances = await _attendanceRepository.GetAttendancesAsync();
            return _mapper.Map<List<AttendanceDto>>(attendances);
        }

        public async Task<AttendanceDto> GetAttendanceByIdAsync(int id)
        {
            var attendance = await EnsureAttendanceExistsAsync(id);

            return _mapper.Map<AttendanceDto>(attendance);
        }

        public async Task<AttendanceDto> UpdateAttendanceAsync(int id, UpdateAttendanceDto updateAttendanceDto)
        {
            //Route Id and Dto Id check 
            if (id != updateAttendanceDto.AttendanceId)
            {
                throw new ArgumentException("Route ID and Attendance ID do not match.");
            }

            //Dto Validation  check
            await  _updateAttendanceDtoValidator.ValidateAndThrowAsync(updateAttendanceDto);


            //Retrieve the existing attendance
            var retrievedAttendance = await EnsureAttendanceExistsAsync(updateAttendanceDto.AttendanceId);

            //check employee exists
            await EnsureEmployeeExistsAsync(updateAttendanceDto.EmployeeId);

            //Duplicated Attendance check
            await EnsureAttendanceDoesNotExistAsync(updateAttendanceDto.EmployeeId, updateAttendanceDto.AttendanceDate, retrievedAttendance.AttendanceId);

            var attendance = _mapper.Map(updateAttendanceDto, retrievedAttendance);
            
            attendance.UpdatedAt = DateTime.UtcNow;

            var updatedAttendance = await _attendanceRepository.UpdateAttendanceAsync(attendance);

            return _mapper.Map<AttendanceDto>(updatedAttendance);
        }


        #region HelperMethods
        private async Task<Employee> EnsureEmployeeExistsAsync(int employeeId) {

            Employee? employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
            {
                throw new KeyNotFoundException($"No employee found with ID: {employeeId}");
            }

            return employee;
        }

        private async Task<Attendance> EnsureAttendanceExistsAsync(int id) {

            Attendance? attendance = await _attendanceRepository.GetAttendanceByIdAsync(id);

            if (attendance== null) {

                throw new KeyNotFoundException($"No attendance found with ID: {id}");
            }

            return attendance;
        }

        private async Task EnsureAttendanceDoesNotExistAsync(int employeeId, DateOnly attendanceDate, int? excludedAttendanceId=null) 
        {
            Attendance? existingAttendance = await _attendanceRepository.GetAttendanceByEmployeeIdAndDateAsync(employeeId, attendanceDate);

            if (existingAttendance != null && existingAttendance.AttendanceId != excludedAttendanceId)
            {
                throw new ArgumentException($"Attendance already exists for employee Id: {employeeId} on {attendanceDate}.");

            }
        }

        private void EnsureAttendanceDateIsToday(DateOnly attendanceDate) {

            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            if (attendanceDate != today)
            {
                throw new ArgumentException(
                    "Attendance can only be recorded for the current date.");
            }
        }

        #endregion
    }
}
