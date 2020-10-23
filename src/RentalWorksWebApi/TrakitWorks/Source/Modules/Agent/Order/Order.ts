routes.push({ pattern: /^module\/order$/, action: function (match: RegExpExecArray) { return OrderController.getModuleScreen(); } });
routes.push({ pattern: /^module\/order\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return OrderController.getModuleScreen(filter); } });

class Order extends OrderBase {
    Module:               string = 'Order';
    apiurl:               string = 'api/v1/order';
    caption:              string = Constants.Modules.Agent.children.Order.caption;
    nav:                  string = Constants.Modules.Agent.children.Order.nav;
    id:                   string = Constants.Modules.Agent.children.Order.id;
    lossDamageSessionId:  string = '';
    successSoundFileName: string;
    errorSoundFileName:   string;
    ActiveViewFields:     any = {};
    ActiveViewFieldsId:   string;
    DefaultOrderType:     string;
    DefaultOrderTypeId: string;
    totalFields = ['WeeklyExtendedNoDiscount', 'WeeklyDiscountAmount', 'WeeklyExtended', 'WeeklyTax', 'WeeklyTotal', 'MonthlyExtendedNoDiscount', 'MonthlyDiscountAmount', 'MonthlyExtended', 'MonthlyTax', 'MonthlyTotal', 'PeriodExtendedNoDiscount', 'PeriodDiscountAmount', 'PeriodExtended', 'PeriodTax', 'PeriodTotal',]
    //-----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        const screen: any = {};
        screen.$view      = FwModule.getModuleControl(`${this.Module}Controller`);

        const $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Order', false, 'BROWSE', true);

            if (typeof filter !== 'undefined' && filter.datafield === 'agent') {
                filter.search = filter.search.split('%20').reverse().join(', ');
            }

            if (typeof filter !== 'undefined') {
                filter.datafield = filter.datafield.charAt(0).toUpperCase() + filter.datafield.slice(1);
                $browse.find(`div[data-browsedatafield="${filter.datafield}"]`).find('input').val(filter.search);
            }

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
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse     = FwModule.openBrowse($browse);

        FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
            if (dt.Rows[rowIndex][dt.ColumnIndex['Status']] === 'CANCELLED') {
                $tr.css('color', '#aaaaaa');
            }
        });

        $browse.data('ondatabind', request =>  {
            request.activeviewfields = this.ActiveViewFields;
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

        const department = JSON.parse(sessionStorage.getItem('department'));;
        const location   = JSON.parse(sessionStorage.getItem('location'));;

        FwAppData.apiMethod(true, 'GET', `api/v1/departmentlocation/${department.departmentid}~${location.locationid}`, null, FwServices.defaultTimeout, response => {
            this.DefaultOrderType   = response.DefaultOrderType;
            this.DefaultOrderTypeId = response.DefaultOrderTypeId;
        }, null, null);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    //addBrowseMenuItems(options: IAddBrowseMenuOptions) {
    //    const $all       = FwMenu.generateDropDownViewBtn('All', true, "ALL");
    //    const $confirmed = FwMenu.generateDropDownViewBtn('Confirmed', false, "CONFIRMED");
    //    const $active    = FwMenu.generateDropDownViewBtn('Active', false, "ACTIVE");
    //    const $hold      = FwMenu.generateDropDownViewBtn('Hold', false, "HOLD");
    //    const $complete  = FwMenu.generateDropDownViewBtn('Complete', false, "COMPLETE");
    //    const $cancelled = FwMenu.generateDropDownViewBtn('Cancelled', false, "CANCELLED");
    //    const $closed    = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");
      
    //    let viewSubitems: Array<JQuery> = [];
    //    viewSubitems.push($all, $confirmed, $active, $hold, $complete, $cancelled, $closed);
    //    FwMenu.addViewBtn(options.$menu, 'View', viewSubitems, true, "Status");

    //    //Location Filter
    //    const location      = JSON.parse(sessionStorage.getItem('location'));
    //    const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
    //    const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

    //    if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
    //        this.ActiveViewFields.LocationId = [location.locationid];
    //    }

    //    let viewLocation: Array<JQuery> = [];
    //    viewLocation.push($userLocation, $allLocations);
    //    FwMenu.addViewBtn(options.$menu, 'Location', viewLocation, true, "LocationId");
    //    return options;
    //}
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasDelete = false;
        FwMenu.addBrowseMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel / Uncancel', 'dpH0uCuEp3E89', (e: JQuery.ClickEvent) => {
            try {
                this.browseCancelOption(options.$browse);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        // View DropDownMenu
        const $all = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $confirmed = FwMenu.generateDropDownViewBtn('Confirmed', false, "CONFIRMED");
        const $active = FwMenu.generateDropDownViewBtn('Active', false, "ACTIVE");
        const $hold = FwMenu.generateDropDownViewBtn('Hold', false, "HOLD");
        const $complete = FwMenu.generateDropDownViewBtn('Complete', false, "COMPLETE");
        const $cancelled = FwMenu.generateDropDownViewBtn('Cancelled', false, "CANCELLED");
        const $closed = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");

        let viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $confirmed, $active, $hold, $complete, $cancelled, $closed);
        FwMenu.addViewBtn(options.$menu, 'View', viewSubitems, true, "Status");

        //Location Filter
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        const viewLocation: Array<JQuery> = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn(options.$menu, 'Location', viewLocation, true, "LocationId");
    }
    //-----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Copy Order', 'Y2TpHAfLr0W8', (e: JQuery.ClickEvent) => {
            try {
                this.copyOrderOrQuote(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Print Order', 'qshFakNJL1Aw', (e: JQuery.ClickEvent) => {
            try {
                this.printQuoteOrder(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        //FwMenu.addSubMenuItem(options.$groupOptions, 'Search', '', (e: JQuery.ClickEvent) => {
        //    try {
        //        this.search(options.$form);
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});

        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel / Uncancel', 'AYsdyaJ8KtGl', (e: JQuery.ClickEvent) => {
            try {
                this.cancelUncancelOrder(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        //no-security
        FwMenu.addSubMenuItem(options.$groupOptions, 'Create Pick List', '', (e: JQuery.ClickEvent) => {
            try {
                this.createPickList(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        //FwMenu.addSubMenuItem(options.$groupOptions, 'Create Snapshot', '1nNrrlUbFv4I', (e: JQuery.ClickEvent) => {
        //    try {
        //        this.createSnapshotOrder(options.$form);
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});
        ////no-security
        //FwMenu.addSubMenuItem(options.$groupOptions, 'View Snapshot', '', (e: JQuery.ClickEvent) => {
        //    try {
        //        this.viewSnapshotOrder(options.$form);
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});
        //FwMenu.addSubMenuItem(options.$groupOptions, 'On Hold', 'cxjzEFBumSaY', (e: JQuery.ClickEvent) => {
        //    try {
        //        this.OrderOnHold(options.$form);
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});
        FwMenu.addSubMenuItem(options.$groupOptions, 'Order Status', 'ddU6D9wE0E8Q', (e: JQuery.ClickEvent) => {
            try {
                this.orderStatus(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Check Out', '2EdnWXXf5S2b', (e: JQuery.ClickEvent) => {
            try {
                this.checkOut(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Check In', '6OkDTK6ILron', (e: JQuery.ClickEvent) => {
            try {
                this.checkIn(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        //FwMenu.addSubMenuItem(options.$groupOptions, 'Change Office Location', 'zXHF8zSQywct', (e: JQuery.ClickEvent) => {
        //    try {
        //        this.changeOfficeLocation(options.$form);
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});
    }
    //------------------------------------------------------------------------------------------
    openForm(mode: string, parentModuleInfo?: any) {
        var $form = FwModule.loadFormFromTemplate(this.Module);
        $form     = FwModule.openForm($form, mode);

        const $submodulePickListBrowse = this.openPickListBrowse($form);
        $form.find('.picklist').append($submodulePickListBrowse);

        const $submoduleContractBrowse = this.openContractBrowse($form);
        $form.find('.contract').append($submoduleContractBrowse);

        //let $submodulePurchaseOrderBrowse = this.openPurchaseOrderBrowse($form);
        //$form.find('.subPurchaseOrderSubModule').append($submodulePurchaseOrderBrowse);

        FwFormField.loadItems($form.find('div[data-datafield="weightselector"]'), [
            { value: 'IMPERIAL', caption: 'Imperial', checked: true},
            { value: 'METRIC',   caption: 'Metric' }
        ]);

        FwFormField.loadItems($form.find('div[data-datafield="OutDeliveryDeliveryType"]'), [
            { value: 'DELIVER', text: 'Deliver to Customer' },
            { value: 'SHIP',    text: 'Ship to Customer' },
            { value: 'PICK UP', text: 'Customer Pick Up' }
        ], true);

        FwFormField.loadItems($form.find('div[data-datafield="InDeliveryDeliveryType"]'), [
            { value: 'DELIVER', text: 'Customer Deliver' },
            { value: 'SHIP',    text: 'Customer Ship' },
            { value: 'PICK UP', text: 'Pick Up from Customer' }
        ], true);

        var deliverAddressTypes = [{ value: 'DEAL',      caption: 'Job' },
                                   { value: 'VENUE',     caption: 'Venue' },
                                   { value: 'WAREHOUSE', caption: 'Warehouse' },
                                   { value: 'OTHER',     caption: 'Other' }];
        FwFormField.loadItems($form.find('div[data-datafield="OutDeliveryAddressType"]'), deliverAddressTypes);
        FwFormField.loadItems($form.find('div[data-datafield="InDeliveryAddressType"]'), deliverAddressTypes);

        if (typeof parentModuleInfo !== 'undefined') {
            FwFormField.setValue($form, 'div[data-datafield="DealId"]', parentModuleInfo.DealId, parentModuleInfo.Deal);
            //FwFormField.setValue($form, 'div[data-datafield="RateType"]', parentModuleInfo.RateTypeId, parentModuleInfo.RateType);
        }

        const $orderItemGridLossDamage = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');
        $orderItemGridLossDamage.find('.submenu-btn').filter('[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"], [data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        $orderItemGridLossDamage.find('.buttonbar').hide();

        this.events($form);

        if (mode === 'NEW') {
            const today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'PickDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStartDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStopDate', today);

            const usersid    = sessionStorage.getItem('usersid');
            const name       = sessionStorage.getItem('name');
            const department = JSON.parse(sessionStorage.getItem('department'));
            const office     = JSON.parse(sessionStorage.getItem('location'));
            const warehouse  = JSON.parse(sessionStorage.getItem('warehouse'));
            FwFormField.setValue($form, 'div[data-datafield="AgentId"]', usersid, name);
            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
            FwFormField.setValue($form, 'div[data-datafield="Rental"]', true, '', true);
            FwFormField.setValue($form, 'div[data-datafield="OrderTypeId"]', this.DefaultOrderTypeId, this.DefaultOrderType);
        };

        this.renderPrintButton($form);
        this.renderSearchButton($form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    renderPrintButton($form: any) {
        var $print = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'Print');
        $print.prepend('<i class="material-icons">print</i>');
        $print.on('click', function () {
            try {
                var $form       = jQuery(this).closest('.fwform');
                var orderNumber = FwFormField.getValue($form, 'div[data-datafield="OrderNumber"]');
                var orderId     = FwFormField.getValue($form, 'div[data-datafield="OrderId"]');
                var $report     = OrderReportController.openForm();

                FwModule.openSubModuleTab($form, $report);

                FwFormField.setValue($report, 'div[data-datafield="OrderId"]', orderId, orderNumber);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderSearchButton($form: any) {
        var self = this;
        var $search = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'QuikSearch', 'searchbtn');
        $search.prepend('<i class="material-icons">search</i>');
        $search.on('click', function () {
            try {
                let $form   = jQuery(this).closest('.fwform');
                let orderId = FwFormField.getValueByDataField($form, 'OrderId');
                
                if (orderId == "") {
                    FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
                } else if (!jQuery(this).hasClass('disabled')) {
                    let search = new SearchInterface();
                    search.renderSearchPopup($form, orderId, self.Module);
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids) {
        const $form = this.openForm('EDIT', uniqueids);
        $form.find('div.fwformfield[data-datafield="OrderId"] input').val(uniqueids.OrderId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    openPickListBrowse($form) {
        const $browse = PickListController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = PickListController.ActiveViewFields;
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId')
            }
        });

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openContractBrowse($form) {
        const $browse = ContractController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = ContractController.ActiveViewFields;
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId')
            }
        });

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    //openPurchaseOrderBrowse($form) {
    //    const $browse = PurchaseOrderController.openBrowse();
    //    $browse.data('ondatabind', function (request) {
    //        request.activeviewfields = PurchaseOrderController.ActiveViewFields;
    //        request.uniqueids = {
    //            OrderId: FwFormField.getValueByDataField($form, 'OrderId')
    //        };
    //    });

    //    return $browse;
    //}
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        // ------
        let $orderItemGridRental: JQuery, $orderItemGridSales: JQuery, $orderItemGridLabor: JQuery, $orderItemGridMisc: JQuery, $orderItemGridUsedSale: JQuery, $combinedOrderItemGrid: JQuery;
        // ----------
        //const $orderStatusHistoryGrid = $form.find('div[data-grid="OrderStatusHistoryGrid"]');
        //const $orderStatusHistoryGridControl = FwBrowse.loadGridFromTemplate('OrderStatusHistoryGrid');
        //$orderStatusHistoryGrid.empty().append($orderStatusHistoryGridControl);
        //$orderStatusHistoryGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'OrderId')
        //    };
        //});
        //FwBrowse.init($orderStatusHistoryGridControl);
        //FwBrowse.renderRuntimeHtml($orderStatusHistoryGridControl);
        FwBrowse.renderGrid({
            nameGrid: 'OrderStatusHistoryGrid',
            gridSecurityId: 'lATsdnAx7B4s',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
                };
            }
        });
        // ----------
        //const $orderSnapshotGrid = $form.find('div[data-grid="OrderSnapshotGrid"]');
        //const $orderSnapshotGridControl = FwBrowse.loadGridFromTemplate('OrderSnapshotGrid');
        //$orderSnapshotGrid.empty().append($orderSnapshotGridControl);
        //$orderSnapshotGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'OrderId')
        //    };
        //});
        //FwBrowse.init($orderSnapshotGridControl);
        //FwBrowse.renderRuntimeHtml($orderSnapshotGridControl);
        // ----------
        //const $orderItemGridRental = $form.find('.rentalgrid div[data-grid="OrderItemGrid"]');
        //const $orderItemGridRentalControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        //$orderItemGridRental.empty().append($orderItemGridRentalControl);
        //$orderItemGridRentalControl.data('isSummary', false);
        //$orderItemGridRental.addClass('R');

        //$orderItemGridRentalControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
        //        RecType: 'R'
        //    };
        //    request.totalfields = totalFields;
        //});
        //$orderItemGridRentalControl.data('beforesave', function (request) {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        //    request.RecType = 'R';
        //}
        //);
        //FwBrowse.addEventHandler($orderItemGridRentalControl, 'afterdatabindcallback', ($control, dt) => {
        //    let rentalItems = $form.find('.rentalgrid tbody').children();
        //    rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
        //});

        //FwBrowse.init($orderItemGridRentalControl);
        //FwBrowse.renderRuntimeHtml($orderItemGridRentalControl);
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.rentalgrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`),
                    RecType: 'R'
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
                request.RecType = 'R';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('R');
                $browse.data('isSummary', false);
                $orderItemGridRental = $fwgrid;
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                let rentalItems = $form.find('.rentalgrid tbody').children();
                rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
            }
        });

        // ----------
        //const $orderItemGridLossDamage = $form.find('.lossdamagegrid div[data-grid="OrderItemGrid"]');
        //const $orderItemGridLossDamageControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        //$orderItemGridLossDamage.empty().append($orderItemGridLossDamageControl);
        //$orderItemGridLossDamageControl.data('isSummary', false);
        //$orderItemGridLossDamage.addClass('F');
        //$orderItemGridLossDamage.find('div[data-datafield="InventoryId"]').attr('data-formreadonly', 'true'); 
        //$orderItemGridLossDamage.find('div[data-datafield="Description"]').attr('data-formreadonly', 'true');
        //$orderItemGridLossDamage.find('div[data-datafield="ItemId"]').attr('data-formreadonly', 'true');
        //$orderItemGridLossDamage.find('div[data-datafield="Price"]').attr('data-digits', '3'); 
        //$orderItemGridLossDamage.find('div[data-datafield="Price"]').attr('data-digitsoptional', 'false'); 

        //$orderItemGridLossDamageControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
        //        RecType: 'F'
        //    };
        //    request.totalfields = totalFields;
        //});
        //$orderItemGridLossDamageControl.data('beforesave', function (request) {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        //    request.RecType = 'F';
        //}
        //);
        //FwBrowse.addEventHandler($orderItemGridLossDamageControl, 'afterdatabindcallback', ($control, dt) => {
        //    let lossDamageItems = $form.find('.lossdamagegrid tbody').children();
        //    lossDamageItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="LossAndDamage"]')) : FwFormField.enable($form.find('[data-datafield="LossAndDamage"]'));
        //});

        //FwBrowse.init($orderItemGridLossDamageControl);
        //FwBrowse.renderRuntimeHtml($orderItemGridLossDamageControl);
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.lossdamagegrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb', 
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                    RecType: 'F'
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
                request.RecType = 'F';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('F');
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                let lossDamageItems = $form.find('.lossdamagegrid tbody').children();
                lossDamageItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="LossAndDamage"]')) : FwFormField.enable($form.find('[data-datafield="LossAndDamage"]'));
            }
        });
        // ----------
        //const $orderNoteGrid = $form.find('div[data-grid="OrderNoteGrid"]');
        //const $orderNoteGridControl = FwBrowse.loadGridFromTemplate('OrderNoteGrid');
        //$orderNoteGrid.empty().append($orderNoteGridControl);
        //$orderNoteGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'OrderId')
        //    };
        //});
        //$orderNoteGridControl.data('beforesave', function (request) {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'OrderId')
        //});
        //FwBrowse.init($orderNoteGridControl);
        //FwBrowse.renderRuntimeHtml($orderNoteGridControl);
        FwBrowse.renderGrid({
            nameGrid: 'OrderNoteGrid',
            gridSecurityId: '8aq0E3nK2upt',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {

            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
                };
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
            }
        });
        // ----------
        //const $orderContactGrid = $form.find('div[data-grid="OrderContactGrid"]');
        //const $orderContactGridControl = FwBrowse.loadGridFromTemplate('OrderContactGrid');
        //$orderContactGrid.empty().append($orderContactGridControl);
        //$orderContactGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'OrderId')
        //    };
        //});
        //$orderContactGridControl.data('beforesave', function (request) {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        //    request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
        //});
        //FwBrowse.init($orderContactGridControl);
        //FwBrowse.renderRuntimeHtml($orderContactGridControl);
        FwBrowse.renderGrid({
            nameGrid: 'OrderContactGrid',
            gridSecurityId: '7CUe9WvpWNat',
            moduleSecurityId: this.id,

            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {

            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
                };
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
                request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
            }
        });

        const itemGrids = [$orderItemGridRental];
        if ($form.attr('data-mode') === 'NEW') {
            for (let i = 0; i < itemGrids.length; i++) {
                itemGrids[i].find('.btn').filter(function () { return jQuery(this).data('type') === 'NewButton' })
                    .off()
                    .on('click', () => {
                        this.saveForm($form, { closetab: false });
                    })
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    browseCancelOption($browse: JQuery) {
        try {
            let $confirmation, $yes, $no;
            let orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
            let orderStatus = $browse.find('.selected [data-formdatafield="Status"]').attr('data-originalvalue');
            if (orderId != null) {
                if (orderStatus === "CANCELLED") {
                    $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                    $confirmation.find('.fwconfirmationbox').css('width', '450px');
                    let html = [];
                    html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                    html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                    html.push('    <div>Would you like to un-cancel this Order?</div>');
                    html.push('  </div>');
                    html.push('</div>');

                    FwConfirmation.addControls($confirmation, html.join(''));
                    $yes = FwConfirmation.addButton($confirmation, 'Un-Cancel Order', false);
                    $no = FwConfirmation.addButton($confirmation, 'Cancel');

                    $yes.on('click', uncancelOrder);
                }
                else {
                    $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                    $confirmation.find('.fwconfirmationbox').css('width', '450px');
                    let html = [];
                    html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                    html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                    html.push('    <div>Would you like to cancel this Order?</div>');
                    html.push('  </div>');
                    html.push('</div>');

                    FwConfirmation.addControls($confirmation, html.join(''));
                    $yes = FwConfirmation.addButton($confirmation, 'Cancel Order', false);
                    $no = FwConfirmation.addButton($confirmation, 'Cancel');

                    $yes.on('click', cancelOrder);
                }

                function cancelOrder() {
                    let request: any = {};

                    FwFormField.disable($confirmation.find('.fwformfield'));
                    FwFormField.disable($yes);
                    $yes.text('Canceling...');
                    $yes.off('click');

                    FwAppData.apiMethod(true, 'POST', `api/v1/order/cancel/${orderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        FwNotification.renderNotification('SUCCESS', 'Order Successfully Cancelled');
                        FwConfirmation.destroyConfirmation($confirmation);
                        FwBrowse.databind($browse);
                    }, function onError(response) {
                        $yes.on('click', cancelOrder);
                        $yes.text('Cancel');
                        FwFunc.showError(response);
                        FwFormField.enable($confirmation.find('.fwformfield'));
                        FwFormField.enable($yes);
                        FwBrowse.databind($browse);
                    }, $browse);
                };

                function uncancelOrder() {
                    let request: any = {};

                    FwFormField.disable($confirmation.find('.fwformfield'));
                    FwFormField.disable($yes);
                    $yes.text('Retrieving...');
                    $yes.off('click');

                    FwAppData.apiMethod(true, 'POST', `api/v1/order/uncancel/${orderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        FwNotification.renderNotification('SUCCESS', 'Order Successfully Retrieved');
                        FwConfirmation.destroyConfirmation($confirmation);
                        FwBrowse.databind($browse);
                    }, function onError(response) {
                        $yes.on('click', uncancelOrder);
                        $yes.text('Cancel');
                        FwFunc.showError(response);
                        FwFormField.enable($confirmation.find('.fwformfield'));
                        FwFormField.enable($yes);
                        FwBrowse.databind($browse);
                    }, $browse);
                };
            } else {
                FwNotification.renderNotification('WARNING', 'Select an Order to perform this action.');
            }
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    cancelUncancelOrder($form: any) {
        let $confirmation, $yes, $no;
        const module = this.Module;
        const id = FwFormField.getValueByDataField($form, `${module}Id`);
        const orderStatus = FwFormField.getValueByDataField($form, 'Status');

        if (id != null) {
            if (orderStatus === "CANCELLED") {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                let html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push(`    <div>Would you like to un-cancel this ${module}?</div>`);
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, `Un-Cancel ${module}`, false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', uncancelOrder);
            }
            else {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                let html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push(`    <div>Would you like to cancel this ${module}?</div>`);
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, `Cancel ${module}`, false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', cancelOrder);
            }
        }
        else {
            if (module === 'Order') {
                FwNotification.renderNotification('WARNING', 'Select an Order to perform this action.');
            } else if (module === 'Quote') {
                FwNotification.renderNotification('WARNING', 'Select a Quote to perform this action.');
            }
        }

        function cancelOrder() {
            let request: any = {};

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Canceling...');
            $yes.off('click');
            const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
            const realConfirm = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);

            FwAppData.apiMethod(true, 'POST', `api/v1/${module}/cancel/${id}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', `${module} Successfully Cancelled`);
                FwConfirmation.destroyConfirmation($confirmation);
                FwModule.refreshForm($form);
            }, function onError(response) {
                $yes.on('click', cancelOrder);
                $yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwModule.refreshForm($form);
            }, realConfirm);
        };

        function uncancelOrder() {
            let request: any = {};

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Retrieving...');
            $yes.off('click');

            FwAppData.apiMethod(true, 'POST', `api/v1/${module}/uncancel/${id}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', `${module} Successfully Retrieved`);
                FwConfirmation.destroyConfirmation($confirmation);
                FwModule.refreshForm($form);
            }, function onError(response) {
                $yes.on('click', uncancelOrder);
                $yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwModule.refreshForm($form);
            }, $form);
        };
    };
    //---------------------------------------------------------------------------------
    orderStatus($form: JQuery) {
        let mode = 'EDIT';
        let orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        orderInfo.OrderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
        let $orderStatusForm = OrderStatusController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $orderStatusForm);
        jQuery('.tab.submodule.active').find('.caption').html('Order Status');
    }
    //----------------------------------------------------------------------------------------------
    checkOut($form: JQuery) {
        let mode = 'EDIT';
        let orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        orderInfo.OrderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
        orderInfo.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
        orderInfo.Warehouse = $form.find('div[data-datafield="WarehouseId"] input.fwformfield-text').val();
        orderInfo.DealId = FwFormField.getValueByDataField($form, 'DealId');
        orderInfo.Deal = $form.find('div[data-datafield="DealId"] input.fwformfield-text').val();
        let $stagingCheckoutForm = StagingCheckoutController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $stagingCheckoutForm);
        jQuery('.tab.submodule.active').find('.caption').html('Staging / Check-Out');
    }
    //----------------------------------------------------------------------------------------------
    checkIn($form: JQuery) {
        let mode = 'EDIT';
        let orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        orderInfo.OrderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
        let $checkinForm = CheckInController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $checkinForm);
        jQuery('.tab.submodule.active').find('.caption').html('Check-In');
    }
    //----------------------------------------------------------------------------------------------
    createPickList($form: JQuery) {
        let mode = 'EDIT';
        let orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        let $pickListForm = CreatePickListController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $pickListForm);
        //jQuery('.tab.submodule.active').find('.caption').html('New Pick List');
        jQuery('.tab.submodule.active[data-tabtype="FORM"]').find('.caption').html('New Pick List');  //justin 09/16/2019 added data-tabtype="FORM" to target the top-level form tab, not the tab page on the Order form
        var $pickListUtilityGrid;
        $pickListUtilityGrid = $pickListForm.find('[data-name="PickListUtilityGrid"]');
        FwBrowse.search($pickListUtilityGrid);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        let status        = FwFormField.getValueByDataField($form, 'Status');
        let hasNotes      = FwFormField.getValueByDataField($form, 'HasNotes');
        let rentalTab     = $form.find('.rentaltab'),
            lossDamageTab = $form.find('.lossdamagetab');

        this.dynamicColumns($form);

        if (status === 'CLOSED' || status === 'CANCELLED' || status === 'SNAPSHOT') {
            FwModule.setFormReadOnly($form);
            $form.find('.btn[data-securityid="searchbtn"]').addClass('disabled');
        }

        if (hasNotes) {
            FwTabs.setTabColor($form.find('.notestab'), '#FFFF00');
        }

        // Hides Loss and Damage menu item from non-LD grids
        var $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        $orderItemGridRental.find('.submenu-btn').filter('[data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();

        if (FwFormField.getValueByDataField($form, 'Rental') === true) {
            rentalTab.show();
            FwFormField.disable($form.find('[data-datafield="LossAndDamage"]'));
        } else {
            rentalTab.hide();
            FwFormField.enable($form.find('[data-datafield="LossAndDamage"]'));
        }
        if (FwFormField.getValueByDataField($form, 'LossAndDamage') === true) {
            lossDamageTab.show();
            FwFormField.disable($form.find('[data-datafield="Rental"]'));
        } else {
            lossDamageTab.hide();
            FwFormField.enable($form.find('[data-datafield="Rental"]'));
        }

        if (FwFormField.getValueByDataField($form, 'HasRentalItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Rental'));
        }
        if (FwFormField.getValueByDataField($form, 'HasLossAndDamageItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'LossAndDamage'));
        }

        let orderid = FwFormField.getValue($form, 'div[data-datafield="OrderId"]');
        FwAppData.apiMethod(true, 'GET', `api/v1/ordersummary/${orderid}`, null, FwServices.defaultTimeout, function onSuccess(response) {
            FwFormField.setValue($form, 'div[data-datafield="ValueTotal"]', response.ValueTotal);
            FwFormField.setValue($form, 'div[data-datafield="ReplacementCostTotal"]', response.ReplacementCostTotal);
            FwFormField.setValue($form, 'div[data-datafield="WeightPounds"]', response.WeightPounds);
            FwFormField.setValue($form, 'div[data-datafield="WeightOunces"]', response.WeightOunces);
        }, null, $form);
    }
    //----------------------------------------------------------------------------------------------
    events($form: any) {
        //Defaults Address information when user selects a deal
        $form.find('[data-datafield="DealId"]').data('onchange', $tr => {
            const dealid = FwFormField.getValueByDataField($form, 'DealId');
            FwFormField.setValue($form, 'div[data-datafield="DealNumber"]', $tr.find('.field[data-browsedatafield="DealNumber"]').attr('data-originalvalue'));   

            if ($form.attr('data-mode') === 'NEW') {
                FwAppData.apiMethod(true, 'GET', `api/v1/deal/${dealid}`, null, FwServices.defaultTimeout, response => {
                    FwFormField.setValueByDataField($form, 'OutDeliveryDeliveryType', response.DefaultOutgoingDeliveryType);
                    FwFormField.setValueByDataField($form, 'InDeliveryDeliveryType', response.DefaultIncomingDeliveryType);
                    if (response.DefaultOutgoingDeliveryType === 'DELIVER' || response.DefaultOutgoingDeliveryType === 'SHIP') {
                        FwFormField.setValueByDataField($form, 'OutDeliveryAddressType', 'DEAL');
                        this.setDealAddress($form, 'Out', response);
                    } else if (response.DefaultOutgoingDeliveryType === 'PICK UP') {
                        FwFormField.setValueByDataField($form, 'OutDeliveryAddressType', 'WAREHOUSE');
                        this.setWarehouseAddress($form, 'Out');
                    }

                    if (response.DefaultIncomingDeliveryType === 'DELIVER' || response.DefaultIncomingDeliveryType === 'SHIP') {
                        FwFormField.setValueByDataField($form, 'InDeliveryAddressType', 'WAREHOUSE');
                        this.setWarehouseAddress($form, 'In');
                    } else if (response.DefaultIncomingDeliveryType === 'PICK UP') {
                        FwFormField.setValueByDataField($form, 'InDeliveryAddressType', 'DEAL');
                        this.setDealAddress($form, 'In', response);
                    }
                }, null, null);
            }
        });

        $form
            .on('click', '[data-type="tab"]', event => {
                if ($form.attr('data-mode') !== 'NEW') {
                    const $tab      = jQuery(event.currentTarget);
                    const tabpageid = jQuery(event.currentTarget).data('tabpageid');

                    if ($tab.hasClass('audittab') == false) {
                        const $gridControls = $form.find(`#${tabpageid} [data-type="Grid"]`);
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

                        const $browseControls = $form.find(`#${tabpageid} [data-type="Browse"]`);
                        if (($tab.hasClass('tabGridsLoaded') === false) && $browseControls.length > 0) {
                            for (let i = 0; i < $browseControls.length; i++) {
                                const $browseControl = jQuery($browseControls[i]);
                                FwBrowse.search($browseControl);
                            }
                        }
                    }
                    $tab.addClass('tabGridsLoaded');
                }
            })
            .on('change', 'div[data-datafield="PickDate"], div[data-datafield="EstimatedStartDate"], div[data-datafield="EstimatedStopDate"]', event => {
                var $element           = jQuery(event.currentTarget);
                var PickDate           = Date.parse(FwFormField.getValueByDataField($form, 'PickDate'));
                var EstimatedStartDate = Date.parse(FwFormField.getValueByDataField($form, 'EstimatedStartDate'));
                var EstimatedStopDate  = Date.parse(FwFormField.getValueByDataField($form, 'EstimatedStopDate'));

                if ($element.attr('data-datafield') === 'EstimatedStartDate' && EstimatedStartDate < PickDate) {
                    $form.find('div[data-datafield="EstimatedStartDate"]').addClass('error');
                    FwNotification.renderNotification('WARNING', "Your chosen 'From Date' is before 'Pick Date'.");
                } else if ($element.attr('data-datafield') === 'PickDate' && EstimatedStartDate < PickDate) {
                    $form.find('div[data-datafield="PickDate"]').addClass('error');
                    FwNotification.renderNotification('WARNING', "Your chosen 'Pick Date' is after 'From Date'.");
                } else if ($element.attr('data-datafield') === 'PickDate' && EstimatedStopDate < PickDate) {
                    $form.find('div[data-datafield="PickDate"]').addClass('error');
                    FwNotification.renderNotification('WARNING', "Your chosen 'Pick Date' is after 'To Date'.");
                } else if (EstimatedStopDate < EstimatedStartDate) {
                    $form.find('div[data-datafield="EstimatedStopDate"]').addClass('error');
                    FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'From Date'.");
                } else if (EstimatedStopDate < PickDate) {
                    $form.find('div[data-datafield="EstimatedStopDate"]').addClass('error');
                    FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'Pick Date'.");
                } else {
                    $form.find('div[data-datafield="PickDate"]').removeClass('error');
                    $form.find('div[data-datafield="EstimatedStartDate"]').removeClass('error');
                    $form.find('div[data-datafield="EstimatedStopDate"]').removeClass('error');
                }
            })
            .on('change', 'div[data-datafield="Rental"]', event => {
                var rentalTab = $form.find('.rentaltab');
                if (FwFormField.getValue($form, 'div[data-datafield="Rental"]') === true) {
                    rentalTab.show();
                    FwFormField.disable($form.find('[data-datafield="LossAndDamage"]'));
                } else {
                    rentalTab.hide();
                    FwFormField.enable($form.find('[data-datafield="LossAndDamage"]'));
                }
            })
            .on('change', 'div[data-datafield="LossAndDamage"]', event => {
                var lossDamageTab = $form.find('.lossdamagetab');
                if (FwFormField.getValue($form, 'div[data-datafield="LossAndDamage"]') === true) {
                    lossDamageTab.show();
                    FwFormField.disable($form.find('[data-datafield="Rental"]'));
                } else {
                    lossDamageTab.hide();
                    FwFormField.enable($form.find('[data-datafield="Rental"]'));
                }
            })
            .on('click', '.addresscopy', event => {
                var $confirmation = FwConfirmation.renderConfirmation('Confirm Copy', 'Copy Outgoing Address into Incoming Address?');
                var $yes          = FwConfirmation.addButton($confirmation, 'Copy', true);
                var $no           = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', function () {
                    FwFormField.setValueByDataField($form, 'InDeliveryToLocation', FwFormField.getValueByDataField($form, 'OutDeliveryToLocation'));
                    FwFormField.setValueByDataField($form, 'InDeliveryToAttention', FwFormField.getValueByDataField($form, 'OutDeliveryToAttention'));
                    FwFormField.setValueByDataField($form, 'InDeliveryToAddress1', FwFormField.getValueByDataField($form, 'OutDeliveryToAddress1'));
                    FwFormField.setValueByDataField($form, 'InDeliveryToAddress2', FwFormField.getValueByDataField($form, 'OutDeliveryToAddress2'));
                    FwFormField.setValueByDataField($form, 'InDeliveryToCity', FwFormField.getValueByDataField($form, 'OutDeliveryToCity'));
                    FwFormField.setValueByDataField($form, 'InDeliveryToState', FwFormField.getValueByDataField($form, 'OutDeliveryToState'));
                    FwFormField.setValueByDataField($form, 'InDeliveryToZipCode', FwFormField.getValueByDataField($form, 'OutDeliveryToZipCode'));
                    FwFormField.setValueByDataField($form, 'InDeliveryToCountryId', FwFormField.getValueByDataField($form, 'OutDeliveryToCountryId'), FwFormField.getTextByDataField($form, 'OutDeliveryToCountryId'));
                    FwFormField.setValueByDataField($form, 'InDeliveryToCrossStreets', FwFormField.getValueByDataField($form, 'OutDeliveryToCrossStreets'), '', true);
                    FwNotification.renderNotification('SUCCESS', 'Address Successfully Copied.');
                });
            })
            .on('change', 'div[data-datafield="OutDeliveryAddressType"], div[data-datafield="InDeliveryAddressType"]', event => {
                let $element = jQuery(event.currentTarget);
                if ($element.attr('data-datafield') === 'OutDeliveryAddressType') {
                    let value = FwFormField.getValueByDataField($form, 'OutDeliveryAddressType');
                    if (value === 'WAREHOUSE') {
                        this.setWarehouseAddress($form, 'Out');
                    } else if (value === 'DEAL') {
                        this.setDealAddress($form, 'Out');
                    }
                } else if ($element.attr('data-datafield') === 'InDeliveryAddressType') {
                    let value = FwFormField.getValueByDataField($form, 'InDeliveryAddressType');
                    if (value === 'WAREHOUSE') {
                        this.setWarehouseAddress($form, 'In');
                    } else if (value === 'DEAL') {
                        this.setDealAddress($form, 'In');
                    }
                }
            })
            .on('click', 'div[data-datafield="OutDeliveryDeliveryType"], div[data-datafield="InDeliveryDeliveryType"]', event => {
                let $element = jQuery(event.currentTarget);
                $element.data('prevValue', FwFormField.getValueByDataField($form, $element.attr('data-datafield')));
            })
            .on('change', 'div[data-datafield="OutDeliveryDeliveryType"], div[data-datafield="InDeliveryDeliveryType"]', event => {
                let $element  = jQuery(event.currentTarget);
                let newValue  = FwFormField.getValue2($element);
                let prevValue = $element.data('prevValue');

                switch ($element.attr('data-datafield')) {
                    case 'OutDeliveryDeliveryType':
                        if ((newValue === 'DELIVER' || newValue === 'SHIP') && prevValue === 'PICK UP') {
                            FwFormField.setValueByDataField($form, 'OutDeliveryAddressType', 'DEAL', '', true);
                        } else if (newValue === 'PICK UP') {
                            FwFormField.setValueByDataField($form, 'OutDeliveryAddressType', 'WAREHOUSE', '', true);
                        }
                        break;
                    case 'InDeliveryDeliveryType':
                        if ((newValue === 'DELIVER' || newValue === 'SHIP') && prevValue === 'PICK UP') {
                            FwFormField.setValueByDataField($form, 'InDeliveryAddressType', 'WAREHOUSE', '', true);
                        } else if (newValue === 'PICK UP') {
                            FwFormField.setValueByDataField($form, 'InDeliveryAddressType', 'DEAL', '', true);
                        }
                        break;
                }
            })
            .on('change', 'div[data-datafield="OrderTypeId"]', event => {
                this.dynamicColumns($form);
            })
        ;
    }
    //----------------------------------------------------------------------------------------------
    setWarehouseAddress($form: any, prefix: string): void {
        if ($form.data('warehousedata')) {
            let warehousedata: any = $form.data('warehousedata');
            this.updateDeliveryAddress($form, prefix, warehousedata);
        } else {
            const warehouseid = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
            FwAppData.apiMethod(true, 'GET', `api/v1/warehouse/${warehouseid}`, null, FwServices.defaultTimeout, response => {
                this.updateDeliveryAddress($form, prefix, response);
                $form.data('warehousedata', {
                    'Attention': response.Attention,
                    'Address1':  response.Address1,
                    'Address2':  response.Address2,
                    'City':      response.City,
                    'State':     response.State,
                    'Zip':       response.Zip,
                    'CountryId': response.CountryId,
                    'Country':   response.Country
                });
            }, null, null);
        }
    }
    //----------------------------------------------------------------------------------------------
    setDealAddress($form: any, prefix: string, response?: any): void {
        if (!response) {
            const dealid = FwFormField.getValueByDataField($form, 'DealId');
            FwAppData.apiMethod(true, 'GET', `api/v1/deal/${dealid}`, null, FwServices.defaultTimeout, res => {
                this.updateDeliveryAddress($form, prefix, res);
            }, null, null);
        } else {
            this.updateDeliveryAddress($form, prefix, response);
        }
    }
    //----------------------------------------------------------------------------------------------
    updateDeliveryAddress($form: any, prefix: string, data: any): void {
        FwFormField.setValueByDataField($form, `${prefix}DeliveryToAttention`, data.Attention);
        FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress1`,  data.Address1);
        FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress2`,  data.Address2);
        FwFormField.setValueByDataField($form, `${prefix}DeliveryToCity`,      data.City);
        FwFormField.setValueByDataField($form, `${prefix}DeliveryToState`,     data.State);
        FwFormField.setValueByDataField($form, `${prefix}DeliveryToZipCode`,   data.Zip);
        FwFormField.setValueByDataField($form, `${prefix}DeliveryToCountryId`, data.CountryId, data.Country);
    }
    //----------------------------------------------------------------------------------------------
    //cancelPickList(pickListId, pickListNumber, $form) {
    //    var $confirmation, $yes, $no;
    //    var orderId = FwFormField.getValueByDataField($form, 'OrderId');
    //    $confirmation = FwConfirmation.renderConfirmation('Cancel Pick List', '<div style="white-space:pre;">\n' +
    //        'Cancel Pick List ' + pickListNumber + '?</div>');
    //    $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
    //    $no = FwConfirmation.addButton($confirmation, 'No');
    //    $yes.on('click', function () {
    //        FwAppData.apiMethod(true, 'DELETE', 'api/v1/picklist/' + pickListId, {}, FwServices.defaultTimeout, function onSuccess(response) {
    //            try {
    //                FwNotification.renderNotification('SUCCESS', 'Pick List Cancelled');
    //                FwConfirmation.destroyConfirmation($confirmation);
    //                var $pickListGridControl = $form.find('[data-name="OrderPickListGrid"]');
    //                $pickListGridControl.data('ondatabind', function (request) {
    //                    request.uniqueids = {
    //                        OrderId: orderId
    //                    };
    //                });
    //                FwBrowse.search($pickListGridControl);
    //            }
    //            catch (ex) {
    //                FwFunc.showError(ex);
    //            }
    //        }, null, $form);
    //    });
    //};
    //----------------------------------------------------------------------------------------------
    addLossDamage($form: JQuery, event: any): void {
        let sessionId, $lossAndDamageItemGridControl;
        const userWarehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
        const dealId = FwFormField.getValueByDataField($form, 'DealId');
        const HTML: Array<string> = [];
        HTML.push(
            `<div class="fwcontrol fwcontainer fwform popup" data-control="FwContainer" data-type="form" data-caption="Loss and Damage">
              <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div style="float:right;" class="close-modal"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>
                <div class="tabpages">
                  <div class="formpage">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Loss and Damage">
                      <div class="formrow">
                        <div class="formcolumn" style="width:100%;margin-top:50px;">
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div class="fwform-section-title" style="margin-bottom:20px;">Loss and Damage</div>
                            <div class="formrow error-msg"></div>
                            <div class="formrow sub-header" style="margin-left:8px;font-size:16px;"><span>Select one or more Orders with Lost or Damaged items, then click Continue</span></div>
                            <div data-control="FwGrid" class="container"></div>
                          </div>
                        </div>
                      </div>
                      <div class="formrow add-button">
                        <div class="select-items fwformcontrol" data-type="button" style="float:right;">Continue</div>
                      </div>
                      <div class="formrow session-buttons" style="display:none;">
                        <div class="options-button fwformcontrol" data-type="button" style="float:left">Options &#8675;</div>
                        <div class="selectall fwformcontrol" data-type="button" style="float:left; margin-left:10px;">Select All</div>
                        <div class="selectnone fwformcontrol" data-type="button" style="float:left; margin-left:10px;">Select None</div>
                        <div class="complete-session fwformcontrol" data-type="button" style="float:right;">Add To Order</div>
                      </div>
                      <div class="formrow option-list" style="display:none;">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Placeholder..." data-datafield=""></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>`
        );

        const addOrderBrowse = () => {
            var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
            $browse.attr('data-hasmultirowselect', 'true');
            $browse.attr('data-type', 'Browse');
            $browse.attr('data-showsearch', 'false');
            FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
                if (dt.Rows[rowIndex][dt.ColumnIndex['Status']] === 'CANCELLED') {
                    $tr.css('color', '#aaaaaa');
                }
            });

            $browse = FwModule.openBrowse($browse);
            $browse.find('.fwbrowse-menu').hide();

            $browse.data('ondatabind', function (request) {
                request.ActiveViewFields = OrderController.ActiveViewFields;
                request.pagesize = 15;
                request.orderby = 'OrderDate desc';
                request.miscfields = {
                    LossAndDamage: true,
                    LossAndDamageWarehouseId: userWarehouseId,
                    LossAndDamageDealId: dealId
                }
            });
            return $browse;
        }

        const startLDSession = (): void => {
            let $browse = jQuery($popup).children().find('.fwbrowse');
            let orderId, $selectedCheckBoxes: any, orderIds: string = '';
            $selectedCheckBoxes = $browse.find('.cbselectrow:checked');
            if ($selectedCheckBoxes.length !== 0) {
                for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                    orderId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');
                    if (orderId) {
                        orderIds = orderIds.concat(', ', orderId);
                    }
                }
                orderIds = orderIds.substring(2);

                const request: any = {};
                request.OrderIds = orderIds;
                request.DealId = dealId;
                request.WarehouseId = userWarehouseId;
                FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/startsession`, request, FwServices.defaultTimeout, response => {
                    sessionId = response.SessionId
                    this.lossDamageSessionId = sessionId;
                    if (sessionId) {
                        $popup.find('.container').html('<div class="formrow"><div data-control="FwGrid" data-grid="LossAndDamageItemGrid" data-securitycaption=""></div></div>');
                        $popup.find('.add-button').hide();
                        $popup.find('.sub-header').hide();
                        $popup.find('.session-buttons').show();
                        const $lossAndDamageItemGrid = $popup.find('div[data-grid="LossAndDamageItemGrid"]');
                        $lossAndDamageItemGridControl = jQuery(jQuery('#tmpl-grids-LossAndDamageItemGridBrowse').html());
                        $lossAndDamageItemGrid.data('sessionId', sessionId);
                        $lossAndDamageItemGrid.data('orderId', orderId);
                        $lossAndDamageItemGrid.empty().append($lossAndDamageItemGridControl);
                        $lossAndDamageItemGridControl.data('ondatabind', function (request) {
                            request.uniqueids = {
                                SessionId: sessionId
                            };
                        });
                        FwBrowse.init($lossAndDamageItemGridControl);
                        FwBrowse.renderRuntimeHtml($lossAndDamageItemGridControl);

                        FwBrowse.search($lossAndDamageItemGridControl);
                    }
                }, null, $browse);
            } else {
                FwNotification.renderNotification('WARNING', 'Select rows in order to perform this function.');
            }
        }
        const events = () => {
            let $orderItemGridLossDamage = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');
            let gridContainer = $popup.find('.container');
            //Close the popup
            $popup.find('.close-modal').one('click', e => {
                FwPopup.destroyPopup($popup);
                jQuery(document).find('.fwpopup').off('click');
                jQuery(document).off('keydown');
            });
            // Starts LD session
            $popup.find('.select-items').on('click', event => {
                startLDSession();
            });
            // Complete Session
            $popup.find('.complete-session').on('click', event => {
            let $lossAndDamageItemGrid = $popup.find('div[data-grid="LossAndDamageItemGrid"]');
            $lossAndDamageItemGrid = jQuery($lossAndDamageItemGrid);
                let request: any = {};
                request.SourceOrderId = FwFormField.getValueByDataField($form, 'OrderId');
                request.SessionId = this.lossDamageSessionId
                FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/completesession`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.success === true) {
                        FwPopup.destroyPopup($popup);
                        FwBrowse.search($orderItemGridLossDamage);
                    } else {
                        FwNotification.renderNotification('ERROR', response.msg); //justin 01/31/2019
                    }
                }, null, $lossAndDamageItemGrid)
            });
            // Select All
            $popup.find('.selectall').on('click', e => {
                let $lossAndDamageItemGrid = $popup.find('div[data-grid="LossAndDamageItemGrid"]');
                $lossAndDamageItemGrid = jQuery($lossAndDamageItemGrid);

                const request: any = {};
                request.SessionId = this.lossDamageSessionId; //justin 01/31/2019
                FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/selectall`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    $popup.find('.error-msg').html('');
                    if (response.success === false) {
                        FwFunc.playErrorSound();
                        $popup.find('div.error-msg').html(`<div><span>${response.msg}</span></div>`);
                    } else {
                        FwFunc.playSuccessSound();
                        FwBrowse.search($lossAndDamageItemGridControl);
                    }
                }, function onError(response) {
                    FwFunc.showError(response);
                    }, $lossAndDamageItemGrid);
            });
            // Select None
            $popup.find('.selectnone').on('click', e => {
                let $lossAndDamageItemGrid = $popup.find('div[data-grid="LossAndDamageItemGrid"]');
                $lossAndDamageItemGrid = jQuery($lossAndDamageItemGrid);

                const request: any = {};
                request.SessionId = this.lossDamageSessionId; //justin 01/31/2019
                FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/selectnone`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    $popup.find('.error-msg').html('');
                    if (response.success === false) {
                        FwFunc.playErrorSound();
                        FwBrowse.search($lossAndDamageItemGridControl);
                        $popup.find('div.error-msg').html(`<div><span">${response.msg}</span></div>`);
                    } else {
                        FwFunc.playSuccessSound();
                        FwBrowse.search($lossAndDamageItemGridControl); //justin 01/31/2019
                    }
                }, function onError(response) {
                    FwFunc.showError(response);
                    }, $lossAndDamageItemGrid);
            });
            //Options button
            $popup.find('.options-button').on('click', e => {
                $popup.find('div.formrow.option-list').toggle();
            });
        }
        const $popupHtml = HTML.join('');
        const $popup = FwPopup.renderPopup(jQuery($popupHtml), { ismodal: true });
        FwPopup.showPopup($popup);
        const $orderBrowse = addOrderBrowse();
        $popup.find('.container').append($orderBrowse);
        FwBrowse.search($orderBrowse);
        events();
    }
    //----------------------------------------------------------------------------------------------
    retireLossDamage($form: JQuery): void {
        const $confirmation = FwConfirmation.renderConfirmation('Confirm?', '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');

        const html: Array<string> = [];;
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div>Create a Lost Contract and Retire the Items?</div>');
        html.push('  </div>');
        html.push('</div>');

        FwConfirmation.addControls($confirmation, html.join(''));
        const $yes = FwConfirmation.addButton($confirmation, 'Retire', false);
        const $no = FwConfirmation.addButton($confirmation, 'Cancel');

        $yes.on('click', retireLD);

        function retireLD() {
            const orderId = FwFormField.getValueByDataField($form, 'OrderId');
            const request: any = {}
            request.OrderId = orderId;
            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            FwFormField.disable($no);
            $yes.text('Retiring...');
            $yes.off('click');

            FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/retire`, request, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success === true) {
                    const uniqueids: any = {};
                    uniqueids.ContractId = response.ContractId;
                    FwNotification.renderNotification('SUCCESS', 'Order Successfully Retired');
                    FwConfirmation.destroyConfirmation($confirmation);
                    const $contractForm = ContractController.loadForm(uniqueids);
                    FwModule.openModuleTab($contractForm, "", true, 'FORM', true)
                    FwModule.refreshForm($form);
                }
            }, function onError(response) {
                $yes.on('click', retireLD);
                $yes.text('Retire');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwModule.refreshForm($form);
                }, $form);
        }
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateOutShipVia($browse: any, $form: any, request: any) {
        request.uniqueids = {
            VendorId: FwFormField.getValue($form, 'div[data-datafield="OutDeliveryCarrierId"]')
        };
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateInShipVia($browse: any, $form: any, request: any) {
        request.uniqueids = {
            VendorId: FwFormField.getValue($form, 'div[data-datafield="InDeliveryCarrierId"]')
        };
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateCarrier($browse: any, $form: any, request: any) {
        request.uniqueids = {
            Freight: true
        };
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateDeal($browse: any, $form: any, request: any) {
        request.uniqueids = {
            LocationId: FwFormField.getValueByDataField($form, 'OfficeLocationId')
        }
    }
    //----------------------------------------------------------------------------------------------
}

//---------------------------------------------------------------------------------
//FwApplicationTree.clickEvents[Constants.Modules.Home.Order.form.menuItems.AddLossDamage.id] = function (event) {
//    let $form = jQuery(this).closest('.fwform');
//    if ($form.attr('data-mode') !== 'NEW') {
//        try {
//            OrderController.addLossDamage($form, event);
//        } catch (ex) {
//            FwFunc.showError(ex);
//        }
//    } else {
//        FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
//    }
//};
////---------------------------------------------------------------------------------
//FwApplicationTree.clickEvents[Constants.Modules.Home.Order.form.menuItems.RetireLossDamage.id] = function (event) {
//    let $form = jQuery(this).closest('.fwform');
//    if ($form.attr('data-mode') !== 'NEW') {
//        try {
//            OrderController.retireLossDamage($form);
//        } catch (ex) {
//            FwFunc.showError(ex);
//        }
//    } else {
//        FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
//    }
//};
//---------------------------------------------------------------------------------
//Create Pick List
//FwApplicationTree.clickEvents[Constants.Modules.Home.Order.form.menuItems.CreatePickList.id] = function (event) {
//    try {
//        var $form = jQuery(this).closest('.fwform');
//        var orderInfo: any = {
//            OrderId: FwFormField.getValueByDataField($form, 'OrderId')
//        };
//        var $pickListForm = CreatePickListController.openForm('EDIT', orderInfo);
//        FwModule.openSubModuleTab($form, $pickListForm);
//        jQuery('.tab.submodule.active').find('.caption').html('New Pick List');
//        var $pickListUtilityGrid = $pickListForm.find('[data-name="PickListUtilityGrid"]');
//        FwBrowse.search($pickListUtilityGrid);
//    } catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//----------------------------------------------------------------------------------------------
//Confirmation for cancelling Pick List
//FwApplicationTree.clickEvents[Constants.Modules.Home.Order.form.menuItems.CancelPickList.id] = function (event) {
//    var $form = jQuery(this).closest('.fwform');
//    var pickListId = $form.find('tr.selected > td.column > [data-formdatafield="PickListId"]').attr('data-originalvalue');
//    var pickListNumber = $form.find('tr.selected > td.column > [data-formdatafield="PickListNumber"]').attr('data-originalvalue');
//    try {
//        OrderController.cancelPickList(pickListId, pickListNumber, $form);
//    } catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//----------------------------------------------------------------------------------------------
//Copy Order
//FwApplicationTree.clickEvents[Constants.Modules.Home.Order.form.menuItems.CopyOrder.id] = function (event) {
//    try {
//        var $form           = jQuery(this).closest('.fwform');
//        const $confirmation = FwConfirmation.renderConfirmation('Copy Order', '');
//        const $yes          = FwConfirmation.addButton($confirmation, 'Copy', false);
//        const $no           = FwConfirmation.addButton($confirmation, 'Cancel');
//        const html          = [];
//        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
//        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
//        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Type" data-datafield="Type" data-enabled="false" style="width:90px;float:left;"></div>');
//        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Job" data-datafield="Deal" data-enabled="false" style="width:340px;float:left;"></div>');
//        html.push('  </div>');
//        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
//        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No" data-datafield="OrderNo" data-enabled="false" style="width:90px;float:left;"></div>');
//        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-enabled="false" style="width:340px;float:left;"></div>');
//        html.push('  </div>');
//        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
//        html.push('    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="New Job" data-datafield="CopyToDealId" data-browsedisplayfield="Deal" data-validationname="DealValidation"></div>');
//        html.push('  </div>');
//        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
//        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Dates" data-datafield="CopyDates"></div>');
//        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Line Item Notes" data-datafield="CopyLineItemNotes"></div>');
//        html.push('  </div>');
//        html.push('</div>');
//        FwConfirmation.addControls($confirmation, html.join(''));

//        var deal        = FwFormField.getText($form, '[data-datafield="DealId"]');
//        var dealId      = FwFormField.getValue($form, '[data-datafield="DealId"]');

//        FwFormField.setValue($confirmation, 'div[data-datafield="Type"]', 'Order');
//        FwFormField.setValue($confirmation, 'div[data-datafield="Deal"]', deal);
//        FwFormField.setValue($confirmation, 'div[data-datafield="OrderNo"]', FwFormField.getValueByDataField($form, 'OrderNumber'));
//        FwFormField.setValue($confirmation, 'div[data-datafield="Description"]', FwFormField.getValueByDataField($form, 'Description'));
//        FwFormField.setValue($confirmation, 'div[data-datafield="CopyToDealId"]', dealId, deal);
//        FwFormField.setValue($confirmation, 'div[data-datafield="CopyDates"]', true);
//        FwFormField.setValue($confirmation, 'div[data-datafield="CopyLineItemNotes"]', true);

//        $yes.on('click', function makeACopy() {
//            const request: any = {
//                CopyToType:             'O',
//                CopyToDealId:           FwFormField.getValueByDataField($confirmation, 'CopyToDealId'),
//                CopyRatesFromInventory: 'False',
//                CopyDates:              (FwFormField.getValueByDataField($confirmation, 'CopyDates') === 'T' ? 'True' : 'False'),
//                CopyLineItemNotes:      (FwFormField.getValueByDataField($confirmation, 'CopyLineItemNotes') === 'T' ? 'True' : 'False'),
//                CombineSubs:            'False',
//                CopyDocuments:          'False'
//            };

//            FwFormField.disable($confirmation.find('.fwformfield'));
//            FwFormField.disable($yes);
//            $yes.text('Copying...');
//            $yes.off('click');
//            const orderId = FwFormField.getValueByDataField($form, 'OrderId');
//            FwAppData.apiMethod(true, 'POST', `api/v1/order/copytoorder/${orderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
//                FwNotification.renderNotification('SUCCESS', 'Order Successfully Copied');
//                FwConfirmation.destroyConfirmation($confirmation);
//                const uniqueids: any = {
//                    OrderId:     response.OrderId,
//                    OrderTypeId: response.OrderTypeId
//                };
//                var $control = OrderController.loadForm(uniqueids);
//                FwModule.openModuleTab($control, "", true, 'FORM', true);
//            }, function onError(response) {
//                $yes.on('click', makeACopy);
//                $yes.text('Copy');
//                FwFunc.showError(response);
//                FwFormField.enable($confirmation.find('.fwformfield'));
//                FwFormField.enable($yes);
//            }, $confirmation);
//        });
//    } catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//----------------------------------------------------------------------------------------------
//Cancel / Uncancel
//FwApplicationTree.clickEvents[Constants.Modules.Home.Order.form.menuItems.CancelUncancel.id] = function (event) {
//    try {
//        let $confirmation, $yes, $no;
//        let $form       = jQuery(this).closest('.fwform');
//        var self        = this;
//        var id          = FwFormField.getValueByDataField($form, 'OrderId');
//        var orderStatus = FwFormField.getValueByDataField($form, 'Status');

//        if (id != null) {
//            if (orderStatus === "CANCELLED") {
//                $confirmation = FwConfirmation.renderConfirmation('Cancel', '<div>Would you like to un-cancel this order?</div>');
//                $yes          = FwConfirmation.addButton($confirmation, 'Un-Cancel Order', false);
//                $no           = FwConfirmation.addButton($confirmation, 'Cancel');

//                $yes.on('click', function uncancelOrder() {
//                    FwFormField.disable($confirmation.find('.fwformfield'));
//                    FwFormField.disable($yes);
//                    $yes.text('Retrieving...');
//                    $yes.off('click');

//                    FwAppData.apiMethod(true, 'POST', `api/v1/order/uncancel/${id}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
//                        FwNotification.renderNotification('SUCCESS', 'Order Successfully Retrieved');
//                        FwConfirmation.destroyConfirmation($confirmation);
//                        FwModule.refreshForm($form);
//                    }, function onError(response) {
//                        $yes.on('click', uncancelOrder);
//                        $yes.text('Cancel');
//                        FwFunc.showError(response);
//                        FwFormField.enable($confirmation.find('.fwformfield'));
//                        FwFormField.enable($yes);
//                        FwModule.refreshForm($form);
//                    }, $form);
//                });
//            } else {
//                $confirmation = FwConfirmation.renderConfirmation('Cancel', '<div>Would you like to cancel this order?</div>');
//                $yes          = FwConfirmation.addButton($confirmation, 'Cancel Order', false);
//                $no           = FwConfirmation.addButton($confirmation, 'Cancel');

//                $yes.on('click', function cancelOrder() {
//                    FwFormField.disable($confirmation.find('.fwformfield'));
//                    FwFormField.disable($yes);
//                    $yes.text('Canceling...');
//                    $yes.off('click');

//                    FwAppData.apiMethod(true, 'POST', `api/v1/order/cancel/${id}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
//                        FwNotification.renderNotification('SUCCESS', 'Order Successfully Cancelled');
//                        FwConfirmation.destroyConfirmation($confirmation);
//                        FwModule.refreshForm($form);
//                    }, function onError(response) {
//                        $yes.on('click', cancelOrder);
//                        $yes.text('Cancel');
//                        FwFunc.showError(response);
//                        FwFormField.enable($confirmation.find('.fwformfield'));
//                        FwFormField.enable($yes);
//                        FwModule.refreshForm($form);
//                    }, $form);
//                });
//            }
//        } else {
//            FwNotification.renderNotification('WARNING', 'Select an Order to perform this action.');
//        }
//    } catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//----------------------------------------------------------------------------------------------
//Order Status
//FwApplicationTree.clickEvents[Constants.Modules.Home.Order.form.menuItems.OrderStatus.id] = function (e) {
//    try {
//        var $form = jQuery(this).closest('.fwform');
//        var orderInfo: any = {
//            OrderId:     FwFormField.getValueByDataField($form, 'OrderId'),
//            OrderNumber: FwFormField.getValueByDataField($form, 'OrderNumber')
//        };
//        var $orderStatusForm = OrderStatusController.openForm('EDIT', orderInfo);
//        FwModule.openSubModuleTab($form, $orderStatusForm);
//        jQuery('.tab.submodule.active').find('.caption').html('Order Status');
//    } catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//----------------------------------------------------------------------------------------------
//Check In
//FwApplicationTree.clickEvents[Constants.Modules.Home.Order.form.menuItems.CheckIn.id] = function (e) {
//    try {
//        var $form = jQuery(this).closest('.fwform');
//        var orderInfo: any = {
//            OrderId:     FwFormField.getValueByDataField($form, 'OrderId'),
//            OrderNumber: FwFormField.getValueByDataField($form, 'OrderNumber')
//        };
//        var $checkinForm = CheckInController.openForm('EDIT', orderInfo);
//        FwModule.openSubModuleTab($form, $checkinForm);
//        jQuery('.tab.submodule.active').find('.caption').html('Check-In');
//    } catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//----------------------------------------------------------------------------------------------
//Check Out
//FwApplicationTree.clickEvents[Constants.Modules.Home.Order.form.menuItems.CheckOut.id] = function (event) {
//    try {
//        var $form = jQuery(this).closest('.fwform');
//        var orderInfo: any = {
//            OrderId:     FwFormField.getValueByDataField($form, 'OrderId'),
//            OrderNumber: FwFormField.getValueByDataField($form, 'OrderNumber'),
//            WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId'),
//            Warehouse:   FwFormField.getText($form, 'div[data-datafield="WarehouseId"]'),
//            DealId:      FwFormField.getValueByDataField($form, 'DealId'),
//            Deal:        FwFormField.getText($form, 'div[data-datafield="DealId"]')
//        };
//        var $stagingCheckoutForm = StagingCheckoutController.openForm('EDIT', orderInfo);
//        FwModule.openSubModuleTab($form, $stagingCheckoutForm);
//        jQuery('.tab.submodule.active').find('.caption').html('Staging / Check-Out');
//    } catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//----------------------------------------------------------------------------------------------
//Open Search Interface
//FwApplicationTree.clickEvents[Constants.Modules.Home.Order.form.menuItems.Search.id] = function (e) {
//    var $form = jQuery(this).closest('.fwform');
//    var orderId = FwFormField.getValueByDataField($form, 'OrderId');
//    if ($form.attr('data-mode') === 'NEW') {
//        OrderController.saveForm($form, { closetab: false });
//        return;
//    }
//    if (orderId == "") {
//        FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
//    } else {
//        var search = new SearchInterface();
//        search.renderSearchPopup($form, orderId, 'Order');
//    }
//};
//----------------------------------------------------------------------------------------------
//Print Order
//FwApplicationTree.clickEvents[Constants.Modules.Home.Order.form.menuItems.PrintOrder.id] = function (e) {
//    try {
//        var $form       = jQuery(this).closest('.fwform');
//        var orderNumber = FwFormField.getValue($form, 'div[data-datafield="OrderNumber"]');
//        var orderId     = FwFormField.getValue($form, 'div[data-datafield="OrderId"]');
//        var $report     = OrderReportController.openForm();

//        FwModule.openSubModuleTab($form, $report);

//        FwFormField.setValue($report, 'div[data-datafield="OrderId"]', orderId, orderNumber);
//    } catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//---------------------------------------------------------------------------------
//Browse Cancel Option
//FwApplicationTree.clickEvents[Constants.Modules.Home.Order.browse.menuItems.CancelUncancel.id] = function (event) {
//    try {
//        let $confirmation, $yes, $no;
//        var $browse     = jQuery(this).closest('.fwbrowse');
//        var orderId     = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
//        var orderStatus = $browse.find('.selected [data-formdatafield="Status"]').attr('data-originalvalue');

//        if (orderId != null) {
//            if (orderStatus === "CANCELLED") {
//                $confirmation = FwConfirmation.renderConfirmation('Cancel', '<div>Would you like to un-cancel this Order?</div>');
//                $yes          = FwConfirmation.addButton($confirmation, 'Un-Cancel Order', false);
//                $no           = FwConfirmation.addButton($confirmation, 'Cancel');

//                $yes.on('click', function uncancelOrder() {
//                    FwFormField.disable($confirmation.find('.fwformfield'));
//                    FwFormField.disable($yes);
//                    $yes.text('Retrieving...');
//                    $yes.off('click');

//                    FwAppData.apiMethod(true, 'POST', `api/v1/order/uncancel/${orderId}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
//                        FwNotification.renderNotification('SUCCESS', 'Order Successfully Retrieved');
//                        FwConfirmation.destroyConfirmation($confirmation);
//                        FwBrowse.databind($browse);
//                    }, function onError(response) {
//                        $yes.on('click', uncancelOrder);
//                        $yes.text('Cancel');
//                        FwFunc.showError(response);
//                        FwFormField.enable($confirmation.find('.fwformfield'));
//                        FwFormField.enable($yes);
//                        FwBrowse.databind($browse);
//                    }, $browse);
//                });
//            } else {
//                $confirmation = FwConfirmation.renderConfirmation('Cancel', '<div>Would you like to cancel this Order?</div>');
//                $yes          = FwConfirmation.addButton($confirmation, 'Cancel Order', false);
//                $no           = FwConfirmation.addButton($confirmation, 'Cancel');

//                $yes.on('click', function cancelOrder() {
//                    FwFormField.disable($confirmation.find('.fwformfield'));
//                    FwFormField.disable($yes);
//                    $yes.text('Canceling...');
//                    $yes.off('click');

//                    FwAppData.apiMethod(true, 'POST', `api/v1/order/cancel/${orderId}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
//                        FwNotification.renderNotification('SUCCESS', 'Order Successfully Cancelled');
//                        FwConfirmation.destroyConfirmation($confirmation);
//                        FwBrowse.databind($browse);
//                    }, function onError(response) {
//                        $yes.on('click', cancelOrder);
//                        $yes.text('Cancel');
//                        FwFunc.showError(response);
//                        FwFormField.enable($confirmation.find('.fwformfield'));
//                        FwFormField.enable($yes);
//                        FwBrowse.databind($browse);
//                    }, $browse);
//                });
//            }
//        } else {
//            FwNotification.renderNotification('WARNING', 'Select an Order to perform this action.');
//        }
//    } catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//---------------------------------------------------------------------------------

var OrderController = new Order();