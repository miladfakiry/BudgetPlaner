using BudgetPlaner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetPlaner.Infrastructure.Persistence.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            // Configure primary key
            builder.HasKey(t => t.Id);

            // Configure property constraints
            builder.Property(t => t.Description)
                .HasMaxLength(200)
                .IsRequired();

            // Critical for financial data: Use specific precision to avoid floating-point errors.
            // 18 digits in total, with 2 decimal places.
            builder.Property(t => t.Amount)
                .HasPrecision(18, 2);

            // Store enums as strings (e.g., "Income") instead of integers (1).
            // This improves readability when inspecting the raw database or debugging.
            builder.Property(t => t.Type)
                .HasConversion<string>();

            // Explicitly define the relationship configuration.
            // While EF Core conventions work well, explicit definition prevents ambiguity.
            builder.HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId);
        }
    }
}
