using BilgeAdamEvimiKur.DTO.DTOs.ProfileDTOs;
using BilgeAdamEvimiKur.ENTITIES.Models;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppProfileVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.BLL.Managers.Abstracts
{
    public interface IProfileManager : IManager<AppUserProfile, ProfileDTO>
    {
        bool UpdateUserProfile(AppUserProfileDTO item);
        Task<List<AppUserProfileDTO>> GetUserProfiles();
        Task<AppUserProfileDTO> GetUserProfileAsync(int Id);
    }
}
