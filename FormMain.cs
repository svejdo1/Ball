using Barbar.Ball.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barbar.Ball
{
    public partial class FormMain : Form
    {
        private readonly IDictionaryProvider m_Provider;
        IList<string> m_Words;

        public FormMain() : this(new NullDictionaryProvider())
        {
        }

        public FormMain(IDictionaryProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            m_Provider = provider;
            m_Words = provider.Load();
            InitializeComponent();
            RefreshCounts();
        }

        private void RefreshCounts()
        {
            lblWords.Text = string.Format("Words count - {0}.", m_Words.Count);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            txtChars.Text += ((Button)sender).Text;
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            txtChars.Text = string.Empty;
            txtResults.Text = string.Empty;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            txtResults.Text = string.Empty;
            var pattern = txtChars.Text.ToLower().ToList();
            var results = new List<string>();
            Parallel.ForEach(m_Words, (word) =>
            {
                var list = new List<char>(pattern);
                bool fail = false;
                for (var i = 0; i < word.Length; i++)
                {
                    if (list.Contains(word[i]))
                    {
                        list.Remove(word[i]);
                    }
                    else
                    {
                        fail = true;
                        break;
                    }

                }
                if (!fail)
                {
                    results.Add(word);
                }
            });
            foreach(string result in results.Distinct().OrderByDescending(r => r.Length))
            {
                txtResults.Text += result + ",   ";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWord.Text))
            {
                var normalized = txtWord.Text.ToLower();
                if (!m_Words.Contains(normalized))
                {
                    m_Words.Add(normalized);
                }
            }
            txtWord.Text = string.Empty;
            RefreshCounts();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWord.Text))
            {
                var normalized = txtWord.Text.ToLower();
                m_Words.Remove(normalized);
            }
            txtWord.Text = string.Empty;
            RefreshCounts();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_Provider.Save(m_Words);
        }
    }
}
