using BudgetPlaner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BudgetPlaner.Infrastructure.Persistence
{
    // Falls du das Interface IApplicationDbContext schon implementiert hast, lass es stehen.
    public class ApplicationDbContext : DbContext
    {
        // Pass configuration options (e.g. ConnectionString) to the base DbContext class.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Clean Architecture Pattern:
            // Automatically scan the assembly for classes implementing IEntityTypeConfiguration.
            // This keeps the DbContext clean and decouples specific entity configuration.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}