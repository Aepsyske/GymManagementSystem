using GymManagementDL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDL.Entities;

namespace GymManagementDL.Repository.Interfaces
{
    public interface IPlanRepository
    {
        IEnumerable<Plan> GetAll();
        Plan? GetByID(int id);
        int Update(Plan plan);
        
    }
}
