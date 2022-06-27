using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Models
{
    public class ProductsController : Controller
    {
        private readonly IProductService _service;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;
        public ProductsController(IProductService service, ICategoryService categoryService, IWebHostEnvironment environment)
        {   
            _service = service;
            _categoryService = categoryService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _service.GetProducts();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {
            if(ModelState.IsValid)
            {
                await _service.Add(productDto);
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)return NotFound();
            var productDto = await _service.GetById(id);
            if(productDto == null)return NotFound();
            var categories = await _categoryService.GetCategories();
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name", productDto.CategoryId);
            return View(productDto);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO productDto)
        {
            if(ModelState.IsValid)
            {
                await _service.Update(productDto);
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)return NotFound();
            var productDto = await _service.GetById(id);
            if(productDto == null)return NotFound();
            return View(productDto);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)return NotFound();
            var productDto = await _service.GetById(id);
            if(productDto == null)return NotFound();
            var wwwroot = _environment.WebRootPath;
            var image = Path.Combine(wwwroot,"images\\"+ productDto.Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExists = exists;
            return View(productDto);
        }
    }
}