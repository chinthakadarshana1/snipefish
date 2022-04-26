using Snipefish.Domain.Entities;

namespace Snipefish.Application.Responses
{
    public class UserAdventuresResponse
    {
        public string UserId { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public List<Adventure> Adventures => new List<Adventure>();
    }
}
