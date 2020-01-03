// You need to include DayPilot javascript library in web application project for this component to work and reference the javascript/css it needs.
//---------------------------------------------------------------------------------
class FwSchedulerDetailedClass {
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control) {
        const dpschedulerid = FwControl.generateControlId('dpscheduler');
        $control.attr('data-dpschedulerid', dpschedulerid);
        const html: Array<string> = [];
        html.push('<div class="calendarmenu">');
        html.push('</div>');
        html.push('<div class="content">');
        html.push('  <div class="schedulercontainer">');
        html.push('    <div class="dpschedulercontainer">');
        html.push('      <div id="' + dpschedulerid + '" class="dpscheduler"></div>');
        html.push('    </div>');
        html.push('  </div>');
        html.push('</div>');
        $control.html(html.join('\n'));

        const $calendarmenu = $control.find('.calendarmenu');
        const $menucontrol = FwMenu.getMenuControl('default');
        $calendarmenu.append($menucontrol);
        const schedulerbtns: Array<string> = [];
        schedulerbtns.push('<div class="schedulerbtns">');
        schedulerbtns.push('  <div class="toggleView">');
        schedulerbtns.push('    <div class="changeview btnSchedule">Schedule</div>');
        schedulerbtns.push('  </div>');
        schedulerbtns.push('  <div class="topnavigation">');
        schedulerbtns.push('    <button class="btnRefreshCalendar">Refresh</button><button class="btnToday">Today</button><button class="btnPrev">&lt;</button><button class="btnNext">&gt;</button>');
        schedulerbtns.push('  </div>');
        schedulerbtns.push('  <div class="datecallout"></div>');
        schedulerbtns.push('</div>');
        FwMenu.addCustomContent($menucontrol, jQuery(schedulerbtns.join('\n')));

        const $form = $control.closest('.fwform');
        const controller = (<any>window)[$form.attr('data-controller')];
        if ((typeof controller !== 'undefined') && (typeof controller.addSchedulerMenuItems !== 'undefined')) {
            controller.addSchedulerMenuItems($menucontrol, $form);
        }

        //let viewCount = 0;
        //if ($control.attr('data-hidedayview') != 'true') viewCount++;
        //if ($control.attr('data-hideweekview') != 'true') viewCount++;
        //if ($control.attr('data-hidemonthview') != 'true') viewCount++;
        //if ($control.attr('data-hideyearview') != 'true') viewCount++;
        //if ($control.attr('data-showeventview') == 'true') viewCount++;
        //$control.find('.toggleView').toggle(viewCount > 1);
        //$control.find('.btnDay').toggle($control.attr('data-hidedayview') !== 'true');
        //$control.find('.btnWeek').toggle($control.attr('data-hideweekview') !== 'true');
        //$control.find('.btn5Week').toggle($control.attr('data-hidemonthview') !== 'true');
        //$control.find('.btnMonth').toggle($control.attr('data-hidemonthview') !== 'true');
        //$control.find('.btnYear').toggle($control.attr('data-hideyearview') !== 'true');
        //$control.find('.btnSchedule').toggle($control.attr('data-showeventview') == 'true');
    };
    //---------------------------------------------------------------------------------
    init($control) {
        $control.on('click', '.btnToday', function () {
            try {
                const today = new DayPilot.Date();
                FwSchedulerDetailed.navigate($control, today);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnNext', function () {
            try {
                const navscheduler = $control.data('dpscheduler');
                const currentDay = navscheduler.startDate;
                const nextMonth = currentDay.addMonths(1).firstDayOfMonth()
                FwSchedulerDetailed.navigate($control, nextMonth);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnPrev', function () {
            try {
                const navscheduler = $control.data('dpscheduler');
                const currentDay = navscheduler.startDate;
                const previousMonth = currentDay.addMonths(-1).firstDayOfMonth();
                FwSchedulerDetailed.navigate($control, previousMonth);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnRefreshCalendar', function () {
            try {
                FwSchedulerDetailed.refresh($control);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        //$control.on('onactivatetab', () => {
        //    const $form = $control.closest('.fwform');
        //    if ($control.attr('data-refreshonactivatetab') !== 'false' && $form.attr('data-mode') !== 'NEW') {   // -- J. Pace commented this out because it is creating dubplicate API requests
        //        FwSchedulerDetailed.refresh($control);
        //    }
        //    $control.attr('data-refreshonactivatetab', 'true');
        //});
    };
    //---------------------------------------------------------------------------------
    loadControl($control) {
        FwSchedulerDetailed.loadScheduler($control);
    };
    //---------------------------------------------------------------------------------
    loadScheduler($control) {
        const dpscheduler = new DayPilot.Scheduler($control.find('.content')[0]);
        $control.data('dpscheduler', dpscheduler);
        // behavior and appearance
        dpscheduler.cellWidth = 50;
        dpscheduler.eventHeight = 30;
        dpscheduler.headerHeight = 25;

        // view
        //dpscheduler.startDate = moment().format('YYYY-MM-DD');  // or just dpscheduler.startDate = "2013-03-25";
        dpscheduler.startDate = FwScheduler.getTodaysDate();    //#1305 11/15/2019 justin hoffman.  Without this, the calandar advances to the next day when viewing the calendar after 4pm on your machine.
        dpscheduler.days = 35;
        dpscheduler.scale = "Day";
        dpscheduler.timeHeaders = [
            { groupBy: "Month" },
            { groupBy: "Day", format: "d" }
        ];
        dpscheduler.treeEnabled = true;
        dpscheduler.bubble = new DayPilot.Bubble({
            cssClassPrefix: "bubble_default",
            onLoad: function (args) {
                const ev = args.source;
                args.async = true;  // notify manually using .loaded()

                // simulating slow server-side load
                //args.html = `<div style='font-weight:bold'>${ev.text()}</div><div>Order Number: ${ev.data.orderNumber}</div><div>Order Status: ${ev.data.orderStatus}</div><div>Deal: ${ev.data.deal}</div><div>Start: ${ev.start().toString("MM/dd/yyyy HH:mm")}</div><div>End: ${ev.data.enddisplay}</div>`;

                //justin hoffman 11/18/2019 why is this code here?  This is specific to RWW
                args.html = ``;
                //args.html += `<div style='font-weight:bold'>${ev.text()}</div><div>Order Number: ${ev.data.orderNumber}</div><div>Order Status: ${ev.data.orderStatus}</div><div>Deal: ${ev.data.deal}</div><div>Start: ${ev.start().toString("MM/dd/yyyy HH:mm")}</div><div>End: ${ev.end().toString("MM/dd/yyyy HH:mm")}</div>`;
                args.html += `<div style='font-weight:bold'>${ev.text()}</div>`;
                if (ev.data.orderNumber) {
                    args.html += `<div>Order Number: ${ev.data.orderNumber}</div>`;
                }
                if (ev.data.orderStatus) {
                    args.html += `<div>Order Status: ${ev.data.orderStatus}</div>`;
                }
                if (ev.data.deal) {
                    args.html += `<div>Deal: ${ev.data.deal}</div>`;
                }
                args.html += `<div>Start: ${ev.start().toString("MM/dd/yyyy HH:mm")}</div><div>End: ${ev.end().toString("MM/dd/yyyy HH:mm")}</div>`;
                if (ev.data.subPoNumber) {
                    args.html += `<div>Sub PO Number: ${ev.data.subPoNumber}</div>`;
                    if (ev.data.subPoVendor) {
                        args.html += `<div>Vendor: ${ev.data.subPoVendor}</div>`;
                    }
                }

                args.loaded();
            }
        });
        dpscheduler.eventMoveHandling = "Disabled";
        dpscheduler.eventDoubleClickHandling = "Enabled";
        if (typeof $control.data('oneventdoubleclicked') === 'function') dpscheduler.onEventDoubleClicked = $control.data('oneventdoubleclicked');

        dpscheduler.init();
    };
    //---------------------------------------------------------------------------------
    navigate($control, date, days?) {
        //if (typeof date === 'string') {
        //    date = DayPilot.Date(new Date(date).toISOString(), true).getDatePart();
        //}
        //#1305 11/15/2019 justin hoffman.  commented above.  With that code, the calandar advances to the next day when viewing the calendar after 4pm on your machine.
        FwSchedulerDetailed.setSelectedDay($control, date);
        const dpscheduler = $control.data('dpscheduler');
        if (date.d.getDate() === 31 || date.d.getDate() === 30) {
            dpscheduler.days = date.daysInMonth();
        } else {
            dpscheduler.days = date.daysInMonth() - date.d.getDate();
        }
        if (days !== undefined) {
            dpscheduler.days = days;
        }
        dpscheduler.startDate = date;
        FwSchedulerDetailed.loadEvents($control)
    };
    //---------------------------------------------------------------------------------
    refresh($control) {
        FwSchedulerDetailed.loadEvents($control);
    };
    //---------------------------------------------------------------------------------
    loadEvents($control) {
        const dpscheduler = $control.data('dpscheduler');
        if (typeof $control.data('ongetevents') === 'function') {
            const ongetevents = $control.data('ongetevents');
            //start       = dpmonth.startDate.addDays(-dpmonth.startDate.dayOfWeek()) // add the trailing days from the previous month that are visible
            //days        = dpmonth.days + dpmonth.startDate.dayOfWeek() + (6 - dpmonth.startDate.addDays(dpmonth.days).dayOfWeek()) // add the first few days from the next month that are visible
            const request: any = {
                start: dpscheduler.startDate,
                mode: $control.find('div.changeview[data-selected="true"]').html()
            };
            ongetevents(request);
        } else {
            throw 'ongetevents is not implemented.';
        }
    };
    //---------------------------------------------------------------------------------
    loadEventsCallback($control, resources, events) {
        const dpscheduler = $control.data('dpscheduler');

        if (typeof dpscheduler !== 'undefined') {
            FwSchedulerDetailed.setDateCallout($control, dpscheduler.startDate);

            dpscheduler.resources = resources;
            dpscheduler.events.list = events;
            dpscheduler.update();
        } else {
            // the user must have navigated away before this finished loading
        }
    };
    //---------------------------------------------------------------------------------
    getSelectedTimeRange($control) {
        const result: any = {
            start: $control.data('selectedstartdate'),
            end: $control.data('selectedenddate').addSeconds(-1)
        };
        const dpmonth = $control.data('dpmonth');
        dpmonth.update();
        return result;
    };
    //---------------------------------------------------------------------------------
    getSelectedDay($control) {
        const result = $control.data('selectedstartdate');
        return result;
    };
    //---------------------------------------------------------------------------------
    setSelectedTimeRange($control, start, end) {
        const dpscheduler = $control.data('dpscheduler');
        if (typeof start === 'string') {
            start = new DayPilot.Date(new Date(start).toISOString());
        }
        if (typeof end === 'string') {
            end = new DayPilot.Date(new Date(end).toISOString());
        }
        $control.data('selectedstartdate', start);
        $control.data('selectedenddate', end);
        dpscheduler.update();
    };
    //---------------------------------------------------------------------------------
    setSelectedDay($control, date) {
        let start;

        if (typeof date === 'string') {
            start = new DayPilot.Date(new Date(date).toISOString());
        } else {
            start = date.getDatePart();
        }
        //end = start.addDays(1).addSeconds(-1);
        const end = start;
        FwSchedulerDetailed.setSelectedTimeRange($control, start, end);
    };
    //---------------------------------------------------------------------------------
    setDateCallout($control, date) {
        const monthnames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        const $datecallout = $control.find('.datecallout');
        $datecallout.html(monthnames[date.getMonth()] + ' ' + date.getYear());
    };
    //---------------------------------------------------------------------------------
    // see http://api.daypilot.org/daypilot-event-data/ for info on how to create the evt object
    addEvent($control, evt) {
        const dpscheduler = $control.data('dpscheduler');
        const action = dpscheduler.events.add(evt);
    }
    //---------------------------------------------------------------------------------
    addButtonMenuItem($control, classname, text, onclick) {
        const $menuitem = jQuery('<button class="buttonmenuitem ' + classname + '"><span class="text">' + text + '</span></button>');
        $control.on('click', '.' + classname, onclick);
        const $menu = $control.find('.menu');
        $menu.append($menuitem);
    }
    //---------------------------------------------------------------------------------
    load($control) {
        if (FwSecurity.isUser()) {
            FwSchedulerDetailed.navigate($control, new DayPilot.Date());
            FwSchedulerDetailed.loadEvents($control);
        }
    };
    //---------------------------------------------------------------------------------
    getTodaysDate() {
        //return new DayPilot.Date(new Date().toISOString());
        let dateStr = moment().format('YYYY-MM-DD');
        let timeStr = moment().format('HH:mm:ss');
        return new DayPilot.Date(dateStr + 'T' + timeStr);   //#1305 11/15/2019 justin hoffman.  Without this, the calandar advances to the next day when viewing the calendar after 4pm on your machine.
    }
    //---------------------------------------------------------------------------------
}

var FwSchedulerDetailed = new FwSchedulerDetailedClass();