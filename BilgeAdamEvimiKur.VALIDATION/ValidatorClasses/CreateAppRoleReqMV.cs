using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppRoleVMs.PureVMs.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VALIDATION.ValidatorClasses
{
    public class CreateAppRoleReqMV : AbstractValidator<CreateAppRoleReqModel>
    {
        public CreateAppRoleReqMV()
        {
            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("İsim alanı boş geçilemez")
                 .MaximumLength(30).WithMessage("isİm 30 karakterden fazla olamaz.");

            RuleFor(x => x.NormalizedName)
                .NotEmpty().WithMessage("Normalleştirilmiş ad alanı boş geçilemez")
                .MaximumLength(50).WithMessage("Normalleştirilmiş ad 50 karakterden fazla olamaz.");
        }
    }
}
