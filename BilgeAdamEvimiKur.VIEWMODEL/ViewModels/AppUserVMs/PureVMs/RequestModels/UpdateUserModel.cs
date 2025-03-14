using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppUserVMs.PureVMs.RequestModels
{
    public class UpdateUserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string NewPassword { get; set; }
        public string Email { get; set; }
        public string EmailConfirmed { get; set; }
        public string Status { get; set; }
        public List<string> RoleNames { get; set; }
        public string NewRoleName { get; set; }
    }
}
