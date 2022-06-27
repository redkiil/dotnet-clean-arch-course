using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _service.GetCategories();
            if(categories == null)return NotFound("Categories not found!");
            return Ok(categories);
        }
        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var category = await _service.GetById(id);
            if(category == null)return NotFound("Category not found!");
            return Ok(category);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CategoryDTO categoryDto)
        {
            if(categoryDto == null)return BadRequest("Invalid data");
            await _service.Add(categoryDto);
            return new CreatedAtRouteResult("GetCategory", new { id = categoryDto.Id }, categoryDto);
        }
        [HttpPut]
        public async Task<ActionResult> Update(int id, [FromBody] CategoryDTO categoryDto)
        {
            if(id != categoryDto.Id)return BadRequest();
            if(categoryDto == null)return BadRequest();
            await _service.Update(categoryDto);
            return Ok(categoryDto);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _service.GetById(id);
            if(category == null)return NotFound("Category not found");
            await _service.Delete(id);
            return Ok(category);
        }
    }
}