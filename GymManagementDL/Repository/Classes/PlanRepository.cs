using GymManagementDL.Data.Context;
using GymManagementDL.Entities;
using GymManagementDL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDL.Repository.Classes
{
    public class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _dbContext;
        public PlanRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Plan> GetAll()
        {
            throw new NotImplementedException();
        }

        public Plan? GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Plan plan)
        {
            throw new NotImplementedException();
        }
    }
}
