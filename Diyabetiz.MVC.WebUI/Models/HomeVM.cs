using Diyabetiz.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diyabetiz.MVC.WebUI.Models
{
    public class HomeVM
    {
        public HomeVM()
        {
            Foods = new List<Food>();
            newsVMList = new List<NewsVM>();
            newsList = new List<News>();
        }
        public Food Food { get; set; }
        public IEnumerable<Food> Foods { get; set; }
        public News News { get; set; }
        public string shortDescription { get; set; }
        public IEnumerable<NewsVM> newsVMList { get; set; }
        public IEnumerable<News> newsList { get; set; }
    }
}