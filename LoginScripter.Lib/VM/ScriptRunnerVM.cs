using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace LoginScripter.Lib.VM
{
    public class ScriptRunnerVM : ViewModelBase
    {
        
        public ScriptRunnerVM()
        {
            QuitCommand = new RelayCommand(Quit, Finished);
        }

        public ICommand QuitCommand { get; private set; }
        private void Quit()
        {
            throw new System.NotImplementedException();
        }

        public override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            Console.WriteLine(propertyName);
            base.RaisePropertyChanged(propertyName);
        }

        private bool _finished;
        public bool Finished {
            get
            {
                return _finished;
            } set
            {
                _finished = value;
                RaisePropertyChanged("Finished");
                RaisePropertyChanged("Message");
            }
        }

        private int _totalScripts;
        public int TotalScripts
        {
            get => _totalScripts; set
            {
                _totalScripts = value;
                RaisePropertyChanged("TotalScripts");
                RaisePropertyChanged("Message");
                RaisePropertyChanged("Progress");
            }
        }

        private int _currentScript;
        public int CurrentScript
        {
            get => _currentScript;
            set
            {
                _currentScript = value;
                RaisePropertyChanged("CurrentScript");
                RaisePropertyChanged("Message");
                RaisePropertyChanged("Progress");
            }
        }

        private string _currScriptName;
        public string CurrentScriptName
        {
            get => _currScriptName;
            set
            {
                _currScriptName = value;
                RaisePropertyChanged("CurrentScriptName");
                RaisePropertyChanged("Message");
            }
        }

        public string Message => CurrentScriptName != null ? $"Now executing {CurrentScriptName}. ({CurrentScript}/{TotalScripts})" : 
           Finished?  $"Finished! ({CurrentScript}/{TotalScripts})" : $"Waiting. ({CurrentScript}/{TotalScripts})";
        public double Progress => TotalScripts != 0 ? CurrentScript / TotalScripts : 0;
    }
}
