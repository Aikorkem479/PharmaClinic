namespace PharmaClinic.Models
{
    public class ContactViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        public bool IsSent { get; set; }
    }
}
