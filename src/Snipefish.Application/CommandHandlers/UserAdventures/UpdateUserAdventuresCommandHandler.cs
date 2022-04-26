using MediatR;
using Snipefish.Application.Commands.UserAdventures;
using Snipefish.Application.Mapping;
using Snipefish.Domain.Repositories.Command;

namespace Snipefish.Application.CommandHandlers.UserAdventures
{
    public class UpdateUserAdventuresCommandHandler : IRequestHandler<UpdateUserAdventuresCommand>
    {
        private readonly IUserAdventureCommandRepository _userAdventuresCommandRepository;

        public UpdateUserAdventuresCommandHandler(IUserAdventureCommandRepository userAdventuresCommandRepository)
        {
            _userAdventuresCommandRepository = userAdventuresCommandRepository;
        }


        public async Task<Unit> Handle(UpdateUserAdventuresCommand request, CancellationToken cancellationToken)
        {
            var userAdventuresEntity = MappingBootstraper.Mapper.Map<Domain.Entities.UserAdventures>(request);

            await _userAdventuresCommandRepository.UpdateAsync(userAdventuresEntity, cancellationToken);

            return Unit.Value;
        }
    }
}
