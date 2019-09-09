using Diyabetiz.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diyabetiz.Entities.Mappings
{
    public class ContactUsMapping  : EntityTypeConfiguration<ContactUs>
    {
        public ContactUsMapping()
        {
            Property(x => x.Name).HasMaxLength(35).IsRequired();
            Property(x => x.Email).HasMaxLength(40).IsRequired();
            Property(x => x.Message).HasMaxLength(1000).IsOptional();
            Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }
    }
}
