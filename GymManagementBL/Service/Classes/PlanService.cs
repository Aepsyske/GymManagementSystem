using GymManagementBL.Service.Interfaces;
using GymManagementBL.Service.ViewModels.PlanViewModel;
using GymManagementDL.Entities;
using GymManagementDL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBL.Service.Classes
{
    internal class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PlanService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public IEnumerable<PlanViewModel> GetAll()
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetAll();
            if (plan == null || !plan.Any()) return [];

            return plan.Select(p => new PlanViewModel
            {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                durationDays = p.DurationDays,
                isActive = p.isActive
            });
        }
        public PlanViewModel? GetPlanByID(int id)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetID(id);
            if (plan is null) return null;
            return new PlanViewModel
            {
                ID = plan.ID,
                Name = plan.Name,
                Price = plan.Price,
                Description = plan.Description,
                durationDays = plan.DurationDays,
                isActive = plan.isActive
            };
        }
        public UpdatePlanViewModel? GetPlanToUpdate(int id)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetID(id);
            if (plan is null || ActiveMembership(id)) return null;
            return new UpdatePlanViewModel()
            {
               Description = plan.Description,
               DurationDays = plan.DurationDays,
               PlanName = plan.Name,
               Price = plan.Price
            };
        }
        public bool TogglePlanStatus(int id)
        {
            var repo = _unitOfWork.GetRepository<Plan>();
            var plan = repo.GetID(id);
            if (plan is null || ActiveMembership(id)) return false;
            plan.isActive = plan.isActive ? false : true;
            plan.UpdatedAt = DateTime.Now;
            try
            {
                repo.Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdatePlan(int id, UpdatePlanViewModel updatePlanViewModel)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetID(id);
            if (plan is null || ActiveMembership(id)) return false;
            (plan.Description, plan.Price, plan.Description, plan.Name) =
            (updatePlanViewModel.Description, updatePlanViewModel.Price, updatePlanViewModel.Description, updatePlanViewModel.PlanName);
            _unitOfWork.GetRepository<Plan>().Update(plan);
            return _unitOfWork.SaveChanges() > 0;
        }

        #region HelperMethod
        private bool ActiveMembership(int id)
        {
            var active = _unitOfWork.GetRepository<Membership>().GetAll(x => x.ID == id && x.Status == "Active");
            return active.Any();
        }
        #endregion
    }
}
