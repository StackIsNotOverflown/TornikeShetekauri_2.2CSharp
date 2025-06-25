using GreenSchoolCAT.Data;
using GreenSchoolCAT.Models;
using GreenSchoolCAT.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GreenSchoolCAT.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ICatService _cat;

        public TestController(ApplicationDbContext db, ICatService cat)
        {
            _db = db;
            _cat = cat;
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Index()
        {
            var tests = await _db.Tests
                .Include(t => t.Teacher)
                .Where(t => t.Questions.Any())
                .ToListAsync();

            return View(tests);
        }

        [Authorize(Roles = "Student")]
        [HttpGet]
        public async Task<IActionResult> EnterPasscode(Guid testId)
        {
            var test = await _db.Tests.FindAsync(testId);
            if (test == null)
            {
                return NotFound();
            }

            ViewBag.TestId = testId;
            return View();
        }

        [Authorize(Roles = "Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnterPasscode(Guid testId, string passcode)
        {
            var test = await _db.Tests.FirstOrDefaultAsync(t => t.GuidId == testId);
            if (test == null || test.Password != passcode)
            {
                ViewBag.ErrorMessage = "პაროლი";
                ViewBag.TestId = testId;
                return View();
            }

            return RedirectToAction("Start", new { id = testId });
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Start(Guid id)
        {
            var test = await _db.Tests
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.GuidId == id);

            if (test == null || !test.Questions.Any())
            {
                return NotFound();
            }

            TempData["TotalQuestions"] = test.Questions.Count;

            return RedirectToAction("Question", new
            {
                score = 0,
                theta = 0.0,
                asked = "",
                testId = id
            });
        }

        [Authorize(Roles = "Student")]
        [HttpGet]
        public IActionResult Question(Guid testId, int score, double theta, string asked)
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            var totalQuestions = _db.Questions.Count(q => q.TestId == testId);
            ViewBag.TotalQuestions = totalQuestions;

            var askedList = string.IsNullOrEmpty(asked)
                ? new List<Guid>()
                : asked.Split(',').Select(Guid.Parse).ToList();

            var pool = _db.Questions
                .Where(q => q.TestId == testId && !askedList.Contains(q.Id))
                .ToList();

            if (!pool.Any() || askedList.Count >= totalQuestions)
            {
                return RedirectToAction("Result", new { score, theta, testId });
            }

            var nextQuestion = _cat.GetNextQuestion(theta, pool);
            askedList.Add(nextQuestion.Id);

            ViewBag.Score = score;
            ViewBag.Theta = theta;
            ViewBag.Asked = string.Join(",", askedList);
            ViewBag.TestId = testId;

            return View(nextQuestion);
        }

        [Authorize(Roles = "Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(Guid id, string answer, int score, double theta, string asked, Guid testId)
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            var question = _db.Questions.FirstOrDefault(q => q.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            var totalQuestions = _db.Questions.Count(q => q.TestId == testId);
            ViewBag.TotalQuestions = totalQuestions;

            bool isCorrect = string.Equals(
                question.CorrectAnswer?.Trim(),
                answer?.Trim(),
                StringComparison.OrdinalIgnoreCase);

            if (isCorrect)
            {
                score++;
            }

            theta = _cat.UpdateTheta(theta, question, isCorrect);

            return RedirectToAction("Question", new
            {
                testId,
                score,
                theta,
                asked
            });
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AllTests()
        {
            var teacherId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var tests = await _db.Tests
                .Where(t => t.TeacherId == teacherId)
                .Include(t => t.Teacher)
                .Include(t => t.Questions)
                .Include(t => t.Results)
                .Select(t => new TestViewModel
                {
                    GuidId = t.GuidId,
                    Name = t.Name,
                    CreatedAt = t.CreatedAt,
                    TeacherName = t.Teacher.FullName,
                    QuestionCount = t.Questions.Count,
                    ResultCount = t.Results.Count
                })
                .ToListAsync();

            return View(tests);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTest(Guid id)
        {
            var teacherId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var test = await _db.Tests
                .Include(t => t.Questions)
                .Include(t => t.Results)
                .FirstOrDefaultAsync(t => t.GuidId == id && t.TeacherId == teacherId);

            if (test == null)
            {
                TempData["ErrorMessage"] = "ტესტი ვერ მოიძებნა ან არ გაქვთ წაშლის უფლება";
                return RedirectToAction(nameof(AllTests));
            }

            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                if (test.Questions != null)
                {
                    _db.Questions.RemoveRange(test.Questions);
                }

                _db.Tests.Remove(test);

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                TempData["SuccessMessage"] = $"ტესტი '{test.Name}' და მასთან დაკავშირებული {test.Questions?.Count ?? 0} კითხვა წარმატებით წაიშალა";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                TempData["ErrorMessage"] = $"შეცდომა ტესტის წაშლისას: {ex.Message}";
            }

            return RedirectToAction(nameof(AllTests));
        }

        [Authorize(Roles = "Student")]
        [HttpGet]
        public IActionResult Result(int score, double theta, Guid testId)
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var totalQuestions = _db.Questions.Count(q => q.TestId == testId);
            ViewBag.TotalQuestions = totalQuestions;

            _db.TestResults.Add(new TestResult
            {
                TestId = testId,
                StudentId = Guid.Parse(userId),
                Score = score,
                AbilityEstimate = theta,
                DateTaken = DateTime.UtcNow
            });

            _db.SaveChanges();

            ViewBag.Score = score;
            ViewBag.Theta = theta;
            return View();
        }
    }
}