using EmailProviderDemo.Bronto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace EmailProviderDemo
{
    class ProviderBronto : IEmailProvider
    {
        public string From { get; set; }
        public IEnumerable<string> To { get; set; }

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

            foreach(string email in To)
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

            ///////////////////////////////////////////////////////////////////
            //
            // Instantiate the client
            //
            ///////////////////////////////////////////////////////////////////

            sessionHeader header = new sessionHeader();
            BrontoSoapPortTypeClient client = new BrontoSoapPortTypeClient();
            header.sessionId = client.login(ConfigurationManager.AppSettings[GetType().Name]);


            ///////////////////////////////////////////////////////////////////
            //
            // Retrieve the contact for which you need the metrics.
            //
            ///////////////////////////////////////////////////////////////////

            contactFilter cf = new contactFilter();
            cf.id = new string[] { "4a8e4dc1-e38f-438b-b8b5-75e46799bc9c" };
            readContacts rc = new readContacts();
            rc.filter = cf;
            contactObject[] co = client.readContacts(header, rc);

            contactObject mycontact = co[0];

            ///////////////////////////////////////////////////////////////////
            //
            // Create a deliverFilter and read all deliverObjects
            //
            ///////////////////////////////////////////////////////////////////

            deliveryFilter df = new deliveryFilter();
            df.deliveryType = new string[] { "normal","test","automated","split","transactional","triggered" };
            readDeliveries rd = new readDeliveries();
       
            rd.filter = df;
            deliveryObject[] dos = client.readDeliveries(header, rd);

            ///////////////////////////////////////////////////////////////////
            //
            // Extract deliverObjects metrics into JSONObject type that ProviderMetrics expects
            //
            ///////////////////////////////////////////////////////////////////
            List<IMetricsProvider> list = new List<IMetricsProvider>();
            try
            {
                foreach (deliveryObject doj in dos)
                {
                    ProviderMetrics item = new ProviderMetrics();
                    item.Name = doj.messageId;
                    item.Bounces = (int)doj.numBounces;
                    item.Clicks = (int)doj.numClicks;
                    item.Opens = (int)doj.numOpens;
                    item.Sends = (int)doj.numSends;
                    // read unsubscribes
                    unsubscribeFilter uf = new unsubscribeFilter();
                    uf.deliveryId = doj.id;
                    readUnsubscribes ru = new readUnsubscribes();
                    ru.filter = uf;
                    ru.pageNumber = 1;
                    if (client.readUnsubscribes(header, ru) == null)
                        item.Unsubscribes = 0;
                    else
                    item.Unsubscribes = client.readUnsubscribes(header, ru).Length;
                    list.Add(item);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                ProviderMetrics item = new ProviderMetrics();
                item.Name = "null";
                item.Bounces = 0;
                item.Clicks = 0;
                item.Opens = 0;
                item.Sends = 0;
                item.Unsubscribes = 0;
                list.Add(item);
            }
            return list;
          
        }
    }
}
