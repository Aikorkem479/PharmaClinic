using Microsoft.AspNetCore.Mvc;
using PharmaClinic.Models;
using PharmaClinic.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmaClinic.Controllers
{
    public class HomeController : Controller
    {
        private readonly AiService _aiService;

        public HomeController(AiService aiService)
        {
            _aiService = aiService;
        }

        // ===== Басты бет (KZ) =====
        public IActionResult Index()
        {
            return View();
        }

        // ===== Басты бет (RU) =====
        [HttpGet]
        public IActionResult IndexRu()
        {
            return View("IndexRu");
        }

        // ===== Дәрігерлер тізімі (GET) =====
        [HttpGet]
        public IActionResult Doctors()
        {
            var doctors = GetDoctors();
            return View(doctors);
        }

        // ----- Дәрігерлер деректері -----
        private List<Doctor> GetDoctors()
        {
            return new List<Doctor>
            {
                new Doctor
                {
                    Name = "Карасаева Махпал Шахиевна",
                    Specialty = "Кардиолог",
                    Description = "Жүрек-қан тамыр ауруларын заманауи әдістермен тексеріп, емдейді.",
                    ImageUrl = "/images/cardiologist.jpeg",
                    ExperienceYears = 23,
                    District = "7-й микрорайон, 35а",
                    ClinicName = "Медицинский центр \"Ча-Кур\"",
                    MapUrl = "https://2gis.kz/aktau/geo/70000001028483978/51.159567,43.644009"
                },
                new Doctor
                {
                    Name = "Миронова Нина Александровна",
                    Specialty = "Аллерголог",
                    Description = "Аллергиялық ауруларды анықтап, жеке емдеу жоспарын құрастырады.",
                    ImageUrl = "/images/allergist.png",
                    ExperienceYears = 51,
                    District = "8-й микрорайон, 38",
                    ClinicName = "Медицинский центр доктора Пасенова",
                    MapUrl = "https://2gis.kz/aktau/geo/70000001028598561"
                }
            };
        }

        // ===== Пікір жіберу (POST) – дәрігерлер беті =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DoctorsReview(string patientName, string doctorName, string comment)
        {
            if (string.IsNullOrWhiteSpace(patientName) ||
                string.IsNullOrWhiteSpace(doctorName) ||
                string.IsNullOrWhiteSpace(comment))
            {
                ViewBag.ReviewError = "Барлық өрістерді толтырыңыз.";
            }
            else
            {
                ViewBag.ReviewSent = true;
                ViewBag.ReviewPatient = patientName;
                ViewBag.ReviewDoctor = doctorName;
                ViewBag.ReviewText = comment;

                var trimmed = patientName.Trim();
                var initial = trimmed.Length > 0 ? trimmed[0].ToString().ToUpper() : "?";
                ViewBag.ReviewInitial = initial;
                ViewBag.ReviewMeta = $"{doctorName} туралы пікір";
            }

            var doctors = GetDoctors();
            return View("Doctors", doctors);
        }

        // ===== ЖАЗЫЛУ (GET) =====
        [HttpGet]
        public IActionResult Book(string doctorName)
        {
            var model = new AppointmentViewModel
            {
                DoctorName = doctorName,
                AvailableDates = GetFakeDates(),
                AvailableTimeSlots = GetFakeTimeSlots(doctorName)
            };

            return View(model);
        }

        // ===== ЖАЗЫЛУ (POST) =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Book(AppointmentViewModel model)
        {
            model.AvailableDates = GetFakeDates();
            model.AvailableTimeSlots = GetFakeTimeSlots(model.DoctorName);

            if (string.IsNullOrWhiteSpace(model.PatientName) ||
                string.IsNullOrWhiteSpace(model.IIN) ||
                string.IsNullOrWhiteSpace(model.SelectedDate) ||
                string.IsNullOrWhiteSpace(model.SelectedTimeSlot))
            {
                ModelState.AddModelError(string.Empty, "Барлық өрістерді толтырыңыз.");
            }
            else if (model.IIN.Length != 12)
            {
                ModelState.AddModelError(nameof(model.IIN), "ЖСН 12 саннан тұруы керек.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.IsSubmitted = true;
            return View(model);
        }

        // Демонстрациялық күндер
        private List<string> GetFakeDates()
        {
            return new List<string>
            {
                "20 қараша (бейсенбі)",
                "21 қараша (жұма)",
                "22 қараша (сенбі)"
            };
        }

        // Дәрігерге қарай бос уақыттар
        private List<string> GetFakeTimeSlots(string doctorName)
        {
            if (!string.IsNullOrEmpty(doctorName) &&
                doctorName.Contains("Карасаева"))
            {
                return new List<string> { "10:00", "10:30", "11:00" };
            }

            return new List<string> { "15:00", "15:30", "16:00" };
        }

        // ===== Байланыс (GET) =====
        [HttpGet]
        public IActionResult Contact()
        {
            var model = new ContactViewModel();
            return View(model);
        }

        // ===== Байланыс (POST) =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name) ||
                string.IsNullOrWhiteSpace(model.Phone) ||
                string.IsNullOrWhiteSpace(model.Message))
            {
                ModelState.AddModelError(string.Empty, "Барлық өрістерді толтырыңыз.");
                return View(model);
            }

            model.IsSent = true;
            return View(model);
        }

        // ===== Анализге дайындық (чек-лист) =====
        [HttpGet]
        public IActionResult Preparation()
        {
            return View(); // Views/Home/Preparation.cshtml
        }

        // ===== Анализді түсіндіру (GET) =====
        [HttpGet]
        public IActionResult ExplainAnalysis()
        {
            var model = new ExplainAnalysisViewModel();
            return View(model); // Views/Home/ExplainAnalysis.cshtml
        }

        // ===== Анализді түсіндіру (POST) =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExplainAnalysis(ExplainAnalysisViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.SelectedTestType))
            {
                ModelState.AddModelError(nameof(model.SelectedTestType),
                    "Талдау түрін таңдаңыз.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Фотоны қазір тек қабылдаймыз, сақтамасақ та болады
            model.AiAnswer = await _aiService.ExplainAnalysisAsync(
                model.SelectedTestType!,
                model.UserQuestion ?? string.Empty
            );

            return View(model);
        }
    }
}
