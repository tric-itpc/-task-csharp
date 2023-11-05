using MyAPI.Models;

namespace MyAPI.Data
{
    public interface IRepository
    {
        public void AddService(Service service);
        public List<PartOfService> GetServicesInfo();
        public void UpdateService(Service service);
        public Task<bool> SaveChangesAsync();

    }
}
