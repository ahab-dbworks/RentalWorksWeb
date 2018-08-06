routes.push({ pattern: /^module\/purchaseorder$/, action: function (match: RegExpExecArray) { return PurchaseOrderController.getModuleScreen(); } });
routes.push({ pattern: /^module\/purchaseorder\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return PurchaseOrderController.getModuleScreen(filter); } });

//----------------------------------------------------------------------------------------------
class PurchaseOrder {
    Module: string = 'PurchaseOrder';
    apiurl: string = 'api/v1/PurchaseOrder';
    caption: string = 'Purchase Order';
    ActiveView: string = 'ALL';
    DefaultPurchasePoType: string;
    DefaultPurchasePoTypeId: string;


    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        var self = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        var $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);

            if (typeof filter !== 'undefined' && filter.datafield === 'agent') {
                filter.search = filter.search.split('%20').reverse().join(', ');
            }

            if (typeof filter !== 'undefined') {
                filter.datafield = filter.datafield.charAt(0).toUpperCase() + filter.datafield.slice(1);
                $browse.find('div[data-browsedatafield="' + filter.datafield + '"]').find('input').val(filter.search);
            }

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
        var self = this;
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);


        var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        self.ActiveView = 'WarehouseId=' + warehouse.warehouseid;

        $browse.data('ondatabind', function (request) {
            request.activeview = self.ActiveView;
        });
        FwBrowse.addLegend($browse, 'On Hold', '#EA300F');
        FwBrowse.addLegend($browse, 'No Charge', '#FF8040');
        FwBrowse.addLegend($browse, 'Late', '#FFB3D9');
        FwBrowse.addLegend($browse, 'Foreign Currency', '#95FFCA');
        FwBrowse.addLegend($browse, 'Multi-Warehouse', '#D6E180');
        FwBrowse.addLegend($browse, 'Repair', '#5EAEAE');
        FwBrowse.addLegend($browse, 'L&D', '#400040');


        let department = JSON.parse(sessionStorage.getItem('department'));;
        let location = JSON.parse(sessionStorage.getItem('location'));;

        FwAppData.apiMethod(true, 'GET', `api/v1/officelocation/${location.locationid}`, null, FwServices.defaultTimeout, function onSuccess(response) {
            self.DefaultPurchasePoType = response.DefaultPurchasePoType;
            self.DefaultPurchasePoTypeId = response.DefaultPurchasePoTypeId;
        }, null, null);

        return $browse;
    };

    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject) {
        var self = this;
        var $all = FwMenu.generateDropDownViewBtn('All', true);
        var $new = FwMenu.generateDropDownViewBtn('New', false);
        var $open = FwMenu.generateDropDownViewBtn('Open', false);
        var $received = FwMenu.generateDropDownViewBtn('Received', false);
        var $complete = FwMenu.generateDropDownViewBtn('Complete', false);
        var $void = FwMenu.generateDropDownViewBtn('Void', false);
        var $closed = FwMenu.generateDropDownViewBtn('Closed', false);
        $all.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ALL';
            FwBrowse.search($browse);
        });
        $new.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'NEW';
            FwBrowse.search($browse);
        });
        $open.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'OPEN';
            FwBrowse.search($browse);
        });
        $received.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'RECEIVED';
            FwBrowse.search($browse);
        });
        $complete.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'COMPLETE';
            FwBrowse.search($browse);
        });
        $void.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'VOID';
            FwBrowse.search($browse);
        });
        $closed.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CLOSED';
            FwBrowse.search($browse);
        });
        var viewSubitems = [];
        viewSubitems.push($all);
        viewSubitems.push($new);
        viewSubitems.push($open);
        viewSubitems.push($received);
        viewSubitems.push($complete);
        viewSubitems.push($void);
        viewSubitems.push($closed);
        var $view;
        $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);

        //Location Filter
        var location = JSON.parse(sessionStorage.getItem('location'));
        var $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false);
        var $userLocation = FwMenu.generateDropDownViewBtn(location.location, true);
        $allLocations.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LocationId=ALL';
            FwBrowse.search($browse);
        });
        $userLocation.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LocationId=' + location.locationid;
            FwBrowse.search($browse);
        });
        var viewLocation = [];
        viewLocation.push($userLocation);
        viewLocation.push($all);
        var $locationView;
        $locationView = FwMenu.addViewBtn($menuObject, 'Location', viewLocation);
        return $menuObject;
    };

    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        var $form, $submodulePickListBrowse, $submoduleContractBrowse;
        var self = this;

        $form = jQuery(jQuery('#tmpl-modules-PurchaseOrderForm').html());
        $form = FwModule.openForm($form, mode);

        FwFormField.disable($form.find('[data-datafield="SubRent"]'));
        FwFormField.disable($form.find('[data-datafield="SubSale"]'));
        FwFormField.disable($form.find('[data-datafield="SubLabor"]'));
        FwFormField.disable($form.find('[data-datafield="SubMiscellaneous"]'));
        FwFormField.disable($form.find('[data-datafield="SubVehicle"]'));

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');

            const today = FwFunc.getDate();
            const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            const office = JSON.parse(sessionStorage.getItem('location'));
            const department = JSON.parse(sessionStorage.getItem('department'));
            const usersid = sessionStorage.getItem('usersid');  // J. Pace 7/09/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
            const name = sessionStorage.getItem('name');

            $form.find('div[data-datafield="Rental"] input').prop('checked', true);
            $form.find('div[data-datafield="Sales"] input').prop('checked', true);
            $form.find('div[data-datafield="Parts"] input').prop('checked', true);
            FwFormField.setValue($form, 'div[data-datafield="ProjectManagerId"]', usersid, name);
            FwFormField.setValue($form, 'div[data-datafield="AgentId"]', usersid, name);
            //$form.find('div[data-datafield="Labor"] input').prop('checked', true);
            FwFormField.setValueByDataField($form, 'PurchaseOrderDate', today);
      
            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
            FwFormField.setValue($form, 'div[data-datafield="PoTypeId"]', this.DefaultPurchasePoTypeId, this.DefaultPurchasePoType);

            //FwFormField.setValueByDataField($form, 'EstimatedStartDate', today);
            //FwFormField.setValueByDataField($form, 'EstimatedStopDate', today);
            //FwFormField.setValueByDataField($form, 'BillingWeeks', '0');
            //FwFormField.setValueByDataField($form, 'BillingMonths', '0');

            //$form.find('div[data-datafield="PickTime"]').attr('data-required', false);
            //$form.find('div[data-datafield="EstimatedStartTime"]').attr('data-required', false);
            //$form.find('div[data-datafield="EstimatedStopTime"]').attr('data-required', false);


            //$form.find('div[data-datafield="PendingPo"] input').prop('checked', true);
            //$form.find('div[data-datafield="Rental"] input').prop('checked', true);
            //$form.find('div[data-datafield="Sales"] input').prop('checked', true);
            //$form.find('div[data-datafield="Miscellaneous"] input').prop('checked', true);
            //$form.find('div[data-datafield="Labor"] input').prop('checked', true);
            //FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
            //FwFormField.disable($form.find('[data-datafield="PoAmount"]'));


            //FwFormField.disable($form.find('.frame'));
            //$form.find(".frame .add-on").children().hide();
        };

        //$form.find('[data-datafield="BillToAddressDifferentFromIssuedToAddress"] .fwformfield-value').on('change', function () {
        //    var $this = jQuery(this);
        //    if ($this.prop('checked') === true) {
        //        FwFormField.enable($form.find('.differentaddress'));
        //    }
        //    else {
        //        FwFormField.disable($form.find('.differentaddress'));
        //    }
        //});

        //$form.find('div[data-datafield="OrderTypeId"]').data('onchange', function ($tr) {
        //    self.CombineActivity = $tr.find('.field[data-browsedatafield="CombineActivityTabs"]').attr('data-originalvalue');
        //    $form.find('[data-datafield="CombineActivity"] input').val(self.CombineActivity);

        //    const rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]')
        //        , salesTab = $form.find('[data-type="tab"][data-caption="Sales"]')
        //        , miscTab = $form.find('[data-type="tab"][data-caption="Misc"]')
        //        , laborTab = $form.find('[data-type="tab"][data-caption="Labor"]');
        //    let combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
        //    if (combineActivity == "true") {
        //        $form.find('.notcombinedtab').hide();
        //        $form.find('.combinedtab').show();
        //    } else if (combineActivity == "false") {
        //        $form.find('.combinedtab').hide();
        //        $form.find('[data-datafield="Rental"] input').prop('checked') ? rentalTab.show() : rentalTab.hide();
        //        $form.find('[data-datafield="Sales"] input').prop('checked') ? salesTab.show() : salesTab.hide();
        //        $form.find('[data-datafield="Miscellaneous"] input').prop('checked') ? miscTab.show() : miscTab.hide();
        //        $form.find('[data-datafield="Labor"] input').prop('checked') ? laborTab.show() : laborTab.hide();
        //    }
        //});


        //$form.find('[data-datafield="NoCharge"] .fwformfield-value').on('change', function () {
        //    var $this = jQuery(this);

        //    if ($this.prop('checked') === true) {
        //        FwFormField.enable($form.find('[data-datafield="NoChargeReason"]'));
        //    } else {
        //        FwFormField.disable($form.find('[data-datafield="NoChargeReason"]'));
        //    }
        //});

        FwFormField.disable($form.find('[data-datafield="RentalTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="SalesTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="LaborTaxRate1"]'));

        this.events($form);
        this.activityCheckboxEvents($form, mode);

        return $form;
    };

    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="PurchaseOrderId"] input').val(uniqueids.PurchaseOrderId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };

    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };

    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        const maxPageSize = 9999;

        let $orderStatusHistoryGrid;
        let $orderStatusHistoryGridControl;
        $orderStatusHistoryGrid = $form.find('div[data-grid="OrderStatusHistoryGrid"]');
        $orderStatusHistoryGridControl = jQuery(jQuery('#tmpl-grids-OrderStatusHistoryGridBrowse').html());
        $orderStatusHistoryGrid.empty().append($orderStatusHistoryGridControl);
        $orderStatusHistoryGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId')
            };
        });
        FwBrowse.init($orderStatusHistoryGridControl);
        FwBrowse.renderRuntimeHtml($orderStatusHistoryGridControl);

        let $orderItemGridRental;
        let $orderItemGridRentalControl;
        $orderItemGridRental = $form.find('.rentalgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridRentalControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridRentalControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        $orderItemGridRentalControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridRentalControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridRental.empty().append($orderItemGridRentalControl);
        $orderItemGridRentalControl.data('isSummary', false);
        $orderItemGridRental.addClass('R');

        $orderItemGridRentalControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                RecType: 'R'
            };
            request.pagesize = maxPageSize;
        });
        $orderItemGridRentalControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'R';
        });

        FwBrowse.addEventHandler($orderItemGridRentalControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'rental');
            let rentalItems = $form.find('.rentalgrid tbody').children();
            rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
        });

        FwBrowse.init($orderItemGridRentalControl);
        FwBrowse.renderRuntimeHtml($orderItemGridRentalControl);

        let $orderItemGridSales;
        let $orderItemGridSalesControl;
        $orderItemGridSales = $form.find('.salesgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridSalesControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridSalesControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        $orderItemGridSalesControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridSalesControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridSales.empty().append($orderItemGridSalesControl);
        $orderItemGridSales.addClass('S');
        $orderItemGridSalesControl.data('isSummary', false);

        $orderItemGridSalesControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                RecType: 'S'
            };
            request.pagesize = maxPageSize;
        });
        $orderItemGridSalesControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'S';
        });
        FwBrowse.addEventHandler($orderItemGridSalesControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'sales');
            let salesItems = $form.find('.salesgrid tbody').children();
            salesItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Sales"]')) : FwFormField.enable($form.find('[data-datafield="Sales"]'));
        });

        FwBrowse.init($orderItemGridSalesControl);
        FwBrowse.renderRuntimeHtml($orderItemGridSalesControl);

        let $orderItemGridPart;
        let $orderItemGridPartControl;
        $orderItemGridPart = $form.find('.partgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridPartControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridPartControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        $orderItemGridPartControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridPartControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridPart.empty().append($orderItemGridPartControl);
        $orderItemGridPart.addClass('P');
        $orderItemGridPartControl.data('isSummary', false);

        $orderItemGridPartControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                RecType: 'P'
            };
            request.pagesize = maxPageSize;
        });
        $orderItemGridPartControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'P';
        });
        FwBrowse.addEventHandler($orderItemGridPartControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'part');
            let partItems = $form.find('.partgrid tbody').children();
            partItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Parts"]')) : FwFormField.enable($form.find('[data-datafield="Parts"]'));
        });

        FwBrowse.init($orderItemGridPartControl);
        FwBrowse.renderRuntimeHtml($orderItemGridPartControl);

        let $orderNoteGrid;
        let $orderNoteGridControl;
        $orderNoteGrid = $form.find('div[data-grid="OrderNoteGrid"]');
        $orderNoteGridControl = jQuery(jQuery('#tmpl-grids-OrderNoteGridBrowse').html());
        $orderNoteGrid.empty().append($orderNoteGridControl);
        $orderNoteGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
            };
        });
        $orderNoteGridControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId')
        });
        FwBrowse.init($orderNoteGridControl);
        FwBrowse.renderRuntimeHtml($orderNoteGridControl);
    };

    //----------------------------------------------------------------------------------------------
    loadAudit($form) {
        let uniqueid = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        FwModule.loadAudit($form, uniqueid);
    };

    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        let $orderItemGridRental;
        $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridRental);
        let $orderItemGridSales;
        $orderItemGridSales = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridSales);
        let $orderItemGridPart;
        $orderItemGridPart = $form.find('.partgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridPart);
        let $orderNoteGrid;
        $orderNoteGrid = $form.find('[data-name="OrderNoteGrid"]');
        FwBrowse.search($orderNoteGrid);

        this.dynamicColumns($form);
    };

    //----------------------------------------------------------------------------------------------
    activityCheckboxEvents($form, mode) {
        const rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]')
            , salesTab = $form.find('[data-type="tab"][data-caption="Sales"]')
            , partsTab = $form.find('[data-type="tab"][data-caption="Parts"]')
            , miscTab = $form.find('[data-type="tab"][data-caption="Misc"]')
            , laborTab = $form.find('[data-type="tab"][data-caption="Labor"]');
        $form.find('[data-datafield="Rental"] input').on('change', e => {
            if (mode == "NEW") {
                if (jQuery(e.currentTarget).prop('checked')) {
                    rentalTab.show();
                } else {
                    rentalTab.hide();
                }
            } else {
                let combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
                if (combineActivity == 'false') {
                    if (jQuery(e.currentTarget).prop('checked')) {
                        rentalTab.show();
                        FwFormField.disable($form.find('[data-datafield="RentalSale"]'));
                    } else {
                        rentalTab.hide();
                        FwFormField.enable($form.find('[data-datafield="RentalSale"]'));
                    }
                }
            }
        });
        $form.find('[data-datafield="Sales"] input').on('change', e => {
            if (mode == "NEW") {
                if (jQuery(e.currentTarget).prop('checked')) {
                    salesTab.show();
                } else {
                    salesTab.hide();
                }
            } else {
                let combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
                if (combineActivity == 'false') {
                    if (jQuery(e.currentTarget).prop('checked')) {
                        salesTab.show();
                    } else {
                        salesTab.hide();
                    }
                }
            }
        });
        $form.find('[data-datafield="Parts"] input').on('change', e => {
            if (mode == "NEW") {
                if (jQuery(e.currentTarget).prop('checked')) {
                    partsTab.show();
                } else {
                    partsTab.hide();
                }
            } else {
                let combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
                if (combineActivity == 'false') {
                    if (jQuery(e.currentTarget).prop('checked')) {
                        partsTab.show();
                    } else {
                        partsTab.hide();
                    }
                }
            }
        });
        $form.find('[data-datafield="Labor"] input').on('change', e => {
            if (mode == "NEW") {
                if (jQuery(e.currentTarget).prop('checked')) {
                    laborTab.show();
                } else {
                    laborTab.hide();
                }
            } else {
                let combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
                if (combineActivity == 'false') {
                    if (jQuery(e.currentTarget).prop('checked')) {
                        laborTab.show();
                    } else {
                        laborTab.hide();
                    }
                }
            }
        });
    };

    //----------------------------------------------------------------------------------------------
    dynamicColumns($form) {
        const PoType = FwFormField.getValueByDataField($form, "PoTypeId"),
            $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
            $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]'),
            $partGrid = $form.find('.partgrid [data-name="OrderItemGrid"]'),
            fields = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field'),
            fieldNames = [];

        for (let i = 3; i < fields.length; i++) {
            let name = jQuery(fields[i]).attr('data-mappedfield');
            if (name != "QuantityOrdered") {
                fieldNames.push(name);
            }
        }

        FwAppData.apiMethod(true, 'GET', `api/v1/potype/${PoType}`, null, FwServices.defaultTimeout, function onSuccess(response) {
            let hiddenFields: Array<string> = fieldNames.filter(function (field) {
                return !this.has(field)
            }, new Set(response.PurchaseShowFields))

            for (let i = 0; i < hiddenFields.length; i++) {
                jQuery($rentalGrid.find('[data-mappedfield="' + hiddenFields[i] + '"]')).parent().hide();
                jQuery($salesGrid.find('[data-mappedfield="' + hiddenFields[i] + '"]')).parent().hide();
                jQuery($partGrid.find('[data-mappedfield="' + hiddenFields[i] + '"]')).parent().hide();
            }
        }, null, null);
    };

    //----------------------------------------------------------------------------------------------
    calculateOrderItemGridTotals($form: any, gridType: string) {
        let subTotal, discount, salesTax, grossTotal, total, rateType;
        let extendedTotal = new Decimal(0);
        let discountTotal = new Decimal(0);
        let taxTotal = new Decimal(0);

        //let rateValue = $form.find('.' + gridType + 'grid .totalType input:checked').val();
        //switch (rateValue) {
        //    case 'W':
        //        rateType = 'Weekly';
        //        break;
        //    case 'P':
        //        rateType = 'Period';
        //        break;
        //    case 'M':
        //        rateType = 'Monthly';
        //        break;

        //}
        rateType = "Period";
        const extendedColumn: any = $form.find('.' + gridType + 'grid [data-browsedatafield="' + rateType + 'Extended"]');
        const discountColumn: any = $form.find('.' + gridType + 'grid [data-browsedatafield="' + rateType + 'DiscountAmount"]');
        const taxColumn: any = $form.find('.' + gridType + 'grid [data-browsedatafield="' + rateType + 'Tax"]');

        for (let i = 1; i < extendedColumn.length; i++) {
            // Extended Column
            let inputValueFromExtended: any = +extendedColumn.eq(i).attr('data-originalvalue');
            extendedTotal = extendedTotal.plus(inputValueFromExtended);
            // DiscountAmount Column
            let inputValueFromDiscount: any = +discountColumn.eq(i).attr('data-originalvalue');
            discountTotal = discountTotal.plus(inputValueFromDiscount);
            // Tax Column
            let inputValueFromTax: any = +taxColumn.eq(i).attr('data-originalvalue');
            taxTotal = taxTotal.plus(inputValueFromTax);
        };

        subTotal = extendedTotal.toFixed(2);
        discount = discountTotal.toFixed(2);
        salesTax = taxTotal.toFixed(2);
        grossTotal = extendedTotal.plus(discountTotal).toFixed(2);
        total = taxTotal.plus(extendedTotal).toFixed(2);

        $form.find('.' + gridType + 'totals [data-totalfield="SubTotal"] input').val(subTotal);
        $form.find('.' + gridType + 'totals [data-totalfield="Discount"] input').val(discount);
        $form.find('.' + gridType + 'totals [data-totalfield="Tax"] input').val(salesTax);
        $form.find('.' + gridType + 'totals [data-totalfield="GrossTotal"] input').val(grossTotal);
        $form.find('.' + gridType + 'totals [data-totalfield="Total"] input').val(total);
    };
    //----------------------------------------------------------------------------------------------
    events($form: any) {
        $form.find('div[data-datafield="VendorId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="RateType"]', $tr.find('.field[data-formdatafield="DefaultRate"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="DefaultRate"]').attr('data-originalvalue'));
        });

        //Populate tax info fields with validation
        $form.find('div[data-datafield="TaxOptionId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="RentalTaxRate1"]', $tr.find('.field[data-browsedatafield="RentalTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="SalesTaxRate1"]', $tr.find('.field[data-browsedatafield="SalesTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="LaborTaxRate1"]', $tr.find('.field[data-browsedatafield="LaborTaxRate1"]').attr('data-originalvalue'));
        });
    };

    //----------------------------------------------------------------------------------------------
    afterSave($form) { };
};
//----------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{4BB0AB54-641E-4638-89B4-0F9BFE88DF82}'] = function (e) {
    var $form, $receiveFromVendorForm;
    try {
        $form = jQuery(this).closest('.fwform');
        var mode = 'EDIT';
        var purchaseOrderInfo: any = {};
        purchaseOrderInfo.PurchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        purchaseOrderInfo.PurchaseOrderNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
        $receiveFromVendorForm = ReceiveFromVendorController.openForm(mode, purchaseOrderInfo);
        FwModule.openSubModuleTab($form, $receiveFromVendorForm);
        jQuery('.tab.submodule.active').find('.caption').html('Receive From Vendor'); 
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{B287428E-FF45-469A-8203-3BFF18E90810}'] = function (e) {
    let $form, $returnToVendorForm;
    try {
        $form = jQuery(this).closest('.fwform');
        let mode = 'EDIT';
        let purchaseOrderInfo: any = {};
        purchaseOrderInfo.PurchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        purchaseOrderInfo.PurchaseOrderNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
        $returnToVendorForm = ReturnToVendorController.openForm(mode, purchaseOrderInfo);
        FwModule.openSubModuleTab($form, $returnToVendorForm);
        jQuery('.tab.submodule.active').find('.caption').html('Return To Vendor');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
var PurchaseOrderController = new PurchaseOrder();