using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginScripter.Lib.Scripts
{
    class SayHiScript : ILoginScript
    {
        public string FriendlyName => "Hello World!";

        public bool Enabled => true;

        public int Run()
        {
            Console.WriteLine("Hello World!");
            return 0;
        }
    }
}
