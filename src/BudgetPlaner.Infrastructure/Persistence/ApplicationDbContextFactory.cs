using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BudgetPlaner.Infrastructure.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Fallback: Use environment variable if available (e.g. in Docker), 
            // otherwise use local development connection string.
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                                    ?? "Host=localhost;Port=5433;Database=budgetdb;Username=admin;Password=password123";

            optionsBuilder.UseNpgsql(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
