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

export const FullCalendar = {
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

        return JSON.stringify(response, FullCalendar.replaceNullWithUndefined);
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

            if (calendarSettings.eventResize !== null)
                calendarSettings.eventResize = eval(calendarSettings.eventResize);

            if (calendarSettings.eventResizeStart !== null)
                calendarSettings.eventResizeStart = eval(calendarSettings.eventResizeStart);

            if (calendarSettings.eventResizeStop !== null)
                calendarSettings.eventResizeStop = eval(calendarSettings.eventResizeStop);

            if (calendarSettings.drop !== null)
                calendarSettings.drop = eval(calendarSettings.drop);

            if (calendarSettings.eventDrop !== null)
                calendarSettings.eventDrop = eval(calendarSettings.eventDrop);

            if (calendarSettings.eventClick !== null)
                calendarSettings.eventClick = eval(calendarSettings.eventClick);

            if (calendarSettings.eventMouseEnter !== null)
                calendarSettings.eventMouseEnter = eval(calendarSettings.eventMouseEnter);

            if (calendarSettings.eventMouseLeave !== null)
                calendarSettings.eventMouseLeave = eval(calendarSettings.eventMouseLeave);

            if (calendarSettings.eventDragStart !== null)
                calendarSettings.eventDragStart = eval(calendarSettings.eventDragStart);

            if (calendarSettings.eventDragStop !== null)
                calendarSettings.eventDragStop = eval(calendarSettings.eventDragStop);

            if (calendarSettings.droppable === true) {
                FullCalendar.draggables = new Draggable(document.getElementById('external-events'), {
                    itemSelector: '.fc-event',
                    eventData: function (eventEl) {
                        return {
                            title: eventEl.innerText
                        };
                    }
                });
            }
            // console.log(calendarSettings);
            FullCalendar.calendarRef = new Calendar(calendarEl, calendarSettings);
            // console.log(BlazorFullCalendar.calendarRef);
            FullCalendar.calendarRef.render();
        },
        calendarChangeDuration: (units, amount) => {
            if (FullCalendar.calendarRef !== null) {
                FullCalendar.calendarRef.setOption('duration', { units: amount });
            }
        },
        calendarSetOption: (option, value) => {
            if (FullCalendar.calendarRef !== null) {
                FullCalendar.calendarRef.setOption(option, value);
            }
        },
        calendarRefetchResources: (newCalendarResourceFeed) => {
            if (FullCalendar.calendarRef !== null) {
                FullCalendar.calendarRef.setOption('resources', newCalendarResourceFeed);
                FullCalendar.calendarRef.refetchResources();
            }
        },
        calendarRefetchEvents: () => {
            if (FullCalendar.calendarRef !== null) {
                FullCalendar.calendarRef.refetchEvents();
            }
        },
        calendarOnDrop: (info) => {
            if (FullCalendar.calendarRef !== null) {
                FullCalendar.interop.callBackDotNetWithInfo('AddEventCallback', info);
                info.remove();
            }
        },
        calendarOnEventResize: (info) => {
            if (FullCalendar.calendarRef !== null) {
                FullCalendar.interop.callBackDotNetWithInfo('UpdateEventCallback', info);
            }
        },
        calendarOnEventClick: (info) => {
            if (FullCalendar.calendarRef !== null) {
                FullCalendar.interop.callBackDotNetWithInfo('ClickEventCallback', info);
            }
        },
        callBackDotNetWithInfo: (functionName, info) => {
            if (FullCalendar.dotNetReference !== null) {
                FullCalendar.dotNetReference.invokeMethodAsync(
                    functionName,
                    FullCalendar.buildCalendarEventChangeResponse(info)
                );
            }
        },
        SetDotNetReference: (ref) => {
            FullCalendar.dotNetReference = ref;
        },

    }
};
