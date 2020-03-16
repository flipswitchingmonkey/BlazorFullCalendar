# BlazorFullCalendar
This is a (fully functional, but as of yet partially implemented) Blazor wrapper for the excellent https://fullcalendar.io/ javascript calendar. 

![image](https://user-images.githubusercontent.com/6930367/76774215-efe03580-67a3-11ea-989e-6faed68d85af.png)

## Getting started

Currently there is no Nuget package yet, so you need to include the project into your own Blazor solution.

In you `_Host.cshtml` load the required fullcalendar scripts. You probably don't need all of them, depending on what functions you want to use. Refer to the fullcalendar documentation for this please.

    <link href='https://unpkg.com/@@fullcalendar/core@@4.4.0/main.min.css' rel='stylesheet' />
    <link href='https://unpkg.com/@@fullcalendar/daygrid@@4.4.0/main.min.css' rel='stylesheet' />
    <link href='https://unpkg.com/@@fullcalendar/timegrid@@4.4.0/main.min.css' rel='stylesheet' />
    <link href='https://unpkg.com/@@fullcalendar/timeline@@4.4.0/main.min.css' rel='stylesheet' />
    <link href='https://unpkg.com/@@fullcalendar/resource-timeline@@4.4.0/main.min.css' rel='stylesheet' />

    <script src="https://unpkg.com/@@fullcalendar/bootstrap@@4.4.0/main.min.js"></script>
    <script src='https://unpkg.com/@@fullcalendar/core@@4.4.0/main.min.js'></script>
    <script src='https://unpkg.com/@@fullcalendar/interaction@@4.4.0/main.min.js'></script>
    <script src='https://unpkg.com/@@fullcalendar/daygrid@@4.4.0/main.min.js'></script>
    <script src='https://unpkg.com/@@fullcalendar/timegrid@@4.4.0/main.min.js'></script>
    <script src='https://unpkg.com/@@fullcalendar/timeline@@4.4.0/main.min.js'></script>
    <script src='https://unpkg.com/@@fullcalendar/resource-common@@4.4.0/main.min.js'></script>
    <script src='https://unpkg.com/@@fullcalendar/resource-timeline@@4.4.0/main.min.js'></script>
    <script src='https://unpkg.com/@@fullcalendar/google-calendar@@4.4.0/main.min.js'></script>

Also in you `_Host.cshtml` add this line to load the custom frontend interop scripts:

    <script src="_content/BlazorFullCalendar/BlazorFullCalendar.js"></script>
    
There is nothing else to be done server-side.

The simplest way to implement the calendar is like this:

    <FullCalendar settings="@calendarSettings" />

    @code {
        public CalendarSettings calendarSettings { get; set; }  = new CalendarSettings()
        {
            plugins = new[] { CalendarPluginTypes.DayGrid, CalendarPluginTypes.Interaction },
            defaultView = "dayGridWeek",
            timeZone = "UTC",
            eventSources = new List<CalendarSourceFeed>()
            {
                new CalendarSourceFeed() { url = "https://fullcalendar.io/demo-events.json", extraParams = new Dictionary<string, string>(){ { "custom","dummy"} } }
            }
        };
    }
    
Basically what you do is put a FullCalendar component on your Razor page and then pass a CalendarSettings object to the component. CalendarSettings (and the associated CalendarViews etc) can get pretty complex. Have a look at the sample pages. I tried to stay close to the way the javascript configuration works.

Many, but by far not every possible option is implemented yet. If you're missing some option, it should be trivial to add (or just open an Issue and I'll see when I get around to add it).

Currently this release works with v4 of FullCalendar. As I understand it, v5 is around the corner and I plan to support it as well, once it goes live (and stable).

