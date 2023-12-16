
using EcommerceWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    
    public class EmployeeController : Controller
    {
        private readonly EcommerceWebDbContext _db;

        public EmployeeController(EcommerceWebDbContext db)
        {
            _db = db;
        }

        private readonly IWebHostEnvironment _env;

        public EmployeeController(IWebHostEnvironment env)
        {
            _env = env;
        }


        // GET: Employee
        public ActionResult Index()
        {
            return View(_db.AdminEmployees.ToList());
        }

        // CREATE: Employee


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(EmployeeVM evm)
        {

            if (ModelState.IsValid)
            {
                if (evm.Picture !=null)
                {
                    string filePath = Path.Combine("Images", Guid.NewGuid().ToString() + Path.GetExtension(evm.Picture.FileName));
                    string fullPath = Path.Combine(_env.WebRootPath, filePath);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        evm.Picture.CopyTo(stream);
                    }

                    AdminEmployee e = new AdminEmployee
                    {
                        EmpID = evm.EmpID,
                        FirstName = evm.FirstName,
                        LastName = evm.LastName,
                        DateofBirth = evm.DateofBirth,
                        Gender = evm.Gender,
                        Email = evm.Email,
                        Address = evm.Address,
                        Phone = evm.Phone,
                        PicturePath = filePath
                    };
                    _db.AdminEmployees.Add(e);
                    _db.SaveChanges();
                    return PartialView("_Success");
                }
            }
            return PartialView("_Error");
        }

        // EDIT: Employee

        public ActionResult Edit(int id)
        {
            AdminEmployee emp = _db.AdminEmployees.Find(id);

            EmployeeVM evm = new EmployeeVM
            {
                EmpID = emp.EmpID,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                DateofBirth = emp.DateofBirth,
                Gender = emp.Gender,
                Email = emp.Email,
                Address = emp.Address,
                Phone = emp.Phone,
                PicturePath = emp.PicturePath

            };
            return View(evm);
        }

        [HttpPost]

        public ActionResult Edit(EmployeeVM evm)
        {

            if (ModelState.IsValid)
            {
                string filePath = evm.PicturePath;
                if (evm.Picture != null)
                {
                    filePath = Path.Combine("Images", Guid.NewGuid().ToString() + Path.GetExtension(evm.Picture.FileName));
                    string fullPath = Path.Combine(_env.WebRootPath, filePath);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        evm.Picture.CopyTo(stream);
                    }

                    AdminEmployee e = new AdminEmployee
                    {
                        EmpID = evm.EmpID,
                        FirstName = evm.FirstName,
                        LastName = evm.LastName,
                        DateofBirth = evm.DateofBirth,
                        Gender = evm.Gender,
                        Email = evm.Email,
                        Address = evm.Address,
                        Phone = evm.Phone,
                        PicturePath = filePath
                    };
                    _db.Entry(e).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    AdminEmployee e = new AdminEmployee
                    {
                        EmpID = evm.EmpID,
                        FirstName = evm.FirstName,
                        LastName = evm.LastName,
                        DateofBirth = evm.DateofBirth,
                        Gender = evm.Gender,
                        Email = evm.Email,
                        Address = evm.Address,
                        Phone = evm.Phone,
                        PicturePath = filePath
                    };
                    _db.Entry(e).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return PartialView(evm);
        }



        // VIEW: Employee Details

        public ActionResult Info(int id)
        {
            AdminEmployee emp = _db.AdminEmployees.Find(id);

            EmployeeVM evm = new EmployeeVM
            {
                EmpID = emp.EmpID,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                DateofBirth = emp.DateofBirth,
                Gender = emp.Gender,
                Email = emp.Email,
                Address = emp.Address,
                Phone = emp.Phone,
                PicturePath = emp.PicturePath

            };
            return View(evm);
        }

        [HttpPost]

        public ActionResult Info(EmployeeVM evm)
        {

            if (ModelState.IsValid)
            {
                string filePath = evm.PicturePath;
                if (evm.Picture != null)
                {
                    filePath = Path.Combine("Images", Guid.NewGuid().ToString() + Path.GetExtension(evm.Picture.FileName));
                    string fullPath = Path.Combine(_env.WebRootPath, filePath);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        evm.Picture.CopyTo(stream);
                    }

                    AdminEmployee e = new AdminEmployee
                    {
                        EmpID = evm.EmpID,
                        FirstName = evm.FirstName,
                        LastName = evm.LastName,
                        DateofBirth = evm.DateofBirth,
                        Gender = evm.Gender,
                        Email = evm.Email,
                        Address = evm.Address,
                        Phone = evm.Phone,
                        PicturePath = filePath
                    };
                    _db.Entry(e).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    AdminEmployee e = new AdminEmployee
                    {
                        EmpID = evm.EmpID,
                        FirstName = evm.FirstName,
                        LastName = evm.LastName,
                        DateofBirth = evm.DateofBirth,
                        Gender = evm.Gender,
                        Email = evm.Email,
                        Address = evm.Address,
                        Phone = evm.Phone,
                        PicturePath = filePath
                    };
                    _db.Entry(e).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return PartialView(evm);
        }


        // DELETE: Employee

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            AdminEmployee employee = _db.AdminEmployees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            AdminEmployee employee = _db.AdminEmployees.Find(id);
            string file_name = employee.PicturePath;
            string path = Path.Combine(_env.WebRootPath, file_name);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            _db.AdminEmployees.Remove(employee);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}