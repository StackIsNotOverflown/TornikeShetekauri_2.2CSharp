using Microsoft.AspNetCore.Mvc;
using Practice._3.Models;

namespace Practice._3.Controllers
{
    public class OrderController : Controller
    {
        private static List<Order> orders = new List<Order>();

        public ActionResult Index()
        {
            return View(orders);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Order order)
        {
            if (ModelState.IsValid)
            {
                orders.Add(order);
                return RedirectToAction("Index");
            }
            return View(order);
        }

        public ActionResult Edit(string shemkvetiID)
        {
            var order = orders.FirstOrDefault(o => o.shemkvetiID == shemkvetiID);
            if (order == null)
            {
                return Index();
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            var existingOrder = orders.FirstOrDefault(o => o.shemkvetiID == order.shemkvetiID);
            if (existingOrder != null)
            {
                existingOrder.personaliID = order.personaliID;
                existingOrder.iuridiuli_fizikuri = order.iuridiuli_fizikuri;
                existingOrder.gvari = order.gvari;
                existingOrder.saxeli = order.saxeli;
                existingOrder.qalaqi = order.qalaqi;
                existingOrder.regioni = order.regioni;
                existingOrder.raioni = order.raioni;
                existingOrder.sqesi = order.sqesi;
                existingOrder.misamarti = order.misamarti;
                existingOrder.mobiluri = order.mobiluri;
                existingOrder.firmis_dasaxeleba = order.firmis_dasaxeleba;
                existingOrder.mobiluri_direqtoris = order.mobiluri_direqtoris;
                existingOrder.gvari_direqtoris = order.gvari_direqtoris;
                existingOrder.sabanko_angarishi = order.sabanko_angarishi;
                existingOrder.email = order.email;

                return RedirectToAction("Index");
            }
            return View(order);
        }

        public ActionResult Delete(string shemkvetiID)
        {
            var order = orders.FirstOrDefault(o => o.shemkvetiID == shemkvetiID);
            if (order != null)
            {
                orders.Remove(order);
            }
            return RedirectToAction("Index");
        }
    }
}