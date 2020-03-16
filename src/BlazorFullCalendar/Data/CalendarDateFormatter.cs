using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorFullCalendar.Data
{
    public class CalendarDateFormatter
    {
        //'numeric' will produce something like 2018
        //'2-digit' will produce something like 18
        public string year { get; set; }	

        //will produce something like September
        //'short' will produce something like Sep
        //'narrow' will produce something like S
        //'numeric' will produce something like 1
        //'2-digit' will produce something like 01
        public string month	{ get; set; }

        //The day of the month.If the date were Jun 3, 2018
        //'numeric' will produce something like 3
        //'2-digit' will produce something like 03
        public string day { get; set; }

        //The day of the week.
        //'long' will produce something like Wednesday
        //'short' will produce something like Wed
        //'narrow' will produce something like W
        public string weekday { get; set; }	
 
        //If the time were 6:05
        //'numeric' would produce something like 6
        //'2-digit' would produce something like 06
        public string hour { get; set; }

        //If the time were 6:05
        //'numeric' would produce something like 5
        //'2-digit' would produce something like 05
        public string minute { get; set; }  

        //'numeric' or '2-digit'
        public string second { get; set; }  

        //true for a 12-hour clock, false for a 24-hour clock
        public bool? hour12 { get; set; }  

        //'short' ('long' is not supported by FullCalendar)
        public string timeZoneName { get; set; }    

        //'short' will produce something like Wk 8
        //'narrow' will produce something like Wk8
        //'numeric' will produce something like 8
        //This flag cannot be combined with any other flags!
        public string week { get; set; }	

        // Normally with a 12-hour clock the meridiem displays as A.M./P.M.
        //'lowercase' will force it to display like a.m./p.m.
        //'short' will force it to display like am/pm
        //'narrow' will force it to display like a/p
        //false will prevent it from displaying altogether
        public string meridiem { get; set; }	
    
        //if true, times like 6:00 will display as 6
        public bool? omitZeroMinute { get; set; }	

        //if true, all commas will be stripped from the outputted string
        public bool? omitCommas { get; set; }	
    }
}
