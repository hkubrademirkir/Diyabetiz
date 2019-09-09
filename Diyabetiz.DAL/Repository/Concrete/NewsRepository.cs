using Diyabetiz.DAL.DbContexts;
using Diyabetiz.DAL.Repository.Abstract;
using Diyabetiz.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diyabetiz.DAL.Repository.Concrete
{
    public class NewsRepository : GenericRepository<News,int>, INewsRepository
    {
        
        public NewsRepository(DiyabetizDbContext context) : base(context)
        {
        }
    }
}

//Ekstra metodlara ihtiyaç duyulması halinde tek bir GenericRepo yerine bu şekilde genişletilebilir.
//INewsRepo ya eklenecek yeni metodlar burada ımplement edilir. 
