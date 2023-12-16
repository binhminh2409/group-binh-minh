using EcommerceWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace EcommerceWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly EcommerceWebDbContext _db;

        public OrderController(EcommerceWebDbContext db)
        {
            _db = db;
        }
        // GET: Order
        public ActionResult Index()
        {
            return View(_db.Orders.OrderByDescending(x => x.OrderID).ToList());
        }
        public ActionResult Details(int id)
        {
            Order ord = _db.Orders.Find(id);
            var Ord_details = _db.OrderDetails.Where(x => x.OrderID == id).ToList();
            var tuple = new Tuple<Order, IEnumerable<OrderDetail>>(ord, Ord_details);

            double SumAmount = Convert.ToDouble(Ord_details.Sum(x => x.TotalAmount));
            ViewBag.TotalItems = Ord_details.Sum(x => x.Quantity);
            ViewBag.Discount = 0;
            ViewBag.TAmount = SumAmount - 0;
            ViewBag.Amount = SumAmount;
            return View(tuple);
        }
    }
}