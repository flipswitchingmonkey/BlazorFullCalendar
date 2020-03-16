using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFullCalendar.Data
{
    public class CalendarResourceColumn
    {
        public string labelText { get; set; }
        public string field { get; set; }
        public string width { get; set; }
        public bool group { get; set; } = false;
    }
}
