using System;
using Newtonsoft.Json;

namespace BlazorFullCalendar.Data
{
    public class CalendarDateItemQuery : JsonSerializable
    {
        [JsonProperty("start")]
        public DateTime? Start { get; set; }

        [JsonProperty("end")]
        public DateTime? End { get; set; }
        //public string custom1 { get; set; }
        //public string custom2 { get; set; }
    }
}
