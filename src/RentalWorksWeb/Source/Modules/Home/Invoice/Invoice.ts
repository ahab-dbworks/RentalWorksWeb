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

        var location = JSON.parse(sessionStorage.getItem('location'));
        self.ActiveView = 'LocationId=' + location.locationid;

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

        this.events($form);
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
        const maxPageSize = 9999;

        let $invoiceItemGridRental;
        let $invoiceItemGridRentalControl;
        $invoiceItemGridRental = $form.find('.rentalgrid div[data-grid="InvoiceItemGrid"]');
        $invoiceItemGridRentalControl = jQuery(jQuery('#tmpl-grids-InvoiceItemGridBrowse').html());
        $invoiceItemGridRental.empty().append($invoiceItemGridRentalControl);
        $invoiceItemGridRentalControl.data('isSummary', false);
        $invoiceItemGridRental.addClass('R');
        $invoiceItemGridRental.attr('data-formreadonly', 'true');

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
        //----------------------------------------------------------------------------------------------
        let $invoiceItemGridSales;
        let $invoiceItemGridSalesControl;
        $invoiceItemGridSales = $form.find('.salesgrid div[data-grid="InvoiceItemGrid"]');
        $invoiceItemGridSalesControl = jQuery(jQuery('#tmpl-grids-InvoiceItemGridBrowse').html());
        $invoiceItemGridSales.empty().append($invoiceItemGridSalesControl);
        $invoiceItemGridSales.addClass('S');
        $invoiceItemGridSales.attr('data-formreadonly', 'true');
        $invoiceItemGridSalesControl.data('isSummary', false);

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
        //----------------------------------------------------------------------------------------------
        let $invoiceItemGridLabor;
        let $invoiceItemGridLaborControl;
        $invoiceItemGridLabor = $form.find('.laborgrid div[data-grid="InvoiceItemGrid"]');
        $invoiceItemGridLaborControl = jQuery(jQuery('#tmpl-grids-InvoiceItemGridBrowse').html());
        $invoiceItemGridLabor.empty().append($invoiceItemGridLaborControl);
        $invoiceItemGridLabor.addClass('L');
        $invoiceItemGridLabor.find('.Extended').attr('data-formreadonly', 'true');
        $invoiceItemGridLabor.find('.ICode').attr('data-formreadonly', 'true');
        $invoiceItemGridLabor.find('.OrderNumber').attr('data-formreadonly', 'true'); 
        $invoiceItemGridLabor.find('.Taxable').attr('data-formreadonly', 'true');

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
        //----------------------------------------------------------------------------------------------
        let $invoiceItemGridMisc;
        let $invoiceItemGridMiscControl;
        $invoiceItemGridMisc = $form.find('.miscgrid div[data-grid="InvoiceItemGrid"]');
        $invoiceItemGridMiscControl = jQuery(jQuery('#tmpl-grids-InvoiceItemGridBrowse').html());
        $invoiceItemGridMisc.empty().append($invoiceItemGridMiscControl);
        $invoiceItemGridMisc.addClass('M');
        $invoiceItemGridMisc.find('.Extended').attr('data-formreadonly', 'true')
        $invoiceItemGridMiscControl.data('isSummary', false);

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
        //----------------------------------------------------------------------------------------------
        var $invoiceItemGridRentalSale;
        var $invoiceItemGridRentalSaleControl;
        $invoiceItemGridRentalSale = $form.find('.rentalsalegrid div[data-grid="InvoiceItemGrid"]');
        $invoiceItemGridRentalSaleControl = jQuery(jQuery('#tmpl-grids-InvoiceItemGridBrowse').html());
        $invoiceItemGridRentalSale.empty().append($invoiceItemGridRentalSaleControl);
        $invoiceItemGridRentalSale.addClass('RS');
        $invoiceItemGridRentalSale.attr('data-formreadonly', 'true');
        $invoiceItemGridRentalSaleControl.attr('data-formreadonly', 'true');
        $invoiceItemGridRentalSaleControl.data('isSummary', false);

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

        //----------------------------------------------------------------------------------------------
        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
        jQuery($form.find('.salesgrid .valtype')).attr('data-validationname', 'SalesInventoryValidation');
        jQuery($form.find('.laborgrid .valtype')).attr('data-validationname', 'LaborRateValidation');
        jQuery($form.find('.miscgrid .valtype')).attr('data-validationname', 'MiscRateValidation');
        jQuery($form.find('.rentalsalegrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
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
        //if (!FwFormField.getValueByDataField($form, 'RentalSale')) { $form.find('[data-type="tab"][data-caption="Rental Sales"]').hide() }


        let $invoiceItemGridRental;
        $invoiceItemGridRental = $form.find('.rentalgrid [data-name="InvoiceItemGrid"]');
        //FwBrowse.search($invoiceItemGridRental);
        let $invoiceItemGridSales;
        $invoiceItemGridSales = $form.find('.salesgrid [data-name="InvoiceItemGrid"]');
        //FwBrowse.search($invoiceItemGridSales);
        let $invoiceItemGridLabor;
        $invoiceItemGridLabor = $form.find('.laborgrid [data-name="InvoiceItemGrid"]');
        //FwBrowse.search($invoiceItemGridLabor);
        let $invoiceItemGridMisc;
        $invoiceItemGridMisc = $form.find('.miscgrid [data-name="InvoiceItemGrid"]');
        //FwBrowse.search($invoiceItemGridMisc);
        let $invoiceItemGridRentalSale;
        $invoiceItemGridRentalSale = $form.find('.rentalsalegrid [data-name="InvoiceItemGrid"]');
        ////FwBrowse.search($invoiceItemGridRentalSale);


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

        //$invoiceItemGridRental.find('.submenu-btn').filter('[data-securityid="5A3352C6-F1D5-4A8C-BD75-045AF7B9988F"]').hide();
        //$invoiceItemGridSales.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        //$invoiceItemGridPart.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        //$invoiceItemGridLabor.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        //$invoiceItemGridMisc.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        //$invoiceItemGridSubRent.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        //$invoiceItemGridSubSales.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        //$invoiceItemGridSubLabor.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        //$invoiceItemGridSubMisc.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();

        //$invoiceItemGridSubRent.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();
        //$invoiceItemGridSubSales.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();
        //$invoiceItemGridSubLabor.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();
        //$invoiceItemGridSubMisc.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();

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
            , rentalSaleTab = $form.find('[data-type="tab"][data-caption="Rental Sales"]')

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
    dynamicColumns($form: any): void {
        let $rentalGrid = $form.find('.rentalgrid [data-name="InvoiceItemGrid"]'),
            $salesGrid = $form.find('.salesgrid [data-name="InvoiceItemGrid"]'),
            $laborGrid = $form.find('.laborgrid [data-name="InvoiceItemGrid"]'),
            $miscGrid = $form.find('.miscgrid [data-name="InvoiceItemGrid"]'),
            $rentalSaleGrid = $form.find('.rentalsalegrid [data-name="InvoiceItemGrid"]'),
            fields = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field'),
            fieldNames = [],
            rentalShowFields: Array<string> = ["OrderNumber", "ICode", "Description", "Quantity", "FromDate", "ToDate", "Days", "Price", "DaysPerWeek", "DiscountAmount", "Extended", "Taxable"],
            salesShowFields: Array<string> = ["OrderNumber", "ICode", "Description", "Quantity", "Unit", "Price", "DiscountPercent", "DiscountAmount", "Extended", "Taxable"],
            laborShowFields: Array<string> = ["OrderNumber", "ICode", "Description", "Quantity", "FromDate", "FromTime", "ToDate", "ToTime", "Days", "Unit", "Rate", "Price", "DiscountAmount", "Extended", "Taxable"],
            miscShowFields: Array<string> = ["OrderNumber", "ICode", "Description", "Quantity", "FromDate", "ToDate", "Unit", "Days", "Rate", "Price", "DiscountPercent", "DiscountAmount","Extended", "Taxable"],
            rentalSaleShowFields: Array<string> = ["OrderNumber", "BarCode", "SerialNumber", "ICode", "Description", "Quantity", "Cost", "Unit", "Rate", "DiscountAmount", "Extended", "Taxable"];

        for (let i = 3; i < fields.length; i++) {
            let name = jQuery(fields[i]).attr('data-mappedfield');
            if (name != "Quantity") {
                fieldNames.push(name);
            }
        }
        console.log('ALLfieldNames', fieldNames)
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
    calculateInvoiceItemGridTotals($form: any, gridType: string): void {
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
    events($form: any) {
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
    };
    //----------------------------------------------------------------------------------------------
    afterSave($form) { };
    //----------------------------------------------------------------------------------------------
    checkBillingDateRange($form: any, event: any): void {
        let parsedFromDate, parsedToDate;

        parsedFromDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingStartDate'));
        parsedToDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingEndDate'));

        if (parsedToDate < parsedFromDate) {
            $form.find('div[data-datafield="BillingEndDate"]').addClass('error');
            FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'From Date'.");
        } else {
            $form.find('div[data-datafield="BillingEndDate"]').removeClass('error');
        }
    };
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
//----------------------------------------------------------------------------------------------
var InvoiceController = new Invoice();