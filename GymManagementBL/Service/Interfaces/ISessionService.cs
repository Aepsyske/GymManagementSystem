using GymManagementBL.Service.ViewModels.SessionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBL.Service.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionByID(int ID);
        bool CreateSession(CreateSessionViewModel createSession);
        UpdateSessionViewModel? GetToUpdateSession(int ID);
        bool UpdateSession(int ID, UpdateSessionViewModel updateSession);
        bool RemoveSession(int ID);
    }
}
