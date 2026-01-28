using BudgetPlaner.Application.Categories.Commands.CreateCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetPlaner.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateCategoryCommand command)
        {
            // Wir schicken den Befehl los. Wer ihn bearbeitet (Handler), ist dem Controller egal.
            var id = await _mediator.Send(command);
            // Wir geben die ID der neuen Kategorie zurück
            return Ok(id);
        }

    }
}
