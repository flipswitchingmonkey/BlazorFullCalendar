using BlazorFullCalendar.Contracts;
using Newtonsoft.Json;

namespace BlazorFullCalendar.Data
{
    public class JsonSerializable : IJsonSerializable
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}
