using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppUserVMs.PureVMs.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VALIDATION.ValidatorClasses
{
    public class ChangePasswordReqMV : AbstractValidator<ChangePasswordReqModel>
    {
        public ChangePasswordReqMV()
        {
            {
                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password alanı boş bırakılamaz.")
                    .MinimumLength(4).WithMessage("Password en az 4 karakter uzunluğunda olmalıdır.");

                RuleFor(x => x.NewPassword)
                  .NotEmpty().WithMessage("Yeni Password alanı boş bırakılamaz.")
                  .MinimumLength(4).WithMessage("Yeni Password en az 4 karakter uzunluğunda olmalıdır.");

                RuleFor(x => x.RetypeNewPassword)
                    .NotEmpty().WithMessage("Onaylanacak yeni password alanı boş bırakılamaz.")
                    .Equal(x => x.NewPassword).WithMessage("Girilen yeni password ile eşleşmelidir.");
            }
        }
    }
}
