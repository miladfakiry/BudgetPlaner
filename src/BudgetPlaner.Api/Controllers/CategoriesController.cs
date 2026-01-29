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
            // We send the command. Who handles it (handler) is not the controller's concern.
            var id = await _mediator.Send(command);
            // We give back the ID of the new category
            return Ok(id);
        }

    }
}
