using MediatR;
using Snipefish.Application.Commands.Todo;
using Snipefish.Application.Mapping;
using Snipefish.Application.Responses;
using Snipefish.Domain.Repositories.Command;

namespace Snipefish.Application.CommandHandlers.Todo
{
    public class AddTodoCommandHandler : IRequestHandler<AddTodoCommand, TodoResponse>
    {
        private readonly ITodoCommandRepository _todoCommandRepository;

        public AddTodoCommandHandler(ITodoCommandRepository todoCommandRepository)
        {
            _todoCommandRepository = todoCommandRepository;
        }

        public async Task<TodoResponse> Handle(AddTodoCommand request, CancellationToken cancellationToken)
        {
            var todoEntity = MappingBootstraper.Mapper.Map<Domain.Entities.Todo>(request);

            await _todoCommandRepository.AddAsync(todoEntity, cancellationToken);

            return MappingBootstraper.Mapper.Map<TodoResponse>(todoEntity);
        }
    }
}
