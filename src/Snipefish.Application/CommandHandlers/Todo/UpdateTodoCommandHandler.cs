using MediatR;
using Snipefish.Application.Commands.Todo;
using Snipefish.Application.Mapping;
using Snipefish.Domain.Repositories.Command;

namespace Snipefish.Application.CommandHandlers.Todo
{
    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand>
    {
        private readonly ITodoCommandRepository _todoCommandRepository;

        public UpdateTodoCommandHandler(ITodoCommandRepository todoCommandRepository)
        {
            _todoCommandRepository = todoCommandRepository;
        }


        public async Task<Unit> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var todoEntity = MappingBootstraper.Mapper.Map<Domain.Entities.Todo>(request);

            await _todoCommandRepository.UpdateAsync(todoEntity, cancellationToken);

            return Unit.Value;
        }
    }
}
