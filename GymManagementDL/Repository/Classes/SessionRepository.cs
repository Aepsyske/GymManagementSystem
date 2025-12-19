using GymManagementDL.Data.Context;
using GymManagementDL.Entities;
using GymManagementDL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDL.Repository.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext _dbContext;
        public SessionRepository(GymDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Session> GetAllSessionWithTrainerAndCategory()
        {
            return _dbContext.sessions.Include(x => x.Trainer).Include(x => x.Category).ToList();
        }

        public int GetCountOfBookedSlot(int SessionID)
        {
           return _dbContext.memberSessions.Where(x => x.SessionID == SessionID).Count();
        }

        public Session? GetSessionWithTrainerAndCategory(int ID)
        {
            return _dbContext.sessions.Include(x => x.Trainer).Include(x => x.Category).FirstOrDefault(x => x.ID == ID);
        }
    }
}
