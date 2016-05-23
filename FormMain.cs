using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ball
{
    public partial class FormMain : Form
    {
        IList<string> m_Words;

        public FormMain() : this(new List<string>())
        {
        }

        public FormMain(IList<string> words)
        {
            m_Words = words;

            InitializeComponent();
            var font = new Font(FontFamily.GenericSansSerif, 21.75f, GraphicsUnit.Point);
            foreach(var c in "aábcčdďeéěfghiíjklmnňoópqrřsštťuúůvwxyýzž".ToUpper().ToCharArray())
            {
                var button = new Button();
                button.Text = c.ToString();
                button.Click += Button_Click;
                button.Font = font;
                button.Height = 40;
                panel.Controls.Add(button);
            }
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
    }
}
