using GymManagementDL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDL.Data.Configurations
{
    internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : Gymuser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(m => m.Name).HasColumnType("varchar").HasMaxLength(50); 
            builder.Property(m => m.Email).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(m => m.Phone).HasColumnType("varchar").HasMaxLength(11);

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("GymUserEmailCheck", "Email like '_%@_%._%'");
            });

            builder.HasIndex(m => m.Email).IsUnique();
            builder.HasIndex(m => m.Phone).IsUnique();

            builder.OwnsOne(m => m.Address, addressBuilder =>
            {
                addressBuilder.Property(a => a.Street).HasColumnType("varchar").HasMaxLength(30);
                addressBuilder.Property(a => a.City).HasColumnType("varchar").HasMaxLength(30);
                addressBuilder.Property(a => a.BuildNo).HasColumnType("varchar");

            });

        }
    }
}
