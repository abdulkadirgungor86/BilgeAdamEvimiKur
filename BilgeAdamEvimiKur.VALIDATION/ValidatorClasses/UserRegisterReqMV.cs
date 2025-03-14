using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppUserVMs.PureVMs.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VALIDATION.ValidatorClasses
{
    public class UserRegisterReqMV : AbstractValidator<UserRegisterReqModel>
    {
        public UserRegisterReqMV()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz.")
                .Must(x => !string.IsNullOrWhiteSpace(x?.Trim())) .WithMessage("Kullanıcı adı yalnızca boşluk karakterlerinden oluşamaz.")
                .Length(4, 20).WithMessage("Kullanıcı adı 4 ile 20 karakter arasında olmalıdır.");
            
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password alanı boş bırakılamaz.")
                .MinimumLength(4).WithMessage("Password en az 4 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Onaylanacak password alanı boş bırakılamaz.")
                .Equal(x => x.Password).WithMessage("Girilen şifre ile eşleşmelidir.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı boş bırakılamaz.").EmailAddress()
                .WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.ConfirmEmail)
                .NotEmpty().WithMessage("Onaylanacak email alanı boş bırakılamaz.").EmailAddress()
                .Equal(x => x.Email).WithMessage("Girilen email ile eşleşmelidir.");
        }

    }
}
