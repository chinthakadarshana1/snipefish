namespace Snipefish.Domain.Entities
{
    public class Step : EntityBase
    {
        public string StepId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsSelected { get; set; }
        public List<Step> SubSteps => new List<Step>();
    }
}
