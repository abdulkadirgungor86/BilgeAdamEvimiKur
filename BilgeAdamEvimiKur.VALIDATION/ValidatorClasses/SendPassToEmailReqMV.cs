using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppUserVMs.PureVMs.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VALIDATION.ValidatorClasses
{
    public class SendPassToEmailReqMV : AbstractValidator<SendPassToEmailReqModel>
    {
        public SendPassToEmailReqMV()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz.")
                .MinimumLength(4).WithMessage("Kullanıcı adı 4 karakter az olamaz")
                .Must(x => !string.IsNullOrWhiteSpace(x?.Trim())).WithMessage("Kullanıcı adı yalnızca boşluk karakterlerinden oluşamaz.")
                .MaximumLength(20).WithMessage("Kullanıcı adı en fazla 20 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı boş bırakılamaz.")
                .MinimumLength(7).WithMessage("Email alanı 7 karakter az olamaz")
                .Must(x => !string.IsNullOrWhiteSpace(x?.Trim())).WithMessage("Email alanı yalnızca boşluk karakterlerinden oluşamaz.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.")
                .MaximumLength(50).WithMessage("En fazla 50 karakter uzunluğunda olmalıdır.");
        }
    }
}
