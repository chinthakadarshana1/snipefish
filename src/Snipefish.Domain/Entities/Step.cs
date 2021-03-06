using System.Text.Json.Serialization;

namespace Snipefish.Domain.Entities
{
    public class Step : EntityBase
    {
        public string StepId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsSelected { get; set; }
        public List<Step>? SubSteps { get; set; }
    }
}
