using AutoMapper;
using FluentValidation;
using WorkForceHub.Application.DTOs.Department;
using WorkForceHub.Application.Interfaces.RepositoryInterfaces;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;
using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        //Private Fields
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        private readonly IValidator<CreateDepartmentDto> _createDepartmentDtoValidator;
        private readonly IValidator<UpdateDepartmentDto> _updateDepartmentDtoValidator;

        //Constructor
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, IValidator<CreateDepartmentDto> createDepartmentDtoValidator, IValidator<UpdateDepartmentDto> updateDepartmentDtoValidator) {

            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _createDepartmentDtoValidator = createDepartmentDtoValidator;
            _updateDepartmentDtoValidator = updateDepartmentDtoValidator;
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto)
        {
            await _createDepartmentDtoValidator.ValidateAndThrowAsync(createDepartmentDto);

            var department = _mapper.Map<Department>(createDepartmentDto);
            
            department.CreatedAt = DateTime.UtcNow;
            
            var result = await _departmentRepository.AddDepartmentAsync(department);

            return _mapper.Map<DepartmentDto>(result);
            
        }

        public async Task DeleteDepartmentByIdAsync(int id)
        {

             Department department = await EnsureDepartmentExistsAsync(id);

            await _departmentRepository.DeleteDepartmentAsync(department);

        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(int id)
        {
            var department = await EnsureDepartmentExistsAsync(id);

            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetDepartmentsAsync();

            return _mapper.Map<List<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto> UpdateDepartmentAsync(int id , UpdateDepartmentDto updateDepartmentDto)
        {
            if (id != updateDepartmentDto.DepartmentId)
            {
                throw new ArgumentException("Route ID and Department ID do not match.");
            }

            await _updateDepartmentDtoValidator.ValidateAndThrowAsync(updateDepartmentDto);

            var retrievedDepartment = await EnsureDepartmentExistsAsync(updateDepartmentDto.DepartmentId);

            var department = _mapper.Map(updateDepartmentDto, retrievedDepartment);
            
            department.UpdatedAt = DateTime.UtcNow;

            var updatedDepartment = await _departmentRepository.UpdateDepartmentAsync(department);
            
            return _mapper.Map<DepartmentDto>(updatedDepartment);
        }

        #region HelperMethod
        private async Task<Department> EnsureDepartmentExistsAsync(int id) {

            Department? department = await _departmentRepository.GetDepartmentByIdAsync(id);

            if (department == null)
            {
                throw new KeyNotFoundException($"No department exists with ID: {id}");
            }

            return department;
        }

        #endregion
    }
}
