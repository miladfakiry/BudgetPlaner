using BudgetPlaner.Application.Common.Interfaces;
using MediatR;

namespace BudgetPlaner.Application.Transactions.Commands.UpdateTransaction
{
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Transactions.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Transaction {request.Id} not found.");
            }

            entity.UpdateDetails(request.Description, request.Amount, request.Date, request.Type, request.CategoryId);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
