using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazingCalculatorLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;


//*** NuGet Packages Required to be installed in AmazingCalculatorLibrary project ***
//dotnet add package Microsoft.EntityFrameworkCore
//dotnet add package Microsoft.EntityFrameworkCore.SqlServer
//dotnet add package Microsoft.EntityFrameworkCore.Tools


//we are setting this up in the Library to use Depedency Injection
// This helps with reusability for future projects
namespace AmazingCalculatorLibrary.Data
{
    public class FitnessDbContext : DbContext
    {
        public FitnessDbContext(DbContextOptions<FitnessDbContext> options) : base(options) { }
        public DbSet<UserProfiles> Users { get; set; }
        public DbSet<WorkoutSession> WorkoutSessions { get; set; }

    // Suggestions for future DbSet properties
    //public DbSet<Workout> Workouts { get; set; }
    //public DbSet<Exercise> Exercises { get; set; }
    //public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
    //public DbSet<WorkoutType> WorkoutTypes { get; set; }
    //public DbSet<WorkoutCategory> WorkoutCategories { get; set; }
    //public DbSet<WorkoutHistory> WorkoutHistories { get; set; }
    //public DbSet<ExerciseCategory> ExerciseCategories { get; set; }

}
}
