using ServiceAnalyzer.Enums;

namespace ServiceAnalyzer.DTO
{
    public class ServiceInfoDTO
    {
        public string Name { get; set; }
        public StatusService Status { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public ServiceInfoDTO() { }

        public ServiceInfoDTO(string name, StatusService status = StatusService.NotActive, string description = "")
        {
            Name = name;
            Status = status;
            Description = description;
            DateTime = DateTime.Now;
        }
    }
}
