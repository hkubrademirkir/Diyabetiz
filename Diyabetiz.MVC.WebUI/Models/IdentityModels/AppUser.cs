using Microsoft.AspNet.Identity.EntityFramework;

namespace Diyabetiz.MVC.WebUI.Models.IdentityModels
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }


    }
}