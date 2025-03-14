using BilgeAdamEvimiKur.BLL.Services.Abstracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.BLL.Services.Concretes
{
    public class ApiService : IApiService
    {
        readonly IHttpClientFactory _httpCF;

        public ApiService(IHttpClientFactory httpCF)
        {
            _httpCF = httpCF;
        }

        public async Task<(bool, string)> MakePostRequestAsync(string url, object data)
        {
            HttpClient hClient = _httpCF.CreateClient();
            string jsonData = JsonConvert.SerializeObject(data);
            StringContent sContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await hClient.PostAsync(url, sContent);
            string responseBody = await responseMessage.Content.ReadAsStringAsync();
            return (responseMessage.IsSuccessStatusCode, responseBody);
        }

    }
}
