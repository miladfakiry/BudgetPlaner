using Microsoft.EntityFrameworkCore;
using BudgetPlaner.Domain.Entities;

namespace BudgetPlaner.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Transaction> Transactions { get; }
        DbSet<Category> Categories { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
