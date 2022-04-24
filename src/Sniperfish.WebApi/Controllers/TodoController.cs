using MediatR;
using Microsoft.AspNetCore.Mvc;
using Snipefish.Application.Commands.Todo;
using Snipefish.Application.Queries.Todo;
using Snipefish.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Snipefish.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {

        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public async Task<Todo> Get(string id)
        {
            return await _mediator.Send(new GetTodoByIdQuery() { Id = id });
        }

        // POST api/<TodoController>
        [HttpPost]
        public async Task Post([FromBody] AddTodoCommand value)
        {
            await _mediator.Send(value);
        }

        // PUT api/<TodoController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] UpdateTodoCommand value)
        {
            await _mediator.Send(value);
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _mediator.Send(new DeleteTodoCommand() { Id = id });
        }
    }
}
