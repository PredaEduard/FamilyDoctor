using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace FamilyDoctor.Models
{
    public enum AppointmentStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Appointment Date")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Required]
        [Display(Name = "Status")]
        public AppointmentStatus? Status { get; set; } = AppointmentStatus.Pending;

        public string? PatientId { get; set; }
        public IdentityUser? Patient { get; set; }

        public string? DoctorId { get; set; }
        public IdentityUser? Doctor { get; set; } // Adăugăm proprietatea de navigare pentru Doctor
    }

}
