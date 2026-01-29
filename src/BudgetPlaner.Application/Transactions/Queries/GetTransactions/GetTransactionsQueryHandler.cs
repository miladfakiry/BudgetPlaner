using BudgetPlaner.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlaner.Application.Transactions.Queries.GetTransactions
{
    public record GetTransactionsQuery : IRequest<List<TransactionDto>>;
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<TransactionDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetTransactionsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            // A. Load Data
            var entities = await _context.Transactions
                .AsNoTracking() // Performance-Tipp: Faster, because we are just reading
                .Include(t => t.Category)
                .OrderByDescending(t => t.Date)
                .ToListAsync(cancellationToken);

            // B. Mapping (Transforming in DTOs)
            var dtos = entities.Select(t => new TransactionDto(
                Id: t.Id,
                Description: t.Description,
                Amount: t.Amount,
                Date: t.Date,
                Type: t.Type.ToString(),
                CategoryName: t.Category?.Name ?? "Keine Kategorie"
            )).ToList();

            return dtos;
        }
    }
}
