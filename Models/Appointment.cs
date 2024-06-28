using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace FamilyDoctor.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public string PatientId { get; set; } // ID-ul pacientului
        public IdentityUser Patient { get; set; }

        [Required]
        public string DoctorId { get; set; } // ID-ul doctorului
        public IdentityUser Doctor { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string Status { get; set; } // Pending, Accepted, Rejected
    }
}
