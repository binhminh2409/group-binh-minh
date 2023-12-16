using EcommerceWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    public class Product1Controller : Controller
    {
        private readonly EcommerceWebDbContext _db;

        public Product1Controller(EcommerceWebDbContext db)
        {
            _db = db;
        }



        public ActionResult Index()
        {
            ViewBag.Categories = _db.Categories.Select(x => x.Name).ToList();
           
            //ViewBag.TopRatedProducts = TopSoldProducts();
            ViewBag.RecentViewsProducts = RecentViewProducts();

            return View("Products");
        }

        //TOP SOLD PRODUCTS
        //public List<TopSoldProduct> TopSoldProducts()
        //{
        //    var prodList = (from prod in _db.OrderDetails
        //                    select new { prod.ProductID, prod.Quantity } into p
        //                    group p by p.ProductID into g
        //                    select new
        //                    {
        //                        pID = g.Key,
        //                        sold = g.Sum(x => x.Quantity)
        //                    }).OrderByDescending(y => y.sold).Take(3).ToList();



        //    List<TopSoldProduct> topSoldProds = new List<TopSoldProduct>();

        //    for (int i = 0; i < 3; i++)
        //    {
        //        topSoldProds.Add(new TopSoldProduct()
        //        {
        //            product = _db.Products.Find(prodList[i].pID),
        //            CountSold = Convert.ToInt32(prodList[i].sold)
        //        });
        //    }
        //    return topSoldProds;
        //}
        //RECENT VIEWS PRODUCTS
        public IEnumerable<Product> RecentViewProducts()
        {
            if (TempShpData.UserID > 0)
            {
                var top3Products = (from recent in _db.RecentlyViews
                                    where recent.CustomerID == TempShpData.UserID
                                    orderby recent.ViewDate descending
                                    select recent.ProductID).ToList().Take(3);

                var recentViewProd = _db.Products.Where(x => top3Products.Contains(x.ProductID));
                return recentViewProd;
            }
            else
            {
                var prod = (from p in _db.Products
                            select p).OrderByDescending(x => x.UnitPrice).Take(3).ToList();
                return prod;
            }
        }

        //ADD TO CART
        public ActionResult AddToCart(int id)
        {
            OrderDetail OD = new OrderDetail();
            OD.ProductID = id;
            int Qty = 1;
            decimal price = _db.Products.Find(id).UnitPrice;
            OD.Quantity = Qty;
            OD.UnitPrice = price;
            OD.TotalAmount = Qty * price;
            OD.Product = _db.Products.Find(id);

            if (TempShpData.items == null)
            {
                TempShpData.items = new List<OrderDetail>();
            }
            TempShpData.items.Add(OD);
            AddRecentViewProduct(id);
            return Redirect(TempData["returnURL"].ToString());

        }

        //VIEW DETAILS
        public ActionResult ViewDetails(int id)
        {
            var prod = _db.Products.Find(id);
            var reviews = _db.Reviews.Where(x => x.ProductID == id).ToList();
            ViewBag.Reviews = reviews;
            ViewBag.TotalReviews = reviews.Count();
            ViewBag.RelatedProducts = _db.Products.Where(y => y.CategoryID == prod.CategoryID).ToList();
            AddRecentViewProduct(id);

            var ratedProd = _db.Reviews.Where(x => x.ProductID == id).ToList();
            int count = ratedProd.Count();
            int TotalRate = ratedProd.Sum(x => x.Rate).GetValueOrDefault();
            ViewBag.AvgRate = TotalRate > 0 ? TotalRate / count : 0;

            this.GetDefaultData();
            return View(prod);
        }

        //WISHLIST
        public ActionResult WishList(int id)
        {

            Wishlist wl = new Wishlist();
            wl.ProductID = id;
            wl.CustomerID = TempShpData.UserID;

            _db.Wishlists.Add(wl);
            _db.SaveChanges();
            AddRecentViewProduct(id);
            ViewBag.WlItemsNo = _db.Wishlists.Where(x => x.CustomerID == TempShpData.UserID).ToList().Count();
            if (TempData["returnURL"].ToString() == "/")
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect(TempData["returnURL"].ToString());
        }

        //ADD RECENT VIEWS PRODUCT IN _db
        public void AddRecentViewProduct(int pid)
        {
            if (TempShpData.UserID > 0)
            {
                RecentlyView Rv = new RecentlyView();
                Rv.CustomerID = TempShpData.UserID;
                Rv.ProductID = pid;
                Rv.ViewDate = DateTime.Now;
                _db.RecentlyViews.Add(Rv);
                _db.SaveChanges();
            }
        }

        //ADD REVIEWS ABOUT PRODUCT
        public ActionResult AddReview(int productID, FormCollection getReview)
        {

            Review r = new Review();
            r.CustomerID = TempShpData.UserID;
            r.ProductID = productID;
            r.Name = getReview["name"];
            r.Email = getReview["email"];
            r.Review1 = getReview["message"];
            r.Rate = Convert.ToInt32(getReview["rate"]);
            r.DateTime = DateTime.Now;

            _db.Reviews.Add(r);
            _db.SaveChanges();
            return RedirectToAction("ViewDetails/" + productID + "");

        }


        public ActionResult Products(int subCatID)
        {
            ViewBag.Categories = _db.Categories.Select(x => x.Name).ToList();
            var prods = _db.Products.Where(y => y.SubCategoryID == subCatID).ToList();
            return View(prods);
        }

        //GET PRODUCTS BY CATEGORY
        public ActionResult GetProductsByCategory(string categoryName, int? page)
        {
            ViewBag.Categories = _db.Categories.Select(x => x.Name).ToList();
            ViewBag.SubCategories = _db.SubCategories.Select(x => x.Name).ToList();
            //ViewBag.TopRatedProducts = TopSoldProducts();

            ViewBag.RecentViewsProducts = RecentViewProducts();

            var prods = _db.Products.Where(x => x.SubCategory.Name == categoryName).ToList();
            int pageSize = 9;
            int pageNumber = page ?? 1;
            return View("Products", prods.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }




        //SEARCH BAR RESULT
        public ActionResult Search(string product, int? page)
        {
            ViewBag.SubCategories = _db.SubCategories.Select(x => x.Name).ToList();
            //ViewBag.TopRatedProducts = TopSoldProducts();

            ViewBag.RecentViewsProducts = RecentViewProducts();

            List<Product> products;
            if (!string.IsNullOrEmpty(product))
            {
                products = _db.Products.Where(x => x.Name.StartsWith(product)).ToList();
            }
            else
            {
                products = _db.Products.ToList();
            }
            int pageSize = 6;
            int pageNumber = page ?? 1;
            return View("Products", products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }


        public JsonResult GetProducts(string term)
        {
            List<string> prodNames = _db.Products.Where(x => x.Name.StartsWith(term)).Select(y => y.Name).ToList();
            return Json(prodNames);
        }


        //  Filter Product By Price
        public ActionResult FilterByPrice(int minPrice, int maxPrice, int? page)
        {
            ViewBag.SubCategories = _db.SubCategories.Select(x => x.Name).ToList();
            //ViewBag.TopRatedProducts = TopSoldProducts();

            ViewBag.RecentViewsProducts = RecentViewProducts();
            ViewBag.filterByPrice = true;
            var filterProducts = _db.Products.Where(x => x.UnitPrice >= minPrice && x.UnitPrice <= maxPrice).ToList();
            int pageSize = 9;
            int pageNumber = page ?? 1;
            return View("Products", filterProducts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }

    }
}