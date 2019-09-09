using Diyabetiz.Entities.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Diyabetiz.Entities.Mappings
{
    public class FoodMapping : EntityTypeConfiguration<Food>
    {
        public FoodMapping()
        {
            Property(x => x.Name).HasMaxLength(50).IsRequired();
            Property(x => x.ImageURL).HasMaxLength(500).IsOptional();
            Property(x => x.Description).HasMaxLength(500).IsRequired();
            Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }
    }
}
