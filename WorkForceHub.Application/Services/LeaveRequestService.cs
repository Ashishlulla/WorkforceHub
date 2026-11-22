using AutoMapper;
using FluentValidation;
using WorkForceHub.Application.DTOs.LeaveRequest;
using WorkForceHub.Application.Interfaces.RepositoryInterfaces;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;
using WorkForceHub.Domain.Entities;
using WorkForceHub.Domain.Enums;


namespace WorkForceHub.Application.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        // Private Fields
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateLeaveRequestDto> _createLeaveRequestDtoValidator;
        private readonly IValidator<UpdateLeaveRequestStatusDto> _updateLeaveRequestDtoValidator;

        //ConstructorIValidator<CreateLeaveRequestDto> _createLeaveRequestDtValidator
        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, 
            IEmployeeRepository employeeRepository, 
            IMapper mapper, 
            IValidator<CreateLeaveRequestDto> createLeaveRequestDtoValidator,
            IValidator<UpdateLeaveRequestStatusDto> updateLeaveRequestDtoValidator) 
        {
            _leaveRequestRepository = leaveRequestRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _createLeaveRequestDtoValidator = createLeaveRequestDtoValidator;
            _updateLeaveRequestDtoValidator = updateLeaveRequestDtoValidator;
        }

        public async Task<LeaveRequestDto> CreateLeaveRequestAsync(CreateLeaveRequestDto createLeaveRequestDto)
        {
            await _createLeaveRequestDtoValidator.ValidateAndThrowAsync(createLeaveRequestDto);

            var employee = await EnsureEmployeeExistsAsync(createLeaveRequestDto.EmployeeId);

            LeaveRequest leaveRequest = _mapper.Map<LeaveRequest>(createLeaveRequestDto);
            leaveRequest.CreatedAt = DateTime.UtcNow;
            leaveRequest.Employee = employee;

            LeaveRequest addedLeaveRequest = await _leaveRequestRepository.AddLeaveRequestAsync(leaveRequest);

            return _mapper.Map<LeaveRequestDto>(addedLeaveRequest);
        }

        public async Task<IEnumerable<LeaveRequestDto>> GetLeaveRequestsAsync()
        {
            IEnumerable<LeaveRequest> leaveRequests = await _leaveRequestRepository.GetLeaveRequestsAsync();
            return _mapper.Map<IEnumerable<LeaveRequestDto>>(leaveRequests);
        }

        public async Task<LeaveRequestDto> GetLeaveRequestByIdAsync(int LeaveRequestId)
        {
            LeaveRequest? leaveRequest = await EnsureLeaveRequestExistsAsync(LeaveRequestId);

            return _mapper.Map<LeaveRequestDto>(leaveRequest);
        }

        public async Task<LeaveRequestDetailsDto> GetLeaveRequestDetailsByIdAsync(int leaveRequestId)
        {
            LeaveRequest? leaveRequest = await _leaveRequestRepository.GetLeaveRequestDetailsByIdAsync(leaveRequestId);

            if (leaveRequest == null)
            {
                throw new KeyNotFoundException($"No leave request found with ID: {leaveRequestId}");
            }

            return _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);
        }

        public async Task<IEnumerable<LeaveRequestDto>> GetLeaveRequestsByEmployeeIdAsync(int employeeId)
        {
            await EnsureEmployeeExistsAsync(employeeId);

            IEnumerable<LeaveRequest> leaveRequests = await _leaveRequestRepository.GetLeaveRequestByEmployeeIdAsync(employeeId);

            return _mapper.Map<IEnumerable<LeaveRequestDto>>(leaveRequests);
        }

        public async Task DeleteLeaveRequestAsync(int leaveRequestId)
        {
            LeaveRequest? leaveRequest = await EnsureLeaveRequestExistsAsync(leaveRequestId);

            await _leaveRequestRepository.DeleteLeaveRequestAsync(leaveRequest);
        }

        public async Task<LeaveRequestDetailsDto> UpdateLeaveRequestStatusAsync(int leaveRequestId, UpdateLeaveRequestStatusDto updateLeaveRequestStatusDto)
        {
 
            await _updateLeaveRequestDtoValidator.ValidateAndThrowAsync(updateLeaveRequestStatusDto);

            LeaveRequest? leaveRequest = await EnsureLeaveRequestExistsAsync(leaveRequestId);

            if (leaveRequest.Status != LeaveStatus.Pending)
            {
                throw new InvalidOperationException("Only pending leave requests can be processed.");
            }

            Employee? employee = await EnsureEmployeeExistsAsync(updateLeaveRequestStatusDto.ApprovedByEmployeeId);

            _mapper.Map(updateLeaveRequestStatusDto, leaveRequest);

            leaveRequest.ApprovedByEmployee = employee;
            leaveRequest.ApprovedAt = DateTime.UtcNow;
            leaveRequest.UpdatedAt = DateTime.UtcNow;

            LeaveRequest updatedLeaveRequest = await _leaveRequestRepository.UpdateLeaveRequestAsync(leaveRequest);

            return _mapper.Map<LeaveRequestDetailsDto>(updatedLeaveRequest);
        }


        #region Helper Methods
        private async Task<Employee> EnsureEmployeeExistsAsync(int employeeId) {

            Employee? employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
            {
                throw new KeyNotFoundException($"No employee found with ID: {employeeId}");
            }

            return employee;
        }

        private async Task<LeaveRequest> EnsureLeaveRequestExistsAsync(int leaveRequestId) {

            LeaveRequest? leaveRequest = await _leaveRequestRepository.GetLeaveRequestByIdAsync(leaveRequestId);

            if (leaveRequest == null)
            {
                throw new KeyNotFoundException($"no leave request found with ID: {leaveRequestId}");
            }

            return leaveRequest;

        }
        #endregion
    }
}
