using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.DTO.DTOs.AppUserDTOs
{
    public class ChangePasswordDTO
    {
        public string Id { get; set; }
        public string? Password { get; set; }
        public string? NewPassword { get; set; }
    }

}
