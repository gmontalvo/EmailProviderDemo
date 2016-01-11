using FuelSDK;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text.RegularExpressions;

namespace EmailProviderDemo
{
    class ProviderExactTarget : IEmailProvider
    {
        ET_Email _email = new ET_Email();

        public string From
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void AddTo(List<string> emails)
        {
            throw new NotImplementedException();
        }

        public string Subject
        {
            get { return _email.Subject; }
            set { _email.Subject = value; }
        }

        public string Body
        {
            get { return _email.HTMLBody; }
            set { _email.HTMLBody = value; }
        }

        public void Send()
        {
            NameValueCollection collection = new NameValueCollection();
            collection.Add("clientId", "GM TODO");
            collection.Add("clientSecret", ConfigurationManager.AppSettings[GetType().Name]);
            _email.AuthStub = new ET_Client(collection);

            _email.EmailType = "HTML";
            _email.IsHTMLPaste = true;
            _email.SyncTextWithHTML = true;

            _email.TextBody = Regex.Replace(_email.HTMLBody, "<.*?>", string.Empty);
            _email.HTMLBody = _email.HTMLBody.Replace("\r\n", "<br>");

            _email.Post();
        }
    }
}
