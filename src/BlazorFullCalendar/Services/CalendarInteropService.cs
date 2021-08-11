using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components;
using BlazorFullCalendar;
using BlazorFullCalendar.Data;

namespace BlazorFullCalendar.Services
{
    public interface ICalendarInteropService
    {
        Task CalendarInit(string elementName, CalendarSettings settings, DotNetObjectReference<ComponentBase> dotNetRef = null);
        Task CalendarChangeDuration(string unit, int amount);
        Task CalendarSetOption(string option, dynamic value);
        Task CalendarChangeResourceFeed(CalendarResourceFeed calendarResourceFeed);
        Task CalendarRefetchEvents();
        Task SetDotNetReference(DotNetObjectReference<FullCalendar> reference);
    }

    public class CalendarInteropService : ICalendarInteropService
    {
        private readonly IJSRuntime jsRuntime;

        public CalendarInteropService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }


        public async Task CalendarInit(string elementName, CalendarSettings settings, DotNetObjectReference<ComponentBase> dotNetRef = null)
        {
            try
            {
                await jsRuntime.InvokeAsync<ElementReference>(
                    "BlazorFullCalendar.FullCalendarWrapper.interop.calendarInit",
                    elementName,
                    settings.ToJson(),
                    dotNetRef
                );
                return;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }

        public Task CalendarChangeDuration(string unit, int amount)
        {
            try
            {
                jsRuntime.InvokeAsync<string>(
                    "BlazorFullCalendar.FullCalendarWrapper.interop.calendarChangeDuration",
                    unit,
                    amount
                    );
                return Task.CompletedTask;
            }
            catch
            {
                return Task.CompletedTask;
            }
        }

        public Task CalendarSetOption(string option, dynamic value)
        {
            string json = JsonConvert.SerializeObject(value);
            try
            {
                jsRuntime.InvokeAsync<string>(
                    "BlazorFullCalendar.FullCalendarWrapper.interop.calendarSetOption",
                    option,
                    json
                    );
                return Task.CompletedTask;
            }
            catch
            {
                return Task.CompletedTask;
            }
        }

        public Task CalendarChangeResourceFeed(CalendarResourceFeed calendarResourceFeed)
        {
            try
            {
                jsRuntime.InvokeAsync<string>(
                    "BlazorFullCalendar.FullCalendarWrapper.interop.calendarRefetchResources",
                    calendarResourceFeed
                    );
                return Task.CompletedTask;
            }
            catch
            {
                return Task.CompletedTask;
            }
        }

        public Task CalendarRefetchEvents()
        {
            try
            {
                jsRuntime.InvokeAsync<string>(
                    "BlazorFullCalendar.FullCalendarWrapper.interop.calendarRefetchEvents"
                    );
                return Task.CompletedTask;
            }
            catch
            {
                return Task.CompletedTask;
            }
        }

        public Task SetDotNetReference(DotNetObjectReference<FullCalendar> reference)
        {
            try
            {
                jsRuntime.InvokeVoidAsync(
                    "BlazorFullCalendar.FullCalendarWrapper.interop.SetDotNetReference",
                    reference);
                return Task.CompletedTask;
            }
            catch
            {
                return Task.CompletedTask;
            }
        }
    }
}
