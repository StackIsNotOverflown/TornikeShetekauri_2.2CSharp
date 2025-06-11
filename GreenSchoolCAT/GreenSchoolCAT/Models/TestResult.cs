using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSchoolCAT.Models
{
    public class TestResult
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }                   

        [ForeignKey(nameof(Test))]
        public Guid TestId { get; set; }             
        public Test Test { get; set; }

        [ForeignKey(nameof(Student))]
        public Guid StudentId { get; set; }           
        public User Student { get; set; }

        public DateTime DateTaken { get; set; }

        public double AbilityEstimate { get; set; }

        public int Score { get; set; }
        
    }
}
