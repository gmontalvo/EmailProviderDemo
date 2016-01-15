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

        public void AddTo(IEnumerable<string> emails)
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

            //messageContentObject _message = new messageContentObject();
            //_message.subject = "Sent from EmailProviderDemo";
            //_message.content = "Test mail from EmailProviderDemo";
            //_message.type = "text";

            //messageContentObject[] _messagelist = new messageContentObject[] { _message };

            //messageObject _mo = new messageObject();
            //_mo.name = "EmailProviderDemo_message_csharp";
            //_mo.content = _messagelist;

            //messageObject[] _molist = new messageObject[] { _mo };
            //_client.addMessages(header, _molist);

            messageFilter _mf = new messageFilter();

            stringValue _strv0 = new stringValue();
            _strv0.@operator = filterOperator.EqualTo;
            _strv0.value = "stringValue";

            _mf.name = new stringValue[] { _strv0 };
            _mf.type = filterType.OR;
            //_mf.id = new String[] { "290291" };

            readMessages _rdm = new readMessages();
            _rdm.filter = _mf;
            _rdm.includeContent = false;
            _rdm.pageNumber = 1;
            messageObject[] _readcurrentmessages = _client.readMessages(header, _rdm);


            stringValue _strv = new stringValue();
            _strv.value = "pradeep.macharla@inmar.com";
            //wideopen filter
            contactFilter _filter = new contactFilter();
            _filter.type = filterType.OR;
            _filter.email = new stringValue[] { _strv };
            readContacts read = new readContacts();
            read.filter = _filter;
            read.pageNumber = 1;
            contactObject[] readContacts = _client.readContacts(header, read);


            deliveryRecipientObject _dr = new deliveryRecipientObject();
            //_dr.deliveryType = "selected";
            _dr.type = "contact";
            _dr.id = readContacts[0].id;
            MessageBox.Show(_dr.id);

            deliveryRecipientObject[] _recipientlist = new deliveryRecipientObject[] { _dr };

            deliveryObject _do = new deliveryObject();
            _do.start = DateTime.Now;
            // values are bulk(aka. regular), test, automated, split, transactional, triggered,ftaf. Only [triggered,test,transactional] can be used
            // with addDeliveries and updateDeliveries
            _do.type = "triggered";
            _do.messageId = "e1510af7-b8f2-40e7-bfd3-5a23b9f77f1d";
            _do.fromEmail = "Gregory.Montalvo@inmar.com";
            _do.fromName = "Greg Montalvo";
            _do.replyEmail = "Gregory.Montalvo@inmar.com";
            //_do.content = _messagelist;
            _do.recipients = _recipientlist;


            deliveryObject[] _dolist = new deliveryObject[] { _do };
            writeResult _res = _client.addDeliveries(header, _dolist);

            MessageBox.Show(_res.results[0].id);



        }

        public IEnumerable<IMetricsProvider> GetMetrics()
        {
            throw new NotImplementedException();
        }
    }
}
