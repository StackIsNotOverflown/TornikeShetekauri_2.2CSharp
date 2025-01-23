using Microsoft.AspNetCore.Mvc;
using Practice._3.Models;

namespace Practice._3.Controllers
{
    public class PersonaliController : Controller
    {
        private static List<Personali> personaliList = new List<Personali>();

        public ActionResult Index()
        {
            return View(personaliList);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Personali personali)
        {
            if (ModelState.IsValid)
            {
                personaliList.Add(personali);
                return RedirectToAction("Index");
            }
            return View(personali);
        }

        public ActionResult Edit(string personaliID)
        {
            var personali = personaliList.FirstOrDefault(p => p.personaliID == personaliID);
            if (personali == null)
            {
                return Index();
            }
            return View(personali);
        }

        [HttpPost]
        public ActionResult Edit(Personali personali)
        {
            var existingPersonali = personaliList.FirstOrDefault(p => p.personaliID == personali.personaliID);
            if (existingPersonali != null)
            {
                existingPersonali.gvari = personali.gvari;
                existingPersonali.saxeli = personali.saxeli;
                existingPersonali.ganyofileba = personali.ganyofileba;
                existingPersonali.qalaqi = personali.qalaqi;
                existingPersonali.regioni = personali.regioni;
                existingPersonali.raioni = personali.raioni;
                existingPersonali.xelfasi = personali.xelfasi;
                existingPersonali.asaki = personali.asaki;
                existingPersonali.staji = personali.staji;
                existingPersonali.tarigi_dabadebis = personali.tarigi_dabadebis;
                existingPersonali.sqesi = personali.sqesi;
                existingPersonali.misamarti_saxlis = personali.misamarti_saxlis;
                existingPersonali.teleponi_saxlis = personali.teleponi_saxlis;
                existingPersonali.mobiluri = personali.mobiluri;
                existingPersonali.email = personali.email;
                existingPersonali.ierarqia = personali.ierarqia;

                return RedirectToAction("Index");
            }
            return View(personali);
        }

        public ActionResult Delete(string personaliID)
        {
            var personali = personaliList.FirstOrDefault(p => p.personaliID == personaliID);
            if (personali != null)
            {
                personaliList.Remove(personali);
            }
            return RedirectToAction("Index");
        }
    }
}
