using Microsoft.EntityFrameworkCore;
using BitirmeProjesiLiman.Core.Repositories;
using BitirmeProjesiLiman.Data.EF.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Data.EF.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly BitirmeProjesiLimanDbContext _context;

        public Repository(BitirmeProjesiLimanDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
