using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlazorFullCalendar.Data
{
    public class CalendarDateItem : JsonSerializable
    {
        //Will uniquely identify your event. Useful for getEventById.
        [JsonProperty("id")]
        public string Id { get; set; }

        //Events that share a groupId will be dragged and resized together automatically.
        [JsonProperty("groupId")]
        public string GroupId { get; set; } = "";

        //Determines if the event is shown in the “all-day” section of the view,
        //if applicable. Determines if time text is displayed in the event. 
        //If this value is not specified, it will be inferred by the start and end properties.
        [JsonProperty("allDay")]
        public bool? AllDay { get; set; } = false;

        //The text that will appear on an event.
        [JsonProperty("title")]
        public string Title { get; set; }

        //A URL that will be visited when this event is clicked by the user.
        //For more information on controlling this behavior, see the eventClick callback.
        [JsonProperty("url")]
        public string Url { get; set; } = "";
        
        [JsonProperty("className")]
        public string ClassName { 
            get => ClassNames;
            set => ClassNames = value;
        }

        //A single string like 'myclass', a space-separated string like 'myclass1 myclass2', 
        //or an array of strings like['myclass1', myclass2' ]. 
        //Determines which HTML classNames will be attached to the rendered event.
        [JsonProperty("classNames")]
        public string ClassNames { get; set; }

        //The days of the week this event repeats.An array of integers 
        //representing days e.g. [0, 1] for an event that repeats on Sundays and Mondays.
        [JsonProperty("daysOfWeek")]
        public int[] DaysOfWeek { get; set; }
        
        [JsonProperty("start")]
        public DateTime? Start { get; set; }
        
        [JsonProperty("end")]
        public DateTime? End { get; set; }

        //The time of day the event starts.
        [JsonProperty("startTime")]
        public DateTime? StartTime { get; set; }

        //The time of day the event ends.
        [JsonProperty("endTime")]
        public DateTime? EndTime { get; set; }

        //When recurrences of the event start.
        [JsonProperty("startRecur")]
        public DateTime? StartRecur { get; set; }

        //When recurrences of the event end.
        [JsonProperty("endRecur")]
        public DateTime? EndRecur { get; set; }

        //Overrides the master editable option for this single event.
        [JsonProperty("editable")]
        public bool? Editable { get; set; }

        //Overrides the master eventStartEditable option for this single event.
        [JsonProperty("startEditable")]
        public bool? StartEditable { get; set; }

        //Overrides the master eventDurationEditable option for this single event.
        [JsonProperty("durationEditable")]
        public bool? DurationEditable { get; set; }

        //Overrides the master eventResourceEditable option for this single event.
        //Requires one of the resource plugins.
        [JsonProperty("resourceEditable")]
        public bool? ResourceEditable { get; set; }

        //Allows alternate rendering of the event, like background events.
        //Can be empty, "background", or "inverse-background"
        [JsonProperty("rendering")]
        public string Rendering { get; set; }

        //Overrides the master eventOverlap option for this single event. 
        //If false, prevents this event from being dragged/resized over other events.
        //Also prevents other events from being dragged/resized over this event.
        [JsonProperty("overlap")]
        public bool? Overlap { get; set; }

        //A groupId belonging to other events, "businessHours", or an object. 
        //Overrides the master eventConstraint option for this single event.
        [JsonProperty("constraint")]
        public string Constraint { get; set; }

        //An alias for specifying the backgroundColor and borderColor at the same time.
        [JsonProperty("color")]
        public string Color { get; set; }

        //Sets an event’s background color just like the calendar-wide eventBackgroundColor option.
        [JsonProperty("backgroundColor")]
        public string BackgroundColor { get; set; }

        //Sets an event’s border color just like the calendar-wide eventBorderColor option.
        [JsonProperty("borderColor")]
        public string BorderColor { get; set; }

        //Sets an event’s text color just like the calendar-wide eventTextColor option.
        [JsonProperty("textColor")]
        public string TextColor { get; set; }

        //A plain object with any miscellaneous properties.It will be directly transferred 
        //to the extendedProps hash in each Event Object.
        //Often, these props are useful in a custom eventRender callback.
        [JsonProperty("extendedProps")]
        public Dictionary<string,string> ExtendedProps { get; set; }

        //The string ID of a Resource.See Associating Events with Resources.
        //Requires one of the resource plugins.
        [JsonProperty("resourceId")]
        public string ResourceId {
            get => ResourceIds?.Length > 0 ? ResourceIds[0] : null;
            set => ResourceIds = new string[] { value };
        }

        //An array of string IDs of Resources. See Associating Events with Resources.
        //Requires one of the resource plugins.
        [JsonProperty("resourceIds")]
        public string[] ResourceIds { get; set; }
    }
}
