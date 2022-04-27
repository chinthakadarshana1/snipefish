using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Snipefish.Application.Commands.UserAdventures;
using Snipefish.Application.Responses;
using Snipefish.Domain.Entities;
using Snipefish.WebClient.Configurations;

namespace Snipefish.WebClient
{
    public class SnipefishWebApi
    {
        private readonly SnipefishWebConfiguration _snipefishWebConfiguration;

        public SnipefishWebApi(SnipefishWebConfiguration snipefishWebConfiguration)
        {
            _snipefishWebConfiguration = snipefishWebConfiguration;
        }

        private RestClient GetRestClient(string apiMethod)
        {
            var options = new RestClientOptions($"{_snipefishWebConfiguration.SnipefishApiInternalUrl}{apiMethod}" )
            {
                ThrowOnAnyError = true,
                Timeout = 30000
            };
            return new RestClient(options);
        }

        public async Task<UserAdventuresResponse?> UserLogin(LoginUserCommand loginRequest, CancellationToken cancellationToken)
        {
            var request = new RestRequest()
                .AddJsonBody(loginRequest);
            return await GetRestClient("UserAdventures/UserLogin").PostAsync<UserAdventuresResponse>(request, cancellationToken);
        }

        public async Task<UserAdventures?> GetAdventuresByUserId(string userId, CancellationToken cancellationToken)
        {
            var request = new RestRequest($"UserAdventures/{userId}", Method.Get);
            return await GetRestClient("").GetAsync<UserAdventures>(request, cancellationToken);
        }
    }
}
