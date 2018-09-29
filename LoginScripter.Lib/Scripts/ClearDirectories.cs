using System;
using System.IO;

namespace LoginScripter.Lib.Scripts
{
    public class ClearDirectories : ILoginScript
    {
        private readonly string[] _foldersToClear = new string[]
        {
            @"C:\TEMP",
            @"D:\TEMP"
        };

        public string FriendlyName => $"clear directories";

        public bool Enabled => true;

        public int Run()
        {
            foreach (string path in _foldersToClear)
            {
                Console.WriteLine($"Starting to clear out {path}.");
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                ClearDirectory(dirInfo);
                Console.WriteLine($"Finished clearing out {path}.");
            }
            return 0;
        }

        private void ClearDirectory(DirectoryInfo dirInf)
        {
            Console.WriteLine($"Clear out directory {dirInf.FullName}.");
            //Clear files
            foreach (FileInfo fileInf in dirInf.GetFiles())
            {
                if (!(fileInf.Attributes.HasFlag(FileAttributes.ReadOnly)))
                {
                    Console.WriteLine($"Removing file {fileInf.FullName}.");
                    fileInf.Delete();
                }
                else
                {
                    Console.WriteLine($"Skipped {fileInf.FullName} because it is readonly.");
                }
            }
            //Clear subdirectories by recursively calling this method and then deleting them.
            foreach (DirectoryInfo subDirInf in dirInf.GetDirectories())
            {
                Console.WriteLine($"Start clear subdir {subDirInf.FullName}.");
                ClearDirectory(subDirInf);
                if ((subDirInf.GetFiles("*", SearchOption.AllDirectories).Length == 0) &&
                    (subDirInf.GetDirectories("*", SearchOption.AllDirectories).Length == 0))
                {
                    subDirInf.Delete();
                    Console.WriteLine($"Finished clear subdir {subDirInf.FullName}.");
                }
                else
                {
                    Console.WriteLine($"Some read only files still exist in {subDirInf.FullName}");
                }

            }
            Console.WriteLine($"Done clearing directory {dirInf.FullName}.");
        }
    }
}
