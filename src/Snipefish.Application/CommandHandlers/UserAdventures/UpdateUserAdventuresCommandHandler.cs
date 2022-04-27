using MediatR;
using Snipefish.Application.Commands.UserAdventures;
using Snipefish.Application.Mapping;
using Snipefish.Application.Responses;
using Snipefish.Domain.Entities;
using Snipefish.Domain.Repositories.Command;
using Snipefish.Domain.Repositories.Query;

namespace Snipefish.Application.CommandHandlers.UserAdventures
{
    public class UpdateUserAdventuresCommandHandler : IRequestHandler<UpdateUserAdventuresCommand, UserAdventuresResponse>
    {
        private readonly IUserAdventureCommandRepository _userAdventuresCommandRepository;
        private readonly IUserAdventureQueryRepository _userAdventureQueryRepository;

        public UpdateUserAdventuresCommandHandler(IUserAdventureCommandRepository userAdventuresCommandRepository, IUserAdventureQueryRepository userAdventureQueryRepository)
        {
            _userAdventuresCommandRepository = userAdventuresCommandRepository;
            _userAdventureQueryRepository = userAdventureQueryRepository;
        }


        public async Task<UserAdventuresResponse> Handle(UpdateUserAdventuresCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.UserAdventures? existingUserAdventures = await _userAdventureQueryRepository.GetUserByUserId(request.UserId, cancellationToken);

            if (existingUserAdventures?.Adventures != null)
            {
                var existingAdventure =
                    existingUserAdventures.Adventures.FirstOrDefault(x => x.Name.ToLower() == request.Name.ToLower());

                if (existingAdventure != null)
                {
                    existingAdventure.IsFinished = request.IsFinished;
                    existingAdventure.StartStep = request.StartStep;

                    await _userAdventuresCommandRepository.UpdateAsync(existingUserAdventures, cancellationToken);
                }
            }

            return MappingBootstraper.Mapper.Map<UserAdventuresResponse>(existingUserAdventures);
        }
    }
}
