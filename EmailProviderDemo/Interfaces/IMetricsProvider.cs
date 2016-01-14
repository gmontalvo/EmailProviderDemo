using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderDemo
{
    interface IMetricsProvider
    {
        string Name { get; }

        int Bounces { get; }
        int Clicks { get; }
        int Opens { get; }
        int Sends { get; }
        int Unsubscribes { get; }
    }
}
