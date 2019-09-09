using Diyabetiz.Entities.Entities.AboutUs;
using System.Data.Entity.ModelConfiguration;

namespace Diyabetiz.Entities.Mappings.AboutUsMappings
{
    public class VisionMapping : EntityTypeConfiguration<Vision>
    {
        public VisionMapping()
        {
            Property(x => x.Description).HasMaxLength(1000).IsRequired();
        }
    }
}
