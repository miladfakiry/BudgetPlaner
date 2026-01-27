using BudgetPlaner.Domain.Enums;
using MediatR;

namespace BudgetPlaner.Application.Transactions.Commands.CreateTransaction
{
    public record CreateTransactionCommand : IRequest<Guid>
    {
        public string Description { get; init; } = string.Empty;
        public decimal Amount { get; init; }
        public DateTime Date { get; init; }
        public TransactionType Type { get; init; }
        public Guid CategoryId { get; init; }
    }
}
