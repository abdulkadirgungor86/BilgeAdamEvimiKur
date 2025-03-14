using BilgeAdamEvimiKur.DTO.DTOs.AppUserDTOs;
using BilgeAdamEvimiKur.ENTITIES.Models;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.RequestModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.BLL.Managers.Abstracts
{
    public interface IAppUserManager : IManager<AppUser, AppUserDTO>
    {
        Task<List<AppUserDTO>> GetAllUserAsync();
        Task DeleteAsync(int id);
        Task<string> SendPassToEmailAsync(SendPassToEmailDTO sendPassToEmailDTO);
        Task<(bool, SignInResult, IList<string?>)> SignInAsync(UserSignInRequestModelDTO userSignInDTO);
        Task<IdentityResult> UserRegisterAsync(UserRegisterRequestModelDTO userRegisterDTO);
        Task<(bool, string)> ConfirmEmailAsync(ConfirmEmailDTO confirmEmailDTO);
        Task<string> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO);
        Task<bool> checkMailActiveAsync(string Id);
        Task SignOutAsync();
    }
}
