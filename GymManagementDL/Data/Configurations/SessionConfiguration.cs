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
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("CapacityCheck", "Capacity BETWEEN 1 AND 25");
                tb.HasCheckConstraint("DateCheck", "EndDate > StartDate");
            });

            builder.HasOne(s => s.Category).WithMany(c => c.Sessions).HasForeignKey(s => s.CategoryID);
            builder.HasOne(s => s.Trainer).WithMany(t => t.Sessions).HasForeignKey(s => s.TrainerID);
        }
    }
}
