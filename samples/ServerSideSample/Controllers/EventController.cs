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
    [Route("api/Events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        public EventController()
        {
        }

        [HttpGet]
        public List<CalendarDateItem> GetEventsDateRange()
        {
            return CalendarPageBase.GetRandomEvents(50);
        }

        [HttpGet("{amount}")]
        public List<CalendarDateItem> GetEventsDateRange(int amount)
        {
            return CalendarPageBase.GetRandomEvents(amount);
        }

    }
}
