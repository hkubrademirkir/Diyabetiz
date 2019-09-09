using Diyabetiz.Entities.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Diyabetiz.Entities.Mappings
{
    public class CarbohydrateCountingMapping : EntityTypeConfiguration<CarbohydrateCounting>
    {
        public CarbohydrateCountingMapping()
        {
            Property(x => x.Title).HasMaxLength(250).IsRequired();
            Property(x => x.ImageURL).HasMaxLength(500).IsOptional();
            Property(x => x.Description).HasColumnType("varchar").IsRequired();
            Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            

        }
    }

}
