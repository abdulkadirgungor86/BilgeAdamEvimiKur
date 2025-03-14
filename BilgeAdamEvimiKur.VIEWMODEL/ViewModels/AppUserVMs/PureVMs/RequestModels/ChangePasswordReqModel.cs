using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppUserVMs.PureVMs.RequestModels
{
    public class ChangePasswordReqModel
    {
        public string? Id { get; set; }
        public string? Password { get; set; }
        public string? NewPassword { get; set; }
        public string? RetypeNewPassword { get; set; }
    }
}
