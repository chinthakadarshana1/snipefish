using MediatR;
using Snipefish.Application.Mapping;
using Snipefish.Application.Queries.Todo;
using Snipefish.Domain.Repositories.Query;

namespace Snipefish.Application.QueryHandlers.Todo
{
    public class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, Domain.Entities.Todo?>
    {
        private readonly ITodoQueryRepository _todoQueryRepository;

        public GetTodoByIdQueryHandler(ITodoQueryRepository todoQueryRepository)
        {
            _todoQueryRepository = todoQueryRepository;
        }

        public async Task<Domain.Entities.Todo?> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
        {

            var todoEntity = MappingBootstraper.Mapper.Map<Domain.Entities.Todo>(request);

            return await _todoQueryRepository.GetById(todoEntity.Id!, cancellationToken);
        }
    }
}
