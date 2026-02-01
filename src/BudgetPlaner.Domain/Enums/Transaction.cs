using BudgetPlaner.Domain.Common;
using BudgetPlaner.Domain.Enums;

namespace BudgetPlaner.Domain.Entities;

public class Transaction : BaseEntity
{
    public string Description { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public TransactionType Type { get; private set; }

    public Guid CategoryId { get; private set; }
    public Category? Category { get; private set; }

    // Private constructor required for EF Core.
    private Transaction() { }

    public Transaction(string description, decimal amount, DateTime date, TransactionType type, Guid categoryId)
    {
        // Guard Clauses: Ensure the entity is never created in an invalid state.
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description required.");

        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative. Use Type 'Expense' instead.");

        Description = description;
        Amount = amount;
        Date = date;
        Type = type;
        CategoryId = categoryId;
    }

    // Domain Logic / Behavior:
    // Instead of public setters (Anemic Model), we use semantic methods to mutate state.
    // This allows us to encapsulate validation logic and business rules.
    public void UpdateDetails(string description, decimal amount, DateTime date, TransactionType type, Guid categoryId)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative.");
        }
        // Potential business validation logic goes here...
        Description = description;
        Amount = amount;
        Date = date;
        Type = Type;
        CategoryId = categoryId;

        UpdateLastModified(); // Update audit trail from BaseEntity
    }
}