using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.VerifyMailVMs.PureVMs.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VALIDATION.ValidatorClasses
{
    public class VerifyMailReqMV : AbstractValidator<VerifyMailReqModel>
    {
        public VerifyMailReqMV()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email adresi boş bırakılamaz")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz");
        }
    }
}
