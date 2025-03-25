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
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(g => g.PhoneNumber).IsRequired()
                .HasMaxLength(13);
            builder.Property(g => g.Password).IsRequired()
                .HasMaxLength(230);
            builder.Property(g => g.FirstName).HasMaxLength(50);
            builder.Property(g => g.LastName).HasMaxLength(50);
            builder.Property(g => g.LastModifiedBy).HasMaxLength(50);
            builder.Property(g => g.city).HasMaxLength(50);
            builder.Property(g => g.email).HasMaxLength(100);
            builder.Property(g => g.Avatar).HasMaxLength(200);
            builder.Property(g => g.NationalCode).HasMaxLength(13);
            builder.Property(g => g.VerificationCode).HasMaxLength(25);
        }
    }
}
