using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Ball
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var words = new List<string>(140000);
            using (var stream = File.OpenRead("cs_CZ.dic"))
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    while(true)
                    {
                        string line = reader.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        if (line[0] >= 'a' && line[0] <= 'z')
                        {
                            var parts = line.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                            var part = parts[0];
                            words.Add(part);
                        }
                    }

                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain(words));
        }
    }
}
