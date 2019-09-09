using Diyabetiz.Entities.Entities.AboutUs;
using System.Data.Entity.ModelConfiguration;

namespace Diyabetiz.Entities.Mappings.AboutUsMappings
{
    public class MissionMapping : EntityTypeConfiguration<Mission>
    {
        public MissionMapping()
        {
            Property(x => x.Description).HasMaxLength(1000).IsRequired();
        }

    }
}
