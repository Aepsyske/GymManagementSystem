using GymManagementDL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDL.Data.Configurations
{
    internal class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Membership> builder)
        {
            builder.Property(m => m.MadeAt).HasColumnType("datetime2").HasDefaultValueSql("GETDATE()");
            builder.Property(m => m.ExpiredAt).HasColumnType("datetime2").HasDefaultValueSql("GETDATE()");
            builder.HasKey(x => new { x.MemberID, x.PlanID });
            builder.Ignore(x => x.ID);
        }
    }
}
