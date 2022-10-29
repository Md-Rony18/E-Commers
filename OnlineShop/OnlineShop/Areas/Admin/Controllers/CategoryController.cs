using Microsoft.AspNetCore.Mvc;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Core.IConfigration;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {
        private readonly IUnityOfWork _unityofwork;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(IUnityOfWork unityOfWork,ILogger<CategoryController> logger)
        {
            _unityofwork=unityOfWork;
            _logger=logger;
        }
        public async Task<IActionResult> Index()
        {
            var data=await _unityofwork.Categorys.GetAllAsync();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await _unityofwork.Categorys.Add(category);
            await _unityofwork.CompleteAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var data = await _unityofwork.Categorys.GetById(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            await _unityofwork.Categorys.Update(category);
            await _unityofwork.CompleteAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await _unityofwork.Categorys.Delete(id);
            await _unityofwork.CompleteAsync();
            return RedirectToAction("Index");
        }
    }
}
