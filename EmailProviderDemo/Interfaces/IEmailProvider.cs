using System.Collections.Generic;

namespace EmailProviderDemo
{
    interface IEmailProvider
    {
        string From { get; set; }
        IEnumerable<string> To { get; set; }

        string Subject { get; set; }
        string Body { get; set; }

        void Send();
        IEnumerable<IMetricsProvider> GetMetrics(int days);
    }
}
