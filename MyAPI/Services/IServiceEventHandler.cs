namespace MyAPI.Services
{
    public interface IServiceEventHandler
    {
        public Task<bool> CreateServices();
        public void StartServices();
    }
}
