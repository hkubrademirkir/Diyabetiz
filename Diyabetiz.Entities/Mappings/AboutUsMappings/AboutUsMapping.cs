using Diyabetiz.Entities.Entities.AboutUs;
using System.Data.Entity.ModelConfiguration;

namespace Diyabetiz.Entities.Mappings.AboutUsMappings
{

    public class AboutUsMapping : EntityTypeConfiguration<AboutUs>
    {
        public AboutUsMapping()
        {
            Property(x => x.Description).HasMaxLength(500).IsRequired();
        }
    }

}
