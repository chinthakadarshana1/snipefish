using MediatR;

namespace Snipefish.Application.Queries.UserAdventures
{
    public class GetUserAdventuresByIdQuery : IRequest<Domain.Entities.UserAdventures>
    {
        public string UserId { get; set; } = null!;
    }
}
