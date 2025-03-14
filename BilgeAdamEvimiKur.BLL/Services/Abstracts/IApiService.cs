using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.BLL.Services.Abstracts
{
    public interface IApiService
    {
        Task<(bool, string)> MakePostRequestAsync(string url, object data);
    }

}
