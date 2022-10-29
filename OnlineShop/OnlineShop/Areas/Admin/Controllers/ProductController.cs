using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Core.IConfigration;
using OnlineShop.Data;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnityOfWork _unityofwork;
        private readonly ILogger<ProductController> _logger;
        private readonly ApplicationDbContext _context;

        public ProductController(
            IUnityOfWork unityOfWork,
            ILogger<ProductController> logger,
            ApplicationDbContext context
            )
        {
            _unityofwork = unityOfWork;
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var data=await _unityofwork.Products.GetAllAsync();
            return View(data);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var exitProduct = await _unityofwork.Products.GetById(id);
            ViewBag.categoryId = new SelectList(await _unityofwork.Categorys.GetAllAsync(), "Id", "Name");
            if (exitProduct == null)
            {
                return NotFound();
            }
            return View(exitProduct);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.categoryId = new SelectList(await _unityofwork.Categorys.GetAllAsync(),"Id","Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product,IFormFile img)
        {
            await _unityofwork.Products.Add(product,img);
            await _unityofwork.CompleteAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var data = await _unityofwork.Products.GetById(id);
            ViewBag.categoryId = new SelectList(await _unityofwork.Categorys.GetAllAsync(), "Id", "Name");
            if (data== null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product product,IFormFile img)
        {
            await _unityofwork.Products.Update(product,img);
            await _unityofwork.CompleteAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await _unityofwork.Products.Delete(id);
            await _unityofwork.CompleteAsync();
            return RedirectToAction("Index");
        }
    }
}
