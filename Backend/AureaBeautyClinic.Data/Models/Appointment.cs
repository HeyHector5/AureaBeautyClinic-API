using System;
using System.Collections.Generic;

namespace AureaBeautyClinic.Data.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int UserId { get; set; }

    public int DoctorId { get; set; }

    public DateTime Scheduled { get; set; }

    public string State { get; set; } = null!;

    public string? Notes { get; set; }

    public DateTime Created { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
