using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Infra.Data.Configurations.Gyms
{
    public class StaffConfig : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.Property(g => g.Salary).IsRequired();
            builder.Property(g => g.Position).IsRequired()
                .HasMaxLength(230);            
        }
    }
}
