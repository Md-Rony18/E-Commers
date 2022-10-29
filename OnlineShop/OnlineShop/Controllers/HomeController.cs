using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Core.IConfigration;
using OnlineShop.Models;
using OnlineShop.Utilitis;
using System.Diagnostics;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnityOfWork _unityOfwork;
        public HomeController(ILogger<HomeController> logger,IUnityOfWork unityOfWork)
        {
            _logger = logger;
            _unityOfwork = unityOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var product = await _unityOfwork.Products.GetAllAsync();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var data = await _unityOfwork.Products.GetById(id);
            ViewData["categoryId"] = new SelectList(await _unityOfwork.Categorys.GetAllAsync(),"Id","Name");
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Card(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<Product> products = new List<Product>();
            var product=await _unityOfwork.Products.GetById(id);
            if(product == null)
            {
                return NoContent();
            }
            products = HttpContext.Session.Get<List<Product>>("products");
            if (products == null)
            {
                products = new List<Product>();
            }
            products.Add(product);
            HttpContext.Session.Set("products",products);
            return RedirectToAction("Index");
        }
        public IActionResult CardItem()
        {
            var products = HttpContext.Session.Get<List<Product>>("products");
            if(products == null)
            {
                products = new List<Product>();
            }
            return View(products);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}