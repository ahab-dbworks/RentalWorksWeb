// You need to include DayPilot javascript library in web application project for this component to work and reference the javascript/css it needs.
//---------------------------------------------------------------------------------
class FwSchedulerDetailedClass {
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control) {
        var html, dpschedulerid, viewCount, schedulerbtns,
            $schedulerbtns, $calendarmenu, $menucontrol, $form, controller;

        dpschedulerid = FwControl.generateControlId('dpscheduler');
        $control.attr('data-dpschedulerid', dpschedulerid);
        html = [];
        html.push('<div class="calendarmenu">');
        html.push('</div>');
        html.push('<div class="content">');
        html.push('  <div class="schedulercontainer">');
        html.push('    <div class="dpschedulercontainer">');
        html.push('      <div id="' + dpschedulerid + '" class="dpscheduler"></div>');
        html.push('    </div>');
        html.push('  </div>');
        html.push('</div>');
        html = html.join('\n');
        $control.html(html);

        $calendarmenu = $control.find('.calendarmenu');
        $menucontrol = FwMenu.getMenuControl('default');
        $calendarmenu.append($menucontrol);
        schedulerbtns = [];
        schedulerbtns.push('<div class="schedulerbtns">');
        schedulerbtns.push('  <div class="toggleView">');
        schedulerbtns.push('    <div class="changeview btnSchedule">Schedule</div>');
        schedulerbtns.push('  </div>');
        schedulerbtns.push('  <div class="topnavigation">');
        schedulerbtns.push('    <button class="btnRefreshCalendar">Refresh</button><button class="btnToday">Today</button><button class="btnPrev">&lt;</button><button class="btnNext">&gt;</button>');
        schedulerbtns.push('  </div>');
        schedulerbtns.push('  <div class="datecallout"></div>');
        schedulerbtns.push('</div>');
        $schedulerbtns = schedulerbtns.join('\n');
        FwMenu.addCustomContent($menucontrol, $schedulerbtns);

        $form = $control.closest('.fwform');
        controller = window[$form.attr('data-controller')];
        if ((typeof controller !== 'undefined') && (typeof controller.addSchedulerMenuItems !== 'undefined')) {
            controller.addSchedulerMenuItems($menucontrol, $form);
        }

        //viewCount = 0;
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
            var today;
            try {
                today = new DayPilot.Date();
                FwSchedulerDetailed.navigate($control, today);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnNext', function () {
            var currentDay, nextMonth, navscheduler;
            try {
                navscheduler = $control.data('dpscheduler');
                currentDay = navscheduler.startDate;
                nextMonth = currentDay.addMonths(1).firstDayOfMonth()
                FwSchedulerDetailed.navigate($control, nextMonth);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnPrev', function () {
            var currentDay, previousMonth, navscheduler;
            try {
                navscheduler = $control.data('dpscheduler');
                currentDay = navscheduler.startDate;
                previousMonth = currentDay.addMonths(-1).firstDayOfMonth();
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
        $control.on('onactivatetab', function () {
            if ($control.attr('data-refreshonactivatetab') !== 'false') {
                FwSchedulerDetailed.refresh($control);
            }
        });
    };
    //---------------------------------------------------------------------------------
    loadControl($control) {
        FwSchedulerDetailed.loadScheduler($control);
    };
    //---------------------------------------------------------------------------------
    loadScheduler($control) {
        var dpscheduler;
        dpscheduler = new DayPilot.Scheduler($control.find('.content')[0]);
        $control.data('dpscheduler', dpscheduler);
        // behavior and appearance
        dpscheduler.cellWidth = 50;
        dpscheduler.eventHeight = 30;
        dpscheduler.headerHeight = 25;

        // view
        dpscheduler.startDate = moment().format('YYYY-MM-DD');  // or just dpscheduler.startDate = "2013-03-25";
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
                var ev = args.source;
                args.async = true;  // notify manually using .loaded()

                // simulating slow server-side load
                args.html = "<div style='font-weight:bold'>" + ev.text() + "</div><div>Order Number: " + ev.data.orderNumber + "</div><div>Order Status: " + ev.data.orderStatus + "</div><div>Deal: " + ev.data.deal + "</div><div>Start: " + ev.start().toString("MM/dd/yyyy HH:mm") + "</div><div>End: " + ev.end().toString("MM/dd/yyyy HH:mm") + "</div>";
                args.loaded();

            }
        });
        dpscheduler.eventMoveHandling = "Disabled";
        dpscheduler.init();
    };
    //---------------------------------------------------------------------------------
    navigate($control, date, days?) {
        var dpscheduler;

        if (typeof date === 'string') {
            date = DayPilot.Date(new Date(date).toISOString(), true).getDatePart();
        }
        FwSchedulerDetailed.setSelectedDay($control, date);
        dpscheduler = $control.data('dpscheduler');
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
        var ongetevents, request, dpscheduler, start, days;
        dpscheduler = $control.data('dpscheduler');

        if (typeof $control.data('ongetevents') === 'function') {
            ongetevents = $control.data('ongetevents');
            //start       = dpmonth.startDate.addDays(-dpmonth.startDate.dayOfWeek()) // add the trailing days from the previous month that are visible
            //days        = dpmonth.days + dpmonth.startDate.dayOfWeek() + (6 - dpmonth.startDate.addDays(dpmonth.days).dayOfWeek()) // add the first few days from the next month that are visible
            request = {
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
        var dpscheduler, start, end, request;
        dpscheduler = $control.data('dpscheduler');

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
        var result, dpmonth;
        result = {
            start: $control.data('selectedstartdate'),
            end: $control.data('selectedenddate').addSeconds(-1)
        };
        dpmonth = $control.data('dpmonth');
        dpmonth.update();
        return result;
    };
    //---------------------------------------------------------------------------------
    getSelectedDay($control) {
        var result;
        result = $control.data('selectedstartdate');
        return result;
    };
    //---------------------------------------------------------------------------------
    setSelectedTimeRange($control, start, end) {
        var dpscheduler, e, action;
        dpscheduler = $control.data('dpscheduler');
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
        var start, end;

        if (typeof date === 'string') {
            start = new DayPilot.Date(new Date(date).toISOString());
        } else {
            start = date.getDatePart();
        }
        //end = start.addDays(1).addSeconds(-1);
        end = start;
        FwSchedulerDetailed.setSelectedTimeRange($control, start, end);
    };
    //---------------------------------------------------------------------------------
    setDateCallout($control, date) {
        var $datecallout, monthnames, firstdayofweek, lastdayofweek;
        monthnames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        $datecallout = $control.find('.datecallout');
        $datecallout.html(monthnames[date.getMonth()] + ' ' + date.getYear());
    };
    //---------------------------------------------------------------------------------
    // see http://api.daypilot.org/daypilot-event-data/ for info on how to create the evt object
    addEvent($control, evt) {
        var dpscheduler, e, action;
        dpscheduler = $control.data('dpscheduler');
        action = dpscheduler.events.add(evt);
    }
    //---------------------------------------------------------------------------------
    addButtonMenuItem($control, classname, text, onclick) {
        var $menu, $menuitem;
        $menuitem = jQuery('<button class="buttonmenuitem ' + classname + '"><span class="text">' + text + '</span></button>');
        $control.on('click', '.' + classname, onclick);
        $menu = $control.find('.menu');
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
        return new DayPilot.Date(new Date().toISOString());
    }
    //---------------------------------------------------------------------------------
}

var FwSchedulerDetailed = new FwSchedulerDetailedClass();