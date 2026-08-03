using ECommerce.Application.DTOs;

namespace ECommerce.Application.AInterfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
        Task<CategoryDto> GetCategoryByIdAsync(int id);
    }
}