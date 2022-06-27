using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Models
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private ICategoryService _service;
        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _service.GetCategories();

            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDto)
        {
            if(ModelState.IsValid)
            {
                await _service.Add(categoryDto);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)return NotFound();
            var categoryDto = await _service.GetById(id);
            if(categoryDto == null)return NotFound();
            return View(categoryDto);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO categoryDto)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _service.Update(categoryDto);
                }catch(Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)return NotFound();
            var categoryDto = await _service.GetById(id);
            if(categoryDto == null)return NotFound();
            return View(categoryDto);
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
            
            var categoryDto = await _service.GetById(id);
            
            if(categoryDto == null)return NotFound();

            return View(categoryDto);
        }
    }
}