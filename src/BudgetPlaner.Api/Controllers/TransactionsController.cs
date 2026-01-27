using BudgetPlaner.Application.Transactions.Commands.CreateTransaction;
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

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateTransactionCommand command)
        {
            // Wir schicken den Befehl los. Wer ihn bearbeitet (Handler), ist dem Controller egal.
            var id = await _mediator.Send(command);

            // Wir geben die ID der neuen Transaktion zurück
            return Ok(id);
        }

    }
}
