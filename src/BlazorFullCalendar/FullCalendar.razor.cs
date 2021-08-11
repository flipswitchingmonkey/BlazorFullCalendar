using BlazorFullCalendar.Data;
using BlazorFullCalendar.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorFullCalendar
{
    public partial class FullCalendar : IDisposable
    {
        ElementReference elem;

        private static uint instanceNumber = 0;

        [Inject] private IJSRuntime jsRuntime { get; set; }

        [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> ExtraAttributes { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string Id { get; set; } = "calendar";
        [Parameter] public CalendarSettings settings { get; set; }
        [Parameter] public EventCallback<CalendarEventChangeResponse> OnAddEvent { get; set; }
        [Parameter] public EventCallback<CalendarEventChangeResponse> OnUpdateEvent { get; set; }
        [Parameter] public EventCallback<CalendarEventChangeResponse> OnResizeEvent { get; set; }
        [Parameter] public EventCallback<CalendarEventChangeResponse> OnClickEvent { get; set; }
        [Parameter] public EventCallback<CalendarEventChangeResponse> OnDropEvent { get; set; }

        private CalendarInteropService interop;
        private DotNetObjectReference<FullCalendar> _objRef;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            instanceNumber++;
            Id += instanceNumber.ToString();
        }

        private async Task Refetch()
        {
            await interop.CalendarRefetchEvents();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                interop = new CalendarInteropService(Id, jsRuntime);
                _objRef = DotNetObjectReference.Create(this);
                await InitCalendar();
                await interop.SetDotNetReference(_objRef);
            }
        }

        public async Task InitCalendar()
        {
            await interop.CalendarInit(settings, _objRef);
            await InvokeAsync(() => { StateHasChanged(); });
        }

        public async Task ChangeResourceFeed(CalendarResourceFeed calendarResourceFeed)
        {
            await interop.CalendarChangeResourceFeed(calendarResourceFeed);
            await InvokeAsync(() => { StateHasChanged(); });
        }

        public async Task CalendarRefetchEvents()
        {
            await interop.CalendarRefetchEvents();
            await InvokeAsync(() => { StateHasChanged(); });
        }

        public async Task ChangeSlotWidth(int value)
        {
            await interop.CalendarSetOption("slotWidth", value);
            await InvokeAsync(() => { StateHasChanged(); });
        }

        [JSInvokable("AddEventCallback")]
        public async Task AddEventCallback(string returnValue)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
                await OnAddEvent.InvokeAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [JSInvokable("UpdateEventCallback")]
        public async Task UpdateEventCallback(string returnValue)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
                await OnUpdateEvent.InvokeAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [JSInvokable("ResizeEventCallback")]
        public async Task ResizeEventCallback(string returnValue)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
                await OnResizeEvent.InvokeAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [JSInvokable("ClickEventCallback")]
        public async Task ClickEventCallback(string returnValue)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
                await OnClickEvent.InvokeAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [JSInvokable("DropEventCallback")]
        public async Task DropEventCallback(string returnValue)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
                await OnDropEvent.InvokeAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Dispose()
        {
            //instanceNumber--;

            if (interop != null)
                interop.Dispose();

            _objRef?.Dispose();
        }
    }
}
