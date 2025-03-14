using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.COMMON.Tools.Services
{
    public static class DirectoryService
    {
        public static string GetSolutionDirectoryPath ()
        {
            string? directory = Directory.GetCurrentDirectory();
            while (Directory.GetParent(directory) != null && !Directory.EnumerateFiles(directory, "*.sln").Any())
            {
                directory = Directory.GetParent(directory).FullName;
            }
            return directory;
        }
    }
}
