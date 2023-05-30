using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UmetnickaDela.Data;
using UmetnickaDela.Infrastructure.Interfaces;


namespace UmetnickaDela.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public Repository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public async Task<bool> Delete(T entity)
        {
            
            context.Set<T>().Remove(entity);
            
            var result = await context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> getById(int id)
        {
            var result = await context.Set<T>().FindAsync(id);
            if (result == null)
                return null;
            return result;

        }

        public async Task<bool> Update(T entity, int id)
        {
            var result = await context.Set<T>().FindAsync(id);  
            mapper.Map<T>(result); //?????
            var saveChange = await context.SaveChangesAsync();
            return saveChange > 0;

            
            
        }

        
    }
}
