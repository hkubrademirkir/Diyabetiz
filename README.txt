Projeyi �alistirmak i�in-->

1) DiyabetizSolution.sln - �alistirin

2) Diyabetiz.DAL -> DbContext -> DiyabetizDbContext.cs 
   connectionString Degistirin.

3) Diyabetiz.DAL -> Migrations klas�r�n� silin
 
4) Package Man Console ile Diyabetiz.DAL projesi i�in;
         enable-migrations komutunu �alistirin
         Olusan configuration.cs s�yle olmali ->  AutomaticMigrationsEnabled = true;  
         update-database komutunu �alistirin

5) Diyabetiz.MVC.WebUI -> Web.config dosyasinda connectionString i degistirin. 

6) Package Man Console ile Diyabetiz.MVC.WebUI projesi i�in;
         enable-migrations komutunu �alistirin
         Olusan configuration.cs s�yle olmali ->  AutomaticMigrationsEnabled = true;  
         update-database komutunu �alistirin
           
5) DiyabetizDb.sql dosyasini ->  
   Microsoft Sql Server Management Studio ile execute ederek verileri alin
    

admin ve web girisi ortaktir y�nlendirmeler farkli-

admin sifre= 123456789
      email= hkd@gmail.com



