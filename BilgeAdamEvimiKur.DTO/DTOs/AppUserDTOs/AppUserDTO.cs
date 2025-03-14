using BilgeAdamEvimiKur.DTO.DTOs.AppRoleDTOs;
using BilgeAdamEvimiKur.DTO.DTOs.BaseEntityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.DTO.DTOs.AppUserDTOs
{
    public class AppUserDTO : BaseEntityDTO
    {
        public AppUserDTO()
        {
            RoleNames = new List<string>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? PasswordHash { get; set; }
        public string Email { get; set; }
        public string EmailConfirmed { get; set; }
        public string Status { get; set; }
        public List<string> RoleNames { get; set; }

    }
}
