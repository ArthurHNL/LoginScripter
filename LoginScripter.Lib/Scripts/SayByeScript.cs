using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginScripter.Lib.Scripts
{
    class SayByeScript : ILoginScript
    {
        public string FriendlyName => "Goodbye World!";

        public bool Enabled => true;

        public int Run()
        {
            Console.WriteLine("Goodbye World!");
            return 0;
        }
    }
}
