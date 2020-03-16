using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFullCalendar.Data
{
    public class CalendarSourceFeed
    {
        public string url { get; set; }
        public string method { get; set; }
        public Dictionary<string, string> extraParams { get; set; }
        public string color { get; set; }
        public string textColor { get; set; }
    }
}
