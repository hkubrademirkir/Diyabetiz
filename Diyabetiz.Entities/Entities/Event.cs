using System;

namespace Diyabetiz.Entities.Entities
{
    public class Event : BaseContent
    {
        public string Adress { get; set; }
        public DateTime EventDate { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }

    }
}
