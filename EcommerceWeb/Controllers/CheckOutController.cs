using Microsoft.AspNetCore.Mvc;
using EcommerceWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceWeb.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly EcommerceWebDbContext _db;

        public CheckOutController(EcommerceWebDbContext db)
        {
            _db = db;
        }

        // GET: CheckOut
        public ActionResult Index()
        {
            ViewBag.PayMethod = new SelectList(_db.PaymentTypes, "PayTypeID", "TypeName");


            var data = this.GetDefaultData();

            return View(data);
        }


        //PLACE ORDER--LAST STEP
        public ActionResult PlaceOrder(FormCollection getCheckoutDetails)
        {

            int shpID = 1;
            if (_db.ShippingDetails.Count() > 0)
            {
                shpID = _db.ShippingDetails.Max(x => x.ShippingID) + 1;
            }
            int payID = 1;
            if (_db.Payments.Count() > 0)
            {
                payID = _db.Payments.Max(x => x.PaymentID) + 1;
            }
            int orderID = 1;
            if (_db.Orders.Count() > 0)
            {
                orderID = _db.Orders.Max(x => x.OrderID) + 1;
            }



            ShippingDetail shpDetails = new ShippingDetail();
            shpDetails.ShippingID = shpID;
            shpDetails.FirstName = getCheckoutDetails["FirstName"];
            shpDetails.LastName = getCheckoutDetails["LastName"];
            shpDetails.Email = getCheckoutDetails["Email"];
            shpDetails.Mobile = getCheckoutDetails["Mobile"];
            shpDetails.Address = getCheckoutDetails["Address"];
            shpDetails.City = getCheckoutDetails["City"];
            shpDetails.PostCode = getCheckoutDetails["PostCode"];
            _db.ShippingDetails.Add(shpDetails);
            _db.SaveChanges();

            Payment pay = new Payment();
            pay.PaymentID = payID;
            pay.Type = Convert.ToInt32(getCheckoutDetails["PayMethod"]);
            _db.Payments.Add(pay);
            _db.SaveChanges();

            Order o = new Order();
            o.OrderID = orderID;
            o.CustomerID = TempShpData.UserID;
            o.PaymentID = payID;
            o.ShippingID = shpID;
            o.Discount = Convert.ToInt32(getCheckoutDetails["discount"]);
            o.TotalAmount = Convert.ToInt32(getCheckoutDetails["totalAmount"]);
            o.isCompleted = true;
            o.OrderDate = DateTime.Now;
            _db.Orders.Add(o);
            _db.SaveChanges();

            foreach (var OD in TempShpData.items)
            {
                OD.OrderID = orderID;
                OD.Order = _db.Orders.Find(orderID);
                OD.Product = _db.Products.Find(OD.ProductID);
                _db.OrderDetails.Add(OD);
                _db.SaveChanges();
            }


            return RedirectToAction("Index", "ThankYou");

        }

    }
}