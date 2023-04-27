using Google.Apis.Calendar.v3.Data;

namespace Last
{
    public class Event
    {
        public Event()
        {
            this.Start = new EventDateTime()
            {
                TimeZone = "Europe/Greece"
            };
            this.End = new EventDateTime()
            {
                TimeZone = "Europe/Greece"
            };
        }

        public string Summary { get; set; }
        public string Description { get; set; }
        public EventDateTime Start { get; set; }
        public EventDateTime End { get; set; }

        public class EventDateTime
        {
            public string DateTime { get; set; }
            public string TimeZone { get; set; }

        }

    }
}
