using MediatR;
using Snipefish.Application.Responses;

namespace Snipefish.Application.Commands.UserAdventures
{
    public class DeleteUserAdventuresCommand : IRequest<UserAdventuresResponse>, IRequest<Unit>
    {
        public string UserId { get; set; } = null!;
    }
}
