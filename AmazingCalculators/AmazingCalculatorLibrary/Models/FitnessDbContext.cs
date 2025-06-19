using Microsoft.EntityFrameworkCore;


namespace AmazingCalculatorLibrary.Models
{
    public class FitnessDbContext : DbContext
    {
        public FitnessDbContext(DbContextOptions<FitnessDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserProfiles> UserProfiles { get; set; }
        public DbSet<WorkoutSession> WorkoutSessions { get; set; }
        public DbSet<Demo> Demo{ get; set; }

    }
}
