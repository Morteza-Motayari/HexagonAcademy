using GreenHeart.Domain.Models.Gyms;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenHeart.Domain.Models.Essays;

namespace GreenHeart.Infra.Data.Configurations.Essays
{
    public class EssayCategoryConfig : IEntityTypeConfiguration<EssayCategory>
    {
        public void Configure(EntityTypeBuilder<EssayCategory> builder)
        {
            builder.Property(g => g.Title).IsRequired()
                .HasMaxLength(200);
        }
    }
}
