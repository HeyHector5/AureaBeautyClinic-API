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
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AppointmentDTO>>>> GetAll()
        {
            var appointments = await _appointmentService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<AppointmentDTO>>.Ok(appointments));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<AppointmentDTO>>> GetById(int id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            if (appointment is null)
                return NotFound(ApiResponse<AppointmentDTO>.Fail($"Appointment with ID {id} was not found."));

            return Ok(ApiResponse<AppointmentDTO>.Ok(appointment));
        }

        [HttpGet("user/{userId:int}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AppointmentDTO>>>> GetByUser(int userId)
        {
            var appointments = await _appointmentService.GetByUserIdAsync(userId);
            return Ok(ApiResponse<IEnumerable<AppointmentDTO>>.Ok(appointments));
        }

        [HttpGet("doctor/{doctorId:int}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AppointmentDTO>>>> GetByDoctor(int doctorId)
        {
            var appointments = await _appointmentService.GetByDoctorIdAsync(doctorId);
            return Ok(ApiResponse<IEnumerable<AppointmentDTO>>.Ok(appointments));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<AppointmentDTO>>> Create([FromBody] CreateAppointmentRequest request)
        {
            var appointment = new Appointments
            {
                UserID = request.UserID,
                DoctorID = request.DoctorID,
                Scheduled = request.Scheduled,
                State = AppointmentStatus.Pending,
                Notes = request.Notes,
                Created = DateTime.UtcNow
            };
            var created = await _appointmentService.CreateAsync(appointment);

            return CreatedAtAction(nameof(GetById), new { id = created.appointmentID },
                ApiResponse<AppointmentDTO>.Ok(created, "Appointment created successfully."));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<AppointmentDTO>>> Update(int id, [FromBody] UpdateAppointmentRequest request)
        {
            var existing = await _appointmentService.GetByIdAsync(id);
            if (existing is null)
                return NotFound(ApiResponse<AppointmentDTO>.Fail($"Appointment with ID {id} was not found."));

            var validStates = new[] { AppointmentStatus.Pending, AppointmentStatus.Confirmed, AppointmentStatus.Cancelled, AppointmentStatus.Completed };
            if (!validStates.Contains(request.State))
                return BadRequest(ApiResponse<AppointmentDTO>.Fail($"Invalid state. Valid values are: {string.Join(", ", validStates)}."));

            var appointment = new Appointments
            {
                AppointmentID = id,
                UserID = existing.userID,
                DoctorID = existing.doctorID,
                Scheduled = request.Scheduled,
                State = request.State,
                Notes = request.Notes,
                Created = existing.created
            };
            await _appointmentService.UpdateAsync(appointment);

            var updated = existing with { scheduled = request.Scheduled, state = request.State, notes = request.Notes };
            return Ok(ApiResponse<AppointmentDTO>.Ok(updated, "Appointment updated successfully."));
        }
    }
}
