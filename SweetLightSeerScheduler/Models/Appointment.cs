namespace SweetLightSeerScheduler.Models
{
    public class Appointment
    {
        public string? EventId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Summary { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Comment { get; set; }
    }
}
