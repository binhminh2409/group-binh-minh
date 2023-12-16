using EcommerceWeb.Controllers;
using EcommerceWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Models
{
    public class WishListController : Controller
    {
        private readonly EcommerceWebDbContext _db;

        public WishListController(EcommerceWebDbContext db)
        {
            _db = db;
        }

        // GET: WishList
        public ActionResult Index()
        {
            this.GetDefaultData();

            var wishlistProducts = _db.Wishlists.Where(x => x.CustomerID == TempShpData.UserID).ToList();
            return View(wishlistProducts);
        }

        //REMOVE ITEM FROM WISHLIST
        public ActionResult Remove(int id)
        {
            _db.Wishlists.Remove(_db.Wishlists.Find(id));
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        //ADD TO CART WISHLIST
        public ActionResult AddToCart(int id)
        {
            OrderDetail OD = new OrderDetail();

            int pid = _db.Wishlists.Find(id).ProductID;
            OD.ProductID = pid;
            int Qty = 1;
            decimal price = _db.Products.Find(pid).UnitPrice;
            OD.Quantity = Qty;
            OD.UnitPrice = price;
            OD.TotalAmount = Qty * price;
            OD.Product = _db.Products.Find(pid);

            if (TempShpData.items == null)
            {
                TempShpData.items = new List<OrderDetail>();
            }
            TempShpData.items.Add(OD);

            _db.Wishlists.Remove(_db.Wishlists.Find(id));
            _db.SaveChanges();

            return Redirect(TempData["returnURL"].ToString());

        }
    }
}