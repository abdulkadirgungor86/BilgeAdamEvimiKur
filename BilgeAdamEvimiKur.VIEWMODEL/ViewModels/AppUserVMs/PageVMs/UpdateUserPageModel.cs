using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppRoleVMs.PureVMs.ResponseModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppUserVMs.PureVMs.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppUserVMs.PageVMs
{
    public class UpdateUserPageModel
    {

        public UpdateUserModel? UpdateUserModel { get; set; }
        public List<AppRoleResModel> AppRoleResModels { get; set; }
    }
}
