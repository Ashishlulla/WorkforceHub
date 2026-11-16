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
            
            var employee = await _employeeRepository.GetEmployeeByIdAsync(createLeaveRequestDto.EmployeeId);

            if (employee == null)
            {
                throw new KeyNotFoundException($"No Employee found with ID: {createLeaveRequestDto.EmployeeId}");
            }

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

        public async Task<LeaveRequestDto?> GetLeaveRequestByIdAsync(int LeaveRequestId)
        {
            LeaveRequest? leaveRequest = await _leaveRequestRepository.GetLeaveRequestByIdAsync(LeaveRequestId);

            if (leaveRequest == null)
            {
                throw new KeyNotFoundException($"No Leave Request exists with ID: {LeaveRequestId}");
            }

            return _mapper.Map<LeaveRequestDto>(leaveRequest);
        }

        public async Task<LeaveRequestDetailsDto?> GetLeaveRequestDetailsByIdAsync(int LeaveRequestId)
        {
            LeaveRequest? leaveRequest = await _leaveRequestRepository.GetLeaveRequestDetailsByIdAsync(LeaveRequestId);

            if (leaveRequest == null)
            {
                throw new KeyNotFoundException($"No leave request found with ID: {LeaveRequestId}");
            }

            return _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);
        }

        public async Task<IEnumerable<LeaveRequestDto>> GetLeaveRequestsByEmployeeIdAsync(int employeeId)
        {
            Employee? employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

            if (employee == null) {

                throw new KeyNotFoundException($"No employee Found with ID: {employeeId}");
            }

            IEnumerable<LeaveRequest> leaveRequests = await _leaveRequestRepository.GetLeaveRequestByEmployeeIdAsync(employeeId);

            return _mapper.Map<IEnumerable<LeaveRequestDto>>(leaveRequests);
        }

        public async Task DeleteLeaveRequestAsync(int leaveRequestId)
        {
            LeaveRequest? leaveRequest = await _leaveRequestRepository.GetLeaveRequestByIdAsync(leaveRequestId);

            if (leaveRequest == null)
            {
                throw new KeyNotFoundException($"No Leave Request found with ID: {leaveRequestId}");
            }

            await _leaveRequestRepository.DeleteLeaveRequestAsync(leaveRequest);
        }

        public async Task<LeaveRequestDetailsDto> UpdateLeaveRequestStatusAsync(int leaveRequestId, UpdateLeaveRequestStatusDto updateLeaveRequestStatusDto)
        {
            await _updateLeaveRequestDtoValidator.ValidateAndThrowAsync(updateLeaveRequestStatusDto);

            LeaveRequest? leaveRequest = await _leaveRequestRepository.GetLeaveRequestByIdAsync(leaveRequestId);

            if (leaveRequest == null)
            {
                throw new KeyNotFoundException($"No leave request found with ID: {leaveRequestId}");
            }

            if (leaveRequest.Status != LeaveStatus.Pending)
            {
                throw new InvalidOperationException("Only pending leave requests can be processed.");
            }

            Employee? employee = await _employeeRepository.GetEmployeeByIdAsync(updateLeaveRequestStatusDto.ApprovedByEmployeeId);
            
            if (employee == null)
            {
                throw new KeyNotFoundException($"No approver employee found with ID: {updateLeaveRequestStatusDto.ApprovedByEmployeeId}");
            }

            _mapper.Map(updateLeaveRequestStatusDto, leaveRequest);

            leaveRequest.ApprovedByEmployee = employee;
            leaveRequest.ApprovedAt = DateTime.UtcNow;
            leaveRequest.UpdatedAt = DateTime.UtcNow;

            LeaveRequest updatedLeaveRequest = await _leaveRequestRepository.UpdateLeaveRequestAsync(leaveRequest);

            return _mapper.Map<LeaveRequestDetailsDto>(updatedLeaveRequest);
        }
    }
}
