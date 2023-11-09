using testTaskServiceAPI.Models.Domain;

namespace testTaskServiceAPI.Models.View
{
    public class ServiceView
    {
        public string Name { get; set; }
        public State State { get; set; }
        public string Description { get; set; }
    }
}
