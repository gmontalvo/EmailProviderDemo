using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class TrackingEventFactory
    {
        public enum EventType
        {
            Bounce,
            Click,
            Open,
            Send,
            Unsubscribe,
        }

        static public ITrackingEvent Get(EventType tracking)
        {
            switch (tracking)
            {
                case EventType.Bounce:
                    throw new NotImplementedException();

                case EventType.Click:
                    throw new NotImplementedException();

                case EventType.Open:
                    throw new NotImplementedException();

                case EventType.Send:
                    throw new NotImplementedException();

                case EventType.Unsubscribe:
                    throw new NotImplementedException();
            }

            throw new ArgumentException("Unknown EventType");
        }

        static public IEnumerable<EventType> GetEvents()
        {
            return Enum.GetValues(typeof(TrackingEventFactory.EventType)).Cast<TrackingEventFactory.EventType>();
        }
    }
}
