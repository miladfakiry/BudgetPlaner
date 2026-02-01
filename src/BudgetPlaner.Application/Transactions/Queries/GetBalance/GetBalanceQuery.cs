using BudgetPlaner.Application.Common.Interfaces;
using BudgetPlaner.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlaner.Application.Transactions.Queries.GetBalance;

public record GetBalanceQuery : IRequest<BalanceDto>
{

    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, BalanceDto>
    {
        private readonly IApplicationDbContext _context;

        public GetBalanceQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BalanceDto> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            // Testing something new here: We are using EF Core to calculate sums directly in the database.
            // Should be more efficient than pulling all data into memory.
            // Let's see how it performs in real-world scenarios. :D
            var income = await _context.Transactions
                .Where(t => t.Type == TransactionType.Income)
                .SumAsync(t => t.Amount, cancellationToken);

            var expense = await _context.Transactions
                .Where(t => t.Type == TransactionType.Expense)
                .SumAsync(t => t.Amount, cancellationToken);

            var balance = income - expense;

            return new BalanceDto(income, expense, balance);
        }
    }
}