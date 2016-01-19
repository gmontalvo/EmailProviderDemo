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
        public string From { get; set; }
        public IEnumerable<string> To { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }

        public void Send()
        {
            SendGridMessage client = new SendGridMessage();
            client.From = new MailAddress(From);
            client.Subject = Subject;
            client.Text = Regex.Replace(Body, "<.*?>", string.Empty);
            client.Html = Body.Replace("\r\n", "<br>");
            client.EnableClickTracking();
            client.EnableOpenTracking();

            foreach(string email in To)
            {
                client.AddTo(email);
            }

            Web transport = new Web(ConfigurationManager.AppSettings[GetType().Name]);
            transport.DeliverAsync(client).ConfigureAwait(false);
        }

        public IEnumerable<IMetricsProvider> GetMetrics(int days)
        {
            List<IMetricsProvider> list = new List<IMetricsProvider>();

            string payload = string.Empty;
            Task task = Task.Run(new Action(() => { payload = GetPayload(days).Result; }));
            task.Wait();

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

        private async Task<string> GetPayload(int days)
        {
            string start = string.Format("{0:yyyy-MM-dd}", DateTime.Now - TimeSpan.FromDays(days));
            string end = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            Client client = new Client(ConfigurationManager.AppSettings[GetType().Name]);
            HttpResponseMessage response = await client.GlobalStats.Get(start, end, "day");

            return await response.Content.ReadAsStringAsync();
        }
    }
}
