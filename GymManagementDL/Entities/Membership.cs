using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDL.Entities
{
    public class Membership : BaseEntity
    {
        public int MemberID { get; set; }
        public Member Member { get; set; } = null!;
        public int PlanID { get; set; }
        public Plan Plan { get; set; } = null!;
        public string Status
        {
            get
            {
                if (ExpiredAt > DateTime.Now) return "Expired";
                else return "Active";
            }
        }
    }
}
