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
    internal class MemberSessionsConfiguration : IEntityTypeConfiguration<MemberSessions>
    {
        public void Configure(EntityTypeBuilder<MemberSessions> builder)
        {
            builder.HasKey(x => new { x.MemberID, x.SessionID });
            builder.Ignore(x => x.ID);
        }
    }
}
