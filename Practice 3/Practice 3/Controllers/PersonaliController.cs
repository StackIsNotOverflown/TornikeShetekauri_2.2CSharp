using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Practice_3.Controllers
{
    public class PersonaliController : Controller
    {
        // GET: PersonaliController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PersonaliController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PersonaliController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonaliController/Create
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

        // GET: PersonaliController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PersonaliController/Edit/5
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

        // GET: PersonaliController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonaliController/Delete/5
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
