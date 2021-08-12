// Bootstrap
//import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";

// fontawesome
import "@fortawesome/fontawesome-free/css/all.min.css";

// FullCalendar
import { Calendar, DurationInput, PluginDef } from '@fullcalendar/core';
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


interface FCWrapperData {
    dotNetReference: any;
    calendarRef: Calendar;
}

abstract class Interop {
    protected wrapperData: FCWrapperData;

    constructor(wrapperData: FCWrapperData) {
        this.wrapperData = wrapperData;
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
        if (this.wrapperData.dotNetReference !== null) {
            this.wrapperData.dotNetReference.invokeMethodAsync(
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

    public onDrop = (info) => {
        if (this.wrapperData.calendarRef !== null) {
            this.callBackDotNetWithInfo('OnDropCallback', info);
            info.remove();
        }
    };

    public onEventChange = (info) => {
        if (this.wrapperData.calendarRef !== null) {
            this.callBackDotNetWithInfo('OnEventChangeCallback', info);
        }
    };

    public onEventClick = (info) => {
        if (this.wrapperData.calendarRef !== null) {
            this.callBackDotNetWithInfo('OnEventClickCallback', info);
        }
    };

    public onEventDrop = (info) => {
        if (this.wrapperData.calendarRef !== null) {
            this.callBackDotNetWithInfo('OnEventDropCallback', info);
        }
    };

    public onEventResize = (info) => {
        if (this.wrapperData.calendarRef !== null) {
            this.callBackDotNetWithInfo('OnEventResizeCallback', info);
        }
    };

    public onEventResizeStart = (info) => {
        if (this.wrapperData.calendarRef !== null) {
            this.callBackDotNetWithInfo('OnEventResizeStartCallback', info);
        }
    };

    public onEventResizeStop = (info) => {
        if (this.wrapperData.calendarRef !== null) {
            this.callBackDotNetWithInfo('OnEventResizeStopCallback', info);
        }
    };

    
    public onEventMouseEnter = (info) => {
        if (this.wrapperData.calendarRef !== null) {
            this.callBackDotNetWithInfo('OnEventMouseEnterCallback', info);
        }
    };

    public onEventMouseLeave = (info) => {
        if (this.wrapperData.calendarRef !== null) {
            this.callBackDotNetWithInfo('OnEventMouseLeaveCallback', info);
        }
    };

    public onEventDragStart = (info) => {
        if (this.wrapperData.calendarRef !== null) {
            this.callBackDotNetWithInfo('OnEventDragStartCallback', info);
        }
    };

    public onEventDragStop = (info) => {
        if (this.wrapperData.calendarRef !== null) {
            this.callBackDotNetWithInfo('OnEventDragStopCallback', info);
        }
    };
}

class InteropCallableFromDotNet extends Interop {

    public calendarChangeDuration = (units: DurationInput) => {
        if (this.wrapperData.calendarRef !== null) {
            this.wrapperData.calendarRef.setOption('duration', units);
        }
    };

    public calendarSetOption = (option, value) => {
        if (this.wrapperData.calendarRef !== null) {
            this.wrapperData.calendarRef.setOption(option, value);
        }
    };

    public calendarRefetchResources = (newCalendarResourceFeed: string) => {
        if (this.wrapperData.calendarRef !== null) {
            this.wrapperData.calendarRef.setOption('events', newCalendarResourceFeed);
            this.wrapperData.calendarRef.refetchEvents();
        }
    };

    public calendarRefetchEvents = () => {
        if (this.wrapperData.calendarRef !== null) {
            this.wrapperData.calendarRef.refetchEvents();
        }
    };
}

class FullCalendarWrapper {
    private calendarEl: HTMLElement;
    private draggables = null;

    private wrapperData: FCWrapperData;

    private fromJsInterop: InteropCallableFromJs;
    public FromDotNetInterop: InteropCallableFromDotNet;

    constructor(calendarDivId: string, settingsJson: string, dotNetReference) {
        this.calendarEl = document.getElementById(calendarDivId);

        this.wrapperData = {
            dotNetReference: dotNetReference,
            calendarRef: null
        }

        var parsedSettings = JSON.parse(settingsJson);
        parsedSettings.plugins = this.addPlugins(parsedSettings);

        this.fromJsInterop = new InteropCallableFromJs(this.wrapperData);
        this.FromDotNetInterop = new InteropCallableFromDotNet(this.wrapperData);

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

        this.wrapperData.calendarRef = new Calendar(this.calendarEl, parsedSettings);
        this.render();
    }

    private addPlugins = (parsedSettings: any): PluginDef[] => {

        let plugins: PluginDef[] = [];

        for (let pluginString of parsedSettings.plugins) {
            if (pluginString.includes('dayGrid')) {
                plugins.push(dayGridPlugin);
            } else if (pluginString.includes('interaction')) {
                plugins.push(interactionPlugin);
            } else if (pluginString.includes('timeGrid')) {
                plugins.push(timeGridPlugin);
            } else if (pluginString.includes('list')) {
                plugins.push(listPlugin);
            } else if (pluginString.includes('bootstrap')) {
                plugins.push(bootstrapPlugin);
            } else if (pluginString.includes('googleCalendar')) {
                plugins.push(googleCalendarPlugin);
            }
        }

        return plugins;
    }

    private bindWithJsInterop = (parsedSettings) => {

        parsedSettings.drop = this.fromJsInterop.onDrop;

        parsedSettings.eventChange = this.fromJsInterop.onEventChange;

        parsedSettings.eventClick = this.fromJsInterop.onEventClick;

        parsedSettings.eventDrop = this.fromJsInterop.onEventDrop;

        parsedSettings.eventResize = this.fromJsInterop.onEventResize;
        parsedSettings.eventResizeStart = this.fromJsInterop.onEventResizeStart;
        parsedSettings.eventResizeStop = this.fromJsInterop.onEventResizeStop;

        parsedSettings.eventMouseEnter = this.fromJsInterop.onEventMouseEnter;
        parsedSettings.eventMouseLeave = this.fromJsInterop.onEventMouseLeave;


        parsedSettings.eventDragStart = this.fromJsInterop.onEventDragStart;
        parsedSettings.eventDragStop = this.fromJsInterop.onEventDragStop;
    }

    set dotNetReference(dotNetReference) {
        this.wrapperData.dotNetReference = dotNetReference;
    }

    get dotNetReference() {
        return this.wrapperData.dotNetReference;
    }

    get calendarReference() {
        return this.wrapperData.calendarRef;
    }

    public render = () => {
        this.wrapperData.calendarRef.render();
    }

    private backInUtcTime = (date) => {
        return moment(date).add(moment(date).utcOffset(), "minutes")
    };
}

export const FCWrapperInstances: Map<string, FullCalendarWrapper> = new Map<string, FullCalendarWrapper>();

export function addFCWrapperInstance(calendarDivId: string, settingsJson: string, dotNetReference) {
    FCWrapperInstances.set(calendarDivId, new FullCalendarWrapper(calendarDivId, settingsJson, dotNetReference));
}

export const interop = {
    calendarChangeDuration: (calendarDivId: string, units: DurationInput) => {
        FCWrapperInstances.get(calendarDivId).FromDotNetInterop.calendarChangeDuration(units);
    },

    calendarSetOption: (calendarDivId: string, option, value) => {
        FCWrapperInstances.get(calendarDivId).FromDotNetInterop.calendarSetOption(option, value);
    },

    calendarRefetchResources: (calendarDivId: string, newCalendarResourceFeed: string) => {
        FCWrapperInstances.get(calendarDivId).FromDotNetInterop.calendarRefetchResources(newCalendarResourceFeed);
    },

    calendarRefetchEvents: (calendarDivId: string) => {
        FCWrapperInstances.get(calendarDivId).FromDotNetInterop.calendarRefetchEvents();
    },

    setDotNetReference: (calendarDivId: string, dotNetReference: any) => {
        FCWrapperInstances.get(calendarDivId).dotNetReference = dotNetReference;
    },
}

export function deleteFCWrapperInstance(calendarDivId: string) {
    FCWrapperInstances.delete(calendarDivId);
}

//setInterval(() => console.log(FCWrapperInstances), 1000);
