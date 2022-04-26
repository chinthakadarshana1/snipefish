using MediatR;
using Microsoft.AspNetCore.Mvc;
using Snipefish.Application.Commands.UserAdventures;
using Snipefish.Application.Queries.UserAdventures;
using Snipefish.Application.Responses;
using Snipefish.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Snipefish.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAdventuresController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserAdventuresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/<UserAdventuresController>/5
        [HttpGet("{id}")]
        public async Task<UserAdventures> Get(string id)
        {
            return await _mediator.Send(new GetUserAdventuresByIdQuery() { UserId = id });
        }

        // POST api/<UserAdventuresController>
        [HttpPost]
        public async Task Post([FromBody] AddUserAdventuresCommand value)
        {
            await _mediator.Send(value);
        }

        // PUT api/<UserAdventuresController>/5
        [HttpPut("{id}")]
        public async Task Put(string id, [FromBody] UpdateUserAdventuresCommand value)
        {
            await _mediator.Send(value);
        }

        // DELETE api/<UserAdventuresController>/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _mediator.Send(new DeleteUserAdventuresCommand() { UserId = id });
        }

        // POST api/<UserAdventuresController>/UserLogin
        [HttpPost]
        [Route("UserLogin")]
        public async Task<UserAdventuresResponse> UserLogin([FromBody] LoginUserCommand value)
        {
            return await _mediator.Send(value);
        }
    }
}
