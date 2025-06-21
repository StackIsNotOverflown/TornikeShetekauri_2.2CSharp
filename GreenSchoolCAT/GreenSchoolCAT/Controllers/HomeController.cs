using GreenSchoolCAT.Data;
using GreenSchoolCAT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace GreenSchoolCAT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

    
        
        public async Task<IActionResult> Upload(TestUploadViewModel model)
        {
            if (!ModelState.IsValid || model.ExcelFile == null || model.ExcelFile.Length == 0)
            {
                ModelState.AddModelError("ExcelFile", "ატვირთეთ სწორი Excel ფაილი.");
                return View(model);
            }

            if (!Path.GetExtension(model.ExcelFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("ExcelFile", ".xlsx ფორმატია საჭირო.");
                return View(model);
            }

            var teacherId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var test = new Test
            {
                Name = model.Name,
                Password = model.Password,
                TeacherId = teacherId,
                CreatedAt = DateTime.UtcNow
            };

            _db.Tests.Add(test);
            await _db.SaveChangesAsync();

            try
            {
                using var stream = new MemoryStream();
                await model.ExcelFile.CopyToAsync(stream);

                using var workbook = new XLWorkbook(stream);
                var sheet = workbook.Worksheet(1);
                var rows = sheet.RowsUsed().Skip(1);

                foreach (var row in rows)
                {
                    var correctOption = row.Cell(6).GetString().Trim();
                    var correctAnswer = correctOption switch
                    {
                        "OptionA" => row.Cell(2).GetString(), // ჯერჯერობით არ ცვლი ერთ ასოიანებად რადგან სატესტო ფაილები ყველა ეგრეა და მაგათი შეცვლა მეზარებააა.
                        "OptionB" => row.Cell(3).GetString(),
                        "OptionC" => row.Cell(4).GetString(),
                        "OptionD" => row.Cell(5).GetString(),
                        _ => null
                    };

                    var question = new Question
                    {
                        TestId = test.GuidId,
                        Text = row.Cell(1).GetString(),
                        OptionA = row.Cell(2).GetString(),
                        OptionB = row.Cell(3).GetString(),
                        OptionC = row.Cell(4).GetString(),
                        OptionD = row.Cell(5).GetString(),
                        CorrectAnswer = correctAnswer,
                        Discrimination = row.Cell(7).GetDouble(),
                        Difficulty = row.Cell(8).GetDouble(),
                        Guessing = row.Cell(9).GetDouble()
                    };

                    _db.Questions.Add(question);
                }

                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "შეცდომა ფაილის დამუშავებისას: " + ex.Message);
                return View(model);
            }

            return RedirectToAction("TeacherHome");
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult TeacherHome()
        {
            return View();
        }

        [Authorize(Roles = "Student")]
        public IActionResult StudentHome()
        {
            return View();
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Guide()
        {
            return View();
        }
    }
}