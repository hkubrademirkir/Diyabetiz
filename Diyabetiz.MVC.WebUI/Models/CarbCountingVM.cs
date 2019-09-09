using Diyabetiz.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diyabetiz.MVC.WebUI.Models
{
    public class CarbCountingVM
    {
        public CarbCountingVM()
        {
            carbCountings = new List<CarbohydrateCounting>();
        }
        public CarbohydrateCounting carbCounting { get; set; }
        public int ContentCount { get; set; }
        public IEnumerable<CarbohydrateCounting> carbCountings { get; set; }
    }
}