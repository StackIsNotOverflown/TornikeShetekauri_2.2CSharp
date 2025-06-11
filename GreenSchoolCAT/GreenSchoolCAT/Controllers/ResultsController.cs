using GreenSchoolCAT.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GreenSchoolCAT.Controllers
{
    [Authorize]
    public class ResultsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ResultsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> MyResults()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var results = await _db.TestResults
                .Where(r => r.StudentId == userId)
                .Include(r => r.Test)
                    .ThenInclude(t => t.Teacher)
                .OrderByDescending(r => r.DateTaken)
                .Select(r => new MyResultsViewModel
                {
                    TestName = r.Test.Name,
                    TeacherName = r.Test.Teacher.FullName,
                    Score = r.Score,
                    Theta = r.AbilityEstimate,
                    DateTaken = r.DateTaken
                })
                .ToListAsync();

            return View(results);
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AllResults()
        {
            var teacherId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var results = await _db.TestResults
                .Include(r => r.Test)
                .Include(r => r.Student)
                .Where(r => r.Test.TeacherId == teacherId)
                .OrderBy(r => r.Test.Name)
                .ThenBy(r => r.Student.FullName)
                .Select(r => new AllResultsViewModel
                {
                    TestName = r.Test.Name,
                    TestPassword = r.Test.Password,
                    TeacherName = r.Test.Teacher.FullName,
                    StudentName = r.Student.FullName,
                    Score = r.Score,
                    Theta = r.AbilityEstimate,
                    DateTaken = r.DateTaken
                })
                .ToListAsync();

            return View(results);
        }
    }
}