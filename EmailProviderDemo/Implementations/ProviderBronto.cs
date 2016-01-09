using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class ProviderBronto : IEmailProvider
    {
        public string From
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void AddTo(string email)
        {
            throw new NotImplementedException();
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
    }
}
