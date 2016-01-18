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
        List<string> _emails = new List<string>();
        Dictionary<int, string> _names = new Dictionary<int, string>();

        public string From { get; set; }

        public void AddTo(IEnumerable<string> emails)
        {
            foreach(string email in emails)
            {
                if (!_emails.Contains(email))
                {
                    _emails.Add(email);
                }
            }
        }

        public string Subject { get; set; }
        public string Body { get;  set ; }

        public void Send()
        {
            List<ET_Subscriber> subscribers = new List<ET_Subscriber>();

            foreach (string email in _emails)
            {
                ET_Subscriber subscriber = new ET_Subscriber();
                subscriber.EmailAddress = email;
                subscriber.SubscriberKey = email;

                subscribers.Add(subscriber);
            }

            ET_TriggeredSend triggered = new ET_TriggeredSend();
            triggered.AuthStub = CreateClient();
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

        public IEnumerable<IMetricsProvider> GetMetrics(int days)
        {
            Dictionary<int, ProviderMetrics> dictionary = new Dictionary<int,ProviderMetrics>();

            ///////////////////////////////////////////////////////////////////
            //
            // setup variables used throughout the routine
            //
            ///////////////////////////////////////////////////////////////////

            ET_Client client = CreateClient();

            string[] properties = new string[]
            {
                "EventDate",
                "SendID",
            };

            DateTime end = DateTime.Now;
            DateTime start = end - TimeSpan.FromDays(days);

            SimpleFilterPart filter = new SimpleFilterPart();
            filter.Property = "EventDate";
            filter.SimpleOperator = SimpleOperators.between;
            filter.DateValue = new DateTime[] { start, end };

            ///////////////////////////////////////////////////////////////////
            //
            // count bounces
            //
            ///////////////////////////////////////////////////////////////////

            ET_BounceEvent bounce = new ET_BounceEvent();
            bounce.GetSinceLastBatch = false;
            bounce.AuthStub = client;
            bounce.SearchFilter = filter;
            bounce.Props = properties;

            GetReturn results = bounce.Get();

            foreach (ET_BounceEvent item in results.Results)
            {
                if (!dictionary.ContainsKey(item.SendID))
                {
                    dictionary[item.SendID] = new ProviderMetrics();
                    dictionary[item.SendID].Name = GetEmailName(client, item.SendID);
                }

                dictionary[item.SendID].Bounces++;
            }

            ///////////////////////////////////////////////////////////////////
            //
            // count clicks
            //
            ///////////////////////////////////////////////////////////////////

            ET_ClickEvent click = new ET_ClickEvent();
            click.GetSinceLastBatch = false;
            click.AuthStub = client;
            click.SearchFilter = filter;
            click.Props = properties;

            results = click.Get();

            foreach (ET_ClickEvent item in results.Results)
            {
                if (!dictionary.ContainsKey(item.SendID))
                {
                    dictionary[item.SendID] = new ProviderMetrics();
                    dictionary[item.SendID].Name = GetEmailName(client, item.SendID);
                }

                dictionary[item.SendID].Clicks++;
            }

            ///////////////////////////////////////////////////////////////////
            //
            // count opens
            //
            ///////////////////////////////////////////////////////////////////

            ET_OpenEvent open = new ET_OpenEvent();
            open.GetSinceLastBatch = false;
            open.AuthStub = client;
            open.SearchFilter = filter;
            open.Props = properties;

            results = open.Get();

            foreach (ET_OpenEvent item in results.Results)
            {
                if (!dictionary.ContainsKey(item.SendID))
                {
                    dictionary[item.SendID] = new ProviderMetrics();
                    dictionary[item.SendID].Name = GetEmailName(client, item.SendID);
                }

                dictionary[item.SendID].Opens++;
            }

            ///////////////////////////////////////////////////////////////////
            //
            // count sends
            //
            ///////////////////////////////////////////////////////////////////

            ET_SentEvent send = new ET_SentEvent();
            send.GetSinceLastBatch = false;
            send.AuthStub = client;
            send.SearchFilter = filter;
            send.Props = properties;

            results = send.Get();

            foreach (ET_SentEvent item in results.Results)
            {
                if (!dictionary.ContainsKey(item.SendID))
                {
                    dictionary[item.SendID] = new ProviderMetrics();
                    dictionary[item.SendID].Name = GetEmailName(client, item.SendID);
                }

                dictionary[item.SendID].Sends++;
            }

            ///////////////////////////////////////////////////////////////////
            //
            // count unsubscribes
            //
            ///////////////////////////////////////////////////////////////////

            ET_UnsubEvent unsubscribe = new ET_UnsubEvent();
            unsubscribe.GetSinceLastBatch = false;
            unsubscribe.AuthStub = client;
            unsubscribe.SearchFilter = filter;
            unsubscribe.Props = properties;

            results = unsubscribe.Get();

            foreach (ET_UnsubEvent item in results.Results)
            {
                if (!dictionary.ContainsKey(item.SendID))
                {
                    dictionary[item.SendID] = new ProviderMetrics();
                    dictionary[item.SendID].Name = GetEmailName(client, item.SendID);
                }

                dictionary[item.SendID].Unsubscribes++;
            }

            return dictionary.Values;
        }

        private ET_Client CreateClient()
        {
            NameValueCollection collection = new NameValueCollection();
            collection.Add("clientId", "e0rp4uc9gouydey7fem6hhmb");
            collection.Add("clientSecret", ConfigurationManager.AppSettings[GetType().Name]);

            return new ET_Client(collection);
        }

        private string GetEmailName(ET_Client client, int sendID)
        {
            if (!_names.ContainsKey(sendID))
            {
                ET_Send send = new ET_Send();
                send.AuthStub = client;

                send.Props = new string[]
                {
                    "ID",
                    "EmailName",
                };

                send.SearchFilter = new SimpleFilterPart()
                {
                    Property = "ID",
                    SimpleOperator = SimpleOperators.equals,
                    Value = new string[] { Convert.ToString(sendID) },
                };

                GetReturn results = send.Get();

                if (results.Results.Length > 0)
                {
                    ET_Send item = (ET_Send)results.Results[0];
                    _names[sendID] = item.EmailName.Trim();
                }
            }

            return _names[sendID];
        }
    }
}
