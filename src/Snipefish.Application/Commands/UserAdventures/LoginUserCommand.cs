using MediatR;
using Snipefish.Application.Responses;

namespace Snipefish.Application.Commands.UserAdventures
{
    public class LoginUserCommand : IRequest<UserAdventuresResponse>
    {
        public string UserEmail { get; set; } = null!;
    }
}
