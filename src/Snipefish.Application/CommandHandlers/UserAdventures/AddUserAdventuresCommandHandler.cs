using MediatR;
using Snipefish.Application.Commands.UserAdventures;
using Snipefish.Application.Mapping;
using Snipefish.Application.Responses;
using Snipefish.Domain.Entities;
using Snipefish.Domain.Repositories.Command;
using Snipefish.Domain.Repositories.Query;

namespace Snipefish.Application.CommandHandlers.UserAdventures
{
    public class AddUserAdventuresCommandHandler : IRequestHandler<AddUserAdventuresCommand, UserAdventuresResponse>
    {
        private readonly IUserAdventureCommandRepository _userAdventuresCommandRepository;
        private readonly IUserAdventureQueryRepository _userAdventureQueryRepository;

        public AddUserAdventuresCommandHandler(IUserAdventureCommandRepository userAdventuresCommandRepository, IUserAdventureQueryRepository userAdventureQueryRepository)
        {
            _userAdventuresCommandRepository = userAdventuresCommandRepository;
            _userAdventureQueryRepository = userAdventureQueryRepository;
        }

        public async Task<UserAdventuresResponse> Handle(AddUserAdventuresCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.UserAdventures? existingUserAdventures = await _userAdventureQueryRepository.GetUserByUserId(request.UserId, cancellationToken);

            var newAdventure = MappingBootstraper.Mapper.Map<Adventure>(request);

            existingUserAdventures!.Adventures ??= new List<Adventure>();
            existingUserAdventures!.Adventures.Add(newAdventure);

            await _userAdventuresCommandRepository.UpdateAsync(existingUserAdventures, cancellationToken);

            return MappingBootstraper.Mapper.Map<UserAdventuresResponse>(existingUserAdventures);
        }
    }
}
