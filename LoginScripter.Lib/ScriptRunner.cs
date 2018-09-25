using LoginScripter.Lib.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginScripter.Lib
{
    public class ScriptRunner
    {
        private readonly ScriptRunnerVM _vm;
        private readonly IReadOnlyList<ILoginScript> _scripts;

        public ScriptRunner()
        {
            _scripts = ScriptContainer.Scripts.Where(s => s.Enabled).ToList();
            _vm = new ScriptRunnerVM()
            {
                CurrentScript = 0,
                TotalScripts = _scripts.Count,
                CurrentScriptName = null,
                Finished = false
            };
        }

        private int _curr;
        private int CurrentScript
        {
            get
            {
                return _curr;
            }
            set
            {
                _curr = value;
                _vm.CurrentScript = value;
            }
        }

        private string _currScriptName;
        private string CurrentScriptName
        {
            get
            {
                return _currScriptName;
            }
            set
            {
                _currScriptName = value;
                _vm.CurrentScriptName = value;
            }
        }

        private bool _finished = false;
        private bool Finished
        {
            get
            {
                return _finished;
            }
            set
            {
                _finished = value;
                _vm.Finished = value;
            }
        }
               
        public void Run()
        {
            CurrentScript = 0;
            Finished = false;
            for (int i = 0; i < _scripts.Count; i++)
            {
                var script = _scripts[i];
                CurrentScriptName = script.FriendlyName;
                CurrentScript = i + 1;
                Console.WriteLine($"Now executing script: \"{script.FriendlyName}\". ({CurrentScript} / {_scripts.Count})");
                Console.WriteLine();
                int res = -1;
                try
                {
                    res = script.Run();
                } catch (Exception e)
                {
                    Console.WriteLine($"ERROR: { e.StackTrace}");
                }
                Console.WriteLine();
                Console.WriteLine($"Finished executing script: \"{script.FriendlyName}\". ({CurrentScript} / {_scripts.Count})");
                Console.WriteLine($"Script exited with code {res}.");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
            CurrentScriptName = null;
            Finished = true;
        }
    }
}
