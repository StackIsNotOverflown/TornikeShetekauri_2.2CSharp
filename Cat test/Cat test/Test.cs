using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Cat_test
{
    public partial class Test : Form
    {
        private IRTModel _irtModel;
        private Question _currentQuestion;
        private int _timeLeft;

        public Test()
        {
            InitializeComponent();
            _irtModel = new IRTModel();
            LoadNextQuestion();
        }

        private void LoadNextQuestion()
        {
            _currentQuestion = _irtModel.GetNextQuestion();
            questionLabel.Text = _currentQuestion.Text;

            // ????????? ?????
            radioButton1.Text = _currentQuestion.Options[0];
            radioButton2.Text = _currentQuestion.Options[1];
            radioButton3.Text = _currentQuestion.Options[2];
            radioButton4.Text = _currentQuestion.Options[3];

            // ???????? ?????
            _timeLeft = 3 * 60; // - 3 *60 ??????? ???? ?????
            timer1.Start();
        }

        private void submitButton_Click(object sender, EventArgs e, string filePath, double _currentAbility)
        {
            int selectedOption = -1;
            if (radioButton1.Checked) selectedOption = 0;
            else if (radioButton2.Checked) selectedOption = 1;
            else if (radioButton3.Checked) selectedOption = 2;
            else if (radioButton4.Checked) selectedOption = 3;

            if (selectedOption != -1)
            {
                bool isCorrect = _irtModel.EvaluateAnswer(_currentQuestion, selectedOption);
               

                _irtModel.UpdateAbility(isCorrect);

                if (_irtModel.HasMoreQuestions())
                {
                    LoadNextQuestion();
                }
                else
                {
                    MessageBox.Show("ტესტი დასრულებულია.");
                    timer1.Stop();
                    string fp = "C:\\Users\\User\\Desktop\\TornikeShetekauri_2.2CSharp\\Cat test\\Cat test\\Results.txt";
                   // using (StreamWriter writer = new StreamWriter, true, string fp)
                   using (StreamWriter writer = new StreamWriter(fp, true))
                    {
                        writer.WriteLine = "Ability =" + _currentAbility + ".";// ეს მიერორებს და არ ვიცი რატომ
                    }
                    
                     
                    
                }
            }
            else
            {
                //idk for now
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_timeLeft > 0)
            {
                _timeLeft--;
                timeLabel.Text = $"დარჩენილი დრო: {_timeLeft / 60:D2}:{_timeLeft % 60:D2}";
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("დრო ამოიწურა!");
                LoadNextQuestion();
            }
        }
    }
}
