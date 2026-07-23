using ECommerce.Core.DTOs;

namespace ECommerce.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
        Task<CategoryDto> GetCategoryByIdAsync(int id);
    }
}