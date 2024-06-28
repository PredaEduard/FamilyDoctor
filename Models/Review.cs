using System;

namespace FamilyDoctor.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
