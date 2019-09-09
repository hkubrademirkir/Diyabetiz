using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diyabetiz.MVC.WebUI.Models.Configurations;

namespace Diyabetiz.MVC.WebUI.Models.IdentityModels
{
    public class IdentityContext  : IdentityDbContext<AppUser>
    {
        public IdentityContext() : base("DiyabetizCnn")
        {
            //Database.Connection.ConnectionString = @"Server=ASUSPC\SQLEXPRESS;Database=DiyabetizDb;Integrated Security=true";
            System.Data.Entity.Database.SetInitializer(new IdentityContextInitializer());

        }
        public static IdentityContext Create()
        {
            return new IdentityContext();
        }

    }
}