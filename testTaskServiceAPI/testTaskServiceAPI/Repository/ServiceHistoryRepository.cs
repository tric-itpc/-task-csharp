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

        public IEnumerable<ServiceHistoryDomain> GetHistoryByName(string name) 
        {
            var history = set
                .Include(x => x.Service)
                .Where(x => x.Service.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            return history;
        }

        public IEnumerable<ServiceHistoryDomain> GetHistoryByDate(DateTime dateStart, DateTime dateEnd, string name)
        {
            var history = set
                .Include(x => x.Service)
                .Where(x => x.DateTime >= dateStart && x.DateTime <= dateEnd)
                .Where(x => x.Service.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            return history;
        }
    }
}
