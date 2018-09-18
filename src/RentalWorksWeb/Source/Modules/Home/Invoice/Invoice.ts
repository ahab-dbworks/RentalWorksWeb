routes.push({ pattern: /^module\/invoice$/, action: function (match: RegExpExecArray) { return InvoiceController.getModuleScreen(); } });
routes.push({ pattern: /^module\/invoice\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return InvoiceController.getModuleScreen(filter); } });

//----------------------------------------------------------------------------------------------
class Invoice {
    Module: string = 'Invoice';
    apiurl: string = 'api/v1/invoice';
    caption: string = 'Invoice';
    ActiveView: string = 'ALL';

    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        var self = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
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
        // Changes text color to light gray if void
        FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
            if (dt.Rows[rowIndex][dt.ColumnIndex['Status']] === 'VOID') {
                $tr.css('color', '#aaaaaa');
            }
        });
        //FwBrowse.addLegend($browse, 'On Hold', '#EA300F');
        //FwBrowse.addLegend($browse, 'No Charge', '#FF8040');
        //FwBrowse.addLegend($browse, 'Late', '#FFB3D9');
        //FwBrowse.addLegend($browse, 'Foreign Currency', '#95FFCA');
        //FwBrowse.addLegend($browse, 'Multi-Warehouse', '#D6E180');
        //FwBrowse.addLegend($browse, 'Repair', '#5EAEAE');
        //FwBrowse.addLegend($browse, 'L&D', '#400040');

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
        var $form;

        $form = jQuery(jQuery(`#tmpl-modules-${this.Module}Form`).html());
        $form = FwModule.openForm($form, mode);

        FwFormField.disable($form.find('[data-datafield="SubRent"]'));
        FwFormField.disable($form.find('[data-datafield="SubSale"]'));
        FwFormField.disable($form.find('[data-datafield="SubLabor"]'));
        FwFormField.disable($form.find('[data-datafield="SubMiscellaneous"]'));
        FwFormField.disable($form.find('[data-datafield="SubVehicle"]'));

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');

            //const today = FwFunc.getDate();
            //const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            //const office = JSON.parse(sessionStorage.getItem('location'));
            //const department = JSON.parse(sessionStorage.getItem('department'));
            //const usersid = sessionStorage.getItem('usersid');  // J. Pace 7/09/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
            //const name = sessionStorage.getItem('name');

            //$form.find('div[data-datafield="Rental"] input').prop('checked', true);
            //$form.find('div[data-datafield="Sales"] input').prop('checked', true);
            //$form.find('div[data-datafield="Parts"] input').prop('checked', true);
            //FwFormField.setValue($form, 'div[data-datafield="ProjectManagerId"]', usersid, name);
            //FwFormField.setValue($form, 'div[data-datafield="AgentId"]', usersid, name);
            //$form.find('div[data-datafield="Labor"] input').prop('checked', true);
            //FwFormField.setValueByDataField($form, 'PurchaseOrderDate', today);

            //FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            //FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
            //FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
            //FwFormField.setValue($form, 'div[data-datafield="PoTypeId"]', this.DefaultPurchasePoTypeId, this.DefaultPurchasePoType);

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
        } else {
            FwFormField.disable($form.find('.ifnew'));
        }

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

        //FwFormField.disable($form.find('[data-datafield="RentalTaxRate1"]'));
        //FwFormField.disable($form.find('[data-datafield="SalesTaxRate1"]'));
        //FwFormField.disable($form.find('[data-datafield="LaborTaxRate1"]'));

        //this.events($form);
        //this.activityCheckboxEvents($form, mode);

        return $form;
    };

    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="InvoiceId"] input').val(uniqueids.InvoiceId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };

    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };

    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        //const maxPageSize = 9999;

        //let $orderStatusHistoryGrid;
        //let $orderStatusHistoryGridControl;
        //$orderStatusHistoryGrid = $form.find('div[data-grid="OrderStatusHistoryGrid"]');
        //$orderStatusHistoryGridControl = jQuery(jQuery('#tmpl-grids-OrderStatusHistoryGridBrowse').html());
        //$orderStatusHistoryGrid.empty().append($orderStatusHistoryGridControl);
        //$orderStatusHistoryGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
        //    };
        //});
        //FwBrowse.init($orderStatusHistoryGridControl);
        //FwBrowse.renderRuntimeHtml($orderStatusHistoryGridControl);

        //let $orderItemGridRental;
        //let $orderItemGridRentalControl;
        //$orderItemGridRental = $form.find('.rentalgrid div[data-grid="OrderItemGrid"]');
        //$orderItemGridRentalControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        //$orderItemGridRentalControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        //$orderItemGridRentalControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        //$orderItemGridRentalControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        //$orderItemGridRental.empty().append($orderItemGridRentalControl);
        //$orderItemGridRentalControl.data('isSummary', false);
        //$orderItemGridRental.addClass('R');

        //$orderItemGridRentalControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
        //        RecType: 'R'
        //    };
        //    request.pagesize = maxPageSize;
        //});
        //$orderItemGridRentalControl.data('beforesave', request => {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        //    request.RecType = 'R';
        //});

        //FwBrowse.addEventHandler($orderItemGridRentalControl, 'afterdatabindcallback', () => {
        //    this.calculateOrderItemGridTotals($form, 'rental');
        //    let rentalItems = $form.find('.rentalgrid tbody').children();
        //    rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
        //});

        //FwBrowse.init($orderItemGridRentalControl);
        //FwBrowse.renderRuntimeHtml($orderItemGridRentalControl);

        //let $orderItemGridSales;
        //let $orderItemGridSalesControl;
        //$orderItemGridSales = $form.find('.salesgrid div[data-grid="OrderItemGrid"]');
        //$orderItemGridSalesControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        //$orderItemGridSalesControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        //$orderItemGridSalesControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        //$orderItemGridSalesControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        //$orderItemGridSales.empty().append($orderItemGridSalesControl);
        //$orderItemGridSales.addClass('S');
        //$orderItemGridSalesControl.data('isSummary', false);

        //$orderItemGridSalesControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
        //        RecType: 'S'
        //    };
        //    request.pagesize = maxPageSize;
        //});
        //$orderItemGridSalesControl.data('beforesave', request => {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        //    request.RecType = 'S';
        //});
        //FwBrowse.addEventHandler($orderItemGridSalesControl, 'afterdatabindcallback', () => {
        //    this.calculateOrderItemGridTotals($form, 'sales');
        //    let salesItems = $form.find('.salesgrid tbody').children();
        //    salesItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Sales"]')) : FwFormField.enable($form.find('[data-datafield="Sales"]'));
        //});

        //FwBrowse.init($orderItemGridSalesControl);
        //FwBrowse.renderRuntimeHtml($orderItemGridSalesControl);

        //let $orderItemGridPart;
        //let $orderItemGridPartControl;
        //$orderItemGridPart = $form.find('.partgrid div[data-grid="OrderItemGrid"]');
        //$orderItemGridPartControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        //$orderItemGridPartControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        //$orderItemGridPartControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        //$orderItemGridPartControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        //$orderItemGridPart.empty().append($orderItemGridPartControl);
        //$orderItemGridPart.addClass('P');
        //$orderItemGridPartControl.data('isSummary', false);

        //$orderItemGridPartControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
        //        RecType: 'P'
        //    };
        //    request.pagesize = maxPageSize;
        //});
        //$orderItemGridPartControl.data('beforesave', request => {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        //    request.RecType = 'P';
        //});
        //FwBrowse.addEventHandler($orderItemGridPartControl, 'afterdatabindcallback', () => {
        //    this.calculateOrderItemGridTotals($form, 'part');
        //    let partItems = $form.find('.partgrid tbody').children();
        //    partItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Parts"]')) : FwFormField.enable($form.find('[data-datafield="Parts"]'));
        //});

        //FwBrowse.init($orderItemGridPartControl);
        //FwBrowse.renderRuntimeHtml($orderItemGridPartControl);

        //let $orderItemGridLabor;
        //let $orderItemGridLaborControl;
        //$orderItemGridLabor = $form.find('.laborgrid div[data-grid="OrderItemGrid"]');
        //$orderItemGridLaborControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        //$orderItemGridLaborControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        //$orderItemGridLaborControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        //$orderItemGridLaborControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        //$orderItemGridLabor.empty().append($orderItemGridLaborControl);
        //$orderItemGridLabor.addClass('L');
        //$orderItemGridLaborControl.data('isSummary', false);

        //$orderItemGridLaborControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
        //        RecType: 'L'
        //    };
        //    request.pagesize = maxPageSize;
        //});
        //$orderItemGridLaborControl.data('beforesave', request => {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        //    request.RecType = 'L';
        //});
        //FwBrowse.addEventHandler($orderItemGridLaborControl, 'afterdatabindcallback', () => {
        //    this.calculateOrderItemGridTotals($form, 'labor');
        //    let laborItems = $form.find('.laborgrid tbody').children();
        //    laborItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Labor"]')) : FwFormField.enable($form.find('[data-datafield="Labor"]'));
        //});

        //FwBrowse.init($orderItemGridLaborControl);
        //FwBrowse.renderRuntimeHtml($orderItemGridLaborControl);

        //let $orderItemGridMisc;
        //let $orderItemGridMiscControl;
        //$orderItemGridMisc = $form.find('.miscgrid div[data-grid="OrderItemGrid"]');
        //$orderItemGridMiscControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        //$orderItemGridMiscControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        //$orderItemGridMiscControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        //$orderItemGridMiscControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        //$orderItemGridMisc.empty().append($orderItemGridMiscControl);
        //$orderItemGridMisc.addClass('M');
        //$orderItemGridMiscControl.data('isSummary', false);

        //$orderItemGridMiscControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
        //        RecType: 'M'
        //    };
        //    request.pagesize = maxPageSize;
        //});
        //$orderItemGridMiscControl.data('beforesave', request => {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        //    request.RecType = 'M';
        //});
        //FwBrowse.addEventHandler($orderItemGridMiscControl, 'afterdatabindcallback', () => {
        //    this.calculateOrderItemGridTotals($form, 'misc');
        //    let miscItems = $form.find('.miscgrid tbody').children();
        //    miscItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Miscellaneous"]')) : FwFormField.enable($form.find('[data-datafield="Miscellaneous"]'));
        //});

        //FwBrowse.init($orderItemGridMiscControl);
        //FwBrowse.renderRuntimeHtml($orderItemGridMiscControl);

        //let $orderItemGridSubRent;
        //let $orderItemGridSubRentControl;
        //$orderItemGridSubRent = $form.find('.subrentalgrid div[data-grid="OrderItemGrid"]');
        //$orderItemGridSubRentControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        //$orderItemGridSubRentControl.find('.suborder').attr('data-visible', 'true');
        //$orderItemGridSubRentControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        //$orderItemGridSubRentControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        //$orderItemGridSubRentControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        //$orderItemGridSubRent.empty().append($orderItemGridSubRentControl);
        //$orderItemGridSubRent.addClass('R');
        //$orderItemGridSubRentControl.data('isSummary', false);

        //$orderItemGridSubRentControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
        //        RecType: 'R',
        //        Summary: true,
        //        Subs: true
        //    };
        //    request.pagesize = maxPageSize;
        //});
        //$orderItemGridSubRentControl.data('beforesave', request => {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        //    request.RecType = 'R';
        //    request.Summary = true;
        //    request.Subs = true;
        //});
        //FwBrowse.addEventHandler($orderItemGridSubRentControl, 'afterdatabindcallback', () => {
        //    this.calculateOrderItemGridTotals($form, 'subrental');
        //    let subrentItems = $form.find('.subrentalgrid tbody').children();
        //    subrentItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubRent"]')) : FwFormField.enable($form.find('[data-datafield="SubRent"]'));
        //});

        //FwBrowse.init($orderItemGridSubRentControl);
        //FwBrowse.renderRuntimeHtml($orderItemGridSubRentControl);

        //let $oderItemGridSubSales;
        //let $oderItemGridSubSalesControl;
        //$oderItemGridSubSales = $form.find('.subsalesgrid div[data-grid="OrderItemGrid"]');
        //$oderItemGridSubSalesControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        //$oderItemGridSubSalesControl.find('.suborder').attr('data-visible', 'true');
        //$oderItemGridSubSalesControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        //$oderItemGridSubSalesControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        //$oderItemGridSubSalesControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        //$oderItemGridSubSales.empty().append($oderItemGridSubSalesControl);
        //$oderItemGridSubSales.addClass('S');
        //$oderItemGridSubSalesControl.data('isSummary', false);

        //$oderItemGridSubSalesControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
        //        RecType: 'S',
        //        Summary: true,
        //        Subs: true
        //    };
        //    request.pagesize = maxPageSize;
        //});
        //$oderItemGridSubSalesControl.data('beforesave', request => {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        //    request.RecType = 'S';
        //    request.Summary = true;
        //    request.Subs = true;
        //});
        //FwBrowse.addEventHandler($oderItemGridSubSalesControl, 'afterdatabindcallback', () => {
        //    this.calculateOrderItemGridTotals($form, 'subsales');
        //    let subsalesItems = $form.find('.subsalesgrid tbody').children();
        //    subsalesItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubSale"]')) : FwFormField.enable($form.find('[data-datafield="SubSale"]'));
        //});

        //FwBrowse.init($oderItemGridSubSalesControl);
        //FwBrowse.renderRuntimeHtml($oderItemGridSubSalesControl);

        //let $orderItemGridSubLabor;
        //let $orderItemGridSubLaborControl;
        //$orderItemGridSubLabor = $form.find('.sublaborgrid div[data-grid="OrderItemGrid"]');
        //$orderItemGridSubLaborControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        //$orderItemGridSubLaborControl.find('.suborder').attr('data-visible', 'true');
        //$orderItemGridSubLaborControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        //$orderItemGridSubLaborControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        //$orderItemGridSubLaborControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        //$orderItemGridSubLabor.empty().append($orderItemGridSubLaborControl);
        //$orderItemGridSubLabor.addClass('L');
        //$orderItemGridSubLaborControl.data('isSummary', false);

        //$orderItemGridSubLaborControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
        //        RecType: 'L',
        //        Summary: true,
        //        Subs: true
        //    };
        //    request.pagesize = maxPageSize;
        //});
        //$orderItemGridSubLaborControl.data('beforesave', request => {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        //    request.RecType = 'L';
        //    request.Summary = true;
        //    request.Subs = true;
        //});
        //FwBrowse.addEventHandler($orderItemGridSubLaborControl, 'afterdatabindcallback', () => {
        //    this.calculateOrderItemGridTotals($form, 'sublabor');
        //    let sublaborItems = $form.find('.sublaborgrid tbody').children();
        //    sublaborItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubLabor"]')) : FwFormField.enable($form.find('[data-datafield="SubLabor"]'));
        //});

        //FwBrowse.init($orderItemGridSubLaborControl);
        //FwBrowse.renderRuntimeHtml($orderItemGridSubLaborControl);

        //let $orderItemGridSubMisc;
        //let $orderItemGridSubMiscControl;
        //$orderItemGridSubMisc = $form.find('.submiscgrid div[data-grid="OrderItemGrid"]');
        //$orderItemGridSubMiscControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        //$orderItemGridSubMiscControl.find('.suborder').attr('data-visible', 'true');
        //$orderItemGridSubMiscControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        //$orderItemGridSubMiscControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        //$orderItemGridSubMiscControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        //$orderItemGridSubMisc.empty().append($orderItemGridSubMiscControl);
        //$orderItemGridSubMisc.addClass('R');
        //$orderItemGridSubMiscControl.data('isSummary', false);

        //$orderItemGridSubMiscControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
        //        RecType: 'R',
        //        Summary: true,
        //        Subs: true
        //    };
        //    request.pagesize = maxPageSize;
        //});
        //$orderItemGridSubMiscControl.data('beforesave', request => {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        //    request.RecType = 'R';
        //    request.Summary = true;
        //    request.Subs = true;
        //});
        //FwBrowse.addEventHandler($orderItemGridSubMiscControl, 'afterdatabindcallback', () => {
        //    this.calculateOrderItemGridTotals($form, 'submisc');
        //    let submiscItems = $form.find('.submiscgrid tbody').children();
        //    submiscItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubMisc"]')) : FwFormField.enable($form.find('[data-datafield="SubMisc"]'));
        //});

        //FwBrowse.init($orderItemGridSubMiscControl);
        //FwBrowse.renderRuntimeHtml($orderItemGridSubMiscControl);

        //let $orderNoteGrid;
        //let $orderNoteGridControl;
        //$orderNoteGrid = $form.find('div[data-grid="OrderNoteGrid"]');
        //$orderNoteGridControl = jQuery(jQuery('#tmpl-grids-OrderNoteGridBrowse').html());
        //$orderNoteGrid.empty().append($orderNoteGridControl);
        //$orderNoteGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
        //    };
        //});
        //$orderNoteGridControl.data('beforesave', request => {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId')
        //});
        //FwBrowse.init($orderNoteGridControl);
        //FwBrowse.renderRuntimeHtml($orderNoteGridControl);
    };

    //----------------------------------------------------------------------------------------------
    loadAudit($form: any): void {
        let uniqueid = FwFormField.getValueByDataField($form, 'InvoiceId');
        FwModule.loadAudit($form, uniqueid);
    };

    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        //if (!FwFormField.getValueByDataField($form, 'Rental')) { $form.find('[data-type="tab"][data-caption="Rental"]').hide() }
        //if (!FwFormField.getValueByDataField($form, 'Sales')) { $form.find('[data-type="tab"][data-caption="Sales"]').hide() }
        //if (!FwFormField.getValueByDataField($form, 'Miscellaneous')) { $form.find('[data-type="tab"][data-caption="Misc"]').hide() }
        //if (!FwFormField.getValueByDataField($form, 'Labor')) { $form.find('[data-type="tab"][data-caption="Labor"]').hide() }
        //if (!FwFormField.getValueByDataField($form, 'Parts')) { $form.find('[data-type="tab"][data-caption="Parts"]').hide() }
        //if (!FwFormField.getValueByDataField($form, 'SubRent')) { $form.find('[data-type="tab"][data-caption="Sub-Rental"]').hide() }
        //if (!FwFormField.getValueByDataField($form, 'SubSale')) { $form.find('[data-type="tab"][data-caption="Sub-Sales"]').hide() }
        //if (!FwFormField.getValueByDataField($form, 'SubMiscellaneous')) { $form.find('[data-type="tab"][data-caption="Sub-Misc"]').hide() }
        //if (!FwFormField.getValueByDataField($form, 'SubLabor')) { $form.find('[data-type="tab"][data-caption="Sub-Labor"]').hide() }

        //let $orderItemGridRental;
        //$orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        ////FwBrowse.search($orderItemGridRental);
        //let $orderItemGridSales;
        //$orderItemGridSales = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        ////FwBrowse.search($orderItemGridSales);
        //let $orderItemGridPart;
        //$orderItemGridPart = $form.find('.partgrid [data-name="OrderItemGrid"]');
        ////FwBrowse.search($orderItemGridPart);
        //let $orderItemGridLabor;
        //$orderItemGridLabor = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        ////FwBrowse.search($orderItemGridLabor);
        //let $orderItemGridMisc;
        //$orderItemGridMisc = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        ////FwBrowse.search($orderItemGridMisc);
        //let $orderItemGridSubRent;
        //$orderItemGridSubRent = $form.find('.subrentalgrid [data-name="OrderItemGrid"]');
        ////FwBrowse.search($orderItemGridSubRent);
        //let $orderItemGridSubSales;
        //$orderItemGridSubSales = $form.find('.subsalesgrid [data-name="OrderItemGrid"]');
        //let $orderItemGridSubLabor;
        //$orderItemGridSubLabor = $form.find('.sublaborgrid [data-name="OrderItemGrid"]');
        //let $orderItemGridSubMisc;
        //$orderItemGridSubMisc = $form.find('.submiscgrid [data-name="OrderItemGrid"]');
        ////FwBrowse.search($orderItemGridSubRent);
        //let $orderNoteGrid;
        //$orderNoteGrid = $form.find('[data-name="OrderNoteGrid"]');
        ////FwBrowse.search($orderNoteGrid);

        ////Click Event on tabs to load grids/browses
        //$form.on('click', '[data-type="tab"]', e => {
        //    let tabname = jQuery(e.currentTarget).attr('id');
        //    let lastIndexOfTab = tabname.lastIndexOf('tab');
        //    let tabpage = tabname.substring(0, lastIndexOfTab) + 'tabpage' + tabname.substring(lastIndexOfTab + 3);

        //    let $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
        //    if ($gridControls.length > 0) {
        //        for (let i = 0; i < $gridControls.length; i++) {
        //            let $gridcontrol = jQuery($gridControls[i]);
        //            FwBrowse.search($gridcontrol);
        //        }
        //    }

        //    let $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
        //    if ($browseControls.length > 0) {
        //        for (let i = 0; i < $browseControls.length; i++) {
        //            let $browseControl = jQuery($browseControls[i]);
        //            FwBrowse.search($browseControl);
        //        }
        //    }
        //});

        //$orderItemGridRental.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        //$orderItemGridSales.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        //$orderItemGridPart.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        //$orderItemGridLabor.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        //$orderItemGridMisc.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        //$orderItemGridSubRent.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        //$orderItemGridSubSales.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        //$orderItemGridSubLabor.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        //$orderItemGridSubMisc.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        //$orderItemGridSubRent.find('.submenu-btn').filter('[data-securityid="89AD5560-637A-4ECF-B7EA-33A462F6B137"]').hide();
        //$orderItemGridSubSales.find('.submenu-btn').filter('[data-securityid="89AD5560-637A-4ECF-B7EA-33A462F6B137"]').hide();
        //$orderItemGridSubLabor.find('.submenu-btn').filter('[data-securityid="89AD5560-637A-4ECF-B7EA-33A462F6B137"]').hide();
        //$orderItemGridSubMisc.find('.submenu-btn').filter('[data-securityid="89AD5560-637A-4ECF-B7EA-33A462F6B137"]').hide();

        //$orderItemGridSubRent.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();
        //$orderItemGridSubSales.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();
        //$orderItemGridSubLabor.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();
        //$orderItemGridSubMisc.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();

        //$orderItemGridSubRent.find('.buttonbar').hide();
        //$orderItemGridSubSales.find('.buttonbar').hide();
        //$orderItemGridSubLabor.find('.buttonbar').hide();
        //$orderItemGridSubMisc.find('.buttonbar').hide();




        //this.dynamicColumns($form);
    };

    //----------------------------------------------------------------------------------------------
    activityCheckboxEvents($form, mode) {
        const rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]')
            , salesTab = $form.find('[data-type="tab"][data-caption="Sales"]')
            , partsTab = $form.find('[data-type="tab"][data-caption="Parts"]')
            , miscTab = $form.find('[data-type="tab"][data-caption="Misc"]')
            , laborTab = $form.find('[data-type="tab"][data-caption="Labor"]')
            , subrentalTab = $form.find('[data-type="tab"][data-caption="Sub-Rental"]')
            , subsalesTab = $form.find('[data-type="tab"][data-caption="Sub-Sales"]')
            , submiscTab = $form.find('[data-type="tab"][data-caption="Sub-Misc"]')
            , sublaborTab = $form.find('[data-type="tab"][data-caption="Sub-Labor"]');
        $form.find('[data-datafield="Rental"] input').on('change', e => {
            if (mode == "NEW") {
                if (jQuery(e.currentTarget).prop('checked')) {
                    rentalTab.show();
                } else {
                    rentalTab.hide();
                }
            } else {
                if (jQuery(e.currentTarget).prop('checked')) {
                    rentalTab.show();
                    FwFormField.disable($form.find('[data-datafield="RentalSale"]'));
                } else {
                    rentalTab.hide();
                    FwFormField.enable($form.find('[data-datafield="RentalSale"]'));
                }
            }
        });

        $form.find('[data-datafield="Sales"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? salesTab.show() : salesTab.hide();
        });
        $form.find('[data-datafield="Parts"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? partsTab.show() : partsTab.hide();
        });
        $form.find('[data-datafield="Miscellaneous"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? miscTab.show() : miscTab.hide();
        });
        $form.find('[data-datafield="Labor"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? laborTab.show() : laborTab.hide();
        });
        $form.find('[data-datafield="SubRent"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? subrentalTab.show() : subrentalTab.hide();
        });
        $form.find('[data-datafield="SubSale"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? subsalesTab.show() : subsalesTab.hide();
        });
        $form.find('[data-datafield="SubMiscellaneous"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? submiscTab.show() : submiscTab.hide();
        });
        $form.find('[data-datafield="SubLabor"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? sublaborTab.show() : sublaborTab.hide();
        });
    };

    //----------------------------------------------------------------------------------------------
    dynamicColumns($form) {
        const PoType = FwFormField.getValueByDataField($form, "PoTypeId"),
            $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
            $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]'),
            $partGrid = $form.find('.partgrid [data-name="OrderItemGrid"]'),
            $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]'),
            $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]'),
            $subrentalGrid = $form.find('.subrentalgrid [data-name="OrderItemGrid"]'),
            $subsalesGrid = $form.find('.subsalesgrid [data-name="OrderItemGrid"]'),
            $sublaborGrid = $form.find('.sublaborgrid [data-name="OrderItemGrid"]'),
            $submiscGrid = $form.find('.submiscgrid [data-name="OrderItemGrid"]'),
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
                jQuery($laborGrid.find('[data-mappedfield="' + hiddenFields[i] + '"]')).parent().hide();
                jQuery($miscGrid.find('[data-mappedfield="' + hiddenFields[i] + '"]')).parent().hide();
                jQuery($subrentalGrid.find('[data-mappedfield="' + hiddenFields[i] + '"]')).parent().hide();
                jQuery($subsalesGrid.find('[data-mappedfield="' + hiddenFields[i] + '"]')).parent().hide();
                jQuery($sublaborGrid.find('[data-mappedfield="' + hiddenFields[i] + '"]')).parent().hide();
                jQuery($submiscGrid.find('[data-mappedfield="' + hiddenFields[i] + '"]')).parent().hide();
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
        //$form.find('div[data-datafield="DealId"]').data('onchange', $tr => {
        //    FwFormField.setValue($form, 'div[data-datafield="RateType"]', $tr.find('.field[data-formdatafield="DefaultRate"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="DefaultRate"]').attr('data-originalvalue'));
        //    FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', $tr.find('.field[data-browsedatafield="BillingCycleId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="BillingCycle"]').attr('data-originalvalue'));
        //});

        //Populate tax info fields with validation
        $form.find('div[data-datafield="TaxOptionId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="RentalTaxRate1"]', $tr.find('.field[data-browsedatafield="RentalTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="SalesTaxRate1"]', $tr.find('.field[data-browsedatafield="SalesTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="LaborTaxRate1"]', $tr.find('.field[data-browsedatafield="LaborTaxRate1"]').attr('data-originalvalue'));
        });
    };
    //----------------------------------------------------------------------------------------------
    afterSave($form) { };
    //----------------------------------------------------------------------------------------------
    voidInvoice($form: any): void {
        var self = this;
        let $confirmation, $yes, $no;
        $confirmation = FwConfirmation.renderConfirmation('Void', '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');
        let html = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div>Would you like to void this Invoice?</div>');
        html.push('  </div>');
        html.push('</div>');

        FwConfirmation.addControls($confirmation, html.join(''));
        $yes = FwConfirmation.addButton($confirmation, 'Void', false);
        $no = FwConfirmation.addButton($confirmation, 'Cancel');

        $yes.on('click', makeVoid);

        function makeVoid() {
            let request: any = {};
            const invoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Voiding...');
            $yes.off('click');

            FwAppData.apiMethod(true, 'POST', `api/v1/invoice/void/${invoiceId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Invoice Successfully Voided');
                FwConfirmation.destroyConfirmation($confirmation);
                FwModule.refreshForm($form, self);
            }, function onError(response) {
                $yes.on('click', makeVoid);
                $yes.text('Void');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwModule.refreshForm($form, self);
            }, $form);
        };
    }
};

//----------------------------------------------------------------------------------------------
// Void Invoice - Form
FwApplicationTree.clickEvents['{DF6B0708-EC5A-475F-8EFB-B52E30BACAA3}'] = function (e) {
    let $form;
    $form = jQuery(this).closest('.fwform');
    try {
        InvoiceController.voidInvoice($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
// Void Invoice - Browse
FwApplicationTree.clickEvents['{DACF4B06-DE63-4867-A684-4C77199D6961}'] = function (e) {
    let $browse;
    $browse = jQuery(this).closest('.fwbrowse');

    try {
        const invoiceId = $browse.find('.selected [data-browsedatafield="InvoiceId"]').attr('data-originalvalue');
        if (invoiceId != null) {
            var self = this;
            let $confirmation, $yes, $no;
            $confirmation = FwConfirmation.renderConfirmation('Void', '');
            $confirmation.find('.fwconfirmationbox').css('width', '450px');
            let html = [];
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div>Would you like to void this Invoice?</div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));
            $yes = FwConfirmation.addButton($confirmation, 'Void', false);
            $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', makeVoid);

            function makeVoid() {
                let request: any = {};

                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Voiding...');
                $yes.off('click');

                FwAppData.apiMethod(true, 'POST', `api/v1/invoice/void/${invoiceId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Invoice Successfully Voided');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwBrowse.databind($browse);
                }, function onError(response) {
                    $yes.on('click', makeVoid);
                    $yes.text('Void');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                    FwBrowse.databind($browse);
                }, $browse);
            };
        } else {
            FwNotification.renderNotification('WARNING', 'Select an Invoice to void.');
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
var InvoiceController = new Invoice();