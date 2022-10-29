using OnlineShop.Core.GenericRepositores;
using OnlineShop.Core.IConfigration;
using OnlineShop.Core.IGenericRepositores;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace OnlineShop.Data
{
    public class UnityOfWork : IUnityOfWork,IDisposable
    {
        public ICategoryRepository Categorys { get; private set; }

        public IProductRepository Products { get; private set; }
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _evn;
        public UnityOfWork(ApplicationDbContext context,ILoggerFactory logger, IHostingEnvironment evn)
        {
            _context = context;
            _logger = logger.CreateLogger("logs");
            _evn = evn;
            Categorys= new CategoryRepository(_context,_logger);
            Products = new ProductRepository(_context, _logger,evn);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
