using Diyabetiz.MVC.WebUI.Models.AccountModels;
using Diyabetiz.MVC.WebUI.Models.IdentityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Diyabetiz.MVC.WebUI.Controllers
{
    public class AccountController : Controller
    {
        AppUser user;
        public static string memberName;
        public static string memberSurname;
        private UserManager<AppUser> userManager;
        private RoleManager<AppRole> roleManager;
        public AccountController()
        {
            IdentityContext db = new IdentityContext();
            UserStore<AppUser> userStore = new UserStore<AppUser>(db);
            userManager = new UserManager<AppUser>(userStore);
            RoleStore<AppRole> roleStore = new RoleStore<AppRole>(db);
            roleManager = new RoleManager<AppRole>(roleStore);
            user = new AppUser();
        }
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                AppUser user1 = userManager.FindByEmail(model.Email);//control by email
                if (user1 != null)
                {
                    user = userManager.Find(user1.UserName, model.Password);//Control by username with catched email
                }
                else
                {
                    TempData["NoteError"] = "E-Mail ya da sifre hatalı, lutfen tekrar deneyiniz.";
                }
                AppUser user2 = userManager.Find(model.Email, model.Password);//Control by username

                if (user != null) //email
                {
                    IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
                    ClaimsIdentity identity = userManager.CreateIdentity(user, "ApplicationCookie");
                    AuthenticationProperties authProps = new AuthenticationProperties();
                    authProps.IsPersistent = model.RememberMe;
                    authManager.SignIn(authProps, identity);
                    //Session["Name"] = user.Name;
                    //Session["Surname"] = user.Surname;
                    memberName = user.Name;
                    memberSurname = user.Surname;
                    Session["NameSurname"] = user.Name + " " + user.Surname;
                    Session["UserId"] = user.Id;
                    //var role = roleManager.FindByName("Admin");
                    //bool result = User.IsInRole(role.Name); //true
                    var role1 = roleManager.FindById(user.Roles.FirstOrDefault().RoleId.ToString());
                    if (role1.Name == "Admin")
                    {
                        return RedirectToAction("Index", "Admin/Admin");
                    }
                    else
                    {
                        return RedirectToAction("Home", "Home");
                    }
                }
                else if (user2 != null)//username
                {
                    IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
                    ClaimsIdentity identity = userManager.CreateIdentity(user2, "ApplicationCookie");
                    AuthenticationProperties authProps = new AuthenticationProperties();
                    authProps.IsPersistent = model.RememberMe;
                    authManager.SignIn(authProps, identity);
                    //Session["Name"] = user2.Name;
                    //Session["Surname"] = user2.Surname;
                    memberName = user2.Name;
                    memberSurname = user2.Surname;
                    Session["NameSurname"] = user2.Name + " " + user2.Surname;
                    Session["UserId"] = user2.Id;
                    var role1 = roleManager.FindById(user2.Roles.FirstOrDefault().RoleId.ToString());
                    if (role1.Name == "Admin")
                    {
                        return RedirectToAction("Index", "Admin/Admin");
                    }
                    else
                    {
                        return RedirectToAction("Home", "Home");
                    }
                 }
                else
                {
                    TempData["NoteError"] = "E-Mail ya da sifre hatalı, lutfen tekrar deneyiniz.";
                    
                }
               
            }
            else
            {
                TempData["NoteError"] = "E-Mail ya da sifre hatalı, lutfen tekrar deneyiniz.";
            }
            return View(model);
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(RegisterVM model)
        {
            if (ModelState.IsValid)
            {

                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.UserName = model.Username;
                IdentityResult iResult;
                if (!roleManager.RoleExists("User"))
                {
                    iResult = roleManager.Create(new AppRole("User"));
                }

                iResult = userManager.Create(user, model.Password);
                if (iResult.Succeeded)
                {
                    userManager.AddToRole(user.Id, "User");
                    return RedirectToAction("SignIn", "Account");
                }
                else
                {
                    TempData["NoteError"] = "Kullanıcı ekleme işleminde hata! Lutfen alanlara girdiğiniz bilgilerin doğruluğunu kontrol ediniz.";
                    //ModelState.AddModelError("RegisterUser", "Kullanıcı ekleme işleminde hata!");
                }
            }
            else
            {
                TempData["NoteError"] = "Kullanıcı ekleme işleminde hata! Lutfen alanlara girdiğiniz bilgilerin doğruluğunu kontrol ediniz.";
            }
            return View(model);



        }

        public ActionResult Logout()
        {
            var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut();
            
            return RedirectToAction("Home", "Home");
        }
    }
}