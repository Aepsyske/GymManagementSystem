using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDL.Entities
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public DateTime MadeAt {  get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
