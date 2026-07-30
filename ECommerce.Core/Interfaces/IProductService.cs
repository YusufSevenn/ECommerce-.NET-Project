using ECommerce.Core.DTOs;
using ECommerce.Core.RequestParameters;
using ECommerce.Core.Wrappersz;

namespace ECommerce.Core.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> CreateProductAsync(ProductCreateDto productCreateDto);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<List<ProductDto>> GetAllProductsAsync();

        Task<PaginatedResult<ProductDto>> GetPaginatedProductsAsync(PaginationParams paginationParams);
    }
}