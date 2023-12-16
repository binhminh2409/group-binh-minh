using EcommerceWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace EcommerceWeb.Controllers
{
    public class CustomerController : Controller
    {
        private readonly EcommerceWebDbContext _db;

        public CustomerController(EcommerceWebDbContext db)
        {
            _db = db;
        }


        private readonly IWebHostEnvironment _env;

        public CustomerController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View(_db.Customers.ToList());
        }

        // CREATE: Customer
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CustomerVM cvm)
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
                    return PartialView("_Success");
                }
            }
            return PartialView("_Error");
        }


        // EDIT: Customer
        public ActionResult Edit(int id)
        {
            Customer cus = _db.Customers.Find(id);

            CustomerVM cvm = new CustomerVM
            {
                CustomerID = cus.CustomerID,
                First_Name = cus.First_Name,
                Last_Name = cus.Last_Name,
                UserName = cus.UserName,
                Password = cus.Password,
                Gender = cus.Gender,
                DateofBirth = cus.DateofBirth,
                Country = cus.Country,
                City = cus.City,
                PostalCode = cus.PostalCode,
                Email = cus.Email,
                Phone = cus.Phone,
                Address = cus.Address,
                PicturePath = cus.PicturePath,
                status = cus.status,
                LastLogin = cus.LastLogin,
                Created = cus.Created,
                Notes = cus.Notes

            };
            return View(cvm);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CustomerVM cvm)
        {

            if (ModelState.IsValid)
            {
                string filePath = cvm.PicturePath;

                if (cvm.Picture != null)
                {
                    filePath = Path.Combine(_env.WebRootPath, "Images", Guid.NewGuid().ToString() + Path.GetExtension(cvm.Picture.FileName));
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
                    _db.Entry(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
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
                    _db.Entry(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return PartialView(cvm);
        }

        // DITELS: Customer

        public ActionResult Details(int id)
        {
            Customer cus = _db.Customers.Find(id);

            CustomerVM cvm = new CustomerVM
            {
                CustomerID = cus.CustomerID,
                First_Name = cus.First_Name,
                Last_Name = cus.Last_Name,
                UserName = cus.UserName,
                Password = cus.Password,
                Gender = cus.Gender,
                DateofBirth = cus.DateofBirth,
                Country = cus.Country,
                City = cus.City,
                PostalCode = cus.PostalCode,
                Email = cus.Email,
                Phone = cus.Phone,
                Address = cus.Address,
                PicturePath = cus.PicturePath,
                status = cus.status,
                LastLogin = cus.LastLogin,
                Created = cus.Created,
                Notes = cus.Notes

            };
            return View(cvm);
        }

        [HttpPost]
        public async Task<ActionResult> Details(CustomerVM cvm)
        {

            if (ModelState.IsValid)
            {
                string filePath = cvm.PicturePath;
                if (cvm.Picture != null)
                {
                    filePath = Path.Combine(_env.WebRootPath, "Images", Guid.NewGuid().ToString() + Path.GetExtension(cvm.Picture.FileName));
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
                    _db.Entry(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
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
                    _db.Entry(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return PartialView(cvm);
        }

        // DELETE: Customer


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Customer customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }




        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            Customer customer = _db.Customers.Find(id);
            string file_name = customer.PicturePath;
            string path = Path.Combine(_env.WebRootPath, file_name);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            _db.Customers.Remove(customer);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}