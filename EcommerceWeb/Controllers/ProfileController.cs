using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace EcommerceWeb.Models
{
    public class ProfileController : Controller
    {
        private readonly EcommerceWebDbContext _db;

        public ProfileController(EcommerceWebDbContext db)
        {
            _db = db;
        }

        // GET: Profile
        public ActionResult Index()
        {
            return View(_db.AdminEmployees.Find(TemData.EmpID));
        }
    }
}