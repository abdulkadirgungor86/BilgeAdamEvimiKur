using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VALIDATION.ValidatorClasses
{
    public class CreateCategoryReqMV : AbstractValidator<CreateCategoryReqModel>
    {
        public CreateCategoryReqMV()
        {
            RuleFor(x => x.CategoryName)
              .NotEmpty().WithMessage("Kategori ismi gereklidir.")
              .MaximumLength(30).WithMessage("Kategori ismi 30 karakterden fazla olamaz.");


            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama gereklidir.")
                .MaximumLength(50).WithMessage("Açıklama 50 karakterden fazla olamaz.");
        }
    }
}
