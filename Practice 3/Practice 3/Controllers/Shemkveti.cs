using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Practice_3.Controllers
{
    public class Shemkveti : Controller
    {
        // GET: Shemkveti
        public ActionResult Index()
        {
            return View();
        }

        // GET: Shemkveti/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Shemkveti/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shemkveti/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Shemkveti/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shemkveti/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Shemkveti/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shemkveti/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
