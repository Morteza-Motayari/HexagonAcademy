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
    public class GymConfig : IEntityTypeConfiguration<Gym>
    {
        public void Configure(EntityTypeBuilder<Gym> builder)
        {
            builder.Property(g => g.Name).IsRequired()
                .HasMaxLength(150);
            builder.Property(g => g.Slug).IsRequired()
                .HasMaxLength(230);
            builder.Property(g => g.Address)
                .HasMaxLength(700);
            
        }
    }
}
