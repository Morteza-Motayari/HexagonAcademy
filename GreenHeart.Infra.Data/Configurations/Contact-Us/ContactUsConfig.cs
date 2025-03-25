using GreenHeart.Domain.Models.Contact_Us;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Infra.Data.Configurations.Contact_Us
{
    public class ContactUsConfig : IEntityTypeConfiguration<ContactUs>
    {
        public void Configure(EntityTypeBuilder<ContactUs> builder)
        {
            builder.Property(g => g.FullName).IsRequired()
                .HasMaxLength(70);
            builder.Property(g => g.Email).IsRequired()
                .HasMaxLength(80);
            builder.Property(g => g.Subject).IsRequired()
                .HasMaxLength(50);
            builder.Property(g => g.Phone)
                .HasMaxLength(15);
            builder.Property(g => g.Description).IsRequired()
                .HasMaxLength(800);
            builder.Property(g => g.Answer)
                .HasMaxLength(800);
            builder.Property(g => g.IP)
                .HasMaxLength(60);
        }
    }
}
