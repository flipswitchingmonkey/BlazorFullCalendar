using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorFullCalendar.Data
{
    public class CalendarEventTimeFormat
    {
        public string hour { get; set; }
        public string minute { get; set; }
        public string second { get; set; }
        public dynamic meridiem { get; set; }
        public bool? omitZeroMinute { get; set; }
        public bool? hour12 { get; set; }
    }
}
