//routes.push({ pattern: /^module\/purchaseorder$/, action: function (match: RegExpExecArray) { return PurchaseOrderController.getModuleScreen(); } });
//routes.push({ pattern: /^module\/purchaseorder\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return PurchaseOrderController.getModuleScreen(filter); } });

//----------------------------------------------------------------------------------------------
class PurchaseOrder {
    Module: string = 'PurchaseOrder';
    apiurl: string = 'api/v1/purchaseorder';
    caption: string = Constants.Modules.Home.PurchaseOrder.caption;
    nav: string = Constants.Modules.Home.PurchaseOrder.nav;
    id: string = Constants.Modules.Home.PurchaseOrder.id;
    DefaultPurchasePoType: string;
    DefaultPurchasePoTypeId: string;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    CachedPurchaseOrderTypes: any = {};
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        const $browse = this.openBrowse();
        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            const chartFilters = JSON.parse(sessionStorage.getItem('chartfilter'));
            if (!chartFilters) {
                FwBrowse.databind($browse);
                FwBrowse.screenload($browse);
            }
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        const location = JSON.parse(sessionStorage.getItem('location'));

        FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
            if (dt.Rows[rowIndex][dt.ColumnIndex['Status']] === 'VOID') {
                $tr.css('color', '#aaaaaa');
            }
        });

        FwAppData.apiMethod(true, 'GET', `api/v1/officelocation/${location.locationid}`, null, FwServices.defaultTimeout, response => {
            this.DefaultPurchasePoType = response.DefaultPurchasePoType;
            this.DefaultPurchasePoTypeId = response.DefaultPurchasePoTypeId;
        }, null, null);

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

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
        });

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
        let $form = FwModule.loadFormFromTemplate(this.Module);
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

        const $emailHistorySubModuleBrowse = this.openEmailHistoryBrowse($form);
        $form.find('.emailhistory-page').append($emailHistorySubModuleBrowse);

        FwFormField.disable($form.find('[data-datafield="RentalTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="SalesTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="LaborTaxRate1"]'));

        //Toggle Buttons - SubRental tab - Sub-Rental totals
        FwFormField.loadItems($form.find('div[data-datafield="totalTypeSubRental"]'), [
            { value: 'W', caption: 'Weekly' },
            { value: 'M', caption: 'Monthly' },
            { value: 'P', caption: 'Period' }
        ]);

        this.events($form);
        this.activityCheckboxEvents($form, mode);
        this.renderSearchButton($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="PurchaseOrderId"] input').val(uniqueids.PurchaseOrderId);
        FwModule.loadForm(this.Module, $form);

        const $vendorInvoiceBrowse = this.openVendorInvoiceBrowse($form);
        $form.find('.vendorinvoice').append($vendorInvoiceBrowse);
        $form.find('.contractSubModule').append(this.openContractBrowse($form));

        //replace default click event on "New" button in Vendor Invoice sub-module to default PO
        $vendorInvoiceBrowse.find('[data-type="NewMenuBarButton"]')
            .off('click')
            .on('click', createNewVendorInvoice);

        function createNewVendorInvoice() {
            const $vendorInvoiceForm = VendorInvoiceController.openForm('NEW');
            const poNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
            const poId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            FwFormField.setValueByDataField($vendorInvoiceForm, 'PurchaseOrderId', poId, poNumber, true);
            FwModule.openSubModuleTab($vendorInvoiceBrowse, $vendorInvoiceForm);
        }

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    openEmailHistoryBrowse($form) {
        const $browse = EmailHistoryController.openBrowse();

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
    renderSearchButton($form: any) {
        const $search = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'QuikSearch', 'searchbtn');
        $search.prepend('<i class="material-icons">search</i>');
        $search.on('click', e => {
            try {
                const $form = jQuery(e.currentTarget).closest('.fwform');
                const orderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);

                if (orderId == "") {
                    FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
                } else if (!jQuery(e.currentTarget).hasClass('disabled')) {
                    const search = new SearchInterface();
                    search.renderSearchPopup($form, orderId, this.Module);
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
        // ----------
        const $orderStatusHistoryGrid = $form.find('div[data-grid="OrderStatusHistoryGrid"]');
        const $orderStatusHistoryGridControl = FwBrowse.loadGridFromTemplate('OrderStatusHistoryGrid');
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
        const $orderItemGridRentalControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $orderItemGridRentalControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        $orderItemGridRentalControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridRentalControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridRentalControl.find('div[data-datafield="PeriodExtended"]').attr('data-formreadonly', 'true');

        $orderItemGridRental.empty().append($orderItemGridRentalControl);
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
        const $orderItemGridSalesControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $orderItemGridSalesControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        $orderItemGridSalesControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridSalesControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridSales.empty().append($orderItemGridSalesControl);
        $orderItemGridSales.addClass('S');

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
        const $orderItemGridPart = $form.find('.partsgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridPartControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $orderItemGridPartControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        $orderItemGridPartControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridPartControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridPart.empty().append($orderItemGridPartControl);
        $orderItemGridPart.addClass('P');

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
            const partItems = $form.find('.partsgrid tbody').children();
            partItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Parts"]')) : FwFormField.enable($form.find('[data-datafield="Parts"]'));
        });

        FwBrowse.init($orderItemGridPartControl);
        FwBrowse.renderRuntimeHtml($orderItemGridPartControl);
        // ----------
        const $orderItemGridLabor = $form.find('.laborgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridLaborControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $orderItemGridLaborControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        $orderItemGridLaborControl.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
        $orderItemGridLaborControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridLaborControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridLabor.empty().append($orderItemGridLaborControl);
        $orderItemGridLabor.addClass('L');

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
        $orderItemGridMiscControl.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
        $orderItemGridMiscControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        $orderItemGridMiscControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridMiscControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridMisc.empty().append($orderItemGridMiscControl);
        $orderItemGridMisc.addClass('M');

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

        $orderItemGridSubRentControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                RecType: 'R',
                Subs: true
            };
        });
        $orderItemGridSubRentControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'R';
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

        $oderItemGridSubSalesControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                RecType: 'S',
                Subs: true
            };
        });
        $oderItemGridSubSalesControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'S';
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
        $orderItemGridSubLaborControl.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
        $orderItemGridSubLaborControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridSubLaborControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridSubLabor.empty().append($orderItemGridSubLaborControl);
        $orderItemGridSubLabor.addClass('L');

        $orderItemGridSubLaborControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                RecType: 'L',
                Subs: true
            };
        });
        $orderItemGridSubLaborControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'L';
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
        $orderItemGridSubMiscControl.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
        $orderItemGridSubMiscControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridSubMiscControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridSubMisc.empty().append($orderItemGridSubMiscControl);
        $orderItemGridSubMisc.addClass('R');

        $orderItemGridSubMiscControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                RecType: 'R',
                Subs: true
            };
        });
        $orderItemGridSubMiscControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.RecType = 'R';
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
        let $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        let $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        let $partsGrid = $form.find('.partsgrid [data-name="OrderItemGrid"]');
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

        if (this.CachedPurchaseOrderTypes[purchaseOrderTypeId] !== undefined) {
            applyPurchaseOrderTypeToColumns($form, this.CachedPurchaseOrderTypes[purchaseOrderTypeId]);
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
                }, new Set(response.PurchaseShowFields))
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
            for (var m = 0; m < purchaseOrderTypeData.hiddenPurchase.length; m++) {
                jQuery($rentalGrid.find('[data-mappedfield="' + purchaseOrderTypeData.hiddenPurchase[m] + '"]')).parent().hide();
                jQuery($salesGrid.find('[data-mappedfield="' + purchaseOrderTypeData.hiddenPurchase[m] + '"]')).parent().hide();
                jQuery($partsGrid.find('[data-mappedfield="' + purchaseOrderTypeData.hiddenPurchase[m] + '"]')).parent().hide();
            }
            for (let o = 0; o < purchaseOrderTypeData.hiddenSubLabor.length; o++) {
                jQuery($subLaborGrid.find(`[data-mappedfield="${purchaseOrderTypeData.hiddenSubLabor[o]}"]`)).parent().hide();
            }
            for (let p = 0; p < purchaseOrderTypeData.hiddenSubMisc.length; p++) {
                jQuery($subMiscGrid.find('[data-mappedfield="' + purchaseOrderTypeData.hiddenSubMisc[p] + '"]')).parent().hide();
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
        this.applyPurchaseOrderTypeAndRateTypeToForm($form);

        const status = FwFormField.getValueByDataField($form, 'Status');
        if (status === 'VOID' || status === 'CLOSED' || status === 'SNAPSHOT') {
            FwModule.setFormReadOnly($form);
            $form.find('.btn[data-securityid="searchbtn"]').addClass('disabled');
        }

        const $scheduleDateFields = $form.find('.activity-unchecked');
        const isRental = FwFormField.getValueByDataField($form, 'Rental');
        const isSales = FwFormField.getValueByDataField($form, 'Sales');
        const isMisc = FwFormField.getValueByDataField($form, 'Miscellaneous');
        const isLabor = FwFormField.getValueByDataField($form, 'Labor');
        const isParts = FwFormField.getValueByDataField($form, 'Parts');
        const isSubRent = FwFormField.getValueByDataField($form, 'SubRent');
        const isSubSale = FwFormField.getValueByDataField($form, 'SubSale');
        const isRepair = FwFormField.getValueByDataField($form, 'Repair');
        const isSubMisc = FwFormField.getValueByDataField($form, 'SubMiscellaneous');
        const isSubLabor = FwFormField.getValueByDataField($form, 'SubLabor');

        if (!isRental) { $form.find('.rentalinventorytab').hide() }
        if (!isSales) { $form.find('.salesinventorytab').hide() }
        if (!isMisc) { $form.find('.misctab').hide() }
        if (!isLabor) { $form.find('.labortab').hide() }
        if (!isParts) { $form.find('.partstab').hide() }
        if (!isSubRent) { $form.find('.subrentaltab').hide() }
        if (!isSubSale) { $form.find('.subsalestab').hide() }
        if (!isRepair) { $form.find('.repairtab').hide() }
        if (!isSubMisc) { $form.find('.submisctab').hide() }
        if (!isSubLabor) { $form.find('.sublabortab').hide() }

        if (!isMisc && !isLabor && !isSubRent && !isSubSale && !isSubMisc && !isSubLabor) $scheduleDateFields.hide();

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

        const $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        const $orderItemGridSales = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        const $orderItemGridPart = $form.find('.partsgrid [data-name="OrderItemGrid"]');
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

        //these are the "Search" options, which is now available in the PO header bar
        //$orderItemGridSubRent.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();
        //$orderItemGridSubSales.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();
        //$orderItemGridSubLabor.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();
        //$orderItemGridSubMisc.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();

        $orderItemGridSubRent.find('.buttonbar').hide();
        $orderItemGridSubSales.find('.buttonbar').hide();
        $orderItemGridSubLabor.find('.buttonbar').hide();
        $orderItemGridSubMisc.find('.buttonbar').hide();

        const rateType = FwFormField.getValueByDataField($form, 'RateType');
        // Display D/W field in subrental tab
        if (rateType === 'DAILY') {
            $form.find(".subRentalDaysPerWeek").show();
        } else {
            $form.find(".subRentalDaysPerWeek").hide();
        }
        // responsible for weekly, monthly toggle button on sub-rental tab
        if (rateType === 'MONTHLY') {
            $form.find('.togglebutton-item input[value="W"]').parent().hide();
            $form.find('.togglebutton-item input[value="M"]').parent().show();
        } else {
            $form.find('.togglebutton-item input[value="W"]').parent().show();
            $form.find('.togglebutton-item input[value="M"]').parent().hide();
        }
        //resets back to period summary frames
        $form.find('.togglebutton-item input[value="P"]').click();

        // this.dynamicColumns($form);
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
            $orderItemGrid = $form.find('.partsgrid [data-name="OrderItemGrid"]');
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
            $orderItemGrid = $form.find('.partsgrid [data-name="OrderItemGrid"]');
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
        const rentalTab = $form.find('.rentalinventorytab')
            , salesTab = $form.find('.salesinventorytab')
            , partsTab = $form.find('.partstab')
            , miscTab = $form.find('.misctab')
            , laborTab = $form.find('.labortab')
            , subrentalTab = $form.find('.subrentaltab')
            , subsalesTab = $form.find('.subsalestab')
            , repairTab = $form.find('.repairtab')
            , submiscTab = $form.find('.submisctab')
            , sublaborTab = $form.find('.sublabortab');
        const $scheduleDateFields = $form.find('.activity-unchecked');
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
            if (jQuery(e.currentTarget).prop('checked')) {
                miscTab.show();
                $scheduleDateFields.show();
            } else {
                miscTab.hide();
            }
        });
        $form.find('[data-datafield="Labor"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                laborTab.show();
                $scheduleDateFields.show();
            }
            else {
                laborTab.hide();
            }
        });
        $form.find('[data-datafield="SubRent"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                subrentalTab.show();
                $scheduleDateFields.show();
            } else {
                subrentalTab.hide();
            }
        });
        $form.find('[data-datafield="SubSale"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                subsalesTab.show();
                $scheduleDateFields.show();
            } else {
                subsalesTab.hide();
            }
        });
        $form.find('[data-datafield="Repair"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? repairTab.show() : repairTab.hide();
        });
        $form.find('[data-datafield="SubMiscellaneous"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                submiscTab.show();
                $scheduleDateFields.show();
            } else {
                submiscTab.hide();
            }
        });
        $form.find('[data-datafield="SubLabor"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                sublaborTab.show();
                $scheduleDateFields.show();
            } else {
                sublaborTab.hide();
            }
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
    voidPO($form: JQuery): void {
        const status = FwFormField.getValueByDataField($form, 'Status');
        if (status === 'NEW') {
            const $confirmation = FwConfirmation.renderConfirmation('Void', '');
            $confirmation.find('.fwconfirmationbox').css('width', '450px');
            const html: Array<string> = [];
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div>Void this Purchase Order?</div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));
            const $yes = FwConfirmation.addButton($confirmation, 'Void', false);
            const $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', makeVoid);
            // ----------
            function makeVoid() {

                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Voiding...');
                $yes.off('click');

                const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
                FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorder/void/${purchaseOrderId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Purchase Order Successfully Voided');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwModule.refreshForm($form, PurchaseOrderController);
                }, function onError(response) {
                    $yes.on('click', makeVoid);
                    $yes.text('Void');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                    FwModule.refreshForm($form, PurchaseOrderController);
                }, $form);
            };
        } else {
            FwNotification.renderNotification('WARNING', 'Only NEW Purchase Orders can be voided.');
        }
    };
    //----------------------------------------------------------------------------------------------
    //dynamicColumns($form: any): void {
    //    const POTYPE = FwFormField.getValueByDataField($form, "PoTypeId"),
    //        $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
    //        $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]'),
    //        $partsgrid = $form.find('.partsgrid [data-name="OrderItemGrid"]'),
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
    //            jQuery($partsgrid.find(`[data-mappedfield="${hiddenPurchase[i]}"]`)).parent().hide();
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
            const request: any = {},
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
        let weeklyType = $form.find(".weeklyType");
        let monthlyType = $form.find(".monthlyType");
        const subRentalDaysPerWeek = $form.find('div[data-datafield="SubRentalDaysPerWeek"]');
        let billingMonths = $form.find(".BillingMonths");
        let billingWeeks = $form.find(".BillingWeeks");

        $form.find(".weeklyType").show();
        $form.find(".monthlyType").hide();
        $form.find(".periodType input").prop('checked', true);
        //Hide/Show summary buttons based on rate type
        $form.find('[data-datafield="RateType"] input').on('change', e => {
            const rateType = FwFormField.getValueByDataField($form, 'RateType');
            if (rateType === 'MONTHLY') {
                $form.find('.togglebutton-item input[value="W"]').parent().hide();
                $form.find('.togglebutton-item input[value="M"]').parent().show();
            } else {
                $form.find('.togglebutton-item input[value="W"]').parent().show();
                $form.find('.togglebutton-item input[value="M"]').parent().hide();
            }
            //resets back to period summary frames
            $form.find('.togglebutton-item input[value="P"]').click();
        });
        // total type radio on sub-rental tab
        $form.find(`[data-datafield="totalTypeSubRental"] input`).on('change', e => {
            let $target = jQuery(e.currentTarget),
                gridType = $target.parents('[data-datafield="totalTypeSubRental"]').attr('data-gridtype'),
                rateType = $target.val(),
                adjustmentsPeriod = $form.find(`.${gridType}AdjustmentsPeriod`),
                adjustmentsWeekly = $form.find(`.${ gridType }AdjustmentsWeekly`),
                adjustmentsMonthly = $form.find(`.${gridType}AdjustmentsMonthly`);
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
            const total = FwFormField.getValue($form, `.${gridType}-total:visible`);
            if (total === '0.00') {
                FwFormField.disable($form.find(`.${gridType}-total-wtax:visible`));
            } else {
                FwFormField.enable($form.find(`.${gridType}-total-wtax:visible`));
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
FwApplicationTree.clickEvents[Constants.Modules.Home.PurchaseOrder.form.menuItems.ReceiveFromVendor.id] = function (e: JQuery.ClickEvent) {
    try {
        const $form = jQuery(this).closest('.fwform');
        const purchaseOrderInfo: any = {};
        purchaseOrderInfo.PurchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        purchaseOrderInfo.PurchaseOrderNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
        let $receiveFromVendorForm = ReceiveFromVendorController.openForm('EDIT', purchaseOrderInfo);
        FwModule.openSubModuleTab($form, $receiveFromVendorForm);
        jQuery('.tab.submodule.active').find('.caption').html('Receive From Vendor');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents[Constants.Modules.Home.PurchaseOrder.form.menuItems.ReturnToVendor.id] = function (e: JQuery.ClickEvent) {
    try {
        const $form = jQuery(this).closest('.fwform');
        const purchaseOrderInfo: any = {};
        purchaseOrderInfo.PurchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        purchaseOrderInfo.PurchaseOrderNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
        let $returnToVendorForm = ReturnToVendorController.openForm('EDIT', purchaseOrderInfo);
        FwModule.openSubModuleTab($form, $returnToVendorForm);
        jQuery('.tab.submodule.active').find('.caption').html('Return To Vendor');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
// Void PO
FwApplicationTree.clickEvents[Constants.Modules.Home.PurchaseOrder.form.menuItems.Void.id] = function (event: JQuery.ClickEvent) {
    try {
        const $form = jQuery(this).closest('.fwform');
        PurchaseOrderController.voidPO($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents[Constants.Modules.Home.PurchaseOrder.form.menuItems.Search.id] = function (e: JQuery.ClickEvent) {
    try {
        const $form = jQuery(this).closest('.fwform');
        const orderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');

        if (orderId == "") {
            FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
        } else {
            const search = new SearchInterface();
            const $popup = search.renderSearchPopup($form, orderId, 'PurchaseOrder');
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
//Assign Bar Codes
FwApplicationTree.clickEvents[Constants.Modules.Home.PurchaseOrder.form.menuItems.AssignBarCodes.id] = function (e: JQuery.ClickEvent) {
    try {
        const $form = jQuery(this).closest('.fwform');
        const purchaseOrderInfo: any = {};
        purchaseOrderInfo.PurchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        purchaseOrderInfo.PurchaseOrderNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
        const $assignBarCodesForm = AssignBarCodesController.openForm('EDIT', purchaseOrderInfo);
        FwModule.openSubModuleTab($form, $assignBarCodesForm);
        jQuery('.tab.submodule.active').find('.caption').html('Assign Bar Codes');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------

var PurchaseOrderController = new PurchaseOrder();