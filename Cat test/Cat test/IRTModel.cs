using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cat_test
{
    

    public class IRTModel
    {
        private List<Question> _questions;
        public double _currentAbility;
        private int _currentQuestionIndex;

        public IRTModel()
        {
            _questions = DataAccess.LoadQuestions();
            _currentAbility = 0.0;
            _currentQuestionIndex = 0;
        }

        public Question GetNextQuestion()
        {
            return _questions[_currentQuestionIndex++];
        }

        public bool EvaluateAnswer(Question question, int selectedOptionIndex)
        {
            return question.CorrectOptionIndex == selectedOptionIndex;
        }

        public void UpdateAbility(bool isCorrect)
        {
            double a = _questions[_currentQuestionIndex - 1].Discrimination;
            double b = _questions[_currentQuestionIndex - 1].Difficulty;
            double c = _questions[_currentQuestionIndex - 1].Guessing;

            double p = c + (1 - c) / (1 + Math.Exp(-a * (_currentAbility - b)));
            _currentAbility += isCorrect ? 1 - p : -p;
        }

        public double GetAbilityEstimate()
        {
            return _currentAbility;
            
        }

        public bool HasMoreQuestions()
        {
            return _currentQuestionIndex < _questions.Count;
        }
    }
}
