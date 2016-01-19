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
    using Provider = EmailProviderFactory.ProviderType;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Provider provider in EmailProviderFactory.GetProviders())
            {
                IEmailProvider email = EmailProviderFactory.Get(provider);

                _sendProviders.Items.Add(email.GetType().Name.Replace("Provider", string.Empty));
                _statsProviders.Items.Add(email.GetType().Name.Replace("Provider", string.Empty));
            }

            if (_sendProviders.Items.Count > 0)
                _sendProviders.SelectedIndex = 0;

            if (_statsProviders.Items.Count > 0)
                _statsProviders.SelectedIndex = 0;
        }

        private void _send_Click(object sender, EventArgs e)
        {
            Provider[] providers = EmailProviderFactory.GetProviders().ToArray();
            Provider provider = providers[_sendProviders.SelectedIndex];
            IEmailProvider email = EmailProviderFactory.Get(provider);

            email.From = _from.Text;
            email.To = _to.Text.Split(';').ToList();
            email.Subject = _subject.Text;
            email.Body = _message.Text;

            email.Send();
        }

        private void _load_Click(object sender, EventArgs e)
        {
            _metrics.Items.Clear();

            Provider[] providers = EmailProviderFactory.GetProviders().ToArray();
            Provider provider = providers[_statsProviders.SelectedIndex];
            IEmailProvider email = EmailProviderFactory.Get(provider);

            foreach(IMetricsProvider metric in email.GetMetrics(Convert.ToInt32(_days.Value)))
            {
                ListViewItem item = _metrics.Items.Add(metric.Name);

                item.SubItems.Add(Convert.ToString(metric.Bounces));
                item.SubItems.Add(Convert.ToString(metric.Clicks));
                item.SubItems.Add(Convert.ToString(metric.Opens));
                item.SubItems.Add(Convert.ToString(metric.Sends));
                item.SubItems.Add(Convert.ToString(metric.Unsubscribes));
            }
        }
    }
}
