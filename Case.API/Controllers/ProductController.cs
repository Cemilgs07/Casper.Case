using AutoMapper;
using Case.Domain.Entities;
using Case.Domain.Repositories;
using Case.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Case.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BaseController> logger)
            : base(unitOfWork, mapper, logger) { }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<Product>(dto);
            await _unitOfWork.ProductRepository.CreateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            var dto = _mapper.Map<UpdateProductDto>(product);

            return Ok(product);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _unitOfWork.ProductRepository.GetAllWithCategoryAsync();
            var result = _mapper.Map<List<ProductDto>>(products);
            return Ok(result);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto dto)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            _mapper.Map(dto, product);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            await _unitOfWork.ProductRepository.DeleteByIdAsync(product.Id);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
