using AureaBeautyClinic.API.Requests;
using AureaBeautyClinic.Shared.Common;
using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;
using AureaBeautyClinic.Shared.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AureaBeautyClinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtyService _specialtyService;

        public SpecialtiesController(ISpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<SpecialtyDTO>>>> GetAll()
        {
            var specialties = await _specialtyService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<SpecialtyDTO>>.Ok(specialties));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<SpecialtyDTO>>> GetById(int id)
        {
            var specialty = await _specialtyService.GetByIdAsync(id);
            if (specialty is null)
                return NotFound(ApiResponse<SpecialtyDTO>.Fail($"Specialty with ID {id} was not found."));

            return Ok(ApiResponse<SpecialtyDTO>.Ok(specialty));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<SpecialtyDTO>>> Create([FromBody] CreateSpecialtyRequest request)
        {
            var specialty = new Specialties
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive
            };
            var created = await _specialtyService.CreateAsync(specialty);

            return CreatedAtAction(nameof(GetById), new { id = created.specialtyID },
                ApiResponse<SpecialtyDTO>.Ok(created, "Specialty created successfully."));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<SpecialtyDTO>>> Update(int id, [FromBody] UpdateSpecialtyRequest request)
        {
            var existing = await _specialtyService.GetByIdAsync(id);
            if (existing is null)
                return NotFound(ApiResponse<SpecialtyDTO>.Fail($"Specialty with ID {id} was not found."));

            var specialty = new Specialties
            {
                SpecialtyID = id,
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive
            };
            await _specialtyService.UpdateAsync(specialty);

            var updated = existing with { name = request.Name, description = request.Description, isActive = request.IsActive };
            return Ok(ApiResponse<SpecialtyDTO>.Ok(updated, "Specialty updated successfully."));
        }
    }
}
