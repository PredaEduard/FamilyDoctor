using System;

namespace FamilyDoctor.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public bool Seen { get; set; }
        public DateTime Date { get; set; }
    }
}
