using ServiceAnalyzer.Enums;

namespace ServiceAnalyzer.DTO
{
    public class ActualServiceStatusDTO
    {
        public string Name { get; set; }
        public StatusService Status { get; set; }

        public ActualServiceStatusDTO() { }
    }
}
