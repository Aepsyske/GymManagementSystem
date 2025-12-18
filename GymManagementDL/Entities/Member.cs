using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDL.Entities
{
    public class Member : Gymuser
    {
        public string? photo { get; set; } = null!;
        public HealthRecord HealthRecord { get; set; } = null!;
        public ICollection<Membership> Member_Memberships { get; set; } = null!;
        public ICollection<MemberSessions> Member_MemberSessions { get; set; } = null!;
    }
}
