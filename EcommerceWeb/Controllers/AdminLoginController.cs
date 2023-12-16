using EcommerceWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    public class AdminLoginController : Controller
    {
        private readonly EcommerceWebDbContext _db;

        public AdminLoginController(EcommerceWebDbContext db)
        {
            _db = db;
        }


        // GET: AdminLogin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AdminLogin login)
        {
            if (ModelState.IsValid)
            {
                var model = (from m in _db.AdminLogins
                             where m.UserName == login.UserName && m.Password == login.Password
                             select m).Any();

                if (model)
                {
                    var loginInfo = _db.AdminLogins.Where(x => x.UserName == login.UserName && x.Password == login.Password).FirstOrDefault();

                    HttpContext.Session.SetString("username", loginInfo.UserName);
                    TemData.EmpID = loginInfo.EmpID;
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            return View();
        }
        // Logout Server Code
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "AdminLogin");
        }
    }
}