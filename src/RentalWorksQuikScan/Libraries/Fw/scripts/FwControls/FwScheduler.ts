// You need to include DayPilot javascript library in web application project for this component to work and reference the javascript/css it needs.
//---------------------------------------------------------------------------------
class FwSchedulerClass {
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control) {
        var html, $daypilotcontrol, dp, nav, navcalendarid, dpcalendarid, navmonthid, nav5weekid, dpmonthid, dp5weekid, dpyearid, navschedulerid, dpschedulerid, viewCount, schedulerbtns,
            $schedulerbtns, $calendarmenu, $menucontrol, $form, controller;

        navcalendarid = FwControl.generateControlId('navcalendar');
        dpcalendarid = FwControl.generateControlId('dpcalendar');
        navmonthid = FwControl.generateControlId('navmonth');
        dpmonthid = FwControl.generateControlId('dpmonth');
        nav5weekid = FwControl.generateControlId('nav5week');
        dp5weekid = FwControl.generateControlId('dp5weekid');
        dpyearid = FwControl.generateControlId('dpyearid');
        navschedulerid = FwControl.generateControlId('navscheduler');
        dpschedulerid = FwControl.generateControlId('dpscheduler');
        $control.attr('data-navcalendarid', navcalendarid);
        $control.attr('data-dpcalendarid', dpcalendarid);
        $control.attr('data-navmonthid', navmonthid);
        $control.attr('data-dpmonthid', dpmonthid);
        $control.attr('data-nav5weekid', nav5weekid);
        $control.attr('data-dp5weekid', dp5weekid);
        $control.attr('data-dpyearid', dpyearid);
        $control.attr('data-navschedulerid', navschedulerid);
        $control.attr('data-dpschedulerid', dpschedulerid);
        html = [];
        html.push('<div class="calendarmenu">');
        html.push('</div>');
        html.push('<div class="content">');
        html.push('  <div class="calendarcontainer" style="display:none;">');
        html.push('    <div class="dpcalendarcontainer">');
        html.push('      <div id="' + dpcalendarid + '" class="dpcalendar"></div>');
        html.push('    </div>');
        html.push('    <div class="navcalendarcontainer">');
        html.push('      <div id="' + navcalendarid + '"></div>');
        html.push('    </div>');
        html.push('  </div>');
        html.push('  <div class="monthcontainer" style="display:none;">');
        html.push('    <div class="dpmonthcontainer">');
        html.push('      <div id="' + dpmonthid + '" class="dpmonth"></div>');
        html.push('    </div>');
        html.push('    <div class="navmonthcontainer">');
        html.push('      <div id="' + navmonthid + '"></div>');
        html.push('    </div>');
        html.push('  </div>');
        html.push('  <div class="fiveweekcontainer" style="display:none;">');
        html.push('    <div class="dpmonthcontainer">');
        html.push('      <div id="' + dp5weekid + '" class="dp5week"></div>');
        html.push('    </div>');
        html.push('    <div class="nav5weekcontainer">');
        html.push('      <div id="' + nav5weekid + '"></div>');
        html.push('    </div>');
        html.push('  </div>');
        html.push('  <div class="yearcontainer" style="display:none;">');
        html.push('    <div class="dpyearcontainer">');
        html.push('      <div id="' + dpyearid + '" class="dp5week"></div>');
        html.push('    </div>');
        html.push('  </div>');
        html.push('  <div class="schedulercontainer" style="display:none;">');
        html.push('    <div class="dpschedulercontainer">');
        html.push('      <div id="' + dpschedulerid + '" class="dpscheduler"></div>');
        html.push('    </div>');
        html.push('    <div class="navschedulercontainer">');
        html.push('      <div id="' + navschedulerid + '"></div>');
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
        //schedulerbtns.push('  <div class="lblView">View:</div>');
        schedulerbtns.push('  <div class="toggleView">');
        schedulerbtns.push('    <div class="changeview btnDay">Day</div>');
        schedulerbtns.push('    <div class="changeview btnWeek">Week</div>');
        schedulerbtns.push('    <div class="changeview btn5Week">5 Week</div>');
        schedulerbtns.push('    <div class="changeview btnMonth">Month</div>');
        //schedulerbtns.push('    <div class="changeview btnYear">Year</div>');
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

        viewCount = 0;
        if ($control.attr('data-hidedayview') != 'true') viewCount++;
        if ($control.attr('data-hideweekview') != 'true') viewCount++;
        if ($control.attr('data-hidemonthview') != 'true') viewCount++;
        if ($control.attr('data-hideyearview') != 'true') viewCount++;
        if ($control.attr('data-showeventview') == 'true') viewCount++;
        $control.find('.toggleView').toggle(viewCount > 1);
        $control.find('.btnDay').toggle($control.attr('data-hidedayview') !== 'true');
        $control.find('.btnWeek').toggle($control.attr('data-hideweekview') !== 'true');
        $control.find('.btn5Week').toggle($control.attr('data-hidemonthview') !== 'true');
        $control.find('.btnMonth').toggle($control.attr('data-hidemonthview') !== 'true');
        $control.find('.btnYear').toggle($control.attr('data-hideyearview') !== 'true');
        $control.find('.btnSchedule').toggle($control.attr('data-showeventview') == 'true');
    };
    //---------------------------------------------------------------------------------
    init($control) {
        $control.on('mousewheel', '.navcalendarcontainer,.navmonthcontainer, .nav5weekcontainer, .navschedulercontainer', function (event) {
            //console.log(event.deltaX, event.deltaY, event.deltaFactor);
            if (jQuery('.pleasewait').length === 0) {
                if (event.deltaY < 0) {
                    $control.find('.btnNext').click();
                } else if (event.deltaY > 0) {
                    $control.find('.btnPrev').click();
                }
            }
        });
        $control.on('click', '.btnToday', function () {
            var today;
            try {
                today = new DayPilot.Date();
                FwScheduler.navigate($control, today);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnDay', function () {
            try {
                FwScheduler.showDayView($control);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnWeek', function () {
            try {
                FwScheduler.showWeekView($control);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btn5Week', function () {
            try {
                FwScheduler.show5WeekView($control);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnMonth', function () {
            try {
                FwScheduler.showMonthView($control);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnYear', function () {
            try {
                FwScheduler.showYearView($control);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnSchedule', function () {
            try {
                FwScheduler.showScheduleView($control);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnNext', function () {
            var currentDay, nextDay, nextWeek, next5Week, nextMonth, nextYear, navcalendar, nav5week, navmonth, navscheduler, schedulerDetailed;
            try {
                if ($control.next().data('dpscheduler') !== undefined) {
                    schedulerDetailed = $control.next().data('dpscheduler');
                }
                if ($control.find('.btnDay').attr('data-selected') === 'true') {
                    navcalendar = $control.data('navcalendar');
                    currentDay = navcalendar.selectionStart;
                    nextDay = currentDay.addDays(1);
                    FwScheduler.navigate($control, nextDay);
                    if (schedulerDetailed !== undefined) {
                        FwSchedulerDetailed.navigate($control.next(), nextDay, 1);
                    }
                } else if ($control.find('.btnWeek').attr('data-selected') === 'true') {
                    navcalendar = $control.data('navcalendar');
                    currentDay = navcalendar.selectionStart;
                    nextWeek = currentDay.addDays(7);
                    FwScheduler.navigate($control, nextWeek);
                    if (schedulerDetailed !== undefined) {
                        FwSchedulerDetailed.navigate($control.next(), nextWeek, 7);
                    }
                } else if ($control.find('.btn5Week').attr('data-selected') === 'true') {
                    nav5week = $control.data('nav5week');
                    currentDay = nav5week.selectionDay;
                    next5Week = currentDay.addDays(35);
                    FwScheduler.navigate($control, next5Week);
                    if (schedulerDetailed !== undefined) {
                        FwSchedulerDetailed.navigate($control.next(), next5Week, 35);
                    }
                } else if ($control.find('.btnMonth').attr('data-selected') === 'true') {
                    navmonth = $control.data('navmonth');
                    currentDay = navmonth.selectionStart;
                    nextMonth = currentDay.addMonths(1);
                    FwScheduler.navigate($control, nextMonth);
                    if (schedulerDetailed !== undefined) {
                        FwSchedulerDetailed.navigate($control.next(), nextMonth, 31);
                    }
                } else if ($control.find('.btnYear').attr('data-selected') === 'true') {
                    navmonth = $control.data('navyear');
                    currentDay = navmonth.selectionStart;
                    nextYear = currentDay.addYears(1);
                    FwScheduler.navigate($control, nextYear);
                } else if ($control.find('.btnSchedule').attr('data-selected') === 'true') {
                    navscheduler = $control.data('navscheduler');
                    currentDay = navscheduler.selectionStart;
                    nextMonth = currentDay.addMonths(1);
                    FwScheduler.navigate($control, nextMonth);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnPrev', function () {
            var currentDay, previousDay, previousWeek, previous5Week, previousMonth, nav5week, navyear, navcalendar, navmonth, navscheduler, schedulerDetailed;
            try {
                if ($control.next().data('dpscheduler') !== undefined) {
                    schedulerDetailed = $control.next().data('dpscheduler');
                }
                if ($control.find('.btnDay').attr('data-selected') === 'true') {
                    navcalendar = $control.data('navcalendar');
                    currentDay = navcalendar.selectionStart;
                    previousDay = currentDay.addDays(-1);
                    FwScheduler.navigate($control, previousDay);
                    if (schedulerDetailed !== undefined) {
                        FwSchedulerDetailed.navigate($control.next(), previousDay, 1);
                    }
                } else if ($control.find('.btnWeek').attr('data-selected') === 'true') {
                    navcalendar = $control.data('navcalendar');
                    currentDay = navcalendar.selectionStart;
                    previousWeek = currentDay.addDays(-7);
                    FwScheduler.navigate($control, previousWeek);
                    if (schedulerDetailed !== undefined) {
                        FwSchedulerDetailed.navigate($control.next(), previousWeek, 7);
                    }
                } else if ($control.find('.btn5Week').attr('data-selected') === 'true') {
                    nav5week = $control.data('nav5week');
                    currentDay = nav5week.selectionDay;
                    previous5Week = currentDay.addDays(-35);
                    FwScheduler.navigate($control, previous5Week);
                    if (schedulerDetailed !== undefined) {
                        FwSchedulerDetailed.navigate($control.next(), previous5Week, 35);
                    }
                } else if ($control.find('.btnMonth').attr('data-selected') === 'true') {
                    navmonth = $control.data('navmonth');
                    currentDay = navmonth.selectionStart;
                    previousMonth = currentDay.addMonths(-1);
                    FwScheduler.navigate($control, previousMonth);
                    if (schedulerDetailed !== undefined) {
                        FwSchedulerDetailed.navigate($control.next(), previousMonth, 31);
                    }
                } else if ($control.find('.btnYear').attr('data-selected') === 'true') {
                    navyear = $control.data('navyear');
                    currentDay = navyear.selectionStart;
                    previousMonth = currentDay.addYears(-1);
                    FwScheduler.navigate($control, previousMonth);
                } else if ($control.find('.btnSchedule').attr('data-selected') === 'true') {
                    navscheduler = $control.data('navscheduler');
                    currentDay = navscheduler.selectionStart;
                    previousMonth = currentDay.addMonths(-1);
                    FwScheduler.navigate($control, previousMonth);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnRefreshCalendar', function () {
            var currentDay, previousDay, previousWeek, previousMonth, navcalendar, navmonth, navscheduler;
            try {
                FwScheduler.refresh($control);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('onactivatetab', function () {
            if ($control.attr('data-refreshonactivatetab') !== 'false') {
                FwScheduler.refresh($control);
            }
        });
    };
    //---------------------------------------------------------------------------------
    loadControl($control) {
        FwScheduler.loadNavCalendar($control);
        FwScheduler.loadNavMonth($control);
        FwScheduler.loadNav5Week($control);
        FwScheduler.loadNavScheduler($control);
        FwScheduler.loadCalendar($control);
        FwScheduler.loadMonth($control);
        FwScheduler.load5Week($control);
        FwScheduler.loadYear($control);
        FwScheduler.loadScheduler($control);
        //FwScheduler.loadEvents($control);
        FwScheduler.show5WeekView($control);
    };
    //---------------------------------------------------------------------------------
    loadNavCalendar($control) {
        var navcalendar;
        navcalendar = new DayPilot.Navigator($control.attr('data-navcalendarid'));
        $control.data('navcalendar', navcalendar);
        navcalendar.showMonths = 2;
        navcalendar.skipMonths = 2;
        navcalendar.selectMode = "day";
        navcalendar.weekStarts = 0;
        navcalendar.onTimeRangeSelected = function (args) {
            var dpcalendar;
            try {
                dpcalendar = $control.data('dpcalendar');
                dpcalendar.startDate = args.start;
                dpcalendar.days = args.days;
                FwScheduler.loadEvents($control);
                dpcalendar.update();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        };
        navcalendar.init();
    };
    //---------------------------------------------------------------------------------
    loadNav5Week($control) {
        var nav5week;
        nav5week = new DayPilot.Navigator($control.attr('data-nav5weekid'));
        $control.data('nav5week', nav5week);
        nav5week.showMonths = 3;
        nav5week.skipMonths = 3;
        nav5week.selectMode = "month";
        nav5week.weekStarts = 0;
        nav5week.onTimeRangeSelected = function (args) {
            var dp5week;
            try {
                dp5week = $control.data('dp5week');
                dp5week.startDate = args.day;
                dp5week.days = 34;
                FwScheduler.loadEvents($control);
                dp5week.update();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        };
        nav5week.init();
    };
    //---------------------------------------------------------------------------------
    loadNavMonth($control) {
        var navmonth;
        navmonth = new DayPilot.Navigator($control.attr('data-navmonthid'));
        $control.data('navmonth', navmonth);
        navmonth.showMonths = 3;
        navmonth.skipMonths = 3;
        navmonth.selectMode = "month";
        navmonth.weekStarts = 0;
        navmonth.onTimeRangeSelected = function (args) {
            var dpmonth;
            try {
                dpmonth = $control.data('dpmonth');
                dpmonth.startDate = args.start;
                dpmonth.days = args.days;
                FwScheduler.loadEvents($control);
                dpmonth.update();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        };
        navmonth.init();
    };
    //---------------------------------------------------------------------------------
    loadNavScheduler($control) {
        var navscheduler;
        navscheduler = new DayPilot.Navigator($control.attr('data-navschedulerid'));
        $control.data('navscheduler', navscheduler);
        navscheduler.showMonths = 2;
        navscheduler.skipMonths = 2;
        navscheduler.selectMode = "month";
        navscheduler.weekStarts = 0;
        navscheduler.onTimeRangeSelected = function (args) {
            var dpscheduler;
            try {
                dpscheduler = $control.data('dpscheduler');
                dpscheduler.startDate = args.start;
                dpscheduler.days = args.days;
                FwScheduler.loadEvents($control);
                dpscheduler.update();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        };
        navscheduler.init();
    };
    //---------------------------------------------------------------------------------
    loadCalendar($control) {
        var dpcalendar;
        dpcalendar = new DayPilot.Calendar($control.attr('data-dpcalendarid'));
        $control.data('dpcalendar', dpcalendar);
        dpcalendar.cellGroupBy = "Day"
        dpcalendar.eventClickHandling = 'Disabled';
        dpcalendar.eventMoveHandling = 'Disabled';
        dpcalendar.eventResizeHandling = 'Disabled';
        dpcalendar.weekStarts = 0;
        dpcalendar.businessBeginsHour = 0;
        dpcalendar.businessEndsHour = 11;
        dpcalendar.onTimeRangeSelected = function (args) {
            try {
                $control.data('selectedstartdate', args.start);
                $control.data('selectedenddate', args.end.addSeconds(-1));
                dpcalendar.clearSelection();
                dpcalendar.update();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        };
        dpcalendar.init();
    };
    //---------------------------------------------------------------------------------
    loadMonth($control) {
        var dpmonth;
        dpmonth = new DayPilot.Month($control.attr('data-dpmonthid'));
        $control.data('dpmonth', dpmonth);
        dpmonth.cellWidth = 40;
        dpmonth.eventHeight = 25;
        dpmonth.headerHeight = 25;
        dpmonth.rowHeaderWidth = 200;
        dpmonth.weekStarts = 0;
        dpmonth.eventClickHandling = 'Disabled';
        dpmonth.eventMoveHandling = 'Disabled';
        dpmonth.eventResizeHandling = 'Disabled';
        dpmonth.viewType = 'Month';
        //dpmonth.cssClassPrefix = "month_white";
        dpmonth.onTimeRangeSelected = function (args) {
            $control.data('selectedstartdate', args.start);
            $control.data('selectedenddate', args.end.addSeconds(-1));
            dpmonth.clearSelection();
            dpmonth.update();
        };
        if (typeof $control.data('ontimerangedoubleclicked') === 'function') dpmonth.onTimeRangeDoubleClicked = $control.data('ontimerangedoubleclicked');
        if (typeof $control.data('oneventclick') === 'function') dpmonth.onEventClick = $control.data('oneventclick');
        if (typeof $control.data('ontimerangeselect') === 'function') dpmonth.onTimeRangeSelect = $control.data('ontimerangeselect');
        dpmonth.onBeforeCellRender = function (args) {
            var selectedstartdate, selectedenddate;

            args.cell.backColor = ((args.cell.start.getMonth() % 2) === 0) ? "#f0f0f0" : "#e2e2e2";

            // color today's date
            if (args.cell.start.getDatePart().getTime() === new DayPilot.Date().getDatePart().getTime()) {
                args.cell.backColor = "rgb(255, 231, 148)";
            }

            // color the selection
            selectedstartdate = $control.data('selectedstartdate');
            selectedenddate = $control.data('selectedenddate');
            if (typeof selectedstartdate !== 'undefined') {
                if ((args.cell.start.getDatePart().getTime() >= selectedstartdate.getDatePart().getTime()) && (args.cell.start.getDatePart().getTime() <= selectedenddate.getDatePart().getTime())) {
                    args.cell.cssClass = 'selecteddate';
                }
            }
        };
        dpmonth.bubble = new DayPilot.Bubble({
            cssOnly: true,
            cssClassPrefix: "bubble_default"
            //,
            //onLoad: function(args) {
            //  var ev = args.source;
            //  args.async = true;  // notify manually using .loaded()

            //  // simulating slow server-side load
            //  setTimeout(function() {
            //    args.html = "testing bubble for: <br>" + ev.text();
            //    args.loaded();
            //  }, 500);
            //}
        });
        dpmonth.init();
    };
    //---------------------------------------------------------------------------------
    load5Week($control) {
        var dp5week;
        dp5week = new DayPilot.Month($control.attr('data-dp5weekid'));
        $control.data('dp5week', dp5week);
        dp5week.cellWidth = 40;
        dp5week.eventHeight = 25;
        dp5week.headerHeight = 25;
        dp5week.rowHeaderWidth = 200;
        dp5week.weekStarts = 0;
        dp5week.eventClickHandling = 'Disabled';
        dp5week.eventMoveHandling = 'Disabled';
        dp5week.eventResizeHandling = 'Disabled';
        dp5week.viewType = 'Weeks';
        dp5week.weeks = 5;
        //dp5week.cssClassPrefix = "month_white";
        dp5week.onTimeRangeSelected = function (args) {
            $control.data('selectedstartdate', args.start);
            $control.data('selectedenddate', args.end.addSeconds(-1));
            dp5week.clearSelection();
            dp5week.update();
        };
        if (typeof $control.data('ontimerangedoubleclicked') === 'function') dp5week.onTimeRangeDoubleClicked = $control.data('ontimerangedoubleclicked');
        if (typeof $control.data('oneventclick') === 'function') dp5week.onEventClick = $control.data('oneventclick');
        if (typeof $control.data('ontimerangeselect') === 'function') dp5week.onTimeRangeSelect = $control.data('ontimerangeselect');

        dp5week.onBeforeCellRender = function (args) {
            var selectedstartdate, selectedenddate;

            args.cell.backColor = ((args.cell.start.getMonth() % 2) === 0) ? "#f0f0f0" : "#e2e2e2";

            // color today's date
            if (args.cell.start.getDatePart().getTime() === new DayPilot.Date().getDatePart().getTime()) {
                args.cell.backColor = "rgb(255, 231, 148)";
            }

            // color the selection
            selectedstartdate = $control.data('selectedstartdate');
            selectedenddate = $control.data('selectedenddate');
            if (typeof selectedstartdate !== 'undefined') {
                if ((args.cell.start.getDatePart().getTime() >= selectedstartdate.getDatePart().getTime()) && (args.cell.start.getDatePart().getTime() <= selectedenddate.getDatePart().getTime())) {
                    args.cell.cssClass = 'selecteddate';
                }
            }
        };
        dp5week.bubble = new DayPilot.Bubble({
            cssOnly: true,
            cssClassPrefix: "bubble_default"
            //,
            //onLoad: function(args) {
            //  var ev = args.source;
            //  args.async = true;  // notify manually using .loaded()

            //  // simulating slow server-side load
            //  setTimeout(function() {
            //    args.html = "testing bubble for: <br>" + ev.text();
            //    args.loaded();
            //  }, 500);
            //}
        });
        dp5week.init();
    };
    //---------------------------------------------------------------------------------
    loadYear($control) {
        var dpyear;
        dpyear = new DayPilot.Scheduler($control.attr('data-dpyearid'));
        $control.data('dpyear', dpyear);
        dpyear.startDate = this.getFirstSundayMonth(dpyear);
        dpyear.cellWidth = 50;
        dpyear.eventHeight = 30;
        dpyear.headerHeight = 25;
        dpyear.days = 37;
        dpyear.scale = "Day";
        dpyear.timeHeaders = [{ groupBy: "Day", format: "ddd" }];
        dpyear.eventClickHandling = 'Disabled';
        dpyear.eventMoveHandling = 'Disabled';
        dpyear.eventResizeHandling = 'Disabled';

        dpyear.bubble = new DayPilot.Bubble({
            cssClassPrefix: "bubble_default",
            onLoad: function (args) {
                var ev = args.source;
                args.async = true;  // notify manually using .loaded()

                // simulating slow server-side load
                args.html = "<div style='font-weight:bold'>" + ev.text() + "</div><div>Order Number: " + ev.data.orderNumber + "</div><div>Order Status: " + ev.data.orderStatus + "</div><div>Deal: " + ev.data.deal + "</div><div>Start: " + ev.data.realStart + "</div><div>End: " + ev.data.realEnd + "</div><div>Id: " + ev.id() + "</div>";
                args.loaded();

            }
        });
        dpyear.init();
    };
    //---------------------------------------------------------------------------------
    loadScheduler($control) {
        var dpscheduler;
        dpscheduler = new DayPilot.Scheduler($control.attr('data-dpschedulerid'));
        $control.data('dpscheduler', dpscheduler);
        dpscheduler.cellWidth = 80;
        dpscheduler.eventHeight = 25;
        dpscheduler.headerHeight = 25;
        dpscheduler.cellGroupBy = "Month";
        dpscheduler.startDate = dpscheduler.startDate.firstDayOfMonth();
        dpscheduler.days = dpscheduler.startDate.daysInMonth();
        dpscheduler.timeHeaders = [
            { groupBy: "Month", format: "MMM yyyy" },
            { groupBy: "Cell", format: "ddd d" }
        ];

        dpscheduler.cellDuration = 1440; // one day
        dpscheduler.moveBy = 'Full';
        dpscheduler.treeEnabled = true;
        dpscheduler.rowHeaderWidth = 200;
        dpscheduler.cellGroupBy = "Month"
        dpscheduler.eventClickHandling = 'Disabled';
        dpscheduler.eventMoveHandling = 'Disabled';
        dpscheduler.eventResizeHandling = 'Disabled';
        dpscheduler.weekStarts = 0;
        dpscheduler.onTimeRangeSelected = function (args) {
            $control.data('selectedstartdate', args.start);
            $control.data('selectedenddate', args.end.addSeconds(-1));
            dpscheduler.clearSelection();
        };
        dpscheduler.init();
    };
    //---------------------------------------------------------------------------------
    navigate($control, date) {
        var navcalendar, nav5week, navmonth, navyear, navscheduler, $changeview, dpmonth;

        if (typeof date === 'string') {
            date = DayPilot.Date(new Date(date).toISOString(), true).getDatePart();
        }
        FwScheduler.setSelectedDay($control, date);
        $changeview = $control.find('div.changeview[data-selected="true"]');
        switch ($changeview.html()) {
            case 'Day':
            case 'Week':
                navcalendar = $control.data('navcalendar');
                navcalendar.select(date);   
                break;
            case '5 Week':
                nav5week = $control.data('nav5week');
                nav5week.select(date);
                nav5week._timeRangeSelectedDispatch();
                break;
            case 'Month':
                navmonth = $control.data('navmonth');
                navmonth.select(date);
                navmonth._timeRangeSelectedDispatch();
                break;
            case 'Year':
                break;
            case 'Schedule':
                navscheduler = $control.data('navscheduler');
                navscheduler.select(date);
                break;
        }
    };
    //---------------------------------------------------------------------------------
    showDayView($control) {
        var navcalendar, dpcalendar, selectedstartdate;
        navcalendar = $control.data('navcalendar');
        navcalendar.selectMode = "day";
        navcalendar.update();
        dpcalendar = $control.data('dpcalendar');
        dpcalendar.viewType = "Day"
        dpcalendar.update();
        $control.find('.fiveweekcontainer').hide();
        $control.find('.monthcontainer').hide();
        $control.find('.schedulercontainer').hide();
        $control.find('.calendarcontainer').show();
        $control.find('.yearcontainer').hide();
        $control.find('.changeview').attr('data-selected', 'false');
        $control.find('.btnDay').attr('data-selected', 'true');
        if ($control.next().data('dpscheduler') !== undefined) {
            $control.next().data('dpscheduler').days = 1;
            $control.next().data('dpscheduler').update();
        }
        if (typeof $control.data('selectedstartdate') !== 'undefined') {
            selectedstartdate = $control.data('selectedstartdate');
            FwScheduler.navigate($control, selectedstartdate);
        }
    };
    //---------------------------------------------------------------------------------
    showWeekView($control) {
        var navcalendar, dpcalendar, selectedstartdate;
        navcalendar = $control.data('navcalendar');
        navcalendar.selectMode = "week";
        navcalendar.update();
        dpcalendar = $control.data('dpcalendar');
        dpcalendar.viewType = "Week"
        dpcalendar.update();
        $control.find('.fiveweekcontainer').hide();
        $control.find('.monthcontainer').hide();
        $control.find('.schedulercontainer').hide();
        $control.find('.calendarcontainer').show();
        $control.find('.yearcontainer').hide();
        $control.find('.changeview').attr('data-selected', 'false');
        $control.find('.btnWeek').attr('data-selected', 'true');
        if ($control.next().data('dpscheduler') !== undefined) {
            $control.next().data('dpscheduler').days = 7;
            $control.next().data('dpscheduler').update();
        }
        if (typeof $control.data('selectedstartdate') !== 'undefined') {
            selectedstartdate = $control.data('selectedstartdate');
            FwScheduler.navigate($control, selectedstartdate);
        }
    };
    //---------------------------------------------------------------------------------
    show5WeekView($control) {
        var navcalendar, dpcalendar, selectedstartdate;
        navcalendar = $control.data('navcalendar');
        navcalendar.selectMode = "week";
        navcalendar.update();
        $control.find('.fiveweekcontainer').show();
        $control.find('.calendarcontainer').hide();
        $control.find('.schedulercontainer').hide();
        $control.find('.monthcontainer').hide();
        $control.find('.yearcontainer').hide();
        $control.find('.changeview').attr('data-selected', 'false');
        $control.find('.btn5Week').attr('data-selected', 'true');
        if ($control.next().data('dpscheduler') !== undefined) {
            $control.next().data('dpscheduler').days = 35;
            $control.next().data('dpscheduler').update();
        }
        if (typeof $control.data('selectedstartdate') !== 'undefined') {
            selectedstartdate = $control.data('selectedstartdate');
            FwScheduler.navigate($control, selectedstartdate);
        }
    };
    //---------------------------------------------------------------------------------
    showMonthView($control) {
        var navcalendar, selectedstartdate, dpcalendar;
        navcalendar = $control.data('navcalendar');
        navcalendar.selectMode = "month";
        navcalendar.update();
        dpcalendar = $control.data('dpcalendar');
        dpcalendar.viewType = "Month"
        dpcalendar.update();
        $control.find('.fiveweekcontainer').hide();
        $control.find('.calendarcontainer').hide();
        $control.find('.schedulercontainer').hide();
        $control.find('.monthcontainer').show();
        $control.find('.yearcontainer').hide();
        $control.find('.changeview').attr('data-selected', 'false');
        $control.find('.btnMonth').attr('data-selected', 'true');
        if ($control.next().data('dpscheduler') !== undefined) {
            $control.next().data('dpscheduler').days = 31;
            $control.next().data('dpscheduler').update();
        }
        if (typeof $control.data('selectedstartdate') !== 'undefined') {
            selectedstartdate = $control.data('selectedstartdate');
            FwScheduler.navigate($control, selectedstartdate);
        }
    };
    //---------------------------------------------------------------------------------
    showYearView($control) {
        var selectedstartdate, dpcalendar;
        dpcalendar = $control.data('dpcalendar');
        dpcalendar.viewType = "Year"
        dpcalendar.update();
        $control.find('.fiveweekcontainer').hide();
        $control.find('.calendarcontainer').hide();
        $control.find('.schedulercontainer').hide();
        $control.find('.monthcontainer').hide();
        $control.find('.yearcontainer').show();
        $control.find('.changeview').attr('data-selected', 'false');
        $control.find('.btnYear').attr('data-selected', 'true');
        if (typeof $control.data('selectedstartdate') !== 'undefined') {
            selectedstartdate = $control.data('selectedstartdate');
            FwScheduler.navigate($control, selectedstartdate);
        }
        FwScheduler.refresh($control);
    };
    //---------------------------------------------------------------------------------
    showScheduleView($control) {
        var navcalendar, selectedstartdate;
        navcalendar = $control.data('navcalendar');
        navcalendar.selectMode = "month";
        navcalendar.update();
        $control.find('.calendarcontainer').hide();
        $control.find('.monthcontainer').hide();
        $control.find('.schedulercontainer').show();
        $control.find('.changeview').attr('data-selected', 'false');
        $control.find('.btnSchedule').attr('data-selected', 'true');
        if (typeof $control.data('selectedstartdate') !== 'undefined') {
            selectedstartdate = $control.data('selectedstartdate');
            FwScheduler.navigate($control, selectedstartdate);
        }
    };
    //---------------------------------------------------------------------------------
    refresh($control) {
        FwScheduler.loadEvents($control);
    };
    //---------------------------------------------------------------------------------
    loadEvents($control) {
        var ongetevents, request, dpcalendar, dp5week, dpmonth, dpscheduler, start, days;
        dpcalendar = $control.data('dpcalendar');
        dp5week = $control.data('dp5week');
        dpmonth = $control.data('dpmonth');
        dpscheduler = $control.data('dpscheduler');

        if (typeof $control.data('ongetevents') === 'function') {
            ongetevents = $control.data('ongetevents');
            switch ($control.find('div.changeview[data-selected="true"]').html()) {
                case 'Day':
                case 'Week':
                    start = dpcalendar.startDate.addDays(-dpmonth.startDate.dayOfWeek()) // add the trailing days from the previous month that are visible
                    days = dpcalendar.days + dpmonth.startDate.dayOfWeek() + (6 - dpmonth.startDate.addDays(dpmonth.days).dayOfWeek()) // add the first few days from the next month that are visible
                    break;
                case '5 Week':
                    start = dp5week.startDate.addDays(-dp5week.startDate.dayOfWeek()) // add the trailing days from the previous month that are visible
                    days = 34; // add the first few days from the next month that are visible
                    break;
                case 'Month':
                    start = dpmonth.startDate.addDays(-dpmonth.startDate.dayOfWeek()) // add the trailing days from the previous month that are visible
                    days = dpmonth.days + dpmonth.startDate.dayOfWeek() + (6 - dpmonth.startDate.addDays(dpmonth.days).dayOfWeek()) // add the first few days from the next month that are visible
                    break;
                case 'Year':
                    start = dpmonth.startDate.addDays(-dpmonth.startDate.dayOfWeek()) // add the trailing days from the previous month that are visible
                    days = dpmonth.days + dpmonth.startDate.dayOfWeek() + (6 - dpmonth.startDate.addDays(dpmonth.days).dayOfWeek()) // add the first few days from the next month that are visible
                    break;
                case 'Schedule':
                    start = dpscheduler.startDate;
                    days = dpscheduler.startDate.daysInMonth();
                    break;
            }
            //start       = dpmonth.startDate.addDays(-dpmonth.startDate.dayOfWeek()) // add the trailing days from the previous month that are visible
            //days        = dpmonth.days + dpmonth.startDate.dayOfWeek() + (6 - dpmonth.startDate.addDays(dpmonth.days).dayOfWeek()) // add the first few days from the next month that are visible
            request = {
                start: start,
                days: days,
                mode: $control.find('div.changeview[data-selected="true"]').html()
            };
            ongetevents(request);
        } else {
            throw 'ongetevents is not implemented.';
        }
    };
    //---------------------------------------------------------------------------------
    loadEventsCallback($control, resources, events) {
        var dpcalendar, dp5week, dpmonth, dpscheduler, dpyear, start, end, request;
        dpcalendar = $control.data('dpcalendar');
        dp5week = $control.data('dp5week');
        dpmonth = $control.data('dpmonth');
        dpyear = $control.data('dpyear');
        dpscheduler = $control.data('dpscheduler');

        if ((typeof dpcalendar !== 'undefined') && (typeof dpmonth !== 'undefined') && (typeof dpscheduler !== 'undefined')) {
            switch ($control.find('div.changeview[data-selected="true"]').html()) {
                case 'Day':
                    FwScheduler.setDateCallout($control, dpcalendar.startDate);
                    break;
                case 'Week':
                    FwScheduler.setDateCallout($control, dpcalendar.startDate);
                    break;
                case '5 Week':
                    FwScheduler.setDateCallout($control, dp5week.startDate);
                    break;
                case 'Month':
                    FwScheduler.setDateCallout($control, dpmonth.startDate);
                    break;
                case 'Year':
                    FwScheduler.setDateCallout($control, dpyear.startDate);
                    break;
                case 'Schedule':
                    FwScheduler.setDateCallout($control, dpscheduler.startDate);
                    break;
            }

            dpcalendar.resources = resources;
            dpcalendar.events.list = events;
            dpcalendar.update();

            dp5week.resources = resources;
            dp5week.events.list = events;
            dp5week.update();

            dpmonth.resources = resources;
            dpmonth.events.list = events;
            dpmonth.update();

            dpscheduler.resources = resources;
            dpscheduler.events.list = events;
            dpscheduler.update();
        } else {
            // the user must have navigated away before this finished loading
        }
    };
    //---------------------------------------------------------------------------------
    loadYearEventsCallback($control, resources, events) {
        var dpyear, start, end, request;
        dpyear = $control.data('dpyear');

        if ((typeof dpyear !== 'undefined')) {
            if ($control.find('div.changeview[data-selected="true"]').html() === 'Year') {
                FwScheduler.setDateCallout($control, dpyear.startDate);
            }
            dpyear.resources = [
                { name: "January", id: "A" },
                { name: "February", id: "B" },
                { name: "March", id: "C" },
                { name: "April", id: "D" },
                { name: "May", id: "E" },
                { name: "June", id: "F" },
                { name: "July", id: "G" },
                { name: "August", id: "H" },
                { name: "September", id: "I" },
                { name: "October", id: "J" },
                { name: "November", id: "K" },
                { name: "December", id: "L" }
            ];
            dpyear.events.list = events;
            dpyear.update();
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
        var dpcalendar, dpmonth, dpscheduler, e, action;
        dpcalendar = $control.data('dpcalendar');
        dpmonth = $control.data('dpmonth');
        dpscheduler = $control.data('dpscheduler');
        if (typeof start === 'string') {
            start = new DayPilot.Date(new Date(start).toISOString());
        }
        if (typeof end === 'string') {
            end = new DayPilot.Date(new Date(end).toISOString());
        }
        $control.data('selectedstartdate', start);
        $control.data('selectedenddate', end);
        dpcalendar.update();
        dpmonth.update();
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
        FwScheduler.setSelectedTimeRange($control, start, end);
    };
    //---------------------------------------------------------------------------------
    setDateCallout($control, date) {
        var $datecallout, monthnames, firstdayofweek, lastdayofweek;
        monthnames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        $datecallout = $control.find('.datecallout');
        switch ($control.find('div.changeview[data-selected="true"]').html()) {
            case 'Day':
                $datecallout.html(monthnames[date.getMonth()] + ' ' + date.getDay() + ', ' + date.getYear());
                break;
            case 'Week':
                firstdayofweek = date.firstDayOfWeek();
                lastdayofweek = firstdayofweek.addDays(6);
                $datecallout.html(monthnames[firstdayofweek.getMonth()] + ' ' + firstdayofweek.getDay() + ' - ' + monthnames[lastdayofweek.getMonth()] + ' ' + lastdayofweek.getDay() + ', ' + lastdayofweek.getYear());
                break;
            case '5 Week':
                firstdayofweek = date.firstDayOfWeek();
                lastdayofweek = firstdayofweek.addDays(34);
                $datecallout.html(monthnames[firstdayofweek.getMonth()] + ' ' + firstdayofweek.getDay() + ' - ' + monthnames[lastdayofweek.getMonth()] + ' ' + lastdayofweek.getDay() + ', ' + lastdayofweek.getYear());
                break;
            case 'Year':
                $datecallout.html(date.getYear());
                break;
            case 'Month':
            case 'Schedule':
                $datecallout.html(monthnames[date.getMonth()] + ' ' + date.getYear());
                break;
        }
    };
    //---------------------------------------------------------------------------------
    // see http://api.daypilot.org/daypilot-event-data/ for info on how to create the evt object
    addEvent($control, evt) {
        var dpcalendar, dpmonth, dpscheduler, e, action;
        dpcalendar = $control.data('dpcalendar');
        dpmonth = $control.data('dpmonth');
        dpscheduler = $control.data('dpscheduler');
        action = dpcalendar.events.add(evt);
        action = dpmonth.events.add(evt);
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
            FwScheduler.navigate($control, new DayPilot.Date());
            FwScheduler.loadEvents($control);
        }
    };
    //---------------------------------------------------------------------------------
    getTodaysDate() {
        return new DayPilot.Date(new Date().toISOString());
    }
    //---------------------------------------------------------------------------------
    getFirstSundayMonth(year) {
        for (var i = 0; i < 12; i++) {
            if (moment().startOf('year').add(i, 'M').format('dddd') === 'Sunday') {
                return moment().startOf('year').add(i, 'M').format('YYYY-MM-DD');
            }
        }
    }
    //---------------------------------------------------------------------------------
}

var FwScheduler = new FwSchedulerClass();