namespace testTaskServiceAPI.Models.Domain
{
    public class ServiceDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public State State { get; set; }
        public string Description { get; set; }

        public ServiceDomain() { }

        public void Update(ServiceDomain service)
        {
            Name = service.Name;
            State = service.State;  
            Description = service.Description;
        }
    }
}
