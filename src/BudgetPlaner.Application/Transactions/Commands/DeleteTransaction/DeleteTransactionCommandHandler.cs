using BudgetPlaner.Application.Common.Interfaces;
using MediatR;

namespace BudgetPlaner.Application.Transactions.Commands.DeleteTransaction
{
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Transactions.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"Transaktion mit ID {request.Id} wurde nicht gefunden.");
            }

            _context.Transactions.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
