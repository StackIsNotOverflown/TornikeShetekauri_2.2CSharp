using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat_test
{
    public class Question
    {
        public string Text { get; set; }
        public string[] Options { get; set; }
        public int CorrectOptionIndex { get; set; }
        public double Discrimination { get; set; }
        public double Difficulty { get; set; }
        public double Guessing { get; set; }
    }
}
