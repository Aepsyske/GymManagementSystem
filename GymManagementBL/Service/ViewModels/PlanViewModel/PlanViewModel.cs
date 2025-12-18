using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBL.Service.ViewModels.PlanViewModel
{
    internal class PlanViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; } 
        public int durationDays { get; set; }
        public bool isActive { get; set; }
    }
}
