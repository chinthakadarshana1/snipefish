using MediatR;
using Snipefish.Application.Responses;

namespace Snipefish.Application.Commands.Todo
{
    public class DeleteTodoCommand : IRequest<TodoResponse>, IRequest<Unit>
    {
        public string Id { get; set; } = null!;
    }
}
