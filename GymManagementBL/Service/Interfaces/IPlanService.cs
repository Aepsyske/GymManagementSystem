using GymManagementBL.Service.ViewModels.PlanViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBL.Service.Interfaces
{
    internal interface IPlanService
    {
        IEnumerable<PlanViewModel> GetAll();
        PlanViewModel? GetPlanByID(int id);
        UpdatePlanViewModel? GetPlanToUpdate(int id);
        bool UpdatePlan(int id, UpdatePlanViewModel updatePlanViewModel);
        bool TogglePlanStatus(int id);
    }
}
