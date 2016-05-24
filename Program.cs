using Barbar.Ball.Concrete;
using System;
using System.IO;
using System.Windows.Forms;

namespace Barbar.Ball
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var provider = new FileDictionaryProvider(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "cs_CZ.dic"));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain(provider));
        }
    }
}
