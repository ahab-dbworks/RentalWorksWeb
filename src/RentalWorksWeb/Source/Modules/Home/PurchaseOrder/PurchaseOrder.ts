//routes.push({ pattern: /^module\/purchaseorder$/, action: function (match: RegExpExecArray) { return PurchaseOrderController.getModuleScreen(); } });
//routes.push({ pattern: /^module\/purchaseorder\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return PurchaseOrderController.getModuleScreen(filter); } });

//----------------------------------------------------------------------------------------------
class PurchaseOrder {
    Module: string = 'PurchaseOrder';
    apiurl: string = 'api/v1/purchaseorder';
    caption: string = 'Purchase Order';
    nav: string = 'module/purchaseorder';
    id: string = '67D8C8BB-CF55-4231-B4A2-BB308ADF18F0';
    DefaultPurchasePoType: string;
    DefaultPurchasePoTypeId: string;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    CachedPurchaseOrderTypes: any = {};
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        const $browse = this.openBrowse();
        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);

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
    };
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);
        const location = JSON.parse(sessionStorage.getItem('location'));

        FwAppData.apiMethod(true, 'GET', `api/v1/officelocation/${location.locationid}`, null, FwServices.defaultTimeout, response => {
            this.DefaultPurchasePoType = response.DefaultPurchasePoType;
            this.DefaultPurchasePoTypeId = response.DefaultPurchasePoTypeId;
        }, null, null);

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

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
        });

        //FwBrowse.search($browse);
        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject: any) {
        const $all = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $new = FwMenu.generateDropDownViewBtn('New', false, "NEW");
        const $open = FwMenu.generateDropDownViewBtn('Open', false, "OPEN");
        const $received = FwMenu.generateDropDownViewBtn('Received', false, "RECEIVED");
        const $complete = FwMenu.generateDropDownViewBtn('Complete', false, "COMPLETE");
        const $void = FwMenu.generateDropDownViewBtn('Void', false, "VOID");
        const $closed = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");

        const viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $new, $open, $received, $complete, $void, $closed);
        FwMenu.addViewBtn($menuObject, 'View', viewSubitems, true, "Status");

        //Location Filter
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        const viewLocation: Array<JQuery> = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn($menuObject, 'Location', viewLocation, true, "LocationId");
        return $menuObject;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentModuleInfo?: any) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        FwFormField.disable($form.find('[data-datafield="SubRent"]'));
        FwFormField.disable($form.find('[data-datafield="SubSale"]'));
        FwFormField.disable($form.find('[data-datafield="SubLabor"]'));
        FwFormField.disable($form.find('[data-datafield="SubMiscellaneous"]'));
        FwFormField.disable($form.find('[data-datafield="SubVehicle"]'));

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');

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

            const usersid = sessionStorage.getItem('usersid');  // J. Pace 7/09/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
            const name = sessionStorage.getItem('name');
            FwFormField.setValue($form, 'div[data-datafield="ProjectManagerId"]', usersid, name);
            FwFormField.setValue($form, 'div[data-datafield="AgentId"]', usersid, name);
            //$form.find('div[data-datafield="Labor"] input').prop('checked', true);
            const today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'PurchaseOrderDate', today);

            const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            const office = JSON.parse(sessionStorage.getItem('location'));
            const department = JSON.parse(sessionStorage.getItem('department'));
            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
            FwFormField.setValue($form, 'div[data-datafield="PoTypeId"]', this.DefaultPurchasePoTypeId, this.DefaultPurchasePoType);
        };

        let $emailHistorySubModuleBrowse = this.openEmailHistoryBrowse($form);
        $form.find('.emailhistory-page').append($emailHistorySubModuleBrowse);

        FwFormField.disable($form.find('[data-datafield="RentalTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="SalesTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="LaborTaxRate1"]'));

        this.events($form);
        this.activityCheckboxEvents($form, mode);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="PurchaseOrderId"] input').val(uniqueids.PurchaseOrderId);
        FwModule.loadForm(this.Module, $form);
        $form.find('.vendorinvoice').append(this.openVendorInvoiceBrowse($form));
        $form.find('.contractSubModule').append(this.openContractBrowse($form));
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    openEmailHistoryBrowse($form) {
        var $browse;

        $browse = EmailHistoryController.openBrowse();

        $browse.data('ondatabind', function (request) {
            request.uniqueids = {
                RelatedToId: $form.find('[data-datafield="PurchaseOrderId"] input.fwformfield-value').val()
            }
        });

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openContractBrowse($form) {
        const poId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        const $browse = ContractController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.uniqueids = {
                PurchaseOrderId: poId
            };
        });
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openVendorInvoiceBrowse($form: JQuery) {
        const $browse = VendorInvoiceController.openBrowse();

        $browse.data('ondatabind', function (request) {
            request.uniqueids = {
                PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
            }
        });

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: JQuery, parameters: any): void {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
        // ----------
        const $orderStatusHistoryGrid = $form.find('div[data-grid="OrderStatusHistoryGrid"]');
        const $orderStatusHistoryGridControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
        $orderStatusHistoryGrid.empty().append($orderStatusHistoryGridControl);
        $orderStatusHistoryGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
            };
        });
        FwBrowse.init($orderStatusHistoryGridControl);
        FwBrowse.renderRuntimeHtml($orderStatusHistoryGridControl);
        // ----------
        const $orderItemGridRental = $form.find('.rentalgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridRentalControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
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
        });
        $orderItemGridRentalControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'R';
        });

        FwBrowse.addEventHandler($orderItemGridRentalControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'rental');
            const rentalItems = $form.find('.rentalgrid tbody').children();
            rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
        });

        FwBrowse.init($orderItemGridRentalControl);
        FwBrowse.renderRuntimeHtml($orderItemGridRentalControl);
        // ----------
        const $orderItemGridSales = $form.find('.salesgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridSalesControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
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
        });
        $orderItemGridSalesControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'S';
        });
        FwBrowse.addEventHandler($orderItemGridSalesControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'sales');
            const salesItems = $form.find('.salesgrid tbody').children();
            salesItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Sales"]')) : FwFormField.enable($form.find('[data-datafield="Sales"]'));
        });

        FwBrowse.init($orderItemGridSalesControl);
        FwBrowse.renderRuntimeHtml($orderItemGridSalesControl);
        // ----------
        const $orderItemGridPart = $form.find('.partgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridPartControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
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
        });
        $orderItemGridPartControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'P';
        });
        FwBrowse.addEventHandler($orderItemGridPartControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'part');
            const partItems = $form.find('.partgrid tbody').children();
            partItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Parts"]')) : FwFormField.enable($form.find('[data-datafield="Parts"]'));
        });

        FwBrowse.init($orderItemGridPartControl);
        FwBrowse.renderRuntimeHtml($orderItemGridPartControl);
        // ----------
        const $orderItemGridLabor = $form.find('.laborgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridLaborControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
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
        });
        $orderItemGridLaborControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'L';
        });
        FwBrowse.addEventHandler($orderItemGridLaborControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'labor');
            const laborItems = $form.find('.laborgrid tbody').children();
            laborItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Labor"]')) : FwFormField.enable($form.find('[data-datafield="Labor"]'));
        });

        FwBrowse.init($orderItemGridLaborControl);
        FwBrowse.renderRuntimeHtml($orderItemGridLaborControl);
        // ----------
        const $orderItemGridMisc = $form.find('.miscgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridMiscControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
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
        });
        $orderItemGridMiscControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'M';
        });
        FwBrowse.addEventHandler($orderItemGridMiscControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'misc');
            const miscItems = $form.find('.miscgrid tbody').children();
            miscItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Miscellaneous"]')) : FwFormField.enable($form.find('[data-datafield="Miscellaneous"]'));
        });

        FwBrowse.init($orderItemGridMiscControl);
        FwBrowse.renderRuntimeHtml($orderItemGridMiscControl);
        // ----------
        const $orderItemGridSubRent = $form.find('.subrentalgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridSubRentControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
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
        });
        $orderItemGridSubRentControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'R';
            request.Summary = true;
            request.Subs = true;
        });
        FwBrowse.addEventHandler($orderItemGridSubRentControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'subrental');
            const subrentItems = $form.find('.subrentalgrid tbody').children();
            subrentItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubRent"]')) : FwFormField.enable($form.find('[data-datafield="SubRent"]'));
        });

        FwBrowse.init($orderItemGridSubRentControl);
        FwBrowse.renderRuntimeHtml($orderItemGridSubRentControl);
        // ----------
        const $oderItemGridSubSales = $form.find('.subsalesgrid div[data-grid="OrderItemGrid"]');
        const $oderItemGridSubSalesControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
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
        });
        $oderItemGridSubSalesControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'S';
            request.Summary = true;
            request.Subs = true;
        });
        FwBrowse.addEventHandler($oderItemGridSubSalesControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'subsales');
            const subsalesItems = $form.find('.subsalesgrid tbody').children();
            subsalesItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubSale"]')) : FwFormField.enable($form.find('[data-datafield="SubSale"]'));
        });

        FwBrowse.init($oderItemGridSubSalesControl);
        FwBrowse.renderRuntimeHtml($oderItemGridSubSalesControl);
        // ----------
        const $orderItemGridSubLabor = $form.find('.sublaborgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridSubLaborControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
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
        });
        $orderItemGridSubLaborControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'L';
            request.Summary = true;
            request.Subs = true;
        });
        FwBrowse.addEventHandler($orderItemGridSubLaborControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'sublabor');
            const sublaborItems = $form.find('.sublaborgrid tbody').children();
            sublaborItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubLabor"]')) : FwFormField.enable($form.find('[data-datafield="SubLabor"]'));
        });

        FwBrowse.init($orderItemGridSubLaborControl);
        FwBrowse.renderRuntimeHtml($orderItemGridSubLaborControl);
        // ----------
        const $orderItemGridSubMisc = $form.find('.submiscgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridSubMiscControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
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
        });
        $orderItemGridSubMiscControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'R';
            request.Summary = true;
            request.Subs = true;
        });
        FwBrowse.addEventHandler($orderItemGridSubMiscControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'submisc');
            const submiscItems = $form.find('.submiscgrid tbody').children();
            submiscItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubMisc"]')) : FwFormField.enable($form.find('[data-datafield="SubMisc"]'));
        });

        FwBrowse.init($orderItemGridSubMiscControl);
        FwBrowse.renderRuntimeHtml($orderItemGridSubMiscControl);
        // ----------
        const $orderNoteGrid = $form.find('div[data-grid="OrderNoteGrid"]');
        const $orderNoteGridControl = FwBrowse.loadGridFromTemplate('OrderNoteGrid');
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
    loadAudit($form: JQuery): void {
        const uniqueid = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        FwModule.loadAudit($form, uniqueid);
    };
    //----------------------------------------------------------------------------------------------

    applyPurchaseOrderTypeAndRateTypeToForm($form) {
        let self = this;

        // find all the tabs on the form
        let $rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]');
        let $salesTab = $form.find('[data-type="tab"][data-caption="Sales"]');
        let $miscTab = $form.find('[data-type="tab"][data-caption="Miscellaneous"]');
        let $laborTab = $form.find('[data-type="tab"][data-caption="Labor"]');
        let $usedSaleTab = $form.find('[data-type="tab"][data-caption="Used Sale"]');

        // find all the grids on the form
        let $subRentalGrid = $form.find('.subrentalgrid [data-name="OrderItemGrid"]');
        let $subSalesGrid = $form.find('.subsalesgrid [data-name="OrderItemGrid"]');
        let $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        let $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        let $usedSaleGrid = $form.find('.purchasegrid [data-name="OrderItemGrid"]');
        let $subLaborGrid = $form.find('.sublaborgrid [data-name="OrderItemGrid"]');
        let $subMiscGrid = $form.find('.submiscgrid [data-name="OrderItemGrid"]');
        let rateType = FwFormField.getValueByDataField($form, 'RateType');

        // get the PurchaseOrderTypeId from the form
        let purchaseOrderTypeId = FwFormField.getValueByDataField($form, 'PoTypeId');

        if (self.CachedPurchaseOrderTypes[purchaseOrderTypeId] !== undefined) {
            applyPurchaseOrderTypeToColumns($form, self.CachedPurchaseOrderTypes[purchaseOrderTypeId]);
        } else {
            let fields = jQuery($subRentalGrid).find('thead tr.fieldnames > td.column > div.field');
            let fieldNames = [];

            for (var i = 3; i < fields.length; i++) {
                var name = jQuery(fields[i]).attr('data-mappedfield');
                if (name != "QuantityOrdered") {
                    fieldNames.push(name);
                }
            }
            let hiddenSubRentals, hiddenSubSales, hiddenLabor, hiddenMisc, hiddenPurchase, hiddenSubLabor, hiddenSubMisc;

            FwAppData.apiMethod(true, 'GET', "api/v1/potype/" + purchaseOrderTypeId, null, FwServices.defaultTimeout, function onSuccess(response) {
                hiddenSubRentals = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.SubRentalShowFields))
                hiddenSubSales = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.SubSaleShowFields))
                hiddenLabor = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.LaborShowFields))
                hiddenMisc = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.MiscShowFields))
                hiddenPurchase = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.PuchaseShowFields))
                hiddenSubLabor = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.SubLaborShowFields))
                hiddenSubMisc = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.SubMiscShowFields))

                self.CachedPurchaseOrderTypes[purchaseOrderTypeId] = {
                    hiddenSubRentals: hiddenSubRentals,
                    hiddenSubSales: hiddenSubSales,
                    hiddenLabor: hiddenLabor,
                    hiddenMisc: hiddenMisc,
                    hiddenPurchase: hiddenPurchase,
                    hiddenSubLabor: hiddenSubLabor,
                    hiddenSubMisc: hiddenSubMisc
                }
                applyPurchaseOrderTypeToColumns($form, self.CachedPurchaseOrderTypes[purchaseOrderTypeId]);
            }, null, null);
        }

        //sets active tab and opens search interface from a newly saved record 
        //12-12-18 moved here from afterSave Jason H 
        let openSearch = $form.attr('data-opensearch');
        let searchType = $form.attr('data-searchtype');
        let activeTabId = $form.attr('data-activetabid');
        let search = new SearchInterface();
        if (openSearch === "true") {
            //FwTabs.setActiveTab($form, $tab); //this method doesn't seem to be working correctly
            let $newTab = $form.find(`#${activeTabId}`);
            $newTab.click();
            if ($form.attr('data-controller') === "OrderController") {
                search.renderSearchPopup($form, FwFormField.getValueByDataField($form, 'OrderId'), 'Order', searchType);
            } else if ($form.attr('data-controller') === "QuoteController") {
                search.renderSearchPopup($form, FwFormField.getValueByDataField($form, 'QuoteId'), 'Quote', searchType);
            }
            $form.removeAttr('data-opensearch data-searchtype data-activetabid');
        }


        function applyPurchaseOrderTypeToColumns($form, purchaseOrderTypeData) {
            for (var i = 0; i < purchaseOrderTypeData.hiddenSubRentals.length; i++) {
                jQuery($subRentalGrid.find('[data-mappedfield="' + purchaseOrderTypeData.hiddenSubRentals[i] + '"]')).parent().hide();
            }
            for (var j = 0; j < purchaseOrderTypeData.hiddenSubSales.length; j++) {
                jQuery($subSalesGrid.find('[data-mappedfield="' + purchaseOrderTypeData.hiddenSubSales[j] + '"]')).parent().hide();
            }
            for (var k = 0; k < purchaseOrderTypeData.hiddenLabor.length; k++) {
                jQuery($laborGrid.find('[data-mappedfield="' + purchaseOrderTypeData.hiddenLabor[k] + '"]')).parent().hide();
            }
            for (var l = 0; l < purchaseOrderTypeData.hiddenMisc.length; l++) {
                jQuery($miscGrid.find('[data-mappedfield="' + purchaseOrderTypeData.hiddenMisc[l] + '"]')).parent().hide();
            }
            for (var l = 0; l < purchaseOrderTypeData.hiddenPurchase.length; l++) {
                jQuery($usedSaleGrid.find('[data-mappedfield="' + purchaseOrderTypeData.hiddenPurchase[l] + '"]')).parent().hide();
            }
            for (let i = 0; i < purchaseOrderTypeData.hiddenSubLabor.length; i++) {
                jQuery($subLaborGrid.find(`[data-mappedfield="${purchaseOrderTypeData.hiddenSubLabor[i]}"]`)).parent().hide();
            }
            for (let i = 0; i < purchaseOrderTypeData.hiddenSubMisc.length; i++) {
                jQuery($subMiscGrid.find('[data-mappedfield="' + purchaseOrderTypeData.hiddenSubMisc[i] + '"]')).parent().hide();
            }
            if (purchaseOrderTypeData.hiddenSubRentals.indexOf('WeeklyExtended') === -1 && rateType === '3WEEK') {
                $subRentalGrid.find('.3weekextended').parent().show();
            } else if (purchaseOrderTypeData.hiddenSubRentals.indexOf('WeeklyExtended') === -1 && rateType !== '3WEEK') {
                $subRentalGrid.find('.weekextended').parent().show();
            }

            let weeklyType = $form.find(".weeklyType");
            let monthlyType = $form.find(".monthlyType");
            let rentalDaysPerWeek = $form.find(".RentalDaysPerWeek");
            let billingMonths = $form.find(".BillingMonths");
            let billingWeeks = $form.find(".BillingWeeks");


            switch (rateType) {
                case 'DAILY':
                    weeklyType.show();
                    monthlyType.hide();
                    rentalDaysPerWeek.show();
                    billingMonths.hide();
                    billingWeeks.show();
                    //$form.find('.combinedgrid [data-name="OrderItemGrid"]').parent().show();
                    //$form.find('.rentalgrid [data-name="OrderItemGrid"]').parent().show();
                    //$form.find('.salesgrid [data-name="OrderItemGrid"]').parent().show();
                    //$form.find('.laborgrid [data-name="OrderItemGrid"]').parent().show();
                    //$form.find('.miscgrid [data-name="OrderItemGrid"]').parent().show();
                    break;
                case 'WEEKLY':
                    weeklyType.show();
                    monthlyType.hide();
                    rentalDaysPerWeek.hide();
                    billingMonths.hide();
                    billingWeeks.show();
                    break;
                case '3WEEK':
                    weeklyType.show();
                    monthlyType.hide();
                    rentalDaysPerWeek.hide();
                    billingMonths.hide();
                    billingWeeks.show();
                    break;
                case 'MONTHLY':
                    weeklyType.hide();
                    monthlyType.show();
                    rentalDaysPerWeek.hide();
                    billingWeeks.hide();
                    billingMonths.show();
                    break;
                default:
                    weeklyType.show();
                    monthlyType.hide();
                    rentalDaysPerWeek.show();
                    billingMonths.hide();
                    billingWeeks.show();
                    break;
            }
        }
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery): void {
        const status = FwFormField.getValueByDataField($form, 'Status');

        this.applyPurchaseOrderTypeAndRateTypeToForm($form);

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


        //Click Event on tabs to load grids/browses
        $form.find('.tabGridsLoaded[data-type="tab"]').removeClass('tabGridsLoaded');
        $form.on('click', '[data-type="tab"]', e => {
            const $tab = jQuery(e.currentTarget);
            const tabname = $tab.attr('id');
            const lastIndexOfTab = tabname.lastIndexOf('tab');  // for cases where "tab" is included in the name of the tab
            const tabpage = `${tabname.substring(0, lastIndexOfTab)}tabpage${tabname.substring(lastIndexOfTab + 3)}`;

            if ($tab.hasClass('audittab') == false) {
                const $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
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

                const $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
                if (($tab.hasClass('tabGridsLoaded') === false) && $browseControls.length > 0) {
                    for (let i = 0; i < $browseControls.length; i++) {
                        const $browseControl = jQuery($browseControls[i]);
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

        const $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        const $orderItemGridSales = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        const $orderItemGridPart = $form.find('.partgrid [data-name="OrderItemGrid"]');
        const $orderItemGridLabor = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        const $orderItemGridMisc = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        const $orderItemGridSubRent = $form.find('.subrentalgrid [data-name="OrderItemGrid"]');
        const $orderItemGridSubSales = $form.find('.subsalesgrid [data-name="OrderItemGrid"]');
        const $orderItemGridSubLabor = $form.find('.sublaborgrid [data-name="OrderItemGrid"]');
        const $orderItemGridSubMisc = $form.find('.submiscgrid [data-name="OrderItemGrid"]');

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

        // this.dynamicColumns($form);
        this.disableCheckboxesOnLoad($form);
    };
    //----------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
          <div data-name="PurchaseOrder" data-control="FwBrowse" data-type="Browse" id="PurchaseOrderBrowse" class="fwcontrol fwbrowse" data-orderby="PurchaseOrderNumber" data-controller="PurchaseOrderController" data-hasinactive="false">
            <div class="column" data-width="0" data-visible="false">
              <div class="field" data-isuniqueid="true" data-datafield="PurchaseOrderId" data-browsedatatype="key"></div>
            </div>
            <div class="column" data-width="100px" data-visible="true">
              <div class="field" data-caption="PO No." data-datafield="PurchaseOrderNumber" data-cellcolor="PurchaseOrderNumberColor" data-browsedatatype="text" data-sort="desc" data-sortsequence="2" data-searchfieldoperators="startswith"></div>
            </div>
            <div class="column" data-width="100px" data-visible="true">
              <div class="field" data-caption="PO Date" data-datafield="PurchaseOrderDate" data-browsedatatype="date" data-sortsequence="1" data-sort="desc"></div>
            </div>
            <div class="column" data-width="350px" data-visible="true">
              <div class="field" data-caption="Description" data-datafield="Description" data-cellcolor="DescriptionColor" data-browsedatatype="text" data-sort="off"></div>
            </div>
            <div class="column" data-width="250px" data-visible="true">
              <div class="field" data-caption="Vendor" data-datafield="Vendor" data-cellcolor="VendorColor" data-browsedatatype="text" data-sort="off"></div>
            </div>
            <div class="column" data-width="100px" data-visible="true">
              <div class="field" data-caption="Total" data-datafield="Total" data-browsedatatype="number" data-cellcolor="CurrencyColor" data-digits="2" data-formatnumeric="true" data-sort="off"></div>
            </div>
            <div class="column" data-width="150px" data-visible="true">
              <div class="field" data-caption="Status" data-datafield="Status" data-cellcolor="StatusColor" data-browsedatatype="text" data-sort="off"></div>
            </div>
            <div class="column" data-width="100px" data-visible="true">
              <div class="field" data-caption="As Of" data-datafield="StatusDate" data-browsedatatype="date" data-sort="off"></div>
            </div>
            <div class="column" data-width="100px" data-visible="true">
              <div class="field" data-caption="Order Number" data-datafield="OrderNumber" data-browsedatatype="text" data-sort="off"></div>
            </div>
            <div class="column" data-width="50px" data-visible="true">
              <div class="field" data-caption="Reference Number" data-datafield="ReferenceNumber" data-browsedatatype="text" data-sort="off"></div>
            </div>
            <div class="column" data-width="180px" data-visible="true">
              <div class="field" data-caption="Agent" data-datafield="Agent" data-multiwordseparator="|" data-browsedatatype="text" data-sort="off"></div>
            </div>
          </div>`;
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
          <div id="purchaseorderform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Purchase Order" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="PurchaseOrderController">
            <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="PurchaseOrderId"></div>
            <div id="purchaseorderform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
              <div class="tabs">
                <div data-type="tab" id="generaltab" class="generaltab tab" data-tabpageid="generaltabpage" data-caption="Purchase Order"></div>
                <div data-type="tab" id="rentaltab" class="tab" data-tabpageid="rentaltabpage" data-caption="Rental"></div>
                <div data-type="tab" id="salestab" class="tab" data-tabpageid="salestabpage" data-caption="Sales"></div>
                <div data-type="tab" id="parttab" class="tab" data-tabpageid="parttabpage" data-caption="Parts"></div>
                <div data-type="tab" id="misctab" class="notcombinedtab tab" data-tabpageid="misctabpage" data-caption="Misc"></div>
                <div data-type="tab" id="labortab" class="notcombinedtab tab" data-tabpageid="labortabpage" data-caption="Labor"></div>
                <div data-type="tab" id="subrentaltab" class="tab" data-tabpageid="subrentaltabpage" data-caption="Sub-Rental"></div>
                <div data-type="tab" id="subsalestab" class="tab" data-tabpageid="subsalestabpage" data-caption="Sub-Sales"></div>
                <!--<div data-type="tab" id="repairtab" class="tab" data-tabpageid="repairtabpage" data-caption="Repair"></div>-->
                <div data-type="tab" id="submisctab" class="notcombinedtab tab" data-tabpageid="submisctabpage" data-caption="Sub-Misc"></div>
                <div data-type="tab" id="sublabortab" class="notcombinedtab tab" data-tabpageid="sublabortabpage" data-caption="Sub-Labor"></div>
                <!--<div data-type="tab" id="alltab" class="combinedtab tab" data-tabpageid="alltabpage" data-caption="Items"></div>
                <div data-type="tab" id="contracttab" class="tab submodule" data-tabpageid="contracttabpage" data-caption="Contract"></div>-->
                <div data-type="tab" id="billingtab" class="tab" data-tabpageid="billingtabpage" data-caption="Billing"></div>
                <div data-type="tab" id="vendorinvoicetab" class="tab" data-tabpageid="vendorinvoicetabpage" data-caption="Vendor Invoice"></div>
                <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
                <div data-type="tab" id="delivershiptab" class="tab" data-tabpageid="delivershiptabpage" data-caption="Deliver/Ship"></div>
                <div data-type="tab" id="contracttab" class="tab submodule" data-tabpageid="contracttabpage" data-caption="Contract"></div>    
                <div data-type="tab" id="notetab" class="tab" data-tabpageid="notetabpage" data-caption="Notes"></div>
                <div data-type="tab" id="historytab" class="tab" data-tabpageid="historytabpage" data-caption="History"></div>
                <div data-type="tab" id="emailhistorytab" class="tab" data-tabpageid="emailhistorytabpage" data-caption="Email History"></div>
              </div>
              <div class="tabpages">
                <!-- PURCHASE ORDER TAB -->
                <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
                  <div class="formpage">
                    <!-- Order / Status section-->
                    <div class="flexrow">
                      <div class="flexcolumn" style="flex:1 1 950px;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Purchase Order">
                          <div class="flexrow">
                            <div class="flexcolumn" style="flex:1 1 550px;">
                              <div class="flexrow">
                                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderNumber" data-enabled="false" style="flex:0 1 100px;"></div>
                                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-required="true" style="flex:1 1 400px;"></div>
                                <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="PO Date" data-datafield="PurchaseOrderDate" style="flex:1 1 100px;"></div>
                                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Requisition No." data-datafield="RequisitionNumber" style="flex:1 1 100px;"></div>
                                <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Requisition Date" data-datafield="RequisitionDate" style="flex:1 1 100px;"></div>
                              </div>
                            </div>
                          </div>
                          <div class="flexrow">
                            <div class="flexcolumn" style="flex:1 1 616px;">
                              <div class="flexrow">
                                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displayfield="Vendor" data-validationname="VendorValidation" data-formbeforevalidate="beforeValidate" data-required="true" style="flex:1 1 400px;"></div>
                                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield RateType" data-caption="Rate" data-datafield="RateType" data-displayfield="RateType" data-validationname="RateTypeValidation" data-validationpeek="false" data-required="true" style="flex:1 1 175px;"></div>
                                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="PoTypeId" data-displayfield="PoType" data-validationname="POTypeValidation" data-required="true" style="flex:1 1 175px;"></div>
                              </div>
                            </div>
                            <div class="flexcolumn" style="flex:1 1 375px;">
                              <div class="flexrow">
                                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-validationpeek="false" data-enabled="false" style="flex:1 1 200px;"></div>
                                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-validationpeek="false" data-enabled="false" style="flex:1 1 200px;"></div>
                                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" data-required="true" style="flex:1 1 175px;"></div>
                              </div>
                            </div>
                          </div>
                          <div class="flexrow"></div>
                        </div>
                      </div>
                      <!-- Status section -->
                      <div class="flexcolumn" style="flex:1 1 125px;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="Status" data-enabled="false" style="flex:1 0 125px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="As of" data-datafield="StatusDate" data-enabled="false" style="flex:1 0 125px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Reference No." data-datafield="ReferenceNumber" style="flex:1 0 125px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <!-- Activity section -->
                    <div class="flexrow">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Activity" style="flex:1 1 770px">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="Rental" style="flex:1 1 90px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="Sales" style="flex:1 1 90px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Parts" data-datafield="Parts" style="flex:1 1 90px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Miscellaneous" data-datafield="Miscellaneous" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Labor" data-datafield="Labor" style="flex:1 1 90px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Rent" data-datafield="SubRent" style="flex:1 1 90px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Sale" data-datafield="SubSale" style="flex:1 1 90px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Repair" data-datafield="Repair" style="flex:1 1 90px;" data-enabled="false"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Miscellaneous" data-datafield="SubMiscellaneous" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Labor" data-datafield="SubLabor" style="flex:1 1 90px;"></div>
                        </div>
                        <!--Hidden field for determining whether checkboxes should be enabled / disabled-->
                        <div class="flexrow" style="display:none;">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasFacilitiesItem" data-datafield="HasFacilitiesItem" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasLaborItem" data-datafield="HasLaborItem" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasLossAndDamageItem" data-datafield="HasLossAndDamageItem" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasMiscellaneousItem" data-datafield="HasMiscellaneousItem" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalItem" data-datafield="HasRentalItem" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalSaleItem" data-datafield="HasRentalSaleItem" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasSalesItem" data-datafield="HasSalesItem" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Location / PO section -->
                    <!--<div class="flexrow">
                      <div class="flexcolumn" style="flex:1 1 125px;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Print">
                          <div class="print fwformcontrol" data-type="button" style="flex:1 1 50px;margin:15px 0 0 10px;">Print</div>
                        </div>
                      </div>
                    </div>-->
                    <!-- Personnel -->
                    <div class="flexrow">
                      <div class="flexcolumn" style="flex:0 1 600px;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Personnel">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="AgentId" data-displayfield="Agent" data-enabled="true" data-required="true" data-validationname="UserValidation" style="flex:1 1 185px;"></div>
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Project Manager" data-datafield="ProjectManagerId" data-displayfield="ProjectManager" data-enabled="true" data-required="true" data-validationname="UserValidation" style="flex:1 1 185px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- RENTAL TAB -->
                <div data-type="tabpage" id="rentaltabpage" class="rentalgrid notcombined tabpage" data-tabid="rentaltab" data-render="false">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Items">
                    <div class="flexrow" style="max-width:1800px;">
                      <div data-control="FwGrid" data-issubgrid="false" data-grid="OrderItemGrid" data-securitycaption="Rental Items"></div>
                    </div>
                  </div>
                  <div class="flexrow" style="max-width:1800px;">
                    <div class="flexcolumn" style="flex:1 1 125px;"></div>
                    <div class="flexcolumn rental-adjustments" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield bottom-line-discount" data-caption="Disc. %" data-rectype="R" data-datafield="RentalDiscountPercent" data-digits="2" style="flex:1 1 50px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals rentalOrderItemTotal bottom-line-total-tax" data-caption="Total" data-rectype="R" data-datafield="RentalTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield rentalTotalWithTax bottom-line-total-tax" data-caption="w/ Tax" data-rectype="R" data-datafield="RentalTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn rental-totals" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- SALES TAB -->
                <div data-type="tabpage" id="salestabpage" class="salesgrid notcombined tabpage" data-tabid="salestab" data-render="false">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sales Items">
                    <div class="flexrow" style="max-width:1800px;">
                      <div data-control="FwGrid" data-issubgrid="false" data-grid="OrderItemGrid" data-securitycaption="Sales Items"></div>
                    </div>
                  </div>
                  <div class="flexrow" style="max-width:1800px;">
                    <div class="flexcolumn" style="flex:1 1 125px;"></div>
                    <div class="flexcolumn sales-adjustments" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="S" data-datafield="SalesDiscountPercent" style="flex:1 1 50px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals salesOrderItemTotal bottom-line-total-tax" data-caption="Total" data-rectype="S" data-datafield="SalesTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield salesTotalWithTax bottom-line-total-tax" data-caption="w/ Tax" data-rectype="S" data-datafield="SalesTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn sales-totals" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- PART TAB -->
                <div data-type="tabpage" id="parttabpage" class=" partgrid tabpage" data-tabid="parttab" data-render="false">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Parts Items">
                    <div class="flexrow" style="max-width:1800px;">
                      <div data-control="FwGrid" data-issubgrid="false" data-grid="OrderItemGrid" data-securitycaption="Parts Items"></div>
                    </div>
                  </div>
                  <div class="flexrow" style="max-width:1800px;">
                    <div class="flexcolumn" style="flex:1 1 125px;"></div>
                    <div class="flexcolumn part-adjustments" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="P" data-datafield="PartsDiscountPercent" style="flex:1 1 50px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals part-order-item-total bottom-line-total-tax" data-caption="Total" data-rectype="P" data-datafield="PartsTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield part-total-wtax bottom-line-total-tax" data-caption="w/ Tax" data-rectype="P" data-datafield="PartsTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn part-totals" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- MISC TAB -->
                <div data-type="tabpage" id="misctabpage" class="miscgrid notcombined tabpage" data-tabid="misctab" data-render="false">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Misc Items">
                    <div class="flexrow" style="max-width:1800px;">
                      <div data-control="FwGrid" data-issubgrid="false" data-grid="OrderItemGrid" data-securitycaption="Misc Items"></div>
                    </div>
                  </div>
                  <div class="flexrow" style="max-width:1800px;">
                    <div class="flexcolumn" style="flex:1 1 125px;"></div>
                    <div class="flexcolumn misc-adjustments" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="M" data-datafield="MiscDiscountPercent" style="flex:1 1 50px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals part-order-item-total bottom-line-total-tax" data-caption="Total" data-rectype="M" data-datafield="MiscTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield part-total-wtax bottom-line-total-tax" data-caption="w/ Tax" data-rectype="M" data-datafield="MiscTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn misc-totals" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- LABOR TAB -->
                <div data-type="tabpage" id="labortabpage" class="laborgrid notcombined tabpage" data-tabid="labortab" data-render="false">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Labor Items">
                    <div class="flexrow" style="max-width:1800px;">
                      <div data-control="FwGrid" data-issubgrid="false" data-grid="OrderItemGrid" data-securitycaption="Labor Items"></div>
                    </div>
                  </div>
                  <div class="flexrow" style="max-width:1800px;">
                    <div class="flexcolumn" style="flex:1 1 125px;"></div>
                    <div class="flexcolumn labor-adjustments" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="L" data-datafield="LaborDiscountPercent" style="flex:1 1 50px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals labor-total bottom-line-total-tax" data-caption="Total" data-rectype="L" data-datafield="LaborTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield labor-total-wtax bottom-line-total-tax" data-caption="w/ Tax" data-rectype="L" data-datafield="LaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn labor-totals" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- SUBRENTAL TAB -->
                <div data-type="tabpage" id="subrentaltabpage" class="subrentalgrid notcombined tabpage" data-tabid="subrentaltab" data-render="false">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sub-Rental Items">
                    <div class="flexrow" style="max-width:1800px;">
                      <div data-issubgrid="true" data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Sub-Rental Item-totals"></div>
                    </div>
                  </div>
                  <div class="flexrow" style="max-width:1800px;">
                    <div class="flexcolumn" style="flex:1 1 125px;"></div>
                    <div class="flexcolumn subrental-adjustments" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield totals subRentalDaysPerWeek" data-caption="D/W" data-datafield="SubRentalDaysPerWeek" data-digits="2" data-digitsoptional="false" style="flex:1 1 50px;"></div>
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="R" data-datafield="SubRentalDiscountPercent" style="flex:1 1 50px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals subrental-total bottom-line-total-tax subrentalAdjustmentsPeriod" data-caption="Total" data-rectype="R" data-datafield="PeriodSubRentalTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield subrental-total-wtax bottom-line-total-tax subrentalAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="R" data-datafield="PeriodSubRentalTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals subrental-total bottom-line-total-tax subrentalAdjustmentsWeekly" data-caption="Total" data-rectype="R" data-datafield="WeeklySubRentalTotal" style="flex:1 1 100px; display:none;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield subrental-total-wtax bottom-line-total-tax subrentalAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="R" data-datafield="WeeklySubRentalTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals subrental-total bottom-line-total-tax subrentalAdjustmentsMonthly" data-caption="Total" data-rectype="R" data-datafield="MonthlySubRentalTotal" style="flex:1 1 100px; display:none;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield subrental-total-wtax bottom-line-total-tax subrentalAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="R" data-datafield="MonthlySubRentalTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn subrental-totals" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield totals totalType" data-caption="" data-gridtype="subrental" data-datafield="" style="flex:1 1 250px;">
                            <div data-value="W" class="weeklyType" data-caption="Weekly" style="margin-top:5px;"></div>
                            <div data-value="M" class="monthlyType" data-caption="Monthly" style="margin-top:5px;"></div>
                            <div data-value="P" class="periodType" data-caption="Period"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- SUBSALES TAB -->
                <div data-type="tabpage" id="subsalestabpage" class="subsalesgrid notcombined tabpage" data-tabid="subsalestab" data-render="false">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sub-Sales Items">
                    <div class="flexrow" style="max-width:1800px;">
                      <div data-issubgrid="true" data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Sub-Sales Items"></div>
                    </div>
                  </div>
                  <div class="flexrow" style="max-width:1800px;">
                    <div class="flexcolumn" style="flex:1 1 125px;"></div>
                    <div class="flexcolumn subsales-adjustments" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="S" data-datafield="SubSalesDiscountPercent" style="flex:1 1 50px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals bottom-line-total-tax" data-caption="Total" data-rectype="S" data-datafield="SubSalesTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield bottom-line-total-tax" data-caption="w/ Tax" data-rectype="S" data-datafield="SubSalesTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn subsales-totals" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- SUBMISC TAB -->
                <div data-type="tabpage" id="submisctabpage" class="submiscgrid notcombined tabpage" data-tabid="submisctab" data-render="false">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sub-Miscellaneous Items">
                    <div class="flexrow" style="max-width:1800px;">
                      <div data-issubgrid="true" data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Sub-Miscellaneous Items"></div>
                    </div>
                  </div>
                  <div class="flexrow" style="max-width:1800px;">
                    <div class="flexcolumn" style="flex:1 1 125px;"></div>
                    <div class="flexcolumn submisc-adjustments" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="M" data-datafield="SubMiscDiscountPercent" style="flex:1 1 50px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals submisc-total bottom-line-total-tax submiscAdjustmentsPeriod" data-caption="Total" data-rectype="M" data-datafield="PeriodSubMiscTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield submisc-total-wtax bottom-line-total-tax submiscAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="M" data-datafield="PeriodSubMiscTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals submisc-total bottom-line-total-tax submiscAdjustmentsWeekly" data-caption="Total" data-rectype="M" data-datafield="WeeklySubMiscTotal" style="flex:1 1 100px; display:none;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield submisc-total-wtax bottom-line-total-tax submiscAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="M" data-datafield="WeeklySubMiscTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals submisc-total bottom-line-total-tax submiscAdjustmentsMonthly" data-caption="Total" data-rectype="M" data-datafield="MonthlySubMiscTotal" style="flex:1 1 100px; display:none;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield submisc-total-wtax bottom-line-total-tax submiscAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="M" data-datafield="MonthlySubMiscTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn submisc-totals" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield totals totalType" data-caption="" data-gridtype="submisc" data-datafield="" style="flex:1 1 250px;">
                            <div data-value="W" class="weeklyType" data-caption="Weekly" style="margin-top:5px;"></div>
                            <div data-value="M" class="monthlyType" data-caption="Monthly" style="margin-top:5px;"></div>
                            <div data-value="P" class="periodType" data-caption="Period"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- SUBLABOR TAB -->
                <div data-type="tabpage" id="sublabortabpage" class="sublaborgrid notcombined tabpage" data-tabid="sublabortab" data-render="false">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sub-Labor Items">
                    <div class="flexrow" style="max-width:1800px;">
                      <div data-issubgrid="true" data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Sub-Labor Items"></div>
                    </div>
                  </div>
                  <div class="flexrow" style="max-width:1800px;">
                    <div class="flexcolumn" style="flex:1 1 125px;"></div>
                    <div class="flexcolumn sublabor-adjustments" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="L" data-datafield="SubLaborDiscountPercent" style="flex:1 1 50px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals sublabor-total bottom-line-total-tax sublaborAdjustmentsPeriod" data-caption="Total" data-rectype="L" data-datafield="PeriodSubLaborTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield sublabor-total-wtax bottom-line-total-tax sublaborAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="L" data-datafield="PeriodSubLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals sublabor-total bottom-line-total-tax sublaborAdjustmentsWeekly" data-caption="Total" data-rectype="L" data-datafield="WeeklySubLaborTotal" style="flex:1 1 100px; display:none;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield sublabor-total-wtax bottom-line-total-tax sublaborAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="L" data-datafield="WeeklySubLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals sublabor-total bottom-line-total-tax sublaborAdjustmentsMonthly" data-caption="Total" data-rectype="L" data-datafield="MonthlySubLaborTotal" style="flex:1 1 100px; display:none;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield sublabor-total-wtax bottom-line-total-tax sublaborAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="L" data-datafield="MonthlySubLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn sublabor-totals" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield totals totalType" data-caption="" data-gridtype="sublabor" data-datafield="" style="flex:1 1 250px;">
                            <div data-value="W" class="weeklyType" data-caption="Weekly" style="margin-top:5px;"></div>
                            <div data-value="M" class="monthlyType" data-caption="Monthly" style="margin-top:5px;"></div>
                            <div data-value="P" class="periodType" data-caption="Period"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- REPAIR TAB -->
                <!--<div data-type="tabpage" id="repairtabpage" class="repairpartgrid notcombined tabpage" data-tabid="repairtab" data-render="false">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Repair Items">
                    <div class="flexrow" style="max-width:1800px;">
                      <div data-issubgrid="true" data-control="FwGrid" data-grid="RepairPartGrid" data-securitycaption="Repair Items"></div>
                    </div>
                    <div class="flexrow" style="max-width:1800px;">
                      <div class="flexcolumn" style="flex:1 1 125px;"></div>
                      <div class="flexcolumn repair-adjustments" style="flex:1 1 300px;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="L" data-datafield="PartDiscountPercent" style="flex:1 1 50px;"></div>
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals part-order-item-total bottom-line-total-tax laborAdjustmentsPeriod" data-caption="Total" data-rectype="L" data-datafield="PeriodLaborTotal" style="flex:1 1 100px;"></div>
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield part-total-wtax bottom-line-total-tax laborAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="L" data-datafield="PartIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals laborOrderItemTotal bottom-line-total-tax laborAdjustmentsWeekly" data-caption="Total" data-rectype="L" data-datafield="WeeklyLaborTotal" style="flex:1 1 100px; display:none;"></div>
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield laborTotalWithTax bottom-line-total-tax laborAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="L" data-datafield="WeeklyLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals laborOrderItemTotal bottom-line-total-tax laborAdjustmentsMonthly" data-caption="Total" data-rectype="L" data-datafield="MonthlyLaborTotal" style="flex:1 1 100px; display:none;"></div>
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield laborTotalWithTax bottom-line-total-tax laborAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="L" data-datafield="MonthlyLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                          </div>
                        </div>
                      </div>
                      <div class="flexcolumn repair-totals" style="flex:1 1 550px;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>-->
                <!-- BILLING TAB PAGE -->
                <div data-type="tabpage" id="billingtabpage" class="tabpage" data-tabid="billingtab" style="width: 400px;">
                  <!-- Billing Period -->
                  <!--<div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Period">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield billing_start_date" data-caption="Start" data-datafield="BillingStartDate" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield billing_end_date" data-caption="Stop" data-datafield="BillingEndDate" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield BillingWeeks week_or_month_field" data-caption="Weeks" data-datafield="BillingWeeks" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield BillingMonths week_or_month_field" data-caption="Months" data-datafield="BillingMonths" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Delay Billing Search Until" data-datafield="DelayBillingSearchUntil" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Lock Billing Dates" data-datafield="LockBillingDates" style="flex:1 1 150px;padding-left:25px;margin-top:10px;"></div>
                      </div>
                    </div>
                  </div>-->
                  <!-- Billing Cycle -->
                  <div class="flexrow" style="flex:0 1 400px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Cycle">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="BillingCycleValidation" class="fwcontrol fwformfield" data-caption="Billing Cycle" data-datafield="BillingCycleId" data-displayfield="BillingCycle" style="flex:0 1 250px;" data-required="true"></div>
                        <div data-control="FwFormField" data-type="validation" data-validationname="CurrencyValidation" class="fwcontrol fwformfield" data-caption="Currency Code" data-datafield="CurrencyId" data-displayfield="CurrencyCode" style="flex:0 1 250px;"></div>
                        <!--<div data-control="FwFormField" data-type="validation" data-validationname="PaymentTermsValidation" class="fwcontrol fwformfield" data-caption="Payment Terms" data-datafield="PaymentTermsId" data-displayfield="PaymentTerms" style="flex:1 1 250px;"></div>-->
                        <!--<div data-control="FwFormField" data-type="validation" data-validationname="PaymentTypeValidation" class="fwcontrol fwformfield" data-caption="Pay Type" data-datafield="PaymentTypeId" data-displayfield="PaymentType" style="flex:1 1 250px;"></div>-->
                      </div>
                    </div>
                  </div>
                  <!-- Bill Based On / Labor Fees / Contact Confirmation -->
                  <!--<div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Determine Quantities to Bill Based on">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DetermineQuantitiesToBillBasedOn" style="flex:1 1 250px;">
                          <div data-value="CONTRACT" data-caption="Contract Activity"></div>
                          <div data-value="ORDER" data-caption="Order Quantity"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:1 1 25px;">
                    &#32;
                  </div>
                  <div class="flexcolumn" style="flex:1 1 300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Labor Prep Fees">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="IncludePrepFeesInRentalRate" style="flex:1 1 400px;">
                          <div data-value="false" data-caption="Add Prep Fees as Labor Charges"></div>
                          <div data-value="true" data-caption="Add Prep Fees into the Rental Item Rate"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:1 1 25px;">
                    &#32;
                  </div>-->
                  <!--<div class="flexcolumn" style="flex:1 1 300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact Confirmation">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Require Contact Confirmation" data-datafield="RequireContactConfirmation" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                  </div>-->
                  <!--</div>-->
                  <!-- Tax Rates / Order Group / Contact Confirmation -->
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 400px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax Rates">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" data-validationname="TaxOptionValidation" class="fwcontrol fwformfield" data-caption="Tax Option" data-datafield="TaxOptionId" data-displayfield="TaxOption" style="flex:1 1 250px"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="RentalTaxRate1" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="SalesTaxRate1" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Labor" data-datafield="LaborTaxRate1" style="flex:1 1 75px;"></div>
                        </div>
                      </div>
                    </div>
                    <!--<div class="flexcolumn" style="flex:1 1 25px;">
                      &#32;
                    </div>-->
                    <!--<div class="flexcolumn" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Hiatus Schedule">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="HiatusDiscountFrom" style="flex:1 1 200px;">
                            <div data-value="DEAL" data-caption="Deal" style="flex:1 1 100px;"></div>
                            <div data-value="ORDER" data-caption="This Order" style="flex:1 1 100px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>-->
                    <!--<div class="flexcolumn" style="flex:1 1 25px;">
                      &#32;
                    </div>-->
                    <!--<div class="flexcolumn" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Group">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="In Group?" data-datafield="InGroup" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Group No" data-datafield="GroupNumber" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>-->
                  </div>
                  <!-- Issue To / Bill To Address -->
                  <!--<div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 15%;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="PrintIssuedToAddressFrom" style="flex:1 1 150px;">
                            <div data-value="DEAL" data-caption="Deal" style="flex:1 1 100px;"></div>
                            <div data-value="CUSTOMER" data-caption="Customer" style="flex:1 1 100px;"></div>
                            <div data-value="ORDER" data-caption="Order" style="flex:1 1 100px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 42.5%;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Issue To">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="IssuedToName" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="IssuedToAttention" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="IssuedToAttention2" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="IssuedToAddress1" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="IssuedToAddress2" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="BillToCity" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="validation" data-validationname="StateValidation" class="fwcontrol fwformfield" data-caption="State/Province" data-datafield="IssuedToState" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="IssuedToZipCode" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" data-validationname="CountryValidation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="IssuedToCountryId" data-displayfield="IssuedToCountry" style="flex:1 1 250px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 42.5%;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Bill To">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Different Than Issue To Address" data-datafield="BillToAddressDifferentFromIssuedToAddress" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Name" data-datafield="BillToName" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Attention" data-datafield="BillToAttention" data-enabled="false" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="" data-datafield="BillToAttention2" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Address" data-datafield="BillToAddress1" data-enabled="false" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="" data-datafield="BillToAddress2" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="City" data-datafield="BillToCity" data-enabled="false" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="validation" data-validationname="StateValidation" class="differentaddress fwcontrol fwformfield" data-caption="State/Province" data-datafield="BillToState" data-enabled="false" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="BillToZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" data-validationname="CountryValidation" class="differentaddress fwcontrol fwformfield" data-caption="Country" data-datafield="BillToCountryId" data-displayfield="BillToCountry" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>-->
                  <!-- Options -->
                  <!--<div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 400px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="No Charge">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="No Charge" data-datafield="NoCharge" style="flex:1 1 75px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Reason" data-datafield="NoChargeReason" style="flex:1 1 350px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 675px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Rental Items to go Out again after being Checked-In without increasing the Order quantity" data-datafield="RoundTripRentals" style="flex:1 1 650px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Discount Reason" data-datafield="DiscountReasonId" data-displayfield="DiscountReason" data-validationname="DiscountReasonValidation" style="flex:1 1 250px; float:left;"></div>
                        </div>
                      </div>
                    </div>
                  </div>-->
                </div>

                <!-- DELIVER/SHIP TAB -->
                <!--<div data-type="tabpage" id="delivershiptabpage" class="tabpage" data-tabid="delivershiptab">
                  <div class="flexpage">
                    <div class="flexrow">
                      <div class="flexcolumn" style="flex:1 1 550px;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Outgoing">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield outtype" data-caption="Type" data-datafield="OutDeliveryDeliveryType" style="flex:1 1 150px;"></div>
                            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On" data-datafield="OutDeliveryTargetShipDate" style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Required By" data-datafield="OutDeliveryRequiredDate" style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Required Time" data-datafield="OutDeliveryRequiredTime" style="flex:1 1 75px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contact" data-datafield="OutDeliveryToContact" style="flex:1 1 250px;"></div>
                            <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="OutDeliveryToContactPhone" style="flex:1 1 250px;"></div>
                          </div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Ship Via">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" data-validationname="VendorValidation" class="fwcontrol fwformfield" data-caption="Carrier" data-datafield="OutDeliveryCarrierId" data-displayfield="OutDeliveryCarrier" data-formbeforevalidate="beforeValidateCarrier"></div>
                            <div data-control="FwFormField" data-type="validation" data-validationname="ShipViaValidation" class="fwcontrol fwformfield" data-caption="Ship Via" data-datafield="OutDeliveryShipViaId" data-displayfield="OutDeliveryShipVia" data-formbeforevalidate="beforeValidateOutShipVia"></div>
                          </div>
                        </div>
                        <div class="flexrow">
                          <div class="flexcolumn" style="width:25%;flex:0 1 auto;">
                            <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Address" data-datafield="OutDeliveryAddressType">
                              <div data-value="DEAL" data-caption="Deal"></div>
                              <div data-value="OTHER" data-caption="Other"></div>
                              <div data-value="VENUE" data-caption="Venue"></div>
                              <div data-value="WAREHOUSE" data-caption="Warehouse"></div>
                            </div>
                          </div>
                          <div class="flexcolumn" style="width:75%;">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="OutDeliveryToLocation"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="OutDeliveryToAttention"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="OutDeliveryToAddress1"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="OutDeliveryToAddress2"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="OutDeliveryToCity"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State/Province" data-datafield="OutDeliveryToState"></div>
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="OutDeliveryToZipCode"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-validationname="CountryValidation" data-datafield="OutDeliveryToCountryId" data-displayfield="OutDeliveryToCountry"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Cross Streets" data-datafield="OutDeliveryToCrossStreets"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Notes" data-datafield="OutDeliveryDeliveryNotes"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order No" data-datafield="OutDeliveryOnlineOrderNumber"></div>
                              <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield online" data-caption="Status" data-datafield="OutDeliveryOnlineOrderStatus"></div>
                            </div>
                          </div>
                        </div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 550px;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Incoming">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield intype" data-caption="Type" data-datafield="InDeliveryDeliveryType" style="flex:1 1 150px;"></div>
                            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On" data-datafield="InDeliveryTargetShipDate" style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Required By" data-datafield="InDeliveryRequiredDate" style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Required Time" data-datafield="InDeliveryRequiredTime" style="flex:1 1 75px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contact" data-datafield="InDeliveryToContact" style="flex:1 1 250px;"></div>
                            <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="InDeliveryToContactPhone" style="flex:1 1 250px;"></div>
                          </div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Ship Via">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" data-validationname="VendorValidation" class="fwcontrol fwformfield" data-caption="Carrier" data-datafield="InDeliveryCarrierId" data-displayfield="InDeliveryCarrier" data-formbeforevalidate="beforeValidateCarrier"></div>
                            <div data-control="FwFormField" data-type="validation" data-validationname="ShipViaValidation" class="fwcontrol fwformfield" data-caption="Ship Via" data-datafield="InDeliveryShipViaId" data-displayfield="InDeliveryShipVia" data-formbeforevalidate="beforeValidateInShipVia"></div>
                          </div>
                        </div>
                        <div class="flexrow">
                          <div class="flexcolumn" style="width:25%;flex:0 1 auto;">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Address" data-datafield="InDeliveryAddressType">
                                <div data-value="DEAL" data-caption="Deal"></div>
                                <div data-value="OTHER" data-caption="Other"></div>
                                <div data-value="VENUE" data-caption="Venue"></div>
                                <div data-value="WAREHOUSE" data-caption="Warehouse"></div>
                              </div>
                            </div>
                            <div class="flexrow">
                              <div class="copy fwformcontrol" data-type="button" style="flex:0 1 50px;margin:15px 0 0 10px;text-align:center;">Copy</div>
                            </div>
                          </div>
                          <div class="flexcolumn" style="width:75%;">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="InDeliveryToLocation"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="InDeliveryToAttention"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="InDeliveryToAddress1"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="InDeliveryToAddress2"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="InDeliveryToCity"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State/Province" data-datafield="InDeliveryToState"></div>
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="InDeliveryToZipCode"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-validationname="CountryValidation" data-datafield="InDeliveryToCountryId" data-displayfield="InDeliveryToCountry"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Cross Streets" data-datafield="InDeliveryToCrossStreets"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Notes" data-datafield="InDeliveryDeliveryNotes"></div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>-->
    <!-- VENDOR INVOICE TAB-->
      <div data-type="tabpage" id="vendorinvoicetabpage" class="tabpage submodule vendorinvoice" data-tabid="vendorinvoicetab">
      </div>
     <!-- CONTRACT TAB -->
           <div data-type="tabpage" id="contracttabpage" class="tabpage contractSubModule" data-tabid="contracttab">
              </div>
                <!-- CONTACTS TAB -->
                <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contacts">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwGrid" data-grid="OrderContactGrid" data-securitycaption="Contacts"></div>
                    </div>
                  </div>
                </div>
                <!-- NOTES TAB -->
                <div data-type="tabpage" id="notetabpage" class="tabpage" data-tabid="notetab">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwGrid" data-grid="OrderNoteGrid" data-securitycaption="Notes"></div>
                    </div>
                  </div>
                </div>
                <!-- HISTORY TAB -->
                <div data-type="tabpage" id="historytabpage" class="tabpage" data-tabid="historytab">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Status History">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwGrid" data-grid="OrderStatusHistoryGrid" data-securitycaption="Order Status History"></div>
                    </div>
                  </div>
                </div>
              <div data-type="tabpage" id="emailhistorytabpage" class="tabpage submodule emailhistory-page" data-tabid="emailhistorytab"></div>
              </div>
            </div>
          </div>`;
    }
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
    //dynamicColumns($form: any): void {
    //    const POTYPE = FwFormField.getValueByDataField($form, "PoTypeId"),
    //        $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
    //        $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]'),
    //        $partGrid = $form.find('.partgrid [data-name="OrderItemGrid"]'),
    //        $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]'),
    //        $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]'),
    //        $subRentalGrid = $form.find('.subrentalgrid [data-name="OrderItemGrid"]'),
    //        $subSalesGrid = $form.find('.subsalesgrid [data-name="OrderItemGrid"]'),
    //        $subLaborGrid = $form.find('.sublaborgrid [data-name="OrderItemGrid"]'),
    //        $subMiscGrid = $form.find('.submiscgrid [data-name="OrderItemGrid"]'),
    //        fields = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field');
    //    let fieldNames: Array<string> = [];

    //    for (let i = 3; i < fields.length; i++) {
    //        let name = jQuery(fields[i]).attr('data-mappedfield');
    //        if (name !== "QuantityOrdered") {
    //            fieldNames.push(name);
    //        }
    //    }

    //    FwAppData.apiMethod(true, 'GET', `api/v1/potype/${POTYPE}`, null, FwServices.defaultTimeout, function onSuccess(response) {
    //        let hiddenPurchase: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.PurchaseShowFields));
    //        let hiddenMisc: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.MiscShowFields));
    //        let hiddenLabor: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.LaborShowFields));
    //        let hiddenSubRental: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.SubRentalShowFields));
    //        let hiddenSubSale: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.SubSaleShowFields));
    //        let hiddenSubMisc: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.SubMiscShowFields));
    //        let hiddenSubLabor: Array<string> = fieldNames.filter(function (field) { return !this.has(field) }, new Set(response.SubLaborShowFields));
    //        // Non-specific showfields
    //        for (let i = 0; i < hiddenPurchase.length; i++) {
    //            jQuery($rentalGrid.find(`[data-mappedfield="${hiddenPurchase[i]}"]`)).parent().hide();
    //            jQuery($salesGrid.find(`[data-mappedfield="${hiddenPurchase[i]}"]`)).parent().hide();
    //            jQuery($partGrid.find(`[data-mappedfield="${hiddenPurchase[i]}"]`)).parent().hide();
    //        }
    //        // Specific showfields
    //        for (let i = 0; i < hiddenMisc.length; i++) { jQuery($miscGrid.find(`[data-mappedfield="${hiddenMisc[i]}"]`)).parent().hide(); }
    //        for (let i = 0; i < hiddenLabor.length; i++) { jQuery($laborGrid.find(`[data-mappedfield="${hiddenLabor[i]}"]`)).parent().hide(); }
    //        for (let i = 0; i < hiddenSubSale.length; i++) { jQuery($subSalesGrid.find(`[data-mappedfield="${hiddenSubSale[i]}"]`)).parent().hide(); }
    //        for (let i = 0; i < hiddenSubRental.length; i++) { jQuery($subRentalGrid.find(`[data-mappedfield="${hiddenSubRental[i]}"]`)).parent().hide(); }
    //        for (let i = 0; i < hiddenSubLabor.length; i++) { jQuery($subLaborGrid.find(`[data-mappedfield="${hiddenSubLabor[i]}"]`)).parent().hide(); }
    //        for (let i = 0; i < hiddenSubMisc.length; i++) { jQuery($subMiscGrid.find(`[data-mappedfield="${hiddenSubMisc[i]}"]`)).parent().hide(); }
    //    }, null, null);
    //};
    ////----------------------------------------------------------------------------------------------
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
    //----------------------------------------------------------------------------------------------

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
//Assign Bar Codes
FwApplicationTree.clickEvents['{649E744B-0BDD-43ED-BB6E-5945CBB0BFA5}'] = function (e) {
    const $form = jQuery(this).closest('.fwform');
    try {
        const mode = 'EDIT';
        let purchaseOrderInfo: any = {};
        purchaseOrderInfo.PurchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        purchaseOrderInfo.PurchaseOrderNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
        const $assignBarCodesForm = AssignBarCodesController.openForm(mode, purchaseOrderInfo);
        FwModule.openSubModuleTab($form, $assignBarCodesForm);
        jQuery('.tab.submodule.active').find('.caption').html('Assign Bar Codes');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------

var PurchaseOrderController = new PurchaseOrder();