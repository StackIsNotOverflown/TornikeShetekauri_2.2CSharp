using System.ComponentModel.DataAnnotations;

namespace GreenSchoolCAT.Models
{
    public class TestViewModel
    {
        [Required]
        public Guid GuidId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string TeacherName { get; set; }
        [Required]
        public int QuestionCount { get; set; }
        [Required]
        public int ResultCount { get; set; }
    }
}