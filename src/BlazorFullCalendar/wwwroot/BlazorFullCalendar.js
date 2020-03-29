// replacer function for json stringify to filter out null values
function replaceNullWithUndefined(key, value) {
    if (value === null) {
        return undefined;
    }
    return value;
}

const getCircularReplacer = () => {
    const seen = new WeakSet();
    return (key, value) => {
        if (typeof value === "object" && value !== null) {
            if (seen.has(value)) {
                return;
            }
            seen.add(value);
        }
        return value;
    };
};

window.BlazorFullCalendar = {
    calendarRef: null,
    draggables: null,
    dotNetReferenc: null,
    bakeInUtcTime: function (date) {
        return moment(date).add(moment(date).utcOffset(), "minutes")
    },
    buildCalendarEventChangeResponse: function (info) {

        var response = {};

        if (info.event !== undefined) {
            var startDate = moment(info.event.start);
            startDate.add(startDate.utcOffset(), "minutes")
            var endDate = moment(info.event.end);
            endDate.add(endDate.utcOffset(), "minutes")

            response = {
                Start: startDate,
                End: endDate,
                Id: info.event.id,
                GroupId: info.event.groupId,
                AllDay: info.event.allDay,
                Title: info.event.title,
                Url: info.event.url,
                Rendering: info.event.rendering,
                BackgroundColor: info.event.backgroundColor,
                TextColor: info.event.textColor,
                ClassNames: info.event.classNames,
                DaysOfWeek: info.event.extendedProps.daysOfWeek,
                StartTime: info.event.extendedProps.startTime,
                EndTime: info.event.extendedProps.endTime,
                StartRecur: info.event.extendedProps.startRecur,
                EndRecur: info.event.extendedProps.endRecur,
                ResourceId: info.event.extendedProps.resourceId,
                ResourceIds: info.event.extendedProps.resourceIds,
            };

            if (info.event.draggedEl !== undefined) {
                response.DataSet = JSON.parse(info.event.draggedEl.dataset.event);
            }
        }
        else if (info.draggedEl !== undefined) {
            response.DataSet = JSON.parse(info.draggedEl.dataset.event);

            var startDate = moment(info.date);
            startDate.add(startDate.utcOffset(), "minutes")
            var endDate = startDate.clone();
            endDate.add(response.DataSet.duration, "hours");

            response.Start = startDate;
            response.End = endDate;
            response.ResourceId = response.DataSet.resourceId;

        }

        return JSON.stringify(response, BlazorFullCalendar.replaceNullWithUndefined);
    },
    replaceNullWithUndefined: function (key, value) {
        // console.log(key, value);
        if (value === null || value === "null") {
            return undefined;
        }
        return value;
    },


    interop: {
        getElementByName: function (name) {
            var elements = document.getElementsByName(name);
            if (elements.length) {
                return elements[0].value;
            } else {
                return "";
            }
        },
        calendarInit: function (elementName, settings) {
            // console.log(settings);
            var calendarSettings = JSON.parse(settings);
            // console.log(calendarSettings);
            var calendarEl = document.getElementById(elementName);

            if (calendarSettings.eventResize !== null) calendarSettings.eventResize = eval(calendarSettings.eventResize);
            if (calendarSettings.eventResizeStart !== null) calendarSettings.eventResizeStart = eval(calendarSettings.eventResizeStart);
            if (calendarSettings.eventResizeStop !== null) calendarSettings.eventResizeStop = eval(calendarSettings.eventResizeStop);
            if (calendarSettings.drop !== null) calendarSettings.drop = eval(calendarSettings.drop);
            if (calendarSettings.eventDrop !== null) calendarSettings.eventDrop = eval(calendarSettings.eventDrop);
            if (calendarSettings.eventClick !== null) calendarSettings.eventClick = eval(calendarSettings.eventClick);
            if (calendarSettings.eventMouseEnter !== null) calendarSettings.eventMouseEnter = eval(calendarSettings.eventMouseEnter);
            if (calendarSettings.eventMouseLeave !== null) calendarSettings.eventMouseLeave = eval(calendarSettings.eventMouseLeave);
            if (calendarSettings.eventDragStart !== null) calendarSettings.eventDragStart = eval(calendarSettings.eventDragStart);
            if (calendarSettings.eventDragStop !== null) calendarSettings.eventDragStop = eval(calendarSettings.eventDragStop);

            if (calendarSettings.droppable === true) {
                BlazorFullCalendar.draggables = new FullCalendarInteraction.Draggable(document.getElementById('external-events'), {
                    itemSelector: '.fc-event',
                    eventData: function (eventEl) {
                        return {
                            title: eventEl.innerText
                        };
                    }
                });
            }
            // console.log(calendarSettings);
            BlazorFullCalendar.calendarRef = new FullCalendar.Calendar(calendarEl, calendarSettings);
            // console.log(BlazorFullCalendar.calendarRef);
            BlazorFullCalendar.calendarRef.render();
        },
        calendarChangeDuration: function (units, amount) {
            if (BlazorFullCalendar.calendarRef !== null) {
                BlazorFullCalendar.calendarRef.setOption('duration', { units: amount });
            }
        },
        calendarSetOption: function (option, value) {
            if (BlazorFullCalendar.calendarRef !== null) {
                BlazorFullCalendar.calendarRef.setOption(option, value);
            }
        },
        calendarRefetchResources: function (newCalendarResourceFeed) {
            if (BlazorFullCalendar.calendarRef !== null) {
                BlazorFullCalendar.calendarRef.setOption('resources', newCalendarResourceFeed);
                BlazorFullCalendar.calendarRef.refetchResources();
            }
        },
        calendarRefetchEvents: function () {
            if (BlazorFullCalendar.calendarRef !== null) {
                BlazorFullCalendar.calendarRef.refetchEvents();
            }
        },
        calendarOnDrop: function (info) {
            if (BlazorFullCalendar.calendarRef !== null) {
                BlazorFullCalendar.interop.callBackDotNetWithInfo('AddEventCallback', info);
                info.remove();
            }
        },
        calendarOnEventResize: function (info) {
            if (BlazorFullCalendar.calendarRef !== null) {
                BlazorFullCalendar.interop.callBackDotNetWithInfo('UpdateEventCallback', info);
            }
        },
        calendarOnEventClick: function (info) {
            if (BlazorFullCalendar.calendarRef !== null) {
                BlazorFullCalendar.interop.callBackDotNetWithInfo('ClickEventCallback', info);
            }
        },
        callBackDotNetWithInfo: function (functionName, info) {
            if (BlazorFullCalendar.dotNetReference !== null) {
                BlazorFullCalendar.dotNetReference.invokeMethodAsync(
                    functionName,
                    BlazorFullCalendar.buildCalendarEventChangeResponse(info)
                );
            }
        },
        SetDotNetReference: function (ref) {
            BlazorFullCalendar.dotNetReference = ref;
        },

    }
};