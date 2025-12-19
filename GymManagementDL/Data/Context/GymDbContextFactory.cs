using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GymManagementDL.Data.Context
{
    internal class GymDbContextFactory
        : IDesignTimeDbContextFactory<GymDbContext>
    {
        public GymDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<GymDbContext>()
                .UseSqlServer(
                    "Server=.;Database=GymManagement;Trusted_Connection=True;TrustServerCertificate=True")
                .Options;

            return new GymDbContext(options);
        }
    }
}
