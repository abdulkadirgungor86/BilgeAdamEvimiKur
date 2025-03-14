using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppProfileVMs.PureVMs.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VALIDATION.ValidatorClasses
{
    public class AppUserProfileReqMV : AbstractValidator<AppUserProfileReqModel>
    {
        public AppUserProfileReqMV()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Isim alanı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("En fazla 50 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("SoyIsim alanı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("En fazla 50 karakter uzunluğunda olmalıdır.");
        }
    }
}
