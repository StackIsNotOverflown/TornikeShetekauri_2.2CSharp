using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSchoolCAT.Models
{
    public class Test
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }                   

        [Key]
        public Guid GuidId { get; set; }              

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

       

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }           

        

        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(Teacher))]
        public Guid TeacherId { get; set; }            
        public User Teacher { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<TestResult> Results { get; set; }
    }
}
