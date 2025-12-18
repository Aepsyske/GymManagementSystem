using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDL.Entities
{
    public class MemberSessions : BaseEntity
    {
        public int MemberID { get; set; }
        public Member Member { get; set; } = null!;
        public int SessionID { get; set; }
        public Session Session { get; set; } = null!;
        public bool IsAttended { get; set; }
    }
}
