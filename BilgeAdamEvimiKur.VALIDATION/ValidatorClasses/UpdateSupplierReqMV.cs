using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.Supplier.PureVMs.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VALIDATION.ValidatorClasses
{
    public class UpdateSupplierReqMV : AbstractValidator<UpdateSupplierReqModel>
    {
        public UpdateSupplierReqMV()
        {
            RuleFor(x => x.SupplierName)
                .NotEmpty().WithMessage("Tedarikçi ismi gereklidir.")
                .MaximumLength(30).WithMessage("Tedarikçi ismi 30 karakterden fazla olamaz.");

            RuleFor(x => x.ContactName)
                .NotEmpty().WithMessage("İletişim ismi gereklidir.")
                .MaximumLength(30).WithMessage("İletişim ismi 30 karakterden fazla olamaz.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email adı gereklidir.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon numarası gereklidir.")
                .Matches(@"^\d{3}-\d{3}-\d{4}$").WithMessage("Telefon numarası formatı 123-456-7890 şeklinde olmalıdır.");

            RuleFor(x => x.WebSite)
                .NotEmpty().WithMessage("Web site adresi gereklidir.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres alanı boş bırakılamaz.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Şehir alanı gereklidir.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Ülke alanı gereklidir.");
        }

    }
}
