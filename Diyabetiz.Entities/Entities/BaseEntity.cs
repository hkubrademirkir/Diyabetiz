using System;

namespace Diyabetiz.Entities.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByID { get; set; }
        public bool? IsActive { get; set; }


    }
}
