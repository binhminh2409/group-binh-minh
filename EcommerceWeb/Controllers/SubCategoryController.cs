using EcommerceWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceWeb.Models
{
    public class SubCategoryController : Controller
    {
        private readonly EcommerceWebDbContext _db;

        public SubCategoryController(EcommerceWebDbContext db)
        {
            _db = db;
        }
        // GET: SubCategory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.categoryList = new SelectList(_db.Categories, "CategoryID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(SubCategory sctg)
        {
            if (ModelState.IsValid)
            {
                _db.SubCategories.Add(sctg);
                _db.SaveChanges();
                return PartialView("_Success");
            }
            ViewBag.supplierList = new SelectList(_db.Categories, "CategoryID", "Name");
            return PartialView("_Error");
        }

    }
}