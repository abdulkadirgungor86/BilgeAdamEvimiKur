using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ProductVMs.PageVMs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VALIDATION.ValidatorClasses
{
    public class CreateProductPageMV  : AbstractValidator<CreateProductPageVM>
    {
        public CreateProductPageMV()
        {
            
            RuleFor(x => x.Product.ProductName)
                .NotEmpty().WithMessage("Ürün adı gereklidir.")
                .MaximumLength(30).WithMessage("Ürün adı 30 karakteri aşamaz.");

            RuleFor(x => x.Product.UnitsInStock)
                .NotEmpty().WithMessage("Stok alanı boş bırakılamaz.")
                .GreaterThanOrEqualTo(0).WithMessage("Stok sayısı 0'dan küçük olamaz.");

            RuleFor(x => x.Product.Price)
                .NotEmpty().WithMessage("Fiyat alanı boş bırakılamaz.")
                .GreaterThan(0).WithMessage("Fiyat değeri 0'dan büyük olamalıdır.");
            
            RuleFor(x => x.Product.CategoryID)
                 .NotEmpty().WithMessage("Kategori seçiniz.");

            RuleFor(x => x.Product.SupplierID)
                 .NotEmpty().WithMessage("Tedarikçi seçiniz.");
        }
    }
}
