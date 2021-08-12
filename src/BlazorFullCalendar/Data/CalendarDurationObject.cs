using Newtonsoft.Json;

namespace BlazorFullCalendar.Data
{
    public class CalendarDurationObject : JsonSerializable
    {
        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("years")]
        public int Years { get; set; }

        [JsonProperty("month")]
        public int Month { get; set; }

        [JsonProperty("months")]
        public int Months { get; set; }

        [JsonProperty("day")]
        public int Day { get; set; }

        [JsonProperty("days")]
        public int Days { get; set; }

        [JsonProperty("hour")]
        public int Hour { get; set; }

        [JsonProperty("hours")]
        public int Hours { get; set; }

        [JsonProperty("minute")]
        public int Minute { get; set; }

        [JsonProperty("minutes")]
        public int Minutes { get; set; }

        [JsonProperty("second")]
        public int Second { get; set; }

        [JsonProperty("seconds")]
        public int Seconds { get; set; }

        [JsonProperty("millisecond")]
        public int Millisecond { get; set; }

        [JsonProperty("milliseconds")]
        public int Milliseconds { get; set; }

        [JsonProperty("ms")]
        public int Ms { get; set; }

        public CalendarDurationObject() { }
        public static CalendarDurationObject FromDays(int amount) => new CalendarDurationObject() { Days = amount };
        public static CalendarDurationObject FromWeeks(int amount) => new CalendarDurationObject() { Days = amount * 7 };
        public static CalendarDurationObject FromMonths(int amount) => new CalendarDurationObject() { Months = amount };
        public static CalendarDurationObject FromYears(int amount) => new CalendarDurationObject() { Years = amount };
        public static CalendarDurationObject FromHours(int amount) => new CalendarDurationObject() { Hours = amount };
        public static CalendarDurationObject FromMinutes(int amount) => new CalendarDurationObject() { Minutes = amount };
    }
}
