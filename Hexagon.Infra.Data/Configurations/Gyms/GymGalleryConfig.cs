using Hexagon.Domain.Models.Gyms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Infra.Data.Configurations.Gyms
{
    public class GymGalleryConfig : IEntityTypeConfiguration<GymGallery>
    {
        public void Configure(EntityTypeBuilder<GymGallery> builder)
        {
            builder.Property(g => g.ImageTitle).IsRequired()
                .HasMaxLength(100);
            builder.Property(g => g.CreatedBy).HasMaxLength(150);
        }
    }
}
