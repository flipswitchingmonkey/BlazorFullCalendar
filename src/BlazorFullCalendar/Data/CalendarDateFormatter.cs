using Newtonsoft.Json;

namespace BlazorFullCalendar.Data
{
    public class CalendarDateFormatter : JsonSerializable
    {
        //'numeric' will produce something like 2018
        //'2-digit' will produce something like 18
        [JsonProperty("year")]
        public string Year { get; set; }

        //will produce something like September
        //'short' will produce something like Sep
        //'narrow' will produce something like S
        //'numeric' will produce something like 1
        //'2-digit' will produce something like 01
        [JsonProperty("month")]
        public string Month	{ get; set; }

        //The day of the month.If the date were Jun 3, 2018
        //'numeric' will produce something like 3
        //'2-digit' will produce something like 03
        [JsonProperty("day")]
        public string Day { get; set; }

        //The day of the week.
        //'long' will produce something like Wednesday
        //'short' will produce something like Wed
        //'narrow' will produce something like W
        [JsonProperty("weekday")]
        public string Weekday { get; set; }	
 
        //If the time were 6:05
        //'numeric' would produce something like 6
        //'2-digit' would produce something like 06
        [JsonProperty("hour")]
        public string Hour { get; set; }

        //If the time were 6:05
        //'numeric' would produce something like 5
        //'2-digit' would produce something like 05
        [JsonProperty("minute")]
        public string Minute { get; set; }  

        //'numeric' or '2-digit'
        [JsonProperty("second")]
        public string Second { get; set; }  

        //true for a 12-hour clock, false for a 24-hour clock
        [JsonProperty("hour12")]
        public bool? Hour12 { get; set; }  

        //'short' ('long' is not supported by FullCalendar)
        [JsonProperty("timeZoneName")]
        public string TimeZoneName { get; set; }    

        //'short' will produce something like Wk 8
        //'narrow' will produce something like Wk8
        //'numeric' will produce something like 8
        //This flag cannot be combined with any other flags!
        [JsonProperty("week")]
        public string Week { get; set; }	

        // Normally with a 12-hour clock the meridiem displays as A.M./P.M.
        //'lowercase' will force it to display like a.m./p.m.
        //'short' will force it to display like am/pm
        //'narrow' will force it to display like a/p
        //false will prevent it from displaying altogether
        [JsonProperty("meridiem")]
        public string Meridiem { get; set; }	
    
        //if true, times like 6:00 will display as 6
        [JsonProperty("omitZeroMinute")]
        public bool? OmitZeroMinute { get; set; }	

        //if true, all commas will be stripped from the outputted string
        [JsonProperty("omitCommas")]
        public bool? OmitCommas { get; set; }	
    }
}
