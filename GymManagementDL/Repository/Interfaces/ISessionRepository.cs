using GymManagementDL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDL.Repository.Interfaces
{
    public interface ISessionRepository
    {
        IEnumerable<Session> GetAllSessionWithTrainerAndCategory();
        int GetCountOfBookedSlot(int SessionID);
    }
}
