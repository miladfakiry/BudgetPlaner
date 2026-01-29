using BudgetPlaner.Application.Transactions.Commands.CreateTransaction;
using BudgetPlaner.Application.Transactions.Queries.GetTransactions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetPlaner.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetTransactionsQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateTransactionCommand command)
        {
            // We send the command. Who handles it (handler) is irrelevant to the controller.
            var id = await _mediator.Send(command);

            // We return the ID of the new transaction
            return Ok(id);
        }

    }
}
