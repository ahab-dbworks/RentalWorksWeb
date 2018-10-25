routes.push({ pattern: /^module\/purchaseorder$/, action: function (match: RegExpExecArray) { return PurchaseOrderController.getModuleScreen(); } });
routes.push({ pattern: /^module\/purchaseorder\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return PurchaseOrderController.getModuleScreen(filter); } });

//----------------------------------------------------------------------------------------------
class PurchaseOrder {
    Module: string = 'PurchaseOrder';
    apiurl: string = 'api/v1/purchaseorder';
    caption: string = 'Purchase Order';
    nav: string = 'module/purchaseorder';
    id: string = '67D8C8BB-CF55-4231-B4A2-BB308ADF18F0';
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
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
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

            // Activity checkbox and tab behavior
            $form.find('div[data-datafield="Rental"] input').prop('checked', true);
            $form.find('div[data-datafield="Sales"] input').prop('checked', true);
            $form.find('div[data-datafield="Parts"] input').prop('checked', true);
            $form.find('[data-type="tab"][data-caption="Misc"]').hide();
            $form.find('[data-type="tab"][data-caption="Labor"]').hide();
            $form.find('[data-type="tab"][data-caption="Sub-Rental"]').hide();
            $form.find('[data-type="tab"][data-caption="Sub-Sales"]').hide();
            $form.find('[data-type="tab"][data-caption="Sub-Misc"]').hide();
            $form.find('[data-type="tab"][data-caption="Sub-Labor"]').hide();

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
                OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
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
        $orderItemGridRentalControl.find('div[data-datafield="PeriodExtended"]').attr('data-formreadonly', 'true');


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

        let $orderItemGridLabor;
        let $orderItemGridLaborControl;
        $orderItemGridLabor = $form.find('.laborgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridLaborControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridLaborControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        $orderItemGridLaborControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridLaborControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridLabor.empty().append($orderItemGridLaborControl);
        $orderItemGridLabor.addClass('L');
        $orderItemGridLaborControl.data('isSummary', false);

        $orderItemGridLaborControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                RecType: 'L'
            };
            request.pagesize = maxPageSize;
        });
        $orderItemGridLaborControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'L';
        });
        FwBrowse.addEventHandler($orderItemGridLaborControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'labor');
            let laborItems = $form.find('.laborgrid tbody').children();
            laborItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Labor"]')) : FwFormField.enable($form.find('[data-datafield="Labor"]'));
        });

        FwBrowse.init($orderItemGridLaborControl);
        FwBrowse.renderRuntimeHtml($orderItemGridLaborControl);

        let $orderItemGridMisc;
        let $orderItemGridMiscControl;
        $orderItemGridMisc = $form.find('.miscgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridMiscControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridMiscControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        $orderItemGridMiscControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridMiscControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridMisc.empty().append($orderItemGridMiscControl);
        $orderItemGridMisc.addClass('M');
        $orderItemGridMiscControl.data('isSummary', false);

        $orderItemGridMiscControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                RecType: 'M'
            };
            request.pagesize = maxPageSize;
        });
        $orderItemGridMiscControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'M';
        });
        FwBrowse.addEventHandler($orderItemGridMiscControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'misc');
            let miscItems = $form.find('.miscgrid tbody').children();
            miscItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Miscellaneous"]')) : FwFormField.enable($form.find('[data-datafield="Miscellaneous"]'));
        });

        FwBrowse.init($orderItemGridMiscControl);
        FwBrowse.renderRuntimeHtml($orderItemGridMiscControl);

        let $orderItemGridSubRent;
        let $orderItemGridSubRentControl;
        $orderItemGridSubRent = $form.find('.subrentalgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridSubRentControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridSubRentControl.find('.suborder').attr('data-visible', 'true');
        $orderItemGridSubRentControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        $orderItemGridSubRentControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridSubRentControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridSubRent.empty().append($orderItemGridSubRentControl);
        $orderItemGridSubRent.addClass('R');
        $orderItemGridSubRentControl.data('isSummary', false);

        $orderItemGridSubRentControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                RecType: 'R',
                Summary: true,
                Subs: true
            };
            request.pagesize = maxPageSize;
        });
        $orderItemGridSubRentControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'R';
            request.Summary = true;
            request.Subs = true;
        });
        FwBrowse.addEventHandler($orderItemGridSubRentControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'subrental');
            let subrentItems = $form.find('.subrentalgrid tbody').children();
            subrentItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubRent"]')) : FwFormField.enable($form.find('[data-datafield="SubRent"]'));
        });

        FwBrowse.init($orderItemGridSubRentControl);
        FwBrowse.renderRuntimeHtml($orderItemGridSubRentControl);

        let $oderItemGridSubSales;
        let $oderItemGridSubSalesControl;
        $oderItemGridSubSales = $form.find('.subsalesgrid div[data-grid="OrderItemGrid"]');
        $oderItemGridSubSalesControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $oderItemGridSubSalesControl.find('.suborder').attr('data-visible', 'true');
        $oderItemGridSubSalesControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        $oderItemGridSubSalesControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $oderItemGridSubSalesControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $oderItemGridSubSales.empty().append($oderItemGridSubSalesControl);
        $oderItemGridSubSales.addClass('S');
        $oderItemGridSubSalesControl.data('isSummary', false);

        $oderItemGridSubSalesControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                RecType: 'S',
                Summary: true,
                Subs: true
            };
            request.pagesize = maxPageSize;
        });
        $oderItemGridSubSalesControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'S';
            request.Summary = true;
            request.Subs = true;
        });
        FwBrowse.addEventHandler($oderItemGridSubSalesControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'subsales');
            let subsalesItems = $form.find('.subsalesgrid tbody').children();
            subsalesItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubSale"]')) : FwFormField.enable($form.find('[data-datafield="SubSale"]'));
        });

        FwBrowse.init($oderItemGridSubSalesControl);
        FwBrowse.renderRuntimeHtml($oderItemGridSubSalesControl);

        let $orderItemGridSubLabor;
        let $orderItemGridSubLaborControl;
        $orderItemGridSubLabor = $form.find('.sublaborgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridSubLaborControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridSubLaborControl.find('.suborder').attr('data-visible', 'true');
        $orderItemGridSubLaborControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        $orderItemGridSubLaborControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridSubLaborControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridSubLabor.empty().append($orderItemGridSubLaborControl);
        $orderItemGridSubLabor.addClass('L');
        $orderItemGridSubLaborControl.data('isSummary', false);

        $orderItemGridSubLaborControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                RecType: 'L',
                Summary: true,
                Subs: true
            };
            request.pagesize = maxPageSize;
        });
        $orderItemGridSubLaborControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'L';
            request.Summary = true;
            request.Subs = true;
        });
        FwBrowse.addEventHandler($orderItemGridSubLaborControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'sublabor');
            let sublaborItems = $form.find('.sublaborgrid tbody').children();
            sublaborItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubLabor"]')) : FwFormField.enable($form.find('[data-datafield="SubLabor"]'));
        });

        FwBrowse.init($orderItemGridSubLaborControl);
        FwBrowse.renderRuntimeHtml($orderItemGridSubLaborControl);

        let $orderItemGridSubMisc;
        let $orderItemGridSubMiscControl;
        $orderItemGridSubMisc = $form.find('.submiscgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridSubMiscControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridSubMiscControl.find('.suborder').attr('data-visible', 'true');
        $orderItemGridSubMiscControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        $orderItemGridSubMiscControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridSubMiscControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridSubMisc.empty().append($orderItemGridSubMiscControl);
        $orderItemGridSubMisc.addClass('R');
        $orderItemGridSubMiscControl.data('isSummary', false);

        $orderItemGridSubMiscControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                RecType: 'R',
                Summary: true,
                Subs: true
            };
            request.pagesize = maxPageSize;
        });
        $orderItemGridSubMiscControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'R';
            request.Summary = true;
            request.Subs = true;
        });
        FwBrowse.addEventHandler($orderItemGridSubMiscControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'submisc');
            let submiscItems = $form.find('.submiscgrid tbody').children();
            submiscItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubMisc"]')) : FwFormField.enable($form.find('[data-datafield="SubMisc"]'));
        });

        FwBrowse.init($orderItemGridSubMiscControl);
        FwBrowse.renderRuntimeHtml($orderItemGridSubMiscControl);

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
        let status = FwFormField.getValueByDataField($form, 'Status');

        if (status === 'VOID' || status === 'CLOSED' || status === 'SNAPSHOT') {
            FwModule.setFormReadOnly($form);
        }

        if (!FwFormField.getValueByDataField($form, 'Rental')) { $form.find('[data-type="tab"][data-caption="Rental"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'Sales')) { $form.find('[data-type="tab"][data-caption="Sales"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'Miscellaneous')) { $form.find('[data-type="tab"][data-caption="Misc"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'Labor')) { $form.find('[data-type="tab"][data-caption="Labor"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'Parts')) { $form.find('[data-type="tab"][data-caption="Parts"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'SubRent')) { $form.find('[data-type="tab"][data-caption="Sub-Rental"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'SubSale')) { $form.find('[data-type="tab"][data-caption="Sub-Sales"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'Repair')) { $form.find('[data-type="tab"][data-caption="Repair"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'SubMiscellaneous')) { $form.find('[data-type="tab"][data-caption="Sub-Misc"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'SubLabor')) { $form.find('[data-type="tab"][data-caption="Sub-Labor"]').hide() }

        let $orderItemGridRental;
        $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridRental);
        let $orderItemGridSales;
        $orderItemGridSales = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridSales);
        let $orderItemGridPart;
        $orderItemGridPart = $form.find('.partgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridPart);
        let $orderItemGridLabor;
        $orderItemGridLabor = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridLabor);
        let $orderItemGridMisc;
        $orderItemGridMisc = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridMisc);
        let $orderItemGridSubRent;
        $orderItemGridSubRent = $form.find('.subrentalgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridSubRent);
        let $orderItemGridSubSales;
        $orderItemGridSubSales = $form.find('.subsalesgrid [data-name="OrderItemGrid"]');
        let $orderItemGridSubLabor;
        $orderItemGridSubLabor = $form.find('.sublaborgrid [data-name="OrderItemGrid"]');
        let $orderItemGridSubMisc;
        $orderItemGridSubMisc = $form.find('.submiscgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridSubRent);
        let $orderNoteGrid;
        $orderNoteGrid = $form.find('[data-name="OrderNoteGrid"]');
        //FwBrowse.search($orderNoteGrid);

        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"]', e => {
            let $tab = jQuery(e.currentTarget);
            let tabname = $tab.attr('id');
            let lastIndexOfTab = tabname.lastIndexOf('tab');  // for cases where "tab" is included in the name of the tab
            let tabpage = tabname.substring(0, lastIndexOfTab) + 'tabpage' + tabname.substring(lastIndexOfTab + 3);

            if ($tab.hasClass('audittab') == false) {
                let $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
                if (($tab.hasClass('tabGridsLoaded') == false) && $gridControls.length > 0) {
                    for (let i = 0; i < $gridControls.length; i++) {
                        try {
                            let $gridcontrol = jQuery($gridControls[i]);
                            FwBrowse.search($gridcontrol);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                }

                let $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
                if (($tab.hasClass('tabGridsLoaded') == false) && $browseControls.length > 0) {
                    for (let i = 0; i < $browseControls.length; i++) {
                        let $browseControl = jQuery($browseControls[i]);
                        FwBrowse.search($browseControl);
                    }
                }
            }
            $tab.addClass('tabGridsLoaded');
        });
        // Display D/W field in subrental tab
        if (FwFormField.getValueByDataField($form, 'RateType') === 'DAILY') {
            $form.find(".subRentalDaysPerWeek").show();
        } else {
            $form.find(".subRentalDaysPerWeek").hide();
        }

        $orderItemGridRental.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();
        $orderItemGridSales.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();
        $orderItemGridPart.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();
        $orderItemGridLabor.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();
        $orderItemGridMisc.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();
        $orderItemGridSubRent.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="89AD5560-637A-4ECF-B7EA-33A462F6B137"]').hide();
        $orderItemGridSubSales.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="89AD5560-637A-4ECF-B7EA-33A462F6B137"]').hide();
        $orderItemGridSubLabor.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="89AD5560-637A-4ECF-B7EA-33A462F6B137"]').hide();
        $orderItemGridSubMisc.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="89AD5560-637A-4ECF-B7EA-33A462F6B137"]').hide();

        $orderItemGridSubRent.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();
        $orderItemGridSubSales.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();
        $orderItemGridSubLabor.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();
        $orderItemGridSubMisc.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();

        $orderItemGridSubRent.find('.buttonbar').hide();
        $orderItemGridSubSales.find('.buttonbar').hide();
        $orderItemGridSubLabor.find('.buttonbar').hide();
        $orderItemGridSubMisc.find('.buttonbar').hide();

        this.dynamicColumns($form);
        this.disableCheckboxesOnLoad($form);
    };
    //----------------------------------------------------------------------------------------------
    bottomLineTotalWithTaxChange($form: any, event: any) {
        // Total and With Tax for all OrderItemGrid
        let $element, $orderItemGrid, recType, purchaseOrderId, total, includeTaxInTotal, isWithTaxCheckbox, totalType, isSubGrid;
        let request: any = {};

        $element = jQuery(event.currentTarget);
        isWithTaxCheckbox = $element.attr('data-type') === 'checkbox';
        recType = $element.attr('data-rectype');
        purchaseOrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
        isSubGrid = $element.closest('[data-type="tabpage"]').find('[data-control="FwGrid"]').attr('data-issubgrid');

        if (recType === 'R') {
            if (isSubGrid === 'true') {
                $orderItemGrid = $form.find('.subrentalgrid [data-name="OrderItemGrid"]');
                total = FwFormField.getValue($form, '.subrental-total:visible');
                includeTaxInTotal = FwFormField.getValue($form, '.subrental-total-wtax:visible');
                FwFormField.setValue($form, '.subrental-total:hidden', '0.00');
                if (!isWithTaxCheckbox) {
                    FwFormField.setValueByDataField($form, 'SubRentalDiscountPercent', '');
                }
                if (total === '0.00') {
                    FwFormField.disable($form.find('.subrental-total-wtax:visible'));
                } else {
                    FwFormField.enable($form.find('.subrental-total-wtax:visible'));
                }
            } else {
                $orderItemGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
                total = FwFormField.getValueByDataField($form, 'RentalTotal');
                includeTaxInTotal = FwFormField.getValueByDataField($form, 'RentalTotalIncludesTax');
                if (!isWithTaxCheckbox) {
                    FwFormField.setValueByDataField($form, 'RentalDiscountPercent', '');
                }
                if (total === '0.00') {
                    FwFormField.disable($form.find('div[data-datafield="RentalTotalIncludesTax"]'));
                } else {
                    FwFormField.enable($form.find('div[data-datafield="RentalTotalIncludesTax"]'));
                }
            }
        }
        if (recType === 'S') {
            if (isSubGrid === 'true') {
                $orderItemGrid = $form.find('.subsalesgrid [data-name="OrderItemGrid"]');
                total = FwFormField.getValueByDataField($form, 'SubSalesTotal');
                includeTaxInTotal = FwFormField.getValueByDataField($form, 'SubSalesTotalIncludesTax');
                if (!isWithTaxCheckbox) {
                    FwFormField.setValueByDataField($form, 'SubSalesDiscountPercent', '');
                }
                if (total === '0.00') {
                    FwFormField.disable($form.find('div[data-datafield="SubSalesTotalIncludesTax"]'));
                } else {
                    FwFormField.enable($form.find('div[data-datafield="SubSalesTotalIncludesTax"]'));
                }
            } else {
                $orderItemGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]');
                total = FwFormField.getValueByDataField($form, 'SalesTotal');
                includeTaxInTotal = FwFormField.getValueByDataField($form, 'SalesTotalIncludesTax');
                if (!isWithTaxCheckbox) {
                    FwFormField.setValueByDataField($form, 'SalesDiscountPercent', '');
                }
                if (total === '0.00') {
                    FwFormField.disable($form.find('div[data-datafield="SalesTotalIncludesTax"]'));
                } else {
                    FwFormField.enable($form.find('div[data-datafield="SalesTotalIncludesTax"]'));
                }
            }
        }
        if (recType === 'L') {
            if (isSubGrid === 'true') {
                $orderItemGrid = $form.find('.sublaborgrid [data-name="OrderItemGrid"]');
                total = FwFormField.getValue($form, '.sublabor-total:visible');
                includeTaxInTotal = FwFormField.getValue($form, '.sublabor-total-wtax:visible');
                FwFormField.setValue($form, '.sublabor-total:hidden', '0.00');
                if (!isWithTaxCheckbox) {
                    FwFormField.setValueByDataField($form, 'SubLaborDiscountPercent', '');
                }
                if (total === '0.00') {
                    FwFormField.disable($form.find('.sublabor-total-wtax:visible'));
                } else {
                    FwFormField.enable($form.find('.sublabor-total-wtax:visible'));
                }
            } else {
                $orderItemGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]');
                total = FwFormField.getValueByDataField($form, 'LaborTotal');
                includeTaxInTotal = FwFormField.getValueByDataField($form, 'LaborTotalIncludesTax');
                if (!isWithTaxCheckbox) {
                    FwFormField.setValueByDataField($form, 'LaborDiscountPercent', '');
                }
                if (total === '0.00') {
                    FwFormField.disable($form.find('div[data-datafield="LaborTotalIncludesTax"]'));
                } else {
                    FwFormField.enable($form.find('div[data-datafield="LaborTotalIncludesTax"]'));
                }
            }
        }
        if (recType === 'M') {
            if (isSubGrid === 'true') {
                $orderItemGrid = $form.find('.submiscgrid [data-name="OrderItemGrid"]');
                total = FwFormField.getValue($form, '.submisc-total:visible');
                includeTaxInTotal = FwFormField.getValue($form, '.submisc-total-wtax:visible');
                FwFormField.setValue($form, '.submisc-total:hidden', '0.00');
                if (!isWithTaxCheckbox) {
                    FwFormField.setValueByDataField($form, 'SubMiscDiscountPercent', '');
                }
                if (total === '0.00') {
                    FwFormField.disable($form.find('.submisc-total-wtax:visible'));
                } else {
                    FwFormField.enable($form.find('.submisc-total-wtax:visible'));
                }
            } else {
                $orderItemGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]');
                total = FwFormField.getValueByDataField($form, 'MiscTotal');
                includeTaxInTotal = FwFormField.getValueByDataField($form, 'MiscTotalIncludesTax');
                if (!isWithTaxCheckbox) {
                    FwFormField.setValueByDataField($form, 'MiscDiscountPercent', '');
                }
                if (total === '0.00') {
                    FwFormField.disable($form.find('div[data-datafield="MiscTotalIncludesTax"]'));
                } else {
                    FwFormField.enable($form.find('div[data-datafield="MiscTotalIncludesTax"]'));
                }
            }
        }
        if (recType === 'P') {
            $orderItemGrid = $form.find('.partgrid [data-name="OrderItemGrid"]');
            total = FwFormField.getValueByDataField($form, 'PartsTotal');
            includeTaxInTotal = FwFormField.getValueByDataField($form, 'PartsTotalIncludesTax');

            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'PartsDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('div[data-datafield="PartsTotalIncludesTax"]'));
            } else {
                FwFormField.enable($form.find('div[data-datafield="PartsTotalIncludesTax"]'));
            }
        }

        request.TotalType = totalType;
        request.IncludeTaxInTotal = includeTaxInTotal;
        request.RecType = recType;
        request.PurchaseOrderId = purchaseOrderId;
        request.Total = +total;
        request.Subs = isSubGrid;

        FwAppData.apiMethod(true, 'POST', `api/v1/${this.Module}/applybottomlinetotal/`, request, FwServices.defaultTimeout, function onSuccess(response) {
            FwBrowse.search($orderItemGrid);
        }, function onError(response) {
            FwFunc.showError(response);
        }, $form);
    };
    //----------------------------------------------------------------------------------------------
    bottomLineDiscountChange($form: any, event: any) {
        // DiscountPercent for all OrderItemGrid
        let $element, $orderItemGrid, purchaseOrderId, recType, discountPercent, isSubGrid;
        let request: any = {};
        $element = jQuery(event.currentTarget);
        recType = $element.attr('data-rectype');
        isSubGrid = $element.closest('[data-type="tabpage"]').find('[data-control="FwGrid"]').attr('data-issubgrid');
        purchaseOrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
        discountPercent = $element.find('.fwformfield-value').val().slice(0, -1);

        if (recType === 'R') {
            if (isSubGrid === 'true') {
                let className = 'subrental';

                $orderItemGrid = $form.find('.subrentalgrid [data-name="OrderItemGrid"]');
                FwFormField.setValue($form, '.subrental-total:visible', '');
                FwFormField.disable($form.find('.subrental-total-wtax:visible'));
            } else {
                $orderItemGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
                FwFormField.setValueByDataField($form, 'RentalTotal', '');
                FwFormField.disable($form.find('div[data-datafield="RentalTotalIncludesTax"]'));
            }
        }
        if (recType === 'S') {
            if (isSubGrid === 'true') {
                $orderItemGrid = $form.find('.subsalesgrid [data-name="OrderItemGrid"]');
                FwFormField.setValueByDataField($form, 'SubSalesTotal', '');
                FwFormField.disable($form.find('div[data-datafield="SubSalesTotalIncludesTax"]'));
            } else {
                $orderItemGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]');
                FwFormField.setValueByDataField($form, 'SalesTotal', '');
                FwFormField.disable($form.find('div[data-datafield="SalesTotalIncludesTax"]'));
            }
        }
        if (recType === 'L') {
            if (isSubGrid === 'true') {
                $orderItemGrid = $form.find('.sublaborgrid [data-name="OrderItemGrid"]');
                FwFormField.setValue($form, '.sublabor-total:visible', '');
                FwFormField.disable($form.find('.sublabor-total-wtax:visible'));
            } else {
                $orderItemGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]');
                FwFormField.setValueByDataField($form, 'LaborTotal', '');
                FwFormField.disable($form.find('div[data-datafield="LaborTotalIncludesTax"]'));
            }
        }
        if (recType === 'M') {
            if (isSubGrid === 'true') {
                $orderItemGrid = $form.find('.submiscgrid [data-name="OrderItemGrid"]');
                FwFormField.setValue($form, '.submisc-total:visible', '');
                FwFormField.disable($form.find('.submisc-total-wtax:visible'));
            } else {
                $orderItemGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]');
                FwFormField.setValueByDataField($form, 'MiscTotal', '');
                FwFormField.disable($form.find('div[data-datafield="MiscTotalIncludesTax"]'));
            }
        }
        if (recType === 'P') {
            $orderItemGrid = $form.find('.partgrid [data-name="OrderItemGrid"]');
            FwFormField.setValueByDataField($form, 'PartsTotal', '');
            FwFormField.disable($form.find('div[data-datafield="PartsTotalIncludesTax"]'));
        }
      
        request.DiscountPercent = parseFloat(discountPercent);
        request.RecType = recType;
        request.PurchaseOrderId = purchaseOrderId;
        request.Subs = isSubGrid;

        FwAppData.apiMethod(true, 'POST', `api/v1/${this.Module}/applybottomlinediscountpercent/`, request, FwServices.defaultTimeout, function onSuccess(response) {
            FwBrowse.search($orderItemGrid);
        }, function onError(response) {
            FwFunc.showError(response);
        }, $form);
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
            , repairTab = $form.find('[data-type="tab"][data-caption="Repair"]')
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
        $form.find('[data-datafield="Repair"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? repairTab.show() : repairTab.hide();
        });
        $form.find('[data-datafield="SubMiscellaneous"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? submiscTab.show() : submiscTab.hide();
        });
        $form.find('[data-datafield="SubLabor"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? sublaborTab.show() : sublaborTab.hide();
        });
    };
    //----------------------------------------------------------------------------------------------
    disableCheckboxesOnLoad($form: any): void {
        // If a record has xxx items, user cannot uncheck corresponding activity checkbox
        if (FwFormField.getValueByDataField($form, 'HasRentalItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Rental'));
        }
        if (FwFormField.getValueByDataField($form, 'HasSalesItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Sales'));
        }
        if (FwFormField.getValueByDataField($form, 'HasMiscellaneousItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Miscellaneous'));
        }
        if (FwFormField.getValueByDataField($form, 'HasLaborItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Labor'));
        }
        //if (FwFormField.getValueByDataField($form, 'HasRentalSaleItem')) {              // These fields are being served but no corresponding tab or checkbox at the moment
        //    FwFormField.disable(FwFormField.getDataField($form, 'RentalSale'));
        //}
        //if (FwFormField.getValueByDataField($form, 'HasLossAndDamageItem')) {
        //    FwFormField.disable(FwFormField.getDataField($form, ''));
        //}
        //if (FwFormField.getValueByDataField($form, 'HasFacilitiesItem')) {
        //    FwFormField.disable(FwFormField.getDataField($form, ''));
        //}
    }
    //----------------------------------------------------------------------------------------------
    dynamicColumns($form: any): void {
        const POTYPE = FwFormField.getValueByDataField($form, "PoTypeId"),
            $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
            $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]'),
            $partGrid = $form.find('.partgrid [data-name="OrderItemGrid"]'),
            $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]'),
            $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]'),
            $subRentalGrid = $form.find('.subrentalgrid [data-name="OrderItemGrid"]'),
            $subSaleGrid = $form.find('.subsalesgrid [data-name="OrderItemGrid"]'),
            $subLaborGrid = $form.find('.sublaborgrid [data-name="OrderItemGrid"]'),
            $subMiscGrid = $form.find('.submiscgrid [data-name="OrderItemGrid"]'),
            fields = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field');
            let fieldNames: Array<string> = [];

        for (let i = 3; i < fields.length; i++) {
            let name = jQuery(fields[i]).attr('data-mappedfield');
            if (name !== "QuantityOrdered") {
                fieldNames.push(name);
            }
        }

        FwAppData.apiMethod(true, 'GET', `api/v1/potype/${POTYPE}`, null, FwServices.defaultTimeout, function onSuccess(response) {
            let hiddenPurchase: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.PurchaseShowFields));
            let hiddenMisc: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.MiscShowFields));
            let hiddenLabor: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.LaborShowFields));
            let hiddenSubRental: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.SubRentalShowFields));
            let hiddenSubSale: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.SubSaleShowFields));
            let hiddenSubMisc: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.SubMiscShowFields));
            let hiddenSubLabor: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.SubLaborShowFields));
            // Non-specific showfields
            for (let i = 0; i < hiddenPurchase.length; i++) {
                jQuery($rentalGrid.find(`[data-mappedfield="${hiddenPurchase[i]}"]`)).parent().hide();
                jQuery($salesGrid.find(`[data-mappedfield="${hiddenPurchase[i]}"]`)).parent().hide();
                jQuery($partGrid.find(`[data-mappedfield="${hiddenPurchase[i]}"]`)).parent().hide();
            }
            // Specific showfields
            for (let i = 0; i < hiddenMisc.length; i++) { jQuery($miscGrid.find(`[data-mappedfield="${hiddenMisc[i]}"]`)).parent().hide(); }
            for (let i = 0; i < hiddenLabor.length; i++) { jQuery($laborGrid.find(`[data-mappedfield="${hiddenLabor[i]}"]`)).parent().hide(); }
            for (let i = 0; i < hiddenSubSale.length; i++) { jQuery($subSaleGrid.find(`[data-mappedfield="${hiddenSubSale[i]}"]`)).parent().hide(); }
            for (let i = 0; i < hiddenSubRental.length; i++) { jQuery($subRentalGrid.find(`[data-mappedfield="${hiddenSubRental[i]}"]`)).parent().hide(); }
            for (let i = 0; i < hiddenSubLabor.length; i++) { jQuery($subLaborGrid.find(`[data-mappedfield="${hiddenSubLabor[i]}"]`)).parent().hide(); }
            for (let i = 0; i < hiddenSubMisc.length; i++) { jQuery($subMiscGrid.find(`[data-mappedfield="${hiddenSubMisc[i]}"]`)).parent().hide(); }
        }, null, null);
    };
    //----------------------------------------------------------------------------------------------
    calculateOrderItemGridTotals($form: any, gridType: string): void {
        let subTotal, discount, salesTax, grossTotal, total, rateType;
        let extendedTotal = new Decimal(0);
        let discountTotal = new Decimal(0);
        let taxTotal = new Decimal(0);

        let rateValue = $form.find(`.${gridType}grid .totalType input:checked`).val();
        switch (rateValue) {
            case 'W':
                rateType = 'Weekly';
                break;
            case 'P':
                rateType = 'Period';
                break;
            case 'M':
                rateType = 'Monthly';
                break;
            default:
                rateType = 'Period';
        }

        const extendedColumn: any = $form.find(`.${gridType}grid [data-browsedatafield="${rateType}Extended"]`);
        const discountColumn: any = $form.find(`.${gridType}grid [data-browsedatafield="${rateType}DiscountAmount"]`);
        const taxColumn: any = $form.find(`.${gridType}grid [data-browsedatafield="${rateType}Tax"]`);

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

        $form.find(`.${gridType}-totals [data-totalfield="SubTotal"] input`).val(subTotal);
        $form.find(`.${gridType}-totals [data-totalfield="Discount"] input`).val(discount);
        $form.find(`.${gridType}-totals [data-totalfield="Tax"] input`).val(salesTax);
        $form.find(`.${gridType}-totals [data-totalfield="GrossTotal"] input`).val(grossTotal);
        $form.find(`.${gridType}-totals [data-totalfield="Total"] input`).val(total);
    };
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        let weeklyType = $form.find(".weeklyType");
        let monthlyType = $form.find(".monthlyType");
        let subRentalDaysPerWeek = $form.find(".subRentalDaysPerWeek");
        let billingMonths = $form.find(".BillingMonths");
        let billingWeeks = $form.find(".BillingWeeks");

        $form.find(".weeklyType").show();
        $form.find(".monthlyType").hide();
        $form.find(".periodType input").prop('checked', true);

        $form.find('div[data-datafield="VendorId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="RateType"]', $tr.find('.field[data-formdatafield="DefaultRate"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="DefaultRate"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', $tr.find('.field[data-browsedatafield="BillingCycleId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="BillingCycle"]').attr('data-originalvalue'));
        });

        //Populate tax info fields with validation
        $form.find('div[data-datafield="TaxOptionId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="RentalTaxRate1"]', $tr.find('.field[data-browsedatafield="RentalTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="SalesTaxRate1"]', $tr.find('.field[data-browsedatafield="SalesTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="LaborTaxRate1"]', $tr.find('.field[data-browsedatafield="LaborTaxRate1"]').attr('data-originalvalue'));
        });

        //Hides Search option for sub item grids
        $form.find('[data-issubgrid="true"] .submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();
        // Bottom Line Total with Tax
        $form.find('.bottom-line-total-tax').on('change', event => {
            this.bottomLineTotalWithTaxChange($form, event);
        });
        // Bottom Line Discount
        $form.find('.bottom-line-discount').on('change', event => {
            this.bottomLineDiscountChange($form, event);
        });
        // SubRentalDaysPerWeek for Sub-Rental OrderItemGrid
        $form.find('div[data-datafield="SubRentalDaysPerWeek"]').on('change', '.fwformfield-text, .fwformfield-value', event => {
            let request: any = {},
            $orderItemGridRental = $form.find('.subrentalgrid [data-name="OrderItemGrid"]'),
            purchaseOrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`),
            daysperweek = FwFormField.getValueByDataField($form, 'SubRentalDaysPerWeek');

            request.DaysPerWeek = parseFloat(daysperweek);
            request.RecType = 'R';
            request.PurchaseOrderId = purchaseOrderId;
            request.Subs = true;

            FwAppData.apiMethod(true, 'POST', `api/v1/${this.Module}/applybottomlinedaysperweek/`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($orderItemGridRental);
            }, function onError(response) {
                FwFunc.showError(response);
            }, $form);
        });

        $form.find(".totalType input").on('change', e => {
            let $target = jQuery(e.currentTarget),
                gridType = $target.parents('.totalType').attr('data-gridtype'),
                rateType = $target.val(),
                adjustmentsPeriod = $form.find('.' + gridType + 'AdjustmentsPeriod'),
                adjustmentsWeekly = $form.find('.' + gridType + 'AdjustmentsWeekly'),
                adjustmentsMonthly = $form.find('.' + gridType + 'AdjustmentsMonthly');
            switch (rateType) {
                case 'W':
                    adjustmentsPeriod.hide();
                    adjustmentsWeekly.show();
                    break;
                case 'M':
                    adjustmentsPeriod.hide();
                    adjustmentsMonthly.show();
                    break;
                case 'P':
                    adjustmentsWeekly.hide();
                    adjustmentsMonthly.hide();
                    adjustmentsPeriod.show();
                    break;
            }
            let total = FwFormField.getValue($form, '.' + gridType + '-total:visible');
            if (total === '0.00') {
                FwFormField.disable($form.find('.' + gridType + '-total-wtax:visible'));
            } else {
                FwFormField.enable($form.find('.' + gridType + '-total-wtax:visible'));
            }
            this.calculateOrderItemGridTotals($form, gridType);
        });

        $form.find('.RateType').on('change', $tr => {
            let rateType = FwFormField.getValueByDataField($form, 'RateType');
            switch (rateType) {
                case 'DAILY':
                    weeklyType.show();
                    monthlyType.hide();
                    subRentalDaysPerWeek.show();
                    billingMonths.hide();
                    billingWeeks.show();
                    $form.find('.combinedgrid [data-name="OrderItemGrid"]').parent().show();
                    $form.find('.rentalgrid [data-name="OrderItemGrid"]').parent().show();
                    $form.find('.salesgrid [data-name="OrderItemGrid"]').parent().show();
                    $form.find('.laborgrid [data-name="OrderItemGrid"]').parent().show();
                    $form.find('.miscgrid [data-name="OrderItemGrid"]').parent().show();
                    break;
                case 'WEEKLY':
                    weeklyType.show();
                    monthlyType.hide();
                    subRentalDaysPerWeek.hide();
                    billingMonths.hide();
                    billingWeeks.show();
                    break;
                case '3WEEK':
                    weeklyType.show();
                    monthlyType.hide();
                    subRentalDaysPerWeek.hide();
                    billingMonths.hide();
                    billingWeeks.show();
                    break;
                case 'MONTHLY':
                    weeklyType.hide();
                    monthlyType.show();
                    subRentalDaysPerWeek.hide();
                    billingWeeks.hide();
                    billingMonths.show();
                    break;
                default:
                    weeklyType.show();
                    monthlyType.hide();
                    subRentalDaysPerWeek.show();
                    billingMonths.hide();
                    billingWeeks.show();
                    break;
            }
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

FwApplicationTree.clickEvents['{D512214F-F6BD-4098-8473-0AC7F675893D}'] = function (e) {
    let search, $form, orderId, $popup;
    $form = jQuery(this).closest('.fwform');
    orderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');

    if (orderId == "") {
        FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
    } else {
        search = new SearchInterface();
        $popup = search.renderSearchPopup($form, orderId, 'PurchaseOrder');
    }
};
//----------------------------------------------------------------------------------------------
var PurchaseOrderController = new PurchaseOrder();