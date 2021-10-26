using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlazorFullCalendar.Data
{
    public class CalendarSourceFeed : JsonSerializable
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("extraParams")]
        public Dictionary<string, string> ExtraParams { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("textColor")]
        public string TextColor { get; set; }
    }
}
