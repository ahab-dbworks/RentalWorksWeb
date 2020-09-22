routes.push({ pattern: /^module\/billingworksheet$/, action: function (match: RegExpExecArray) { return BillingWorksheetController.getModuleScreen(); } });

class BillingWorksheet {
    Module: string = 'BillingWorksheet';
    apiurl: string = 'api/v1/billingworksheet';
    caption: string = Constants.Modules.Billing.children.BillingWorksheet.caption;
    nav: string = Constants.Modules.Billing.children.BillingWorksheet.nav;
    id: string = Constants.Modules.Billing.children.BillingWorksheet.id;
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
    }
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


        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasDelete = true;
        FwMenu.addBrowseMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Approve', 'eMYtyUHlOkUuo', (e: JQuery.ClickEvent) => {
            try {
                this.browseApproveOrUnapprove(options.$browse, 'approve');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Unapprove', 'sJydlcSDO02Zs', (e: JQuery.ClickEvent) => {
            try {
                this.browseApproveOrUnapprove(options.$browse, 'approve');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        const $new = FwMenu.generateDropDownViewBtn('New', false, "NEW");
        const $approved = FwMenu.generateDropDownViewBtn('Approved', false, "APPROVED");
        //const $newapproved = FwMenu.generateDropDownViewBtn('New & Approved', false, "NEWAPPROVED");
        const $processed = FwMenu.generateDropDownViewBtn('Processed', false, "PROCESSED");
        const $closed = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");
        const $void = FwMenu.generateDropDownViewBtn('Void', false, "VOID");
        const $all = FwMenu.generateDropDownViewBtn('All', true, "ALL");

        const viewSubitems: Array<JQuery> = [];
        //viewSubitems.push($all, $new, $approved, $newapproved, $processed, $closed, $void);
        viewSubitems.push($all, $new, $approved, $processed, $closed, $void);
        FwMenu.addViewBtn(options.$menu, 'View', viewSubitems, true, "Status");

        //Location Filter
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        const viewLocation: Array<JQuery> = [];
        viewLocation.push($allLocations, $userLocation);
        FwMenu.addViewBtn(options.$menu, 'Location', viewLocation, true, "LocationId");
    }
    //-----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Approve', 'eMYtyUHlOkUuo', (e: JQuery.ClickEvent) => {
            try {
                this.approveOrUnaproveWorksheet(options.$form, 'approve')
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Unapprove', 'sJydlcSDO02Zs', (e: JQuery.ClickEvent) => {
            try {
                this.approveOrUnaproveWorksheet(options.$form, 'unapprove')
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');

            const usersid = sessionStorage.getItem('usersid');  // J. Pace 7/09/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
            const name = sessionStorage.getItem('name');
            FwFormField.setValue($form, 'div[data-datafield="ProjectManagerId"]', usersid, name);
            FwFormField.setValue($form, 'div[data-datafield="AgentId"]', usersid, name);

            const today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'WorksheetDate', today);

            FwFormField.enable($form.find('[data-datafield="StatusDate"]'));
            FwFormField.disable($form.find('[data-datafield="DealId"]'));

            FwFormField.setValueByDataField($form, 'StatusDate', today);
            const department = JSON.parse(sessionStorage.getItem('department'));
            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);

            const office = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);

            FwFormField.setValueByDataField($form, 'Status', 'NEW');

            // hide tabs
            $form.find('.hide-new').hide();
        } else {
            FwFormField.disable($form.find('.ifnew'));
        }

        if (typeof parentModuleInfo !== 'undefined') {
            FwFormField.setValue($form, 'div[data-datafield="DealId"]', parentModuleInfo.DealId, parentModuleInfo.Deal);
            FwFormField.setValue($form, 'div[data-datafield="DealNumber"]', parentModuleInfo.DealNumber);
            FwFormField.setValue($form, 'div[data-datafield="OrderId"]', parentModuleInfo.OrderId, parentModuleInfo.OrderNumber);
            FwFormField.setValue($form, 'div[data-datafield="WorksheetDescription"]', parentModuleInfo.OrderDescription);
            FwFormField.setValue($form, 'div[data-datafield="OrderTypeId"]', parentModuleInfo.OrderTypeId, parentModuleInfo.OrderType);
        }

        this.events($form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="BillingWorksheetId"] input').val(uniqueids.BillingWorksheetId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: JQuery, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
        const invoiceItemTotalFields = ["LineTotalWithTax", "Tax", "Tax2", "LineTotal", "LineTotalBeforeDiscount", "DiscountAmount"];
        //                               Total               Tax   SubTotal      GrossTotal                 Discount
        // ----------
        //const $invoiceItemGridRental = $form.find('.rentalgrid div[data-grid="InvoiceItemGrid"]');
        //const $invoiceItemGridRentalControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
        //$invoiceItemGridRental.empty().append($invoiceItemGridRentalControl);
        //$invoiceItemGridRentalControl.data('isSummary', false);
        //$invoiceItemGridRental.addClass('R');
        //$invoiceItemGridRentalControl.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Rate');

        //$invoiceItemGridRentalControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId'),
        //        RecType: 'R'
        //    };
        //    request.totalfields = invoiceItemTotalFields;
        //});
        //$invoiceItemGridRentalControl.data('beforesave', request => {
        //    request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
        //    request.RecType = 'R';
        //});

        //FwBrowse.addEventHandler($invoiceItemGridRentalControl, 'afterdatabindcallback', ($invoiceItemGridRentalControl, dt) => {
        //    this.calculateInvoiceItemGridTotals($form, 'rental', dt.Totals);
        //});
        //FwBrowse.init($invoiceItemGridRentalControl);
        //FwBrowse.renderRuntimeHtml($invoiceItemGridRentalControl);


        FwBrowse.renderGrid({
            nameGrid: 'InvoiceItemGrid',
            gridSelector: '.rentalgrid div[data-grid="InvoiceItemGrid"]',
            gridSecurityId: '5xgHiF8dduf',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId'),
                    RecType: 'R'
                };
                request.totalfields = invoiceItemTotalFields;
            },
            beforeSave: (request: any) => {
                    request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
                    request.RecType = 'R';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('R');
                $browse.data('isSummary', false);
                $browse.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Rate');
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateInvoiceItemGridTotals($form, 'rental', dt.Totals);
            },
        });
        // ----------
        //const $invoiceItemGridSales = $form.find('.salesgrid div[data-grid="InvoiceItemGrid"]');
        //const $invoiceItemGridSalesControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
        //$invoiceItemGridSales.empty().append($invoiceItemGridSalesControl);
        //$invoiceItemGridSales.addClass('S');
        //$invoiceItemGridSalesControl.data('isSummary', false);
        //$invoiceItemGridSalesControl.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Price');

        //$invoiceItemGridSalesControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId'),
        //        RecType: 'S'
        //    };
        //    request.totalfields = invoiceItemTotalFields;
        //});
        //$invoiceItemGridSalesControl.data('beforesave', request => {
        //    request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
        //    request.RecType = 'S';
        //});
        //FwBrowse.addEventHandler($invoiceItemGridSalesControl, 'afterdatabindcallback', ($invoiceItemGridSalesControl, dt) => {
        //    this.calculateInvoiceItemGridTotals($form, 'sales', dt.Totals);
        //});
        //FwBrowse.init($invoiceItemGridSalesControl);
        //FwBrowse.renderRuntimeHtml($invoiceItemGridSalesControl);


        FwBrowse.renderGrid({
            nameGrid: 'InvoiceItemGrid',
            gridSelector: '.salesgrid div[data-grid="InvoiceItemGrid"]',
            gridSecurityId: '5xgHiF8dduf',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId'),
                    RecType: 'S'
                };
                request.totalfields = invoiceItemTotalFields;
            },
            beforeSave: (request: any) => {
                request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
                request.RecType = 'S';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('S');
                $browse.data('isSummary', false);
                $browse.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Price');
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateInvoiceItemGridTotals($form, 'sales', dt.Totals);
            },
        });
        // ----------
        //const $invoiceItemGridFacilities = $form.find('.facilitiesgrid div[data-grid="InvoiceItemGrid"]');
        //const $invoiceItemGridFacilitiesControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
        //$invoiceItemGridFacilities.empty().append($invoiceItemGridFacilitiesControl);
        //$invoiceItemGridFacilities.addClass('F');
        //$invoiceItemGridFacilities.find('div[data-datafield="Extended"]').attr('data-formreadonly', 'true');
        //$invoiceItemGridFacilities.find('div[data-datafield="InventoryId"]').attr('data-formreadonly', 'true');
        //$invoiceItemGridFacilities.find('div[data-datafield="OrderNumber"]').attr('data-formreadonly', 'true');
        //$invoiceItemGridFacilities.find('div[data-datafield="Taxable"]').attr('data-formreadonly', 'true');
        //$invoiceItemGridFacilitiesControl.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Rate');
        //$invoiceItemGridFacilitiesControl.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
        //$invoiceItemGridFacilitiesControl.data('isSummary', false);

        //$invoiceItemGridFacilitiesControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId'),
        //        RecType: 'F'
        //    };
        //    request.totalfields = invoiceItemTotalFields;
        //});
        //$invoiceItemGridFacilitiesControl.data('beforesave', request => {
        //    request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
        //    request.RecType = 'F';
        //});
        //FwBrowse.addEventHandler($invoiceItemGridFacilitiesControl, 'afterdatabindcallback', ($invoiceItemGridFacilitiesControl, dt) => {
        //    this.calculateInvoiceItemGridTotals($form, 'facilities', dt.Totals);
        //});
        //FwBrowse.init($invoiceItemGridFacilitiesControl);
        //FwBrowse.renderRuntimeHtml($invoiceItemGridFacilitiesControl);

        FwBrowse.renderGrid({
            nameGrid: 'InvoiceItemGrid',
            gridSelector: '.facilitiesgrid div[data-grid="InvoiceItemGrid"]',
            gridSecurityId: '5xgHiF8dduf',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId'),
                    RecType: 'F'
                };
                request.totalfields = invoiceItemTotalFields;
            },
            beforeSave: (request: any) => {
                request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
                request.RecType = 'F';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('F');
                $fwgrid.find('div[data-datafield="Extended"]').attr('data-formreadonly', 'true');
                $fwgrid.find('div[data-datafield="InventoryId"]').attr('data-formreadonly', 'true');
                $fwgrid.find('div[data-datafield="OrderNumber"]').attr('data-formreadonly', 'true');
                $fwgrid.find('div[data-datafield="Taxable"]').attr('data-formreadonly', 'true');
                $browse.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Rate');
                $browse.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
                $browse.data('isSummary', false);
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateInvoiceItemGridTotals($form, 'facilities', dt.Totals);
            },
        });
        //const $invoiceItemGridLabor = $form.find('.laborgrid div[data-grid="InvoiceItemGrid"]');
        //const $invoiceItemGridLaborControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
        //$invoiceItemGridLabor.empty().append($invoiceItemGridLaborControl);
        //$invoiceItemGridLabor.addClass('L');
        //$invoiceItemGridLabor.find('div[data-datafield="Extended"]').attr('data-formreadonly', 'true');
        //$invoiceItemGridLabor.find('div[data-datafield="InventoryId"]').attr('data-formreadonly', 'true');
        //$invoiceItemGridLabor.find('div[data-datafield="OrderNumber"]').attr('data-formreadonly', 'true');
        //$invoiceItemGridLabor.find('div[data-datafield="Taxable"]').attr('data-formreadonly', 'true');
        //$invoiceItemGridLaborControl.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Rate');
        //$invoiceItemGridLaborControl.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
        //FwBrowse.disableGrid($invoiceItemGridLabor);
        //$invoiceItemGridLaborControl.attr('data-deleteoption', 'false');

        //$invoiceItemGridLaborControl.data('isSummary', false);

        //$invoiceItemGridLaborControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId'),
        //        RecType: 'L'
        //    };
        //    request.totalfields = invoiceItemTotalFields;
        //});
        //$invoiceItemGridLaborControl.data('beforesave', request => {
        //    request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
        //    request.RecType = 'L';
        //});
        //FwBrowse.addEventHandler($invoiceItemGridLaborControl, 'afterdatabindcallback', ($invoiceItemGridLaborControl, dt) => {
        //    this.calculateInvoiceItemGridTotals($form, 'labor', dt.Totals);
        //});
        //FwBrowse.init($invoiceItemGridLaborControl);
        //FwBrowse.renderRuntimeHtml($invoiceItemGridLaborControl);
        // ----------
        // ----------
        //const $invoiceItemGridMisc = $form.find('.miscgrid div[data-grid="InvoiceItemGrid"]');
        //const $invoiceItemGridMiscControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
        //$invoiceItemGridMisc.empty().append($invoiceItemGridMiscControl);
        //$invoiceItemGridMisc.addClass('M');
        //$invoiceItemGridMisc.find('div[data-datafield="Extended"]').attr('data-formreadonly', 'true')
        //$invoiceItemGridMiscControl.data('isSummary', false);
        //$invoiceItemGridMiscControl.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Rate');
        //$invoiceItemGridMiscControl.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');

        //$invoiceItemGridMiscControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId'),
        //        RecType: 'M'
        //    };
        //    request.totalfields = invoiceItemTotalFields;
        //});
        //$invoiceItemGridMiscControl.data('beforesave', request => {
        //    request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
        //    request.RecType = 'M';
        //});
        //FwBrowse.addEventHandler($invoiceItemGridMiscControl, 'afterdatabindcallback', ($invoiceItemGridMiscControl, dt) => {
        //    this.calculateInvoiceItemGridTotals($form, 'misc', dt.Totals);
        //});
        //FwBrowse.init($invoiceItemGridMiscControl);
        //FwBrowse.renderRuntimeHtml($invoiceItemGridMiscControl);

        FwBrowse.renderGrid({
            nameGrid: 'InvoiceItemGrid',
            gridSelector: '.miscgrid div[data-grid="InvoiceItemGrid"]',
            gridSecurityId: '5xgHiF8dduf',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId'),
                    RecType: 'M'
                };
                request.totalfields = invoiceItemTotalFields;
            },
            beforeSave: (request: any) => {
                request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
                request.RecType = 'M';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('M');
                $fwgrid.find('div[data-datafield="Extended"]').attr('data-formreadonly', 'true')
                $browse.data('isSummary', false);
                $browse.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Rate');
                $browse.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateInvoiceItemGridTotals($form, 'misc', dt.Totals);
            },
        });
        // ----------
        //const $invoiceItemGridRentalSale = $form.find('.rentalsalegrid div[data-grid="InvoiceItemGrid"]');
        //const $invoiceItemGridRentalSaleControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
        //$invoiceItemGridRentalSale.empty().append($invoiceItemGridRentalSaleControl);
        //$invoiceItemGridRentalSale.addClass('RS');
        //FwBrowse.disableGrid($invoiceItemGridRentalSale);
        //$invoiceItemGridRentalSaleControl.attr('data-deleteoption', 'false');
        //$invoiceItemGridRentalSaleControl.data('isSummary', false);
        //$invoiceItemGridRentalSaleControl.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Price');

        //$invoiceItemGridRentalSaleControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId'),
        //        RecType: 'RS'
        //    };
        //    request.totalfields = invoiceItemTotalFields;
        //});
        //$invoiceItemGridRentalSaleControl.data('beforesave', request => {
        //    request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
        //    request.RecType = 'RS';
        //});
        //FwBrowse.addEventHandler($invoiceItemGridRentalSaleControl, 'afterdatabindcallback', ($invoiceItemGridRentalSaleControl, dt) => {
        //    //this.calculateInvoiceItemGridTotals($form, 'rentalsale', dt.Totals);
        //});
        //FwBrowse.init($invoiceItemGridRentalSaleControl);
        //FwBrowse.renderRuntimeHtml($invoiceItemGridRentalSaleControl);

        FwBrowse.renderGrid({
            nameGrid: 'InvoiceItemGrid',
            gridSelector: '.rentalsalegrid div[data-grid="InvoiceItemGrid"]',
            gridSecurityId: '5xgHiF8dduf',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId'),
                    RecType: 'RS'
                };
                request.totalfields = invoiceItemTotalFields;
            },
            beforeSave: (request: any) => {
                request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
                request.RecType = 'RS';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('RS');
                FwBrowse.disableGrid($fwgrid);
                $browse.attr('data-deleteoption', 'false');
                $browse.data('isSummary', false);
                $browse.find('div[data-datafield="Rate"]').attr('data-caption', 'Unit Price');
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateInvoiceItemGridTotals($form, 'rentalsale', dt.Totals);
            },
        });
        // ----------
        //const $invoiceNoteGrid = $form.find('div[data-grid="InvoiceNoteGrid"]');
        //const $invoiceNoteGridControl = FwBrowse.loadGridFromTemplate('InvoiceNoteGrid');
        //$invoiceNoteGrid.empty().append($invoiceNoteGridControl);
        //$invoiceNoteGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InvoiceId: $form.find('div.fwformfield[data-datafield="BillingWorksheetId"] input').val()
        //    }
        //});
        //$invoiceNoteGridControl.data('beforesave', function (request) {
        //    request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
        //})
        //FwBrowse.init($invoiceNoteGridControl);
        //FwBrowse.renderRuntimeHtml($invoiceNoteGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'InvoiceNoteGrid',
            gridSecurityId: '8YECGu7qFOty',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InvoiceId: $form.find('div.fwformfield[data-datafield="BillingWorksheetId"] input').val()
                };
            },
            beforeSave: (request: any) => {
                request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
            },
        });
        // ----------
        const glTotalFields = ["Debit", "Credit"];
        //const $glDistributionGrid = $form.find('div[data-grid="GlDistributionGrid"]');
        //const $glDistributionGridControl = FwBrowse.loadGridFromTemplate('GlDistributionGrid');
        //$glDistributionGrid.empty().append($glDistributionGridControl);
        //$glDistributionGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId')
        //    };
        //    request.totalfields = glTotalFields;
        //});
        //FwBrowse.addEventHandler($glDistributionGridControl, 'afterdatabindcallback', ($glDistributionGridControl, dt) => {
        //    FwFormField.setValue2($form.find('.gldistribution-totals [data-totalfield="Debit"]'), dt.Totals.Debit);
        //    FwFormField.setValue2($form.find('.gldistribution-totals [data-totalfield="Credit"]'), dt.Totals.Credit);
        //});
        //FwBrowse.init($glDistributionGridControl);
        //FwBrowse.renderRuntimeHtml($glDistributionGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'GlDistributionGrid',
            gridSecurityId: '8YECGu7qFOty',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId')
                };
                request.totalfields = glTotalFields;
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                FwFormField.setValue2($form.find('.gldistribution-totals [data-totalfield="Debit"]'), dt.Totals.Debit);
                FwFormField.setValue2($form.find('.gldistribution-totals [data-totalfield="Credit"]'), dt.Totals.Credit);
            },
        });
        // ----------
        //const $manualGlGrid = $form.find('div[data-grid="ManualGlTransactionsGrid"]');
        //const $manualGlGridControl = FwBrowse.loadGridFromTemplate('ManualGlTransactionsGrid');
        //$manualGlGrid.empty().append($manualGlGridControl);
        //$manualGlGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId')
        //    };
        //    request.totalfields = ["Amount"];
        //});
        //FwBrowse.addEventHandler($manualGlGridControl, 'afterdatabindcallback', ($manualGlGridControl, dt) => {
        //    FwFormField.setValue2($form.find('.manualgl-totals [data-totalfield="Amount"]'), dt.Totals.Amount);
        //});
        //$manualGlGridControl.data('beforesave', request => {
        //    request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
        //});
        //FwBrowse.init($manualGlGridControl);
        //FwBrowse.renderRuntimeHtml($manualGlGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'ManualGlTransactionsGrid',
            gridSecurityId: '8YECGu7qFOty',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId')
                };
                request.totalfields = ["Amount"];
            },
            beforeSave: (request: any) => {
                request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                FwFormField.setValue2($form.find('.manualgl-totals [data-totalfield="Amount"]'), dt.Totals.Amount);
            },
        });
        // ----------
        //const $invoiceOrderGrid = $form.find('div[data-grid="InvoiceOrderGrid"]');
        //const $invoiceOrderGridControl = FwBrowse.loadGridFromTemplate('InvoiceOrderGrid');
        //$invoiceOrderGrid.empty().append($invoiceOrderGridControl);
        //$invoiceOrderGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId')
        //    };
        //});
        //FwBrowse.init($invoiceOrderGridControl);
        //FwBrowse.renderRuntimeHtml($invoiceOrderGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'InvoiceOrderGrid',
            gridSecurityId: '8YECGu7qFOty',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId')
                };
            }
        });
        // ----------
        //const $invoiceRevenueGrid = $form.find('div[data-grid="InvoiceRevenueGrid"]');
        //const $invoiceRevenueGridControl = FwBrowse.loadGridFromTemplate('InvoiceRevenueGrid');
        //$invoiceRevenueGrid.empty().append($invoiceRevenueGridControl);
        //$invoiceRevenueGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId')
        //    };
        //});
        //FwBrowse.init($invoiceRevenueGridControl);
        //FwBrowse.renderRuntimeHtml($invoiceRevenueGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'InvoiceRevenueGrid',
            gridSecurityId: '8YECGu7qFOty',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId')
                };
            }
        });
        // ----------
        //const $invoiceReceiptGrid = $form.find('div[data-grid="InvoiceReceiptGrid"]');
        //const $invoiceReceiptGridControl = FwBrowse.loadGridFromTemplate('InvoiceReceiptGrid');
        //$invoiceReceiptGrid.empty().append($invoiceReceiptGridControl);
        //$invoiceReceiptGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId')
        //    };
        //});
        //FwBrowse.init($invoiceReceiptGridControl);
        //FwBrowse.renderRuntimeHtml($invoiceReceiptGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'InvoiceReceiptGrid',
            gridSecurityId: '8YECGu7qFOty',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId')
                };
            }
        });
        // ----------
        //const $invoiceStatusHistoryGrid = $form.find('div[data-grid="InvoiceStatusHistoryGrid"]');
        //const $invoiceStatusHistoryGridControl = FwBrowse.loadGridFromTemplate('InvoiceStatusHistoryGrid');
        //$invoiceStatusHistoryGrid.empty().append($invoiceStatusHistoryGridControl);
        //$invoiceStatusHistoryGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId')
        //    };
        //});
        //FwBrowse.init($invoiceStatusHistoryGridControl);
        //FwBrowse.renderRuntimeHtml($invoiceStatusHistoryGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'InvoiceStatusHistoryGrid',
            gridSecurityId: '8YECGu7qFOty',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId')
                };
            }
        });
        // Invoice Item Adjustment Grids
        // ----------
        const itemPageSize = 3;
        const itemAdjustmentTotalFields = ["LineTotalWithTax", "Tax", "LineTotal"];
        //const $invoiceItemGridAdjustmentRental = $form.find('.rentaladjustment div[data-grid="InvoiceItemGrid"]');
        //const $invoiceItemGridAdjustmentRentalControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
        //$invoiceItemGridAdjustmentRentalControl.attr('data-pagesize', itemPageSize);
        //$invoiceItemGridAdjustmentRental.empty().append($invoiceItemGridAdjustmentRentalControl);
        //$invoiceItemGridAdjustmentRental.addClass('R');

        //$invoiceItemGridAdjustmentRentalControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId'),
        //        RecType: 'A',
        //        AvailFor: 'R',
        //        pagesize: itemPageSize
        //    };
        //    request.totalfields = itemAdjustmentTotalFields;
        //});
        //$invoiceItemGridAdjustmentRentalControl.data('beforesave', request => {
        //    request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
        //    request.RecType = 'A';
        //    request.AvailFor = 'R';
        //    request.pagesize = itemPageSize;
        //});
        //FwBrowse.addEventHandler($invoiceItemGridAdjustmentRentalControl, 'afterdatabindcallback', ($invoiceItemGridAdjustmentRentalControl, dt) => {
        //    this.calculateInvoiceItemGridTotals($form, 'rentaladjustment', dt.Totals, true);
        //});

        //FwBrowse.init($invoiceItemGridAdjustmentRentalControl);
        //FwBrowse.renderRuntimeHtml($invoiceItemGridAdjustmentRentalControl);


        FwBrowse.renderGrid({
            nameGrid: 'InvoiceItemGrid',
            gridSelector: '.rentaladjustment div[data-grid="InvoiceItemGrid"]',
            gridSecurityId: '5xgHiF8dduf',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId'),
                RecType: 'A',
                AvailFor: 'R',
                pagesize: itemPageSize
            };
            request.totalfields = itemAdjustmentTotalFields;
            },
            beforeSave: (request: any) => {
                request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
                request.RecType = 'A';
                request.AvailFor = 'R';
                request.pagesize = itemPageSize;
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('R');
                $browse.attr('data-pagesize', itemPageSize);
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateInvoiceItemGridTotals($form, 'rentaladjustment', dt.Totals, true);
            },
        });
        // ----------
        //const $invoiceItemGridAdjustmentSales = $form.find('.salesadjustment div[data-grid="InvoiceItemGrid"]');
        //const $invoiceItemGridAdjustmentSalesControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
        //$invoiceItemGridAdjustmentSalesControl.attr('data-pagesize', itemPageSize);

        //$invoiceItemGridAdjustmentSales.empty().append($invoiceItemGridAdjustmentSalesControl);
        //$invoiceItemGridAdjustmentSales.addClass('S');
        //$invoiceItemGridAdjustmentSalesControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId'),
        //        pagesize: itemPageSize,
        //        RecType: 'A',
        //        AvailFor: 'S'
        //    };
        //    request.totalfields = itemAdjustmentTotalFields;
        //});
        //$invoiceItemGridAdjustmentSalesControl.data('beforesave', request => {
        //    request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
        //    request.RecType = 'A';
        //    request.AvailFor = 'S';
        //    request.pagesize = itemPageSize;
        //});
        //FwBrowse.addEventHandler($invoiceItemGridAdjustmentSalesControl, 'afterdatabindcallback', ($invoiceItemGridAdjustmentSalesControl, dt) => {
        //    this.calculateInvoiceItemGridTotals($form, 'salesadjustment', dt.Totals, true);
        //});

        //FwBrowse.init($invoiceItemGridAdjustmentSalesControl);
        //FwBrowse.renderRuntimeHtml($invoiceItemGridAdjustmentSalesControl);

        FwBrowse.renderGrid({
            nameGrid: 'InvoiceItemGrid',
            gridSelector: '.salesadjustment div[data-grid="InvoiceItemGrid"]',
            gridSecurityId: '5xgHiF8dduf',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
            request.uniqueids = {
                InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId'),
                pagesize: itemPageSize,
                RecType: 'A',
                AvailFor: 'S'
            };
            request.totalfields = itemAdjustmentTotalFields;
            },
            beforeSave: (request: any) => {
            request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
            request.RecType = 'A';
            request.AvailFor = 'S';
            request.pagesize = itemPageSize;
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('S');
                $browse.attr('data-pagesize', itemPageSize);
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateInvoiceItemGridTotals($form, 'salesadjustment', dt.Totals, true);
            },
        });
        // ----------
        //const $invoiceItemGridAdjustmentParts = $form.find('.partsadjustment div[data-grid="InvoiceItemGrid"]');
        //const $invoiceItemGridAdjustmentPartsControl = FwBrowse.loadGridFromTemplate('InvoiceItemGrid');
        //$invoiceItemGridAdjustmentPartsControl.attr('data-pagesize', itemPageSize);

        //$invoiceItemGridAdjustmentParts.empty().append($invoiceItemGridAdjustmentPartsControl);
        //$invoiceItemGridAdjustmentParts.addClass('P')
        //$invoiceItemGridAdjustmentPartsControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId'),
        //        pagesize: itemPageSize,
        //        RecType: 'A',
        //        AvailFor: 'P'
        //    };
        //    request.totalfields = itemAdjustmentTotalFields;
        //});
        //$invoiceItemGridAdjustmentPartsControl.data('beforesave', request => {
        //    request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
        //    request.RecType = 'A';
        //    request.AvailFor = 'P';
        //    request.pagesize = itemPageSize;
        //});
        //FwBrowse.addEventHandler($invoiceItemGridAdjustmentPartsControl, 'afterdatabindcallback', ($invoiceItemGridAdjustmentPartsControl, dt) => {
        //    this.calculateInvoiceItemGridTotals($form, 'partsadjustment', dt.Totals, true);
        //});
        //FwBrowse.init($invoiceItemGridAdjustmentPartsControl);
        //FwBrowse.renderRuntimeHtml($invoiceItemGridAdjustmentPartsControl);

        FwBrowse.renderGrid({
            nameGrid: 'InvoiceItemGrid',
            gridSelector: '.salesadjustment div[data-grid="InvoiceItemGrid"]',
            gridSecurityId: '5xgHiF8dduf',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InvoiceId: FwFormField.getValueByDataField($form, 'BillingWorksheetId'),
                    pagesize: itemPageSize,
                    RecType: 'A',
                    AvailFor: 'P'
                };
                request.totalfields = itemAdjustmentTotalFields;
            },
            beforeSave: (request: any) => {
                    request.InvoiceId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
                    request.RecType = 'A';
                    request.AvailFor = 'P';
                    request.pagesize = itemPageSize;
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('P');
                $browse.attr('data-pagesize', itemPageSize);
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateInvoiceItemGridTotals($form, 'partsadjustment', dt.Totals, true);
            },
        });
        // ----------
        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
        jQuery($form.find('.salesgrid .valtype')).attr('data-validationname', 'SalesInventoryValidation');
        jQuery($form.find('.laborgrid .valtype')).attr('data-validationname', 'LaborRateValidation');
        jQuery($form.find('.miscgrid .valtype')).attr('data-validationname', 'MiscRateValidation');
        jQuery($form.find('.rentalsalegrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'OrderId':
                request.uniqueids.BillingCycleType = 'ONDEMAND';
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateorder`);
                break;
            case 'DepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedepartment`);
                break;
            case 'AgentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateagent`);
                break;
            case 'ProjectManagerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateprojectmanager`);
                break;
            case 'OutsideSalesRepresentativeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateoutsidesalesrepresentative`);
                break;
            case 'PaymentTermsId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepaymentterms`);
                break;
            case 'PaymentTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepaymenttype`);
                break;
            case 'CurrencyId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecurrency`);
                break;
            case 'TaxOptionId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatetaxoption`);
                break;
        }
    }
    loadAudit($form: JQuery): void {
        const uniqueid = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
        FwModule.loadAudit($form, uniqueid);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery, response): void {
        // Disbles form for certain statuses. Maintain position under 'IsStandAloneInvoice' condition since status overrides
        const status = FwFormField.getValueByDataField($form, 'Status');
        if (status === 'CLOSED' || status === 'PROCESSED' || status === 'VOID') {
            FwModule.setFormReadOnly($form);
        }
        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"][data-enabled!="false"]', e => {
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

        this.dynamicColumns($form);
        this.applyTaxOptions($form, response);
    }
    //----------------------------------------------------------------------------------------------
    applyTaxOptions($form: JQuery, response: any) {
        const $taxFields = $form.find('[data-totalfield="Tax"]');
        const tax1Name = response.Tax1Name;
        const tax2Name = response.Tax2Name;

        const updateCaption = ($fields, taxName, count) => {
            for (let i = 0; i < $fields.length; i++) {
                const $field = jQuery($fields[i]);
                const taxType = $field.attr('data-taxtype');
                if (typeof taxType != 'undefined') {
                    const taxRateName = taxType + 'TaxRate' + count;
                    const taxRatePercentage = response[taxRateName];
                    if (typeof taxRatePercentage == 'number') {
                        const caption = taxName + ` (${taxRatePercentage.toFixed(3) + '%'})`;
                        $field.find('.fwformfield-caption').text(caption);
                    }
                }
            }

            const $billingTabTaxFields = $form.find(`[data-datafield="RentalTaxRate${count}"], [data-datafield="SalesTaxRate${count}"], [data-datafield="LaborTaxRate${count}"]`);
            for (let i = 0; i < $billingTabTaxFields.length; i++) {
                const $field = jQuery($billingTabTaxFields[i]);
                const taxType = $field.attr('data-taxtype');
                if (typeof taxType != 'undefined') {
                    const newCaption = taxType + ' ' + taxName;
                    $field.find('.fwformfield-caption').text(newCaption);
                }
                $field.show();
            }
        }

        if (tax1Name != "") {
            updateCaption($taxFields, tax1Name, 1);
        }

        const $tax2Fields = $form.find('[data-totalfield="Tax2"]');
        if (tax2Name != "") {
            $tax2Fields.show();
            updateCaption($tax2Fields, tax2Name, 2);
        } else {
            $tax2Fields.hide();
            $form.find(`[data-datafield="RentalTaxRate2"], [data-datafield="SalesTaxRate2"], [data-datafield="LaborTaxRate2"]`).hide();
        }
    }
    //----------------------------------------------------------------------------------------------
    approveOrUnaproveWorksheet($form: any, indicator: string) {
        const billingWorksheetId = FwFormField.getValueByDataField($form, 'BillingWorksheetId');
        if (indicator === 'approve') {
            FwAppData.apiMethod(true, 'POST', `api/v1/billingworksheet/${billingWorksheetId}/approve`, null, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success === true) {
                    FwModule.refreshForm($form);
                } else {
                    FwNotification.renderNotification('WARNING', response.msg);
                }
            }, null, $form);
        } else if (indicator === 'unapprove') {
            FwAppData.apiMethod(true, 'POST', `api/v1/billingworksheet/${billingWorksheetId}/unapprove`, null, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success === true) {
                    FwModule.refreshForm($form);
                } else {
                    FwNotification.renderNotification('WARNING', response.msg);
                }
            }, null, $form);
        }
    }
    //----------------------------------------------------------------------------------------------
    browseApproveOrUnapprove($browse: any, indicator: string) {
        const billingWorksheetId = $browse.find('.selected [data-browsedatafield="BillingWorksheetId"]').attr('data-originalvalue');
        if (indicator === 'approve') {
            if (typeof billingWorksheetId !== 'undefined') {
                FwAppData.apiMethod(true, 'POST', `api/v1/billingworksheet/${billingWorksheetId}/approve`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.success === true) {
                        FwBrowse.search($browse);
                    } else {
                        FwNotification.renderNotification('WARNING', response.msg);
                    }
                }, null, $browse);
            } else {
                FwNotification.renderNotification('WARNING', 'No Worksheet Selected');
            }
        } else if (indicator === 'unapprove') {
            if (typeof billingWorksheetId !== 'undefined') {
                FwAppData.apiMethod(true, 'POST', `api/v1/billingworksheet/${billingWorksheetId}/unapprove`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.success === true) {
                        FwBrowse.search($browse);
                    } else {
                        FwNotification.renderNotification('WARNING', response.msg);
                    }
                }, null, $browse);
            } else {
                FwNotification.renderNotification('WARNING', 'No Worksheet Selected');
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    dynamicColumns($form: JQuery): void {
        const invoiceType = FwFormField.getValueByDataField($form, 'InvoiceType');

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
        if (invoiceType === 'CREDIT') { rentalShowFields.push('Adjustment'); }
        const hiddenRentals: Array<string> = fieldNames.filter(function (field) {
            return !this.has(field)
        }, new Set(rentalShowFields))
        for (let i = 0; i < hiddenRentals.length; i++) {
            jQuery($rentalGrid.find(`[data-mappedfield="${hiddenRentals[i]}"]`)).parent().hide();
        }
        // ----------
        const salesShowFields: Array<string> = ["OrderNumber", "ICode", "Description", "Quantity", "Unit", "Cost", "Rate", "DiscountPercent", "DiscountAmount", "Extended", "Taxable"];
        if (invoiceType === 'CREDIT') { salesShowFields.push('Adjustment'); }
        const hiddenSales = fieldNames.filter(function (field) {
            return !this.has(field)
        }, new Set(salesShowFields))
        const $salesGrid = $form.find('.salesgrid [data-name="InvoiceItemGrid"]');
        for (let i = 0; i < hiddenSales.length; i++) {
            jQuery($salesGrid.find(`[data-mappedfield="${hiddenSales[i]}"]`)).parent().hide();
        }
        // ----------
        const laborShowFields: Array<string> = ["OrderNumber", "ICode", "Description", "Quantity", "FromDate", "FromTime", "ToDate", "ToTime", "Days", "Unit", "Rate", "Cost", "DiscountAmount", "Extended", "Taxable"];
        if (invoiceType === 'CREDIT') { laborShowFields.push('Adjustment'); }
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
        if (invoiceType === 'CREDIT') { rentalSaleShowFields.push('Adjustment'); }
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
    }
    //----------------------------------------------------------------------------------------------
    calculateInvoiceItemGridTotals($form: JQuery, gridType: string, totals?, isAdjustment?: boolean): void {
        if (isAdjustment) {
            const subTotal = totals.LineTotal;
            const salesTax = totals.Tax;
            const salesTax2 = totals.Tax2;
            const total = totals.LineTotalWithTax;

            $form.find(`.${gridType}-totals [data-totalfield="SubTotal"] input`).val(subTotal);
            $form.find(`.${gridType}-totals [data-totalfield="Tax"] input`).val(salesTax);
            $form.find(`.${gridType}-totals [data-totalfield="Tax2"] input`).val(salesTax2);
            $form.find(`.${gridType}-totals [data-totalfield="Total"] input`).val(total);
        } else {
            const grossTotal = totals.LineTotalBeforeDiscount;
            const discount = totals.DiscountAmount;
            const subTotal = totals.LineTotal;
            const salesTax = totals.Tax;
            const salesTax2 = totals.Tax2;
            const total = totals.LineTotalWithTax;

            $form.find(`.${gridType}-totals [data-totalfield="GrossTotal"] input`).val(grossTotal);
            $form.find(`.${gridType}-totals [data-totalfield="Discount"] input`).val(discount);
            $form.find(`.${gridType}-totals [data-totalfield="SubTotal"] input`).val(subTotal);
            $form.find(`.${gridType}-totals [data-totalfield="Tax"] input`).val(salesTax);
            $form.find(`.${gridType}-totals [data-totalfield="Tax2"] input`).val(salesTax2);
            $form.find(`.${gridType}-totals [data-totalfield="Total"] input`).val(total);
        }
    }
    //----------------------------------------------------------------------------------------------
    //openEmailHistoryBrowse($form) {
    //    const $browse = EmailHistoryController.openBrowse();

    //    $browse.data('ondatabind', function (request) {
    //        request.uniqueids = {
    //            RelatedToId: $form.find('[data-datafield="InvoiceId"] input.fwformfield-value').val()
    //        }
    //    });

    //    return $browse;
    //}
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
        $form.find('div[data-type="button"].update-worksheet').click(e => {
            this.updateWorksheet($form);
        });
        $form.find('div[data-datafield="OrderId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="WorksheetDescription"]', $tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="DealId"]', $tr.find('.field[data-browsedatafield="DealId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="Deal"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="DealNumber"]', $tr.find('.field[data-browsedatafield="DealNumber"]').attr('data-originalvalue'));
        });
    }
    //----------------------------------------------------------------------------------------------
    updateWorksheet($form: JQuery) {

    }
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
    }
}
//----------------------------------------------------------------------------------------------


var BillingWorksheetController = new BillingWorksheet();