using GreenSchoolCAT.Models;
using System.Collections.Generic;

namespace GreenSchoolCAT.Services
{
    public interface ICatService
    {
        Question GetNextQuestion(double theta, IEnumerable<Question> pool);
        double UpdateTheta(double currentTheta, Question question, bool correct);
    }

}
