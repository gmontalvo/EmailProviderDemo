using FuelSDK;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace EmailProviderDemo
{
    class ProviderExactTarget : IEmailProvider
    {
        List<string> _subscribers = new List<string>();

        public string From { get; set; }

        public void AddTo(List<string> emails)
        {
            _subscribers = emails;
        }

        public string Subject { get; set; }
        public string Body { get;  set ; }

        public void Send()
        {
            NameValueCollection collection = new NameValueCollection();
            collection.Add("clientId", "GM TODO");
            collection.Add("clientSecret", ConfigurationManager.AppSettings[GetType().Name]);
            ET_Client client = new ET_Client(collection);

            List<ET_Subscriber> subscribers = new List<ET_Subscriber>();

            _subscribers.ForEach(subscriber =>
            {
                subscribers.Add(new ET_Subscriber() { EmailAddress = subscriber, SubscriberKey = subscriber });
            });

            ET_TriggeredSend triggered = new ET_TriggeredSend();
            triggered.AuthStub = client;
            triggered.Subscribers = subscribers.ToArray();

            for (int i = 0; i < triggered.Subscribers.Length; i++)
            {
                triggered.Subscribers[i].Owner = new Owner();
                triggered.Subscribers[i].Owner.FromAddress = From;
                triggered.Subscribers[i].Attributes = new FuelSDK.Attribute[2];

                triggered.Subscribers[i].Attributes[1] = new FuelSDK.Attribute();
                triggered.Subscribers[i].Attributes[1].Name = "Subject";
                triggered.Subscribers[i].Attributes[1].Value = Subject;

                triggered.Subscribers[i].Attributes[0] = new FuelSDK.Attribute();
                triggered.Subscribers[i].Attributes[0].Name = "HTML_Content";
                triggered.Subscribers[i].Attributes[0].Value = Body.Replace("\r\n", "<br>");
            }

            triggered.Send();

            //ET_Email email = new ET_Email();
            //email.AuthStub = client;
            //email.Subject = Subject;
            //email.EmailType = "HTML";
            //email.IsHTMLPaste = true;
            //email.SyncTextWithHTML = true;
            //email.TextBody = Regex.Replace(Body, "<.*?>", string.Empty);
            //email.HTMLBody = Body.Replace("\r\n", "<br>");

            //PostReturn results = email.Post();

            //int emailID = results.Results[0].NewID;
            //string key = Convert.ToString(Guid.NewGuid());
            //List<ET_Subscriber> subscribers = new List<ET_Subscriber>();

            //_subscribers.ForEach(subscriber =>
            //{
            //    subscribers.Add(new ET_Subscriber() { EmailAddress = subscriber, SubscriberKey = subscriber });
            //});

            //ET_Send send = new ET_Send();
            //send.AuthStub = client;
            //send.Subject = Subject;

            //send.Email.EmailType = "HTML";
            //send.Email.IsHTMLPaste = true;
            //send.Email.SyncTextWithHTML = true;
            //send.Email.TextBody = Regex.Replace(Body, "<.*?>", string.Empty);
            //send.Email.HTMLBody = Body.Replace("\r\n", "<br>");

            //send.EmailSendDefinition.FromAddress = From;
            //send.EmailSendDefinition.t
            //send.Post();

            //ET_TriggeredSend triggeredsend = new ET_TriggeredSend();
            //triggeredsend.AuthStub = client;
            //triggeredsend.Name = key;
            //triggeredsend.CustomerKey = "SDK Created TriggeredSend";
            //triggeredsend.Email = new ET_Email() { ID = emailID };
            //triggeredsend.SendClassification = new ET_SendClassification() { CustomerKey = "2222" };
            //PostReturn prtriggeredsend = triggeredsend.Post();
            //Console.WriteLine("Post Status: " + prtriggeredsend.Status.ToString());

            //ET_TriggeredSend triggered = new ET_TriggeredSend();
            //triggered.AuthStub = client;
            //triggered.CustomerKey = Convert.ToString(Guid.NewGuid());
            //triggered.Subscribers = subscribers.ToArray();

            //SendReturn send = triggered.Send();

            //private SendReturn SendEmail(ET_Client sendingClient, String triggedSendDefinition, EmailTemplateData emailToSend) {
            ////this is used in a couple of places to extract it here
            //string emailSendID = Convert.ToString(emailToSend.ETSubscriberKey);

            //ET_TriggeredSend tsdSend = new ET_TriggeredSend();
            //tsdSend.AuthStub = sendingClient;
            //tsdSend.Name = triggedSendDefinition;
            //tsdSend.CustomerKey = triggedSendDefinition;

            ////create the subscriber....
            //tsdSend.Subscribers = new ET_Subscriber[] {
            //    new ET_Subscriber() {
            //        EmailAddress = emailToSend.EmailToAddress, SubscriberKey = emailSendID
            //    }
            //};

            ////The HTML_Content is the body of the email etc.
            //tsdSend.Subscribers[0].Owner = new Owner();
            //tsdSend.Subscribers[0].Owner.FromAddress = emailToSend.EmailFromAddress;
            //tsdSend.Subscribers[0].Owner.FromName = emailToSend.EmailFromName;
            //tsdSend.Subscribers[0].Attributes = new FuelSDK.Attribute[2];
            //tsdSend.Subscribers[0].Attributes[0] = new FuelSDK.Attribute();
            //tsdSend.Subscribers[0].Attributes[0].Name = "HTML_Content";
            //tsdSend.Subscribers[0].Attributes[0].Value = emailToSend.EmailBodyHTMLContent;
            //tsdSend.Subscribers[0].Attributes[1] = new FuelSDK.Attribute();
            //tsdSend.Subscribers[0].Attributes[1].Name = "Subject";
            //tsdSend.Subscribers[0].Attributes[1].Value = emailToSend.EmailSubject;

            ////********************SEND THE EMAIL*************************
            ////this is where the latency occurs.
            //SendReturn srSend = tsdSend.Send();

            ////there is some other code in here that records send results.

            //return srSend;
            // }        
        }

        public IEnumerable<IMetricsProvider> GetMetrics()
        {
            throw new NotImplementedException();
        }
    }
}
