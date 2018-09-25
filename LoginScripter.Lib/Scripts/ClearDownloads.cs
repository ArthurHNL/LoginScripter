using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoginScripter.Lib;
using Syroot.Windows.IO;
using System.IO;
using System.Threading.Tasks;

namespace LoginScripter.Lib.Scripts
{
    class ClearDownloads : ILoginScript
    {
        private readonly int _maxFileAge = 3;

        public string FriendlyName => $"remove downloads older then {_maxFileAge} days";

        public bool Enabled => true;

        public int Run()
        {
            string downloadFolderPath = KnownFolders.Downloads.Path;

            Directory.GetFiles(downloadFolderPath, "*", SearchOption.AllDirectories)
                .Select(f => new FileInfo(f))
                .Where(f => f.LastAccessTime < DateTime.Now.AddDays(-_maxFileAge))
                .ToList()
                .ForEach(f =>
                {
                    Console.WriteLine($"Removing {f.Name}");
                    f.Delete();
                });

            return 0;
        }
    }
}
