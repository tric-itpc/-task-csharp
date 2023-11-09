using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using testTaskServiceAPI.Models;
using testTaskServiceAPI.Models.Domain;

namespace testTaskServiceAPI.Repository
{
    public class ServiceRepository : Repository<ServiceDomain>
    {
        private Context db;
        private DbSet<ServiceDomain> set;
        public ServiceRepository(Context db) : base(db)
        {
            this.db = db;
            set = db.Set<ServiceDomain>();
        }

        public bool Contains(ServiceDomain service)
        {
            return set.Select(x => x.Name).Contains(service.Name);
        }

        public ServiceDomain GetByName(string name)
        {
            return set.Where(x => x.Name == name).Single();
        }

        public IEnumerable<ServiceDomain> GetAll()
        {
            return set;
        }
    }
}
