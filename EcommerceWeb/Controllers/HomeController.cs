using EcommerceWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace EcommerceWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly EcommerceWebDbContext _db;

        public HomeController(EcommerceWebDbContext db)
        {
            _db = db;
        }

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.MenProduct = _db.Products.Where(x => x.Category.Name.Equals("Men's Fashion")).ToList();
            ViewBag.WomenProduct = _db.Products.Where(x => x.Category.Name.Equals("Women's Fashion")).ToList();
            ViewBag.AccessoriesProduct = _db.Products.Where(x => x.Category.Name.Equals("Electronic Accessories")).ToList();
            ViewBag.ElectronicsProduct = _db.Products.Where(x => x.Category.Name.Equals("Electronic Devices")).ToList();
            ViewBag.Slider = _db.genMainSliders.ToList();
            ViewBag.PromoRight = _db.genPromoRights.ToList();

            this.GetDefaultData();

            return View();
        }
    }
}