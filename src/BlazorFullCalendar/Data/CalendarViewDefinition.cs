using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorFullCalendar.Data
{
    public class CalendarViewDefinition
    {
        public string type { get; set; }
        public string buttonText { get; set; }
        //public Dictionary<string,float> duration { get; set; }
        public CalendarDurationObject duration { get; set; }
        public CalendarDateFormatter[] slotLabelFormat { get; set; }
        public CalendarDurationObject slotLabelInterval { get; set; }
        public CalendarDateFormatter columnHeaderFormat { get; set; }
        public bool? allDaySlot { get; set; }
        public string allDayText { get; set; }
        public CalendarDurationObject minTime { get; set; }
        public CalendarDurationObject maxTime { get; set; }

        public CalendarViewDefinition() { }

        public CalendarViewDefinition(string viewType, string viewButtonText, CalendarDurationObject viewDuration = null,
            CalendarDateFormatter[] slotLabelFormatDefault = null, CalendarDateFormatter columnHeaderFormatDefault = null)
        {
            type = viewType;
            buttonText = viewButtonText;
            duration = viewDuration;
            slotLabelFormat = slotLabelFormatDefault;
            columnHeaderFormat = columnHeaderFormatDefault;
        }

        public static CalendarViewDefinition DayGridWeeks(int numberOfWeeks, string label="Weeks", CalendarDateFormatter[] slotLabelFormatDefault = null, CalendarDateFormatter columnHeaderFormatDefault = null) 
            => new CalendarViewDefinition()
        {
            type = CalendarPluginTypes.DayGrid,
            buttonText = label,
            duration = CalendarDurationObject.Weeks(numberOfWeeks),
            slotLabelFormat = slotLabelFormatDefault,
            columnHeaderFormat = columnHeaderFormatDefault
        };

        public static CalendarViewDefinition DayGridMonths(int numberOfMonths, string label = "Months", CalendarDateFormatter[] slotLabelFormatDefault = null, CalendarDateFormatter columnHeaderFormatDefault = null)
            => new CalendarViewDefinition()
            {
                type = CalendarPluginTypes.DayGrid,
                buttonText = label,
                duration = CalendarDurationObject.Months(numberOfMonths),
                slotLabelFormat = slotLabelFormatDefault,
                columnHeaderFormat = columnHeaderFormatDefault
            };
    }
}
