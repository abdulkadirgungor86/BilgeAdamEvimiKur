using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppUserVMs.PureVMs.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VALIDATION.ValidatorClasses
{
    public class UserSignInReqMV : AbstractValidator<UserSignInReqModel>
    {
        public UserSignInReqMV()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz.")
                .Length(4, 20).WithMessage("Kullanıcı adı 4 ile 20 karakter arasında olmalıdır.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre alanı boş bırakılamaz.")
                .MinimumLength(4).WithMessage("Şifre en az 4 karakter uzunluğunda olmalıdır.");
        }
    }
}
