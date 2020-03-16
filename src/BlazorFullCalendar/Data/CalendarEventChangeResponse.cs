using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorFullCalendar.Data
{
    public class CalendarEventChangeResponse
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Id { get; set; }
        public string GroupId { get; set; }
        public string AllDay { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Rendering { get; set; }
        public string BackgroundColor { get; set; }
        public string TextColor { get; set; }
        public string[] ClassNames { get; set; }
        public string DaysOfWeek { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? StartRecur { get; set; }
        public DateTime? EndRecur { get; set; }
        public string ResourceId { get; set; }
        public string[] ResourceIds { get; set; }
        public Dictionary<string, dynamic> DataSet { get; set; }
    }
}
