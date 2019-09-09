using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diyabetiz.MVC.WebUI.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        [Authorize(Roles = "Admin")]   
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult adminLogout()
        {
            var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Admin");

        }

    }
}