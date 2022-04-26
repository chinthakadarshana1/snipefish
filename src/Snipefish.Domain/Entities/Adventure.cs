namespace Snipefish.Domain.Entities
{
    public class Adventure : EntityBase
    {
        public string UserId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
