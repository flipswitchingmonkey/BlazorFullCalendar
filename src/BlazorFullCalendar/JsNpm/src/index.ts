// Bootstrap
//import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";

// fontawesome
import "@fortawesome/fontawesome-free/css/all.min.css";

// FullCalendar
import { Calendar, PluginDef } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import listPlugin from '@fullcalendar/list';
import interactionPlugin, { Draggable } from '@fullcalendar/interaction';
import googleCalendarPlugin from '@fullcalendar/google-calendar';
import bootstrapPlugin from '@fullcalendar/bootstrap';

// moment.js
const moment = require('moment');

// replacer function for json stringify to filter out null values
function replaceNullWithUndefined(key, value) {
    if (value === null) {
        return undefined;
    }

    return value;
}

export const getCircularReplacer = () => {
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

export const FullCalendarWrapper = {
    calendarRef: null,
    draggables: null,
    dotNetReference: null,
    bakeInUtcTime: (date) => {
        return moment(date).add(moment(date).utcOffset(), "minutes")
    },
    buildCalendarEventChangeResponse: (info) => {

        var response: any = {};

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

        return JSON.stringify(response, FullCalendarWrapper.replaceNullWithUndefined);
    },
    replaceNullWithUndefined: (key, value) => {
        // console.log(key, value);
        if (value === null || value === "null") {
            return undefined;
        }
        return value;
    },


    interop: {
        getElementByName: (name) => {
            var elements = document.getElementsByName(name);
            console.log(elements);

            if (elements.length) {
                return elements[0];
            } else {
                return "";
            }
        },
        calendarInit: (elementName, settings) => {
            //console.log(settings);
            var calendarSettings = JSON.parse(settings);
            //console.log(calendarSettings);
            var calendarEl = document.getElementById(elementName);

            let plugins: PluginDef[] = [];
            for (let it of calendarSettings.plugins) {
                if (it.includes('dayGrid')) {
                    plugins.push(dayGridPlugin);
                } else if (it.includes('interaction')) {
                    plugins.push(interactionPlugin);
                } else if (it.includes('timeGrid')) {
                    plugins.push(timeGridPlugin);
                } else if (it.includes('list')) {
                    plugins.push(listPlugin);
                } else if (it.includes('bootstrap')) {
                    plugins.push(bootstrapPlugin);
                } else if (it.includes('googleCalendar')) {
                    plugins.push(googleCalendarPlugin);
                }
            }

            calendarSettings.plugins = plugins;

            calendarSettings.eventResize = FullCalendarWrapper.interop.calendarOnEventResize;

            calendarSettings.eventChange = FullCalendarWrapper.interop.calendarOnEventChange;

            if (calendarSettings.eventResizeStart !== null)
                calendarSettings.eventResizeStart = eval(calendarSettings.eventResizeStart);

            if (calendarSettings.eventResizeStop !== null)
                calendarSettings.eventResizeStop = eval(calendarSettings.eventResizeStop);

            calendarSettings.drop = FullCalendarWrapper.interop.calendarOnDrop;

            calendarSettings.eventDrop = FullCalendarWrapper.interop.calendarOnEventDrop;

            calendarSettings.eventClick = FullCalendarWrapper.interop.calendarOnEventClick;

            if (calendarSettings.eventMouseEnter !== null)
                calendarSettings.eventMouseEnter = eval(calendarSettings.eventMouseEnter);

            if (calendarSettings.eventMouseLeave !== null)
                calendarSettings.eventMouseLeave = eval(calendarSettings.eventMouseLeave);

            if (calendarSettings.eventDragStart !== null)
                calendarSettings.eventDragStart = eval(calendarSettings.eventDragStart);

            if (calendarSettings.eventDragStop !== null)
                calendarSettings.eventDragStop = eval(calendarSettings.eventDragStop);

            if (calendarSettings.droppable === true) {
                FullCalendarWrapper.draggables = new Draggable(document.getElementById('external-events'), {
                    itemSelector: '.fc-event',
                    eventData: function (eventEl) {
                        return {
                            title: eventEl.innerText
                        };
                    }
                });
            }
            // console.log(calendarSettings);
            FullCalendarWrapper.calendarRef = new Calendar(calendarEl, calendarSettings);
            // console.log(BlazorFullCalendar.calendarRef);
            FullCalendarWrapper.calendarRef.render();
        },
        calendarChangeDuration: (units, amount) => {
            if (FullCalendarWrapper.calendarRef !== null) {
                FullCalendarWrapper.calendarRef.setOption('duration', { units: amount });
            }
        },
        calendarSetOption: (option, value) => {
            if (FullCalendarWrapper.calendarRef !== null) {
                FullCalendarWrapper.calendarRef.setOption(option, value);
            }
        },
        calendarRefetchResources: (newCalendarResourceFeed) => {
            if (FullCalendarWrapper.calendarRef !== null) {
                FullCalendarWrapper.calendarRef.setOption('resources', newCalendarResourceFeed);
                FullCalendarWrapper.calendarRef.refetchResources();
            }
        },
        calendarRefetchEvents: () => {
            if (FullCalendarWrapper.calendarRef !== null) {
                FullCalendarWrapper.calendarRef.refetchEvents();
            }
        },
        calendarOnDrop: (info) => {
            if (FullCalendarWrapper.calendarRef !== null) {
                FullCalendarWrapper.interop.callBackDotNetWithInfo('AddEventCallback', info);
                info.remove();
            }
        },
        calendarOnEventResize: (info) => {
            if (FullCalendarWrapper.calendarRef !== null) {
                FullCalendarWrapper.interop.callBackDotNetWithInfo('ResizeEventCallback', info);
            }
        },
        calendarOnEventChange: (info) => {
            if (FullCalendarWrapper.calendarRef !== null) {
                FullCalendarWrapper.interop.callBackDotNetWithInfo('UpdateEventCallback', info);
            }
        },
        calendarOnEventClick: (info) => {
            if (FullCalendarWrapper.calendarRef !== null) {
                FullCalendarWrapper.interop.callBackDotNetWithInfo('ClickEventCallback', info);
            }
        },
        calendarOnEventDrop: (info) => {
            if (FullCalendarWrapper.calendarRef !== null) {
                FullCalendarWrapper.interop.callBackDotNetWithInfo('DropEventCallback', info);
            }
        },
        callBackDotNetWithInfo: (functionName, info) => {
            if (FullCalendarWrapper.dotNetReference !== null) {
                FullCalendarWrapper.dotNetReference.invokeMethodAsync(
                    functionName,
                    FullCalendarWrapper.buildCalendarEventChangeResponse(info)
                );
            }
        },
        SetDotNetReference: (ref) => {
            FullCalendarWrapper.dotNetReference = ref;
        },

    }
};
