using api_back_end.Context;
using Microsoft.EntityFrameworkCore;

namespace back_end_api.Repository.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly FlightsDbContext context;
        public GenericRepository(FlightsDbContext context)
        {
            this.context = context;
        }

        public async Task Add(T entity) => await context.Set<T>().AddAsync(entity);
        public void Delete(T entity) => context.Set<T>().Remove(entity);
        public async Task<T?> Get(int id) => await context.Set<T>().FindAsync(id);
        public async Task<IEnumerable<T>> GetAll() => await context.Set<T>().ToListAsync();
        public async Task SaveChanges() => await context.SaveChangesAsync();
        public void Update(T entity) => context.Set<T>().Update(entity);
    }
}
