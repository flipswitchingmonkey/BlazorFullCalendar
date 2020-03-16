using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlazorFullCalendar.Data
{
    public class CalendarSettings
    {
        public string schedulerLicenseKey { get; set; } = "GPL-My-Project-Is-Open-Source";
        public string defaultView { get; set; }
        public string[] plugins { get; set; }
        public bool? refetchResourcesOnNavigate { get; set; }
        public bool? editable { get; set; } = false;
        public bool? droppable { get; set; } = false;

        //Allow events’ start times to be editable through dragging.
        public bool? eventStartEditable { get; set; }
        public bool? eventResizableFromStart { get; set; }
        public bool? eventDurationEditable { get; set; } 
        public bool? eventResourceEditable { get; set; }

        //Called when an external draggable element or an event from another calendar has 
        //been dropped onto the calendar.
        public string drop { get; set; }

        //Triggered also when existing element is dragged.
        public string eventDrop { get; set; }

        //Triggered when event drag starts. Returns state BEFORE change!
        public string eventDragStart { get; set; }

        //Triggered when event drag stops. Returns state BEFORE change!
        public string eventDragStop { get; set; }

        //Triggered when event resizing finishes. Returns state AFTER change!
        public string eventResize { get; set; }

        //Triggered when event resizing starts. Returns state BEFORE change!
        public string eventResizeStart { get; set; }

        //Triggered when event resizing stops. Returns state BEFORE change!
        public string eventResizeStop { get; set; }

        public string eventClick { get; set; }

        public string eventMouseEnter { get; set; }

        public string eventMouseLeave { get; set; }

        public string locale { get; set; }
        public string timeZone { get; set; }

        //Sunday=0, Monday=1, Tuesday=2, etc.
        public int firstDay { get; set; }
        public CalendarDurationObject minTime { get; set; }
        public CalendarDurationObject maxTime { get; set; }
        public CalendarHeader header { get; set; }
        //public Dictionary<string, int> duration { get; set; }
        public CalendarDurationObject duration { get; set; }
        public CalendarDateFormatter[] slotLabelFormat { get; set; }
        public CalendarDurationObject slotLabelInterval { get; set; }
        public CalendarResourceFeed resources { get; set; }
        public Dictionary<string, CalendarViewDefinition> views { get; set; }
        public List<CalendarResourceColumn> resourceColumns { get; set; }
        public List<CalendarSourceFeed> eventSources { get; set; }
        public CalendarDateItem[] events { get; set; }
        public CalendarEventTimeFormat eventTimeFormat { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}
