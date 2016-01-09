using SendGrid;
using System.Collections.Generic;
using System.Net.Mail;

namespace EmailProviderDemo
{
    class ProviderSendGrid : IEmailProvider
    {
        SendGridMessage _message = new SendGridMessage();

        public string From
        {
            get { return _message.From.Address; }
            set { _message.From = new MailAddress(value);  }
        }

        public void AddTo(string email)
        {
            _message.AddTo(email);
        }

        public void AddTo(List<string> emails)
        {
            _message.AddTo(emails);
        }

        public string Subject
        {
            get { return _message.Subject; }
            set { _message.Subject = value; }
        }

        public string Body
        {
            get { return _message.Html; }
            set { _message.Html = value; }
        }
    }
}
