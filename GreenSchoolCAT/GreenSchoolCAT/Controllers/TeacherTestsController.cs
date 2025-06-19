using GreenSchoolCAT.Data;
using GreenSchoolCAT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GreenSchoolCAT.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherTestsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TeacherTestsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var teacherId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var tests = await _db.Tests
                .Where(t => t.TeacherId == teacherId) 
                .Include(t => t.Questions)
                .Include(t => t.Results)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return View(tests);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var teacherId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var test = await _db.Tests
                .Include(t => t.Questions)
                .Include(t => t.Results)
                .FirstOrDefaultAsync(t => t.GuidId == id && t.TeacherId == teacherId); 

            if (test == null)
            {
                return NotFound();
            }

            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                
                if (test.Results.Any())
                {
                    _db.TestResults.RemoveRange(test.Results);
                }

                
                if (test.Questions.Any())
                {
                    _db.Questions.RemoveRange(test.Questions);
                }

                
                _db.Tests.Remove(test);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                TempData["SuccessMessage"] = "Test deleted successfully";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                TempData["ErrorMessage"] = $"Error deleting test: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}