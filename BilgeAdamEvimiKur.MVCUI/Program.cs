
using BilgeAdamEvimiKur.IOC.DependencyResolvers;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContextService();
builder.Services.AddIdentityService();

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSessionService();

builder.Services.AddApplicationCookieService();
builder.Services.AddHttpClient();

builder.Services.AddRepositoryServices();
builder.Services.AddManagerServices();
builder.Services.AddMapperServices();
builder.Services.AddValidatiorServices();
builder.Services.AddBLLCustomService();

/***********************************/
WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area}/{controller=Home}/{Action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
