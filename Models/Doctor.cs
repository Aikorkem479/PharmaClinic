namespace PharmaClinic.Models
{
    public class Doctor
    {
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        public int ExperienceYears { get; set; }          // стаж (жыл)
        public string District { get; set; } = string.Empty; // қай жер / микрорайон
        public string ClinicName { get; set; } = string.Empty; // медцентр аты
        public string? MapUrl { get; set; }               // карта сілтеме (2ГИС, т.б.)

        /// <summary>
        /// "F" – әйел дәрігер, "M" – ер дәрігер.
        /// </summary>
        public string Gender { get; set; } = "F";
    }
}
