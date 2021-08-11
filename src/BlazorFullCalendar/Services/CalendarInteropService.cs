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
    public interface ICalendarInteropService : IDisposable
    {
        Task CalendarInit(CalendarSettings settings, DotNetObjectReference<FullCalendar> dotNetRef);
        Task CalendarDispose();
        Task CalendarChangeDuration(string unit, int amount);
        Task CalendarSetOption(string option, dynamic value);
        Task CalendarChangeResourceFeed(CalendarResourceFeed calendarResourceFeed);
        Task CalendarRefetchEvents();
        Task SetDotNetReference(DotNetObjectReference<FullCalendar> reference);
    }

    public class CalendarInteropService : ICalendarInteropService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly string _calendarDivId;
        private readonly DotNetObjectReference<CalendarInteropService> _objRef;

        public CalendarInteropService(string calendarDivId, IJSRuntime jsRuntime)
        {
            _calendarDivId = calendarDivId;
            _jsRuntime = jsRuntime;
            _objRef = DotNetObjectReference.Create(this);
        }


        public async Task CalendarInit(CalendarSettings settings, DotNetObjectReference<FullCalendar> dotNetRef)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync(
                    "BlazorFullCalendar.AddFCWrapperInstance",
                    _calendarDivId,
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

        public async Task CalendarDispose()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync(
                    "BlazorFullCalendar.DeleteFCWrapperInstance",
                    _calendarDivId
                );
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }

        public async Task CalendarChangeDuration(string unit, int amount)
        {
            try
            {
                await _jsRuntime.InvokeAsync<string>(
                    //$"BlazorFullCalendar.FCWrapperInstances.get('{_calendarDivId}']).FromDotNetInterop.CalendarChangeDuration",
                    "BlazorFullCalendar.interop.calendarChangeDuration",
                    _calendarDivId,
                    unit,
                    amount
                );
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }

        public async Task CalendarSetOption(string option, dynamic value)
        {
            string json = JsonConvert.SerializeObject(value);
            try
            {
                await _jsRuntime.InvokeAsync<string>(
                    //$"BlazorFullCalendar.FCWrapperInstances.get('{_calendarDivId}').FromDotNetInterop.CalendarSetOption",
                    "BlazorFullCalendar.interop.calendarSetOption",
                    _calendarDivId,
                    option,
                    json
                );
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }

        public async Task CalendarChangeResourceFeed(CalendarResourceFeed calendarResourceFeed)
        {
            try
            {
                await _jsRuntime.InvokeAsync<string>(
                    //$"BlazorFullCalendar.FCWrapperInstances.get('{_calendarDivId}').FromDotNetInterop.CalendarRefetchResources",
                    "BlazorFullCalendar.interop.calendarRefetchResources",
                    _calendarDivId,
                    calendarResourceFeed
                );
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }

        public async Task CalendarRefetchEvents()
        {
            try
            {
                await _jsRuntime.InvokeAsync<string>(
                    //$"BlazorFullCalendar.FCWrapperInstances.get('{_calendarDivId}').FromDotNetInterop.CalendarRefetchEvents"
                    "BlazorFullCalendar.interop.calendarRefetchEvents",
                    _calendarDivId
                );
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }

        public async Task SetDotNetReference(DotNetObjectReference<FullCalendar> reference)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync(
                    //$"BlazorFullCalendar.FCWrapperInstances.get('{_calendarDivId}').SetDotNetReference",
                    "BlazorFullCalendar.interop.setDotNetReference",
                    _calendarDivId,
                    reference
                );
                return;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return;
            }
        }

        public void Dispose()
        {
            CalendarDispose();

            _objRef?.Dispose();
        }
    }
}
