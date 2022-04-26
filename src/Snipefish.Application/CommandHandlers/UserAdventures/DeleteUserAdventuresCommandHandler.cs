using MediatR;
using Snipefish.Application.Commands.UserAdventures;
using Snipefish.Application.Mapping;
using Snipefish.Domain.Repositories.Command;

namespace Snipefish.Application.CommandHandlers.UserAdventures
{
    public class DeleteUserAdventuresCommandHandler : IRequestHandler<DeleteUserAdventuresCommand>
    {
        private readonly IUserAdventureCommandRepository _userAdventuresCommandRepository;

        public DeleteUserAdventuresCommandHandler(IUserAdventureCommandRepository userAdventuresCommandRepository)
        {
            _userAdventuresCommandRepository = userAdventuresCommandRepository;
        }


        public async Task<Unit> Handle(DeleteUserAdventuresCommand request, CancellationToken cancellationToken)
        {
            var userAdventuresEntity = MappingBootstraper.Mapper.Map<Domain.Entities.UserAdventures>(request);

            await _userAdventuresCommandRepository.DeleteAsync(userAdventuresEntity, cancellationToken);

            return Unit.Value;
        }
    }
}
