using Diyabetiz.Entities.Entities;
using Diyabetiz.Entities.Entities.AboutUs;
using Diyabetiz.Entities.Mappings;
using Diyabetiz.Entities.Mappings.AboutUsMappings;
using System.Data.Entity;

namespace Diyabetiz.DAL.DbContexts
{
    public class DiyabetizDbContext : DbContext
    {
        public DiyabetizDbContext()  : base()
        {
            Database.Connection.ConnectionString = @"Server=ASUSPC\SQLEXPRESS;Database=DiyabetizDb;Integrated Security=true";
        }

        #region Entities

        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Mission> Mission { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<Vision> Vision { get; set; }
        
        public DbSet<CarbohydrateCounting> CarbohydrateCounting { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<DiabetesEducation> DiabetesEducation { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<News> News { get; set; }



        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AboutUsMapping());
            modelBuilder.Configurations.Add(new MissionMapping());
            modelBuilder.Configurations.Add(new PlanMapping());
            modelBuilder.Configurations.Add(new VisionMapping());
          
            modelBuilder.Configurations.Add(new CarbohydrateCountingMapping());
            modelBuilder.Configurations.Add(new ContactUsMapping());
            modelBuilder.Configurations.Add(new DiabetesEducationMapping());
            modelBuilder.Configurations.Add(new EventMapping());
            modelBuilder.Configurations.Add(new FoodMapping());
            modelBuilder.Configurations.Add(new NewsMapping());
     


        }

    }
}
