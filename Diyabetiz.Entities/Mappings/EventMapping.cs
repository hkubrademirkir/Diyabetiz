using Diyabetiz.Entities.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Diyabetiz.Entities.Mappings
{
    public class EventMapping : EntityTypeConfiguration<Event>
    {
        public EventMapping()
        {
            Property(x => x.Title).HasMaxLength(250).IsRequired();
            Property(x => x.ImageURL).HasMaxLength(500).IsOptional();
            Property(x => x.Description).HasColumnType("varchar").IsRequired();
            Property(x => x.Adress).HasMaxLength(600).IsRequired();
            Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(x => x.ThemeColor).HasMaxLength(30).IsOptional();


        }
    }
}
