using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GreenSchoolCAT.Models
{
    public class TestUploadViewModel
    {
        [Required(ErrorMessage = "შეიყვანეთ ტესტის სახელი")]
        public string Name { get; set; }

        [Required(ErrorMessage = "შეიყვანეთ პაროლი")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ატვირთეთ Excel ფაილი")]
        public IFormFile ExcelFile { get; set; }
    }
}
