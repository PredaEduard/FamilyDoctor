using System.ComponentModel.DataAnnotations;

namespace FamilyDoctor.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Alte proprietăți specifice doctorului pot fi adăugate aici

        // Relația cu programările
        public ICollection<Appointment> Appointments { get; set; }
    }
}