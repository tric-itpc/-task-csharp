using Microsoft.EntityFrameworkCore;
using testTaskServiceAPI.Models;
using testTaskServiceAPI.Models.Domain;

namespace testTaskServiceAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private Context db;
        private DbSet<T> set;
        public Repository(Context db) 
        {
            this.db = db;
            set = db.Set<T>();
        }

        public async void Add(T value)
        {
            set.Add(value);
            await db.SaveChangesAsync();
        }

        public async void Delete(T value)
        {
            set.Remove(value);
            await db.SaveChangesAsync();
        }

        public async void Update(T value)
        {
            set.Update(value);
            await db.SaveChangesAsync();
        }

        public T GetById(int id)
        {
            var value = set.Find(id);
            if (value == null)
                throw new Exception();
            return value;
        }
    }
}
