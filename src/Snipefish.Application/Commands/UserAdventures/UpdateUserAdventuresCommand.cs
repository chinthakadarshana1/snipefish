using MediatR;
using Snipefish.Application.Responses;
using Snipefish.Domain.Entities;

namespace Snipefish.Application.Commands.UserAdventures
{
    public class UpdateUserAdventuresCommand : IRequest<UserAdventuresResponse>
    {
        public string UserId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsFinished { get; set; }
        public Step? StartStep { get; set; }
    }
}
