﻿routes.push({ pattern: /^module\/inventoryitem$/, action: function(match: RegExpExecArray) { return InventoryItemController.getModuleScreen(); } });

class InventoryItem {
    Module: string = 'InventoryItem';
    apiurl: string = 'api/v1/rentalinventory';
    caption: string = 'Inventory Item';
    nav: string = 'module/inventoryitem';
    id: string = '803A2616-4DB6-4BAC-8845-ECAD34C369A8';
    ActiveView: string = 'ALL';
    AvailableFor: string = 'X';
    yearData: any = [];
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
    openFormInventory($form: any) {
        FwFormField.loadItems($form.find('.lamps'), [
            { value: '0', text: '0' },
            { value: '1', text: '1' },
            { value: '2', text: '2' },
            { value: '3', text: '3' },
            { value: '4', text: '4' }
        ], true);
    };
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $browse, self = this;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let self = this;
        let $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        $browse.data('ondatabind', function (request) {
            request.activeviewfields = self.ActiveViewFields;
        });
        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (var key in response) {
                    FwBrowse.addLegend($browse, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $browse)
        } catch (ex) {
            FwFunc.showError(ex);
        }
        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, uniqueids?) {
        var $form, controller, $calendar, inventoryId, $realScheduler;
        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //let warehouseId = JSON.parse(sessionStorage.warehouse).warehouseid;
        let self         = this;
        let warehouseId  = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
        let startOfMonth = moment().startOf('month').format('MM/DD/YYYY');
        let endOfMonth   = moment().endOf('month').format('MM/DD/YYYY');

        if (typeof uniqueids !== 'undefined') {
            inventoryId = uniqueids.InventoryId;
        }

        self.calculateYearly();
        $calendar = $form.find('.calendar');
        $calendar
            .data('ongetevents', function (calendarRequest) {
                startOfMonth = moment(calendarRequest.start.value).format('MM/DD/YYYY');
                endOfMonth = moment(calendarRequest.start.value).add(calendarRequest.days, 'd').format('MM/DD/YYYY');

                FwAppData.apiMethod(true, 'GET', `api/v1/inventoryavailability/getcalendarandscheduledata?&InventoryId=${inventoryId}&WarehouseId=${warehouseId}&FromDate=${startOfMonth}&ToDate=${endOfMonth}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwScheduler.loadYearEventsCallback($calendar, [{ id: '1', name: '' }], self.yearlyEvents);
                    var calendarevents = response.InventoryAvailabilityCalendarEvents;
                    var schedulerEvents = response.InventoryAvailabilityScheduleEvents;
                    for (var i = 0; i < calendarevents.length; i++) {
                        if (calendarevents[i].textColor !== 'rgb(0,0,0') {
                            calendarevents[i].html = `<div style="color:${calendarevents[i].textColor}">${calendarevents[i].text}</div>`
                        }
                    }
                    //for (var i = 0; i < schedulerEvents.length; i++) {
                    //    if (schedulerEvents[i].textColor !== 'rgb(0,0,0') {
                    //        schedulerEvents[i].html = `<div style="color:${schedulerEvents[i].textColor}">${schedulerEvents[i].text}</div>`
                    //    }
                    //}
                    //self.loadScheduler($form, response.InventoryAvailabilityScheduleEvents, response.InventoryAvailabilityScheduleResources);
                    FwScheduler.loadEventsCallback($calendar, [{ id: '1', name: '' }], calendarevents);
                }, function onError(response) {
                    FwFunc.showError(response);
                }, $calendar)

                //FwAppData.apiMethod(true, 'GET', `api/v1/inventoryavailabilitydate?InventoryId=${inventoryId}&WarehouseId=${warehouseId}&FromDate=${moment().startOf('year').format('MM/DD/YYYY')}&ToDate=${moment().endOf('year').format('MM/DD/YYYY')}`, null, FwServices.defaultTimeout, function onSuccess(response) {

                //}, function onError(response) {
                //    FwFunc.showError(response);
                //}, $calendar)
            })
            .data('ontimerangedoubleclicked', function (event) {
                try {
                    var date;
                    date = event.start.toString('MM/dd/yyyy');
                    FwScheduler.setSelectedDay($calendar, date);
                    //DriverController.openTicket($form);
                    $form.find('div[data-type="Browse"][data-name="Schedule"] .browseDate .fwformfield-value').val(date).change();
                    $form.find('div.tab.schedule').click();
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        $realScheduler = $form.find('.realscheduler');
        $realScheduler.data('ongetevents', function (request) {
            var start = moment(request.start.value).format('MM/DD/YYYY');
            var end = moment(request.start.value).add(31, 'days').format('MM/DD/YYYY')

            FwAppData.apiMethod(true, 'GET', `api/v1/inventoryavailability/getcalendarandscheduledata?&InventoryId=${inventoryId}&WarehouseId=${warehouseId}&FromDate=${start}&ToDate=${end}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                var schedulerEvents = response.InventoryAvailabilityScheduleEvents;
                for (var i = 0; i < schedulerEvents.length; i++) {
                    if (schedulerEvents[i].textColor !== 'rgb(0,0,0') {
                        schedulerEvents[i].html = `<div style="color:${schedulerEvents[i].textColor}">${schedulerEvents[i].text}</div>`
                    }
                }
                FwSchedulerDetailed.loadEventsCallback($realScheduler, response.InventoryAvailabilityScheduleResources, response.InventoryAvailabilityScheduleEvents);
            }, function onError(response) {
                FwFunc.showError(response);
            }, $calendar)
        });

        controller = $form.attr('data-controller');
        if (typeof window[controller]['openFormInventory'] === 'function') {
            window[controller]['openFormInventory']($form);
        }

        FwFormField.loadItems($form.find('div[data-datafield="Classification"]'), [
            {value:'I',     text:'Item'},
            {value:'A',     text:'Accessory'},
            {value:'C',     text:'Complete'},
            {value:'K',     text:'Kit'},
            {value:'N',     text:'Container'},
            {value:'M',     text:'Misc.'}
        ], false);

        FwFormField.loadItems($form.find('div[data-datafield="TrackedBy"]'), [
            {value:'BARCODE',    text:'Barcode'},
            {value:'SERIALNO',   text:'Serial No'},
            {value:'RFID',       text:'RFID'},
            {value:'QUANTITY',   text:'Quantity'}
        ], false);

        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"]', e => {
            if ($form.data('mode') !== 'NEW') {
                const tabpageid = jQuery(e.currentTarget).data('tabpageid');

                const $gridControls = $form.find(`#${tabpageid} [data-type="Grid"]`);
                if ($gridControls.length > 0) {
                    for (let i = 0; i < $gridControls.length; i++) {
                        const $gridcontrol = jQuery($gridControls[i]);
                        FwBrowse.search($gridcontrol);
                    }
                }

                const $browseControls = $form.find(`#${tabpageid} [data-type="Browse"]`);
                if ($browseControls.length > 0) {
                    for (let i = 0; i < $browseControls.length; i++) {
                        const $browseControl = jQuery($browseControls[i]);
                        FwBrowse.search($browseControl);
                    }
                }
            }
        });

        $form.on('change', 'div[data-datafield="Classification"] .fwformfield-value', function () {
            var $this      = jQuery(this);
            var $trackedby = $form.find('div[data-datafield="TrackedBy"]');

            $form.find('.completeskitstab').hide();
            $form.find('.containertab').hide();
            $form.find('.completetab').hide();
            $form.find('.kittab').hide();

            switch ($this.val()) {
                case 'I':
                    $form.find('.completeskitstab').show();
                    FwFormField.enable($trackedby);
                    break;
                case 'A':
                    $form.find('.completeskitstab').show();
                    FwFormField.enable($trackedby);
                    break;
                case 'C':
                    $form.find('.completetab').show();
                    FwFormField.disable($trackedby);
                    FwFormField.setValueByDataField($form, 'TrackedBy', '');
                    break;
                case 'K':
                    $form.find('.kittab').show();
                    $form.find('.completeskitstab').show();
                    FwFormField.disable($trackedby);
                    FwFormField.setValueByDataField($form, 'TrackedBy', '');
                    break;
                case 'N':
                    $form.find('.containertab').show();
                    FwFormField.disable($trackedby);
                    FwFormField.setValueByDataField($form, 'TrackedBy', '');
                    break;
                case 'M':
                    $form.find('.completeskitstab').show();
                    FwFormField.disable($trackedby);
                    FwFormField.setValueByDataField($form, 'TrackedBy', 'QUANTITY');
                    break;
            }
        });

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form = this.openForm('EDIT', uniqueids);
        $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(uniqueids.InventoryId);
        FwModule.loadForm(this.Module, $form);

        var $calendar = $form.find('.calendar');
        if ($calendar.length > 0) {
            setTimeout(function () {
                var schddate = FwScheduler.getTodaysDate();
                FwScheduler.navigate($calendar, schddate);
                FwScheduler.refresh($calendar);
            }, 1);
        }

        var $realScheduler = $form.find('.realscheduler');
        if ($realScheduler.length > 0) {
            setTimeout(function () {
                var schddate = FwSchedulerDetailed.getTodaysDate();
                FwSchedulerDetailed.navigate($realScheduler, schddate);
                FwSchedulerDetailed.refresh($realScheduler);
            }, 1);
        }

        let $submoduleRepairOrderBrowse = this.openRepairOrderBrowse($form);
        $form.find('.repairOrderSubModule').append($submoduleRepairOrderBrowse);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    openRepairOrderBrowse($form) {
        let inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
        let $browse     = RepairController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = RepairController.ActiveViewFields;
            request.uniqueids = {
                InventoryId: inventoryId
            };
        });
        jQuery($browse).find('.ddviewbtn-caption:contains("Show:")').siblings('.ddviewbtn-select').find('.ddviewbtn-dropdown-btn:contains("All")').click();
        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //----------------------------------------------------------------------------------------------
    loadAudit($form: any) {
        var uniqueid = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        $form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').on('change', function () {
            let $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
            } else {
                FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
            }
        });

        $form.find('div[data-datafield="InventoryTypeId"]').data('onchange', $tr => {
            if ($tr.find('.field[data-browsedatafield="Wardrobe"]').attr('data-originalvalue') === 'true') {
                $form.find('.wardrobetab').show();
            } else {
                $form.find('.wardrobetab').hide();
            }
        });

        $form.find('div[data-datafield="CategoryId"]').data('onchange', $tr => {
            FwFormField.disable($form.find('.subcategory'));
            if ($tr.find('.field[data-browsedatafield="SubCategoryCount"]').attr('data-originalvalue') > 0) {
                FwFormField.enable($form.find('.subcategory'));
            } else {
                FwFormField.setValueByDataField($form, 'SubCategoryId', '')
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    loadScheduler($form, events, resources) {

        var dp = new DayPilot.Scheduler($form.find('.realscheduler')[0]);

        // behavior and appearance
        dp.cellWidth = 40;
        dp.eventHeight = 30;
        dp.headerHeight = 25;

        // view
        dp.startDate = moment().format('YYYY-MM-DD');  // or just dp.startDate = "2013-03-25";
        dp.days = 31;
        dp.scale = "Day";
        dp.timeHeaders = [
            { groupBy: "Month" },
            { groupBy: "Day", format: "dddd" }
        ];
        dp.treeEnabled = true;
        dp.resources = resources;
        dp.events.list = events;
        //dp.resources = [
        //    {
        //        name: "200002-024", id: "A", expanded: true, children: [
        //            { name: "A", id: "A.1" },
        //            { name: "B", id: "A.2" }
        //        ]
        //    },
        //    { name: "L300044", id: "B" },
        //    { name: "L300230", id: "C" },
        //    { name: "L300962", id: "D" }
        //];
        //dp.events.list = [
        //    {
        //        start: "2018-11-30T00:00:00",
        //        end: "2018-12-10T00:00:00",
        //        id: "1",
        //        resource: "A",
        //        text: "200002-024 PENDING EXCHANGE (DED RENTALS)",
        //        orderNumber: "200002-024",
        //        orderStatus: "CONFIRMED",
        //        deal: "Testing"
        //    },
        //    {
        //        start: "2018-12-13T00:00:00",
        //        end: "2018-12-27T00:00:00",
        //        id: "6",
        //        resource: "A",
        //        text: "200002-024 PENDING EXCHANGE (DED RENTALS)",
        //        orderNumber: "200002-024",
        //        orderStatus: "CONFIRMED",
        //        deal: "Testing"
        //    },
        //    {
        //        start: "2018-11-30T00:00:00",
        //        end: "2018-12-27T00:00:00",
        //        id: "2",
        //        resource: "B",
        //        text: "L300044 SUB-RENTAL REPORT - SUBS (THE MOVIE HOUSE - 2014 RENTALS)",
        //        orderNumber: "L300044",
        //        orderStatus: "CONFIRMED",
        //        deal: "Testing"
        //    },
        //    {
        //        start: "2018-11-30T00:00:00",
        //        end: "2018-12-27T00:00:00",
        //        id: "2",
        //        resource: "C",
        //        text: "L300230 CROSS I-CODE EXCHANGE TEST (FELD RENTALS)",
        //        orderNumber: "L300230",
        //        orderStatus: "CONFIRMED",
        //        deal: "Testing"
        //    },
        //    {
        //        start: "2018-11-30T00:00:00",
        //        end: "2018-12-27T00:00:00",
        //        id: "2",
        //        resource: "D",
        //        text: "L300962 ORDER FOR STAGING A CONTAINER (ENDLESS3)",
        //        orderNumber: "L300962",
        //        orderStatus: "CONFIRMED",
        //        deal: "Testing"
        //    }
        //];
        dp.bubble = new DayPilot.Bubble({
            cssClassPrefix: "bubble_default",
            onLoad: function (args) {
                var ev = args.source;
                args.async = true;  // notify manually using .loaded()

                // simulating slow server-side load
                args.html = "<div style='font-weight:bold'>" + ev.text() + "</div><div>Order Number: " + ev.data.orderNumber + "</div><div>Order Status: " + ev.data.orderStatus + "</div><div>Deal: " + ev.data.deal + "</div><div>Start: " + ev.start().toString("MM/dd/yyyy HH:mm") + "</div><div>End: " + ev.end().toString("MM/dd/yyyy HH:mm") + "</div><div>Id: " + ev.id() + "</div>";
                args.loaded();

            }
        });
        dp.eventMoveHandling = "Disabled";
        dp.init();

        //dp.onBeforeEventRender = function (args) {
        //    args.data.html = '123';
        //    args.data.backColor = "#ffc0c0";
        //};
        //dp.update();
    }
    //----------------------------------------------------------------------------------------------
    loadGantt($form) {
        var dp = new DayPilot.Gantt($form.find('#gantt')[0]);
        dp.startDate = moment().format('YYYY-MM-DD');
        dp.days = 31;
        dp.init();
        dp.onBeforeRowHeaderRender = function (args) {
            args.row.backColor = 'coral';
        };
        dp.onBeforeCellRender = function (args) {
            args.cell.backColor = 'coral';
        };

        dp.tasks.list = [
            {
                "id": "1",
                "text": "200002-024",
                "start": "2018-11-30T00:00:00",
                "end": "2018-12-15T00:00:00",
                "complete": "200002-024 PENDING EXCHANGE (DED RENTALS)",
                "box": {
                    "bubbleHtml": "Task details (box): <br/>Task 0<br>Starting on November 21, 2018",
                    "html": "200002-024 PENDING EXCHANGE (DED RENTALS)",
                    "htmlRight": "200002-024"
                },
            },
            {
                "id": "2",
                "text": "L300044",
                "start": "2018-11-30T00:00:00",
                "end": "2018-12-30T00:00:00",
                "complete": "L300044 SUB-RENTAL REPORT - SUBS (THE MOVIE HOUSE - 2014 RENTALS)",
                "box": {
                    "bubbleHtml": "Task details (box): <br/>Task 0<br>Starting on November 21, 2018",
                    "html": "L300044 SUB-RENTAL REPORT - SUBS (THE MOVIE HOUSE - 2014 RENTALS)",
                    "htmlRight": "L300044"
                },
            },
            {
                "id": "5",
                "text": "L300044",
                "start": "2018-12-16T00:00:00",
                "end": "2018-12-30T00:00:00",
                "complete": "L300044 SUB-RENTAL REPORT - SUBS (THE MOVIE HOUSE - 2014 RENTALS)",
                "box": {
                    "bubbleHtml": "Task details (box): <br/>Task 0<br>Starting on November 21, 2018",
                    "html": "L300044 SUB-RENTAL REPORT - SUBS (THE MOVIE HOUSE - 2014 RENTALS)",
                    "htmlRight": "L300044"
                },
            },
            {
                "id": "3",
                "text": "L300230",
                "start": "2018-11-30T00:00:00",
                "end": "2018-12-30T00:00:00",
                "complete": "L300230 CROSS I-CODE EXCHANGE TEST (FELD RENTALS)",
                "title": "hello",
                "box": {
                    "bubbleHtml": "Task details (box): <br/>Task 0<br>Starting on November 21, 2018",
                    "html": "L300230 CROSS I-CODE EXCHANGE TEST (FELD RENTALS)",
                    "title": "L300230 CROSS I-CODE EXCHANGE TEST (FELD RENTALS)",
                    "htmlRight": "L300230"
                },
            },
            {
                "id": "4",
                "text": "L300962",
                "start": "2018-11-30T00:00:00",
                "end": "2018-12-02T00:00:00",
                "complete": "",
                "title": "hello",
                "box": {
                    "bubbleHtml": "Task details (box): <br/>Task 0<br>Starting on November 21, 2018",
                    "html": "L300962 ORDER FOR STAGING A CONTAINER (ENDLESS3)",
                    "title": "L300962 ORDER FOR STAGING A CONTAINER (ENDLESS3)",
                    "htmlRight": "L300962"
                },
                "row": {
                    "bubbleHtml": "Task details (row): <br/>Task 0<br>Starting on November 21, 2018"
                }
            },
        ]
        dp.update();
    }
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject: any) {
        const $all: JQuery = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $item: JQuery = FwMenu.generateDropDownViewBtn('Item', true, "I");
        const $accessory: JQuery = FwMenu.generateDropDownViewBtn('Accessory', false, "A");
        const $complete: JQuery = FwMenu.generateDropDownViewBtn('Complete', false, "C");
        const $kit: JQuery = FwMenu.generateDropDownViewBtn('Kit', false, "K");
        const $set: JQuery = FwMenu.generateDropDownViewBtn('Set', false, "S");
        const $misc: JQuery = FwMenu.generateDropDownViewBtn('Misc', false, "M");
        const $container: JQuery = FwMenu.generateDropDownViewBtn('Container', false, "N");

        FwMenu.addVerticleSeparator($menuObject);

        var viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $item, $accessory, $complete, $kit, $set, $misc);
        if (this.AvailableFor === "R") {
            viewSubitems.push($container);
        }
        FwMenu.addViewBtn($menuObject, 'View', viewSubitems, true, "Classification");

        return $menuObject;
    };
    //----------------------------------------------------------------------------------------------
    afterLoadSetClassification($form: any) {
        var $classification = $form.find('div[data-datafield="Classification"]');
        var $trackedby      = $form.find('div[data-datafield="TrackedBy"]');

        var $completeskitstab = $form.find('.completeskitstab');
        var $containertab     = $form.find('.containertab');
        var $completetab      = $form.find('.completetab');
        var $kittab           = $form.find('.kittab');
        var $settingstab      = $form.find('.settingstab');

        var $rentalinventorywarehousegrid = $form.find('[data-grid="RentalInventoryWarehouseGrid"]');
        var $containerwarehousegrid       = $form.find('[data-grid="ContainerWarehouseGrid"]');

        switch (FwFormField.getValue2($classification)) {
            case 'I':
            case 'A':
                $classification.find('option[value="N"]').hide();
                $classification.find('option[value="C"]').hide();
                $classification.find('option[value="K"]').hide();
                $classification.find('option[value="M"]').hide();
                FwBrowse.search($rentalinventorywarehousegrid);
                break;
            case 'N':
                $completeskitstab.hide();
                $containertab.show();
                $settingstab.show();
                $rentalinventorywarehousegrid.hide();
                $containerwarehousegrid.show();
                FwBrowse.search($containerwarehousegrid);
                FwFormField.disable($classification);
                FwFormField.disable($trackedby);
                FwFormField.setValue2($trackedby, '');
                break;
            case 'C':
                $completeskitstab.hide();
                $completetab.show();
                FwBrowse.search($rentalinventorywarehousegrid);
                FwFormField.disable($classification);
                FwFormField.disable($trackedby);
                FwFormField.setValue2($trackedby, '');
                break;
            case 'K':
                $kittab.show();
                FwBrowse.search($rentalinventorywarehousegrid);
                FwFormField.disable($classification);
                FwFormField.disable($trackedby);
                FwFormField.setValue2($trackedby, '');
                break;
            case 'M':
                FwBrowse.search($rentalinventorywarehousegrid);
                FwFormField.disable($classification);
                FwFormField.disable($trackedby);
                break;
        };
    }
    //----------------------------------------------------------------------------------------------
    calculateYearly() {
        for (var jan = 0; jan <= 30; jan++) {
            this.yearlyEvents.push({
                start: moment('2018-04-02T00:00:00').add(jan, 'days').format('YYYY-MM-DD'),
                end: moment('2018-04-03T00:00:00').add(jan, 'days').format('YYYY-MM-DD'),
                realStart: "January " + (jan + 1),
                realEnd: "January " + (jan + 2),
                id: "1",
                resource: "A",
                text: "Available",
                backColor: "lime",
                orderNumber: "200002-024",
                orderStatus: "CONFIRMED",
                deal: "Testing"
            })
        }
        for (var feb = 0; feb < 28; feb++) {
            this.yearlyEvents.push({
                start: moment('2018-04-05T00:00:00').add(feb, 'days').format('YYYY-MM-DD'),
                end: moment('2018-04-06T00:00:00').add(feb, 'days').format('YYYY-MM-DD'),
                realStart: "February " + (feb + 1),
                realEnd: "February " + (feb + 2),
                id: "1",
                resource: "B",
                text: "Available",
                backColor: "lime",
                orderNumber: "200002-024",
                orderStatus: "CONFIRMED",
                deal: "Testing"
            })
        }
        for (var mar = 0; mar < 31; mar++) {
            this.yearlyEvents.push({
                start: moment('2018-04-05T00:00:00').add(mar, 'days').format('YYYY-MM-DD'),
                end: moment('2018-04-06T00:00:00').add(mar, 'days').format('YYYY-MM-DD'),
                realStart: "March " + (mar + 1),
                realEnd: "March " + (mar + 2),
                id: "1",
                resource: "C",
                text: "Available",
                backColor: "lime",
                orderNumber: "200002-024",
                orderStatus: "CONFIRMED",
                deal: "Testing"
            })
        }
        for (var May = 0; May < 31; May++) {
            this.yearlyEvents.push({
                start: moment('2018-04-03T00:00:00').add(May, 'days').format('YYYY-MM-DD'),
                end: moment('2018-04-04T00:00:00').add(May, 'days').format('YYYY-MM-DD'),
                realStart: "May " + (May + 1),
                realEnd: "May " + (May + 2),
                id: "1",
                resource: "E",
                text: "Available",
                backColor: "lime",
                orderNumber: "200002-024",
                orderStatus: "CONFIRMED",
                deal: "Testing"
            })
        }
        for (var June = 0; June < 30; June++) {
            this.yearlyEvents.push({
                start: moment('2018-04-06T00:00:00').add(June, 'days').format('YYYY-MM-DD'),
                end: moment('2018-04-07T00:00:00').add(June, 'days').format('YYYY-MM-DD'),
                realStart: "June " + (June + 1),
                realEnd: "June " + (June + 2),
                id: "1",
                resource: "F",
                text: "Available",
                backColor: "lime",
                orderNumber: "200002-024",
                orderStatus: "CONFIRMED",
                deal: "Testing"
            })
        }
        for (var July = 0; July < 31; July++) {
            this.yearlyEvents.push({
                start: moment('2018-04-01T00:00:00').add(July, 'days').format('YYYY-MM-DD'),
                end: moment('2018-04-02T00:00:00').add(July, 'days').format('YYYY-MM-DD'),
                realStart: "July " + (July + 1),
                realEnd: "July " + (July + 2),
                id: "1",
                resource: "G",
                text: "Available",
                backColor: "lime",
                orderNumber: "200002-024",
                orderStatus: "CONFIRMED",
                deal: "Testing"
            })
        }
        for (var August = 0; August < 30; August++) {
            this.yearlyEvents.push({
                start: moment('2018-04-01T00:00:00').add(August, 'days').format('YYYY-MM-DD'),
                end: moment('2018-04-02T00:00:00').add(August, 'days').format('YYYY-MM-DD'),
                realStart: "August " + (August + 1),
                realEnd: "August " + (August + 2),
                id: "1",
                resource: "H",
                text: "Available",
                backColor: "lime",
                orderNumber: "200002-024",
                orderStatus: "CONFIRMED",
                deal: "Testing"
            })
        }
        for (var September = 0; September < 31; September++) {
            this.yearlyEvents.push({
                start: moment('2018-04-07T00:00:00').add(September, 'days').format('YYYY-MM-DD'),
                end: moment('2018-04-08T00:00:00').add(September, 'days').format('YYYY-MM-DD'),
                realStart: "September " + (September + 1),
                realEnd: "September " + (September + 2),
                id: "1",
                resource: "I",
                text: "Available",
                backColor: "lime",
                orderNumber: "200002-024",
                orderStatus: "CONFIRMED",
                deal: "Testing"
            })
        }
        for (var October = 0; October < 31; October++) {
            this.yearlyEvents.push({
                start: moment('2018-04-02T00:00:00').add(October, 'days').format('YYYY-MM-DD'),
                end: moment('2018-04-03T00:00:00').add(October, 'days').format('YYYY-MM-DD'),
                realStart: "October " + (October + 1),
                realEnd: "October " + (October + 2),
                id: "1",
                resource: "J",
                text: "Available",
                backColor: "lime",
                orderNumber: "200002-024",
                orderStatus: "CONFIRMED",
                deal: "Testing"
            })
        }
        for (var November = 0; November < 30; November++) {
            this.yearlyEvents.push({
                start: moment('2018-04-05T00:00:00').add(November, 'days').format('YYYY-MM-DD'),
                end: moment('2018-04-06T00:00:00').add(November, 'days').format('YYYY-MM-DD'),
                realStart: "November " + (November + 1),
                realEnd: "November " + (November + 2),
                id: "1",
                resource: "K",
                text: "Available",
                backColor: "lime",
                orderNumber: "200002-024",
                orderStatus: "CONFIRMED",
                deal: "Testing"
            })
        }
        for (var December = 0; December < 31; December++) {
            this.yearlyEvents.push({
                start: moment('2018-04-07T00:00:00').add(December, 'days').format('YYYY-MM-DD'),
                end: moment('2018-04-08T00:00:00').add(December, 'days').format('YYYY-MM-DD'),
                realStart: "December " + (December + 1),
                realEnd: "December " + (December + 2),
                id: "1",
                resource: "L",
                text: "Available",
                backColor: "lime",
                orderNumber: "200002-024",
                orderStatus: "CONFIRMED",
                deal: "Testing"
            })
        }
    }
    //----------------------------------------------------------------------------------------------
    yearlyEvents: any = [
        {
            start: "2018-04-01T00:00:00",
            end: "2018-04-02T00:00:00",

            id: "1",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-02T00:00:00",
            end: "2018-04-03T00:00:00",

            id: "2",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-03T00:00:00",
            end: "2018-04-04T00:00:00",

            id: "3",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-04T00:00:00",
            end: "2018-04-05T00:00:00",

            id: "3",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-05T00:00:00",
            end: "2018-04-06T00:00:00",

            id: "4",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-06T00:00:00",
            end: "2018-04-07T00:00:00",

            id: "1",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-07T00:00:00",
            end: "2018-04-08T00:00:00",

            id: "2",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-08T00:00:00",
            end: "2018-04-09T00:00:00",

            id: "1",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-09T00:00:00",
            end: "2018-04-10T00:00:00",

            id: "2",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-10T00:00:00",
            end: "2018-04-11T00:00:00",

            id: "1",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-11T00:00:00",
            end: "2018-04-12T00:00:00",

            id: "1",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-12T00:00:00",
            end: "2018-04-13T00:00:00",

            id: "2",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-13T00:00:00",
            end: "2018-04-14T00:00:00",

            id: "3",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-14T00:00:00",
            end: "2018-04-15T00:00:00",

            id: "3",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-15T00:00:00",
            end: "2018-04-16T00:00:00",

            id: "4",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-16T00:00:00",
            end: "2018-04-17T00:00:00",

            id: "1",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-17T00:00:00",
            end: "2018-04-18T00:00:00",

            id: "2",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-18T00:00:00",
            end: "2018-04-19T00:00:00",

            id: "1",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-19T00:00:00",
            end: "2018-04-20T00:00:00",

            id: "2",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-20T00:00:00",
            end: "2018-04-21T00:00:00",

            id: "1",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-21T00:00:00",
            end: "2018-04-22T00:00:00",

            id: "1",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-22T00:00:00",
            end: "2018-04-23T00:00:00",

            id: "2",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-23T00:00:00",
            end: "2018-04-24T00:00:00",

            id: "3",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-24T00:00:00",
            end: "2018-04-25T00:00:00",

            id: "3",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-25T00:00:00",
            end: "2018-04-26T00:00:00",

            id: "4",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-26T00:00:00",
            end: "2018-04-27T00:00:00",

            id: "1",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-27T00:00:00",
            end: "2018-04-28T00:00:00",

            id: "2",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-28T00:00:00",
            end: "2018-04-29T00:00:00",

            id: "1",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
        {
            start: "2018-04-29T00:00:00",
            end: "2018-04-30T00:00:00",

            id: "2",
            resource: "D",
            text: "Available",
            backColor: "lime",
            orderNumber: "200002-024",
            orderStatus: "CONFIRMED",
            deal: "Testing"
        },
    ];
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        // RentalInventoryWarehouseGrid
        let $rentalInventoryWarehouseGrid = $form.find('div[data-grid="RentalInventoryWarehouseGrid"]');
        let $rentalInventoryWarehouseGridControl = jQuery(jQuery('#tmpl-grids-RentalInventoryWarehouseGridBrowse').html());
        $rentalInventoryWarehouseGrid.empty().append($rentalInventoryWarehouseGridControl);
        $rentalInventoryWarehouseGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $rentalInventoryWarehouseGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($rentalInventoryWarehouseGridControl);
        FwBrowse.renderRuntimeHtml($rentalInventoryWarehouseGridControl);

        // ContainerWarehouseGrid
        let containerWarehouseGrid: any;
        let containerWarehouseGridControl: any;
        containerWarehouseGrid = $form.find('div[data-grid="ContainerWarehouseGrid"]');
        containerWarehouseGridControl = jQuery(jQuery('#tmpl-grids-ContainerWarehouseGridBrowse').html());
        containerWarehouseGrid.empty().append(containerWarehouseGridControl);
        containerWarehouseGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        containerWarehouseGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init(containerWarehouseGridControl);
        FwBrowse.renderRuntimeHtml(containerWarehouseGridControl);

        // InventoryAvailabilityGrid
        let $inventoryAvailabilityGrid = $form.find('div[data-grid="InventoryAvailabilityGrid"]');
        let $inventoryAvailabilityGridControl = jQuery(jQuery('#tmpl-grids-InventoryAvailabilityGridBrowse').html());
        $inventoryAvailabilityGrid.empty().append($inventoryAvailabilityGridControl);
        $inventoryAvailabilityGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryAvailabilityGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryAvailabilityGridControl);
        FwBrowse.renderRuntimeHtml($inventoryAvailabilityGridControl);

        // InventoryCompleteKitGrid
        let $inventoryCompleteKitGrid = $form.find('div[data-grid="InventoryCompleteKitGrid"]');
        let $inventoryCompleteKitGridControl = jQuery(jQuery('#tmpl-grids-InventoryCompleteKitGridBrowse').html());
        $inventoryCompleteKitGrid.empty().append($inventoryCompleteKitGridControl);
        $inventoryCompleteKitGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryCompleteKitGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryCompleteKitGridControl);
        FwBrowse.renderRuntimeHtml($inventoryCompleteKitGridControl);

        // InventorySubstituteGrid
        let $inventorySubstituteGrid = $form.find('div[data-grid="InventorySubstituteGrid"]');
        let $inventorySubstituteGridControl = jQuery(jQuery('#tmpl-grids-InventorySubstituteGridBrowse').html());
        $inventorySubstituteGrid.empty().append($inventorySubstituteGridControl);
        $inventorySubstituteGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(),
                WarehouseId: warehouse.warehouseid
            };
        });
        $inventorySubstituteGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventorySubstituteGridControl);
        FwBrowse.renderRuntimeHtml($inventorySubstituteGridControl);

        // InventoryCompatibilityGrid
        let $inventoryCompatibilityGrid = $form.find('div[data-grid="InventoryCompatibilityGrid"]');
        let $inventoryCompatibilityGridControl = jQuery(jQuery('#tmpl-grids-InventoryCompatibilityGridBrowse').html());
        $inventoryCompatibilityGrid.empty().append($inventoryCompatibilityGridControl);
        $inventoryCompatibilityGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryCompatibilityGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryCompatibilityGridControl);
        FwBrowse.renderRuntimeHtml($inventoryCompatibilityGridControl);

        // InventoryQcGrid
        let $inventoryQcGrid = $form.find('div[data-grid="InventoryQcGrid"]');
        let $inventoryQcGridControl = jQuery(jQuery('#tmpl-grids-InventoryQcGridBrowse').html());
        $inventoryQcGrid.empty().append($inventoryQcGridControl);
        $inventoryQcGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryQcGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryQcGridControl);
        FwBrowse.renderRuntimeHtml($inventoryQcGridControl);

        // InventoryAttributeValueGrid
        let $inventoryAttributeValueGrid = $form.find('div[data-grid="InventoryAttributeValueGrid"]');
        let $inventoryAttributeValueGridControl = jQuery(jQuery('#tmpl-grids-InventoryAttributeValueGridBrowse').html());
        $inventoryAttributeValueGrid.empty().append($inventoryAttributeValueGridControl);
        $inventoryAttributeValueGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryAttributeValueGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryAttributeValueGridControl);
        FwBrowse.renderRuntimeHtml($inventoryAttributeValueGridControl);

        // InventoryVendorGrid
        let $inventoryVendorGrid = $form.find('div[data-grid="InventoryVendorGrid"]');
        let $inventoryVendorGridControl = jQuery(jQuery('#tmpl-grids-InventoryVendorGridBrowse').html());
        $inventoryVendorGrid.empty().append($inventoryVendorGridControl);
        $inventoryVendorGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryVendorGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryVendorGridControl);
        FwBrowse.renderRuntimeHtml($inventoryVendorGridControl);

        // InventoryWarehouseStagingGrid
        let $inventoryWarehouseStagingGrid = $form.find('div[data-grid="InventoryWarehouseStagingGrid"]');
        let $inventoryWarehouseStagingGridControl = jQuery(jQuery('#tmpl-grids-InventoryWarehouseStagingGridBrowse').html());
        $inventoryWarehouseStagingGrid.empty().append($inventoryWarehouseStagingGridControl);
        $inventoryWarehouseStagingGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryWarehouseStagingGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryWarehouseStagingGridControl);
        FwBrowse.renderRuntimeHtml($inventoryWarehouseStagingGridControl);
    };

    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        this.afterLoadSetClassification($form);
        this.addAssetTab($form);

        if ($form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'))
        } else {
            FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategoryId"]'))
        }

        if ($form.find('[data-datafield="InventoryTypeIsWardrobe"] .fwformfield-value').prop('checked') === true) {
            $form.find('.wardrobetab').show();
        };

        if ($form.find('[data-datafield="SubCategoryCount"] .fwformfield-value').val() > 0) {
            FwFormField.enable($form.find('.subcategory'));
        } else {
            FwFormField.disable($form.find('.subcategory'));
        }
    };
    //----------------------------------------------------------------------------------------------
    addAssetTab($form: any): void {
        var classificationValue = FwFormField.getValueByDataField($form, 'Classification');
        var trackedByValue      = FwFormField.getValueByDataField($form, 'TrackedBy');

        if ((classificationValue === 'I' || classificationValue === 'A') && (trackedByValue !== 'QUANTITY')) {
            $form.find('.tab.asset').show();
            var $submoduleAssetBrowse = this.openAssetBrowse($form);
            $form.find('.tabpage.asset').html($submoduleAssetBrowse);
            $submoduleAssetBrowse.find('div.btn[data-type="NewMenuBarButton"]')
                .off('click')
                .on('click', function () {
                    var $browse       = jQuery(this).closest('.fwbrowse');
                    var controller    = $browse.attr('data-controller');
                    var assetFormData = {
                        InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                    };
                    if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
                    if (typeof window[controller]['openForm'] !== 'function') throw 'Missing javascript function: ' + controller + '.openForm';
                    var $assetForm = window[controller]['openForm']('NEW', assetFormData);
                    FwModule.openSubModuleTab($browse, $assetForm);
                })
            ;
        }
    };
    //----------------------------------------------------------------------------------------------
    openAssetBrowse($form: any) {
        let $browse, inventoryId;
        $browse = AssetController.openBrowse();
        inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');

        $browse.data('ondatabind', request => {
            request.activeviewfields = AssetController.ActiveViewFields;
            request.uniqueids = {
                InventoryId: inventoryId
            }
        });
        return $browse;
    };

    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $grid, request) {
        const validationName = request.module;
        const InventoryTypeValue = jQuery($grid.find('[data-validationname="InventoryTypeValidation"] input')).val();
        const CategoryTypeId = jQuery($grid.find('[data-validationname="RentalCategoryValidation"] input')).val();

        switch (validationName) {
            case 'InventoryTypeValidation':
                request.uniqueids = {
                    Rental: true
                };
                break;
            case 'RentalCategoryValidation':
                request.uniqueids = {
                    InventoryTypeId: InventoryTypeValue
                };
                break;
            case 'SubCategoryValidation':
                request.uniqueids = {
                    TypeId: InventoryTypeValue,
                    CategoryId: CategoryTypeId
                };
                break;
        };
    };

    //--------------------------------------------------------------------------------------------
}

var InventoryItemController = new InventoryItem();