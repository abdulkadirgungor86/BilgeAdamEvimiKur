using BilgeAdamEvimiKur.BLL.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.BLL.Services.Concretes
{
    public class SessionService : ISessionService
    {
        readonly ISession _session;

        public SessionService(IHttpContextAccessor httpCA)
        {
            _session = httpCA.HttpContext.Session;
        }

        public T GetObject<T>(string key) where T : class
        {
            string oString = _session.GetString(key);
            if (!string.IsNullOrEmpty(oString)) return JsonConvert.DeserializeObject<T>(oString);
            return null;
        }

        public void SetObject<T>(string key, T value)
        {
            string oString = JsonConvert.SerializeObject(value);
            _session.SetString(key, oString);
        }
    }
}
