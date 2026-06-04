using AureaBeautyClinic.API.Requests;
using AureaBeautyClinic.Shared.Common;
using AureaBeautyClinic.Shared.Constants;
using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;
using AureaBeautyClinic.Shared.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AureaBeautyClinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentervice _Appointmentervice;

        public AppointmentController(IAppointmentervice Appointmentervice)
        {
            _Appointmentervice = Appointmentervice;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AppointmentDTO>>>> GetAll()
        {
            var Appointment = await _Appointmentervice.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<AppointmentDTO>>.Ok(Appointment));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<AppointmentDTO>>> GetById(int id)
        {
            var appointment = await _Appointmentervice.GetByIdAsync(id);
            if (appointment is null)
                return NotFound(ApiResponse<AppointmentDTO>.Fail($"Appointment with ID {id} was not found."));

            return Ok(ApiResponse<AppointmentDTO>.Ok(appointment));
        }

        [HttpGet("user/{UserId:int}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AppointmentDTO>>>> GetByUser(int UserId)
        {
            var Appointment = await _Appointmentervice.GetByUserIdAsync(UserId);
            return Ok(ApiResponse<IEnumerable<AppointmentDTO>>.Ok(Appointment));
        }

        [HttpGet("doctor/{DoctorId:int}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AppointmentDTO>>>> GetByDoctor(int DoctorId)
        {
            var Appointment = await _Appointmentervice.GetByDoctorIdAsync(DoctorId);
            return Ok(ApiResponse<IEnumerable<AppointmentDTO>>.Ok(Appointment));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<AppointmentDTO>>> Create([FromBody] CreateAppointmentRequest request)
        {
            var appointment = new Appointment
            {
                UserId = request.UserId,
                DoctorId = request.DoctorId,
                Scheduled = request.Scheduled,
                State = Appointmenttatus.Pending,
                Notes = request.Notes,
                Created = DateTime.UtcNow
            };
            var created = await _Appointmentervice.CreateAsync(appointment);

            return CreatedAtAction(nameof(GetById), new { id = created.AppointmentId },
                ApiResponse<AppointmentDTO>.Ok(created, "Appointment created successfully."));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<AppointmentDTO>>> Update(int id, [FromBody] UpdateAppointmentRequest request)
        {
            var existing = await _Appointmentervice.GetByIdAsync(id);
            if (existing is null)
                return NotFound(ApiResponse<AppointmentDTO>.Fail($"Appointment with ID {id} was not found."));

            var validStates = new[] { Appointmenttatus.Pending, Appointmenttatus.Confirmed, Appointmenttatus.Cancelled, Appointmenttatus.Completed };
            if (!validStates.Contains(request.State))
                return BadRequest(ApiResponse<AppointmentDTO>.Fail($"Invalid state. Valid values are: {string.Join(", ", validStates)}."));

            var appointment = new Appointment
            {
                AppointmentId = id,
                UserId = existing.UserId,
                DoctorId = existing.DoctorId,
                Scheduled = request.Scheduled,
                State = request.State,
                Notes = request.Notes,
                Created = existing.created
            };
            await _Appointmentervice.UpdateAsync(appointment);

            var updated = existing with { scheduled = request.Scheduled, state = request.State, notes = request.Notes };
            return Ok(ApiResponse<AppointmentDTO>.Ok(updated, "Appointment updated successfully."));
        }
    }
}
