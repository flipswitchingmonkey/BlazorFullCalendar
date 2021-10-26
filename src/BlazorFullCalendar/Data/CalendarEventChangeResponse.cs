using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlazorFullCalendar.Data
{
    public class CalendarEventChangeResponse : JsonSerializable
    {
        [JsonProperty("start")]
        public DateTime? Start { get; set; }

        [JsonProperty("end")]
        public DateTime? End { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("groupId")]
        public string GroupId { get; set; }

        [JsonProperty("allDay")]
        public string AllDay { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("rendering")]
        public string Rendering { get; set; }

        [JsonProperty("backgroundColor")]
        public string BackgroundColor { get; set; }

        [JsonProperty("textColor")]
        public string TextColor { get; set; }

        [JsonProperty("classNames")]
        public string[] ClassNames { get; set; }

        [JsonProperty("daysOfWeek")]
        public string DaysOfWeek { get; set; }

        [JsonProperty("startTime")]
        public DateTime? StartTime { get; set; }

        [JsonProperty("endTime")]
        public DateTime? EndTime { get; set; }

        [JsonProperty("startRecur")]
        public DateTime? StartRecur { get; set; }

        [JsonProperty("endRecur")]
        public DateTime? EndRecur { get; set; }

        [JsonProperty("resourceId")]
        public string ResourceId { get; set; }

        [JsonProperty("resourceIds")]
        public string[] ResourceIds { get; set; }

        [JsonProperty("dataSet")]
        public Dictionary<string, dynamic> DataSet { get; set; }
    }
}
