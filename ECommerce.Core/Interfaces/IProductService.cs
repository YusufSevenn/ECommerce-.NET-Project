using ECommerce.Core.DTOs;

namespace ECommerce.Core.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(ProductCreateDto productCreateDto);
        Task<ProductDto> GetProductByIdAsync(int id);
    }
}