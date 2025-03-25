using GreenHeart.Domain.Models.Links;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenHeart.Infra.Data.Configurations.Common
{
    public class UserCertificatesConfig : IEntityTypeConfiguration<UserCertificates>
    {
        public void Configure(EntityTypeBuilder<UserCertificates> builder)
        {
            builder.Property(g => g.PlaceOftake).HasMaxLength(150);           
        }
    }
}
