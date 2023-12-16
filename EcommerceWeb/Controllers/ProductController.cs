using EcommerceWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly EcommerceWebDbContext _db;

        public ProductController(EcommerceWebDbContext db)
        {
            _db = db;
        }


        private readonly IWebHostEnvironment _env;

        public ProductController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // GET: Product
        public ActionResult Index()
        {
            return View(_db.Products.ToList());
        }


        // CREATE: Product

        public ActionResult Create()
        {
            ViewBag.supplierList = _db.Suppliers.Select(x => new SelectListItem { Value = x.SupplierID.ToString(), Text = x.CompanyName }).ToList();
            ViewBag.categoryList = _db.Categories.Select(x => new SelectListItem { Value = x.CategoryID.ToString(), Text = x.Name }).ToList();
            ViewBag.SubCategoryList = _db.SubCategories.Select(x => new SelectListItem { Value = x.SubCategoryID.ToString(), Text = x.Name }).ToList();
            return View();
        }


        [HttpPost]
        public ActionResult Create(ProductVM pvm)
        {

            if (ModelState.IsValid)
            {
                if (pvm.Picture != null)
                {
                    string filePath = Path.Combine("Images", Guid.NewGuid().ToString() + Path.GetExtension(pvm.Picture.FileName));
                    string fullPath = Path.Combine(_env.WebRootPath, filePath);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        pvm.Picture.CopyTo(stream);
                    }

                    Product p = new Product
                    {
                        ProductID = pvm.ProductID,
                        Name = pvm.Name,
                        SupplierID = pvm.SupplierID,
                        CategoryID = pvm.CategoryID,
                        SubCategoryID = pvm.SubCategoryID,
                        UnitPrice = pvm.UnitPrice,
                        OldPrice = pvm.OldPrice,
                        Discount = pvm.Discount,
                        UnitInStock = pvm.UnitInStock,
                        ProductAvailable = pvm.ProductAvailable,
                        ShortDescription = pvm.ShortDescription,
                        Note = pvm.Note,
                        PicturePath = filePath
                    };
                    _db.Products.Add(p);
                    _db.SaveChanges();
                    return PartialView("_Success");
                }
            }
            ViewBag.supplierList = new SelectList(_db.Suppliers, "SupplierID", "CompanyName");
            ViewBag.categoryList = new SelectList(_db.Suppliers, "CategoryID", "Name");
            ViewBag.SubCategoryList = new SelectList(_db.Suppliers, "SubCategoryID", "Name");
            return PartialView("_Error");
        }




        // EDIT: Product


        public ActionResult Edit(int id)
        {
            Product p = _db.Products.Find((int)id);

            ProductVM pvm = new ProductVM
            {
                ProductID = p.ProductID,
                Name = p.Name,
                SupplierID = p.SupplierID,
                CategoryID = p.CategoryID,
                SubCategoryID = (int)p.SubCategoryID,
                UnitPrice = p.UnitPrice,
                OldPrice = (int)p.OldPrice,
                Discount = (int)p.Discount,
                UnitInStock = (int)p.UnitInStock,
                ProductAvailable = p.ProductAvailable,
                ShortDescription = p.ShortDescription,
                Note = p.Note,
                PicturePath = p.PicturePath

            };
            ViewBag.supplierList = new SelectList(_db.Suppliers, "SupplierID", "CompanyName");
            ViewBag.categoryList = new SelectList(_db.Categories, "CategoryID", "Name");
            ViewBag.SubCategoryList = new SelectList(_db.SubCategories, "SubCategoryID", "Name");
            return View(pvm);
        }

        [HttpPost]

        public ActionResult Edit(ProductVM pvm)
        {

            if (ModelState.IsValid)
            {
                string filePath = Path.Combine("Images", Guid.NewGuid().ToString() + Path.GetExtension(pvm.Picture.FileName));
                if (pvm.Picture != null)
                {
                    string fullPath = Path.Combine(_env.WebRootPath, filePath);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        pvm.Picture.CopyTo(stream);
                    }
                    Product p = new Product
                    {
                        ProductID = pvm.ProductID,
                        Name = pvm.Name,
                        SupplierID = pvm.SupplierID,
                        CategoryID = pvm.CategoryID,
                        SubCategoryID = pvm.SubCategoryID,
                        UnitPrice = pvm.UnitPrice,
                        OldPrice = pvm.OldPrice,
                        Discount = pvm.Discount,
                        UnitInStock = pvm.UnitInStock,
                        ProductAvailable = pvm.ProductAvailable,
                        ShortDescription = pvm.ShortDescription,
                        Note = pvm.Note,
                        PicturePath = filePath
                    };
                    _db.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Product p = new Product
                    {
                        ProductID = pvm.ProductID,
                        Name = pvm.Name,
                        SupplierID = pvm.SupplierID,
                        CategoryID = pvm.CategoryID,
                        SubCategoryID = pvm.SubCategoryID,
                        UnitPrice = pvm.UnitPrice,
                        OldPrice = pvm.OldPrice,
                        Discount = pvm.Discount,
                        UnitInStock = pvm.UnitInStock,
                        ProductAvailable = pvm.ProductAvailable,
                        ShortDescription = pvm.ShortDescription,
                        Note = pvm.Note,
                        PicturePath = filePath
                    };
                    _db.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.supplierList = new SelectList(_db.Suppliers, "SupplierID", "CompanyName");
            ViewBag.categoryList = new SelectList(_db.Categories, "CategoryID", "Name");
            ViewBag.SubCategoryList = new SelectList(_db.SubCategories, "SubCategoryID", "Name");
            return PartialView(pvm);
        }


        // DETAILS: Product
        public ActionResult Details(int id)
        {
            Product p = _db.Products.Find(id);

            ProductVM pvm = new ProductVM
            {
                ProductID = p.ProductID,
                Name = p.Name,
                SupplierID = p.SupplierID,
                CategoryID = p.CategoryID,
                SubCategoryID = p.SubCategoryID,
                UnitPrice = p.UnitPrice,
                OldPrice = p.OldPrice,
                Discount = p.Discount,
                UnitInStock = p.UnitInStock,
                ProductAvailable = p.ProductAvailable,
                ShortDescription = p.ShortDescription,
                Note = p.Note,
                PicturePath = p.PicturePath

            };
            ViewBag.supplierList = new SelectList(_db.Suppliers, "SupplierID", "CompanyName");
            ViewBag.categoryList = new SelectList(_db.Categories, "CategoryID", "Name");
            ViewBag.SubCategoryList = new SelectList(_db.SubCategories, "SubCategoryID", "Name");
            return View(pvm);
        }

        [HttpPost]

        public ActionResult Details(ProductVM pvm)
        {

            if (ModelState.IsValid)
            {
                string filePath = pvm.PicturePath;
                if (pvm.Picture != null)
                {
                    filePath = Path.Combine("Images", Guid.NewGuid().ToString() + Path.GetExtension(pvm.Picture.FileName));
                    string fullPath = Path.Combine(_env.WebRootPath, filePath);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        pvm.Picture.CopyTo(stream);
                    }

                    Product p = new Product
                    {
                        ProductID = pvm.ProductID,
                        Name = pvm.Name,
                        SupplierID = pvm.SupplierID,
                        CategoryID = pvm.CategoryID,
                        SubCategoryID = pvm.SubCategoryID,
                        UnitPrice = pvm.UnitPrice,
                        OldPrice = pvm.OldPrice,
                        Discount = pvm.Discount,
                        UnitInStock = pvm.UnitInStock,
                        ProductAvailable = pvm.ProductAvailable,
                        ShortDescription = pvm.ShortDescription,
                        Note = pvm.Note,
                        PicturePath = filePath
                    };
                    _db.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Product p = new Product
                    {
                        ProductID = pvm.ProductID,
                        Name = pvm.Name,
                        SupplierID = pvm.SupplierID,
                        CategoryID = pvm.CategoryID,
                        SubCategoryID = pvm.SubCategoryID,
                        UnitPrice = pvm.UnitPrice,
                        OldPrice = pvm.OldPrice,
                        Discount = pvm.Discount,
                        UnitInStock = pvm.UnitInStock,
                        ProductAvailable = pvm.ProductAvailable,
                        ShortDescription = pvm.ShortDescription,
                        Note = pvm.Note,
                        PicturePath = filePath
                    };
                    _db.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.supplierList = new SelectList(_db.Suppliers, "SupplierID", "CompanyName");
            ViewBag.categoryList = new SelectList(_db.Categories, "CategoryID", "Name");
            ViewBag.SubCategoryList = new SelectList(_db.SubCategories, "SubCategoryID", "Name");
            return PartialView(pvm);
        }


        // DELETE: Product

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Product product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            Product product = _db.Products.Find(id);
            string file_name = product.PicturePath;
            string path = Path.Combine(_env.WebRootPath, file_name);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}