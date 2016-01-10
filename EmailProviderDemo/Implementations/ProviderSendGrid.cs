using SendGrid;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

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

        public void AddTo(string email)
        {
            _email.AddTo(email);
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
            set { _email.Html = value; _email.Text = Regex.Replace(value, "<.*?>", string.Empty); }
        }

        public void Send()
        {
            _email.EnableClickTracking();
            _email.EnableOpenTracking();

            NetworkCredential credentials = new NetworkCredential("gmontalvo", ConfigurationManager.AppSettings[GetType().Name]);
            Web transport = new Web(credentials);
            transport.DeliverAsync(_email).Wait();
        }
    }
}
