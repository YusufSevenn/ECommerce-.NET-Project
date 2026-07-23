using Microsoft.AspNetCore.Mvc;
using ECommerce.Core.DTOs;
using ECommerce.Interfaces;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryCreateDto categoryCreateDto)
        {
            var isSuccess = await _categoryService.CreateCategoryAsync(categoryCreateDto);

            if (isSuccess)
            {
                return Ok("Kategori başarıyla eklendi.");
            }
            return BadRequest("Kategori eklenirken bir hata oluştu.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var categoryDto = await _categoryService.GetCategoryByIdAsync(id);

            if (categoryDto == null) return NotFound("Kategori bulunamadı.");

            return Ok(categoryDto);
        }
    }
}