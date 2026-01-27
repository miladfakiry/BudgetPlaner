using BudgetPlaner.Application.Common.Interfaces;
using MediatR;

namespace BudgetPlaner.Application.Transactions.Commands.CreateTransaction
{
    internal class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Transaction(
                request.Description,
                request.Amount,
                request.Date,
                request.Type,
                request.CategoryId
            );
            _context.Transactions.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
