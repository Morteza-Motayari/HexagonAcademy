using GreenHeart.Domain.Models.Banners;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Infra.Data.Configurations.Banners
{
    public class BannerConfig : IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> builder)
        {
            builder.Property(b=>b.BannerName)
                .HasMaxLength(150).IsRequired();
            builder.Property(b => b.BannerUrl)
                .HasMaxLength(250).IsRequired();
        }
    }
}
