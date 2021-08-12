using BlazorFullCalendar.Data;
using BlazorFullCalendar.Features;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorFullCalendar
{
    public partial class FullCalendar : IAsyncDisposable
    {
        private static uint instanceNumber = 0;

        [Inject] private IJSRuntime _jsRuntime { get; set; }

        [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> ExtraAttributes { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string Id { get; set; } = "calendar";
        [Parameter] public CalendarSettings settings { get; set; }

        [Parameter] public EventCallback<CalendarEventChangeResponse> OnDrop { get; set; }

        [Parameter] public EventCallback<CalendarEventChangeResponse> OnEventChange { get; set; }
        [Parameter] public EventCallback<CalendarEventChangeResponse> OnEventClick { get; set; }

        [Parameter] public EventCallback<CalendarEventChangeResponse> OnEventDrop { get; set; }

        [Parameter] public EventCallback<CalendarEventChangeResponse> OnEventResize { get; set; }
        [Parameter] public EventCallback<CalendarEventChangeResponse> OnEventResizeStart { get; set; }
        [Parameter] public EventCallback<CalendarEventChangeResponse> OnEventResizeStop { get; set; }

        [Parameter] public EventCallback<CalendarEventChangeResponse> OnEventMouseEnter { get; set; }
        [Parameter] public EventCallback<CalendarEventChangeResponse> OnEventMouseLeave { get; set; }

        [Parameter] public EventCallback<CalendarEventChangeResponse> OnEventDragStart { get; set; }
        [Parameter] public EventCallback<CalendarEventChangeResponse> OnEventDragStop { get; set; }

        private CalendarInteropFeature _interop;
        private DotNetObjectReference<FullCalendar> _objRef;
        private ElementReference _calendarDivReference;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            instanceNumber++;
            Id += instanceNumber.ToString();
        }

        private async Task Refetch()
        {
            await _interop.CalendarRefetchEventsAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new CalendarInteropFeature(Id, _jsRuntime);
                _objRef = DotNetObjectReference.Create(this);
                await InitCalendar();
                await _interop.SetDotNetReferenceAsync(_objRef);
            }
        }

        public async Task InitCalendar()
        {
            await _interop.CalendarInitAsync(settings, _objRef);
            await InvokeAsync(() => { StateHasChanged(); });
        }

        public async Task ChangeResourceFeed(CalendarResourceFeed calendarResourceFeed)
        {
            await _interop.CalendarChangeResourceFeedAsync(calendarResourceFeed);
            await InvokeAsync(() => { StateHasChanged(); });
        }

        public async Task CalendarRefetchEvents()
        {
            await _interop.CalendarRefetchEventsAsync();
            await InvokeAsync(() => { StateHasChanged(); });
        }

        public async Task ChangeSlotWidth(int value)
        {
            await _interop.CalendarSetOptionAsync("slotWidth", value);
            await InvokeAsync(() => { StateHasChanged(); });
        }

        [JSInvokable("OnDropCallback")]
        public async Task OnDropCallback(string returnValue)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
                await OnDrop.InvokeAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [JSInvokable("OnEventChangeCallback")]
        public async Task OnEventChangeCallback(string returnValue)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
                await OnEventChange.InvokeAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [JSInvokable("OnEventClickCallback")]
        public async Task OnEventClickCallback(string returnValue)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
                await OnEventClick.InvokeAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [JSInvokable("OnEventDropCallback")]
        public async Task OnEventDropCallback(string returnValue)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
                await OnEventDrop.InvokeAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [JSInvokable("OnEventResizeCallback")]
        public async Task OnEventResizeCallback(string returnValue)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
                await OnEventResize.InvokeAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [JSInvokable("OnEventResizeStartCallback")]
        public async Task OnEventResizeStartCallback(string returnValue)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
                await OnEventResizeStart.InvokeAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [JSInvokable("OnEventResizeStopCallback")]
        public async Task OnEventResizeStopCallback(string returnValue)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
                await OnEventResizeStop.InvokeAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [JSInvokable("OnEventMouseEnterCallback")]
        public async Task OnEventMouseEnterCallback(string returnValue)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
                await OnEventMouseEnter.InvokeAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [JSInvokable("OnEventMouseLeaveCallback")]
        public async Task OnEventMouseLeaveCallback(string returnValue)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
                await OnEventMouseLeave.InvokeAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [JSInvokable("OnEventDragStartCallback")]
        public async Task OnEventDragStartCallback(string returnValue)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
                await OnEventDragStart.InvokeAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [JSInvokable("OnEventDragStopCallback")]
        public async Task OnEventDragStopCallback(string returnValue)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
                await OnEventDragStop.InvokeAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_interop != null)
            {
                await _interop.DisposeAsync();
            }

            _objRef?.Dispose();
        }
    }
}
