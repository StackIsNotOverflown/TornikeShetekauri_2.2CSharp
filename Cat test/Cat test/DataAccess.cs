using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat_test
{
    

    public static class DataAccess
    {
        public static List<Question> LoadQuestions()
        {
            return new List<Question>
        {
            new Question { Text = "What is 2 + 2?", Options = new[] { "3", "4", "5", "6" }, CorrectOptionIndex = 1, Discrimination = 1.0, Difficulty = 0.0, Guessing = 0.25 },
            new Question { Text = "What is the capital of France?", Options = new[] { "Berlin", "Madrid", "Paris", "Rome" }, CorrectOptionIndex = 2, Discrimination = 1.2, Difficulty = -0.5, Guessing = 0.25 },
        };
        }
    }
}
