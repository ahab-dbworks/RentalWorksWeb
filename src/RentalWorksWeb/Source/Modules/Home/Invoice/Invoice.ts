routes.push({ pattern: /^module\/invoice$/, action: function (match: RegExpExecArray) { return InvoiceController.getModuleScreen(); } });
routes.push({ pattern: /^module\/invoice\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return InvoiceController.getModuleScreen(filter); } });

//----------------------------------------------------------------------------------------------
class Invoice {
    Module: string = 'Invoice';
    apiurl: string = 'api/v1/invoice';
    caption: string = 'Invoice';
    nav: string = 'module/invoice';
    id: string = '9B79D7D8-08A1-4F6B-AC0A-028DFA9FE10F';
    ActiveView: string = 'ALL';

    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        let screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        let $browse = this.openBrowse();
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
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        let location = JSON.parse(sessionStorage.getItem('location'));
        this.ActiveView = `LocationId=${location.locationid}`;

        $browse.data('ondatabind', request => {
            request.activeview = this.ActiveView;
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
    addBrowseMenuItems($menuObject: any) {
        let location = JSON.parse(sessionStorage.getItem('location'));
        let $new = FwMenu.generateDropDownViewBtn('New', false);
        let $approved = FwMenu.generateDropDownViewBtn('Approved', false);
        let $newapproved = FwMenu.generateDropDownViewBtn('New & Approved', false);
        let $processed = FwMenu.generateDropDownViewBtn('Processed', false);
        let $closed = FwMenu.generateDropDownViewBtn('Closed', false);
        let $void = FwMenu.generateDropDownViewBtn('Void', false);
        let $all = FwMenu.generateDropDownViewBtn('All', true);
        let view = [];
        view[0] = `LocationId=${location.locationid}`;

        $new.on('click', e => {
            let $browse;
            $browse = jQuery(e.currentTarget).closest('.fwbrowse');
            this.ActiveView = 'Status=NEW';
            view[1] = this.ActiveView;
            if (view.length > 1) {
                this.ActiveView = view.join(', ');
            }
            FwBrowse.search($browse);
        });
        $approved.on('click', e => {
            let $browse;
            $browse = jQuery(e.currentTarget).closest('.fwbrowse');
            this.ActiveView = 'Status=APPROVED';
            view[1] = this.ActiveView;
            if (view.length > 1) {
                this.ActiveView = view.join(', ');
            }
            FwBrowse.search($browse);
        });
        $newapproved.on('click', e => {
            let $browse;
            $browse = jQuery(e.currentTarget).closest('.fwbrowse');
            this.ActiveView = 'Status=NEWAPPROVED';
            view[1] = this.ActiveView;
            if (view.length > 1) {
                this.ActiveView = view.join(', ');
            }
            FwBrowse.search($browse);
        });
        $processed.on('click', e => {
            let $browse;
            $browse = jQuery(e.currentTarget).closest('.fwbrowse');
            this.ActiveView = 'Status=PROCESSED';
            view[1] = this.ActiveView;
            if (view.length > 1) {
                this.ActiveView = view.join(', ');
            }
            FwBrowse.search($browse);
        });
        $closed.on('click', e => {
            let $browse;
            $browse = jQuery(e.currentTarget).closest('.fwbrowse');
            this.ActiveView = 'Status=CLOSED';
            view[1] = this.ActiveView;
            if (view.length > 1) {
                this.ActiveView = view.join(', ');
            }
            FwBrowse.search($browse);
        });
        $void.on('click', e => {
            let $browse;
            $browse = jQuery(e.currentTarget).closest('.fwbrowse');
            this.ActiveView = 'Status=VOID';
            view[1] = this.ActiveView;
            if (view.length > 1) {
                this.ActiveView = view.join(', ');
            }
            FwBrowse.search($browse);
        });
        $all.on('click', e => {
            let $browse;
            $browse = jQuery(e.currentTarget).closest('.fwbrowse');
            this.ActiveView = 'Status=ALL';
            view[1] = this.ActiveView;
            if (view.length > 1) {
                this.ActiveView = view.join(', ');
            }
            FwBrowse.search($browse);
        });

        const viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $new, $approved, $newapproved, $processed, $closed, $void);

        let $view;
        $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);

        //Location Filter
        let $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false);
        let $userLocation = FwMenu.generateDropDownViewBtn(location.location, true);
        $allLocations.on('click', e => {
            let $browse;
            $browse = jQuery(e.currentTarget).closest('.fwbrowse');
            this.ActiveView = 'LocationId=ALL';
            view[0] = this.ActiveView;
            if (view.length > 1) {
                this.ActiveView = view.join(', ');
            }
            FwBrowse.search($browse);
        });
        $userLocation.on('click', e => {
            let $browse;
            $browse = jQuery(e.currentTarget).closest('.fwbrowse');
            this.ActiveView = `LocationId=${location.locationid}`;
            view[0] = this.ActiveView;
            if (view.length > 1) {
                this.ActiveView = view.join(', ');
            }
            FwBrowse.search($browse);
        });
        const viewLocation = [];
        viewLocation.push($allLocations, $userLocation);
        let $locationView;
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

            FwFormField.setValue($form, 'div[data-datafield="ProjectManagerId"]', usersid, name);
            FwFormField.setValue($form, 'div[data-datafield="AgentId"]', usersid, name);
            FwFormField.setValue($form, 'div[data-datafield="OutsideSalesRepresentativeId"]', usersid, name);

            FwFormField.setValueByDataField($form, 'BillingStartDate', today);
            FwFormField.setValueByDataField($form, 'InvoiceDate', today);
            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            //FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
            //FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

            //FwFormField.disable($form.find('.frame'));
            //$form.find(".frame .add-on").children().hide();
        } else {
            FwFormField.disable($form.find('.ifnew'));
        }

        this.events($form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="InvoiceId"] input').val(uniqueids.InvoiceId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: JQuery, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        const maxPageSize = 9999;
        // ----------
        let $invoiceItemGridRental;
        let $invoiceItemGridRentalControl;
        $invoiceItemGridRental = $form.find('.rentalgrid div[data-grid="InvoiceItemGrid"]');
        $invoiceItemGridRentalControl = jQuery(jQuery('#tmpl-grids-InvoiceItemGridBrowse').html());
        $invoiceItemGridRental.empty().append($invoiceItemGridRentalControl);
        $invoiceItemGridRentalControl.data('isSummary', false);
        $invoiceItemGridRental.addClass('R');
        $invoiceItemGridRentalControl.attr('data-enabled', 'false');
        $invoiceItemGridRentalControl.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Rate');

        $invoiceItemGridRentalControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId'),
                RecType: 'R'
            };
            request.pagesize = maxPageSize;
        });
        $invoiceItemGridRentalControl.data('beforesave', request => {
            request.InvoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
            request.RecType = 'R';
        });

        FwBrowse.addEventHandler($invoiceItemGridRentalControl, 'afterdatabindcallback', () => {
            this.calculateInvoiceItemGridTotals($form, 'rental');
            //let rentalItems = $form.find('.rentalgrid tbody').children();
            //rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
        });
        FwBrowse.init($invoiceItemGridRentalControl);
        FwBrowse.renderRuntimeHtml($invoiceItemGridRentalControl);
        // ----------
        let $invoiceNoteGrid;
        let $invoiceNoteGridControl;
        $invoiceNoteGrid = $form.find('div[data-grid="InvoiceNoteGrid"]');
        $invoiceNoteGridControl = FwBrowse.loadGridFromTemplate('InvoiceNoteGrid');
        $invoiceNoteGrid.empty().append($invoiceNoteGridControl);
        $invoiceNoteGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InvoiceId: $form.find('div.fwformfield[data-datafield="InvoiceId"] input').val()
            }
        });
        $invoiceNoteGridControl.data('beforesave', function (request) {
            request.InvoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
        })
        FwBrowse.init($invoiceNoteGridControl);
        FwBrowse.renderRuntimeHtml($invoiceNoteGridControl);
        // ----------
        let $invoiceItemGridSales;
        let $invoiceItemGridSalesControl;
        $invoiceItemGridSales = $form.find('.salesgrid div[data-grid="InvoiceItemGrid"]');
        $invoiceItemGridSalesControl = jQuery(jQuery('#tmpl-grids-InvoiceItemGridBrowse').html());
        $invoiceItemGridSales.empty().append($invoiceItemGridSalesControl);
        $invoiceItemGridSales.addClass('S');
        $invoiceItemGridSalesControl.attr('data-enabled', 'false');
        $invoiceItemGridSalesControl.data('isSummary', false);
        $invoiceItemGridSalesControl.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Price');

        $invoiceItemGridSalesControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId'),
                RecType: 'S'
            };
            request.pagesize = maxPageSize;
        });
        $invoiceItemGridSalesControl.data('beforesave', request => {
            request.InvoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
            request.RecType = 'S';
        });
        FwBrowse.addEventHandler($invoiceItemGridSalesControl, 'afterdatabindcallback', () => {
            this.calculateInvoiceItemGridTotals($form, 'sales');
            //let salesItems = $form.find('.salesgrid tbody').children();
            //salesItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Sales"]')) : FwFormField.enable($form.find('[data-datafield="Sales"]'));
        });
        FwBrowse.init($invoiceItemGridSalesControl);
        FwBrowse.renderRuntimeHtml($invoiceItemGridSalesControl);
        // ----------
        let $invoiceItemGridLabor;
        let $invoiceItemGridLaborControl;
        $invoiceItemGridLabor = $form.find('.laborgrid div[data-grid="InvoiceItemGrid"]');
        $invoiceItemGridLaborControl = jQuery(jQuery('#tmpl-grids-InvoiceItemGridBrowse').html());
        $invoiceItemGridLabor.empty().append($invoiceItemGridLaborControl);
        $invoiceItemGridLabor.addClass('L');
        $invoiceItemGridLabor.find('div[data-datafield="Extended"]').attr('data-formreadonly', 'true');
        $invoiceItemGridLabor.find('div[data-datafield="InventoryId"]').attr('data-formreadonly', 'true');
        $invoiceItemGridLabor.find('div[data-datafield="OrderNumber"]').attr('data-formreadonly', 'true');
        $invoiceItemGridLabor.find('div[data-datafield="Taxable"]').attr('data-formreadonly', 'true');
        $invoiceItemGridLaborControl.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Rate');

        $invoiceItemGridLaborControl.data('isSummary', false);

        $invoiceItemGridLaborControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId'),
                RecType: 'L'
            };
            request.pagesize = maxPageSize;
        });
        $invoiceItemGridLaborControl.data('beforesave', request => {
            request.InvoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
            request.RecType = 'L';
        });
        FwBrowse.addEventHandler($invoiceItemGridLaborControl, 'afterdatabindcallback', () => {
            this.calculateInvoiceItemGridTotals($form, 'labor');
            //let laborItems = $form.find('.laborgrid tbody').children();
            //laborItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Labor"]')) : FwFormField.enable($form.find('[data-datafield="Labor"]'));
        });
        FwBrowse.init($invoiceItemGridLaborControl);
        FwBrowse.renderRuntimeHtml($invoiceItemGridLaborControl);
        // ----------
        let $invoiceItemGridMisc;
        let $invoiceItemGridMiscControl;
        $invoiceItemGridMisc = $form.find('.miscgrid div[data-grid="InvoiceItemGrid"]');
        $invoiceItemGridMiscControl = jQuery(jQuery('#tmpl-grids-InvoiceItemGridBrowse').html());
        $invoiceItemGridMisc.empty().append($invoiceItemGridMiscControl);
        $invoiceItemGridMisc.addClass('M');
        $invoiceItemGridMisc.find('div[data-datafield="Extended"]').attr('data-formreadonly', 'true')
        $invoiceItemGridMiscControl.data('isSummary', false);
        $invoiceItemGridMiscControl.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Rate');

        $invoiceItemGridMiscControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId'),
                RecType: 'M'
            };
            request.pagesize = maxPageSize;
        });
        $invoiceItemGridMiscControl.data('beforesave', request => {
            request.InvoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
            request.RecType = 'M';
        });
        FwBrowse.addEventHandler($invoiceItemGridMiscControl, 'afterdatabindcallback', () => {
            this.calculateInvoiceItemGridTotals($form, 'misc');
            //let miscItems = $form.find('.miscgrid tbody').children();
            //miscItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Miscellaneous"]')) : FwFormField.enable($form.find('[data-datafield="Miscellaneous"]'));
        });
        FwBrowse.init($invoiceItemGridMiscControl);
        FwBrowse.renderRuntimeHtml($invoiceItemGridMiscControl);
        // ----------
        let $invoiceItemGridRentalSale;
        let $invoiceItemGridRentalSaleControl;
        $invoiceItemGridRentalSale = $form.find('.rentalsalegrid div[data-grid="InvoiceItemGrid"]');
        $invoiceItemGridRentalSaleControl = jQuery(jQuery('#tmpl-grids-InvoiceItemGridBrowse').html());
        $invoiceItemGridRentalSale.empty().append($invoiceItemGridRentalSaleControl);
        $invoiceItemGridRentalSale.addClass('RS');
        $invoiceItemGridRentalSaleControl.attr('data-enabled', 'false');
        $invoiceItemGridRentalSaleControl.data('isSummary', false);
        $invoiceItemGridRentalSaleControl.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Price');

        $invoiceItemGridRentalSaleControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId'),
                RecType: 'RS'
            };
            request.pagesize = maxPageSize;
        });
        $invoiceItemGridRentalSaleControl.data('beforesave', function (request) {
            request.InvoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
            request.RecType = 'RS';
        });
        FwBrowse.addEventHandler($invoiceItemGridMiscControl, 'afterdatabindcallback', () => {
            this.calculateInvoiceItemGridTotals($form, 'rentalsale');
            //let rentalSaleItems = $form.find('.rentalsale tbody').children();
            //rentalSaleItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="RentalSale"]')) : FwFormField.enable($form.find('[data-datafield="RentalSale"]'));
        });
        FwBrowse.init($invoiceItemGridRentalSaleControl);
        FwBrowse.renderRuntimeHtml($invoiceItemGridRentalSaleControl);
        // ----------
        let $glDistributionGrid;
        let $glDistributionGridControl;
        $glDistributionGrid = $form.find('div[data-grid="GlDistributionGrid"]');
        $glDistributionGridControl = FwBrowse.loadGridFromTemplate('GlDistributionGrid');
        $glDistributionGrid.empty().append($glDistributionGridControl);
        $glDistributionGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId')
            };
        });
        FwBrowse.init($glDistributionGridControl);
        FwBrowse.renderRuntimeHtml($glDistributionGridControl);
        // ----------
        let $invoiceOrderGrid;
        let $invoiceOrderGridControl;
        $invoiceOrderGrid = $form.find('div[data-grid="InvoiceOrderGrid"]');
        $invoiceOrderGridControl = FwBrowse.loadGridFromTemplate('InvoiceOrderGrid');
        $invoiceOrderGrid.empty().append($invoiceOrderGridControl);
        $invoiceOrderGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId')
            };
        });
        FwBrowse.init($invoiceOrderGridControl);
        FwBrowse.renderRuntimeHtml($invoiceOrderGridControl);
        // ----------
        let $invoiceRevenueGrid;
        let $invoiceRevenueGridControl;
        $invoiceRevenueGrid = $form.find('div[data-grid="InvoiceRevenueGrid"]');
        $invoiceRevenueGridControl = FwBrowse.loadGridFromTemplate('InvoiceRevenueGrid');
        $invoiceRevenueGrid.empty().append($invoiceRevenueGridControl);
        $invoiceRevenueGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId')
            };
        });
        FwBrowse.init($invoiceRevenueGridControl);
        FwBrowse.renderRuntimeHtml($invoiceRevenueGridControl);
        // ----------
        let $invoiceReceiptGrid;
        let $invoiceReceiptGridControl;
        $invoiceReceiptGrid = $form.find('div[data-grid="InvoiceReceiptGrid"]');
        $invoiceReceiptGridControl = FwBrowse.loadGridFromTemplate('InvoiceReceiptGrid');
        $invoiceReceiptGrid.empty().append($invoiceReceiptGridControl);
        $invoiceReceiptGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId')
            };
        });
        FwBrowse.init($invoiceReceiptGridControl);
        FwBrowse.renderRuntimeHtml($invoiceReceiptGridControl);
        // ----------
        let $invoiceStatusHistoryGrid;
        let $invoiceStatusHistoryGridControl;
        $invoiceStatusHistoryGrid = $form.find('div[data-grid="InvoiceStatusHistoryGrid"]');
        $invoiceStatusHistoryGridControl = FwBrowse.loadGridFromTemplate('InvoiceStatusHistoryGrid');
        $invoiceStatusHistoryGrid.empty().append($invoiceStatusHistoryGridControl);
        $invoiceStatusHistoryGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId')
            };
        });
        FwBrowse.init($invoiceStatusHistoryGridControl);
        FwBrowse.renderRuntimeHtml($invoiceStatusHistoryGridControl);
        // ----------
        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
        jQuery($form.find('.salesgrid .valtype')).attr('data-validationname', 'SalesInventoryValidation');
        jQuery($form.find('.laborgrid .valtype')).attr('data-validationname', 'LaborRateValidation');
        jQuery($form.find('.miscgrid .valtype')).attr('data-validationname', 'MiscRateValidation');
        jQuery($form.find('.rentalsalegrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
    };
    //----------------------------------------------------------------------------------------------
    loadAudit($form: JQuery): void {
        let uniqueid = FwFormField.getValueByDataField($form, 'InvoiceId');
        FwModule.loadAudit($form, uniqueid);
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery): void {
        const STATUS = FwFormField.getValueByDataField($form, 'Status');

        if (STATUS === 'CLOSED' || STATUS === 'PROCESSED' || STATUS === 'VOID') {
            FwModule.setFormReadOnly($form);
        }
        if (!FwFormField.getValueByDataField($form, 'HasRentalItem')) { $form.find('[data-type="tab"][data-caption="Rental"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'HasSalesItem')) { $form.find('[data-type="tab"][data-caption="Sales"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'HasMiscellaneousItem')) { $form.find('[data-type="tab"][data-caption="Misc"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'HasLaborItem')) { $form.find('[data-type="tab"][data-caption="Labor"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'HasFacilityItem')) { $form.find('[data-type="tab"][data-caption="Facilities"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'HasMeterItem')) { $form.find('[data-type="tab"][data-caption="Meter"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'HasTransportationItem')) { $form.find('[data-type="tab"][data-caption="Transportation"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'HasRentalSaleItem')) { $form.find('[data-type="tab"][data-caption="Rental Sale"]').hide() }

        let $invoiceItemGridRental = $form.find('.rentalgrid [data-name="InvoiceItemGrid"]');
        let $invoiceItemGridSales = $form.find('.salesgrid [data-name="InvoiceItemGrid"]');
        let $invoiceItemGridLabor = $form.find('.laborgrid [data-name="InvoiceItemGrid"]');
        let $invoiceItemGridMisc = $form.find('.miscgrid [data-name="InvoiceItemGrid"]');
        let $invoiceItemGridRentalSale = $form.find('.rentalsalegrid [data-name="InvoiceItemGrid"]');

        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"]', e => {
            let tabname = jQuery(e.currentTarget).attr('id');
            let lastIndexOfTab = tabname.lastIndexOf('tab');
            let tabpage = tabname.substring(0, lastIndexOfTab) + 'tabpage' + tabname.substring(lastIndexOfTab + 3);

            let $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
            if ($gridControls.length > 0) {
                for (let i = 0; i < $gridControls.length; i++) {
                    let $gridcontrol = jQuery($gridControls[i]);
                    FwBrowse.search($gridcontrol);
                }
            }

            let $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
            if ($browseControls.length > 0) {
                for (let i = 0; i < $browseControls.length; i++) {
                    let $browseControl = jQuery($browseControls[i]);
                    FwBrowse.search($browseControl);
                }
            }
        });

        // Hides DELETE grid menu item
        $invoiceItemGridRental.find('.submenu-btn').filter('[data-securityid="27053421-85CC-46F4-ADB3-85CEC8A8090B"]').hide();
        $invoiceItemGridSales.find('.submenu-btn').filter('[data-securityid="27053421-85CC-46F4-ADB3-85CEC8A8090B"]').hide();
        $invoiceItemGridLabor.find('.submenu-btn').filter('[data-securityid="27053421-85CC-46F4-ADB3-85CEC8A8090B"]').hide();
        $invoiceItemGridRentalSale.find('.submenu-btn').filter('[data-securityid="27053421-85CC-46F4-ADB3-85CEC8A8090B"]').hide();
        // Hides row DELETE button
        $invoiceItemGridRental.find('.browsecontextmenucell').hide();
        $invoiceItemGridSales.find('.browsecontextmenucell').hide();
        $invoiceItemGridLabor.find('.browsecontextmenucell').hide();
        $invoiceItemGridRentalSale.find('.browsecontextmenucell').hide();
        // Hides ADD button
        $invoiceItemGridRental.find('.buttonbar').hide();
        $invoiceItemGridSales.find('.buttonbar').hide();
        $invoiceItemGridLabor.find('.buttonbar').hide();
        $invoiceItemGridRentalSale.find('.buttonbar').hide();

        this.dynamicColumns($form);
    };
    //----------------------------------------------------------------------------------------------
    activityCheckboxEvents($form, mode) {
        const rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]')
            , salesTab = $form.find('[data-type="tab"][data-caption="Sales"]')
            , partsTab = $form.find('[data-type="tab"][data-caption="Parts"]')
            , miscTab = $form.find('[data-type="tab"][data-caption="Misc"]')
            , laborTab = $form.find('[data-type="tab"][data-caption="Labor"]')
            , rentalSaleTab = $form.find('[data-type="tab"][data-caption="Rental Sale"]');

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
        $form.find('[data-datafield="Miscellaneous"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? miscTab.show() : miscTab.hide();
        });
        $form.find('[data-datafield="Labor"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? laborTab.show() : laborTab.hide();
        });
        $form.find('[data-datafield="RentalSale"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? rentalSaleTab.show() : rentalSaleTab.hide();
        });
    };
    //----------------------------------------------------------------------------------------------
    dynamicColumns($form: JQuery): void {
        let $rentalGrid = $form.find('.rentalgrid [data-name="InvoiceItemGrid"]'),
            $salesGrid = $form.find('.salesgrid [data-name="InvoiceItemGrid"]'),
            $laborGrid = $form.find('.laborgrid [data-name="InvoiceItemGrid"]'),
            $miscGrid = $form.find('.miscgrid [data-name="InvoiceItemGrid"]'),
            $rentalSaleGrid = $form.find('.rentalsalegrid [data-name="InvoiceItemGrid"]'),
            fields = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field'),
            fieldNames = [],
            rentalShowFields: Array<string> = ["OrderNumber", "ICode", "Description", "Quantity", "FromDate", "ToDate", "Days", "Rate", "Cost", "DaysPerWeek", "DiscountPercent", "DiscountAmount", "Extended", "Taxable"],
            salesShowFields: Array<string> = ["OrderNumber", "ICode", "Description", "Quantity", "Unit", "Cost", "Rate", "DiscountPercent", "DiscountAmount", "Extended", "Taxable"],
            laborShowFields: Array<string> = ["OrderNumber", "ICode", "Description", "Quantity", "FromDate", "FromTime", "ToDate", "ToTime", "Days", "Unit", "Rate", "Cost", "DiscountAmount", "Extended", "Taxable"],
            miscShowFields: Array<string> = ["OrderNumber", "ICode", "Description", "Quantity", "FromDate", "ToDate", "Unit", "Days", "Rate", "Cost", "DiscountPercent", "DiscountAmount", "Extended", "Taxable"],
            rentalSaleShowFields: Array<string> = ["OrderNumber", "SerialNumber", "BarCode", "ICode", "Description", "Quantity", "Cost", "Unit", "Rate", "DiscountAmount", "Extended", "Taxable"];

        for (let i = 3; i < fields.length; i++) {
            let name = jQuery(fields[i]).attr('data-mappedfield');
            if (name != "Quantity") {
                fieldNames.push(name);
            }
        }
        let hiddenRentals: Array<string> = fieldNames.filter(function (field) {
            return !this.has(field)
        }, new Set(rentalShowFields))
        let hiddenSales = fieldNames.filter(function (field) {
            return !this.has(field)
        }, new Set(salesShowFields))
        let hiddenLabor = fieldNames.filter(function (field) {
            return !this.has(field)
        }, new Set(laborShowFields))
        let hiddenMisc = fieldNames.filter(function (field) {
            return !this.has(field)
        }, new Set(miscShowFields))
        let hiddenRentalSale = fieldNames.filter(function (field) {
            return !this.has(field)
        }, new Set(rentalSaleShowFields))

        for (let i = 0; i < hiddenRentals.length; i++) {
            jQuery($rentalGrid.find(`[data-mappedfield="${hiddenRentals[i]}"]`)).parent().hide();
        }
        for (let i = 0; i < hiddenSales.length; i++) {
            jQuery($salesGrid.find(`[data-mappedfield="${hiddenSales[i]}"]`)).parent().hide();
        }
        for (let i = 0; i < hiddenLabor.length; i++) {
            jQuery($laborGrid.find(`[data-mappedfield="${hiddenLabor[i]}"]`)).parent().hide();
        }
        for (let i = 0; i < hiddenMisc.length; i++) {
            jQuery($miscGrid.find(`[data-mappedfield="${hiddenMisc[i]}"]`)).parent().hide();
        }
        for (let i = 0; i < hiddenRentalSale.length; i++) {
            jQuery($rentalSaleGrid.find(`[data-mappedfield="${hiddenRentalSale[i]}"]`)).parent().hide();
        }
    };
    //----------------------------------------------------------------------------------------------
    calculateInvoiceItemGridTotals($form: JQuery, gridType: string): void {
        let subTotal, discount, salesTax, grossTotal, total, rateType;
        let extendedTotal = new Decimal(0);
        let discountTotal = new Decimal(0);
        let taxTotal = new Decimal(0);

        const extendedColumn: any = $form.find(`.${gridType}grid [data-browsedatafield="Extended"]`);
        const discountColumn: any = $form.find(`.${gridType}grid [data-browsedatafield="DiscountAmount"]`);
        const taxColumn: any = $form.find(`.${gridType}grid [data-browsedatafield="Tax"]`);

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

        $form.find('.' + gridType + '-totals [data-totalfield="SubTotal"] input').val(subTotal);
        $form.find('.' + gridType + '-totals [data-totalfield="Discount"] input').val(discount);
        $form.find('.' + gridType + '-totals [data-totalfield="Tax"] input').val(salesTax);
        $form.find('.' + gridType + '-totals [data-totalfield="GrossTotal"] input').val(grossTotal);
        $form.find('.' + gridType + '-totals [data-totalfield="Total"] input').val(total);
    };
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        //Populate tax info fields with validation
        $form.find('div[data-datafield="TaxOptionId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="RentalTaxRate1"]', $tr.find('.field[data-browsedatafield="RentalTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="SalesTaxRate1"]', $tr.find('.field[data-browsedatafield="SalesTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="LaborTaxRate1"]', $tr.find('.field[data-browsedatafield="LaborTaxRate1"]').attr('data-originalvalue'));
        });
        // Billing Date Validation
        $form.find('.billing-date-validation').on('changeDate', event => {
            this.checkBillingDateRange($form, event);
        });
        //Open Print Invoice Report
        $form.find('.print-invoice').on('click', e => {
            let $report, invoiceNumber, invoiceId, recordTitle, printTab, module, hideModule;
            module = this.Module;
            try {
                invoiceNumber = $form.find(`div.fwformfield[data-datafield="${module}Number"] input`).val();
                invoiceId = $form.find(`div.fwformfield[data-datafield="${module}Id"] input`).val();
                recordTitle = jQuery('.tabs .active[data-tabtype="FORM"] .caption').text();
                $report = RwInvoiceReportController.openForm();

                FwModule.openSubModuleTab($form, $report);

                $report.find(`div.fwformfield[data-datafield="${module}Id"] input`).val(invoiceId);
                $report.find(`div.fwformfield[data-datafield="${module}Id"] .fwformfield-text`).val(invoiceNumber);
                jQuery('.tab.submodule.active').find('.caption').html(`Print ${module}`);

                printTab = jQuery('.tab.submodule.active');
                printTab.find('.caption').html(`Print ${module}`);
                printTab.attr('data-caption', `${module} ${recordTitle}`);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });

        //Populate tax info fields with validation
        $form.find('div[data-datafield="TaxOptionId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="RentalTaxRate1"]', $tr.find('.field[data-browsedatafield="RentalTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="SalesTaxRate1"]', $tr.find('.field[data-browsedatafield="SalesTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="LaborTaxRate1"]', $tr.find('.field[data-browsedatafield="LaborTaxRate1"]').attr('data-originalvalue'));
        });
    };
    //----------------------------------------------------------------------------------------------
    checkBillingDateRange($form: JQuery, event: any): void {
        let parsedFromDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingStartDate'));
        let parsedToDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingEndDate'));

        if (parsedToDate < parsedFromDate) {
            $form.find('div[data-datafield="BillingEndDate"]').addClass('error');
            FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'From Date'.");
        } else {
            $form.find('div[data-datafield="BillingEndDate"]').removeClass('error');
        }
    };
    //----------------------------------------------------------------------------------------------
    voidInvoice($form: JQuery): void {
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
            let $confirmation, $yes, $no;
            $confirmation = FwConfirmation.renderConfirmation('Void', '');
            $confirmation.find('.fwconfirmationbox').css('width', '450px');
            let html = [];
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div>Void this Invoice?</div>');
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
//-----------------------------------------------------------------------------------------------------
//Print Invoice menu item
FwApplicationTree.clickEvents['{3A693D4E-3B9B-4749-A9B6-C8302F1EDE6A}'] = function (e) {
    try {
        var $form = jQuery(this).closest('.fwform');
        $form.find('.print-invoice').trigger('click');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
//form approve
FwApplicationTree.clickEvents['{117CCDFA-FFC3-49CE-B41B-0F6CE9A69518}'] = function (event) {
    var $form, invoiceId;
    $form = jQuery(this).closest('.fwform');
    invoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
    FwAppData.apiMethod(true, 'POST', `api/v1/invoice/toggleapproved/${invoiceId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
        if (response.success === true) {
            FwModule.refreshForm($form, InvoiceController);
        } else {
            FwNotification.renderNotification('WARNING', response.msg);
        }
    }, null, $form);
};
//----------------------------------------------------------------------------------------------
//form unapprove
FwApplicationTree.clickEvents['{F8C5F06C-4B9D-4495-B589-B44B02AE7915}'] = function (event) {
    var $form, invoiceId;
    $form = jQuery(this).closest('.fwform');
    invoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
    FwAppData.apiMethod(true, 'POST', `api/v1/invoice/toggleapproved/${invoiceId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
        if (response.success === true) {
            FwModule.refreshForm($form, InvoiceController);
        } else {
            FwNotification.renderNotification('WARNING', response.msg);
        }
    }, null, $form);
};
//----------------------------------------------------------------------------------------------
//browse approve
FwApplicationTree.clickEvents['{9D1A3607-EE4A-49E6-8EAE-DB3E0FF06EAE}'] = function (event) {
    let $browse;
    let invoiceId;
    $browse = jQuery(this).closest('.fwbrowse');
    try {
        invoiceId = $browse.find('.selected [data-browsedatafield="InvoiceId"]').attr('data-originalvalue');
        if (typeof invoiceId !== 'undefined') {
            FwAppData.apiMethod(true, 'POST', `api/v1/invoice/toggleapproved/${invoiceId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success === true) {
                    FwBrowse.search($browse);
                } else {
                    FwNotification.renderNotification('WARNING', response.msg);
                }
            }, null, $browse);
        } else {
            FwNotification.renderNotification('WARNING', 'No Invoice Selected');
        }
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
//browse unapprove
FwApplicationTree.clickEvents['{F9D43CB6-2666-4AE0-B35C-77735561B9B9}'] = function (event) {
    let $browse;
    let invoiceId;
    $browse = jQuery(this).closest('.fwbrowse');
    try {
        invoiceId = $browse.find('.selected [data-browsedatafield="InvoiceId"]').attr('data-originalvalue');
        if (typeof invoiceId !== 'undefined') {
            FwAppData.apiMethod(true, 'POST', `api/v1/invoice/toggleapproved/${invoiceId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success === true) {
                    FwBrowse.search($browse);
                } else {
                    FwNotification.renderNotification('WARNING', response.msg);
                }
            }, null, $browse);
        } else {
            FwNotification.renderNotification('WARNING', 'No Invoice Selected');
        }
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------

var InvoiceController = new Invoice();