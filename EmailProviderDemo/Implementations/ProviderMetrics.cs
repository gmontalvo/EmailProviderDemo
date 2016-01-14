using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderDemo
{
    class ProviderMetrics : IMetricsProvider
    {
        public string Name { get; set; }

        public int Bounces { get; set; }
        public int Clicks { get; set; }
        public int Opens { get; set; }
        public int Sends { get; set; }
        public int Unsubscribes { get; set; }
    }
}
