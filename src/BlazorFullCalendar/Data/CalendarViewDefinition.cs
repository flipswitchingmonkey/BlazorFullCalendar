using Newtonsoft.Json;

namespace BlazorFullCalendar.Data
{
    public class CalendarViewDefinition : JsonSerializable
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("buttonText")]
        public string ButtonText { get; set; }

        [JsonProperty("duration")]
        public CalendarDurationObject Duration { get; set; }

        [JsonProperty("slotLabelFormat")]
        public CalendarDateFormatter[] SlotLabelFormat { get; set; }

        [JsonProperty("slotLabelInterval")]
        public CalendarDurationObject SlotLabelInterval { get; set; }

        [JsonProperty("dayHeaderFormat")]
        public CalendarDateFormatter DayHeaderFormat { get; set; }

        [JsonProperty("allDaySlot")]
        public bool? AllDaySlot { get; set; }

        [JsonProperty("allDayText")]
        public string AllDayText { get; set; }

        [JsonProperty("slotMinTime")]
        public CalendarDurationObject SlotMinTime { get; set; }

        [JsonProperty("slotMaxTime")]
        public CalendarDurationObject SlotMaxTime { get; set; }

        public CalendarViewDefinition() { }

        public CalendarViewDefinition(string viewType, string viewButtonText, CalendarDurationObject viewDuration = null,
            CalendarDateFormatter[] slotLabelFormatDefault = null, CalendarDateFormatter columnHeaderFormatDefault = null)
        {
            Type = viewType;
            ButtonText = viewButtonText;
            Duration = viewDuration;
            SlotLabelFormat = slotLabelFormatDefault;
            DayHeaderFormat = columnHeaderFormatDefault;
        }

        public static CalendarViewDefinition DayGridWeeks(int numberOfWeeks, string label="Weeks", CalendarDateFormatter[] slotLabelFormatDefault = null, CalendarDateFormatter columnHeaderFormatDefault = null) 
            => new CalendarViewDefinition()
        {
            Type = CalendarPluginTypes.DayGrid,
            ButtonText = label,
            Duration = CalendarDurationObject.FromWeeks(numberOfWeeks),
            SlotLabelFormat = slotLabelFormatDefault,
            DayHeaderFormat = columnHeaderFormatDefault
        };

        public static CalendarViewDefinition DayGridMonths(int numberOfMonths, string label = "Months", CalendarDateFormatter[] slotLabelFormatDefault = null, CalendarDateFormatter columnHeaderFormatDefault = null)
            => new CalendarViewDefinition()
        {
            Type = CalendarPluginTypes.DayGrid,
            ButtonText = label,
            Duration = CalendarDurationObject.FromMonths(numberOfMonths),
            SlotLabelFormat = slotLabelFormatDefault,
            DayHeaderFormat = columnHeaderFormatDefault
        };
    }
}
