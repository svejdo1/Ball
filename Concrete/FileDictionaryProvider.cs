using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Barbar.Ball.Concrete
{
    internal class FileDictionaryProvider : IDictionaryProvider
    {
        private string m_FileName;
        private object m_Lock = new object();

        public FileDictionaryProvider(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            m_FileName = fileName;
        }

        public IList<string> Load()
        {
            IList<string> words;
            lock(m_Lock)
            {
                words = new List<string>(140000);
                using (var stream = File.OpenRead(m_FileName))
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    while (true)
                    {
                        string line = reader.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        if (line[0] >= 'A' && line[0] <= 'Z')
                        {
                            continue;
                        }
                        var parts = line.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                        var part = parts[0];
                        words.Add(part);
                    }
                }
            }
            return words;
        }

        public void Save(IList<string> keywords)
        {
            string temporaryFile = Path.GetTempFileName();
            using (var stream = File.OpenWrite(temporaryFile))
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                foreach (string keyword in keywords)
                {
                    writer.WriteLine(keyword);
                }
            }

            lock(m_Lock)
            {
                File.Copy(temporaryFile, m_FileName, true);
                File.Delete(temporaryFile);
            }
        }
    }
}
