using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailProviderDemo
{
    class ProviderSendGrid : IEmailProvider
    {
        SendGridMessage _email = new SendGridMessage();

        public string From
        {
            get { return _email.From.Address; }
            set { _email.From = new MailAddress(value);  }
        }

        public void AddTo(List<string> emails)
        {
            _email.AddTo(emails);
        }

        public string Subject
        {
            get { return _email.Subject; }
            set { _email.Subject = value; }
        }

        public string Body
        {
            get { return _email.Html; }
            set { _email.Html = value; }
        }

        public void Send()
        {
            Test();

            //_email.EnableClickTracking();
            //_email.EnableOpenTracking();

            //_email.Text = Regex.Replace(_email.Html, "<.*?>", string.Empty);
            //_email.Html = _email.Html.Replace("\r\n", "<br>");

            //Web transport = new Web(ConfigurationManager.AppSettings[GetType().Name]);
            //transport.DeliverAsync(_email);
        }

        public async void Test()
        {
            string start = string.Format("{0:yyyy-MM-dd}", DateTime.Now - TimeSpan.FromDays(7));
            string end = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            Client client = new Client(ConfigurationManager.AppSettings[GetType().Name]);
            HttpResponseMessage response = await client.GlobalStats.Get(start, end, "day");

            MessageBox.Show("");
        }
    }
}
