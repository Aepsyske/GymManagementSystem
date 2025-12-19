using AutoMapper;
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
        private readonly IMapper _mapper;
        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateSession(CreateSessionViewModel createSession)
        {
            try
            {
                if (!IsTrainerExist(createSession.TrainerId)) return false;
                if (!IsCategoryExist(createSession.CategoryId)) return false;
                if (!IsDateValid(createSession.StartDate, createSession.EndDate)) return false;
                if (createSession.Capacity > 25 || createSession.Capacity < 0) return false;
                var SessionEntity = _mapper.Map<CreateSessionViewModel, Session>(createSession);
                _unitOfWork.GetRepository<Session>().Add(SessionEntity);
                return _unitOfWork.SaveChanges() > 0;
            }catch(Exception ex)
            {
                Console.WriteLine("Create Session Failed " + ex);
                return false;
            }
        }
        public UpdateSessionViewModel? GetToUpdateSession(int ID)
        {
            var Session = _unitOfWork.GetRepository<Session>().GetID(ID);
            if(!IsSessionValid(Session!)) return null;
            return _mapper.Map<Session, UpdateSessionViewModel>(Session);
        }
        public bool UpdateSession(int ID, UpdateSessionViewModel updateSession)
        {
            try
            {
                var Session = _unitOfWork.GetRepository<Session>().GetID(ID);
                if (!IsSessionValid(Session!)) return false;
                if (!IsTrainerExist(updateSession.TrainerId)) return false;
                if(!IsDateValid(updateSession.StartDate, updateSession.EndDate)) return false;
                _mapper.Map(updateSession, Session);
                Session.UpdatedAt = DateTime.Now;
                _unitOfWork.GetRepository<Session>().Update(Session);
                return _unitOfWork.SaveChanges() > 0;
            }catch(Exception ex)
            {
                Console.WriteLine($"Update Session Failed {ex}"); return false;
            }
        }
        public bool RemoveSession(int ID)
        {
            try
            {
                var session = _unitOfWork.GetRepository<Session>().GetID(ID);
                if (!IsSessionAvaliable(session!)) return false;

                _unitOfWork.GetRepository<Session>().Delete(session);
                return _unitOfWork.SaveChanges() > 0;
            }catch(Exception ex)
            {
                Console.WriteLine($"Session Can't Be Removed {ex}"); return false;
            }
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var session = _unitOfWork.sessionRepository.GetAllSessionWithTrainerAndCategory();
            if (session.Any()) return [];
            var MappedSessions = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(session);
            foreach (var s in MappedSessions) s.AvaliableSlot = s.Capacity - _unitOfWork.sessionRepository.GetCountOfBookedSlot(s.ID);
            return MappedSessions;
        }

        public SessionViewModel? GetSessionByID(int ID)
        {
            var session = _unitOfWork.sessionRepository.GetSessionWithTrainerAndCategory(ID);
            if (session is null) return null;
            var MappedSessions = _mapper.Map<Session, SessionViewModel>(session);
            MappedSessions.AvaliableSlot = MappedSessions.Capacity - _unitOfWork.sessionRepository.GetCountOfBookedSlot(MappedSessions.ID);
            return MappedSessions;
        }

        #region Helper
        private bool IsTrainerExist(int ID)
        {
            return _unitOfWork.GetRepository<Trainer>().GetID(ID) is not null;
        }
        private bool IsCategoryExist(int ID)
        {
            return _unitOfWork.GetRepository<Category>().GetID(ID) is not null;
        }
        private bool IsDateValid(DateTime StartDate, DateTime EndDate)
        {
            return StartDate < EndDate;
        }
        private bool IsSessionValid(Session session)
        {
            if(session is null) return false;
            if (session.EndDate < DateTime.Now) return false;
            if (session.StartDate >= DateTime.Now) return false;
            var HasActive = _unitOfWork.sessionRepository.GetCountOfBookedSlot(session.ID) > 0;
            if (HasActive) return false;
            return true;
        }
        private bool IsSessionAvaliable(Session session)
        {
            if (session is null) return false;
            if (session.StartDate <= DateTime.Now && session.EndDate >= DateTime.Now) return false;
            if(session.StartDate >= DateTime.Now) return false;
            var ActiveBooking = _unitOfWork.sessionRepository.GetCountOfBookedSlot(session.ID) > 0;
            if (ActiveBooking) return false;
            return true;
        }
        #endregion
    }
}
