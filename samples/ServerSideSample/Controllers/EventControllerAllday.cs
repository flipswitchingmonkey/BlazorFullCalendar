using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorFullCalendar.Data;
using ServerSideSample.Pages;


namespace ServerSideSample.Controllers
{
    [Route("api/EventsAllday")]
    [ApiController]
    public class EventControllerAllday : ControllerBase
    {
        public EventControllerAllday()
        {
        }

        [HttpGet]
        public List<CalendarDateItem> GetEventsDateRange()
        {
            return CalendarPageBase.GetRandomEvents(10, allday: true, maxEventDuration:240, label:"Project");
        }

        [HttpGet("{amount}")]
        public List<CalendarDateItem> GetEventsDateRange(int amount)
        {
            return CalendarPageBase.GetRandomEvents(amount, allday:true, maxEventDuration: 240, label: "Project");
        }
    }
}
