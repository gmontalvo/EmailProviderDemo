using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailProviderDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (EmailProviderFactory.ProviderType provider in EmailProviderFactory.GetProviders())
            {
                IEmailProvider email = EmailProviderFactory.Get(provider);
                _sendProviders.Items.Add(email.GetType().Name.Replace("Provider", string.Empty));
            }

            if (_sendProviders.Items.Count > 0)
                _sendProviders.SelectedIndex = 0;
        }

        private void _send_Click(object sender, EventArgs e)
        {
            EmailProviderFactory.ProviderType[] providers = EmailProviderFactory.GetProviders().ToArray();
            EmailProviderFactory.ProviderType provider = providers[_sendProviders.SelectedIndex];
            IEmailProvider email = EmailProviderFactory.Get(provider);

            email.From = _from.Text;
            email.AddTo(new List<string>(_to.Text.Split(';')));
            email.Subject = _subject.Text;
            email.Body = _message.Text;

            email.Send();
        }
    }
}
