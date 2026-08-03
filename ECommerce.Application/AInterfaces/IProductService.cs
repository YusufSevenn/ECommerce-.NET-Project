using ECommerce.Application.DTOs;
using ECommerce.Core.RequestParameters;
using ECommerce.Core.Wrappers;

namespace ECommerce.Application.AInterfaces
{
    public interface IProductService
    {
        Task<ProductDto> CreateProductAsync(ProductCreateDto productCreateDto);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<List<ProductDto>> GetAllProductsAsync();

        Task<PaginatedResult<ProductDto>> GetPaginatedProductsAsync(PaginationParams paginationParams);
    }
}