namespace Snipefish.Domain.Entities
{
    public class UserAdventures : EntityBase
    {
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public List<Adventure> Adventures => new List<Adventure>();
    }
}
