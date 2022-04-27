namespace Snipefish.Domain.Entities
{
    public class Todo : EntityBase
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
