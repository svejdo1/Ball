using Barbar.Ball.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Barbar.Ball
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
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
