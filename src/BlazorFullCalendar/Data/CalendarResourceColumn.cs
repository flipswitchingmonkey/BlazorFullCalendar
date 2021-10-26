using Newtonsoft.Json;

namespace BlazorFullCalendar.Data
{
    public class CalendarResourceColumn : JsonSerializable
    {
        [JsonProperty("labelText")]
        public string LabelText { get; set; }

        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("width")]
        public string Width { get; set; }

        [JsonProperty("group")]
        public bool Group { get; set; } = false;
    }
}
