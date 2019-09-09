using Diyabetiz.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diyabetiz.MVC.WebUI.Models
{
    public class NewsVM
    {
        public News  news { get; set; }
        public string title { get; set; }
        public string shortDescription { get; set; }
        public string imageUrl { get; set; }
        public DateTime? createdDate { get; set; }
      
    }
}