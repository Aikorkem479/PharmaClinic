using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PharmaClinic.Models
{
    public class ExplainAnalysisViewModel
    {
        [Display(Name = "Талдау түрі")]
        [Required(ErrorMessage = "Талдау түрін таңдаңыз.")]
        public string? SelectedTestType { get; set; }

        [Display(Name = "Анализ нәтижесі немесе сұрағыңыз")]
        public string? UserQuestion { get; set; }

        [Display(Name = "Анализ қағазының фотосын тіркеу (қалауыңызша)")]
        public IFormFile? AnalysisPhoto { get; set; }

        // ЖИ жауабы
        public string? AiAnswer { get; set; }
    }
}
