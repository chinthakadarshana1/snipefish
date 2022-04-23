using MediatR;
using Snipefish.Application.Responses;

namespace Snipefish.Application.Commands.Todo
{
    public class UpdateTodoCommand : IRequest<TodoResponse>, IRequest<Unit>
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
