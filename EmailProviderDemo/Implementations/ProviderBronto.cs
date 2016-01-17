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
                _emails.Add(email);
            }
        }

        public string Subject { get; set; }
        public string Body { get; set; }

        public void Send()
        {
            From = "gregory.montalvo@inmar.com";
            Subject = "Test";
            Body = "<b>BOLD TEXT</b>\r\n\r\ntest\r\ntest\r\ntest\r\n";
            _emails.Add("pradeep.macharla@inmar.com");

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
            // create the list of recipients
            //
            ///////////////////////////////////////////////////////////////////

            List<stringValue> recipientValues = new List<stringValue>();

            foreach(string email in _emails)
            {
                stringValue recipientValue = new stringValue();
                recipientValue.value = email;

                recipientValues.Add(recipientValue);
            }

            ///////////////////////////////////////////////////////////////////
            //
            // TODO: add recipients to contact list ??
            //
            ///////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////
            //
            // query the contact list to determine contact ID's
            //
            ///////////////////////////////////////////////////////////////////

            contactFilter cf = new contactFilter();
            cf.type = filterType.OR;
            cf.email = recipientValues.ToArray();

            readContacts rc = new readContacts();
            rc.filter = cf;
            contactObject[] contacts = client.readContacts(header, rc);

            ///////////////////////////////////////////////////////////////////
            //
            // using list of contacts, create list of recipients
            //
            ///////////////////////////////////////////////////////////////////

            List<deliveryRecipientObject> recipients = new List<deliveryRecipientObject>();

            foreach(contactObject contact in contacts)
            {
                deliveryRecipientObject recipient = new deliveryRecipientObject();
                recipient.type = "contact";
                recipient.id = contact.id;

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
            mo.name = Convert.ToString(Guid.NewGuid());
            mo.content = new messageContentObject[] { mco };

            messageObject[] messages = new messageObject[] { mo };
            client.addMessages(header, messages);

            ///////////////////////////////////////////////////////////////////
            //
            // query the message list to determine message ID's
            //
            ///////////////////////////////////////////////////////////////////

            stringValue messageValue = new stringValue();
            messageValue.@operator = filterOperator.Contains;
            messageValue.value = mo.name;

            messageFilter mf = new messageFilter();
            mf.name = new stringValue[] { messageValue };
            mf.type = filterType.OR;

            readMessages rm = new readMessages();
            rm.filter = mf;
            rm.includeContent = false;
            messages = client.readMessages(header, rm);

            ///////////////////////////////////////////////////////////////////
            //
            // using list of messages, create list of deliveries
            //
            ///////////////////////////////////////////////////////////////////

            List<deliveryObject> deliveries = new List<deliveryObject>();

            foreach (messageObject message in messages)
            {
                deliveryObject delivery = new deliveryObject();
                delivery.start = DateTime.Now;
                delivery.type = "triggered";
                delivery.fromEmail = From;
                delivery.replyEmail = From;
                delivery.fromName = From.Substring(0, From.IndexOf('@'));
                delivery.messageId = message.id;
                delivery.recipients = recipients.ToArray();

                deliveries.Add(delivery);
            }

            // send the deliveries!
            client.addDeliveries(header, deliveries.ToArray());
        }

        public IEnumerable<IMetricsProvider> GetMetrics()
        {
            return new List<IMetricsProvider>();
        }
    }
}
