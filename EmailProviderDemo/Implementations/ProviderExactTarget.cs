using FuelSDK;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

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

        public void AddTo(string email)
        {
            throw new NotImplementedException();
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
            _email.EmailType = "HTML";
            _email.IsHTMLPaste = true;
            _email.SyncTextWithHTML = true; 
            _email.AuthStub = CreateClient();

            _email.Post();
        }

        private ET_Client CreateClient()
        {
            NameValueCollection collection = new NameValueCollection();
            collection.Add("clientId", "GM TODO");
            collection.Add("clientSecret", "GM TODO");

            return new ET_Client(collection);
        }
    }
}
