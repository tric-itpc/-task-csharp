using ServiceAnalyzer.DTO;
using ServiceAnalyzer.Enums;

namespace ServiceAnalyzer.Model
{
    public class ServiceInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public StatusService Status { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public ServiceInfo() { }

        public ServiceInfo(string name, StatusService status = StatusService.NotActive, string description = "")
        {
            Name = name;
            Status = status;
            Description = description;
            DateTime = DateTime.Now;
        }

        public ServiceInfo(ServiceInfoDTO info)
        {
            Name = info.Name;
            Status = info.Status;
            Description = info.Description;
            DateTime = DateTime.Now;
        }

        public ServiceInfoDTO ToServiceInfoDTO()
        {
            var item = new ServiceInfoDTO
            {
                Name = this.Name,
                Status = this.Status,
                Description = this.Description,
                DateTime = this.DateTime
            };

            return item;
        }

        public ActualServiceStatusDTO ToActualServiceStatusDTO()
        {
            var item = new ActualServiceStatusDTO
            {
                Name = this.Name,
                Status = this.Status,
            };

            return item;
        }
    }
}
