using BilgeAdamEvimiKur.VALIDATION.ValidatorClasses;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppProfileVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppRoleVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppUserVMs.PageVMs;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppUserVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.OrderVMs.PageVMs;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ProductVMs.PageVMs;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ProductVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.Supplier.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.VerifyMailVMs.PureVMs.RequestModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.IOC.DependencyResolvers
{
    public static class ValidationResolver
    {
        public static void AddValidatiorServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            services.AddScoped<IValidator<CreateCategoryReqModel>, CreateCategoryReqMV>();
            services.AddScoped<IValidator<CreateProductReqModel>, CreateProductReqMV>();
            services.AddScoped<IValidator<UserSignInReqModel>, UserSignInReqMV>();
            services.AddScoped<IValidator<UserRegisterReqModel>, UserRegisterReqMV>();
            services.AddScoped<IValidator<UpdateCategoryReqModel>, UpdateCategoryReqMV>();
            services.AddScoped<IValidator<UpdateProductReqModel>, UpdateProductReqMV>();
            services.AddScoped<IValidator<OrderPageVM>, OrderPageMV>();
            services.AddScoped<IValidator<CreateProductPageVM>, CreateProductPageMV>();
            services.AddScoped<IValidator<UpdateProductPageVM>, UpdateProductPageMV>();
            services.AddScoped<IValidator<CreateAppRoleReqModel>, CreateAppRoleReqMV>();
            services.AddScoped<IValidator<UpdateAppRoleReqModel>, UpdateAppRoleReqMV>();
            services.AddScoped<IValidator<UpdateAppUserProfileReqModel>, UpdateAppUserProfileReqMV>();
            services.AddScoped<IValidator<CreateSupplierReqModel>,  CreateSupplierReqMV>();
            services.AddScoped<IValidator<UpdateSupplierReqModel>, UpdateSupplierReqMV>();
            services.AddScoped<IValidator<VerifyMailReqModel>, VerifyMailReqMV>();
            services.AddScoped<IValidator<AppUserProfileReqModel>, AppUserProfileReqMV>();
            services.AddScoped<IValidator<SendPassToEmailReqModel>, SendPassToEmailReqMV>();
            services.AddScoped<IValidator<ChangePasswordReqModel>, ChangePasswordReqMV>();
        }
    }

}
