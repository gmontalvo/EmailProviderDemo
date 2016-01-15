using System.Collections.Generic;

namespace EmailProviderDemo
{
    interface IEmailProvider
    {
        string From { get; set; }
        void AddTo(IEnumerable<string> emails);

        string Subject { get; set; }
        string Body { get; set; }

        void Send();
        IEnumerable<IMetricsProvider> GetMetrics();
    }
}
