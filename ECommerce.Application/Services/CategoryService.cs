using AutoMapper;
using ECommerce.Core.DTOs;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Interfaces;

namespace ECommerce.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            if (string.IsNullOrWhiteSpace(categoryCreateDto.Name))
            {
                throw new ArgumentException("Kategori ismi boş bırakılamaz.");
            }

            //DTO -> Entity dönüşümü
            var category = _mapper.Map<Category>(categoryCreateDto);

            await _unitOfWork.Categories.AddAsync(category);
            var result = await _unitOfWork.SaveAsync();

            return result > 0;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
            {
                return null;
            }

            //Entity -> DTO dönüşümü
            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }
    }
}