using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFullCalendar.Data
{
    public class CalendarDateItem
    {
        //Will uniquely identify your event. Useful for getEventById.
        public string id { get; set; }

        //Events that share a groupId will be dragged and resized together automatically.
        public string groupId { get; set; } = "";

        //Determines if the event is shown in the “all-day” section of the view,
        //if applicable. Determines if time text is displayed in the event. 
        //If this value is not specified, it will be inferred by the start and end properties.
        public bool? allDay { get; set; } = false;

        //The text that will appear on an event.
        public string title { get; set; }

        //A URL that will be visited when this event is clicked by the user.
        //For more information on controlling this behavior, see the eventClick callback.
        public string url { get; set; } = "";
        
        public string className { 
            get => classNames;
            set => classNames=value;
        }

        //A single string like 'myclass', a space-separated string like 'myclass1 myclass2', 
        //or an array of strings like['myclass1', myclass2' ]. 
        //Determines which HTML classNames will be attached to the rendered event.
        public string classNames { get; set; }

        //The days of the week this event repeats.An array of integers 
        //representing days e.g. [0, 1] for an event that repeats on Sundays and Mondays.
        public int[] daysOfWeek { get; set; }
        
        public DateTime? start { get; set; }
        
        public DateTime? end { get; set; }

        //The time of day the event starts.
        public DateTime? startTime { get; set; }

        //The time of day the event ends.
        public DateTime? endTime { get; set; }

        //When recurrences of the event start.
        public DateTime? startRecur { get; set; }

        //When recurrences of the event end.
        public DateTime? endRecur { get; set; }

        //Overrides the master editable option for this single event.
        public bool? editable { get; set; }

        //Overrides the master eventStartEditable option for this single event.
        public bool? startEditable { get; set; }

        //Overrides the master eventDurationEditable option for this single event.
        public bool? durationEditable { get; set; }

        //Overrides the master eventResourceEditable option for this single event.
        //Requires one of the resource plugins.
        public bool? resourceEditable { get; set; }

        //Allows alternate rendering of the event, like background events.
        //Can be empty, "background", or "inverse-background"
        public string rendering { get; set; }

        //Overrides the master eventOverlap option for this single event. 
        //If false, prevents this event from being dragged/resized over other events.
        //Also prevents other events from being dragged/resized over this event.
        public bool? overlap { get; set; }

        //A groupId belonging to other events, "businessHours", or an object. 
        //Overrides the master eventConstraint option for this single event.
        public string constraint { get; set; }

        //An alias for specifying the backgroundColor and borderColor at the same time.
        public string color { get; set; }

        //Sets an event’s background color just like the calendar-wide eventBackgroundColor option.
        public string backgroundColor { get; set; }

        //Sets an event’s border color just like the calendar-wide eventBorderColor option.
        public string borderColor { get; set; }

        //Sets an event’s text color just like the calendar-wide eventTextColor option.
        public string textColor { get; set; }

        //A plain object with any miscellaneous properties.It will be directly transferred 
        //to the extendedProps hash in each Event Object.
        //Often, these props are useful in a custom eventRender callback.
        public Dictionary<string,string> extendedProps { get; set; }

        //The string ID of a Resource.See Associating Events with Resources.
        //Requires one of the resource plugins.
        public string resourceId {
            get => resourceIds?.Length > 0 ? resourceIds[0] : null;
            set => resourceIds = new string[] { value };
        }

        //An array of string IDs of Resources. See Associating Events with Resources.
        //Requires one of the resource plugins.
        public string[] resourceIds { get; set; }
    }
}
