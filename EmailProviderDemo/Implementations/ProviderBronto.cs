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
        }
    }
}
