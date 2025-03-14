using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VALIDATION.ValidatorClasses
{
    public class UpdateCategoryReqMV : AbstractValidator<UpdateCategoryReqModel>
    {
        public UpdateCategoryReqMV()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Kategori ismi boş bırakılamaz.")
                .MaximumLength(30).WithMessage("Kategori ismi 30 karakterden fazla olamaz.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama alanı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Açıklama alanı 100 karakterden fazla olamaz.");
        }

    }
}
