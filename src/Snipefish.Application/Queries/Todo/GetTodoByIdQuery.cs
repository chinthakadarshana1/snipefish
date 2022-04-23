using MediatR;

namespace Snipefish.Application.Queries.Todo
{
    public class GetTodoByIdQuery : IRequest<Domain.Entities.Todo>
    {
        public string Id { get; set; } = null!;
    }
}
