using EcommerceWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceWeb.Models
{
    public class SupplierController : Controller
    {
        private readonly EcommerceWebDbContext _db;

        public SupplierController(EcommerceWebDbContext db)
        {
            _db = db;
        }
        // GET: Supplier
        public ActionResult Index()
        {
            return View(_db.Suppliers.ToList());
        }

        // CREATE: Supplier

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Supplier suplr)
        {
            if (ModelState.IsValid)
            {
                _db.Suppliers.Add(suplr);
                _db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Error");
        }


        // EDIT: Supplier

        public ActionResult Edit(int id)
        {
            Supplier suplr = _db.Suppliers.Single(x => x.SupplierID == id);
            if (suplr == null)
            {
                return NotFound();
            }
            return View(suplr);
        }

        [HttpPost]
        public ActionResult Edit(Supplier suplr)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(suplr).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(suplr);
        }


        // DETAILS: Supplier

        public ActionResult Details(int id)
        {
            Supplier suplr = _db.Suppliers.Find(id);
            if (suplr == null)
            {
                return NotFound();
            }
            return View(suplr);
        }

        // DELETE: Supplier

        public ActionResult Delete(int id)
        {
            Supplier suplr = _db.Suppliers.Find(id);
            if (suplr == null)
            {
                return NotFound();
            }
            return View(suplr);
        }

        //Post Delete Confirmed

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier suplr = _db.Suppliers.Find(id);
            _db.Suppliers.Remove(suplr);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}