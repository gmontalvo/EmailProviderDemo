using System;
using System.Collections.Generic;
using System.Linq;

namespace EmailProviderDemo
{
    static class EmailProviderFactory
    {
        public enum ProviderType
        {
            Bronto,
            ExactTarget,
            SendGrid,
        }

        static public IEmailProvider Get(ProviderType provider)
        {
            IEmailProvider email = null;

            switch (provider)
            {
            case ProviderType.Bronto:
                email = new ProviderBronto();
                break;

            case ProviderType.ExactTarget:
                email = new ProviderExactTarget();
                break;

            case ProviderType.SendGrid:
                email = new ProviderSendGrid();
                break;

            default:
                throw new ArgumentException("Unknown ProviderType");
            }

            return email;
        }

        static public IEnumerable<ProviderType> GetProviders()
        {
            return Enum.GetValues(typeof(EmailProviderFactory.ProviderType)).Cast<EmailProviderFactory.ProviderType>();
        }
    }
}
