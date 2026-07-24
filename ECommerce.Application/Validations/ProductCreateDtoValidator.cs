using ECommerce.Core.DTOs;
using FluentValidation;

namespace ECommerce.Applicaiton.Validations
{
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün ismi boş bırakılamaz.")
                .MaximumLength(200).WithMessage("Ürün ismi en fazla 200 karakter olabilir.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Geçerli bir kategori seçilmelidir.");
        }
    }
}