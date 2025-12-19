using GymManagementBL.Service.Interfaces;
using GymManagementBL.Service.ViewModels.AnalyticsViewModel;
using GymManagementDL.Entities;
using GymManagementDL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBL.Service.Classes
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel GetAllAnalytics()
        {
            var Sessions = _unitOfWork.GetRepository<Session>().GetAll();
            return new AnalyticsViewModel
            {
                ActiveMembers = _unitOfWork.GetRepository<Membership>().GetAll(x => x.Status == "Active").Count(),
                TotalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count(),
                UpcomingSessions = Sessions.Where(x => x.StartDate < DateTime.Now).Count(),
                OngoingSessions = Sessions.Where(x => x.StartDate <= DateTime.Now).Count(),
                CompletedSessions = Sessions.Where(x => x.EndDate < DateTime.Now).Count()
            };
        }
    }
}
