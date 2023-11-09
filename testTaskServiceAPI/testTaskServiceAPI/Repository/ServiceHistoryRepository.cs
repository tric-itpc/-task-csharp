using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using testTaskServiceAPI.Models;
using testTaskServiceAPI.Models.Domain;

namespace testTaskServiceAPI.Repository
{
    public class ServiceHistoryRepository : Repository<ServiceHistoryDomain>
    {
        private Context db;
        private DbSet<ServiceHistoryDomain> set;
        public ServiceHistoryRepository(Context db) : base(db)
        {
            this.db = db;
            set = db.Set<ServiceHistoryDomain>();
        }

        public IEnumerable<ServiceHistoryDomain> GetAll() 
        {
            return set;
        }
    }
}
