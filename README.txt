Projeyi Çalistirmak için-->

1) DiyabetizSolution.sln - Çalistirin

2) Diyabetiz.DAL -> DbContext -> DiyabetizDbContext.cs 
   connectionString Degistirin.

3) Diyabetiz.DAL -> Migrations klasörünü silin
 
4) Package Man Console ile Diyabetiz.DAL projesi için;
         enable-migrations komutunu çalistirin
         Olusan configuration.cs söyle olmali ->  AutomaticMigrationsEnabled = true;  
         update-database komutunu çalistirin

5) Diyabetiz.MVC.WebUI -> Web.config dosyasinda connectionString i degistirin. 

6) Package Man Console ile Diyabetiz.MVC.WebUI projesi için;
         enable-migrations komutunu çalistirin
         Olusan configuration.cs söyle olmali ->  AutomaticMigrationsEnabled = true;  
         update-database komutunu çalistirin
           
5) DiyabetizDb.sql dosyasini ->  
   Microsoft Sql Server Management Studio ile execute ederek verileri alin
    

admin ve web girisi ortaktir yönlendirmeler farkli-

admin sifre= 123456789
      email= hkd@gmail.com



