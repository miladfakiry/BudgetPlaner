using MediatR;

namespace BudgetPlaner.Application.Transactions.Commands.DeleteTransaction
{
    public record DeleteTransactionCommand(Guid Id) : IRequest;

}
