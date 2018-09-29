using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace LoginScripter.Lib.Scripts
{
    public class PullGitRepos : ILoginScript
    {
        public string FriendlyName => "pull git respositories";

        public bool Enabled => true;

        private readonly string[] foldersWithRepositories =
        {
            @"C:\Repositories\"
        };

        public int Run()
        {
            foreach (string repoRoot in foldersWithRepositories)
            {
                Console.WriteLine($"Searching for repositories in {repoRoot}.");
                DirectoryInfo rootDirInf = new DirectoryInfo(repoRoot);
                //Get all folders containing a .git folder
                List<DirectoryInfo> repos = new List<DirectoryInfo>();
                rootDirInf.GetDirectories(".git", SearchOption.AllDirectories).ToList<DirectoryInfo>().ForEach(
                    dirInf =>
                    {
                        repos.Add(dirInf.Parent);
                        Console.WriteLine($"Discovered repository in {dirInf.Parent.FullName}.");
                    }
                    );
                Console.WriteLine($"Pulling repositories in {repoRoot}.");
                repos.ForEach(r => PullGitRepo(r));

            }
            return 0;
        }

        private void PullGitRepo(DirectoryInfo repoToPull)
        {
            Console.WriteLine($"Pulling {repoToPull}.");
            Console.WriteLine($"Spawning git process...");
            Console.WriteLine();
            Process p = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "git.exe",
                    WorkingDirectory = repoToPull.FullName,
                    Arguments = "pull",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            p.Start();
            while (!p.StandardOutput.EndOfStream)
            {
                Console.WriteLine(p.StandardOutput.ReadLine());
            };
            Console.WriteLine();
            Console.WriteLine($"Finished pulling repository. Check git output for details.");
        }
    }
}
