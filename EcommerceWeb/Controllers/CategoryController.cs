using EcommerceWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly EcommerceWebDbContext _db;

        public CategoryController(EcommerceWebDbContext db)
        {
            _db = db;
        }

        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category ctg)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(ctg);
                _db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Error");
        }


    }
}