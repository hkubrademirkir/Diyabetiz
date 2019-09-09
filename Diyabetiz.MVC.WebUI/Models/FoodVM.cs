using Diyabetiz.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diyabetiz.MVC.WebUI.Models
{
    public class FoodVM
    {
        public FoodVM()
        {
            Foods = new List<Food>();
        }
        public Food Food { get; set; }
        public IEnumerable<Food> Foods { get; set; }
    }
}