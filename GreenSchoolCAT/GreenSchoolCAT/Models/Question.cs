using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSchoolCAT.Models
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; }                  

        [ForeignKey(nameof(Test))]
        public Guid TestId { get; set; }
        [Required]
        public Test Test { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }

        [Required]
        [MaxLength(100)]
        public string OptionA { get; set; }

        [Required]
        [MaxLength(100)]
        public string OptionB { get; set; }

        [Required]
        [MaxLength(100)]
        public string OptionC { get; set; }

        [Required]
        [MaxLength(100)]
        public string OptionD { get; set; }

        [Required]
        [MaxLength(7)]// როდესაც კონროლერს დავააფდეითებ, ეს 1-ად უნდა გადიწიოს!// ახლა ამის შეცვლა მეშინია
        
        public string CorrectAnswer { get; set; }
        [Required]
        public double? Discrimination { get; set; }
        [Required]
        public double? Difficulty { get; set; }
        [Required]
        public double? Guessing { get; set; }
    }
}
