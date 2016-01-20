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
        Dictionary<int, string> _names = new Dictionary<int, string>();

        public string From { get; set; }
        public IEnumerable<string> To { get; set; }
    
        public string Subject { get; set; }
        public string Body { get;  set ; }

        public void Send()
        {
            ///////////////////////////////////////////////////////////////////
            //
            // setup the client stub, and add requisite
            // parameters to html body
            //
            ///////////////////////////////////////////////////////////////////

            ET_Client client = CreateClient();
            Body += "\r\nThis email was sent by:\r\n%%Member_Busname%%\r\n%%Member_Addr%%\r\n%%Member_City%%, %%Member_State%%, %%Member_PostalCode%%, %%Member_Country%%";

            ///////////////////////////////////////////////////////////////////
            //
            // use emails to setup the list of subscribers
            //
            ///////////////////////////////////////////////////////////////////

            List<ET_Subscriber> subscribers = new List<ET_Subscriber>();

            foreach (string recipient in To)
            {
                ET_Subscriber subscriber = new ET_Subscriber();
                subscriber.EmailAddress = recipient;
                subscriber.SubscriberKey = recipient;

                subscribers.Add(subscriber);
            }

            ///////////////////////////////////////////////////////////////////
            //
            // create email within ExactTarget email list
            //
            ///////////////////////////////////////////////////////////////////

            ET_Email email = new ET_Email();
            email.AuthStub = client;
            email.Name = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            email.Subject = Subject;
            email.HTMLBody = Body.Replace("\r\n", "<br>");
            email.TextBody = Regex.Replace(Body, "<.*?>", string.Empty);
            email.EmailType = "html";

            PostReturn results = email.Post();

            ///////////////////////////////////////////////////////////////////
            //
            // create triggered email within ExactTarget triggered email list
            //
            ///////////////////////////////////////////////////////////////////

            ET_TriggeredSend create = new ET_TriggeredSend();
            create.AuthStub = client;
            create.Name = email.Name;
            create.AutoAddSubscribers = true;
            create.AutoAddSubscribersSpecified = true;
            create.List = new ET_List() { ID = 228 };
            create.CustomerKey = Convert.ToString(Guid.NewGuid());
            create.Email = new ET_Email() { ID = results.Results[0].NewID };
            create.SendClassification = new ET_SendClassification() { CustomerKey = "Default Commercial" };

            create.Post();

            ///////////////////////////////////////////////////////////////////
            //
            // patch triggered email (to set active)
            //
            ///////////////////////////////////////////////////////////////////

            ET_TriggeredSend update = new ET_TriggeredSend();
            update.AuthStub = client;
            update.CustomerKey = create.CustomerKey;
            update.TriggeredSendStatus = TriggeredSendStatusEnum.Active;

            update.Patch();

            ///////////////////////////////////////////////////////////////////
            //
            // send triggered email
            //
            ///////////////////////////////////////////////////////////////////

            ET_TriggeredSend send = new ET_TriggeredSend();
            send.AuthStub = client;
            send.CustomerKey = update.CustomerKey;
            send.Subscribers = subscribers.ToArray();

            send.Send();
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
