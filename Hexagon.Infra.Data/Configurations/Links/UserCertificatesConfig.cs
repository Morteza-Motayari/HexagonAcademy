using Hexagon.Domain.Models.Links;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hexagon.Infra.Data.Configurations.Common
{
    public class UserCertificatesConfig : IEntityTypeConfiguration<UserCertificates>
    {
        public void Configure(EntityTypeBuilder<UserCertificates> builder)
        {
            builder.Property(g => g.PlaceOftake).HasMaxLength(150).IsRequired();           
        }
    }
}
