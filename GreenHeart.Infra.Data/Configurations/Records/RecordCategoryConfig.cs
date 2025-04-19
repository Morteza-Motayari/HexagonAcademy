using GreenHeart.Domain.Models.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Infra.Data.Configurations.Records
{
    public class RecordCategoryConfig : IEntityTypeConfiguration<RecordCategory>
    {
        public void Configure(EntityTypeBuilder<RecordCategory> builder)
        {
            builder.Property(b=>b.Title).IsRequired()
                .HasMaxLength(50);
        }
    }
}
