using System;
using System.Collections.Generic;

namespace AureaBeautyClinic.Data.Models;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public int UserId { get; set; }

    public int SpecialtyId { get; set; }

    public string? LicenseNumber { get; set; }

    public string? Biography { get; set; }

    public string? PhotoUrl { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Specialty Specialty { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
