using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AmazingCalculatorLibrary.Models
{
    internal class FitnessDbContextFactory : IDesignTimeDbContextFactory<FitnessDbContext>
    {
        public FitnessDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FitnessDbContext>();
            optionsBuilder.UseSqlServer(args[0]);

            return new FitnessDbContext(optionsBuilder.Options);
        }
    }
}
