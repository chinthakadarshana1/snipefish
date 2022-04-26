using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Snipefish.Application.Commands.UserAdventures;
using Snipefish.Application.Responses;
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
            var options = new RestClientOptions($"{_snipefishWebConfiguration.SnipefishApiUrl}{apiMethod}" )
            {
                ThrowOnAnyError = true,
                Timeout = 1000
            };
            return new RestClient(options);
        }

        public async Task<UserAdventuresResponse?> UserLogin(LoginUserCommand loginRequest, CancellationToken cancellationToken)
        {
            var request = new RestRequest()
                .AddJsonBody(loginRequest);
            return await GetRestClient("UserAdventures/UserLogin").PostAsync<UserAdventuresResponse>(request, cancellationToken);
        }
    }
}
