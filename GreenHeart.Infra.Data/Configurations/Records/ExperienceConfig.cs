using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.Models.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Infra.Data.Configurations.Gyms
{
    public class ExperienceConfig : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.Property(g => g.Title).IsRequired()
                .HasMaxLength(150);
            builder.Property(g => g.Slug).IsRequired()
                .HasMaxLength(230);
            builder.Property(g => g.Detail).HasMaxLength(1000);
            builder.Property(g => g.Company).HasMaxLength(120);
            builder.Property(g => g.HowLong).HasMaxLength(120);
        }
    }
}
