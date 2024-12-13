using Hexagon.Domain.Models.KeyWords;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Infra.Data.Configurations.KeyWords
{
    public class KeyWordConfig : IEntityTypeConfiguration<KeyWord>
    {
        public void Configure(EntityTypeBuilder<KeyWord> builder)
        {
            builder.Property(g => g.Key).IsRequired()
                .HasMaxLength(30);
        }
    }
}
