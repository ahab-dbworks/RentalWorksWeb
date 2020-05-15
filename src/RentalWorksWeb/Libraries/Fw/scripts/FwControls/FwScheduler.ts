// You need to include DayPilot javascript library in web application project for this component to work and reference the javascript/css it needs.
//---------------------------------------------------------------------------------
class FwSchedulerClass {
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control) {
        var controller;

        const navcalendarid = FwControl.generateControlId('navcalendar');
        $control.attr('data-navcalendarid', navcalendarid);
        const dpcalendarid = FwControl.generateControlId('dpcalendar');
        $control.attr('data-dpcalendarid', dpcalendarid);
        const navmonthid = FwControl.generateControlId('navmonth');
        $control.attr('data-navmonthid', navmonthid);
        const navyearid = FwControl.generateControlId('navyear');
        $control.attr('data-navyearid', navyearid);
        const dpmonthid = FwControl.generateControlId('dpmonth');
        $control.attr('data-dpmonthid', dpmonthid);
        const nav5weekid = FwControl.generateControlId('nav5week');
        $control.attr('data-nav5weekid', nav5weekid);
        const dp5weekid = FwControl.generateControlId('dp5weekid');
        $control.attr('data-dp5weekid', dp5weekid);
        const dpyearid = FwControl.generateControlId('dpyearid');
        $control.attr('data-dpyearid', dpyearid);
        const navschedulerid = FwControl.generateControlId('navscheduler');
        $control.attr('data-navschedulerid', navschedulerid);
        const dpschedulerid = FwControl.generateControlId('dpscheduler');
        $control.attr('data-dpschedulerid', dpschedulerid);
        const html: Array<string> = [];
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
        html.push('    <div class="navyearcontainer">');
        html.push(`      <div id="${navyearid}"></div>`);
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
        $control.html(html.join('\n'));

        const $calendarmenu = $control.find('.calendarmenu');
        const $menucontrol = FwMenu.getMenuControl('default');
        $calendarmenu.append($menucontrol);
        const schedulerbtns: Array<string> = [];
        schedulerbtns.push('<div class="schedulerbtns">');
        //schedulerbtns.push('  <div class="lblView">View:</div>');
        schedulerbtns.push('  <div class="toggleView">');
        schedulerbtns.push('    <div class="changeview btnDay">Day</div>');
        schedulerbtns.push('    <div class="changeview btnWeek">Week</div>');
        schedulerbtns.push('    <div class="changeview btn5Week">5 Week</div>');
        schedulerbtns.push('    <div class="changeview btnMonth">Month</div>');
        schedulerbtns.push('    <div class="changeview btnYear">Year</div>');
        schedulerbtns.push('    <div class="changeview btnSchedule">Schedule</div>');
        schedulerbtns.push('  </div>');
        schedulerbtns.push('  <div class="topnavigation">');
        schedulerbtns.push('    <button class="btnRefreshCalendar">Refresh</button><button class="btnToday">Today</button><button class="btnPrev">&lt;</button><button class="btnNext">&gt;</button>');
        schedulerbtns.push('  </div>');
        schedulerbtns.push('  <div class="datecallout"></div>');
        schedulerbtns.push('  <div class="jumpdate"><span>Jump To: <input class="value" type="text" data-type="text" /><i class="material-icons btndate">&#xE8DF;</i></span></div>');

        schedulerbtns.push('</div>');
        const $schedulerbtns: any = schedulerbtns.join('\n');
        FwMenu.addCustomContent($menucontrol, $schedulerbtns);

        const $form = $control.closest('.fwform');
        controller = window[$form.attr('data-controller')];
        if ((typeof controller !== 'undefined') && (typeof controller.addSchedulerMenuItems !== 'undefined')) {
            controller.addSchedulerMenuItems($menucontrol, $form);
        }

        let viewCount = 0;
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

        // menu date input
        const $datebtn = $control.find('div[data-control="FwMenu"] .schedulerbtns .jumpdate input.value')
        $datebtn.inputmask('mm/dd/yyyy');
        $datebtn.datepicker({
            autoclose: true,
            format: "m/d/yyyy",
            todayHighlight: true,
            firstDay: 1,
        }).off('focus');
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
        $control.on('click', '.btnToday', e => {
            e.stopImmediatePropagation();

            try {
                const today = new DayPilot.Date();
                FwScheduler.navigate($control, today);
                $control.find('div[data-control="FwMenu"] .schedulerbtns .jumpdate input.value').val('');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnDay', e => {
            e.stopImmediatePropagation();

            try {
                FwScheduler.showDayView($control);
                $control.find('div[data-control="FwMenu"] .schedulerbtns .jumpdate input.value').val('');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnWeek', e => {
            e.stopImmediatePropagation();

            try {
                FwScheduler.showWeekView($control);
                $control.find('div[data-control="FwMenu"] .schedulerbtns .jumpdate input.value').val('');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btn5Week', e => {
            e.stopImmediatePropagation();

            try {
                FwScheduler.show5WeekView($control);
                $control.find('div[data-control="FwMenu"] .schedulerbtns .jumpdate input.value').val('');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnMonth', e => {
            e.stopImmediatePropagation();

            try {
                FwScheduler.showMonthView($control);
                $control.find('div[data-control="FwMenu"] .schedulerbtns .jumpdate input.value').val('');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnYear', e => {
            e.stopImmediatePropagation();

            try {
                FwScheduler.showYearView($control);
                $control.find('div[data-control="FwMenu"] .schedulerbtns .jumpdate input.value').val('');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnSchedule', e => {
            e.stopImmediatePropagation();

            try {
                FwScheduler.showScheduleView($control);
                $control.find('div[data-control="FwMenu"] .schedulerbtns .jumpdate input.value').val('');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnNext', e => {
            e.stopImmediatePropagation();
            let currentDay, nextDay, nextWeek, next5Week, nextMonth, nextYear, navcalendar, nav5week, navmonth, navscheduler, schedulerDetailed;

            try {
                const $schedulerControl = $control.parents().find('.realscheduler')
                if ($schedulerControl.length) {
                    schedulerDetailed = $schedulerControl.data('dpscheduler');
                }
                if ($control.find('.btnDay').attr('data-selected') === 'true') {
                    navcalendar = $control.data('navcalendar');
                    currentDay = navcalendar.selectionStart;
                    nextDay = currentDay.addDays(1);
                    FwScheduler.navigate($control, nextDay);
                    if (schedulerDetailed !== undefined) {
                        FwSchedulerDetailed.navigate($schedulerControl, nextDay, 1);
                    }
                } else if ($control.find('.btnWeek').attr('data-selected') === 'true') {
                    navcalendar = $control.data('navcalendar');
                    currentDay = navcalendar.selectionStart;
                    nextWeek = currentDay.addDays(7);
                    FwScheduler.navigate($control, nextWeek);
                    if (schedulerDetailed !== undefined) {
                        FwSchedulerDetailed.navigate($schedulerControl, nextWeek, 7);
                    }
                } else if ($control.find('.btn5Week').attr('data-selected') === 'true') {
                    nav5week = $control.data('nav5week');
                    currentDay = nav5week.selectionDay;
                    next5Week = currentDay.addDays(35);
                    FwScheduler.navigate($control, next5Week);
                    if (schedulerDetailed !== undefined) {
                        FwSchedulerDetailed.navigate($schedulerControl, next5Week, 35);
                    }
                } else if ($control.find('.btnMonth').attr('data-selected') === 'true') {
                    navmonth = $control.data('navmonth');
                    currentDay = navmonth.selectionStart;
                    nextMonth = currentDay.addMonths(1);
                    FwScheduler.navigate($control, nextMonth);
                    if (schedulerDetailed !== undefined) {
                        FwSchedulerDetailed.navigate($schedulerControl, nextMonth, 31);
                    }
                } else if ($control.find('.btnYear').attr('data-selected') === 'true') {
                    navmonth = $control.data('navmonth');
                    currentDay = navmonth.selectionStart;
                    nextYear = currentDay.addMonths(12);
                    FwScheduler.navigate($control, nextYear);
                } else if ($control.find('.btnSchedule').attr('data-selected') === 'true') {
                    navscheduler = $control.data('navscheduler');
                    currentDay = navscheduler.selectionStart;
                    nextMonth = currentDay.addMonths(1);
                    FwScheduler.navigate($control, nextMonth);
                }
                $control.find('div[data-control="FwMenu"] .schedulerbtns .jumpdate input.value').val('');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnPrev', e => {
            e.stopImmediatePropagation();
            let currentDay, previousDay, previousWeek, previous5Week, previousMonth, nav5week, navcalendar, navmonth, navscheduler, schedulerDetailed;

            try {
                const $schedulerControl = $control.parents().find('.realscheduler')
                if ($schedulerControl.length) {
                    schedulerDetailed = $schedulerControl.data('dpscheduler');
                }
                if ($control.find('.btnDay').attr('data-selected') === 'true') {
                    navcalendar = $control.data('navcalendar');
                    currentDay = navcalendar.selectionStart;
                    previousDay = currentDay.addDays(-1);
                    FwScheduler.navigate($control, previousDay);
                    if (schedulerDetailed !== undefined) {
                        FwSchedulerDetailed.navigate($schedulerControl, previousDay, 1);
                    }
                } else if ($control.find('.btnWeek').attr('data-selected') === 'true') {
                    navcalendar = $control.data('navcalendar');
                    currentDay = navcalendar.selectionStart;
                    previousWeek = currentDay.addDays(-7);
                    FwScheduler.navigate($control, previousWeek);
                    if (schedulerDetailed !== undefined) {
                        FwSchedulerDetailed.navigate($schedulerControl, previousWeek, 7);
                    }
                } else if ($control.find('.btn5Week').attr('data-selected') === 'true') {
                    nav5week = $control.data('nav5week');
                    currentDay = nav5week.selectionDay;
                    previous5Week = currentDay.addDays(-35);
                    FwScheduler.navigate($control, previous5Week);
                    if (schedulerDetailed !== undefined) {
                        FwSchedulerDetailed.navigate($schedulerControl, previous5Week, 35);
                    }
                } else if ($control.find('.btnMonth').attr('data-selected') === 'true') {
                    navmonth = $control.data('navmonth');
                    currentDay = navmonth.selectionStart;
                    previousMonth = currentDay.addMonths(-1);
                    FwScheduler.navigate($control, previousMonth);
                    if (schedulerDetailed !== undefined) {
                        FwSchedulerDetailed.navigate($schedulerControl, previousMonth, 31);
                    }
                } else if ($control.find('.btnYear').attr('data-selected') === 'true') {
                    navmonth = $control.data('navmonth');
                    currentDay = navmonth.selectionStart;
                    const previousYear = currentDay.addMonths(-12);
                    FwScheduler.navigate($control, previousYear);
                } else if ($control.find('.btnSchedule').attr('data-selected') === 'true') {
                    navscheduler = $control.data('navscheduler');
                    currentDay = navscheduler.selectionStart;
                    previousMonth = currentDay.addMonths(-1);
                    FwScheduler.navigate($control, previousMonth);
                }
                $control.find('div[data-control="FwMenu"] .schedulerbtns .jumpdate input.value').val('');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('click', '.btnRefreshCalendar', () => {
            try {
                FwScheduler.refresh($control);
                const $schedulerControl = $control.parents().find('.realscheduler');
                if ($schedulerControl.length > 0) {
                    FwSchedulerDetailed.refresh($schedulerControl);
                }
                $control.find('div[data-control="FwMenu"] .schedulerbtns .jumpdate input.value').val('');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        // Jump to Date
        $control.on('click', '.btndate', e => {
            try {
                const $this = jQuery(e.currentTarget);
                $this.closest('.jumpdate').find('input').datepicker('show');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $control.on('change', '.jumpdate input', e => {
            e.stopPropagation();
            try {
                const date = new Date(`${jQuery(e.currentTarget).val()}`);
                const dayPilotDate = new DayPilot.Date(date);
                FwScheduler.navigate($control, dayPilotDate);

                const $schedulerControl = $control.parents().find('.realscheduler');
                if ($schedulerControl.length > 0) {
                    FwSchedulerDetailed.navigate($schedulerControl, dayPilotDate)
                }
                $schedulerControl.find('div[data-control="FwMenu"] .schedulerbtns .jumpdate input.value').val(`${jQuery(e.currentTarget).val()}`);

            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        //$control.on('onactivatetab', () => {
        //    const $form = $control.closest('.fwform');

        //    if ($control.attr('data-refreshonactivatetab') !== 'false' && $form.attr('data-mode') !== 'NEW') {     //// -- J. Pace commented this out because it is creating dubplicate API requests
        //        FwScheduler.refresh($control);
        //    }
        //    $control.attr('data-refreshonactivatetab', 'true');
        //});
    };
    //---------------------------------------------------------------------------------
    loadControl($control) {
        FwScheduler.loadNavCalendar($control);
        FwScheduler.loadNavMonth($control);
        FwScheduler.loadNav5Week($control);
        FwScheduler.loadNavYear($control);
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
        const navcalendar = new DayPilot.Navigator($control.attr('data-navcalendarid'));
        $control.data('navcalendar', navcalendar);
        navcalendar.showMonths = 2;
        navcalendar.skipMonths = 2;
        navcalendar.selectMode = "day";
        const userid = JSON.parse(sessionStorage.getItem('userid'));
        if (userid.firstdayofweek) {
            navcalendar.weekStarts = userid.firstdayofweek;
        } else {
            navcalendar.weekStarts = 0;
        }
        navcalendar.onTimeRangeSelected = function (args) {
            try {
                const dpcalendar = $control.data('dpcalendar');
                dpcalendar.startDate = args.start;
                dpcalendar.days = args.days;
                FwScheduler.loadEvents($control);
                dpcalendar.update();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        };
        //navcalendar.eventDoubleClickHandling = "Enabled";
        //if (typeof $control.data('oneventdoubleclicked') === 'function') navcalendar.onEventDoubleClicked = $control.data('oneventdoubleclicked');
        navcalendar.init();
    };
    //---------------------------------------------------------------------------------
    loadNav5Week($control) {
        const nav5week = new DayPilot.Navigator($control.attr('data-nav5weekid'));
        $control.data('nav5week', nav5week);
        nav5week.showMonths = 3;
        nav5week.skipMonths = 3;
        nav5week.selectMode = "month";
        const userid = JSON.parse(sessionStorage.getItem('userid'));
        if (userid.firstdayofweek) {
            nav5week.weekStarts = userid.firstdayofweek;
        } else {
            nav5week.weekStarts = 0;
        }
        nav5week.onTimeRangeSelected = function (args) {
            try {
                const dp5week = $control.data('dp5week');
                dp5week.startDate = args.day;
                dp5week.days = 34;
                FwScheduler.loadEvents($control);
                dp5week.update();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        };
        //nav5week.eventDoubleClickHandling = "Enabled";
        //if (typeof $control.data('oneventdoubleclicked') === 'function') nav5week.onEventDoubleClicked = $control.data('oneventdoubleclicked');
        nav5week.init();
    };
    //---------------------------------------------------------------------------------
    loadNavMonth($control) {
        const navmonth = new DayPilot.Navigator($control.attr('data-navmonthid'));
        $control.data('navmonth', navmonth);
        navmonth.showMonths = 3;
        navmonth.skipMonths = 3;
        navmonth.selectMode = "month";
        const userid = JSON.parse(sessionStorage.getItem('userid'));
        if (userid.firstdayofweek) {
            navmonth.weekStarts = userid.firstdayofweek;
        } else {
            navmonth.weekStarts = 0;
        }
        navmonth.onTimeRangeSelected = function (args) {
            try {
                const dpmonth = $control.data('dpmonth');
                dpmonth.startDate = args.start;
                dpmonth.days = args.days;
                FwScheduler.loadEvents($control);
                dpmonth.update();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        };
        //navmonth.eventDoubleClickHandling = "Enabled";
        //if (typeof $control.data('oneventdoubleclicked') === 'function') navmonth.onEventDoubleClicked = $control.data('oneventdoubleclicked');
        navmonth.init();
    };
    //---------------------------------------------------------------------------------
    loadNavYear($control) {
        const navyear = new DayPilot.Navigator($control.attr('data-navyearid'));
        $control.data('navyear', navyear);
        navyear.showMonths = 12;
        navyear.skipMonths = 12;
        navyear.selectMode = "month";
        navyear.weekStarts = 0;
        navyear.onTimeRangeSelected = function (args) {
            try {
                const dpyear = $control.data('dpyear');
                dpyear.startDate = args.start;
                dpyear.days = args.days;
                FwScheduler.loadEvents($control);
                dpyear.update();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        };
        //navmonth.eventDoubleClickHandling = "Enabled";
        //if (typeof $control.data('oneventdoubleclicked') === 'function') navmonth.onEventDoubleClicked = $control.data('oneventdoubleclicked');
        navyear.init();
    };
    //---------------------------------------------------------------------------------
    loadNavScheduler($control) {
        const navscheduler = new DayPilot.Navigator($control.attr('data-navschedulerid'));
        $control.data('navscheduler', navscheduler);
        navscheduler.showMonths = 2;
        navscheduler.skipMonths = 2;
        navscheduler.selectMode = "month";
        navscheduler.weekStarts = 0;
        navscheduler.onTimeRangeSelected = function (args) {
            try {
                const dpscheduler = $control.data('dpscheduler');
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
        const dpcalendar = new DayPilot.Calendar($control.attr('data-dpcalendarid'));
        $control.data('dpcalendar', dpcalendar);
        dpcalendar.cellGroupBy = "Day"
        dpcalendar.eventClickHandling = 'Enabled';
        dpcalendar.eventMoveHandling = 'Disabled';
        dpcalendar.eventResizeHandling = 'Disabled';
        dpcalendar.heightSpec = 'Full';
        const userid = JSON.parse(sessionStorage.getItem('userid'));
        if (userid.firstdayofweek) {
            dpcalendar.weekStarts = userid.firstdayofweek;
        } else {
            dpcalendar.weekStarts = 0;
        }
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
        if (typeof $control.data('onheaderclick') === 'function') dpcalendar.onHeaderClick = $control.data('onheaderclick');
        dpcalendar.eventDoubleClickHandling = "Enabled";
        if (typeof $control.data('oneventdoubleclicked') === 'function') dpcalendar.onEventDoubleClicked = $control.data('oneventdoubleclicked');
        if (typeof $control.data('oneventclick') === 'function') dpcalendar.onEventClick = $control.data('oneventclick');

        dpcalendar.init();
    };
    //---------------------------------------------------------------------------------
    loadMonth($control) {
        const dpmonth = new DayPilot.Month($control.attr('data-dpmonthid'));
        $control.data('dpmonth', dpmonth);
        dpmonth.cellWidth = 40;
        dpmonth.eventHeight = 25;
        dpmonth.headerHeight = 25;
        dpmonth.rowHeaderWidth = 200;
        const userid = JSON.parse(sessionStorage.getItem('userid'));
        if (userid.firstdayofweek) {
            dpmonth.weekStarts = userid.firstdayofweek;
        } else {
            dpmonth.weekStarts = 0;
        }
        dpmonth.eventClickHandling = 'Enabled';
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
        dpmonth.eventDoubleClickHandling = "Enabled";
        if (typeof $control.data('oneventdoubleclicked') === 'function') dpmonth.onEventDoubleClicked = $control.data('oneventdoubleclicked');
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
        const dp5week = new DayPilot.Month($control.attr('data-dp5weekid'));
        $control.data('dp5week', dp5week);
        dp5week.cellWidth = 40;
        dp5week.eventHeight = 25;
        dp5week.headerHeight = 25;
        dp5week.rowHeaderWidth = 200;
        const userid = JSON.parse(sessionStorage.getItem('userid'));
        if (userid.firstdayofweek) {
            dp5week.weekStarts = userid.firstdayofweek;
        } else {
            dp5week.weekStarts = 0;
        }
        dp5week.eventClickHandling = 'Enabled';
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
        dp5week.eventDoubleClickHandling = "Enabled";
        if (typeof $control.data('oneventdoubleclicked') === 'function') dp5week.onEventDoubleClicked = $control.data('oneventdoubleclicked');
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
        const dpyear = new DayPilot.Scheduler($control.attr('data-dpyearid'));
        $control.data('dpyear', dpyear);
        dpyear.startDate = this.getFirstSundayMonth(dpyear);
        dpyear.cellWidth = 50;
        dpyear.eventHeight = 30;
        dpyear.headerHeight = 25;
        dpyear.days = 37;
        dpyear.scale = "Day";
        dpyear.timeHeaders = [{ groupBy: "Day", format: "ddd" }];
        dpyear.eventClickHandling = 'Enabled';
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
        dpyear.eventDoubleClickHandling = "Enabled";
        if (typeof $control.data('oneventdoubleclicked') === 'function') dpyear.onEventDoubleClicked = $control.data('oneventdoubleclicked');
        if (typeof $control.data('oneventclick') === 'function') dpyear.onEventClick = $control.data('oneventclick');

        dpyear.init();
    };
    //---------------------------------------------------------------------------------
    loadScheduler($control) {
        const dpscheduler = new DayPilot.Scheduler($control.attr('data-dpschedulerid'));
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
        let navcalendar, nav5week, navmonth, navscheduler;

        if (typeof date === 'string') {
            date = DayPilot.Date(new Date(date).toISOString(), true).getDatePart();
        }
        FwScheduler.setSelectedDay($control, date);
        const $changeview = $control.find('div.changeview[data-selected="true"]');
        switch ($changeview.html()) {
            case 'Day':
            case 'Week':
                navcalendar = $control.data('navcalendar');
                navcalendar.select(date);
                navcalendar._timeRangeSelectedDispatch();
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
                const navyear = $control.data('navyear');
                navyear.select(date);
                navyear._timeRangeSelectedDispatch();
                break;
            case 'Schedule':
                navscheduler = $control.data('navscheduler');
                navscheduler.select(date);
                break;
        }
    };
    //---------------------------------------------------------------------------------
    showDayView($control) {
        const navcalendar = $control.data('navcalendar');
        navcalendar.selectMode = "day";
        navcalendar.update();
        const dpcalendar = $control.data('dpcalendar');
        dpcalendar.viewType = "Day"
        dpcalendar.update();
        $control.find('.fiveweekcontainer').hide();
        $control.find('.monthcontainer').hide();
        $control.find('.schedulercontainer').hide();
        $control.find('.calendarcontainer').show();
        $control.find('.yearcontainer').hide();
        if ($control.attr('data-shownav') === 'false') {
            $control.find('.navcalendarcontainer').hide();
        }
        $control.find('.changeview').attr('data-selected', 'false');
        $control.find('.btnDay').attr('data-selected', 'true');
        if ($control.next().data('dpscheduler') !== undefined) {
            $control.next().data('dpscheduler').days = 1;
            $control.next().data('dpscheduler').update();
        }
        if (typeof $control.data('selectedstartdate') !== 'undefined') {
            const selectedstartdate = $control.data('selectedstartdate');
            FwScheduler.navigate($control, selectedstartdate);
        }
    };
    //---------------------------------------------------------------------------------
    showWeekView($control) {
        const navcalendar = $control.data('navcalendar');
        navcalendar.selectMode = "week";
        navcalendar.update();
        const dpcalendar = $control.data('dpcalendar');
        dpcalendar.viewType = "Week"
        dpcalendar.update();
        $control.find('.fiveweekcontainer').hide();
        $control.find('.monthcontainer').hide();
        $control.find('.schedulercontainer').hide();
        $control.find('.calendarcontainer').show();
        $control.find('.yearcontainer').hide();
        if ($control.attr('data-shownav') === 'false') {
            $control.find('.navcalendarcontainer').hide();
        }
        $control.find('.changeview').attr('data-selected', 'false');
        $control.find('.btnWeek').attr('data-selected', 'true');
        if ($control.next().data('dpscheduler') !== undefined) {
            $control.next().data('dpscheduler').days = 7;
            $control.next().data('dpscheduler').update();
        }
        if (typeof $control.data('selectedstartdate') !== 'undefined') {
            const selectedstartdate = $control.data('selectedstartdate');
            FwScheduler.navigate($control, selectedstartdate);
        }
    };
    //---------------------------------------------------------------------------------
    show5WeekView($control) {
        const navcalendar = $control.data('navcalendar');
        navcalendar.selectMode = "week";
        navcalendar.update();
        $control.find('.fiveweekcontainer').show();
        $control.find('.calendarcontainer').hide();
        $control.find('.schedulercontainer').hide();
        $control.find('.monthcontainer').hide();
        $control.find('.yearcontainer').hide();
        if ($control.attr('data-shownav') === 'false') {
            $control.find('.nav5weekcontainer').hide();
        }
        $control.find('.changeview').attr('data-selected', 'false');
        $control.find('.btn5Week').attr('data-selected', 'true');
        if ($control.next().data('dpscheduler') !== undefined) {
            $control.next().data('dpscheduler').days = 35;
            $control.next().data('dpscheduler').update();
        }
        if (typeof $control.data('selectedstartdate') !== 'undefined') {
            const selectedstartdate = $control.data('selectedstartdate');
            FwScheduler.navigate($control, selectedstartdate);
        }
    };
    //---------------------------------------------------------------------------------
    showMonthView($control) {
        const navcalendar = $control.data('navcalendar');
        navcalendar.selectMode = "month";
        navcalendar.update();
        const dpcalendar = $control.data('dpcalendar');
        dpcalendar.viewType = "Month"
        dpcalendar.update();
        $control.find('.fiveweekcontainer').hide();
        $control.find('.calendarcontainer').hide();
        $control.find('.schedulercontainer').hide();
        $control.find('.monthcontainer').show();
        $control.find('.yearcontainer').hide();
        if ($control.attr('data-shownav') === 'false') {
            $control.find('.navmonthcontainer').hide();
        }
        $control.find('.changeview').attr('data-selected', 'false');
        $control.find('.btnMonth').attr('data-selected', 'true');
        if ($control.next().data('dpscheduler') !== undefined) {
            $control.next().data('dpscheduler').days = 31;
            $control.next().data('dpscheduler').update();
        }
        if (typeof $control.data('selectedstartdate') !== 'undefined') {
            const selectedstartdate = $control.data('selectedstartdate');
            FwScheduler.navigate($control, selectedstartdate);
        }
    };
    //---------------------------------------------------------------------------------
    showYearView($control) {
        const dpcalendar = $control.data('dpcalendar');
        dpcalendar.viewType = "Year"
        dpcalendar.update();
        $control.find('.fiveweekcontainer').hide();
        $control.find('.calendarcontainer').hide();
        $control.find('.schedulercontainer').hide();
        $control.find('.monthcontainer').hide();
        $control.find('.navyearcontainer').hide();
        $control.find('.yearcontainer').show();
        $control.find('.changeview').attr('data-selected', 'false');
        $control.find('.btnYear').attr('data-selected', 'true');
        if (typeof $control.data('selectedstartdate') !== 'undefined') {
            const selectedstartdate = $control.data('selectedstartdate');
            FwScheduler.navigate($control, selectedstartdate);
        }
        FwScheduler.refresh($control);
    };
    //---------------------------------------------------------------------------------
    showScheduleView($control) {
        const navcalendar = $control.data('navcalendar');
        navcalendar.selectMode = "month";
        navcalendar.update();
        $control.find('.calendarcontainer').hide();
        $control.find('.monthcontainer').hide();
        $control.find('.schedulercontainer').show();
        $control.find('.changeview').attr('data-selected', 'false');
        $control.find('.btnSchedule').attr('data-selected', 'true');
        if (typeof $control.data('selectedstartdate') !== 'undefined') {
            const selectedstartdate = $control.data('selectedstartdate');
            FwScheduler.navigate($control, selectedstartdate);
        }
    };
    //---------------------------------------------------------------------------------
    refresh($control) {
        FwScheduler.loadEvents($control);
    };
    //---------------------------------------------------------------------------------
    loadEvents($control) {
        let start, days;
        const dpcalendar = $control.data('dpcalendar');
        const dp5week = $control.data('dp5week');
        const dpmonth = $control.data('dpmonth');
        const dpyear = $control.data('dpyear');
        const dpscheduler = $control.data('dpscheduler');

        if (typeof $control.data('ongetevents') === 'function') {
            const ongetevents = $control.data('ongetevents');
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
                    start = dpyear.startDate.addDays(-dpyear.startDate.dayOfWeek()) // add the trailing days from the previous month that are visible
                    const endDate = dpyear.startDate.addMonths(11);
                    days = 330; //temp solution to a more accurate day count
                    break;
                case 'Schedule':
                    start = dpscheduler.startDate;
                    days = dpscheduler.startDate.daysInMonth();
                    break;
            }
            //start       = dpmonth.startDate.addDays(-dpmonth.startDate.dayOfWeek()) // add the trailing days from the previous month that are visible
            //days        = dpmonth.days + dpmonth.startDate.dayOfWeek() + (6 - dpmonth.startDate.addDays(dpmonth.days).dayOfWeek()) // add the first few days from the next month that are visible
            const request: any = {
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

            dpyear.resources = resources;
            dpyear.events.list = events;
            dpyear.update();

            dpscheduler.resources = resources;
            dpscheduler.events.list = events;
            dpscheduler.update();
        } else {
            // the user must have navigated away before this finished loading
        }
    };
    //---------------------------------------------------------------------------------
    //loadYearEventsCallback($control, resources, events) {
    //    var dpyear, start, end, request;
    //    dpyear = $control.data('dpyear');

    //    if ((typeof dpyear !== 'undefined')) {
    //        if ($control.find('div.changeview[data-selected="true"]').html() === 'Year') {
    //            FwScheduler.setDateCallout($control, dpyear.startDate);
    //        }
    //        dpyear.resources = [
    //            { name: "January", id: "A" },
    //            { name: "February", id: "B" },
    //            { name: "March", id: "C" },
    //            { name: "April", id: "D" },
    //            { name: "May", id: "E" },
    //            { name: "June", id: "F" },
    //            { name: "July", id: "G" },
    //            { name: "August", id: "H" },
    //            { name: "September", id: "I" },
    //            { name: "October", id: "J" },
    //            { name: "November", id: "K" },
    //            { name: "December", id: "L" }
    //        ];
    //        dpyear.events.list = events;
    //        dpyear.update();
    //    }
    //};
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
        const dpyear = $control.data('dpyear');
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
        dpyear.update();
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
        //return new DayPilot.Date(new Date().toISOString());
        let dateStr = moment().format('YYYY-MM-DD');
        let timeStr = moment().format('HH:mm:ss');
        return new DayPilot.Date(dateStr + 'T' + timeStr);   //#1305 11/15/2019 justin hoffman.  Without this, the calandar advances to the next day when viewing the calendar after 4pm on your machine.
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