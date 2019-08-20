abstract class InventoryBase {
    Module: string = 'BaseInventory';
    apiurl: string = 'api/v1/baseinventory';
    caption: string = 'Base Inventory';
    AvailableFor: string = 'X';
    yearData: any = [];
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};

        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
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
        let $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
            request.uniqueids = {
                WarehouseId: warehouse.warehouseid
            }
        });

        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (let key in response) {
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
        let inventoryId;
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            let controlDefaults = JSON.parse(sessionStorage.getItem('controldefaults'));
            FwFormField.setValue($form, 'div[data-datafield="UnitId"]', controlDefaults.defaultunitid, controlDefaults.defaultunit);
            if (this.Module === 'RentalInventory') {
                RentalInventoryController.iCodeMask($form);
            }
        }

        if (typeof uniqueids !== 'undefined') {
            inventoryId = uniqueids.InventoryId;
        }

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, '.warehousefilter', warehouse.warehouseid, warehouse.warehouse);

        this.calculateYearly();

        const $calendar = $form.find('.calendar');
        this.addCalendarEvents($form, $calendar, inventoryId);

        const $realScheduler = $form.find('.realscheduler');
        this.addSchedulerEvents($form, $realScheduler, inventoryId);

        if (mode === 'NEW') {
            this.setupNewMode($form);
        }

        const controller = $form.attr('data-controller');
        if (typeof window[controller]['openFormInventory'] === 'function') {
            window[controller]['openFormInventory']($form);
        }

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    addCalendarEvents($form, $control, inventoryId) {
        let startOfMonth = moment().startOf('month').format('MM/DD/YYYY');
        let endOfMonth = moment().endOf('month').format('MM/DD/YYYY');
        $control
            .data('ongetevents', calendarRequest => {
                startOfMonth = moment(calendarRequest.start.value).format('MM/DD/YYYY');
                endOfMonth = moment(calendarRequest.start.value).add(calendarRequest.days, 'd').format('MM/DD/YYYY');
                let warehouseId = FwFormField.getValue($form, '.warehousefilter');   //justin 11/11/2018 fixing build error

                FwAppData.apiMethod(true, 'GET', `api/v1/inventoryavailability/calendarandscheduledata?&InventoryId=${inventoryId}&WarehouseId=${warehouseId}&FromDate=${startOfMonth}&ToDate=${endOfMonth}`, null, FwServices.defaultTimeout, response => {
                    FwScheduler.loadYearEventsCallback($control, [{ id: '1', name: '' }], this.yearlyEvents);
                    var calendarevents = response.InventoryAvailabilityCalendarEvents;
                    var schedulerEvents = response.InventoryAvailabilityScheduleEvents;
                    for (let i = 0; i < calendarevents.length; i++) {
                        if (calendarevents[i].textColor !== 'rgb(0,0,0') { // missing closing parens here
                            calendarevents[i].html = `<div style="color:${calendarevents[i].textColor};">${calendarevents[i].text}</div>`
                        }
                    }


                    //for (var i = 0; i < schedulerEvents.length; i++) {
                    //    if (schedulerEvents[i].textColor !== 'rgb(0,0,0') {
                    //        schedulerEvents[i].html = `<div style="color:${schedulerEvents[i].textColor}">${schedulerEvents[i].text}</div>`
                    //    }
                    //}
                    //self.loadScheduler($form, response.InventoryAvailabilityScheduleEvents, response.InventoryAvailabilityScheduleResources);
                    FwScheduler.loadEventsCallback($control, [{ id: '1', name: '' }], calendarevents);
                    this.loadInventoryDataTotals($form, response.InventoryData);
                }, function onError(response) {
                    FwFunc.showError(response);
                }, $control)

                //FwAppData.apiMethod(true, 'GET', `api/v1/inventoryavailabilitydate?InventoryId=${inventoryId}&WarehouseId=${warehouseId}&FromDate=${moment().startOf('year').format('MM/DD/YYYY')}&ToDate=${moment().endOf('year').format('MM/DD/YYYY')}`, null, FwServices.defaultTimeout, function onSuccess(response) {

                //}, function onError(response) {
                //    FwFunc.showError(response);
                //}, $calendar)
            })
            .data('ontimerangedoubleclicked', function (event) {
                try {
                    const date = event.start.toString('MM/dd/yyyy');
                    FwScheduler.setSelectedDay($control, date);
                    //DriverController.openTicket($form);
                    $form.find('div[data-type="Browse"][data-name="Schedule"] .browseDate .fwformfield-value').val(date).change();
                    $form.find('div.tab.schedule').click();
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
    }
    //----------------------------------------------------------------------------------------------
    addSchedulerEvents($form, $control, inventoryId) {
        $control
            .data('ongetevents', function (request) {
                var start = moment(request.start.value).format('MM/DD/YYYY');
                var end = moment(request.start.value).add(31, 'days').format('MM/DD/YYYY')
                let warehouseId = FwFormField.getValue($form, '.warehousefilter');

                FwAppData.apiMethod(true, 'GET', `api/v1/inventoryavailability/calendarandscheduledata?&InventoryId=${inventoryId}&WarehouseId=${warehouseId}&FromDate=${start}&ToDate=${end}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    var schedulerEvents = response.InventoryAvailabilityScheduleEvents;
                    for (let i = 0; i < schedulerEvents.length; i++) {
                        if (schedulerEvents[i].textColor !== 'rgb(0,0,0)') {
                            schedulerEvents[i].html = `<div style="color:${schedulerEvents[i].textColor};">${schedulerEvents[i].text}</div>`
                        } else {
                            schedulerEvents[i].html = `<div style="color:${schedulerEvents[i].textColor};text-align:left;">${schedulerEvents[i].text}</div>`
                        }
                    }
                    FwSchedulerDetailed.loadEventsCallback($control, response.InventoryAvailabilityScheduleResources, response.InventoryAvailabilityScheduleEvents);
                }, function onError(response) {
                    FwFunc.showError(response);
                }, $control)
            })
    }
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
                FwSchedulerDetailed.navigate($realScheduler, schddate, 35);
                FwSchedulerDetailed.refresh($realScheduler);
            }, 1);
        }

        let controller = $form.attr('data-controller');
        if (controller == "SalesInventoryController" || controller == "RentalInventoryController") {
            let $submoduleRepairOrderBrowse = this.openRepairOrderBrowse($form);
            $form.find('.repairOrderSubModule').append($submoduleRepairOrderBrowse);
        }
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    openRepairOrderBrowse($form) {
        const inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
        const $browse = RepairController.openBrowse();
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

        //Accessory Revenue Allocation checkboxes and radio events
        $form.find(`[data-datafield="OverrideSystemDefaultRevenueAllocationBehavior"] .fwformfield-value`).on('change', e => {
            const $this = jQuery(e.currentTarget);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="AllocateRevenueForAccessories"]'));
            } else {
                FwFormField.disable($form.find('[data-datafield="AllocateRevenueForAccessories"]'));
                FwFormField.disable($form.find('[data-datafield="PackageRevenueCalculationFormula"]'));
            }
        });
        $form.find(`[data-datafield="AllocateRevenueForAccessories"] .fwformfield-value`).on('change', e => {
            const $this = jQuery(e.currentTarget);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="PackageRevenueCalculationFormula"]'));
            } else {
                FwFormField.disable($form.find('[data-datafield="PackageRevenueCalculationFormula"]'));
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
    //jh 08/19/2019 obsolete
    //loadGantt($form) {
    //    var dp = new DayPilot.Gantt($form.find('#gantt')[0]);
    //    dp.startDate = moment().format('YYYY-MM-DD');
    //    dp.days = 31;
    //    dp.init();
    //    dp.onBeforeRowHeaderRender = function (args) {
    //        args.row.backColor = 'coral';
    //    };
    //    dp.onBeforeCellRender = function (args) {
    //        args.cell.backColor = 'coral';
    //    };

    //    dp.tasks.list = [
    //        {
    //            "id": "1",
    //            "text": "200002-024",
    //            "start": "2018-11-30T00:00:00",
    //            "end": "2018-12-15T00:00:00",
    //            "complete": "200002-024 PENDING EXCHANGE (DED RENTALS)",
    //            "box": {
    //                "bubbleHtml": "Task details (box): <br/>Task 0<br>Starting on November 21, 2018",
    //                "html": "200002-024 PENDING EXCHANGE (DED RENTALS)",
    //                "htmlRight": "200002-024"
    //            },
    //        },
    //        {
    //            "id": "2",
    //            "text": "L300044",
    //            "start": "2018-11-30T00:00:00",
    //            "end": "2018-12-30T00:00:00",
    //            "complete": "L300044 SUB-RENTAL REPORT - SUBS (THE MOVIE HOUSE - 2014 RENTALS)",
    //            "box": {
    //                "bubbleHtml": "Task details (box): <br/>Task 0<br>Starting on November 21, 2018",
    //                "html": "L300044 SUB-RENTAL REPORT - SUBS (THE MOVIE HOUSE - 2014 RENTALS)",
    //                "htmlRight": "L300044"
    //            },
    //        },
    //        {
    //            "id": "5",
    //            "text": "L300044",
    //            "start": "2018-12-16T00:00:00",
    //            "end": "2018-12-30T00:00:00",
    //            "complete": "L300044 SUB-RENTAL REPORT - SUBS (THE MOVIE HOUSE - 2014 RENTALS)",
    //            "box": {
    //                "bubbleHtml": "Task details (box): <br/>Task 0<br>Starting on November 21, 2018",
    //                "html": "L300044 SUB-RENTAL REPORT - SUBS (THE MOVIE HOUSE - 2014 RENTALS)",
    //                "htmlRight": "L300044"
    //            },
    //        },
    //        {
    //            "id": "3",
    //            "text": "L300230",
    //            "start": "2018-11-30T00:00:00",
    //            "end": "2018-12-30T00:00:00",
    //            "complete": "L300230 CROSS I-CODE EXCHANGE TEST (FELD RENTALS)",
    //            "title": "hello",
    //            "box": {
    //                "bubbleHtml": "Task details (box): <br/>Task 0<br>Starting on November 21, 2018",
    //                "html": "L300230 CROSS I-CODE EXCHANGE TEST (FELD RENTALS)",
    //                "title": "L300230 CROSS I-CODE EXCHANGE TEST (FELD RENTALS)",
    //                "htmlRight": "L300230"
    //            },
    //        },
    //        {
    //            "id": "4",
    //            "text": "L300962",
    //            "start": "2018-11-30T00:00:00",
    //            "end": "2018-12-02T00:00:00",
    //            "complete": "",
    //            "title": "hello",
    //            "box": {
    //                "bubbleHtml": "Task details (box): <br/>Task 0<br>Starting on November 21, 2018",
    //                "html": "L300962 ORDER FOR STAGING A CONTAINER (ENDLESS3)",
    //                "title": "L300962 ORDER FOR STAGING A CONTAINER (ENDLESS3)",
    //                "htmlRight": "L300962"
    //            },
    //            "row": {
    //                "bubbleHtml": "Task details (row): <br/>Task 0<br>Starting on November 21, 2018"
    //            }
    //        },
    //    ]
    //    dp.update();
    //}
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
    loadInventoryDataTotals($form: JQuery, data) {
        $form.find(`.inv-data-totals [data-totalfield="Total"] input`).val(data.Total.Total);
        $form.find(`.inv-data-totals [data-totalfield="In"] input`).val(data.In.Total);
        $form.find(`.inv-data-totals [data-totalfield="QcRequired"] input`).val(data.QcRequired.Total);
        $form.find(`.inv-data-totals [data-totalfield="InContainer"] input`).val(data.InContainer.Total);
        $form.find(`.inv-data-totals [data-totalfield="Staged"] input`).val(data.Staged.Total);
        $form.find(`.inv-data-totals [data-totalfield="Out"] input`).val(data.Out.Total);
        $form.find(`.inv-data-totals [data-totalfield="InRepair"] input`).val(data.InRepair.Total);
        $form.find(`.inv-data-totals [data-totalfield="InTransit"] input`).val(data.InTransit.Total);
        //$form.find(`.inv-data-totals [data-totalfield="QcRequired"] input`).val(data.QcRequired.Total); // on PO still wip
    }
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
    afterLoad($form: any) {
        //Disable "Create Complete" if classification isn't Item or Accessory
        const classification = FwFormField.getValueByDataField($form, 'Classification');
        if (classification !== 'A' && classification !== 'I') {
            $form.find('.fwform-menu .submenu-btn').css({ 'pointer-events': 'none', 'color': 'lightgray' });
        }

        //Enable/disable duplicate fields based on classification
        if (classification === 'C') {
            FwFormField.disable($form.find('.kits-tab[data-datafield="SeparatePackageOnQuoteOrder"]'));
            FwFormField.disable($form.find('.kits-tab[data-datafield="OverrideSystemDefaultRevenueAllocationBehavior"]'));
            FwFormField.enable($form.find('.completes-tab[data-datafield="SeparatePackageOnQuoteOrder"]'));
            FwFormField.enable($form.find('.completes-tab[data-datafield="OverrideSystemDefaultRevenueAllocationBehavior"]'));
        }

        const overrideDefaultAllocation = FwFormField.getValueByDataField($form, 'OverrideSystemDefaultRevenueAllocationBehavior');
        const allocateRevChecked = FwFormField.getValueByDataField($form, 'AllocateRevenueForAccessories');
        const $allocateRevenueForAcc = $form.find('[data-datafield="AllocateRevenueForAccessories"]');
        const $calculationFormulaRadio = $form.find('[data-datafield="PackageRevenueCalculationFormula"]');
        if (overrideDefaultAllocation) {
            FwFormField.enable($allocateRevenueForAcc);
            if (allocateRevChecked) {
                FwFormField.enable($calculationFormulaRadio);
            }
        } else {
            FwFormField.disable($allocateRevenueForAcc);
            FwFormField.disable($calculationFormulaRadio);
        }

        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"]', e => {
            const tabname = jQuery(e.currentTarget).attr('id');
            const lastIndexOfTab = tabname.lastIndexOf('tab');
            const tabpage = `${tabname.substring(0, lastIndexOfTab)}tabpage${tabname.substring(lastIndexOfTab + 3)}`;

            const $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
            if ($gridControls.length > 0) {
                for (let i = 0; i < $gridControls.length; i++) {
                    const $gridcontrol = jQuery($gridControls[i]);
                    FwBrowse.search($gridcontrol);
                }
            }

            const $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
            if ($browseControls.length > 0) {
                for (let i = 0; i < $browseControls.length; i++) {
                    const $browseControl = jQuery($browseControls[i]);
                    FwBrowse.search($browseControl);
                }
            }
        });
    }
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
    ]
}
//var InventoryBaseController = new InventoryBase();