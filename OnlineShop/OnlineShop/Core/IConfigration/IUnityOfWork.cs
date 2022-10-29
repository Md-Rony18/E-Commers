using OnlineShop.Areas.Admin.Models;
using OnlineShop.Core.IGenericRepositores;

namespace OnlineShop.Core.IConfigration
{
    public interface IUnityOfWork
    {
        ICategoryRepository Categorys { get; }
        IProductRepository Products { get; }
        Task CompleteAsync();
    }
}
