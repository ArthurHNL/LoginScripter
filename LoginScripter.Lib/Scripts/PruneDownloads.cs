using Syroot.Windows.IO;
using System;
using System.IO;
using System.Linq;

namespace LoginScripter.Lib.Scripts
{
    public class PruneDownloads : ILoginScript
    {
        private readonly int _maxFileAge = 3;

        public string FriendlyName => "prune downloads";

        public bool Enabled => true;

        public int Run()
        {
            Console.WriteLine($"Removing downloads older then {_maxFileAge} days.");
            string downloadFolderPath = KnownFolders.Downloads.Path;

            Directory.GetFiles(downloadFolderPath, "*", SearchOption.AllDirectories)
                .Select(f => new FileInfo(f))
                .Where(f => f.LastAccessTime < DateTime.Now.AddDays(-_maxFileAge))
                .ToList()
                .ForEach(f =>
                {
                    if (!(f.Attributes.HasFlag(FileAttributes.ReadOnly)))
                    {
                        Console.WriteLine($"Removing {f.Name}");
                        f.Delete();
                    }
                    else
                    {
                        Console.WriteLine($"Skipping {f.Name} (readonly).");
                    }
                });

            return 0;
        }
    }
}
