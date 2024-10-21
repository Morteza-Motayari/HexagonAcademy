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
    public class SportConfig : IEntityTypeConfiguration<Sport>
    {
        public void Configure(EntityTypeBuilder<Sport> builder)
        {
            builder.Property(g => g.Title).IsRequired()
                .HasMaxLength(150);
            builder.Property(g => g.Slug).IsRequired()
                .HasMaxLength(230);

        }
    }
}
