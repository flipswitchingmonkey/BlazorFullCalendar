using Newtonsoft.Json;

namespace BlazorFullCalendar.Data
{
    public class CalendarHeader : JsonSerializable
    {
        [JsonProperty("left")]
        public string Left { get; set; } = "title";

        [JsonProperty("center")]
        public string Center { get; set; } = "";

        [JsonProperty("right")]
        public string Right { get; set; } = "today prev,next";
    }
}
