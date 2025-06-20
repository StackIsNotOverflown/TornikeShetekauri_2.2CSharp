namespace GreenSchoolCAT.Models
{
    public class TestViewModel
    {
        public Guid GuidId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TeacherName { get; set; }
        public int QuestionCount { get; set; }
        public int ResultCount { get; set; }
    }
}