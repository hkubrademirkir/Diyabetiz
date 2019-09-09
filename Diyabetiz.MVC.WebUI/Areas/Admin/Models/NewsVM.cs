using Diyabetiz.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diyabetiz.MVC.WebUI.Areas.Admin.Models
{
    public class NewsVM
    {
        public NewsVM()
        {
            newsList = new List<News>();
        }
        public News news { get; set; }
        public IEnumerable<News> newsList { get; set; }
    }
}