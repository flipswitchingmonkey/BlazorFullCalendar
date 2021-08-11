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

    if (value === null || value === "null") {
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

abstract class Interop {

    protected dotNetReference;
    protected calendarRef;

    constructor(dotNetReference) {
        this.SetDotNetReference(dotNetReference);
    }

    public SetDotNetReference = (dotNetReference) => {
        this.dotNetReference = dotNetReference;
    }

    public SetFCalendarReference = (calendarReference) => {
        this.calendarRef = calendarReference;
    }

    protected getElementByName = (name) => {
        var elements = document.getElementsByName(name);

        if (elements.length) {
            return elements[0];
        } else {
            return "";
        }
    };
};

class InteropCallableFromJs extends Interop {

    private callBackDotNetWithInfo = (functionName, info) => {
        if (this.dotNetReference !== null) {
            this.dotNetReference.invokeMethodAsync(
                functionName,
                this.buildCalendarEventChangeResponse(info)
            );
        }
    };

    private buildCalendarEventChangeResponse = (info) => {

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

        return JSON.stringify(response, replaceNullWithUndefined);
    };

    public CalendarOnDrop = (info) => {
        if (this.calendarRef !== null) {
            this.callBackDotNetWithInfo('AddEventCallback', info);
            info.remove();
        }
    };

    public CalendarOnEventResize = (info) => {
        if (this.calendarRef !== null) {
            this.callBackDotNetWithInfo('ResizeEventCallback', info);
        }
    };

    public CalendarOnEventChange = (info) => {
        if (this.calendarRef !== null) {
            this.callBackDotNetWithInfo('UpdateEventCallback', info);
        }
    };

    public CalendarOnEventClick = (info) => {
        if (this.calendarRef !== null) {
            this.callBackDotNetWithInfo('ClickEventCallback', info);
        }
    };

    public CalendarOnEventDrop = (info) => {
        if (this.calendarRef !== null) {
            this.callBackDotNetWithInfo('DropEventCallback', info);
        }
    };
}

class InteropCallableFromDotNet extends Interop {

    public CalendarChangeDuration = (units, amount) => {
        if (this.calendarRef !== null) {
            this.calendarRef.setOption('duration', { units: amount });
        }
    };

    public CalendarSetOption = (option, value) => {
        if (this.calendarRef !== null) {
            this.calendarRef.setOption(option, value);
        }
    };

    public CalendarRefetchResources = (newCalendarResourceFeed) => {
        if (this.calendarRef !== null) {
            this.calendarRef.setOption('resources', newCalendarResourceFeed);
            this.calendarRef.refetchResources();
        }
    };

    public CalendarRefetchEvents = () => {
        if (this.calendarRef !== null) {
            this.calendarRef.refetchEvents();
        }
    };
}

class FullCalendarWrapper {
    private calendarEl: HTMLElement;

    private calendarRef = null;
    private draggables = null;
    private dotNetReference = null;

    private fromJsInterop: InteropCallableFromJs;
    public FromDotNetInterop: InteropCallableFromDotNet;

    constructor(calendarDivId: string, settingsJson: string, dotNetReference) {
        this.calendarEl = document.getElementById(calendarDivId);
        this.dotNetReference = dotNetReference;

        var parsedSettings = JSON.parse(settingsJson);
        parsedSettings.plugins = this.addPlugins(parsedSettings);

        this.fromJsInterop = new InteropCallableFromJs(dotNetReference);
        this.FromDotNetInterop = new InteropCallableFromDotNet(dotNetReference);

        this.bindWithJsInterop(parsedSettings);

        if (parsedSettings.droppable === true) {
            this.draggables = new Draggable(document.getElementById('external-events'), {
                itemSelector: '.fc-event',
                eventData: function (eventEl) {
                    return {
                        title: eventEl.innerText
                    };
                }
            });
        }

        this.calendarRef = new Calendar(this.calendarEl, parsedSettings);
        this.Render();

        this.fromJsInterop.SetFCalendarReference(this.calendarRef);
        this.FromDotNetInterop.SetFCalendarReference(this.calendarRef);
    }

    private addPlugins = (parsedSettings: any): PluginDef[] => {

        let plugins: PluginDef[] = [];

        for (let it of parsedSettings.plugins) {
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

        return plugins;
    }

    private bindWithJsInterop = (parsedSettings) => {

        parsedSettings.eventResize = this.fromJsInterop.CalendarOnEventResize;
        parsedSettings.eventChange = this.fromJsInterop.CalendarOnEventChange;
        parsedSettings.drop = this.fromJsInterop.CalendarOnDrop;
        parsedSettings.eventDrop = this.fromJsInterop.CalendarOnEventDrop;
        parsedSettings.eventClick = this.fromJsInterop.CalendarOnEventClick;

        if (parsedSettings.eventResizeStart !== null)
            parsedSettings.eventResizeStart = eval(parsedSettings.eventResizeStart);

        if (parsedSettings.eventResizeStop !== null)
            parsedSettings.eventResizeStop = eval(parsedSettings.eventResizeStop);

        if (parsedSettings.eventMouseEnter !== null)
            parsedSettings.eventMouseEnter = eval(parsedSettings.eventMouseEnter);

        if (parsedSettings.eventMouseLeave !== null)
            parsedSettings.eventMouseLeave = eval(parsedSettings.eventMouseLeave);

        if (parsedSettings.eventDragStart !== null)
            parsedSettings.eventDragStart = eval(parsedSettings.eventDragStart);

        if (parsedSettings.eventDragStop !== null)
            parsedSettings.eventDragStop = eval(parsedSettings.eventDragStop);
    }

    public SetDotNetReference = (dotNetReference) => {

        this.dotNetReference = dotNetReference;

        this.fromJsInterop.SetDotNetReference(dotNetReference);
        this.FromDotNetInterop.SetDotNetReference(dotNetReference);
    };

    public Render = () => {
        this.calendarRef.render();
    }

    private backInUtcTime = (date) => {
        return moment(date).add(moment(date).utcOffset(), "minutes")
    };
}

export const FCWrapperInstances: Map<string, FullCalendarWrapper> = new Map<string, FullCalendarWrapper>();

export function AddFCWrapperInstance(calendarDivId: string, settingsJson: string, dotNetReference) {
    FCWrapperInstances.set(calendarDivId, new FullCalendarWrapper(calendarDivId, settingsJson, dotNetReference));
}

export const interop = {
    calendarChangeDuration: (calendarDivId: string, units, amount) => {
        FCWrapperInstances.get(calendarDivId).FromDotNetInterop.CalendarChangeDuration(units, amount);
    },

    calendarSetOption: (calendarDivId: string, option, value) => {
        FCWrapperInstances.get(calendarDivId).FromDotNetInterop.CalendarSetOption(option, value);
    },

    calendarRefetchResources: (calendarDivId: string, newCalendarResourceFeed) => {
        FCWrapperInstances.get(calendarDivId).FromDotNetInterop.CalendarRefetchResources(newCalendarResourceFeed);
    },

    calendarRefetchEvents: (calendarDivId: string) => {
        FCWrapperInstances.get(calendarDivId).FromDotNetInterop.CalendarRefetchEvents();
    },

    setDotNetReference: (calendarDivId: string, dotNetReference) => {
        FCWrapperInstances.get(calendarDivId).SetDotNetReference(dotNetReference);
    },
}

export function DeleteFCWrapperInstance(calendarDivId: string) {
    FCWrapperInstances.delete(calendarDivId);
}
