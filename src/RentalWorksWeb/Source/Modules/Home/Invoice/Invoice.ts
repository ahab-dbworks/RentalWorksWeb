class Invoice {
    Module: string = 'Invoice';
    apiurl: string = 'api/v1/invoice';
    caption: string = Constants.Modules.Home.Invoice.caption;
    nav: string = Constants.Modules.Home.Invoice.nav; $
    id: string = Constants.Modules.Home.Invoice.id;
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

        const chartFilters = JSON.parse(sessionStorage.getItem('chartfilter'));
        if (!chartFilters) {
            $browse.data('ondatabind', request => {
                request.activeviewfields = this.ActiveViewFields;
            });
        } else {
            $browse.data('ondatabind', request => {
                request.activeviewfields = '';
            });
        }

        // Changes text color to light gray if void
        FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
            if (dt.Rows[rowIndex][dt.ColumnIndex['Status']] === 'VOID') {
                $tr.css('color', '#aaaaaa');
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
    addBrowseMenuItems($menuObject: any) {
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $new = FwMenu.generateDropDownViewBtn('New', false, "NEW");
        const $approved = FwMenu.generateDropDownViewBtn('Approved', false, "APPROVED");
        const $newapproved = FwMenu.generateDropDownViewBtn('New & Approved', false, "NEWAPPROVED");
        const $processed = FwMenu.generateDropDownViewBtn('Processed', false, "PROCESSED");
        const $closed = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");
        const $void = FwMenu.generateDropDownViewBtn('Void', false, "VOID");
        const $all = FwMenu.generateDropDownViewBtn('All', true, "ALL");

        const viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $new, $approved, $newapproved, $processed, $closed, $void);
        FwMenu.addViewBtn($menuObject, 'View', viewSubitems, true, "Status");

        //Location Filter
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        const viewLocation = [];
        viewLocation.push($allLocations, $userLocation);
        FwMenu.addViewBtn($menuObject, 'Location', viewLocation, true, "LocationId");
        return $menuObject;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        FwFormField.disable($form.find('[data-datafield="SubRent"]'));
        FwFormField.disable($form.find('[data-datafield="SubSale"]'));
        FwFormField.disable($form.find('[data-datafield="SubLabor"]'));
        FwFormField.disable($form.find('[data-datafield="SubMiscellaneous"]'));
        FwFormField.disable($form.find('[data-datafield="SubVehicle"]'));

        const $emailHistorySubModuleBrowse = this.openEmailHistoryBrowse($form);
        $form.find('.emailhistory-page').append($emailHistorySubModuleBrowse);

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');

            const usersid = sessionStorage.getItem('usersid');  // J. Pace 7/09/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
            const name = sessionStorage.getItem('name');
            FwFormField.setValue($form, 'div[data-datafield="ProjectManagerId"]', usersid, name);
            FwFormField.setValue($form, 'div[data-datafield="AgentId"]', usersid, name);

            const today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'BillingStartDate', today);
            FwFormField.setValueByDataField($form, 'BillingEndDate', today);
            FwFormField.setValueByDataField($form, 'InvoiceDate', today);
            FwFormField.enable($form.find('[data-datafield="StatusDate"]'));
            FwFormField.enable($form.find('[data-datafield="RateType"]'));
            FwFormField.setValueByDataField($form, 'StatusDate', today);
            const department = JSON.parse(sessionStorage.getItem('department'));
            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);

            const office = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);

            FwFormField.setValueByDataField($form, 'InvoiceType', 'BILLING');
            FwFormField.setValueByDataField($form, 'Status', 'NEW');

            // hide tabs
            $form.find('.hide-new').hide();
        } else {
            FwFormField.disable($form.find('.ifnew'));
        }

        this.events($form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="InvoiceId"] input').val(uniqueids.InvoiceId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: JQuery, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
        const invoiceItemTotalFields = ["LineTotalWithTax", "Tax", "LineTotal", "LineTotalBeforeDiscount", "DiscountAmount"];
        //                               Total               Tax   SubTotal      GrossTotal                 Discount
        // ----------
        const $invoiceItemGridRental = $form.find('.rentalgrid div[data-grid="InvoiceItemGrid"]');
        const $invoiceItemGridRentalControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
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
            request.totalfields = invoiceItemTotalFields;
        });
        $invoiceItemGridRentalControl.data('beforesave', request => {
            request.InvoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
            request.RecType = 'R';
        });

        FwBrowse.addEventHandler($invoiceItemGridRentalControl, 'afterdatabindcallback', ($invoiceItemGridRentalControl, dt) => {
            this.calculateInvoiceItemGridTotals($form, 'rental', dt.Totals);
        });
        FwBrowse.init($invoiceItemGridRentalControl);
        FwBrowse.renderRuntimeHtml($invoiceItemGridRentalControl);
        // ----------
        const $invoiceItemGridSales = $form.find('.salesgrid div[data-grid="InvoiceItemGrid"]');
        const $invoiceItemGridSalesControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
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
            request.totalfields = invoiceItemTotalFields;
        });
        $invoiceItemGridSalesControl.data('beforesave', request => {
            request.InvoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
            request.RecType = 'S';
        });
        FwBrowse.addEventHandler($invoiceItemGridSalesControl, 'afterdatabindcallback', ($invoiceItemGridSalesControl, dt) => {
            this.calculateInvoiceItemGridTotals($form, 'sales', dt.Totals);
        });
        FwBrowse.init($invoiceItemGridSalesControl);
        FwBrowse.renderRuntimeHtml($invoiceItemGridSalesControl);
        // ----------
        const $invoiceItemGridLabor = $form.find('.laborgrid div[data-grid="InvoiceItemGrid"]');
        const $invoiceItemGridLaborControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
        $invoiceItemGridLabor.empty().append($invoiceItemGridLaborControl);
        $invoiceItemGridLabor.addClass('L');
        $invoiceItemGridLabor.find('div[data-datafield="Extended"]').attr('data-formreadonly', 'true');
        $invoiceItemGridLabor.find('div[data-datafield="InventoryId"]').attr('data-formreadonly', 'true');
        $invoiceItemGridLabor.find('div[data-datafield="OrderNumber"]').attr('data-formreadonly', 'true');
        $invoiceItemGridLabor.find('div[data-datafield="Taxable"]').attr('data-formreadonly', 'true');
        $invoiceItemGridLaborControl.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Rate');
        $invoiceItemGridLaborControl.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');

        $invoiceItemGridLaborControl.data('isSummary', false);

        $invoiceItemGridLaborControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId'),
                RecType: 'L'
            };
            request.totalfields = invoiceItemTotalFields;
        });
        $invoiceItemGridLaborControl.data('beforesave', request => {
            request.InvoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
            request.RecType = 'L';
        });
        FwBrowse.addEventHandler($invoiceItemGridLaborControl, 'afterdatabindcallback', ($invoiceItemGridLaborControl, dt) => {
            this.calculateInvoiceItemGridTotals($form, 'labor', dt.Totals);
        });
        FwBrowse.init($invoiceItemGridLaborControl);
        FwBrowse.renderRuntimeHtml($invoiceItemGridLaborControl);
        // ----------
        const $invoiceItemGridMisc = $form.find('.miscgrid div[data-grid="InvoiceItemGrid"]');
        const $invoiceItemGridMiscControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
        $invoiceItemGridMisc.empty().append($invoiceItemGridMiscControl);
        $invoiceItemGridMisc.addClass('M');
        $invoiceItemGridMisc.find('div[data-datafield="Extended"]').attr('data-formreadonly', 'true')
        $invoiceItemGridMiscControl.data('isSummary', false);
        $invoiceItemGridMiscControl.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Rate');
        $invoiceItemGridMiscControl.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');

        $invoiceItemGridMiscControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId'),
                RecType: 'M'
            };
            request.totalfields = invoiceItemTotalFields;
        });
        $invoiceItemGridMiscControl.data('beforesave', request => {
            request.InvoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
            request.RecType = 'M';
        });
        FwBrowse.addEventHandler($invoiceItemGridMiscControl, 'afterdatabindcallback', ($invoiceItemGridMiscControl, dt) => {
            this.calculateInvoiceItemGridTotals($form, 'misc', dt.Totals);
        });
        FwBrowse.init($invoiceItemGridMiscControl);
        FwBrowse.renderRuntimeHtml($invoiceItemGridMiscControl);
        // ----------
        const $invoiceItemGridRentalSale = $form.find('.rentalsalegrid div[data-grid="InvoiceItemGrid"]');
        const $invoiceItemGridRentalSaleControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
        $invoiceItemGridRentalSale.empty().append($invoiceItemGridRentalSaleControl);
        $invoiceItemGridRentalSale.addClass('RS');
        $invoiceItemGridRentalSaleControl.attr('data-enabled', 'false');
        $invoiceItemGridRentalSaleControl.data('isSummary', false);
        $invoiceItemGridRentalSaleControl.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Price');

        $invoiceItemGridRentalSaleControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId'),
                RecType: 'RS'
            };
            request.totalfields = invoiceItemTotalFields;
        });
        $invoiceItemGridRentalSaleControl.data('beforesave', request => {
            request.InvoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
            request.RecType = 'RS';
        });
        FwBrowse.addEventHandler($invoiceItemGridRentalSaleControl, 'afterdatabindcallback', ($invoiceItemGridRentalSaleControl, dt) => {
            this.calculateInvoiceItemGridTotals($form, 'rentalsale', dt.Totals);
        });
        FwBrowse.init($invoiceItemGridRentalSaleControl);
        FwBrowse.renderRuntimeHtml($invoiceItemGridRentalSaleControl);
        // ----------
        const $invoiceNoteGrid = $form.find('div[data-grid="InvoiceNoteGrid"]');
        const $invoiceNoteGridControl = FwBrowse.loadGridFromTemplate('InvoiceNoteGrid');
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
        const glTotalFields = ["Debit", "Credit"];
        const $glDistributionGrid = $form.find('div[data-grid="GlDistributionGrid"]');
        const $glDistributionGridControl = FwBrowse.loadGridFromTemplate('GlDistributionGrid');
        $glDistributionGrid.empty().append($glDistributionGridControl);
        $glDistributionGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId')
            };
            request.totalfields = glTotalFields;
        });
        FwBrowse.addEventHandler($glDistributionGridControl, 'afterdatabindcallback', ($glDistributionGridControl, dt) => {
            FwFormField.setValue2($form.find('.gldistribution-totals [data-totalfield="Debit"]'), dt.Totals.Debit);
            FwFormField.setValue2($form.find('.gldistribution-totals [data-totalfield="Credit"]'), dt.Totals.Credit);
        });
        FwBrowse.init($glDistributionGridControl);
        FwBrowse.renderRuntimeHtml($glDistributionGridControl);
        // ----------
        const $manualGlGrid = $form.find('div[data-grid="ManualGlTransactionsGrid"]');
        const $manualGlGridControl = FwBrowse.loadGridFromTemplate('ManualGlTransactionsGrid');
        $manualGlGrid.empty().append($manualGlGridControl);
        $manualGlGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId')
            };
            request.totalfields = ["Amount"];
        });
        FwBrowse.addEventHandler($manualGlGridControl, 'afterdatabindcallback', ($manualGlGridControl, dt) => {
            FwFormField.setValue2($form.find('.manualgl-totals [data-totalfield="Amount"]'), dt.Totals.Amount);
        });
        $manualGlGridControl.data('beforesave', request => {
            request.InvoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
        });
        FwBrowse.init($manualGlGridControl);
        FwBrowse.renderRuntimeHtml($manualGlGridControl);
        // ----------
        const $invoiceOrderGrid = $form.find('div[data-grid="InvoiceOrderGrid"]');
        const $invoiceOrderGridControl = FwBrowse.loadGridFromTemplate('InvoiceOrderGrid');
        $invoiceOrderGrid.empty().append($invoiceOrderGridControl);
        $invoiceOrderGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId')
            };
        });
        FwBrowse.init($invoiceOrderGridControl);
        FwBrowse.renderRuntimeHtml($invoiceOrderGridControl);
        // ----------
        const $invoiceRevenueGrid = $form.find('div[data-grid="InvoiceRevenueGrid"]');
        const $invoiceRevenueGridControl = FwBrowse.loadGridFromTemplate('InvoiceRevenueGrid');
        $invoiceRevenueGrid.empty().append($invoiceRevenueGridControl);
        $invoiceRevenueGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId')
            };
        });
        FwBrowse.init($invoiceRevenueGridControl);
        FwBrowse.renderRuntimeHtml($invoiceRevenueGridControl);
        // ----------
        const $invoiceReceiptGrid = $form.find('div[data-grid="InvoiceReceiptGrid"]');
        const $invoiceReceiptGridControl = FwBrowse.loadGridFromTemplate('InvoiceReceiptGrid');
        $invoiceReceiptGrid.empty().append($invoiceReceiptGridControl);
        $invoiceReceiptGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId')
            };
        });
        FwBrowse.init($invoiceReceiptGridControl);
        FwBrowse.renderRuntimeHtml($invoiceReceiptGridControl);
        // ----------
        const $invoiceStatusHistoryGrid = $form.find('div[data-grid="InvoiceStatusHistoryGrid"]');
        const $invoiceStatusHistoryGridControl = FwBrowse.loadGridFromTemplate('InvoiceStatusHistoryGrid');
        $invoiceStatusHistoryGrid.empty().append($invoiceStatusHistoryGridControl);
        $invoiceStatusHistoryGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId')
            };
        });
        FwBrowse.init($invoiceStatusHistoryGridControl);
        FwBrowse.renderRuntimeHtml($invoiceStatusHistoryGridControl);
        // Invoice Item Adjustment Grids
        // ----------
        const itemPageSize = 3;
        const itemAdjustmentTotalFields = ["LineTotalWithTax", "Tax", "LineTotal"];
        const $invoiceItemGridAdjustmentRental = $form.find('.rentaladjustment div[data-grid="InvoiceItemGrid"]');
        const $invoiceItemGridAdjustmentRentalControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
        $invoiceItemGridAdjustmentRentalControl.attr('data-pagesize', itemPageSize);
        $invoiceItemGridAdjustmentRental.empty().append($invoiceItemGridAdjustmentRentalControl);
        $invoiceItemGridAdjustmentRental.addClass('R');

        $invoiceItemGridAdjustmentRentalControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId'),
                RecType: 'A',
                AvailFor: 'R',
                pagesize: itemPageSize
            };
            request.totalfields = itemAdjustmentTotalFields;
        });
        $invoiceItemGridAdjustmentRentalControl.data('beforesave', request => {
            request.InvoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
            request.RecType = 'A';
            request.AvailFor = 'R';
            request.pagesize = itemPageSize;
        });
        FwBrowse.addEventHandler($invoiceItemGridAdjustmentRentalControl, 'afterdatabindcallback', ($invoiceItemGridAdjustmentRentalControl, dt) => {
            this.calculateInvoiceItemGridTotals($form, 'rentaladjustment', dt.Totals, true);
        });

        FwBrowse.init($invoiceItemGridAdjustmentRentalControl);
        FwBrowse.renderRuntimeHtml($invoiceItemGridAdjustmentRentalControl);
        // ----------
        const $invoiceItemGridAdjustmentSales = $form.find('.salesadjustment div[data-grid="InvoiceItemGrid"]');
        const $invoiceItemGridAdjustmentSalesControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
        $invoiceItemGridAdjustmentSalesControl.attr('data-pagesize', itemPageSize);

        $invoiceItemGridAdjustmentSales.empty().append($invoiceItemGridAdjustmentSalesControl);
        $invoiceItemGridAdjustmentSales.addClass('S');
        $invoiceItemGridAdjustmentSalesControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId'),
                pagesize: itemPageSize,
                RecType: 'A',
                AvailFor: 'S'
            };
            request.totalfields = itemAdjustmentTotalFields;
        });
        $invoiceItemGridAdjustmentSalesControl.data('beforesave', request => {
            request.InvoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
            request.RecType = 'A';
            request.AvailFor = 'S';
            request.pagesize = itemPageSize;
        });
        FwBrowse.addEventHandler($invoiceItemGridAdjustmentSalesControl, 'afterdatabindcallback', ($invoiceItemGridAdjustmentSalesControl, dt) => {
            this.calculateInvoiceItemGridTotals($form, 'salesadjustment', dt.Totals, true);
        });

        FwBrowse.init($invoiceItemGridAdjustmentSalesControl);
        FwBrowse.renderRuntimeHtml($invoiceItemGridAdjustmentSalesControl);
        // ----------
        const $invoiceItemGridAdjustmentParts = $form.find('.partsadjustment div[data-grid="InvoiceItemGrid"]');
        const $invoiceItemGridAdjustmentPartsControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
        $invoiceItemGridAdjustmentPartsControl.attr('data-pagesize', itemPageSize);

        $invoiceItemGridAdjustmentParts.empty().append($invoiceItemGridAdjustmentPartsControl);
        $invoiceItemGridAdjustmentParts.addClass('P')
        $invoiceItemGridAdjustmentPartsControl.data('ondatabind', request => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'InvoiceId'),
                pagesize: itemPageSize,
                RecType: 'A',
                AvailFor: 'P'
            };
            request.totalfields = itemAdjustmentTotalFields;
        });
        $invoiceItemGridAdjustmentPartsControl.data('beforesave', request => {
            request.InvoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
            request.RecType = 'A';
            request.AvailFor = 'P';
            request.pagesize = itemPageSize;
        });
        FwBrowse.addEventHandler($invoiceItemGridAdjustmentPartsControl, 'afterdatabindcallback', ($invoiceItemGridAdjustmentPartsControl, dt) => {
            this.calculateInvoiceItemGridTotals($form, 'partsadjustment', dt.Totals, true);
        });
        FwBrowse.init($invoiceItemGridAdjustmentPartsControl);
        FwBrowse.renderRuntimeHtml($invoiceItemGridAdjustmentPartsControl);
        // ----------
        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
        jQuery($form.find('.salesgrid .valtype')).attr('data-validationname', 'SalesInventoryValidation');
        jQuery($form.find('.laborgrid .valtype')).attr('data-validationname', 'LaborRateValidation');
        jQuery($form.find('.miscgrid .valtype')).attr('data-validationname', 'MiscRateValidation');
        jQuery($form.find('.rentalsalegrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
    };
    //----------------------------------------------------------------------------------------------
    loadAudit($form: JQuery): void {
        const uniqueid = FwFormField.getValueByDataField($form, 'InvoiceId');
        FwModule.loadAudit($form, uniqueid);
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery): void {
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
        const IsStandAloneInvoice = FwFormField.getValueByDataField($form, 'IsStandAloneInvoice') === true;
        if (IsStandAloneInvoice) {
            FwFormField.enable($form.find('[data-datafield="RateType"]'));
        }
        // Disbles form for certain statuses. Maintain position under 'IsStandAloneInvoice' condition since status overrides
        const status = FwFormField.getValueByDataField($form, 'Status');
        if (status === 'CLOSED' || status === 'PROCESSED' || status === 'VOID') {
            FwModule.setFormReadOnly($form);
        }
        // Hide tab behavior
        if (!FwFormField.getValueByDataField($form, 'HasRentalItem')) { $form.find('[data-type="tab"][data-caption="Rental"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'HasSalesItem')) { $form.find('[data-type="tab"][data-caption="Sales"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'HasLaborItem')) { $form.find('[data-type="tab"][data-caption="Labor"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'HasFacilityItem')) { $form.find('[data-type="tab"][data-caption="Facilities"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'HasMeterItem')) { $form.find('[data-type="tab"][data-caption="Meter"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'HasTransportationItem')) { $form.find('[data-type="tab"][data-caption="Transportation"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'HasRentalSaleItem')) { $form.find('[data-type="tab"][data-caption="Used Sale"]').hide() }

        const $invoiceItemGridRental = $form.find('.rentalgrid [data-name="InvoiceItemGrid"]');
        const $invoiceItemGridSales = $form.find('.salesgrid [data-name="InvoiceItemGrid"]');
        const $invoiceItemGridLabor = $form.find('.laborgrid [data-name="InvoiceItemGrid"]');
        const $invoiceItemGridRentalSale = $form.find('.rentalsalegrid [data-name="InvoiceItemGrid"]');
        // Hides DELETE grid menu item
        $invoiceItemGridRental.find('.submenu-btn').filter('[data-securityid="27053421-85CC-46F4-ADB3-85CEC8A8090B"]').hide();
        $invoiceItemGridSales.find('.submenu-btn').filter('[data-securityid="27053421-85CC-46F4-ADB3-85CEC8A8090B"]').hide();
        $invoiceItemGridLabor.find('.submenu-btn').filter('[data-securityid="27053421-85CC-46F4-ADB3-85CEC8A8090B"]').hide();
        $invoiceItemGridRentalSale.find('.submenu-btn').filter('[data-securityid="27053421-85CC-46F4-ADB3-85CEC8A8090B"]').hide();
        // Hides grid row DELETE button
        $invoiceItemGridRental.find('.browsecontextmenucell').hide();
        $invoiceItemGridSales.find('.browsecontextmenucell').hide();
        $invoiceItemGridLabor.find('.browsecontextmenucell').hide();
        $invoiceItemGridRentalSale.find('.browsecontextmenucell').hide();
        // Hides grid ADD button
        $invoiceItemGridRental.find('.buttonbar').hide();
        $invoiceItemGridSales.find('.buttonbar').hide();
        $invoiceItemGridLabor.find('.buttonbar').hide();
        $invoiceItemGridRentalSale.find('.buttonbar').hide();

        this.dynamicColumns($form);
    };

    //----------------------------------------------------------------------------------------------
    dynamicColumns($form: JQuery): void {
        const $rentalGrid = $form.find('.rentalgrid [data-name="InvoiceItemGrid"]');
        const fields = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field');
        const fieldNames = [];
        for (let i = 3; i < fields.length; i++) {
            let name = jQuery(fields[i]).attr('data-mappedfield');
            if (name != "Quantity") {
                fieldNames.push(name);
            }
        }
        // ----------
        const rentalShowFields: Array<string> = ["OrderNumber", "ICode", "Description", "Quantity", "FromDate", "ToDate", "Days", "Rate", "Cost", "DaysPerWeek", "DiscountPercent", "DiscountAmount", "Extended", "Taxable"];
        const hiddenRentals: Array<string> = fieldNames.filter(function (field) {
            return !this.has(field)
        }, new Set(rentalShowFields))
        for (let i = 0; i < hiddenRentals.length; i++) {
            jQuery($rentalGrid.find(`[data-mappedfield="${hiddenRentals[i]}"]`)).parent().hide();
        }
        // ----------
        const salesShowFields: Array<string> = ["OrderNumber", "ICode", "Description", "Quantity", "Unit", "Cost", "Rate", "DiscountPercent", "DiscountAmount", "Extended", "Taxable"];
        const hiddenSales = fieldNames.filter(function (field) {
            return !this.has(field)
        }, new Set(salesShowFields))
        const $salesGrid = $form.find('.salesgrid [data-name="InvoiceItemGrid"]');
        for (let i = 0; i < hiddenSales.length; i++) {
            jQuery($salesGrid.find(`[data-mappedfield="${hiddenSales[i]}"]`)).parent().hide();
        }
        // ----------
        const laborShowFields: Array<string> = ["OrderNumber", "ICode", "Description", "Quantity", "FromDate", "FromTime", "ToDate", "ToTime", "Days", "Unit", "Rate", "Cost", "DiscountAmount", "Extended", "Taxable"];
        const hiddenLabor = fieldNames.filter(function (field) {
            return !this.has(field)
        }, new Set(laborShowFields))
        const $laborGrid = $form.find('.laborgrid [data-name="InvoiceItemGrid"]');
        for (let i = 0; i < hiddenLabor.length; i++) {
            jQuery($laborGrid.find(`[data-mappedfield="${hiddenLabor[i]}"]`)).parent().hide();
        }
        // ----------
        const miscShowFields: Array<string> = ["OrderNumber", "ICode", "Description", "Quantity", "FromDate", "ToDate", "Unit", "Days", "Rate", "Cost", "DiscountPercent", "DiscountAmount", "Extended", "Taxable"];
        const hiddenMisc = fieldNames.filter(function (field) {
            return !this.has(field)
        }, new Set(miscShowFields))
        const $miscGrid = $form.find('.miscgrid [data-name="InvoiceItemGrid"]');
        for (let i = 0; i < hiddenMisc.length; i++) {
            jQuery($miscGrid.find(`[data-mappedfield="${hiddenMisc[i]}"]`)).parent().hide();
        }
        // ----------
        const rentalSaleShowFields: Array<string> = ["OrderNumber", "SerialNumber", "BarCode", "ICode", "Description", "Quantity", "Cost", "Unit", "Rate", "DiscountAmount", "Extended", "Taxable"];
        const hiddenRentalSale = fieldNames.filter(function (field) {
            return !this.has(field)
        }, new Set(rentalSaleShowFields))
        const $rentalSaleGrid = $form.find('.rentalsalegrid [data-name="InvoiceItemGrid"]');
        for (let i = 0; i < hiddenRentalSale.length; i++) {
            jQuery($rentalSaleGrid.find(`[data-mappedfield="${hiddenRentalSale[i]}"]`)).parent().hide();
        }
        // ----------
        // Item Adjustment Grids - Rental, Sales, Parts
        const adjustmentShowFields: Array<string> = ["ICode", "Description", "BarCode", "SerialNumber", "Quantity", "Rate", "Extended", "Taxable"];
        const hiddenAdjustment = fieldNames.filter(function (field) {
            return !this.has(field)
        }, new Set(adjustmentShowFields))
        const $rentalAdjustmentGrid = $form.find('.rentaladjustment [data-name="InvoiceItemGrid"]');
        const $salesAdjustmentGrid = $form.find('.salesadjustment [data-name="InvoiceItemGrid"]');
        const $partsAdjustmentGrid = $form.find('.partsadjustment [data-name="InvoiceItemGrid"]');
        for (let i = 0; i < hiddenAdjustment.length; i++) {
            jQuery($rentalAdjustmentGrid.find(`[data-mappedfield="${hiddenAdjustment[i]}"]`)).parent().hide();
            jQuery($salesAdjustmentGrid.find(`[data-mappedfield="${hiddenAdjustment[i]}"]`)).parent().hide();
            jQuery($partsAdjustmentGrid.find(`[data-mappedfield="${hiddenAdjustment[i]}"]`)).parent().hide();
        }
    };
    //----------------------------------------------------------------------------------------------
    calculateInvoiceItemGridTotals($form: JQuery, gridType: string, totals?, isAdjustment?: boolean): void {
        if (isAdjustment) {
            const subTotal = totals.LineTotal;
            const salesTax = totals.Tax;
            const total = totals.LineTotalWithTax;

            $form.find(`.${gridType}-totals [data-totalfield="SubTotal"] input`).val(subTotal);
            $form.find(`.${gridType}-totals [data-totalfield="Tax"] input`).val(salesTax);
            $form.find(`.${gridType}-totals [data-totalfield="Total"] input`).val(total);
        } else {
            const grossTotal = totals.LineTotalBeforeDiscount;
            const discount = totals.DiscountAmount;
            const subTotal = totals.LineTotal;
            const salesTax = totals.Tax;
            const total = totals.LineTotalWithTax;

            $form.find(`.${gridType}-totals [data-totalfield="GrossTotal"] input`).val(grossTotal);
            $form.find(`.${gridType}-totals [data-totalfield="Discount"] input`).val(discount);
            $form.find(`.${gridType}-totals [data-totalfield="SubTotal"] input`).val(subTotal);
            $form.find(`.${gridType}-totals [data-totalfield="Tax"] input`).val(salesTax);
            $form.find(`.${gridType}-totals [data-totalfield="Total"] input`).val(total);
        }
    };
    //----------------------------------------------------------------------------------------------
    openEmailHistoryBrowse($form) {
        const $browse = EmailHistoryController.openBrowse();

        $browse.data('ondatabind', function (request) {
            request.uniqueids = {
                RelatedToId: $form.find('[data-datafield="InvoiceId"] input.fwformfield-value').val()
            }
        });

        return $browse;
    }
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
            try {
                const module = this.Module;
                const recordTitle = jQuery('.tabs .active[data-tabtype="FORM"] .caption').text();
                const $report = InvoiceReportController.openForm();

                FwModule.openSubModuleTab($form, $report);

                const invoiceId = $form.find(`div.fwformfield[data-datafield="${module}Id"] input`).val();
                $report.find(`div.fwformfield[data-datafield="${module}Id"] input`).val(invoiceId);
                const invoiceNumber = $form.find(`div.fwformfield[data-datafield="${module}Number"] input`).val();
                $report.find(`div.fwformfield[data-datafield="${module}Id"] .fwformfield-text`).val(invoiceNumber);
                jQuery('.tab.submodule.active').find('.caption').html(`Print ${module}`);

                const printTab = jQuery('.tab.submodule.active');
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
        try {
            const parsedFromDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingStartDate'));
            const parsedToDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingEndDate'));

            if (parsedToDate < parsedFromDate) {
                $form.find('div[data-datafield="BillingEndDate"]').addClass('error');
                FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'From Date'.");
            } else {
                $form.find('div[data-datafield="BillingEndDate"]').removeClass('error');
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    };
    //----------------------------------------------------------------------------------------------
    voidInvoice($form: JQuery): void {
        try {
            const $confirmation = FwConfirmation.renderConfirmation('Void', '');
            $confirmation.find('.fwconfirmationbox').css('width', '450px');
            const html: Array<string> = [];
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div>Void Invoice?</div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));
            const $yes = FwConfirmation.addButton($confirmation, 'Void', false);
            const $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', makeVoid);

            function makeVoid() {
                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Voiding...');
                $yes.off('click');

                const invoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
                FwAppData.apiMethod(true, 'POST', `api/v1/invoice/${invoiceId}/void`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Invoice Successfully Voided');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwModule.refreshForm($form, InvoiceController);
                }, function onError(response) {
                    $yes.on('click', makeVoid);
                    $yes.text('Void');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                    FwModule.refreshForm($form, InvoiceController);
                }, $form);
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    creditInvoice($form) {
        const status = FwFormField.getValueByDataField($form, 'Status');
        if (status === 'PROCESSED' || status === 'CLOSED') {
            const $confirmation = FwConfirmation.renderConfirmation('Credit Invoice', '');
            $confirmation.find('.fwconfirmationbox').css('width', '550px');
            const html: Array<string> = [];
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="FULL - Every line of the Invoice will be credited 100%" data-invoicefield="FULL" style="float:left;width:100px;"></div>`);
            html.push('  </div>');
            html.push(' <div class="formrow" style="width:100%;display:flex;align-content:flex-start;align-items:center;padding-bottom:13px;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="" data-invoicefield="PARTIAL" style="float:left;width:30px;"></div>`);
            html.push('  </div>');
            html.push('  <span style="margin:18px 0px 0px 0px;">PARTIAL - Every Line of the Invoice will be credited</span>');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow" style="margin:0px 0px 0px 0px;">');
            html.push('    <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield input-field" data-caption="" data-enabled="false" data-invoicefield="PartialInput" style="width:45px;float:left;margin:0px 0px 0px 0px;"></div>');
            html.push('  </div>');
            html.push('  <span style="margin:18px 0px 0px 0px;">%</span>');
            html.push(' </div>');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="MANUAL - Items must be credited manually" data-invoicefield="Manual" style="float:left;width:100px;"></div>`);
            html.push('  </div>');
            html.push(' <div class="formrow" style="width:100%;display:flex;align-content:flex-start;align-items:center;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="" data-invoicefield="FLAT_AMOUNT" style="float:left;width:30px;"></div>`);
            html.push('  </div>');
            html.push('  <span style="margin:18px 0px 0px 0px;">FLAT AMT - Credit a flat amount</span>');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow" style="margin:0px 0px 0px 0px;">');
            html.push('    <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield input-field" data-caption="" data-enabled="false" data-invoicefield="FlatAmountInput" style="width:115px;float:left;margin:0px 0px 0px 0px;"></div>');
            html.push('  </div>');
            html.push(' </div>');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow" style="margin-bottom: 0;">');
            html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allocate Across All Items" data-invoicefield="AllocateAllItems" style="float:left;width:100px;padding: 0 0 0 30px;"></div>`);
            html.push('  </div>');
            html.push(' <div class="formrow" style="width:100%;display:flex;align-content:flex-start;align-items:center;padding-bottom:13px;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="" data-invoicefield="USAGE_DAYS" style="float:left;width:30px;"></div>`);
            html.push('  </div>');
            html.push('  <span style="margin:18px 0px 0px 0px;">USAGE DAYS - Cedit a number of Usage Days</span>');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow" style="margin:0px 0px 0px 0px;">');
            html.push('    <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield input-field" data-caption="" data-enabled="false" data-invoicefield="UsageDaysInput" style="width:45px;float:left;margin:0px 0px 0px 0px;"></div>');
            html.push('  </div>');
            html.push(' </div>');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="TAX ONLY - Credit the Sales Tax Only" data-invoicefield="TAX_ONLY" style="float:left;width:100px;"></div>`);
            html.push('  </div>');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Adjust Cost of Subs" data-invoicefield="AdjustCost" checked="checked" style="float:left;width:100px;"></div>`);
            html.push('  </div>');
            html.push('  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Credit Note">');
            html.push(`    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">`);
            html.push('      <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="" data-invoicefield="CreditNote"></div>');
            html.push('    </div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));
            const $yes = FwConfirmation.addButton($confirmation, 'Create', false);
            const $no = FwConfirmation.addButton($confirmation, 'Cancel');


            const full = $confirmation.find('div[data-invoicefield="FULL"] input');
            const partial = $confirmation.find('div[data-invoicefield="PARTIAL"] input');
            const partialInput = $confirmation.find('div[data-invoicefield="PartialInput"]');
            const manual = $confirmation.find('div[data-invoicefield="MANUAL"] input');
            const flatAmount = $confirmation.find('div[data-invoicefield="FLAT_AMOUNT"] input');
            const flatAmountInput = $confirmation.find('div[data-invoicefield="FlatAmountInput"]');
            const allocateAllItems = $confirmation.find('div[data-invoicefield="AllocateAllItems"] input');
            const usageDays = $confirmation.find('div[data-invoicefield="USAGE_DAYS"] input');
            const usageDaysInput = $confirmation.find('div[data-invoicefield="UsageDaysInput"]');
            const taxOnly = $confirmation.find('div[data-invoicefield="TAX_ONLY"] input');

            full.on('change', e => {
                if (jQuery(e.currentTarget).prop('checked')) {
                    partial.prop('checked', false);
                    manual.prop('checked', false);
                    flatAmount.prop('checked', false);
                    allocateAllItems.prop('checked', false);
                    usageDays.prop('checked', false);
                    taxOnly.prop('checked', false);
                    $confirmation.find('.input-field input').val('');
                    FwFormField.disable(partialInput);
                    FwFormField.disable(flatAmountInput);
                    FwFormField.disable($confirmation.find('div[data-invoicefield="AllocateAllItems"]'));
                    FwFormField.disable(usageDaysInput);
                }
            });
            partial.on('change', e => {
                if (jQuery(e.currentTarget).prop('checked')) {
                    full.prop('checked', false);
                    manual.prop('checked', false);
                    flatAmount.prop('checked', false);
                    allocateAllItems.prop('checked', false);
                    usageDays.prop('checked', false);
                    taxOnly.prop('checked', false);
                    $confirmation.find('.input-field input').val('');
                    FwFormField.disable($confirmation.find('div[data-invoicefield="AllocateAllItems"]'));
                    FwFormField.enable(partialInput);
                    FwFormField.disable(flatAmountInput);
                    FwFormField.disable(usageDaysInput);
                }
            });
            manual.on('change', e => {
                if (jQuery(e.currentTarget).prop('checked')) {
                    full.prop('checked', false);
                    partial.prop('checked', false);
                    flatAmount.prop('checked', false);
                    allocateAllItems.prop('checked', false);
                    usageDays.prop('checked', false);
                    taxOnly.prop('checked', false);
                    $confirmation.find('.input-field input').val('');
                    FwFormField.disable($confirmation.find('div[data-invoicefield="AllocateAllItems"]'));
                    FwFormField.disable(partialInput);
                    FwFormField.disable(flatAmountInput);
                    FwFormField.disable(usageDaysInput);
                }
            });
            flatAmount.on('change', e => {
                if (jQuery(e.currentTarget).prop('checked')) {
                    full.prop('checked', false);
                    partial.prop('checked', false);
                    manual.prop('checked', false);
                    usageDays.prop('checked', false);
                    taxOnly.prop('checked', false);
                    $confirmation.find('.input-field input').val('');
                    FwFormField.enable(flatAmountInput);
                    FwFormField.enable($confirmation.find('div[data-invoicefield="AllocateAllItems"]'));
                    FwFormField.disable(partialInput);
                    FwFormField.disable(usageDaysInput);
                }
            });
            usageDays.on('change', e => {
                if (jQuery(e.currentTarget).prop('checked')) {
                    full.prop('checked', false);
                    partial.prop('checked', false);
                    manual.prop('checked', false);
                    flatAmount.prop('checked', false);
                    allocateAllItems.prop('checked', false);
                    taxOnly.prop('checked', false);
                    $confirmation.find('.input-field input').val('');
                    FwFormField.disable($confirmation.find('div[data-invoicefield="AllocateAllItems"]'));
                    FwFormField.enable(usageDaysInput);
                    FwFormField.disable(partialInput);
                    FwFormField.disable(flatAmountInput);
                }
            });
            taxOnly.on('change', e => {
                if (jQuery(e.currentTarget).prop('checked')) {
                    full.prop('checked', false);
                    partial.prop('checked', false);
                    manual.prop('checked', false);
                    flatAmount.prop('checked', false);
                    allocateAllItems.prop('checked', false);
                    usageDays.prop('checked', false);
                    $confirmation.find('.input-field input').val('');
                    FwFormField.disable($confirmation.find('div[data-invoicefield="AllocateAllItems"]'));
                    FwFormField.disable(partialInput);
                    FwFormField.disable(flatAmountInput);
                    FwFormField.disable(usageDaysInput);
                }
            });

            $yes.on('click', () => {
                const request: any = {};
                const creditMethods = [full, partial, manual, flatAmount, usageDays, taxOnly];
                for (let i = 0; i < creditMethods.length; i++) {
                    if (creditMethods[i].prop('checked') === true) {
                        request.CreditMethod = creditMethods[i].closest('[data-invoicefield]').attr('data-invoicefield');
                        break;
                    }
                }

                if (request.CreditMethod === 'PARTIAL') {
                    request.Percent = partialInput.find('input').val();
                }
                if (request.CreditMethod === 'FLAT_AMOUNT') {
                    request.Amount = flatAmountInput.find('input').val();
                    request.Allocate = allocateAllItems.prop('checked');
                }
                if (request.CreditMethod === 'USAGE_DAYS') {
                    request.UsageDays = usageDaysInput.find('input').val();
                }

                request.InvoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
                request.Notes = $confirmation.find('div[data-invoicefield="CreditNote"] textarea').val();
                request.CreditFromDate = FwFormField.getValueByDataField($form, 'BillingStartDate');
                request.CreditToDate = FwFormField.getValueByDataField($form, 'BillingEndDate');
                request.AdjustCost = $confirmation.find('div[data-invoicefield="AdjustCost"] input').prop('checked');
                request.TaxOnly = taxOnly.prop('checked');

                FwAppData.apiMethod(true, 'POST', `api/v1/invoice/creditinvoice`, request, FwServices.defaultTimeout, response => {
                    // capture the "@creditid" output parameter and open the Credit Invoice using that ID so the user can see newly-created Credit Invoice Form.
                    FwNotification.renderNotification('INFO', 'Creating Credit Invoice...');
                    const uniqueids: any = {};
                    uniqueids.InvoiceId = response.CreditId;
                    const InvoiceForm = InvoiceController.loadForm(uniqueids);

                    FwModule.openModuleTab(InvoiceForm, "", true, 'FORM', true);
                }, ex => FwFunc.showError(ex), $form);

                FwConfirmation.destroyConfirmation($confirmation);
            });
        } else
            FwNotification.renderNotification('WARNING', 'This feature is only available for PROCESSED or CLOSED Invoices.')
    }
};

//----------------------------------------------------------------------------------------------
// Void Invoice - Form
FwApplicationTree.clickEvents[Constants.Modules.Home.Invoice.form.menuItems.Void.id] = function (e: JQuery.ClickEvent) {
    const $form = jQuery(this).closest('.fwform');
    try {
        InvoiceController.voidInvoice($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
// Void Invoice - Browse
FwApplicationTree.clickEvents[Constants.Modules.Home.Invoice.browse.menuItems.Void.id] = function (e: JQuery.ClickEvent) {
    try {
        const $browse = jQuery(this).closest('.fwbrowse');
        const invoiceId = $browse.find('.selected [data-browsedatafield="InvoiceId"]').attr('data-originalvalue');
        if (invoiceId != null) {
            const $confirmation = FwConfirmation.renderConfirmation('Void', '');
            $confirmation.find('.fwconfirmationbox').css('width', '450px');
            const html: Array<string> = [];
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div>Void Invoice?</div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));
            const $yes = FwConfirmation.addButton($confirmation, 'Void', false);
            const $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', makeVoid);

            function makeVoid() {
                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Voiding...');
                $yes.off('click');

                FwAppData.apiMethod(true, 'POST', `api/v1/invoice/${invoiceId}/void`, null, FwServices.defaultTimeout, function onSuccess(response) {
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
FwApplicationTree.clickEvents[Constants.Modules.Home.Invoice.form.menuItems.PrintInvoice.id] = function (e: JQuery.ClickEvent) {
    try {
        const $form = jQuery(this).closest('.fwform');
        $form.find('.print-invoice').trigger('click');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
//form approve
FwApplicationTree.clickEvents[Constants.Modules.Home.Invoice.form.menuItems.Approve.id] = function (event: JQuery.ClickEvent) {
    try {
        const $form = jQuery(this).closest('.fwform');
        const invoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
        FwAppData.apiMethod(true, 'POST', `api/v1/invoice/${invoiceId}/approve`, null, FwServices.defaultTimeout, function onSuccess(response) {
            if (response.success === true) {
                FwModule.refreshForm($form, InvoiceController);
            } else {
                FwNotification.renderNotification('WARNING', response.msg);
            }
        }, null, $form);
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
//form unapprove
FwApplicationTree.clickEvents[Constants.Modules.Home.Invoice.form.menuItems.Unapprove.id] = function (event: JQuery.ClickEvent) {
    try {
        const $form = jQuery(this).closest('.fwform');
        const invoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
        FwAppData.apiMethod(true, 'POST', `api/v1/invoice/${invoiceId}/unapprove`, null, FwServices.defaultTimeout, function onSuccess(response) {
            if (response.success === true) {
                FwModule.refreshForm($form, InvoiceController);
            } else {
                FwNotification.renderNotification('WARNING', response.msg);
            }
        }, null, $form);
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
//form credit invoice
FwApplicationTree.clickEvents[Constants.Modules.Home.Invoice.form.menuItems.CreditInvoice.id] = function (event: JQuery.ClickEvent) {
    try {
        const $form = jQuery(this).closest('.fwform');
        InvoiceController.creditInvoice($form);
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
//browse approve
FwApplicationTree.clickEvents[Constants.Modules.Home.Invoice.browse.menuItems.Approve.id] = function (event: JQuery.ClickEvent) {
    try {
        const $browse = jQuery(this).closest('.fwbrowse');
        const invoiceId = $browse.find('.selected [data-browsedatafield="InvoiceId"]').attr('data-originalvalue');
        if (typeof invoiceId !== 'undefined') {
            FwAppData.apiMethod(true, 'POST', `api/v1/invoice/${invoiceId}/approve`, null, FwServices.defaultTimeout, function onSuccess(response) {
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
FwApplicationTree.clickEvents[Constants.Modules.Home.Invoice.browse.menuItems.Unapprove.id] = function (event: JQuery.ClickEvent) {
    try {
        const $browse = jQuery(this).closest('.fwbrowse');
        const invoiceId = $browse.find('.selected [data-browsedatafield="InvoiceId"]').attr('data-originalvalue');
        if (typeof invoiceId !== 'undefined') {
            FwAppData.apiMethod(true, 'POST', `api/v1/invoice/${invoiceId}/unapprove`, null, FwServices.defaultTimeout, function onSuccess(response) {
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