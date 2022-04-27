using MediatR;
using Snipefish.Application.Responses;

namespace Snipefish.Application.Commands.Todo
{
    public class AddTodoCommand : IRequest<TodoResponse>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
