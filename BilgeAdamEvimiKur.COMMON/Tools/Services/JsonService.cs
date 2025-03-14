using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.COMMON.Tools.Services
{
    public static class JsonService
    {
        public async static Task<Dictionary<string, string>> ReadFromFileAsync(List<string> keys)
        {
            string? filePath = Path.Combine(DirectoryService.GetSolutionDirectoryPath(), "configsettings.json");
            string? jsonData = await System.IO.File.ReadAllTextAsync(filePath);
            JObject? jsonObject = JObject.Parse(jsonData);
            Dictionary<string, string> configSettings = new Dictionary<string, string>();
            foreach (string key in keys) configSettings.Add(key, jsonObject.GetValue(key).ToString());
            return configSettings;
        }
    }
}
