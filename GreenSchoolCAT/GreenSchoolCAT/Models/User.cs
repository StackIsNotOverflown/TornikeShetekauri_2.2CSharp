using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenSchoolCAT.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required] public string FullName { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Role { get; set; }  //რაც ხარ, ადმინიც მალე დაემატება შეჯიბრისთვის

        
        public ICollection<Test> TestsCreated { get; set; }
        public ICollection<TestResult> ResultsTaken { get; set; }
    }
}
