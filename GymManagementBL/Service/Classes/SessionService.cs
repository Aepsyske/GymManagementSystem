using GymManagementBL.Service.Interfaces;
using GymManagementBL.Service.ViewModels.SessionViewModel;
using GymManagementDL.Entities;
using GymManagementDL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBL.Service.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var session = _unitOfWork.sessionRepository.GetAllSessionWithTrainerAndCategory();
            if (session.Any()) return [];
            return session.Select(s => new SessionViewModel
            {
                ID = s.ID,
                Description = s.Description,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Capacity = s.Capacity,
                TrainerName = s.Trainer.Name,
                CategoryName = s.Category.CategoryName.ToString(),
                AvaliableSlot = s.Capacity - _unitOfWork.sessionRepository.GetCountOfBookedSlot(s.ID)
            });
        }
    }
}
