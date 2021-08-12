using System.Collections.Generic;
using Newtonsoft.Json;
using BlazorFullCalendar.Contracts;

namespace BlazorFullCalendar.Data
{
    public class CalendarSettings : JsonSerializable
    {
        //[JsonProperty("schedulerLicenseKey")]
        //public string SchedulerLicenseKey { get; set; } = "CC-Attribution-NonCommercial-NoDerivatives";

        [JsonProperty("initialView")]
        public string InitialView { get; set; }

        [JsonProperty("plugins")]
        public string[] Plugins { get; set; }

        [JsonProperty("refetchResourcesOnNavigate")]
        public bool? RefetchResourcesOnNavigate { get; set; }

        [JsonProperty("editable")]
        public bool? Editable { get; set; } = false;

        [JsonProperty("droppable")]
        public bool? Droppable { get; set; } = false;

        //Allow events’ start times to be editable through dragging.
        [JsonProperty("eventStartEditable")]
        public bool? EventStartEditable { get; set; }

        [JsonProperty("eventResizableFromStart")]
        public bool? EventResizableFromStart { get; set; }

        [JsonProperty("eventDurationEditable")]
        public bool? EventDurationEditable { get; set; }

        [JsonProperty("eventResourceEditable")]
        public bool? EventResourceEditable { get; set; }

        [JsonProperty("themeSystem")]
        public string ThemeSystem { get; set; } = "standard";

        //Called when an external draggable element or an event from another calendar has 
        //been dropped onto the calendar.
        [JsonProperty("drop")]
        public string Drop { get; set; }

        //Triggered also when existing element is dragged.
        [JsonProperty("eventDrop")]
        public string EventDrop { get; set; }

        //Triggered when event drag starts. Returns state BEFORE change!
        [JsonProperty("eventDragStart")]
        public string EventDragStart { get; set; }

        //Triggered when event drag stops. Returns state BEFORE change!
        [JsonProperty("eventDragStop")]
        public string EventDragStop { get; set; }

        //Triggered when event resizing finishes. Returns state AFTER change!
        [JsonProperty("eventResize")]
        public string EventResize { get; set; }

        //Triggered when event resizing starts. Returns state BEFORE change!
        [JsonProperty("eventResizeStart")]
        public string EventResizeStart { get; set; }

        //Triggered when event resizing stops. Returns state BEFORE change!
        [JsonProperty("eventResizeStop")]
        public string EventResizeStop { get; set; }

        [JsonProperty("eventClick")]
        public string EventClick { get; set; }

        [JsonProperty("eventMouseEnter")]
        public string EventMouseEnter { get; set; }

        [JsonProperty("eventMouseLeave")]
        public string EventMouseLeave { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("timeZone")]
        public string TimeZone { get; set; }

        //Sunday=0, Monday=1, Tuesday=2, etc.
        [JsonProperty("firstDay")]
        public int FirstDay { get; set; }

        [JsonProperty("slotMinTime")]
        public CalendarDurationObject SlotMinTime { get; set; }

        [JsonProperty("slotMaxTime")]
        public CalendarDurationObject SlotMaxTime { get; set; }

        [JsonProperty("headerToolbar")]
        public CalendarHeader HeaderToolbar { get; set; }

        [JsonProperty("duration")]
        public CalendarDurationObject Duration { get; set; }

        [JsonProperty("slotLabelFormat")]
        public CalendarDateFormatter[] SlotLabelFormat { get; set; }

        [JsonProperty("slotLabelInterval")]
        public CalendarDurationObject SlotLabelInterval { get; set; }

        [JsonProperty("resources")]
        public CalendarResourceFeed Resources { get; set; }

        [JsonProperty("views")]
        public Dictionary<string, CalendarViewDefinition> Views { get; set; }

        [JsonProperty("resourceColumns")]
        public List<CalendarResourceColumn> ResourceColumns { get; set; }

        [JsonProperty("eventSources")]
        public List<CalendarSourceFeed> EventSources { get; set; }

        [JsonProperty("events")]
        public CalendarDateItem[] Events { get; set; }

        [JsonProperty("eventTimeFormat")]
        public CalendarEventTimeFormat EventTimeFormat { get; set; }
    }
}
