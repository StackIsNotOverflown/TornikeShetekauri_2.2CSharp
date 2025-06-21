

using GreenSchoolCAT.Models;


namespace GreenSchoolCAT.Services
{
    public class CatService : ICatService
    {
        public Question GetNextQuestion(double theta, IEnumerable<Question> pool)
        {
            return pool
                .OrderBy(q => Math.Abs((q.Difficulty ?? 0.0) - theta))
                .FirstOrDefault();
        }

        public double UpdateTheta(double currentTheta, Question question, bool correct)
        {
            var a = question.Discrimination ?? 1.0;
            var b = question.Difficulty ?? 0.0;
            var c = question.Guessing ?? 0.25;

            double expTerm = Math.Exp(-a * (currentTheta - b));
            double p = c + (1 - c) / (1 + expTerm);
            double delta = (correct ? 1 : 0) - p;

            return currentTheta + 0.5 * delta;
        }
    }
}
