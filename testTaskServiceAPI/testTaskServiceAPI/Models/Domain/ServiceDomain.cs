namespace testTaskServiceAPI.Models.Domain
{
    public class ServiceDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public State State { get; set; }
        public string Description { get; set; }

        public ServiceDomain() { }

        public ServiceDomain(int id, string name, State state, string description)
        {
            Id = id;
            Name = name;
            State = state;
            Description = description;
        }

        public void Update(ServiceDomain service)
        {
            Name = service.Name;
            State = service.State;  
            Description = service.Description;
        }
    }
}
