using Microsoft.EntityFrameworkCore;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Core.IGenericRepositores;
using OnlineShop.Data;

namespace OnlineShop.Core.GenericRepositores
{
    public class CategoryRepository : GenericRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {

        }

        public override async Task<IEnumerable<Category>> GetAllAsync()
        {
            try
            {
                return await _table.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"GetAll Method Failed..!!");
                return Enumerable.Empty<Category>();
            }
        }
        public override async Task<Category> GetById(Guid id)
        {
            try
            {
                return await _table.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll Method Failed..!!");
                return null;
            }
        }

        public override async Task<bool> Add(Category entity)
        {
            try
            {
                await _table.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll Method Failed..!!");
                return false;
            }
        }
        public override async Task<bool> Update(Category entity)
        {
            try
            {
                var existCategory = await _table.Where(c => c.Id == entity.Id).FirstOrDefaultAsync();
                if (existCategory == null)
                {
                    await Add(entity);
                }
                existCategory.Name = entity.Name;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll Method Failed..!!");
                return false;
            }
        }
        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var existCategory = await _table.Where(c => c.Id == id).FirstOrDefaultAsync();
                if(existCategory != null)
                {
                     _table.Remove(existCategory);
                }
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll Method Failed..!!");
                return false;
            }
        }
    }
}
