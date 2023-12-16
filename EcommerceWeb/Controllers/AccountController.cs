using EcommerceWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EcommerceWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly EcommerceWebDbContext _db;

        public AccountController(EcommerceWebDbContext db)
        {
            _db = db;
        }

        private readonly IWebHostEnvironment _env;

        public AccountController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // GET: Account
        public ActionResult Index()
        {
            this.GetDefaultData();

            var usr = _db.Customers.Find(TempShpData.UserID);
            return View(usr);

        }


        //REGISTER CUSTOMER
        [HttpPost]
        public ActionResult Register(Customer cust)
        {
            if (ModelState.IsValid)
            {
                _db.Customers.Add(cust);
                _db.SaveChanges();

                HttpContext.Session.SetString("username", cust.UserName);
                TempShpData.UserID = GetUser(cust.UserName).CustomerID;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }



        //LOG IN
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection formColl)
        {
            string usrName = formColl["UserName"];
            string Pass = formColl["Password"];

            if (ModelState.IsValid)
            {
                var cust = (from m in _db.Customers
                            where (m.UserName == usrName && m.Password == Pass)
                            select m).SingleOrDefault();

                if (cust != null)
                {
                    TempShpData.UserID = cust.CustomerID;
                    HttpContext.Session.SetString("username", cust.UserName);
                    return RedirectToAction("Index", "Home");
                }

            }
            return View();
        }

        //LOG OUT
        public ActionResult Logout()
        {
            HttpContext.Session.SetString("username", null);
            TempShpData.UserID = 0;
            TempShpData.items = null;
            return RedirectToAction("Index", "Home");
        }



        public Customer GetUser(string _usrName)
        {
            var cust = (from c in _db.Customers
                        where c.UserName == _usrName
                        select c).FirstOrDefault();
            return cust;
        }

        //UPDATE CUSTOMER DATA
        [HttpPost]
        public ActionResult Update(Customer cust)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(cust).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
                HttpContext.Session.SetString("username", cust.UserName);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        // CREATE: Customer

        public ActionResult RegisterCustomer()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> RegisterCustomer(CustomerVM cvm)
        {
            if (ModelState.IsValid)
            {
                if (cvm.Picture != null)
                {
                    string filePath = Path.Combine(_env.WebRootPath, "Images", Guid.NewGuid().ToString() + Path.GetExtension(cvm.Picture.FileName));
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await cvm.Picture.CopyToAsync(fileStream);
                    }

                    Customer c = new Customer
                    {
                        CustomerID = cvm.CustomerID,
                        First_Name = cvm.First_Name,
                        Last_Name = cvm.Last_Name,
                        UserName = cvm.UserName,
                        Password = cvm.Password,
                        Gender = cvm.Gender,
                        DateofBirth = cvm.DateofBirth,
                        Country = cvm.Country,
                        City = cvm.City,
                        PostalCode = cvm.PostalCode,
                        Email = cvm.Email,
                        Phone = cvm.Phone,
                        Address = cvm.Address,
                        PicturePath = filePath,
                        status = cvm.status,
                        LastLogin = cvm.LastLogin,
                        Created = cvm.Created,
                        Notes = cvm.Notes
                    };
                    _db.Customers.Add(c);
                    _db.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }
            }
            return PartialView("_Error");
        }


    }
}