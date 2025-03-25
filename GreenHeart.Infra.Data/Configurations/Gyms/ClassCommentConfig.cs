using GreenHeart.Domain.Models.Gyms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Infra.Data.Configurations.Gyms
{
    public class ClassCommentConfig : IEntityTypeConfiguration<ClassComment>
    {
        public void Configure(EntityTypeBuilder<ClassComment> builder)
        {
            builder.Property(g => g.Comment).IsRequired()
                .HasMaxLength(150);
        }
    }
}
