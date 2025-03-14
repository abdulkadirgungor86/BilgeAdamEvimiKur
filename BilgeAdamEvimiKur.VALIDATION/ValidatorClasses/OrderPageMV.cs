using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.OrderVMs.PageVMs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VALIDATION.ValidatorClasses
{
    public class OrderPageMV : AbstractValidator<OrderPageVM>
    {
        public OrderPageMV()
        {
            RuleFor(x => x.Order.ShippingAddress)
              .NotEmpty().WithMessage("Teslimat adresi boş bırakılamaz.")
              .MinimumLength(10).WithMessage("Teslimat adresi en az 10 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.Payment.CardUserName)
                .NotEmpty().WithMessage("Kart sahibi adı boş bırakılamaz.")
                .MinimumLength(4).WithMessage("Kart sahibi adı en az 4 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.Payment.CardNumber)
                .NotEmpty().WithMessage("Kart numarası boş bırakılamaz.")
                .CreditCard().WithMessage("Geçerli bir kart numarası girin.");

            RuleFor(x => x.Payment.ExpiryMonth)
                .NotEmpty().WithMessage("Geçerlilik süresi (ay) alanı boş bırakılamaz.")
                .InclusiveBetween(1, 12).WithMessage("Geçerli bir ay girin.");

            RuleFor(x => x.Payment.ExpiryYear)
                .NotEmpty().WithMessage("Geçerlilik süresi (yıl) alanı boş bırakılamaz.")
                .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("Geçerli bir yıl girin.");

            RuleFor(x => x.Payment.CVC)
                .NotEmpty().WithMessage("Güvenlik numarası alanı boş bırakılamaz.")
                .Matches(@"^\d{3}$").WithMessage("Geçerli bir CVC girin.");

        }
    }
}
