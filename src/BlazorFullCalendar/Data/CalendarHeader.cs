using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFullCalendar.Data
{
    public class CalendarHeader
    {
        public string left { get; set; } = "title";
        public string center { get; set; } = "";
        public string right { get; set; } = "today prev,next";
    }
}
