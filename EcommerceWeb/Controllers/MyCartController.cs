using EcommerceWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EcommerceWeb.Controllers
{
    public class MyCartController : Controller
    { 
        private readonly EcommerceWebDbContext _db;

        public MyCartController(EcommerceWebDbContext db)
        {
            _db = db;
        }

        // GET: MyCart
        public ActionResult Index()
        {
            this.GetDefaultData();

            var usr = _db.Customers.Find(TempShpData.UserID);
            return View(usr);

        }

        public ActionResult Remove(int id)
        {
            TempShpData.items.RemoveAll(x => x.ProductID == id);
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult ProcedToCheckout(FormCollection formcoll)
        {
            var a = TempShpData.items.ToList();
            for (int i = 0; i < formcoll.Count / 2; i++)
            {

                int pID = Convert.ToInt32(formcoll["shcartID-" + i + ""]);
                var ODetails = TempShpData.items.FirstOrDefault(x => x.ProductID == pID);


                int qty = Convert.ToInt32(formcoll["Qty-" + i + ""]);
                ODetails.Quantity = qty;
                ODetails.UnitPrice = ODetails.UnitPrice;
                ODetails.TotalAmount = qty * ODetails.UnitPrice;
                TempShpData.items.RemoveAll(x => x.ProductID == pID);

                if (TempShpData.items == null)
                {
                    TempShpData.items = new List<OrderDetail>();
                }
                TempShpData.items.Add(ODetails);

            }

            return RedirectToAction("Index", "CheckOut");
        }


    }
}