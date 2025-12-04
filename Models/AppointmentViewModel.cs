using System.Collections.Generic;

namespace PharmaClinic.Models
{
    public class AppointmentViewModel
    {
        public string DoctorName { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string IIN { get; set; } = string.Empty; // ЖСН

        public string? SelectedDate { get; set; }
        public string? SelectedTimeSlot { get; set; }

        public bool IsSubmitted { get; set; }

        // Таңдау үшін қолжетімді күндер мен уақыттар
        public List<string> AvailableDates { get; set; } = new();
        public List<string> AvailableTimeSlots { get; set; } = new();
    }
}
