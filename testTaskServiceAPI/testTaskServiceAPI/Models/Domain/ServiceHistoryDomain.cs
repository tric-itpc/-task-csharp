namespace testTaskServiceAPI.Models.Domain
{
    public class ServiceHistoryDomain
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public ServiceDomain Service {get; set;}
        public State? StateOld { get; set; }
        public State StateNew { get; set; }
        public DateTime DateTime { get; set; }

        public ServiceHistoryDomain() { }
        public ServiceHistoryDomain(ServiceDomain serviceOld, ServiceDomain serviceNew)
        {
            ServiceId = serviceOld.Id;
            StateOld = serviceOld.State;
            StateNew = serviceNew.State;
            DateTime = DateTime.Now;
        }

        public ServiceHistoryDomain(ServiceDomain service)
        {
            ServiceId = service.Id;
            StateNew = service.State;
            DateTime = DateTime.Now;
        }
    }
}
