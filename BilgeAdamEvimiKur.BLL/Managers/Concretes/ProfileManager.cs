using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.DAL.Repositories.Abstracts;
using BilgeAdamEvimiKur.DTO.DTOs.AppUserDTOs;
using BilgeAdamEvimiKur.DTO.DTOs.ProfileDTOs;
using BilgeAdamEvimiKur.ENTITIES.Enums;
using BilgeAdamEvimiKur.ENTITIES.Interfaces;
using BilgeAdamEvimiKur.ENTITIES.Models;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppProfileVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.RequestModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.BLL.Managers.Concretes
{
    public class ProfileManager : BaseManager<AppUserProfile, ProfileDTO>, IProfileManager
    {
        readonly IMapper _mapper;
        readonly IProfileRepository _pRep;
        readonly IAppUserManager _appUserManager;

        public ProfileManager(IProfileRepository profileRep, IMapper mapper, IAppUserManager appUserManager) : base(mapper, profileRep)
        {
            _mapper = mapper;
            _pRep = profileRep;
            _appUserManager = appUserManager;
        }

        public override string Destroy(ProfileDTO item)
        {
            if (item != null)
            {
                AppUserProfile entity = _mapper.Map<AppUserProfile>(item);

                if (entity != null)
                {

                    if (entity.Status == DataStatus.Deleted)
                    {
                        try
                        {
                            AppUserProfile profile = _pRep.FirstOrDefault(e => e.ID == item.ID);
                            if (profile != null)
                            {
                                _pRep.Destroy(profile);
                                return "Destroy işlemi başarılı";
                            }
                            return "Hata :  profile nesnesi  null geldi.  ProfileManager/Destroy/profile";
                        }
                        catch (Exception ex)
                        {
                            return ex.Message;
                        }
                    }
                    return "Veriyi yok etmek için önce silmeniz lazım";

                }
                else return "Hata : ProfileManager/Destroy/Mapper";
            }
            else return "Olmayan veri yok edilemez.";
        }

        public async Task<List<AppUserProfileDTO>> GetUserProfiles()
        {
            List<AppUserProfileDTO> profiles = new List<AppUserProfileDTO>();
            foreach (AppUserDTO item in _appUserManager.GetAll())
            {
                AppUserProfileDTO container = new AppUserProfileDTO();
                container.Id = item.Id;
                container.UserName = item.UserName;
                AppUserProfile aUP = _pRep.Find(item.Id);
                if(aUP != null)
                {
                    container.FirstName = aUP.FirstName;
                    container.LastName = aUP.LastName;
                    container.Status = aUP.Status.ToString();
                }
                profiles.Add(container);
            }
            return profiles;
        }

        public async Task<AppUserProfileDTO> GetUserProfileAsync(int Id)
        {
            AppUserProfileDTO container = new AppUserProfileDTO();
            AppUserDTO aUDTO = await _appUserManager.FindAsync(Id);
            AppUserProfile aUP = _pRep.Find(Id);

            container.Id = aUDTO.Id;
            container.UserName = aUDTO.UserName;

            if(aUP != null)
            {
                container.FirstName = aUP.FirstName;
                container.LastName = aUP.LastName;
                container.Status= aUP.Status.ToString();
            }
            return container;
        }


        public bool UpdateUserProfile (AppUserProfileDTO item)
        {
            try
            {
                ProfileDTO profileDTO = _mapper.Map<ProfileDTO>(item);
                profileDTO.Status = (ENTITIES.Enums.DataStatus.Updated).ToString();
                base.Add(profileDTO);
                return true;
            } catch { return false; }
        }

    }
}
