using Hexagon.Domain.Models.Wallets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Infra.Data.Configurations.Wallets
{
    public class WalletConfig : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.Property(w => w.IP)
                .HasMaxLength(30);
            builder.Property(w => w.OS)
                .HasMaxLength(150);
            builder.Property(w => w.RefId)
                .HasMaxLength(40);
            builder.Property(w => w.Description)
                .HasMaxLength(600);
            builder.Property(w => w.Authority)
                .HasMaxLength(200);
        }
    }
}
