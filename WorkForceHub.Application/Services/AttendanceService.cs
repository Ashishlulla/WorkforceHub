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

        public async Task<AttendanceDto> CreateAttendanceAsync(CreateAttendanceDto createAttendanceDto)
        {
            await _createAttendanceDtoValidator.ValidateAndThrowAsync(createAttendanceDto);

            var employee = await _employeeRepository.GetEmployeeByIdAsync(createAttendanceDto.EmployeeId);

            if (employee == null)
            {
                throw new ArgumentException($"No employee found with Id: {createAttendanceDto.EmployeeId}");
            }

            DateOnly currentDay = DateOnly.FromDateTime(DateTime.Today);

            if (createAttendanceDto.AttendanceDate !=  currentDay)
            {
                throw new KeyNotFoundException("Attendace can be only recorded for current date only.");
            }

       
            var isAttendanceExists = await _attendanceRepository.GetAttendanceByEmployeeIdAndDateAsync(createAttendanceDto.EmployeeId, currentDay);

            if (isAttendanceExists != null) {

                throw new ArgumentException("Attendance for employee already exists for today.");
            }

            var  newAttendance = _mapper.Map<Attendance>(createAttendanceDto);
            newAttendance.CreatedAt = DateTime.UtcNow;

            var attendance = await _attendanceRepository.AddAttendanceAsync(newAttendance);

            return _mapper.Map<AttendanceDto>(attendance);
        }

        public async Task DeleteAttendanceByIdAsync(int id)
        {
            var attendance = await _attendanceRepository.GetAttendanceByIdAsync(id);
            if (attendance == null)
            {
                throw new KeyNotFoundException($"No attendance exist with Id: {id}");
            }
            await _attendanceRepository.DeleteAttendanceByIdAsync(attendance);
        }

        public async Task<AttendanceDetailsDto> GetAttendanceDetailsByIdAsync(int id)
        {
            var attendance = await _attendanceRepository.GetAttendanceDetailsByIdAsync(id);
            if (attendance == null)
            {

                throw new KeyNotFoundException($"No attendance exists with Id: {id}");
            }

            return _mapper.Map<AttendanceDetailsDto>(attendance); 
        }

        public async Task<IEnumerable<AttendanceDto>> GetAttendancesAsync()
        {
            var attendances = await _attendanceRepository.GetAttendancesAsync();
            return _mapper.Map<List<AttendanceDto>>(attendances);
        }

        public async Task<AttendanceDto> GetAttendanceByIdAsync(int id)
        {
            var attendance = await _attendanceRepository.GetAttendanceByIdAsync(id);
            if (attendance == null) {

                throw new KeyNotFoundException($"No attendance exists with Id: {id}");
            }

            return _mapper.Map<AttendanceDto>(attendance);
        }

        public async Task<AttendanceDto> UpdateAttendanceAsync(int id, UpdateAttendanceDto updateAttendanceDto)
        {
            await  _updateAttendanceDtoValidator.ValidateAndThrowAsync(updateAttendanceDto);

            if (id != updateAttendanceDto.AttendanceId)
            {
                throw new ArgumentException("Route Id and and User passed Id don not match");
            }
            var existingAttendance = await _attendanceRepository.GetAttendanceByIdAsync(id);

            if (existingAttendance == null)
            {
                throw new KeyNotFoundException($"No Attendanc exists with Id: {id}");
            }
            var retrievedEmployee = await _employeeRepository.GetEmployeeByIdAsync(updateAttendanceDto.EmployeeId);

            if (retrievedEmployee == null)
            {
                throw new KeyNotFoundException($"No employee exists with Id: {updateAttendanceDto.EmployeeId}");
            }

            var attendance = _mapper.Map<Attendance>(existingAttendance);
            attendance.UpdatedAt = DateTime.Today;

            var 

        }
    }
}
