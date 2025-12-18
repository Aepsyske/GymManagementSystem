using GymManagementDL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDL.Data.Context
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=GymManagement;Trusted_Connection=True;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<HealthRecord> healthRecords { get; set; }
        public DbSet<Trainer> trainers { get; set; }
        public DbSet<Plan> plans { get; set; }
        public DbSet<Session> sessions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Membership> memberships { get; set; }
        public DbSet<MemberSessions> memberSessions { get; set; }
    }
}
