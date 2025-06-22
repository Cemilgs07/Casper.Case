using AutoMapper;
using Case.Domain.Entities;
using Case.Domain.Repositories;
using Case.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Case.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BaseController> logger) : base(unitOfWork, mapper, logger) { }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
        {
            // başarılya test edildi.
            if (!ModelState.IsValid)
            {
             
                return BadRequest(ModelState);
            }

            var category = _mapper.Map<Category>(dto);
            await _unitOfWork.CategorytRepository.CreateAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return Ok(category);

        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _unitOfWork.CategorytRepository.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _unitOfWork.CategorytRepository.GetAllAsync();
            var result = _mapper.Map<List<CategoryDto>>(categories);
            return Ok(result);
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryDto dto)
        {
            var category = await _unitOfWork.CategorytRepository.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            _mapper.Map(dto, category);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _unitOfWork.CategorytRepository.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            await _unitOfWork.CategorytRepository.DeleteByIdAsync(category.Id);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
