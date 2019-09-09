using Diyabetiz.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diyabetiz.MVC.WebUI.Areas.Admin.Models
{
    public class EventVM
    {
        public EventVM()
        {
            Events = new List<Event>();
            EventVMs = new List<EventVM>();
        }
        public Event Event { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<EventVM> EventVMs { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }
    }
}