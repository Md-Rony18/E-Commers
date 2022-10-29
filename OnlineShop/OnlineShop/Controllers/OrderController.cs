using Microsoft.AspNetCore.Mvc;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Data;
using OnlineShop.Utilitis;

namespace OnlineShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrderNow()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderNow(Order order)
        {
            
                List<Product> products = HttpContext.Session.Get<List<Product>>("products");
                if (products != null)
                {
                    foreach (var item in products)
                    {
                        OrderDetails orderDetails = new OrderDetails();
                        orderDetails.ProductId = item.Id;
                        order.OrderDetails.Add(orderDetails);
                    }
                    order.OrderId = getorderId();
                } 
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                HttpContext.Session.Set("products", null);
            
            return View();
        }

        public string getorderId()
        {
            int rowcount = _context.Orders.ToList().Count+1;
            return rowcount.ToString("000");
        }
    }
}
