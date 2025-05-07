using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AmazingCalculatorLibrary.Models
{
    public class FitnessDbContextFactory : IDesignTimeDbContextFactory<FitnessDbContext>
    {
        public FitnessDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FitnessDbContext>();
            optionsBuilder.UseSqlServer("Server=tcp:ccad18.database.windows.net,1433;Initial Catalog=fitnessdb;Persist Security Info=False;User ID=c3repo;Password=Pa$$w0rd!!!!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            return new FitnessDbContext(optionsBuilder.Options);
        }
    }
}
