using BudgetPlaner.Domain.Enums;
using MediatR;

namespace BudgetPlaner.Application.Transactions.Commands.UpdateTransaction;

public record UpdateTransactionCommand : IRequest
{
    public Guid Id { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
    public TransactionType Type { get; init; }
    public Guid CategoryId { get; init; }
}
