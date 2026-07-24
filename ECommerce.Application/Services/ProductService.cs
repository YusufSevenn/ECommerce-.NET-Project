using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using AutoMapper;
using ECommerce.Core.DTOs;

namespace ECommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //Dependency Injection ile UnitOfWork'ü içeri aalıyoruz
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateProductAsync(ProductCreateDto productCreateDto)
        {
            //İŞ KURALI 1: DTO'dan gelen veriyi kontrol ediyoruz.
            if (string.IsNullOrWhiteSpace(productCreateDto.Name))
            {
                throw new ArgumentException("Ürün ismi boş bırakılamaz.");
            }

            if (productCreateDto.Price <= 0)
            {
                throw new ArgumentException("Ürün fiyatı 0'dan büyük olmalıdır.");
            }

            // 1. DÖNÜŞÜM : DTO -> Entity
            var product = _mapper.Map<Product>(productCreateDto);

            //Kurallar geçildiyse veritabanı işlemine başla
            await _unitOfWork.Products.AddAsync(product);
            var result = await _unitOfWork.SaveAsync();

            if (result > 0)
            {
                var createdProduct = await _unitOfWork.Products.GetSingleWithIncludesAsync(
                    p => p.Id == product.Id,
                    p => p.Category
                );
                return _mapper.Map<ProductDto>(createdProduct);
            }

            return null;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetSingleWithIncludesAsync(p => p.Id == p.Id, p => p.Category);
            if (product == null)
            {
                return null;
            }

            // 2.DÖNÜŞÜM : Entity -> DTO
            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllWithIncludesAsync(p => p.Category);

            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}