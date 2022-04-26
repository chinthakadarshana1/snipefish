using MediatR;
using Snipefish.Application.Mapping;
using Snipefish.Application.Queries.UserAdventures;
using Snipefish.Domain.Repositories.Query;

namespace Snipefish.Application.QueryHandlers.UserAdventures
{
    public class GetUserAdventuresByIdQueryHandler : IRequestHandler<GetUserAdventuresByIdQuery, Domain.Entities.UserAdventures?>
    {
        private readonly IUserAdventureQueryRepository _userAdventureQueryRepository;

        public GetUserAdventuresByIdQueryHandler(IUserAdventureQueryRepository userAdventureQueryRepository)
        {
            _userAdventureQueryRepository = userAdventureQueryRepository;
        }

        public async Task<Domain.Entities.UserAdventures?> Handle(GetUserAdventuresByIdQuery request, CancellationToken cancellationToken)
        {
            var userAdventure = MappingBootstraper.Mapper.Map<Domain.Entities.UserAdventures>(request);

            return await _userAdventureQueryRepository.GetById(userAdventure.UserId, cancellationToken);
        }
    }
}
