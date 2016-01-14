using System;
using System.Collections.Generic;
using System.Configuration;
using EmailProviderDemo.Bronto;
using System.Windows.Forms;

namespace EmailProviderDemo
{
    class ProviderBronto : IEmailProvider
    {
	
		BrontoSoapPortTypeClient _client = new BrontoSoapPortTypeClient();
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
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Body
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void Send()
        {
             //Get session id using api token
            String sessionId = _client.login(ConfigurationManager.AppSettings[GetType().Name]);
            MessageBox.Show(sessionId);

            // session header contains sessionId to make further calls
            sessionHeader header = new sessionHeader();
            header.sessionId = sessionId;

            /*
            // create a contact object, that represents an email in Bronto
            contactObject pradeep = new contactObject();
            pradeep.email = "pradeep.macharla@inmar.com";
            
            // create collection of contact objects
            contactObject[] contacts = new contactObject[]{pradeep};

            //add contacts collection to client
            writeResult _result = _client.addContacts(header, contacts);

            stringValue _strv = new stringValue();
            _strv.value = "pradeep.macharla@inmar.com";

            //wideopen filter
            contactFilter _filter = new contactFilter();
            _filter.type = filterType.OR;
            _filter.email = new stringValue[]{_strv};

            readContacts read = new readContacts();
            read.filter = _filter;
            read.pageNumber = 1;

            contactObject[] readContacts = _client.readContacts(header, read);
            MessageBox.Show(readContacts[0].email);
            */

            messageContentObject _message = new messageContentObject();
            _message.subject = "Sent from EmailProviderDemo";
            _message.content = "Test mail from EmailProviderDemo";
            _message.type = "text";

            messageContentObject[] _messagelist = new messageContentObject[] { _message };

            messageObject _mo = new messageObject();
            _mo.name = "EmailProviderDemo message";
            _mo.content = _messagelist;


            stringValue _strv = new stringValue();
            _strv.value = "macharla.pradeep.kumar@gmail.com";
            //wideopen filter
            contactFilter _filter = new contactFilter();
            _filter.type = filterType.OR;
            _filter.email = new stringValue[] { _strv };
            readContacts read = new readContacts();
            read.filter = _filter;
            read.pageNumber = 1;
            contactObject[] readContacts = _client.readContacts(header, read);


            deliveryRecipientObject _dr = new deliveryRecipientObject();
            _dr.deliveryType = "selected";
            _dr.type = "contact";
            _dr.id = readContacts[0].id;

            deliveryRecipientObject[] _recipientlist = new deliveryRecipientObject[] { _dr };

            deliveryObject _do = new deliveryObject();
            _do.start = new DateTime();
            _do.type = "bulk";
            _do.fromEmail = "Gregory.Montalvo@inmar.com";
            _do.fromName = "Greg Montalvo";
            _do.replyEmail = "Gregory.Montalvo@inmar.com";
            _do.content = _messagelist;
            _do.recipients = _recipientlist;
            _do.messageId = _mo.id;

            deliveryObject[] _dolist = new deliveryObject[] { _do };

            writeResult _res =  _client.addDeliveries(header, _dolist);
            
            MessageBox.Show(_res.results[0].id);



        }

        public IEnumerable<IMetricsProvider> GetMetrics()
        {
            throw new NotImplementedException();
        }
    }
}
