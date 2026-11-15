using AutoMapper;
using FluentValidation;
using WorkForceHub.Application.DTOs.Shift;
using WorkForceHub.Application.Interfaces.RepositoryInterfaces;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;
using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.Services
{
    public class ShiftService : IShiftService
    {
        //Private Fields
        private readonly IShiftRepository _shiftRepository;
 
        private readonly IMapper _mapper;

        private readonly IValidator<CreateShiftDto> _createShiftDtoValidator;
        private readonly IValidator<UpdateShiftDto> _updateShiftDtoValidator;

        //Constructor
        public ShiftService(IShiftRepository shiftRepository,
            IMapper mapper,
            IValidator<CreateShiftDto> createShiftDtoValidator,
            IValidator<UpdateShiftDto> updateShiftDtoValidator)
        {
            
            _shiftRepository = shiftRepository;
            _mapper = mapper;
            _createShiftDtoValidator = createShiftDtoValidator;
            _updateShiftDtoValidator = updateShiftDtoValidator;
        }

        public async Task<ShiftDto> CreateShiftAsync(CreateShiftDto createShiftDto)
        {
            await _createShiftDtoValidator.ValidateAndThrowAsync(createShiftDto);

            Shift shift = _mapper.Map<Shift>(createShiftDto);
            shift.CreatedAt = DateTime.UtcNow;

            Shift addedShift = await _shiftRepository.AddShiftAsync(shift);

            return _mapper.Map<ShiftDto>(addedShift);
        }

        public async Task DeleteShiftAsync(int id)
        {
            var shift = await _shiftRepository.GetShiftDetailsByIdAsync(id);
            if (shift == null) {

                throw new KeyNotFoundException($"No Shift found with Id: {id}");
            }

            if (shift.Employees.Any()) {

                throw new InvalidOperationException("Cannot delete shift because employees are assigned to it.");
            }

            await _shiftRepository.DeleteShiftAsync(shift);
        }

        public async Task<ShiftDto> GetShiftByIdAsync(int id)
        {
            var shift = await _shiftRepository.GetShiftByIdAsync(id);
            
            if (shift == null)
            {
                throw new KeyNotFoundException($"No Shift found with Id: {id}");
            }

            return _mapper.Map<ShiftDto>(shift);
        }

        public async Task<ShiftDetailsDto> GetShiftDetailsByIdAsync(int id)
        {
            var shiftDetails = await _shiftRepository.GetShiftDetailsByIdAsync(id);
            if (shiftDetails == null)
            {

                throw new KeyNotFoundException($"No Shift found with Id: {id}");
            }

            return _mapper.Map<ShiftDetailsDto>(shiftDetails);
        }

        public async Task<IEnumerable<ShiftDto>> GetShiftsAsync()
        {
            var shifts = await _shiftRepository.GetShiftsAsync();

            return _mapper.Map<List<ShiftDto>>(shifts);
        }

        public async Task<ShiftDto> UpdateShiftAsync(int id, UpdateShiftDto updateShiftDto)
        {
            await _updateShiftDtoValidator.ValidateAndThrowAsync(updateShiftDto);

            if (id != updateShiftDto.ShiftId)
            {
                throw new ArgumentException("Route ID and user-provided Id do not match.");
            }

            Shift? shift = await _shiftRepository.GetShiftByIdAsync(id);

            if (shift == null)
            {
                throw new KeyNotFoundException($"No shift found with id : {updateShiftDto.ShiftId} ");
            }

            shift.UpdatedAt = DateTime.UtcNow;

            var shiftTobeUpdated = _mapper.Map(updateShiftDto, shift);

            var updatedShift = await _shiftRepository.UpdateShiftAsync(shiftTobeUpdated);

            return _mapper.Map<ShiftDto>(updatedShift);

        }
    }
}
