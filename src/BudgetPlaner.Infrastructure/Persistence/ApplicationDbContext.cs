using BudgetPlaner.Domain.Entities;
using BudgetPlaner.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BudgetPlaner.Infrastructure.Persistence
{ 
    public class ApplicationDbContext : DbContext, IApplicationDbContext
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