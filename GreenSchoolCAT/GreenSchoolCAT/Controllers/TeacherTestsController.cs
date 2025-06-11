using GreenSchoolCAT.Data;
using GreenSchoolCAT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenSchoolCAT.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherTestsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeacherTestsController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var teacherId = _userManager.GetUserId(User);
            var tests = await _db.Tests
                .Where(t => t.TeacherId == teacherId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return View(tests);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var teacherId = _userManager.GetUserId(User);
            var test = await _db.Tests
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.Id == id && t.TeacherId == teacherId);

            if (test == null)
                return NotFound();

            _db.Questions.RemoveRange(test.Questions);
            _db.Tests.Remove(test);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
