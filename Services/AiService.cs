using System.Threading.Tasks;

namespace PharmaClinic.Services
{
    public class AiService
    {
        public Task<string> ExplainAnalysisAsync(string testType, string userQuestion)
        {
            if (string.IsNullOrWhiteSpace(testType))
                testType = "Талдау түрі көрсетілмеген";

            if (string.IsNullOrWhiteSpace(userQuestion))
                userQuestion = "Қосымша сұрақ жазылмады.";

            var text =
                "‼ МАҢЫЗДЫ ЕСКЕРТУ ‼\n" +
                "Егер кеуде тұсыңыз қатты ауырып, дем жетпей қалса, бет-ерін, тіл, тамақ ісінсе – " +
                "сайтқа емес, бірден 103-ке қоңырау шалу керек.\n\n" +
                $"Талдау түрі: {testType}\n" +
                $"Сіздің сұрағыңыз: {userQuestion}\n\n" +
                "Бұл онлайн түсіндірме нақты диагнозды алмастырмайды. " +
                "Толық қорытынды үшін тірі дәрігерге қаралу қажет.";

            return Task.FromResult(text);
        }
    }
}
