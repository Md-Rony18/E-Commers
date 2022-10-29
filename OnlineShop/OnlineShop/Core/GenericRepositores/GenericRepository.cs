using Microsoft.EntityFrameworkCore;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Core.IGenericRepositores;
using OnlineShop.Data;

namespace OnlineShop.Core.GenericRepositores
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        protected  ApplicationDbContext _context;
        protected ILogger _logger;
        protected DbSet<T> _table;
        public GenericRepository(ApplicationDbContext context,ILogger logger)
        {
            _context = context;
            _table = _context.Set<T>();
        }
        
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await _table.FindAsync(id);
        }

        public virtual async Task<bool> Add(T entity)
        {
            await _table.AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> Add(T entity, IFormFile file)
        {
            await _table.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Update(T entity, IFormFile file)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
