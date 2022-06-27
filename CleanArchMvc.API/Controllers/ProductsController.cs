using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _service.GetProducts();
            if(products == null)return NotFound("Products not found!");
            return Ok(products);
        }
        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var product = await _service.GetById(id);
            if(product == null)return NotFound("Product not found!");
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductDTO productDto)
        {
            if(productDto == null)return BadRequest("Invalid data");
            await _service.Add(productDto);
            return new CreatedAtRouteResult("GetProduct", new { id = productDto.Id}, productDto);
        }
        [HttpPut]
        public async Task<ActionResult> Update(int id, [FromBody] ProductDTO ProductDto)
        {
            if(id != ProductDto.Id)return BadRequest();
            if(ProductDto == null)return BadRequest();
            await _service.Update(ProductDto);
            return Ok(ProductDto);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _service.GetById(id);
            if(product == null)return NotFound("Product not found");
            await _service.Delete(id);
            return Ok(product);
        }
    }
}