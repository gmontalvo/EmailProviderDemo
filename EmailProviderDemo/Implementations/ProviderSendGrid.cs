using Newtonsoft.Json.Linq;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            _email.EnableClickTracking();
            _email.EnableOpenTracking();

            _email.Text = Regex.Replace(_email.Html, "<.*?>", string.Empty);
            _email.Html = _email.Html.Replace("\r\n", "<br>");

            Web transport = new Web(ConfigurationManager.AppSettings[GetType().Name]);
            transport.DeliverAsync(_email);
        }

        public IEnumerable<IMetricsProvider> GetMetrics()
        {
            List<IMetricsProvider> list = new List<IMetricsProvider>();
            string payload = string.Empty;
            
            Task.Run(new Action(() =>
            {
                payload = GetPayload().Result;
            })).Wait();

            JArray jobjects = JArray.Parse(payload);

            foreach (JObject jobject in jobjects)
            {
                ProviderMetrics item = new ProviderMetrics();
                item.Name = (string)jobject["date"];

                JArray stats = (JArray)jobject["stats"];
                JObject metrics = (JObject)stats[0]["metrics"];

                item.Bounces = (int)metrics["bounces"];
                item.Clicks = (int)metrics["clicks"];
                item.Opens = (int)metrics["opens"];
                item.Sends = (int)metrics["delivered"];
                item.Unsubscribes = (int)metrics["unsubscribes"];

                list.Add(item);
            }
            
            return list;
        }

        private async Task<string> GetPayload()
        {
            string start = string.Format("{0:yyyy-MM-dd}", DateTime.Now - TimeSpan.FromDays(7));
            string end = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            Client client = new Client(ConfigurationManager.AppSettings[GetType().Name]);
            HttpResponseMessage response = await client.GlobalStats.Get(start, end, "day");

            return await response.Content.ReadAsStringAsync();
        }
    }
}
