using testTaskServiceAPI.Models.Domain;

namespace testTaskServiceAPI.Models.View
{
    public class ServiceHistoryView
    {
        public string ServiceName { get; set; }
        public DateTime DateTime { get; set; }
        public State? StateOld { get; set; }
        public State StateNew { get; set; }
    }
}
