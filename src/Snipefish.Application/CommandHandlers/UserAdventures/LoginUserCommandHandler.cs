using MediatR;
using Snipefish.Application.Commands.UserAdventures;
using Snipefish.Application.Mapping;
using Snipefish.Application.Responses;
using Snipefish.Domain.Repositories.Command;
using Snipefish.Domain.Repositories.Query;

namespace Snipefish.Application.CommandHandlers.UserAdventures
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserAdventuresResponse>
    {
        private readonly IUserAdventureCommandRepository _userAdventuresCommandRepository;
        private readonly IUserAdventureQueryRepository _userAdventureQueryRepository;

        public LoginUserCommandHandler(IUserAdventureCommandRepository userAdventuresCommandRepository, IUserAdventureQueryRepository userAdventureQueryRepository)
        {
            _userAdventuresCommandRepository = userAdventuresCommandRepository;
            _userAdventureQueryRepository = userAdventureQueryRepository;
        }

        public async Task<UserAdventuresResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.UserAdventures? existingUser = await _userAdventureQueryRepository.GetUserByEmail(request.UserName, cancellationToken);

            if (existingUser == null)
            {
                existingUser = new Domain.Entities.UserAdventures();
                existingUser.UserName = request.UserName;
                existingUser.UserId = Guid.NewGuid().ToString();
                await _userAdventuresCommandRepository.AddAsync(existingUser, cancellationToken);
            }

            return MappingBootstraper.Mapper.Map<UserAdventuresResponse>(existingUser);
        }
    }
}
