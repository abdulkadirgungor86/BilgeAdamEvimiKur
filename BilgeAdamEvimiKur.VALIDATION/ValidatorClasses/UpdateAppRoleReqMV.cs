using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppRoleVMs.PureVMs.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VALIDATION.ValidatorClasses
{
    public class UpdateAppRoleReqMV : AbstractValidator<UpdateAppRoleReqModel>
    {
        public UpdateAppRoleReqMV()
        {
            RuleFor(x => x.Name)
                  .NotEmpty().WithMessage("Kullanıcı rol ismi boş bırakılamaz")
                  .MaximumLength(30).WithMessage("Kullanıcı rol ismi 30 karakterden fazla olamaz.");

            RuleFor(x => x.NormalizedName)
                .NotEmpty().WithMessage("Normalleştirilmiş kulllanıcı rol ismi boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Normalleştirilmiş kulllanıcı rol ismi 100 karakterden fazla olamaz.");
        }
    }
}
