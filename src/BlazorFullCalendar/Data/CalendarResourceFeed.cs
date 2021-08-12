using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlazorFullCalendar.Data
{
    public class CalendarResourceFeed : JsonSerializable
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("extraParams")]
        public Dictionary<string, string> ExtraParams { get; set; }
    }
}
