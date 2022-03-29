using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : DbContext
    {
        private static string ConnectionString =
            "Server=barrel.itcollege.ee;User Id=student;Password=Student.Pass.1;Database=student_ditarr_Exam;MultipleActiveResultSets=true";

        public DbSet<Quiz> Quizzes { get; set; } = default!;
        public DbSet<Question> Questions { get; set; } = default!;
        public DbSet<Choice> Choices { get; set; } = default!;

        // not recommended - do not hardcode DB conf!
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}

// dotnet ef migrations add InitialCreate --project DAL --startup-project WebApp
// dotnet ef database update --project DAL --startup-project WebApp

//  dotnet aspnet-codegenerator razorpage -m TestTable -dc ApplicationDbContext -udl -outDir Pages/TestTable --referenceScriptLibraries
