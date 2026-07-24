using ECommerce.Core.DTOs;

namespace ECommerce.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
        Task<CategoryDto> GetCategoryByIdAsync(int id);
    }
}