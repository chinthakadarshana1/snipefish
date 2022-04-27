using MediatR;
using Snipefish.Application.Responses;
using Snipefish.Domain.Entities;

namespace Snipefish.Application.Commands.UserAdventures
{
    public class UpdateUserAdventuresCommand : IRequest<UserAdventuresResponse>, IRequest<Unit>
    {
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public List<Adventure> Adventures => new List<Adventure>();
    }
}
