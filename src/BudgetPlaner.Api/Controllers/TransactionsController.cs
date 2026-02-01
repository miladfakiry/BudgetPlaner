using BudgetPlaner.Application.Transactions.Commands.CreateTransaction;
using BudgetPlaner.Application.Transactions.Commands.DeleteTransaction;
using BudgetPlaner.Application.Transactions.Commands.UpdateTransaction;
using BudgetPlaner.Application.Transactions.Queries.GetBalance;
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

        [HttpGet("balance")]
        public async Task<ActionResult<BalanceDto>> GetBalance()
        {
            var result = await _mediator.Send(new GetBalanceQuery());
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateTransactionCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID in URL passt nicht zur ID im Body.");
            }
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteTransactionCommand(id));
            return NoContent();
        }

    }
}
