using GymManagementDL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDL.Entities
{
    public class Trainer : Gymuser
    {
        public Speciality Speciality { get; set; }
        public ICollection<Session> Sessions { get; set; } = null!;
    }
}
