using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
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
            switch (provider)
            {
                case ProviderType.Bronto:
                    return new ProviderBronto();

                case ProviderType.ExactTarget:
                    return new ProviderExactTarget();

                case ProviderType.SendGrid:
                    return new ProviderSendGrid();
            }

            throw new ArgumentException("Unknown ProviderType");
        }

        static public IEnumerable<ProviderType> GetProviders()
        {
            return Enum.GetValues(typeof(EmailProviderFactory.ProviderType)).Cast<EmailProviderFactory.ProviderType>();
        }
    }
}
