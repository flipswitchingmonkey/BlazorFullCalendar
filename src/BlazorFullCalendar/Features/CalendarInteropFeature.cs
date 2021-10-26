using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using BlazorFullCalendar.Data;

namespace BlazorFullCalendar.Features
{
    public interface ICalendarInteropService : IAsyncDisposable
    {
        Task CalendarInitAsync(CalendarSettings settings, DotNetObjectReference<FullCalendar> dotNetRef);
        Task CalendarDisposeAsync();
        Task CalendarChangeDurationAsync(CalendarDurationObject duration);
        Task CalendarSetOptionAsync(string option, dynamic value);
        Task CalendarChangeResourceFeedAsync(CalendarResourceFeed calendarResourceFeed);
        Task CalendarRefetchEventsAsync();
        Task SetDotNetReferenceAsync(DotNetObjectReference<FullCalendar> reference);
    }

    public class CalendarInteropFeature : ICalendarInteropService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly string _calendarDivId;
        private readonly DotNetObjectReference<CalendarInteropFeature> _objRef;

        public CalendarInteropFeature(string calendarDivId, IJSRuntime jsRuntime)
        {
            _calendarDivId = calendarDivId;
            _jsRuntime = jsRuntime;
            _objRef = DotNetObjectReference.Create(this);
        }


        public async Task CalendarInitAsync(CalendarSettings settings, DotNetObjectReference<FullCalendar> dotNetRef)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync(
                    "BlazorFullCalendar.addFCWrapperInstance",
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

        public async Task CalendarDisposeAsync()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync(
                    "BlazorFullCalendar.deleteFCWrapperInstance",
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

        public async Task CalendarChangeDurationAsync(CalendarDurationObject duration)
        {
            try
            {
                await _jsRuntime.InvokeAsync<string>(
                    "BlazorFullCalendar.interop.calendarChangeDuration",
                    _calendarDivId,
                    duration.ToJson()
                );
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }

        public async Task CalendarSetOptionAsync(string option, dynamic value)
        {
            string json = JsonConvert.SerializeObject(value);
            try
            {
                await _jsRuntime.InvokeAsync<string>(
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

        public async Task CalendarChangeResourceFeedAsync(CalendarResourceFeed calendarResourceFeed)
        {
            try
            {
                await _jsRuntime.InvokeAsync<string>(
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

        public async Task CalendarRefetchEventsAsync()
        {
            try
            {
                await _jsRuntime.InvokeAsync<string>(
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

        public async Task SetDotNetReferenceAsync(DotNetObjectReference<FullCalendar> reference)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync(
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

        public async ValueTask DisposeAsync()
        {
            await CalendarDisposeAsync();

            _objRef?.Dispose();
        }
    }
}
