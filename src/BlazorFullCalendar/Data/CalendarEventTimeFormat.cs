using Newtonsoft.Json;

namespace BlazorFullCalendar.Data
{
    public class CalendarEventTimeFormat : JsonSerializable
    {
        [JsonProperty("hour")]
        public string Hour { get; set; }

        [JsonProperty("minute")]
        public string Minute { get; set; }

        [JsonProperty("second")]
        public string Second { get; set; }

        [JsonProperty("meridiem")]
        public dynamic Meridiem { get; set; }

        [JsonProperty("omitZeroMinute")]
        public bool? OmitZeroMinute { get; set; }

        [JsonProperty("hour12")]
        public bool? Hour12 { get; set; }
    }
}
