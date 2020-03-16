using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorFullCalendar.Data
{
    public class CalendarDurationObject
    {
        public int year { get; set; }
        public int years { get; set; }
        public int month { get; set; }
        public int months { get; set; }
        public int day { get; set; }
        public int days { get; set; }
        public int hour { get; set; }
        public int hours { get; set; }
        public int minute { get; set; }
        public int minutes { get; set; }
        public int second { get; set; }
        public int seconds { get; set; }
        public int millisecond { get; set; }
        public int milliseconds { get; set; }
        public int ms { get; set; }

        public CalendarDurationObject() { }
        public static CalendarDurationObject Days(int amount) => new CalendarDurationObject() { days = amount };
        public static CalendarDurationObject Weeks(int amount) => new CalendarDurationObject() { days = amount * 7 };
        public static CalendarDurationObject Months(int amount) => new CalendarDurationObject() { months = amount };
        public static CalendarDurationObject Years(int amount) => new CalendarDurationObject() { years = amount };
        public static CalendarDurationObject Hours(int amount) => new CalendarDurationObject() { hours = amount };
        public static CalendarDurationObject Minutes(int amount) => new CalendarDurationObject() { minutes = amount };
    }
}
