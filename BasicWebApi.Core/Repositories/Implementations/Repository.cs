using BasicWebApi.Core.Interfaces;
using BasicWebApi.Domain.Entities;
using BasicWebApi.Persistance;
using Microsoft.EntityFrameworkCore;

namespace BasicWebApi.Core.Services
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public virtual async Task<int> Add(T entity)
        {
           await _dbSet.AddAsync(entity);
           await _context.SaveChangesAsync();
           return entity.Id;
        }

        public virtual async Task<int> Delete(int id)
        {
            _dbSet.Remove(_dbSet.Find(id)!);
            await _context.SaveChangesAsync();

            return id;
        }

        public virtual async Task<int> Update(T entity)
        {

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<IEnumerable<T>> GetAllPageable(int take, int skip)
        {
            return await _dbSet
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
        }
    }
}
