using BilgeAdamEvimiKur.DTO.DTOs.BaseEntityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.DTO.DTOs.ProfileDTOs
{
    public class ProfileDTO : BaseEntityDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

}
