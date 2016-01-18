using EmailProviderDemo.Bronto;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace EmailProviderDemo
{
    class ProviderBronto : IEmailProvider
    {
        List<string> _emails = new List<string>();

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
        public string Body { get; set; }

        public void Send()
        {
            ///////////////////////////////////////////////////////////////////
            //
            // login, which creates the requisite header
            //
            ///////////////////////////////////////////////////////////////////

            sessionHeader header = new sessionHeader();
            BrontoSoapPortTypeClient client = new BrontoSoapPortTypeClient();
            header.sessionId = client.login(ConfigurationManager.AppSettings[GetType().Name]);

            ///////////////////////////////////////////////////////////////////
            //
            // add recipients to contact list
            //
            ///////////////////////////////////////////////////////////////////

            List<contactObject> contactValues = new List<contactObject>();

            foreach(string email in _emails)
            {
                contactObject co = new contactObject();
                co.email = email;
                contactValues.Add(co);
            }

            writeResult contacts = client.addOrUpdateContacts(header, contactValues.ToArray());

            ///////////////////////////////////////////////////////////////////
            //
            // using list of contacts, create list of recipients
            //
            ///////////////////////////////////////////////////////////////////

            List<deliveryRecipientObject> recipients = new List<deliveryRecipientObject>();

            foreach(resultItem item in contacts.results)
            {
                deliveryRecipientObject recipient = new deliveryRecipientObject();
                recipient.type = "contact";
                recipient.id = item.id;

                recipients.Add(recipient);
            }

            ///////////////////////////////////////////////////////////////////
            //
            // add email to message list
            //
            ///////////////////////////////////////////////////////////////////

            messageContentObject mco = new messageContentObject();
            mco.subject = Subject;
            mco.content = Body.Replace("\r\n", "<br>");
            mco.type = "html";

            messageObject mo = new messageObject();
            mo.name = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            mo.content = new messageContentObject[] { mco };

            writeResult messages = client.addMessages(header, new messageObject[] { mo });

            ///////////////////////////////////////////////////////////////////
            //
            // using list of messages, create list of deliveries
            //
            ///////////////////////////////////////////////////////////////////

            List<deliveryObject> deliveries = new List<deliveryObject>();

            foreach(resultItem item in messages.results)
            {
                deliveryObject delivery = new deliveryObject();
                delivery.start = DateTime.Now;
                delivery.startSpecified = true;
                delivery.messageId = item.id;
                delivery.fromEmail = From;
                delivery.fromName = From.Substring(0, From.IndexOf('@'));
                delivery.recipients = recipients.ToArray();

                deliveries.Add(delivery);
            }

            // send the deliveries!
            client.addDeliveriesAsync(header, deliveries.ToArray());
        }

        public IEnumerable<IMetricsProvider> GetMetrics(int days)
        {
            return new List<IMetricsProvider>();
        }
    }
}
