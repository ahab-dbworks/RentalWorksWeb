﻿routes.push({ pattern: /^module\/expendableitem$/, action: function(match: RegExpExecArray) { return ExpendableItemController.getModuleScreen(); } });

class ExpendableItem {
    Module: string = 'ExpendableItem';
    apiurl: string = 'api/v1/salesinventory';
    caption: string = Constants.Modules.Home.children.ExpendableItem.caption;
	nav: string = Constants.Modules.Home.children.ExpendableItem.nav;
	id: string = Constants.Modules.Home.children.ExpendableItem.id;
    ActiveView: string = 'ALL';
    AvailableFor: string = 'X';
    yearData: any = [];
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
    openFormExpendableItem($form: any) {
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
        let self = this;
        let warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;   //justin 11/11/2018 fixing build error
        let startOfMonth = moment().startOf('month').format('MM/DD/YYYY');
        let endOfMonth = moment().endOf('month').format('MM/DD/YYYY');

        if (typeof uniqueids !== 'undefined') {
            inventoryId = uniqueids.InventoryId;
        }

        self.calculateYearly();
        $calendar = $form.find('.calendar');
        $calendar
            .data('ongetevents', function (calendarRequest) {
                startOfMonth = moment(calendarRequest.start.value).format('MM/DD/YYYY');
                endOfMonth = moment(calendarRequest.start.value).add(calendarRequest.days, 'd').format('MM/DD/YYYY');

                FwAppData.apiMethod(true, 'GET', `api/v1/inventoryavailability/calendarandscheduledata?&InventoryId=${inventoryId}&WarehouseId=${warehouseId}&FromDate=${startOfMonth}&ToDate=${endOfMonth}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    //FwScheduler.loadYearEventsCallback($calendar, [{ id: '1', name: '' }], self.yearlyEvents);
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
        $realScheduler
            .data('ongetevents', function (request) {
                var start = moment(request.start.value).format('MM/DD/YYYY');
                var end = moment(request.start.value).add(31, 'days').format('MM/DD/YYYY')

                FwAppData.apiMethod(true, 'GET', `api/v1/inventoryavailability/calendarandscheduledata?&InventoryId=${inventoryId}&WarehouseId=${warehouseId}&FromDate=${start}&ToDate=${end}`, null, FwServices.defaultTimeout, function onSuccess(response) {
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
            })
        if (mode === 'NEW') {
            this.setupNewMode($form);
        }

        controller = $form.attr('data-controller');
        if (typeof window[controller]['openFormExpendableItem'] === 'function') {
            window[controller]['openFormExpendableItem']($form);
        }

        FwFormField.loadItems($form.find('div[data-datafield="Classification"]'), [
            {value:'I',     text:'Item'},
            {value:'A',     text:'Accessory'},
            {value:'C',     text:'Complete'},
            {value:'K',     text:'Kit'},
            {value:'N',     text:'Container'},
            {value:'M',     text:'Misc.'}
        ], true);

        FwFormField.loadItems($form.find('div[data-datafield="TrackedBy"]'), [
            {value:'BARCODE',    text:'Barcode'},
            {value:'SERIALNO',   text:'Serial No'},
            {value:'RFID',       text:'RFID'},
            {value:'QUANTITY',   text:'Quantity'}
        ], true);

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form, $calendar, schddate, $realScheduler;

        $form = this.openForm('EDIT', uniqueids);
        $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(uniqueids.InventoryId);
        FwModule.loadForm(this.Module, $form);

        $calendar = $form.find('.calendar');
        if ($calendar.length > 0) {
            setTimeout(function () {
                schddate = FwScheduler.getTodaysDate();
                FwScheduler.navigate($calendar, schddate);
                FwScheduler.refresh($calendar);
            }, 1);
        }

        $realScheduler = $form.find('.realscheduler');
        if ($realScheduler.length > 0) {
            setTimeout(function () {
                schddate = FwSchedulerDetailed.getTodaysDate();
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
        let $browse;
        $browse = RepairController.openBrowse();
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
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        let classificationValue, trackedByValue;

        $form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').on('change', function () {
            let $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
            }
            else {
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
        // Hides or shows Asset tab for particular settings on the form
        $form.find('.class-tracked-radio input').on('change', () => {
            classificationValue = FwFormField.getValueByDataField($form, 'Classification');
            trackedByValue = FwFormField.getValueByDataField($form, 'TrackedBy');
            if (classificationValue === 'I' || classificationValue === 'A') {
                if (trackedByValue !== 'QUANTITY') {
                    $form.find('.asset-submodule').show();
                } else {
                    $form.find('.asset-submodule').hide();
                }
            } else {
                $form.find('.asset-submodule').hide();
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
    addBrowseMenuItems(options: IAddBrowseMenuOptions) {
        const $all: JQuery = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $item: JQuery = FwMenu.generateDropDownViewBtn('Item', true, "I");
        const $accessory: JQuery = FwMenu.generateDropDownViewBtn('Accessory', false, "A");
        const $complete: JQuery = FwMenu.generateDropDownViewBtn('Complete', false, "C");
        const $kit: JQuery = FwMenu.generateDropDownViewBtn('Kit', false, "K");
        const $set: JQuery = FwMenu.generateDropDownViewBtn('Set', false, "S");
        const $misc: JQuery = FwMenu.generateDropDownViewBtn('Misc', false, "M");
        const $container: JQuery = FwMenu.generateDropDownViewBtn('Container', false, "N");

        FwMenu.addVerticleSeparator(options.$menu);

        var viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $item, $accessory, $complete, $kit, $set, $misc);
        if (this.AvailableFor === "R") {
            viewSubitems.push($container);
        }
        FwMenu.addViewBtn(options.$menu, 'View', viewSubitems, true, "Classification");

        return options;
    };
    //----------------------------------------------------------------------------------------------
    setupNewMode($form: any) {
        FwFormField.enable($form.find('[data-datafield="Classification"]'));

        $form.find('div[data-datafield="Classification"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);

            $form.find('.completeskitstab').show();
            $form.find('.containertab').hide();
            $form.find('.completetab').hide();
            $form.find('.kittab').hide();

            if ($this.prop('checked') === true && $this.val() === 'I') {
                FwFormField.enable($form.find('div[data-datafield="TrackedBy"]'));
                $form.find('.tracked-by-column').show();
            }
            if ($this.prop('checked') === true && $this.val() === 'A') {
                FwFormField.enable($form.find('div[data-datafield="TrackedBy"]'));
                $form.find('.tracked-by-column').show();
            }
            if ($this.prop('checked') === true && $this.val() === 'C') {
                $form.find('.completetab').show();
                $form.find('.completeskitstab').hide();
                FwFormField.enable($form.find('div[data-datafield="TrackedBy"]'));
                $form.find('.tracked-by-column').hide();
                $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
            }
            if ($this.prop('checked') === true && $this.val() === 'K') {
                $form.find('.kittab').show();
                FwFormField.enable($form.find('div[data-datafield="TrackedBy"]'));
                $form.find('.tracked-by-column').hide();
                $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
            }
            if ($this.prop('checked') === true && $this.val() === 'N') {
                $form.find('.containertab').show();
                $form.find('.completeskitstab').hide();
                FwFormField.enable($form.find('div[data-datafield="TrackedBy"]'));
                $form.find('.tracked-by-column').hide();
                $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
            }
            if ($this.prop('checked') === true && $this.val() === 'S') {
                FwFormField.enable($form.find('div[data-datafield="TrackedBy"]'));
                $form.find('.tracked-by-column').hide();
                $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
            }
            if ($this.prop('checked') === true && $this.val() === 'M') {
                FwFormField.setValueByDataField($form, 'TrackedBy', 'QUANTITY');
                FwFormField.disable($form.find('div[data-datafield="TrackedBy"]'));
                $form.find('.tracked-by-column').show();
            }
        })
    }
    //----------------------------------------------------------------------------------------------
    afterLoadSetClassification($form: any) {
        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'I' || FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'A') {
            FwFormField.enable($form.find('[data-datafield="Classification"]'));
            $form.find('.completeradio').hide();
            $form.find('.kitradio').hide();
            $form.find('.containerradio').hide();
            $form.find('.miscradio').hide();
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'N') {
            $form.find('.containertab').show();
            $form.find('.completeskitstab').hide();
            $form.find('.kitradio').hide();
            $form.find('.completeradio').hide();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.miscradio').hide();
            $form.find('.tracked-by-column').hide();
            $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'C') {
            $form.find('.completetab').show();
            $form.find('.completeskitstab').hide();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.containerradio').hide();
            $form.find('.miscradio').hide();
            $form.find('.tracked-by-column').hide();
            $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'K') {
            $form.find('.kittab').show();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.containerradio').hide();
            $form.find('.miscradio').hide();
            $form.find('.tracked-by-column').hide();
            $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'M') {
            $form.find('.completeradio').hide();
            $form.find('.kitradio').hide();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.containerradio').hide();
            FwFormField.setValueByDataField($form, 'TrackedBy', 'QUANTITY');
            FwFormField.disable($form.find('div[data-datafield="TrackedBy"]'));
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'S') {
            $form.find('.completeradio').hide();
            $form.find('.kitradio').hide();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.containerradio').hide();
            $form.find('.tracked-by-column').hide();
            $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
        }
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
        FwBrowse.renderGrid({
            nameGrid:         'RentalInventoryWarehouseGrid',
            gridSecurityId:   'g8sCuKjUVrW1',
            moduleSecurityId: this.id,
            $form:            $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            } 
        });

        // ContainerWarehouseGrid
        FwBrowse.renderGrid({
            nameGrid:         'ContainerWarehouseGrid',
            gridSecurityId:   '4gsBzepUJdWm',
            moduleSecurityId: this.id,
            $form:            $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            } 
        });

        // InventoryAvailabilityGrid
        FwBrowse.renderGrid({
            nameGrid:         'InventoryAvailabilityGrid',
            gridSecurityId:   'g8sCuKjUVrW1',
            moduleSecurityId: this.id,
            $form:            $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            } 
        });

        // InventoryConsignmentGrid
        FwBrowse.renderGrid({
            nameGrid:         'InventoryConsignmentGrid',
            gridSecurityId:   'JKfdyoLXFqu3',
            moduleSecurityId: this.id,
            $form:            $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            } 
        });

        // InventoryCompleteKitGrid
        FwBrowse.renderGrid({
            nameGrid:         'InventoryCompleteKitGrid',
            gridSecurityId:   'gflkb5sQf7it',
            moduleSecurityId: this.id,
            $form:            $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            } 
        });

        // InventorySubstituteGrid
        FwBrowse.renderGrid({
            nameGrid:         'InventorySubstituteGrid',
            gridSecurityId:   '5sN9zKtGzNTq',
            moduleSecurityId: this.id,
            $form:            $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            } 
        });

        // InventoryCompatibilityGrid
        FwBrowse.renderGrid({
            nameGrid:         'InventoryCompatibilityGrid',
            gridSecurityId:   'mlAKf5gRPNNI',
            moduleSecurityId: this.id,
            $form:            $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            } 
        });

        // InventoryQcGrid
        FwBrowse.renderGrid({
            nameGrid:         'InventoryQcGrid',
            gridSecurityId:   'g8sCuKjUVrW1',
            moduleSecurityId: this.id,
            $form:            $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            } 
        });

        // InventoryAttributeValueGrid
        FwBrowse.renderGrid({
            nameGrid:         'InventoryAttributeValueGrid',
            gridSecurityId:   'CntxgVXDQtQ7',
            moduleSecurityId: this.id,
            $form:            $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            } 
        });

        // InventoryVendorGrid
        FwBrowse.renderGrid({
            nameGrid:         'InventoryVendorGrid',
            gridSecurityId:   's9vdtBqItIEi',
            moduleSecurityId: this.id,
            $form:            $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            } 
        });

        // InventoryWarehouseStagingGrid
        FwBrowse.renderGrid({
            nameGrid:         'InventoryWarehouseStagingGrid',
            gridSecurityId:   'g8sCuKjUVrW1',
            moduleSecurityId: this.id,
            $form:            $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            } 
        });
    };

    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        this.addAssetTab($form);

        let classificationType = FwFormField.getValueByDataField($form, 'Classification');
        //Change the grid on primary to tab when classification is container
        if (classificationType == 'N') {
            $form.find('[data-grid="RentalInventoryWarehouseGrid"]').hide();
            $form.find('[data-grid="ContainerWarehouseGrid"]').show();
            let $containerWarehouseGrid = $form.find('[data-name="ContainerWarehouseGrid"]');
            FwBrowse.search($containerWarehouseGrid);

            //Show settings tab
            $form.find('.settingstab').show();
        } else {
            let $rentalInventoryWarehouseGrid = $form.find('[data-name="RentalInventoryWarehouseGrid"]');
            FwBrowse.search($rentalInventoryWarehouseGrid);
        }

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
        let $submoduleAssetBrowse, classificationValue, trackedByValue;
        classificationValue = FwFormField.getValueByDataField($form, 'Classification');
        trackedByValue = FwFormField.getValueByDataField($form, 'TrackedBy');

        if (classificationValue === 'I' || classificationValue === 'A') {
            if (trackedByValue !== 'QUANTITY') {
                $form.find('.tab.asset').show();
                $submoduleAssetBrowse = this.openAssetBrowse($form);
                $form.find('.tabpage.asset').html($submoduleAssetBrowse);
                $submoduleAssetBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
                $submoduleAssetBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
                    var $assetForm, controller, $browse, assetFormData: any = {};
                    $browse = jQuery(this).closest('.fwbrowse');
                    controller = $browse.attr('data-controller');
                    assetFormData.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                    if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
                    if (typeof window[controller]['openForm'] !== 'function') throw 'Missing javascript function: ' + controller + '.openForm';
                    $assetForm = window[controller]['openForm']('NEW', assetFormData);
                    FwModule.openSubModuleTab($browse, $assetForm);
                });
            }
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

var ExpendableItemController = new ExpendableItem();