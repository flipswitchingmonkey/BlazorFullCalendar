using Newtonsoft.Json;

namespace BlazorFullCalendar.Data
{
    public class CalendarResourceItem : JsonSerializable
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
