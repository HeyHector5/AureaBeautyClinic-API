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
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<DoctorDTO>>>> GetAll()
        {
            var doctors = await _doctorService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<DoctorDTO>>.Ok(doctors));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<DoctorDTO>>> GetById(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor is null)
                return NotFound(ApiResponse<DoctorDTO>.Fail($"Doctor with ID {id} was not found."));

            return Ok(ApiResponse<DoctorDTO>.Ok(doctor));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<DoctorDTO>>> Create([FromBody] CreateDoctorRequest request)
        {
            var doctor = new Doctor
            {
                UserId = request.UserId,
                SpecialtyId = request.SpecialtyId,
                LicenseNumber = request.LicenseNumber,
                Biography = request.Biography,
                PhotoURL = request.PhotoURL,
                IsActive = request.IsActive
            };
            var created = await _doctorService.CreateAsync(doctor);

            return CreatedAtAction(nameof(GetById), new { id = created.DoctorId },
                ApiResponse<DoctorDTO>.Ok(created, "Doctor created successfully."));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<DoctorDTO>>> Update(int id, [FromBody] UpdateDoctorRequest request)
        {
            var existing = await _doctorService.GetByIdAsync(id);
            if (existing is null)
                return NotFound(ApiResponse<DoctorDTO>.Fail($"Doctor with ID {id} was not found."));

            var doctor = new Doctor
            {
                DoctorId = id,
                UserId = existing.UserId,
                SpecialtyId = request.SpecialtyId,
                LicenseNumber = request.LicenseNumber,
                Biography = request.Biography,
                PhotoURL = request.PhotoURL,
                IsActive = request.IsActive
            };
            await _doctorService.UpdateAsync(doctor);

            var updated = existing with
            {
                SpecialtyId = request.SpecialtyId,
                licenseNumber = request.LicenseNumber,
                biography = request.Biography,
                photoURL = request.PhotoURL,
                isActive = request.IsActive
            };
            return Ok(ApiResponse<DoctorDTO>.Ok(updated, "Doctor updated successfully."));
        }
    }
}
