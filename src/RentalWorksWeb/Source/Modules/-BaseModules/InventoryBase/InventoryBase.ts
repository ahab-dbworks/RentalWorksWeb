abstract class InventoryBase {
    Module: string = 'BaseInventory';
    apiurl: string = 'api/v1/baseinventory';
    caption: string = 'Base Inventory';
    AvailableFor: string = 'X';
    yearData: any = [];
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    CreateCompleteId: string;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        FwMenu.addBrowseMenuButtons(options);

        const $all: JQuery = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $item: JQuery = FwMenu.generateDropDownViewBtn('Item', true, "I");
        const $accessory: JQuery = FwMenu.generateDropDownViewBtn('Accessory', false, "A");
        const $complete: JQuery = FwMenu.generateDropDownViewBtn('Complete', false, "C");
        const $kit: JQuery = FwMenu.generateDropDownViewBtn('Kit', false, "K");
        const $set: JQuery = FwMenu.generateDropDownViewBtn('Set', false, "S");
        const $misc: JQuery = FwMenu.generateDropDownViewBtn('Misc', false, "M");
        const $container: JQuery = FwMenu.generateDropDownViewBtn('Container', false, "N");

        FwMenu.addVerticleSeparator(options.$menu);

        const viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $item, $accessory, $complete, $kit, $set, $misc);
        if (this.AvailableFor === "R") {
            viewSubitems.push($container);
        }
        FwMenu.addViewBtn(options.$menu, 'View', viewSubitems, true, "Classification");
    }
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        FwMenu.addFormMenuButtons(options);
        FwMenu.addSubMenuItem(options.$groupOptions, 'Create Complete', this.CreateCompleteId, (e: JQuery.ClickEvent) => {
            try {
                this.createComplete(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
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
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        const warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
            request.uniqueids = {
                WarehouseId: warehouseId
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
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, uniqueids?) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            const controlDefaults = JSON.parse(sessionStorage.getItem('controldefaults'));
            FwFormField.setValue($form, 'div[data-datafield="UnitId"]', controlDefaults.defaultunitid, controlDefaults.defaultunit); // 2 condition blocks of same eval?
            FwFormField.setValue($form, 'div[data-datafield="Rank"]', controlDefaults.defaultrank, controlDefaults.defaultrank);
            if (this.Module === 'RentalInventory') {
                RentalInventoryController.iCodeMask($form);
            }
            // Disables 'Create Complete' menu option for any new Inventory
            $form.find('.submenu-btn').filter(() => {
                return jQuery(this).text() === 'Create Complete';
            }).css({ 'pointer-events': 'none', 'color': '#E0E0E0' });


            // hide the set and wall options initially.  will be shown if a Sets InventoryType is selected
            $form.find('.setradio').hide();
            $form.find('.wallradio').hide();

        }

        let inventoryId;
        if (typeof uniqueids !== 'undefined') {
            inventoryId = uniqueids.InventoryId;
        } else {
            inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
        }

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, '.warehousefilter', warehouse.warehouseid, warehouse.warehouse);

        //this.calculateYearly();

        const $calendar = $form.find('.calendar');
        this.addCalSchedEvents($form, $calendar, inventoryId);

        const $realScheduler = $form.find('.realscheduler');
        this.addCalSchedEvents($form, $realScheduler, inventoryId);

        if (mode === 'NEW') {
            this.setupNewMode($form);
        }

        let userassignedicodes = JSON.parse(sessionStorage.getItem('controldefaults')).userassignedicodes;
        if (userassignedicodes) {
            FwFormField.enable($form.find('[data-datafield="ICode"]'));
            $form.find('[data-datafield="ICode"]').attr(`data-required`, `true`);
        }
        else {
            FwFormField.disable($form.find('[data-datafield="ICode"]'));
            $form.find('[data-datafield="ICode"]').attr(`data-required`, `false`);
        }

        const controller = $form.attr('data-controller');
        if (typeof window[controller]['openFormInventory'] === 'function') {
            window[controller]['openFormInventory']($form);
        }

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT', uniqueids);
        $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(uniqueids.InventoryId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    openSubModuleBrowse($form, module: string) {
        try {
            let $browse = null;
            if (typeof window[`${module}Controller`] !== undefined && typeof window[`${module}Controller`].openBrowse === 'function') {
                $browse = (<any>window)[`${module}Controller`].openBrowse();
                $browse.data('ondatabind', request => {
                    request.activeviewfields = (<any>window)[`${module}Controller`].ActiveViewFields;
                    request.uniqueids = {
                        InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                    }
                });
            }
            return $browse;
        } catch (ex) {

        }
    }
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    createComplete($form: JQuery) {
        try {
            const inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            FwAppData.apiMethod(true, 'POST', `api/v1/inventorycompletekit/createcomplete/${inventoryId}`, null, FwServices.defaultTimeout,
                response => {
                    const uniqueIds: any = {};
                    uniqueIds.InventoryId = response.PackageId;
                    const $completeForm = RentalInventoryController.loadForm(uniqueIds);
                    FwModule.openSubModuleTab($form, $completeForm);
                }, ex => {
                    FwFunc.showError(ex);
                }, $form);
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    addCalSchedEvents($form, $control, inventoryId) {
        if ($control.hasClass('calendar')) {
            $control
                .data('ongetevents', request => {
                    const startOfMonth = moment(request.start.value).format('MM/DD/YYYY');
                    const days = request.days ? request.days : 34;
                    let endOfMonth = moment(request.start.value).add(days, 'days').format('MM/DD/YYYY');
                    let warehouseId;
                    let allWh;

                    if ($form.is('tr') || $form.data('fromQuikSearch')) {
                        allWh = $form.data('allwarehousesfilter');
                    } else if ($form.find('[data-datafield="AllWarehouses"]').length > 0) {
                        allWh = FwFormField.getValueByDataField($form, 'AllWarehouses');
                    } else {
                        allWh = false;
                    }

                    if (allWh && allWh != 'F') {
                        warehouseId = '';
                    } else {
                        if ($form.is('tr') || $form.data('fromQuikSearch')) {
                            if (typeof $form.data('warehousefilter') === 'string') {
                                warehouseId = $form.data('warehousefilter');
                            } else {
                                warehouseId = FwBrowse.getValueByDataField($control, $form, 'WarehouseId');
                            }
                        } else {
                            warehouseId = FwFormField.getValue($form, '.warehousefilter');            //justin 11/11/2018 fixing build error
                        }
                    }
                    if (inventoryId === null || inventoryId === '') {
                        inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');     // err here if line in grid has no icode selected
                    }
                    let includeHours: boolean = false;
                    if ((request.mode === 'Day') || (request.mode === 'Week')) {
                        includeHours = true;
                    }

                    const availRequest: any = {
                        InventoryId: inventoryId,
                        WarehouseId: warehouseId.split(','),
                        FromDate: startOfMonth,
                        ToDate: endOfMonth,
                        IncludeHours: includeHours,
                    };

                    if (request.mode === 'Year') {
                        availRequest.YearView = true;

                        const dpyear = $control.data('dpyear');
                        const requestYear = startOfMonth.substring(startOfMonth.length - 4)
                        const date = new Date(`01/01/${requestYear}`);
                        dpyear.startDate = new DayPilot.Date(date);
                    }

                    FwAppData.apiMethod(true, 'POST', `api/v1/inventoryavailability/calendarandscheduledata`, availRequest, FwServices.defaultTimeout, response => {
                        let calendarEvents = response.InventoryAvailabilityCalendarEvents;
                        $control.data('reserveDates', response.Dates);                           // loading reservation data onto control for use in renderDatePopup()
                        for (let i = 0; i < calendarEvents.length; i++) {
                            if (calendarEvents[i].textColor !== 'rgb(0,0,0)') {
                                calendarEvents[i].html = `<div style="color:${calendarEvents[i].textColor};">${calendarEvents[i].text}</div>`
                            }
                        }

                        let resources = [{ id: '1', name: '' }];
                        $control.find('div.adjustcontainer').css('max-width', '1670px');
                        if ($control.find('div.changeview[data-selected="true"]').html() === 'Year') {
                            resources = response.InventoryAvailabilityScheduleResources;
                            calendarEvents = response.InventoryAvailabilityScheduleEvents;
                            for (let i = 0; i < calendarEvents.length; i++) {
                                calendarEvents[i].html = `<div style="${calendarEvents[i].backColor ? `background-color:${calendarEvents[i].backColor};` : ''}color:${calendarEvents[i].textColor};text-align:center;">${calendarEvents[i].text}</div>`;
                            }

                            $control.closest('div.adjustcontainer').css('max-width', '1670px');
                            $control.closest('div.inner-cal-container').css('max-width', '1650px');
                        } else {
                            $control.closest('div.adjustcontainer').css('max-width', '1365px');
                            $control.closest('div.inner-cal-container').css('max-width', '1345px');
                        }
                        FwScheduler.loadEventsCallback($control, resources, calendarEvents);

                        if ($form.is('tr') || $form.data('fromQuikSearch')) {
                            $form = jQuery('#availabilityCalendarPopup');
                        }
                        this.loadInventoryDataTotals($form, response.InventoryData);
                    }, function onError(response) {
                        FwFunc.showError(response);
                    }, $control)
                })
                .data('oneventclick', e => {
                    const data = e.e.data;
                    const reservationDate = moment(data.start.value).format('YYYY-MM-DD');
                    const displayDate = moment(data.start.value).format('MM/DD/YYYY');
                    this.renderReservationsPopup($control, reservationDate, displayDate);
                })
                .data('oneventdoubleclick', e => {
                    if (typeof $control.data('oneventclick') === 'function') {
                        $control.data('oneventclick')(e);
                    }
                })
                .data('oneventdoubleclicked', e => {
                    if (typeof $control.data('oneventclick') === 'function') {
                        $control.data('oneventclick')(e);
                    }
                })
                .data('ontimerangeselect', e => {
                    try {
                        const date = e.start.toString('MM/dd/yyyy');
                        FwScheduler.setSelectedDay($control, date);
                        //DriverController.openTicket($form);
                        $form.find('div[data-type="Browse"][data-name="Schedule"] .browseDate .fwformfield-value').val(date).change();
                        $form.find('div.tab.schedule').click();
                        const reservationDate = moment(e.start.value).format('YYYY-MM-DD');
                        const displayDate = moment(e.start.value).format('MM/DD/YYYY');
                        this.renderReservationsPopup($control, reservationDate, displayDate);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                })
                .data('ontimerangedoubleclicked', e => {
                    if (typeof $control.data('ontimerangeselect') === 'function') {
                        $control.data('ontimerangeselect')(e);
                    }
                })
        } else if ($control.hasClass('realscheduler')) {
            $control
                .data('ongetevents', request => {
                    const startOfMonth = moment(request.start.value).format('MM/DD/YYYY');
                    const endOfMonth = moment(request.start.value).add(31, 'days').format('MM/DD/YYYY');
                    let warehouseId;
                    let allWh;

                    if ($form.is('tr') || $form.data('fromQuikSearch')) {
                        allWh = $form.data('allwarehousesfilter');
                    } else if ($form.find('[data-datafield="AllWarehouses"]').length > 0) {
                        allWh = FwFormField.getValueByDataField($form, 'AllWarehouses');
                    } else {
                        allWh = false;
                    }

                    if (allWh && allWh != 'F') { //checkbox values return strings sometimes instead of booleans
                        warehouseId = '';
                    } else {
                        if ($form.is('tr') || $form.data('fromQuikSearch')) {
                            if (typeof $form.data('warehousefilter') === 'string') {
                                warehouseId = $form.data('warehousefilter');
                            } else {
                                warehouseId = FwBrowse.getValueByDataField($control, $form, 'WarehouseId');
                            }
                        } else {
                            warehouseId = FwFormField.getValue($form, '.warehousefilter');
                        }
                    }
                    if (inventoryId === null || inventoryId === '') {
                        inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                    }
                    const availRequest: any = {
                        InventoryId: inventoryId,
                        WarehouseId: warehouseId.split(','),
                        FromDate: startOfMonth,
                        ToDate: endOfMonth,
                    };
                    FwAppData.apiMethod(true, 'POST', `api/v1/inventoryavailability/calendarandscheduledata`, availRequest, FwServices.defaultTimeout, function onSuccess(response) {
                        const schedulerEvents = response.InventoryAvailabilityScheduleEvents;
                        for (let i = 0; i < schedulerEvents.length; i++) {
                            if (schedulerEvents[i].isWarehouseTotal === true) {
                                schedulerEvents[i].html = `<div class="warehouse" style="color:${schedulerEvents[i].textColor};text-align:center;">${schedulerEvents[i].text}</div>`
                            } else {
                                //schedulerEvents[i].html = `<div style="color:${schedulerEvents[i].textColor};text-align:left;"><span style="font-weight:700;padding:0 5px 0 0;">${schedulerEvents[i].total}</span>${schedulerEvents[i].text}</div>`
                                let html: string = "";
                                html += `<div class="order" style="`;
                                if (schedulerEvents[i].backColor) {
                                    html += `background-color:${schedulerEvents[i].backColor};`;
                                }
                                html += `color:${schedulerEvents[i].textColor};text-align:left;"><span style="font-weight:700;padding:0 5px 0 0;">${schedulerEvents[i].total}</span>${schedulerEvents[i].text}</div>`;
                                schedulerEvents[i].html = html;
                            }
                        }
                        FwSchedulerDetailed.loadEventsCallback($control, response.InventoryAvailabilityScheduleResources, response.InventoryAvailabilityScheduleEvents);
                    }, function onError(response) {
                        FwFunc.showError(response);
                    }, $control)
                })
                .data('oneventdoubleclicked', request => {
                    const data = request.e.data;
                    let module;
                    let datafield;
                    let id;
                    let title;
                    const orderType = data.orderType;
                    if (orderType) {
                        switch (data.orderType) {
                            case 'O': //ORDER
                                module = 'Order';
                                datafield = 'OrderId';
                                id = data.orderId;
                                title = data.orderDescription;
                                break;
                            case 'Q': //QUOTE
                                module = 'Quote';
                                datafield = 'QuoteId';
                                id = data.orderId;
                                title = data.orderDescription;
                                break;
                            case 'C': //PURCHASE ORDER
                                module = 'PurchaseOrder';
                                datafield = 'PurchaseOrderId';
                                id = data.orderId;
                                title = data.orderDescription;
                                break;
                            case 'T': //TRANSFER
                                module = 'TransferOrder';
                                datafield = 'TransferId';
                                id = data.orderId;
                                title = data.orderDescription;
                                break;
                            case 'R': //REPAIR
                                module = 'Repair';
                                datafield = 'RepairId';
                                id = data.orderId;
                                title = data.orderDescription;
                                break;
                            case 'N': //CONTAINER
                                module = 'Container';
                                datafield = 'ContainerItemId';
                                id = data.orderId;
                                title = data.orderDescription;
                                break;
                            case 'PENDING': //PENDING EXCHANGE
                                module = 'Contract';
                                datafield = 'ContractId';
                                id = data.contractId;
                                title = data.orderDescription;
                                break;
                            default:
                                FwFunc.showError('Invalid Order Type');
                                break;
                        }
                    }

                    FwValidation.validationPeek($control, module, id, datafield, $form, title);
                });
        }
    }
    //----------------------------------------------------------------------------------------------
    loadScheduler($form, events, resources) {
        const dp = new DayPilot.Scheduler($form.find('.realscheduler')[0]);

        // behavior and appearance
        dp.cellWidth = 40;
        dp.eventHeight = 30;
        dp.headerHeight = 25;

        // view
        //dp.startDate = moment().format('YYYY-MM-DD');  // or just dp.startDate = "2013-03-25";
        dp.startDate = FwSchedulerDetailed.getTodaysDate();  //#1305 11/15/2019 justin hoffman.  Without this, the calandar advances to the next day when viewing the calendar after 4pm on your machine.
        dp.days = 31;
        dp.scale = "Day";
        dp.timeHeaders = [
            { groupBy: "Month" },
            { groupBy: "Day", format: "dddd" }
        ];
        dp.treeEnabled = true;
        dp.resources = resources;
        dp.events.list = events;

        dp.bubble = new DayPilot.Bubble({
            cssClassPrefix: "bubble_default",
            onLoad: function (args) {
                var ev = args.source;
                args.async = true;  // notify manually using .loaded()

                //args.html = "<div style='font-weight:bold'>" + ev.text() + "</div><div>Order Number: " + ev.data.orderNumber + "</div><div>Order Status: " + ev.data.orderStatus + "</div><div>Deal: " + ev.data.deal + "</div><div>Start: " + ev.start().toString("MM/dd/yyyy HH:mm") + "</div><div>End: " + ev.end().toString("MM/dd/yyyy HH:mm") + "</div><div>Id: " + ev.id() + "</div>";
                args.html = ``;
                args.html += `<div style='font-weight:bold'>${ev.text()}</div><div>Order Number: ${ev.data.orderNumber}</div><div>Order Status: ${ev.data.orderStatus}</div><div>Deal: ${ev.data.deal}</div><div>Start: ${ev.start().toString("MM/dd/yyyy HH:mm")}</div><div>End: ${ev.end().toString("MM/dd/yyyy HH:mm")}</div><div>Id: ${ev.id()}</div>`;
                if (ev.data.subPoNumber) {
                    args.html += `<div>Sub PO Number: ${ev.data.subPoNumber}</div>`;
                    if (ev.data.subPoVendor) {
                        args.html += `<div>Vendor: ${ev.data.subPoVendor}</div>`;
                    }
                }
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
    events($form: any): void {
        $form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').on('change', function () {
            const $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
            }
            else {
                FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
            }
        })

        $form.find('div[data-datafield="InventoryTypeId"]').data('onchange', $tr => {
            if ($tr.find('.field[data-browsedatafield="Wardrobe"]').attr('data-originalvalue') === 'true') {
                $form.find('.wardrobetab').show();
            } else {
                $form.find('.wardrobetab').hide();
            }

            if ($tr.find('.field[data-browsedatafield="Sets"]').attr('data-originalvalue') === 'true') {
                const classificationValue = FwFormField.getValueByDataField($form, 'Classification');
                $form.find('.wallsection').hide();
                $form.find('.settab').hide();
                $form.find('.optionssection').hide();
                $form.find('.manufacturersection').hide();
                if (classificationValue === 'W') {
                    $form.find('.wallsection').show();
                }
                else if (classificationValue === 'S') {
                    $form.find('.settab').show();
                }
            } else {
                $form.find('.wallsection').hide();
                $form.find('.settab').hide();
                $form.find('.optionssection').show();
                $form.find('.manufacturersection').show();
            }


            if (!FwFormField.getValueByDataField($form, 'InventoryId')) { // new mode
                if ($tr.find('.field[data-browsedatafield="Sets"]').attr('data-originalvalue') === 'true') {
                    $form.find('.kitradio').hide();
                    $form.find('.completeradio').hide();
                    $form.find('.itemradio').hide();
                    $form.find('.accessoryradio').hide();
                    $form.find('.miscradio').hide();
                    $form.find('.containerradio').hide();
                    $form.find('.setradio').show();
                    $form.find('.wallradio').show();
                    FwFormField.setValueByDataField($form, 'Classification', 'W');
                    $form.find('[data-datafield="Classification"] .fwformfield-value').change(); // thank you Josh
                }
                else {
                    $form.find('.kitradio').show();
                    $form.find('.completeradio').show();
                    $form.find('.itemradio').show();
                    $form.find('.accessoryradio').show();
                    $form.find('.miscradio').show();
                    $form.find('.containerradio').show();
                    $form.find('.setradio').hide();
                    $form.find('.wallradio').hide();
                    FwFormField.setValueByDataField($form, 'Classification', 'I');
                    $form.find('[data-datafield="Classification"] .fwformfield-value').change(); // thank you Josh
                }
            }
        })

        $form.find('div[data-datafield="CategoryId"]').data('onchange', $tr => {
            if ($tr.find('.field[data-browsedatafield="SubCategoryCount"]').attr('data-originalvalue') > 0) {
                FwFormField.enable($form.find('div[data-datafield="SubCategoryId"]'));
                $form.find('[data-datafield="SubCategoryId"]').attr(`data-required`, `true`);
            } else {
                FwFormField.setValueByDataField($form, 'SubCategoryId', '')
                $form.find('[data-datafield="SubCategoryId"]').attr(`data-required`, `false`);
                FwFormField.disable($form.find('div[data-datafield="SubCategoryId"]'));
            }
        })
        // Hides or shows Asset tab for particular settings on the form
        $form.find('.class-tracked-radio input').on('change', () => {
            const classificationValue = FwFormField.getValueByDataField($form, 'Classification');
            const trackedByValue = FwFormField.getValueByDataField($form, 'TrackedBy');
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
        })
        $form.find(`[data-datafield="AllocateRevenueForAccessories"] .fwformfield-value`).on('change', e => {
            const $this = jQuery(e.currentTarget);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="PackageRevenueCalculationFormula"]'));
            } else {
                FwFormField.disable($form.find('[data-datafield="PackageRevenueCalculationFormula"]'));
            }
        })

        // G/L Accounts
        $form.find('div[data-datafield="AssetAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="AssetAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        })
        $form.find('div[data-datafield="IncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="IncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        })
        $form.find('div[data-datafield="SubIncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="SubIncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        })
        $form.find('div[data-datafield="EquipmentSaleIncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="EquipmentSaleIncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        })
        $form.find('div[data-datafield="LdIncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="LdIncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        })
        $form.find('div[data-datafield="CostOfGoodsSoldExpenseAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="CostOfGoodsSoldExpenseAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        })
        $form.find('div[data-datafield="CostOfGoodsRentedExpenseAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="CostOfGoodsRentedExpenseAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        })

        //Load Availability Calender when the tab is clicked
        $form.find('[data-type="tab"][data-caption="Availability Calendar"]').on('click', e => {

            //enable warehouse filters (to bypass enabling inventory access in security tree)
            const $warehouseFilters = $form.find('[data-datafield="WarehouseId"], [data-datafield="AllWarehouses"]');
            FwFormField.enable($warehouseFilters);

            if ($form.attr('data-mode') !== 'NEW') {
                let schddate;
                const $calendar = $form.find('.calendar');
                if ($calendar.length > 0) {
                    schddate = FwScheduler.getTodaysDate();
                    FwScheduler.navigate($calendar, schddate);
                    //FwScheduler.refresh($calendar);
                }

                const $realScheduler = $form.find('.realscheduler');
                if ($realScheduler.length > 0) {
                    schddate = FwSchedulerDetailed.getTodaysDate();
                    FwSchedulerDetailed.navigate($realScheduler, schddate, 35);
                    //FwSchedulerDetailed.refresh($realScheduler);
                }

                // Legend for Avail Calendar
                const availSchedControl = $form.find('.cal-sched')
                try {
                    if (availSchedControl.hasClass('legend-loaded') === false) {
                        FwAppData.apiMethod(true, 'GET', `${this.apiurl}/availabilitylegend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                            for (let key in response) {
                                FwBrowse.addLegend(availSchedControl, key, response[key]);
                            }
                            availSchedControl.addClass('legend-loaded');
                        }, function onError(response) {
                            FwFunc.showError(response);
                        }, availSchedControl);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }

                $form.find('.warehousefilter').on('change keyup', e => {
                    e.stopImmediatePropagation();
                    if (e.type != 'keyup') { //overriding event that sets form as modified in fwmodule
                        FwScheduler.refresh($calendar);
                        FwSchedulerDetailed.refresh($realScheduler);
                    }
                });
            } else {
                e.stopImmediatePropagation();
                FwNotification.renderNotification('WARNING', 'Save Record first.');
            }
        });

        //Price calculation section for completes and kits
        $form.find('[data-datafield="PackagePrice"]').on('change', e => {
            const packagePrice = FwFormField.getValue2($form.find('[data-datafield="PackagePrice"]:visible'));
            const classification = FwFormField.getValueByDataField($form, 'Classification');
            if (packagePrice == 'CP') {
                this.enablePricingFields($form);
            } else {
                if (classification == 'K') {
                    FwBrowse.disableGrid($form.find('[data-grid="InventoryWarehouseKitPricingGrid"]'));
                } else if (classification == 'C') {
                    FwBrowse.disableGrid($form.find('[data-grid="InventoryWarehouseCompletePricingGrid"]'));
                }
            }

            if (classification == 'K') {
                FwBrowse.search($form.find('[data-name="InventoryWarehouseKitPricingGrid"]'));
            } else if (classification == 'C') {
                FwBrowse.search($form.find('[data-name="InventoryWarehouseCompletePricingGrid"]'));
            }
        });

        //Toggle All Warehouses
        $form.find(`[data-datafield="AllWarehouses"]`).on('change', e => {
            e.stopImmediatePropagation();
            const $this = jQuery(e.currentTarget);
            if (FwFormField.getValue2($this)) {
                FwFormField.disable($form.find('[data-datafield="WarehouseId"]'));
            } else {
                FwFormField.enable($form.find('[data-datafield="WarehouseId"]'));
            }

            const $calendar = $form.find('.calendar');
            const $realScheduler = $form.find('.realscheduler');
            FwSchedulerDetailed.refresh($realScheduler);
            FwScheduler.refresh($calendar);
        });
    }
    //----------------------------------------------------------------------------------------------
    enablePricingFields($form) {
        const classification = FwFormField.getValueByDataField($form, 'Classification');
        let $fields;
        if (classification == 'K') {
            $fields = $form.find('[data-grid="InventoryWarehouseKitPricingGrid"] .can-enable');
        } else if (classification == 'C') {
            $fields = $form.find('[data-grid="InventoryWarehouseCompletePricingGrid"] .can-enable');
        }

        jQuery.each($fields, (i, el) => {
            const $cell = jQuery(el);
            if ($cell) {
                $cell.attr('data-formreadonly', 'false');
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderReservationsPopup($control: any, reservationDate: string, displayDate): void {
        const reserveDates = $control.data('reserveDates');
        const theDate = reserveDates.filter(el => {
            return el.TheDate.startsWith(reservationDate);
        })
        if (theDate.length && theDate[0].Reservations.length) {
            const html: Array<string> = [];
            html.push(
                `<div class="fwcontrol fwcontainer fwform popup" data-control="FwContainer" data-type="form" data-caption="Reservations" style="overflow:auto;max-height: calc(100vh - 140px);">
                  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                    <div class="tabpages">
                      <div class="formpage">
                        <div class="formrow">
                          <div class="formcolumn" style="width:100%;margin-top:5px;">
                            <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                              <div id="availabilityTable" class="flexrow" style="max-width:none;margin:15px;">
                                <table>
                                  <thead>
                                    <tr>
                                      <th>Order Type</th>
                                      <th>Order No.</th>
                                      <th>Description</th>
                                      <th>Deal</th>
                                      <th class="number">Reserved</th>
                                      <th class="number">Sub</th>
                                      <th class="number">Staged</th>
                                      <th class="number">Out</th>
                                      <th>From</th>
                                      <th>To</th>
                                    <tr>
                                  </thead>
                                  <tbody>
                                  </tbody>
                                </table>
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>`);

            const $popup = FwPopup.renderPopup(jQuery(html.join('')), { ismodal: true }, `Reservations ${displayDate}`);
            FwPopup.showPopup($popup);
            const $rows: any = [];
            const reservations = theDate[0].Reservations;
            for (let i = 0; i < reservations.length; i++) {
                const data = reservations[i]
                const row = `
                    <tr class="data-row">
                        <td>${data.OrderTypeDescription}</td>
                        <td class="order-number" data-id="${data.OrderId}" data-ordertype="${data.OrderType}"><span>${data.OrderNumber}</span>${data.OrderType !== 'N' ? '<i class= "material-icons btnpeek">more_horiz</i>' : ''}</td>
                        <td>${data.OrderDescription}</td>
                        <td data-id="${data.DealId}"><span>${data.Deal}</span>${data.Deal !== '' ? '<i class= "material-icons btnpeek">more_horiz</i>' : ''}</td>
                        <td class="number">${data.QuantityReserved.Total}</td>
                        <td class="number">${data.QuantitySub}</td>
                        <td class="number">${data.QuantityStaged.Total}</td>
                        <td class="number">${data.QuantityOut.Total}</td>
                        <td>${data.FromDateTimeDisplay}</td>
                        <td>${data.ToDateTimeDisplay}</td>
                    </tr>
                    <tr class="avail-calendar" style="display:none;"><tr>
                    `;
                $rows.push(row);
            }

            $popup.find('tbody').empty().append($rows);

            this.reservationsPopupEvents($popup);
        } else {
            FwNotification.renderNotification('INFO', 'No reservation data for this date.')
        }
    }
    //----------------------------------------------------------------------------------------------
    reservationsPopupEvents($control) {
        //add validation peeks
        $control.find('#availabilityTable table tr td i.btnpeek')
            .off('click')
            .on('click', e => {
                try {
                    //$control.find('.btnpeek').hide();
                    //$validationbrowse.data('$control').find('.validation-loader').show();
                    //setTimeout(function () {
                    const $control = jQuery(e.currentTarget).closest('td');
                    const validationId = $control.attr('data-id');
                    let datafield;
                    let validationPeekFormName;
                    if ($control.hasClass('order-number')) {
                        const orderType = $control.attr('data-ordertype');
                        switch (orderType) {
                            case 'O':
                                datafield = 'OrderId';
                                validationPeekFormName = 'Order';
                                break;
                            case 'Q':
                                datafield = 'QuoteId';
                                validationPeekFormName = 'Quote';
                                break;
                            case 'R':
                                datafield = 'RepairId';
                                validationPeekFormName = 'Repair';
                                break;
                            case 'T':
                                datafield = 'TransferId';
                                validationPeekFormName = 'TransferOrder';
                                break;
                        }
                    } else if ($control.hasClass('inventory-number')) {
                        datafield = 'InventoryId';
                        validationPeekFormName = 'RentalInventory';
                    } else {
                        datafield = 'DealId';
                        validationPeekFormName = 'Deal';
                    }
                    const title = $control.find('span').text();

                    FwValidation.validationPeek($control, validationPeekFormName, validationId, datafield, null, title);
                    //$validationbrowse.data('$control').find('.validation-loader').hide();
                    //$control.find('.btnpeek').show()
                    //})
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        //jQuery('#application')
        //    .off('click')
        //    .on('click', () => {
        //        if ($control.length) {
        //            $control.remove();
        //        }
        //    });
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
            $form.find('.wallsection').hide();
            $form.find('.optionssection').show();
            $form.find('.manufacturersection').show();
            $form.find('.settab').hide();

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
                $form.find('.settab').show();
                $form.find('.wallsection').hide();
                $form.find('.optionssection').hide();
                $form.find('.manufacturersection').hide();
                FwFormField.enable($form.find('div[data-datafield="TrackedBy"]'));
                $form.find('.tracked-by-column').hide();
                $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
            }
            if ($this.prop('checked') === true && $this.val() === 'W') {
                $form.find('.wallsection').show();
                $form.find('.optionssection').hide();
                $form.find('.manufacturersection').hide();
                $form.find('.settab').hide();
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
            $form.find('.setradio').hide();
            $form.find('.wallradio').hide();
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'N') {
            $form.find('.containertab').show();
            $form.find('.completeskitstab').hide();
            $form.find('.kitradio').hide();
            $form.find('.completeradio').hide();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.miscradio').hide();
            $form.find('.setradio').hide();
            $form.find('.wallradio').hide();
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
            $form.find('.setradio').hide();
            $form.find('.wallradio').hide();
            $form.find('.tracked-by-column').hide();
            $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);

            $form.find('.warehouse-pricing').hide();
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'K') {
            $form.find('.kittab').show();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.containerradio').hide();
            $form.find('.miscradio').hide();
            $form.find('.setradio').hide();
            $form.find('.wallradio').hide();
            $form.find('.tracked-by-column').hide();
            $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);

            $form.find('.warehouse-pricing').hide();
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'M') {
            $form.find('.completeradio').hide();
            $form.find('.kitradio').hide();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.containerradio').hide();
            $form.find('.setradio').hide();
            $form.find('.wallradio').hide();
            FwFormField.setValueByDataField($form, 'TrackedBy', 'QUANTITY');
            FwFormField.disable($form.find('div[data-datafield="TrackedBy"]'));
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'S') {
            $form.find('.completeradio').hide();
            $form.find('.kitradio').hide();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.miscradio').hide();
            $form.find('.containerradio').hide();
            $form.find('.tracked-by-column').hide();
            $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
        }


        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'W') {
            $form.find('.completeradio').hide();
            $form.find('.kitradio').hide();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.miscradio').hide();
            $form.find('.containerradio').hide();
            $form.find('.tracked-by-column').hide();
            $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
        }

    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
        const categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
        const controller = $form.attr('data-controller');
        request.uniqueids = {};
        switch (datafield) {
            case 'InventoryTypeId':
                request.uniqueids.HasCategories = true;
                if (controller === 'RentalInventoryController') {
                    request.uniqueids.Rental = true;
                }
                if (controller === 'SalesInventoryController') {
                    request.uniqueids.Sales = true;
                }
                if (controller === 'PartsInventoryController') {
                    request.uniqueids.Parts = true;
                }
                break;
            case 'CategoryId':
                if (inventoryTypeId) {
                    request.uniqueids.InventoryTypeId = inventoryTypeId;
                }
                break;
            case 'SubCategoryId':
                if (inventoryTypeId) {
                    request.uniqueids.InventoryTypeId = inventoryTypeId;
                }
                if (categoryId) {
                    request.uniqueids.CategoryId = categoryId;
                }
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        //Disable "Create Complete" if classification isn't Item or Accessory
        const classification = FwFormField.getValueByDataField($form, 'Classification');
        if (classification !== 'A' && classification !== 'I') {
            $form.find('.fwform-menu .submenu-btn').css({ 'pointer-events': 'none', 'color': 'lightgray' });
        }
        if ($form.find('[data-datafield="SubCategoryCount"] .fwformfield-value').val() > 0) {
            FwFormField.enable($form.find('[data-datafield="SubCategoryId"]'));
        } else {
            FwFormField.disable($form.find('[data-datafield="SubCategoryId"]'));
        }

        if ($form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'))
        } else {
            FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategoryId"]'))
        }

        if ($form.find('[data-datafield="InventoryTypeIsWardrobe"] .fwformfield-value').prop('checked') === true) {
            $form.find('.wardrobetab').show();
        }

        if ($form.find('[data-datafield="InventoryTypeIsSets"] .fwformfield-value').prop('checked') === true) {
            if (classification === 'W') {
                $form.find('.wallsection').show();
                $form.find('.optionssection').hide();
                $form.find('.manufacturersection').hide();
            }
            else if (classification === 'S') {
                $form.find('.settab').show();
                $form.find('.optionssection').hide();
                $form.find('.manufacturersection').hide();
            }
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
        $form.on('click', '[data-type="tab"][data-enabled!="false"]', e => {
            const $tab = jQuery(e.currentTarget);
            const tabPageId = $tab.attr('data-tabpageid');
            if ($tab.hasClass('audittab') == false) {
                const $gridControls = $form.find(`#${tabPageId} [data-type="Grid"]`);
                if (($tab.hasClass('tabGridsLoaded') === false) && $gridControls.length > 0) {
                    for (let i = 0; i < $gridControls.length; i++) {
                        try {
                            const $gridcontrol = jQuery($gridControls[i]);
                            FwBrowse.search($gridcontrol);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                }

                if ($tab.hasClass('tabGridsLoaded') === false) {
                    const submoduleName = $tab.attr('data-submodulename');
                    const controller = $form.attr('data-controller');
                    let $browseControl;
                    const $tabpage = $form.find(`#${tabPageId}`);
                    switch (submoduleName) {
                        case 'Asset':
                        case 'Repair':
                            if (controller == "SalesInventoryController" || controller == "RentalInventoryController") {
                                $browseControl = this.openSubModuleBrowse($form, submoduleName);
                                $tabpage.append($browseControl);
                                FwBrowse.search($browseControl);
                                //jQuery($browse).find('.ddviewbtn-caption:contains("Show:")').siblings('.ddviewbtn-select').find('.ddviewbtn-dropdown-btn:contains("All")').click();
                            }
                            break;
                        case 'Order':
                        case 'Invoice':
                        case 'TransferOrder':
                        case 'PurchaseOrder':
                        case 'PurchaseHistory':
                            $browseControl = this.openSubModuleBrowse($form, submoduleName);
                            $tabpage.append($browseControl);
                            FwBrowse.search($browseControl);
                            break;
                        case 'RetiredHistory':
                            $browseControl = this.openSubModuleBrowse($form, submoduleName);
                            $tabpage.append($browseControl);
                            FwBrowse.search($browseControl);
                            break;
                        default:
                            let $browseControls = $form.find(`#${tabPageId} [data-type="Browse"]`);
                            if ($browseControls.length > 0) {
                                for (let i = 0; i < $browseControls.length; i++) {
                                    const $browseControl = jQuery($browseControls[i]);
                                    FwBrowse.search($browseControl);
                                }
                            }
                            break;
                    }
                }
            }
            $tab.addClass('tabGridsLoaded');
        });

        //Enable/disable grid based on packageprice
        const packagePrice = FwFormField.getValueByDataField($form, 'PackagePrice');
        if (packagePrice == 'CP') {
            this.enablePricingFields($form);
        } else {
            FwBrowse.disableGrid($form.find('[data-grid="InventoryWarehouseCompletePricingGrid"]'));
            FwBrowse.disableGrid($form.find('[data-grid="InventoryWarehouseKitPricingGrid"]'));
        };
    }
    //----------------------------------------------------------------------------------------------
    //calculateYearly() {
    //    for (var jan = 0; jan <= 30; jan++) {
    //        this.yearlyEvents.push({
    //            start: moment('2018-04-02T00:00:00').add(jan, 'days').format('YYYY-MM-DD'),
    //            end: moment('2018-04-03T00:00:00').add(jan, 'days').format('YYYY-MM-DD'),
    //            realStart: "January " + (jan + 1),
    //            realEnd: "January " + (jan + 2),
    //            id: "1",
    //            resource: "A",
    //            text: "Available",
    //            backColor: "lime",
    //            orderNumber: "200002-024",
    //            orderStatus: "CONFIRMED",
    //            deal: "Testing"
    //        })
    //    }
    //    for (var feb = 0; feb < 28; feb++) {
    //        this.yearlyEvents.push({
    //            start: moment('2018-04-05T00:00:00').add(feb, 'days').format('YYYY-MM-DD'),
    //            end: moment('2018-04-06T00:00:00').add(feb, 'days').format('YYYY-MM-DD'),
    //            realStart: "February " + (feb + 1),
    //            realEnd: "February " + (feb + 2),
    //            id: "1",
    //            resource: "B",
    //            text: "Available",
    //            backColor: "lime",
    //            orderNumber: "200002-024",
    //            orderStatus: "CONFIRMED",
    //            deal: "Testing"
    //        })
    //    }
    //    for (var mar = 0; mar < 31; mar++) {
    //        this.yearlyEvents.push({
    //            start: moment('2018-04-05T00:00:00').add(mar, 'days').format('YYYY-MM-DD'),
    //            end: moment('2018-04-06T00:00:00').add(mar, 'days').format('YYYY-MM-DD'),
    //            realStart: "March " + (mar + 1),
    //            realEnd: "March " + (mar + 2),
    //            id: "1",
    //            resource: "C",
    //            text: "Available",
    //            backColor: "lime",
    //            orderNumber: "200002-024",
    //            orderStatus: "CONFIRMED",
    //            deal: "Testing"
    //        })
    //    }
    //    for (var May = 0; May < 31; May++) {
    //        this.yearlyEvents.push({
    //            start: moment('2018-04-03T00:00:00').add(May, 'days').format('YYYY-MM-DD'),
    //            end: moment('2018-04-04T00:00:00').add(May, 'days').format('YYYY-MM-DD'),
    //            realStart: "May " + (May + 1),
    //            realEnd: "May " + (May + 2),
    //            id: "1",
    //            resource: "E",
    //            text: "Available",
    //            backColor: "lime",
    //            orderNumber: "200002-024",
    //            orderStatus: "CONFIRMED",
    //            deal: "Testing"
    //        })
    //    }
    //    for (var June = 0; June < 30; June++) {
    //        this.yearlyEvents.push({
    //            start: moment('2018-04-06T00:00:00').add(June, 'days').format('YYYY-MM-DD'),
    //            end: moment('2018-04-07T00:00:00').add(June, 'days').format('YYYY-MM-DD'),
    //            realStart: "June " + (June + 1),
    //            realEnd: "June " + (June + 2),
    //            id: "1",
    //            resource: "F",
    //            text: "Available",
    //            backColor: "lime",
    //            orderNumber: "200002-024",
    //            orderStatus: "CONFIRMED",
    //            deal: "Testing"
    //        })
    //    }
    //    for (var July = 0; July < 31; July++) {
    //        this.yearlyEvents.push({
    //            start: moment('2018-04-01T00:00:00').add(July, 'days').format('YYYY-MM-DD'),
    //            end: moment('2018-04-02T00:00:00').add(July, 'days').format('YYYY-MM-DD'),
    //            realStart: "July " + (July + 1),
    //            realEnd: "July " + (July + 2),
    //            id: "1",
    //            resource: "G",
    //            text: "Available",
    //            backColor: "lime",
    //            orderNumber: "200002-024",
    //            orderStatus: "CONFIRMED",
    //            deal: "Testing"
    //        })
    //    }
    //    for (var August = 0; August < 30; August++) {
    //        this.yearlyEvents.push({
    //            start: moment('2018-04-01T00:00:00').add(August, 'days').format('YYYY-MM-DD'),
    //            end: moment('2018-04-02T00:00:00').add(August, 'days').format('YYYY-MM-DD'),
    //            realStart: "August " + (August + 1),
    //            realEnd: "August " + (August + 2),
    //            id: "1",
    //            resource: "H",
    //            text: "Available",
    //            backColor: "lime",
    //            orderNumber: "200002-024",
    //            orderStatus: "CONFIRMED",
    //            deal: "Testing"
    //        })
    //    }
    //    for (var September = 0; September < 31; September++) {
    //        this.yearlyEvents.push({
    //            start: moment('2018-04-07T00:00:00').add(September, 'days').format('YYYY-MM-DD'),
    //            end: moment('2018-04-08T00:00:00').add(September, 'days').format('YYYY-MM-DD'),
    //            realStart: "September " + (September + 1),
    //            realEnd: "September " + (September + 2),
    //            id: "1",
    //            resource: "I",
    //            text: "Available",
    //            backColor: "lime",
    //            orderNumber: "200002-024",
    //            orderStatus: "CONFIRMED",
    //            deal: "Testing"
    //        })
    //    }
    //    for (var October = 0; October < 31; October++) {
    //        this.yearlyEvents.push({
    //            start: moment('2018-04-02T00:00:00').add(October, 'days').format('YYYY-MM-DD'),
    //            end: moment('2018-04-03T00:00:00').add(October, 'days').format('YYYY-MM-DD'),
    //            realStart: "October " + (October + 1),
    //            realEnd: "October " + (October + 2),
    //            id: "1",
    //            resource: "J",
    //            text: "Available",
    //            backColor: "lime",
    //            orderNumber: "200002-024",
    //            orderStatus: "CONFIRMED",
    //            deal: "Testing"
    //        })
    //    }
    //    for (var November = 0; November < 30; November++) {
    //        this.yearlyEvents.push({
    //            start: moment('2018-04-05T00:00:00').add(November, 'days').format('YYYY-MM-DD'),
    //            end: moment('2018-04-06T00:00:00').add(November, 'days').format('YYYY-MM-DD'),
    //            realStart: "November " + (November + 1),
    //            realEnd: "November " + (November + 2),
    //            id: "1",
    //            resource: "K",
    //            text: "Available",
    //            backColor: "lime",
    //            orderNumber: "200002-024",
    //            orderStatus: "CONFIRMED",
    //            deal: "Testing"
    //        })
    //    }
    //    for (var December = 0; December < 31; December++) {
    //        this.yearlyEvents.push({
    //            start: moment('2018-04-07T00:00:00').add(December, 'days').format('YYYY-MM-DD'),
    //            end: moment('2018-04-08T00:00:00').add(December, 'days').format('YYYY-MM-DD'),
    //            realStart: "December " + (December + 1),
    //            realEnd: "December " + (December + 2),
    //            id: "1",
    //            resource: "L",
    //            text: "Available",
    //            backColor: "lime",
    //            orderNumber: "200002-024",
    //            orderStatus: "CONFIRMED",
    //            deal: "Testing"
    //        })
    //    }
    //}
    //yearlyEvents: any = [
    //    {
    //        start: "2018-04-01T00:00:00",
    //        end: "2018-04-02T00:00:00",

    //        id: "1",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-02T00:00:00",
    //        end: "2018-04-03T00:00:00",

    //        id: "2",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-03T00:00:00",
    //        end: "2018-04-04T00:00:00",

    //        id: "3",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-04T00:00:00",
    //        end: "2018-04-05T00:00:00",

    //        id: "3",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-05T00:00:00",
    //        end: "2018-04-06T00:00:00",

    //        id: "4",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-06T00:00:00",
    //        end: "2018-04-07T00:00:00",

    //        id: "1",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-07T00:00:00",
    //        end: "2018-04-08T00:00:00",

    //        id: "2",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-08T00:00:00",
    //        end: "2018-04-09T00:00:00",

    //        id: "1",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-09T00:00:00",
    //        end: "2018-04-10T00:00:00",

    //        id: "2",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-10T00:00:00",
    //        end: "2018-04-11T00:00:00",

    //        id: "1",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-11T00:00:00",
    //        end: "2018-04-12T00:00:00",

    //        id: "1",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-12T00:00:00",
    //        end: "2018-04-13T00:00:00",

    //        id: "2",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-13T00:00:00",
    //        end: "2018-04-14T00:00:00",

    //        id: "3",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-14T00:00:00",
    //        end: "2018-04-15T00:00:00",

    //        id: "3",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-15T00:00:00",
    //        end: "2018-04-16T00:00:00",

    //        id: "4",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-16T00:00:00",
    //        end: "2018-04-17T00:00:00",

    //        id: "1",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-17T00:00:00",
    //        end: "2018-04-18T00:00:00",

    //        id: "2",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-18T00:00:00",
    //        end: "2018-04-19T00:00:00",

    //        id: "1",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-19T00:00:00",
    //        end: "2018-04-20T00:00:00",

    //        id: "2",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-20T00:00:00",
    //        end: "2018-04-21T00:00:00",

    //        id: "1",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-21T00:00:00",
    //        end: "2018-04-22T00:00:00",

    //        id: "1",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-22T00:00:00",
    //        end: "2018-04-23T00:00:00",

    //        id: "2",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-23T00:00:00",
    //        end: "2018-04-24T00:00:00",

    //        id: "3",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-24T00:00:00",
    //        end: "2018-04-25T00:00:00",

    //        id: "3",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-25T00:00:00",
    //        end: "2018-04-26T00:00:00",

    //        id: "4",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-26T00:00:00",
    //        end: "2018-04-27T00:00:00",

    //        id: "1",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-27T00:00:00",
    //        end: "2018-04-28T00:00:00",

    //        id: "2",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-28T00:00:00",
    //        end: "2018-04-29T00:00:00",

    //        id: "1",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //    {
    //        start: "2018-04-29T00:00:00",
    //        end: "2018-04-30T00:00:00",

    //        id: "2",
    //        resource: "D",
    //        text: "Available",
    //        backColor: "lime",
    //        orderNumber: "200002-024",
    //        orderStatus: "CONFIRMED",
    //        deal: "Testing"
    //    },
    //]
}
//var InventoryBaseController = new InventoryBase();