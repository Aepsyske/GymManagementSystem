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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new(); 
        public UnitOfWork(GymDbContext dbContext, ISessionRepository sessionRepository) 
        {
          _dbContext = dbContext;
          this.sessionRepository = sessionRepository;
        }

        public ISessionRepository sessionRepository { get; }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var entityType = typeof(TEntity);
            if (_repositories.TryGetValue(entityType, out var repository))
                return (IGenericRepository<TEntity>) repository;
            var NewRepo = new GenericRepository<TEntity>(_dbContext);
            _repositories[entityType] = NewRepo;
            return NewRepo;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
