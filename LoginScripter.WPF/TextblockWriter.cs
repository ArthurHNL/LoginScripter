using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LoginScripter.WPF
{
    public class TextblockWriter : TextWriter
    {
        public override Encoding Encoding => Encoding.ASCII;

        private TextBlock _control;

        public TextblockWriter(TextBlock control)
        {
            _control = control;
        }

        public override void Write(char c)
        {
            _control.Text += c;
        }
        public override void Write(string s)
        {
            _control.Text += s;
        }
        public override void Write(int value)
        {
            _control.Text += value;
        }
    }
}
