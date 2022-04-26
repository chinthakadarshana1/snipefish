using MediatR;
using Snipefish.Application.Commands.UserAdventures;
using Snipefish.Application.Mapping;
using Snipefish.Application.Responses;
using Snipefish.Domain.Repositories.Command;

namespace Snipefish.Application.CommandHandlers.UserAdventures
{
    public class AddUserAdventuresCommandHandler : IRequestHandler<AddUserAdventuresCommand, UserAdventuresResponse>
    {
        private readonly IUserAdventureCommandRepository _userAdventuresCommandRepository;

        public AddUserAdventuresCommandHandler(IUserAdventureCommandRepository userAdventuresCommandRepository)
        {
            _userAdventuresCommandRepository = userAdventuresCommandRepository;
        }

        public async Task<UserAdventuresResponse> Handle(AddUserAdventuresCommand request, CancellationToken cancellationToken)
        {
            var userAdventures = MappingBootstraper.Mapper.Map<Domain.Entities.UserAdventures>(request);

            await _userAdventuresCommandRepository.AddAsync(userAdventures, cancellationToken);

            return MappingBootstraper.Mapper.Map<UserAdventuresResponse>(userAdventures);
        }
    }
}
