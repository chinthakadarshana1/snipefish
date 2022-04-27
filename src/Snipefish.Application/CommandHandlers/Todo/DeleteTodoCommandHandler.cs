using MediatR;
using Snipefish.Application.Commands.Todo;
using Snipefish.Application.Mapping;
using Snipefish.Domain.Repositories.Command;

namespace Snipefish.Application.CommandHandlers.Todo
{
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand>
    {
        private readonly ITodoCommandRepository _todoCommandRepository;

        public DeleteTodoCommandHandler(ITodoCommandRepository todoCommandRepository)
        {
            _todoCommandRepository = todoCommandRepository;
        }


        public async Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todoEntity = MappingBootstraper.Mapper.Map<Domain.Entities.Todo>(request);

            await _todoCommandRepository.DeleteAsync(todoEntity, cancellationToken);

            return Unit.Value;
        }
    }
}
