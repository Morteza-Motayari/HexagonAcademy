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
    public class CertificateConfig : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.Property(g => g.Name).IsRequired()
                .HasMaxLength(150);
            builder.Property(g => g.Name)
                 .HasMaxLength(1000);

        }
    }
}
