using AutoMapper;
using ECommerce.Core.DTOs;
using ECommerce.Core.Entities;

namespace ECommerce.Application.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            //1. YÖN: Entity'den DTO'ya (Veritabanından okuyup dışarı dönerken)
            CreateMap<Product, ProductDto>();
            CreateMap<Category, CategoryDto>();

            //2. YÖN: DTO'dan Entity'e (Dışarıdan veri alıp veritabanına yazarken)
            CreateMap<ProductCreateDto, Product>();
            CreateMap<CategoryCreateDto, Category>();
        }
    }
}