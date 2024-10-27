using Hexagon.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Infra.Data.Configurations.Users
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(g => g.RoleTitle).IsRequired()
                .HasMaxLength(230);
            builder.Property(g => g.RoleName).IsRequired()
                .HasMaxLength(230);
        }
    }
}
