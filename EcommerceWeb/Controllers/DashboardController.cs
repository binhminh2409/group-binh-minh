using EcommerceWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace IMS_Project.Controllers
{
    public class DashboardController : Controller
    {
        private readonly EcommerceWebDbContext _db;

        public DashboardController(EcommerceWebDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {

            ViewBag.latestOrders = _db.Orders.OrderByDescending(x => x.OrderID).Take(10).ToList();
            //ViewBag.NewOrders = db.Orders.Where(a => a.DIspatched == false && a.Shipped == false && a.Deliver == false).Count();
            //ViewBag.DispatchedOrders = db.Orders.Where(a => a.DIspatched == true && a.Shipped == false && a.Deliver == false).Count();
            //ViewBag.ShippedOrders = db.Orders.Where(a => a.DIspatched == true && a.Shipped == true && a.Deliver == false).Count();
            //ViewBag.DeliveredOrders = db.Orders.Where(a => a.DIspatched == true && a.Shipped == true && a.Deliver == true).Count();
            return View();
        }

    }
}