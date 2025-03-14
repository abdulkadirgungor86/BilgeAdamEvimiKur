using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.BLL.Services.Abstracts
{
    public interface ISessionService
    {
        T GetObject<T>(string key) where T : class;
        void SetObject<T>(string key, T value);
    }

}
