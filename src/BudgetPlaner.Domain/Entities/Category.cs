using BudgetPlaner.Domain.Common;

namespace BudgetPlaner.Domain.Entities;

public class Category : BaseEntity
{
    // Enforce encapsulation: "private set" prevents external code from changing the state directly.
    public string Name { get; private set; } = string.Empty;

    // Navigation property for EF Core (One-to-Many relationship).
    public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

    // Parameterless constructor required by EF Core for object materialization (reflection).
    private Category() { }

    // Public constructor enforces valid state upon creation.
    public Category(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty.");

        Name = name;
    }
}