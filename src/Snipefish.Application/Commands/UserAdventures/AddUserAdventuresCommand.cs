using MediatR;
using Snipefish.Application.Responses;
using Snipefish.Domain.Entities;

namespace Snipefish.Application.Commands.UserAdventures
{
    public class AddUserAdventuresCommand : IRequest<UserAdventuresResponse>
    {
        public string UserId { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public List<Adventure> Adventures => new List<Adventure>();
    }
}
