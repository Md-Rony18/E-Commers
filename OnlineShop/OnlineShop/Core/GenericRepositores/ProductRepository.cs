using Microsoft.EntityFrameworkCore;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Core.IGenericRepositores;
using OnlineShop.Data;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace OnlineShop.Core.GenericRepositores
{
    public class ProductRepository : GenericRepository<Product>,IProductRepository
    {
        private readonly IHostingEnvironment _env;
        public ProductRepository(ApplicationDbContext context, ILogger logger,IHostingEnvironment evn) : base(context, logger)
        {
            _env = evn;
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                return await _table.Include(c => c.Category).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"GetAllAsync Method is Failed..!!");
                return Enumerable.Empty<Product>();
            }
        }

        public override async Task<Product> GetById(Guid id)
        {
            try
            {
                return await _table.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetById Method is Failed..!!");
                return null;
            }
        }

        public override async Task<bool> Add(Product entity, IFormFile file)
        {
            try
            {
                var imgExtension=Path.GetExtension(file.FileName);
                if (imgExtension == ".jpeg" || imgExtension == ".png" || imgExtension==".gif" || imgExtension==".jpg")
                {
                    var rootPath = _env.WebRootPath;
                    var filePath = "content/product/"+file.FileName;
                    var fullPath=Path.Combine(rootPath, filePath);
                    FileStream steam=new FileStream(fullPath, FileMode.Create);
                    file.CopyToAsync(steam);
                    entity.Img = filePath;
                    await _table.AddAsync(entity);
                }
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Add Method is Failed..!!");
                return false;
            }
        }

        public override async Task<bool> Update(Product entity, IFormFile file)
        {
            try
            {
                var existProduct = await _table.Where(c => c.Id == entity.Id).FirstOrDefaultAsync();
                if(existProduct == null)
                {
                    await Add(entity, file);
                }
                var rootPaths = _env.WebRootPath;
                var existFullPath=Path.Combine(rootPaths,existProduct.Img);
                FileInfo fileInfo = new FileInfo(existFullPath);
                if (fileInfo.Exists)
                {
                    System.IO.File.Delete(existFullPath);
                    fileInfo.Delete();
                }
                var imgExtension=Path.GetExtension(file.FileName);
                if (imgExtension == ".jpeg" || imgExtension == ".png" || imgExtension==".gif" || imgExtension==".jpg")
                {
                    var rootPath = _env.WebRootPath;
                    var filePath="content/product/"+file.FileName;
                    var fullPath=Path.Combine(rootPath,filePath);
                    FileStream stream=new FileStream(fullPath, FileMode.Create);
                    file.CopyToAsync(stream);
                    existProduct.Img = filePath;
                    existProduct.Models = entity.Models;
                    existProduct.Name = entity.Name;
                    existProduct.Price=entity.Price;
                    existProduct.CategoryId = entity.CategoryId;
                    existProduct.Quantity = entity.Quantity;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update Method is Failed..!!");
                return false;
            }
        }
        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var existProduct = await _table.Where(c => c.Id == id).FirstOrDefaultAsync();
                var rootPath = _env.WebRootPath;
                var existPath = Path.Combine(rootPath, existProduct.Img);
                FileInfo file=new FileInfo(existPath);
                if (file.Exists)
                {
                    System.IO.File.Delete(existPath);
                    file.Delete();
                }
                if(existProduct != null)
                {
                    _table.Remove(existProduct);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete Method is Failed..!!");
                return false;
            }
        }
    }
}
