//routes.push({ pattern: /^module\/purchaseorder$/, action: function (match: RegExpExecArray) { return PurchaseOrderController.getModuleScreen(); } });
//routes.push({ pattern: /^module\/purchaseorder\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return PurchaseOrderController.getModuleScreen(filter); } });

//----------------------------------------------------------------------------------------------
class PurchaseOrder implements IModule {
    Module: string = 'PurchaseOrder';
    apiurl: string = 'api/v1/purchaseorder';
    caption: string = Constants.Modules.Agent.children.PurchaseOrder.caption;
    nav: string = Constants.Modules.Agent.children.PurchaseOrder.nav;
    id: string = Constants.Modules.Agent.children.PurchaseOrder.id;
    DefaultPurchasePoType: string;
    DefaultPurchasePoTypeId: string;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    CachedPurchaseOrderTypes: any = {};
    totalFields = ['WeeklyExtendedNoDiscount', 'WeeklyDiscountAmount', 'WeeklyExtended', 'WeeklyTax1', 'WeeklyTax2', 'WeeklyTax', 'WeeklyTotal', 'MonthlyExtendedNoDiscount', 'MonthlyDiscountAmount', 'MonthlyExtended', 'MonthlyTax', 'MonthlyTax1', 'MonthlyTax2', 'MonthlyTotal', 'PeriodExtendedNoDiscount', 'PeriodDiscountAmount', 'PeriodExtended', 'PeriodTax', 'PeriodTax1', 'PeriodTax2', 'PeriodTotal',]
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasInactive = false;
        FwMenu.addBrowseMenuButtons(options);

        const $all = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $new = FwMenu.generateDropDownViewBtn('New', false, "NEW");
        const $open = FwMenu.generateDropDownViewBtn('Open', false, "OPEN");
        const $received = FwMenu.generateDropDownViewBtn('Received', false, "RECEIVED");
        const $complete = FwMenu.generateDropDownViewBtn('Complete', false, "COMPLETE");
        const $void = FwMenu.generateDropDownViewBtn('Void', false, "VOID");
        const $closed = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");

        const viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $new, $open, $received, $complete, $void, $closed);
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

        // Agent DropDownMenu
        const $allAgents = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $myAgent = FwMenu.generateDropDownViewBtn('My Agent Purchase Orders', false, "AGENT");

        const viewAgentItems: Array<JQuery> = [];
        viewAgentItems.push($allAgents, $myAgent);
        FwMenu.addViewBtn(options.$menu, 'My', viewAgentItems, true, "My");
    }
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Assign Barcodes', '7UU96BApz2Va', (e: JQuery.ClickEvent) => {
            try {
                this.assignBarCodes(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Purchase Order Status', 'Buwl6WTPrOMk', (e: JQuery.ClickEvent) => {
            try {
                this.purchaseOrderStatus(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Receive From Vendor', 'MtgBxCKWVl7m', (e: JQuery.ClickEvent) => {
            try {
                this.receiveFromVendor(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Return To Vendor', 'cCxoTvTCDTcm', (e: JQuery.ClickEvent) => {
            try {
                this.returnToVendor(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        //no-security
        FwMenu.addSubMenuItem(options.$groupOptions, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
            try {
                this.search(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Toggle Close', 'rmIBsGJIEjAZ', (e: JQuery.ClickEvent) => {
            try {
                this.toggleClosePurchaseOrder(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Void', 'u5eAwyixomSFN', (e: JQuery.ClickEvent) => {
            try {
                this.voidPO(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen(): IModuleScreen {
        const screen: IModuleScreen = {};
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
        let $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        const location = JSON.parse(sessionStorage.getItem('location'));

        FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
            if (dt.Rows[rowIndex][dt.ColumnIndex['Status']] === 'VOID') {
                $tr.css('color', '#aaaaaa');
            }
        });

        FwAppData.apiMethod(true, 'GET', `${this.apiurl}/officelocation/${location.locationid}`, null, FwServices.defaultTimeout, response => {
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
    openForm(mode: string, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        FwFormField.disable($form.find('[data-datafield="SubRent"]'));
        FwFormField.disable($form.find('[data-datafield="SubSale"]'));
        FwFormField.disable($form.find('[data-datafield="SubLabor"]'));
        FwFormField.disable($form.find('[data-datafield="SubMiscellaneous"]'));
        FwFormField.disable($form.find('[data-datafield="SubVehicle"]'));
        //FwTabs.hideTab($form.find('.vendorinvoicetab'));
        //FwTabs.hideTab($form.find('.contracttab'));
        //FwTabs.hideTab($form.find('.emailhistorytab'));

        let nodeActivity = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'hb52dbhX1mNLZ');
        if (nodeActivity !== undefined && nodeActivity.properties.visible === 'T') {
            FwTabs.showTab($form.find('.activitytab'));
        } else {
            //FwTabs.hideTab($form.find('.activitytab'));
        }

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
            FwFormField.setValue($form, 'div[data-datafield="CurrencyId"]', office.defaultcurrencyid, office.defaultcurrencycode);
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
            FwFormField.setValue($form, 'div[data-datafield="PoTypeId"]', this.DefaultPurchasePoTypeId, this.DefaultPurchasePoType);
        };

        let nodeEmailHistory = FwApplicationTree.getNodeById(FwApplicationTree.tree, '3XHEm3Q8WSD8z');
        if (nodeEmailHistory !== undefined && nodeEmailHistory.properties.visible === 'T') {
            FwTabs.showTab($form.find('.emailhistorytab'));
            const $emailHistorySubModuleBrowse = this.openEmailHistoryBrowse($form);
            $form.find('.emailhistory-page').append($emailHistorySubModuleBrowse);
        }

        FwFormField.disable($form.find('[data-datafield="RentalTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="SalesTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="LaborTaxRate1"]'));

        //Toggle Buttons - SubRental tab - Sub-Rental totals
        FwFormField.loadItems($form.find('div[data-datafield="totalTypeSubRental"]'), [
            { value: 'W', caption: 'Weekly' },
            { value: 'M', caption: 'Monthly' },
            { value: 'P', caption: 'Period', checked: 'checked' }
        ]);
        FwFormField.loadItems($form.find('div[data-datafield="ReceiveDeliveryDeliveryType"]'), [
            { value: 'DELIVER', text: 'Vendor Deliver' },
            { value: 'SHIP', text: 'Vendor Ship' },
            { value: 'PICK UP', text: 'Pick Up' }
        ], true);
        FwFormField.loadItems($form.find('div[data-datafield="ReceiveDeliveryAddressType"]'), [
            { value: 'DEAL', caption: 'Deal' },
            { value: 'VENUE', caption: 'Venue' },
            { value: 'WAREHOUSE', caption: 'Warehouse' },
            { value: 'OTHER', caption: 'Other' }
        ]);

        FwFormField.loadItems($form.find('div[data-datafield="ReceiveDeliveryOnlineOrderStatus"]'), [
            { value: 'PARTIAL', text: 'Partial' },
            { value: 'COMPLETE', text: 'Complete' }
        ], true);

        FwFormField.loadItems($form.find('div[data-datafield="ReturnDeliveryDeliveryType"]'), [
            { value: 'DELIVER', text: 'Deliver' },
            { value: 'SHIP', text: 'Ship' },
            { value: 'PICK UP', text: 'Vendor Pick Up' }
        ], true);

        FwFormField.loadItems($form.find('div[data-datafield="ReturnDeliveryAddressType"]'), [
            { value: 'DEAL', caption: 'Deal' },
            { value: 'VENUE', caption: 'Venue' },
            { value: 'WAREHOUSE', caption: 'Warehouse' },
            { value: 'OTHER', caption: 'Other' }
        ], true);

        //Toggle Buttons - Summary tab
        FwFormField.loadItems($form.find('div[data-datafield="totalTypeProfitLoss"]'), [
            { value: 'W', caption: 'Weekly' },
            { value: 'M', caption: 'Monthly' },
            { value: 'P', caption: 'Period', checked: 'checked' }
        ]);

        FwFormField.setValue($form, 'div[data-datafield="ShowShipping"]', true);  //justin hoffman 03/12/2020 - this is temporary until the Shipping tab is updated

        this.events($form);
        this.activityCheckboxEvents($form, mode);
        this.renderPrintButton($form);
        this.renderSearchButton($form);
        this.applyRateType($form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="PurchaseOrderId"] input').val(uniqueids.PurchaseOrderId);

        FwModule.loadForm(this.Module, $form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    renderPrintButton($form: any) {
        var $print = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'Print');
        $print.prepend('<i class="material-icons">print</i>');
        $print.on('click', () => {
            this.printPurchaseOrder($form);
        });
    }
    //----------------------------------------------------------------------------------------------
    printPurchaseOrder($form: any) {
        try {
            const purchaseOrderNumber = FwFormField.getValue($form, `div[data-datafield="PurchaseOrderNumber"]`);
            const purchaseOrderId = FwFormField.getValue($form, `div[data-datafield="PurchaseOrderId"]`);
            const vendorId = FwFormField.getValue($form, `div[data-datafield="VendorId"]`);
            const $report = PurchaseOrderReportController.openForm();
            FwModule.openSubModuleTab($form, $report);

            FwFormField.setValue($report, `div[data-datafield="PurchaseOrderId"]`, purchaseOrderId, purchaseOrderNumber);
            FwFormField.setValue($report, `div[data-datafield="CompanyIdField"]`, vendorId);
            const $tab = FwTabs.getTabByElement($report);
            $tab.find('.caption').html(`Print Purchase Order`);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
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
        FwBrowse.renderGrid({
            nameGrid: 'OrderStatusHistoryGrid',
            gridSecurityId: 'lATsdnAx7B4s',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
                }
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.rentalgrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.quikSearch(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Template', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyTemplate(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Line-Items', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyLineItems(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });

                FwMenu.addSubMenuItem($optionsgroup, 'Bold / Unbold Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.boldUnbold(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Mute / Unmute Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.muteUnmute(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid2')
                FwMenu.addSubMenuItem($viewgroup, 'Color Legend', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.colorLegend(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
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
                $fwgrid.addClass('purchase');
                $browse.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
                $browse.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
                $browse.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
                $browse.find('div[data-datafield="PeriodExtended"]').attr('data-formreadonly', 'true');
                $browse.find('[data-datafield="Description"]').attr({ 'data-datatype': 'validation', 'data-validationpeek': 'false' });
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateOrderItemGridTotals($form, 'rental', dt.Totals);
                const rentalItems = $form.find('.rentalgrid tbody').children();
                rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
            },
            onOverrideNotesTemplate: ($field, controlhtml, $confirmation, $browse, $tr, $ok) => {
                OrderItemGridController.addPrintNotes($field, controlhtml, $confirmation, $browse, $tr, $ok);
            },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.salesgrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.quikSearch(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Template', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyTemplate(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Line-Items', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyLineItems(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });

                FwMenu.addSubMenuItem($optionsgroup, 'Bold / Unbold Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.boldUnbold(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Mute / Unmute Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.muteUnmute(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid2')
                FwMenu.addSubMenuItem($viewgroup, 'Color Legend', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.colorLegend(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                    RecType: 'S'
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
                request.RecType = 'S';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('S');
                $fwgrid.addClass('purchase');
                $browse.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
                $browse.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
                $browse.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
                $browse.find('[data-datafield="Description"]').attr({ 'data-datatype': 'validation', 'data-validationpeek': 'false' });
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateOrderItemGridTotals($form, 'sales', dt.Totals);
                const salesItems = $form.find('.salesgrid tbody').children();
                salesItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Sales"]')) : FwFormField.enable($form.find('[data-datafield="Sales"]'));
            },
            onOverrideNotesTemplate: ($field, controlhtml, $confirmation, $browse, $tr, $ok) => {
                OrderItemGridController.addPrintNotes($field, controlhtml, $confirmation, $browse, $tr, $ok);
            },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.partsgrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.quikSearch(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Template', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyTemplate(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Line-Items', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyLineItems(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });

                FwMenu.addSubMenuItem($optionsgroup, 'Bold / Unbold Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.boldUnbold(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Mute / Unmute Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.muteUnmute(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid2')
                FwMenu.addSubMenuItem($viewgroup, 'Color Legend', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.colorLegend(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                    RecType: 'P'
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
                request.RecType = 'P';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('P');
                $fwgrid.addClass('purchase');
                $browse.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
                $browse.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
                $browse.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
                $browse.find('[data-datafield="Description"]').attr({ 'data-datatype': 'validation', 'data-validationpeek': 'false' });
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateOrderItemGridTotals($form, 'parts', dt.Totals);
                const partItems = $form.find('.partsgrid tbody').children();
                partItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Parts"]')) : FwFormField.enable($form.find('[data-datafield="Parts"]'));
            },
            onOverrideNotesTemplate: ($field, controlhtml, $confirmation, $browse, $tr, $ok) => {
                OrderItemGridController.addPrintNotes($field, controlhtml, $confirmation, $browse, $tr, $ok);
            },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.laborgrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.quikSearch(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Template', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyTemplate(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Line-Items', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyLineItems(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });

                FwMenu.addSubMenuItem($optionsgroup, 'Bold / Unbold Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.boldUnbold(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Mute / Unmute Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.muteUnmute(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid2')
                FwMenu.addSubMenuItem($viewgroup, 'Color Legend', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.colorLegend(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                    RecType: 'L'
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
                request.RecType = 'L';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('L');
                $fwgrid.addClass('purchase');
                $browse.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
                $browse.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
                $browse.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
                $browse.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
                $browse.find('[data-datafield="Description"]').attr({ 'data-datatype': 'validation', 'data-validationpeek': 'false' });
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateOrderItemGridTotals($form, 'labor', dt.Totals);
                const laborItems = $form.find('.laborgrid tbody').children();
                laborItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Labor"]')) : FwFormField.enable($form.find('[data-datafield="Labor"]'));
            },
            onOverrideNotesTemplate: ($field, controlhtml, $confirmation, $browse, $tr, $ok) => {
                OrderItemGridController.addPrintNotes($field, controlhtml, $confirmation, $browse, $tr, $ok);
            },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.miscgrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.quikSearch(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Template', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyTemplate(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Line-Items', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyLineItems(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });

                FwMenu.addSubMenuItem($optionsgroup, 'Bold / Unbold Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.boldUnbold(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Mute / Unmute Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.muteUnmute(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid2')
                FwMenu.addSubMenuItem($viewgroup, 'Color Legend', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.colorLegend(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                    RecType: 'M'
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
                request.RecType = 'M';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('M');
                $fwgrid.addClass('purchase');
                $browse.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
                $browse.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
                $browse.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
                $browse.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
                $browse.find('[data-datafield="Description"]').attr({ 'data-datatype': 'validation', 'data-validationpeek': 'false' });
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateOrderItemGridTotals($form, 'misc', dt.Totals);
                const miscItems = $form.find('.miscgrid tbody').children();
                miscItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Miscellaneous"]')) : FwFormField.enable($form.find('[data-datafield="Miscellaneous"]'));
            },
            onOverrideNotesTemplate: ($field, controlhtml, $confirmation, $browse, $tr, $ok) => {
                OrderItemGridController.addPrintNotes($field, controlhtml, $confirmation, $browse, $tr, $ok);
            },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.subrentalgrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.quikSearch(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Bold / Unbold Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.boldUnbold(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Mute / Unmute Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.muteUnmute(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid2')
                FwMenu.addSubMenuItem($viewgroup, 'Color Legend', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.colorLegend(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                    RecType: 'R',
                    Subs: true,
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
                request.RecType = 'R';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('R');
                $fwgrid.addClass('sub');
                $browse.find('.suborder').attr('data-visible', 'true');
                $browse.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
                $browse.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
                $browse.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                $browse.data('totals', dt.Totals);
                this.calculateOrderItemGridTotals($form, 'subrental', dt.Totals);
                const subrentItems = $form.find('.subrentalgrid tbody').children();
                subrentItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubRent"]')) : FwFormField.enable($form.find('[data-datafield="SubRent"]'));
            },
            onOverrideNotesTemplate: ($field, controlhtml, $confirmation, $browse, $tr, $ok) => {
                OrderItemGridController.addPrintNotes($field, controlhtml, $confirmation, $browse, $tr, $ok);
            },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.subsalesgrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.quikSearch(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Bold / Unbold Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.boldUnbold(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Mute / Unmute Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.muteUnmute(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid2')
                FwMenu.addSubMenuItem($viewgroup, 'Color Legend', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.colorLegend(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                    RecType: 'S',
                    Subs: true,
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
                request.RecType = 'S';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('S');
                $fwgrid.addClass('sub');
                $browse.find('.suborder').attr('data-visible', 'true');
                $browse.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
                $browse.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
                $browse.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                $browse.data('totals', dt.Totals);
                this.calculateOrderItemGridTotals($form, 'subsales', dt.Totals);
                const subsalesItems = $form.find('.subsalesgrid tbody').children();
                subsalesItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubSale"]')) : FwFormField.enable($form.find('[data-datafield="SubSale"]'));
            },
            onOverrideNotesTemplate: ($field, controlhtml, $confirmation, $browse, $tr, $ok) => {
                OrderItemGridController.addPrintNotes($field, controlhtml, $confirmation, $browse, $tr, $ok);
            },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.sublaborgrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')

                FwMenu.addSubMenuItem($optionsgroup, 'Bold / Unbold Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.boldUnbold(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Mute / Unmute Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.muteUnmute(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid2')
                FwMenu.addSubMenuItem($viewgroup, 'Color Legend', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.colorLegend(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                    RecType: 'L',
                    Subs: true,
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
                request.RecType = 'L';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('L');
                $fwgrid.addClass('sub');
                $browse.find('.suborder').attr('data-visible', 'true');
                $browse.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
                $browse.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
                $browse.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
                $browse.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                $browse.data('totals', dt.Totals);
                this.calculateOrderItemGridTotals($form, 'sublabor', dt.Totals);
                const sublaborItems = $form.find('.sublaborgrid tbody').children();
                sublaborItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubLabor"]')) : FwFormField.enable($form.find('[data-datafield="SubLabor"]'));
            },
            onOverrideNotesTemplate: ($field, controlhtml, $confirmation, $browse, $tr, $ok) => {
                OrderItemGridController.addPrintNotes($field, controlhtml, $confirmation, $browse, $tr, $ok);
            },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.submiscgrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')

                FwMenu.addSubMenuItem($optionsgroup, 'Bold / Unbold Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.boldUnbold(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Mute / Unmute Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.muteUnmute(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid2')
                FwMenu.addSubMenuItem($viewgroup, 'Color Legend', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.colorLegend(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                    RecType: 'M',
                    Subs: true,
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
                request.RecType = 'M';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('M');
                $fwgrid.addClass('sub');
                $browse.find('.suborder').attr('data-visible', 'true');
                $browse.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
                $browse.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
                $browse.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
                $browse.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                $browse.data('totals', dt.Totals);
                this.calculateOrderItemGridTotals($form, 'submisc', dt.Totals);
                const submiscItems = $form.find('.submiscgrid tbody').children();
                submiscItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="SubMisc"]')) : FwFormField.enable($form.find('[data-datafield="SubMisc"]'));
            },
            onOverrideNotesTemplate: ($field, controlhtml, $confirmation, $browse, $tr, $ok) => {
                OrderItemGridController.addPrintNotes($field, controlhtml, $confirmation, $browse, $tr, $ok);
            },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderContactGrid',
            gridSecurityId: 'B9CzDEmYe1Zf',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
                };
            },
            beforeSave: (request: any) => {
                let companyId = FwFormField.getValueByDataField($form, 'VendorId');
                if (companyId === '') {
                    companyId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
                }
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
                request.CompanyId = companyId;
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $browse.data('deletewithnoids', OrderContactGridController.deleteWithNoIds);
                $browse.find('div[data-datafield="IsOrderedBy"]').attr('data-caption', 'Ordered From');
            },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'PurchaseOrderPaymentScheduleGrid',
            gridSecurityId: 'NhVLHR4uMbkRQ',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasEdit = false;
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
                }
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderNoteGrid',
            gridSecurityId: '8aq0E3nK2upt',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                };
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'ActivityGrid',
            gridSecurityId: 'hb52dbhX1mNLZ',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`),
                    ShowShipping: FwFormField.getValueByDataField($form, 'ShowShipping')
                };
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
            },
        });
        // ----------
        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
        jQuery($form.find('.salesgrid .valtype')).attr('data-validationname', 'SalesInventoryValidation');
        jQuery($form.find('.laborgrid .valtype')).attr('data-validationname', 'LaborRateValidation');
        jQuery($form.find('.miscgrid .valtype')).attr('data-validationname', 'MiscRateValidation');
        jQuery($form.find('.partsgrid .valtype')).attr('data-validationname', 'PartsInventoryValidation');
        jQuery($form.find('.rentalsalegrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
    };
    //----------------------------------------------------------------------------------------------
    loadAudit($form: JQuery): void {
        const uniqueid = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        FwModule.loadAudit($form, uniqueid);
    };
    //----------------------------------------------------------------------------------------------
    applyPurchaseOrderTypeToForm($form) {
        let self = this;

        // find all the grids on the form
        let $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        let $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        let $partsGrid = $form.find('.partsgrid [data-name="OrderItemGrid"]');
        let $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        let $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        let $subRentalGrid = $form.find('.subrentalgrid [data-name="OrderItemGrid"]');
        let $subSalesGrid = $form.find('.subsalesgrid [data-name="OrderItemGrid"]');
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

        // Documents Grid - Need to put this here, because renderGrids is called from openForm and uniqueid is not available yet on the form
        // Moved documents grid from loadForm to afterLoad so it loads on new records. - Jason H 04/20/20
        const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        FwAppDocumentGrid.renderGrid({
            $form: $form,
            caption: 'Documents',
            nameGrid: 'PurchaseOrderDocumentGrid',
            getBaseApiUrl: () => {
                return `${this.apiurl}/${purchaseOrderId}/document`;
            },
            gridSecurityId: 'OCGVS960nEwc',
            moduleSecurityId: this.id,
            parentFormDataFields: 'PurchaseOrderId',
            uniqueid1Name: 'PurchaseOrderId',
            getUniqueid1Value: () => purchaseOrderId,
            uniqueid2Name: '',
            getUniqueid2Value: () => ''
        });

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

            if (purchaseOrderTypeData.hiddenSubRentals.indexOf('DaysPerWeek') === -1 && rateType === 'DAILY') {
                $subRentalGrid.find('.dw').parent().show();
            }
        }
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery, response): void {
        this.applyPurchaseOrderTypeToForm($form);

        const status = FwFormField.getValueByDataField($form, 'Status');
        if (status === 'VOID' || status === 'CLOSED' || status === 'SNAPSHOT') {
            FwModule.setFormReadOnly($form);
            $form.find('.btn[data-securityid="searchbtn"]').addClass('disabled');

            OrderBaseController.disableOrderItemGridMenus($form);
        }

        const $toggleCloseOption = $form.find('.submenu-btn .caption:contains(Toggle Close)');
        if (status === 'CLOSED') {
            $toggleCloseOption.text('Re-Open Purchase Order');
        } else if (status === 'COMPLETE') {
            $toggleCloseOption.text('Close Purchase Order');
        } else {
            $toggleCloseOption.parents('.submenu-btn').hide();
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

        if (!isRental) { $form.find('.rentaltab').hide() }
        if (!isSales) { $form.find('.salestab').hide() }
        if (!isMisc) { $form.find('.misctab').hide() }
        if (!isLabor) { $form.find('.labortab').hide() }
        if (!isParts) { $form.find('.partstab').hide() }
        if (!isSubRent) { $form.find('.subrentaltab').hide() }
        if (!isSubSale) { $form.find('.subsalestab').hide() }
        if (!isRepair) { $form.find('.repairtab').hide() }
        if (!isSubMisc) { $form.find('.submisctab').hide() }
        if (!isSubLabor) { $form.find('.sublabortab').hide() }

        //summary section visibility
        isRental ? $form.find('.rental-pl').show() : $form.find('.rental-pl').hide();
        isSales ? $form.find('.sales-pl').show() : $form.find('.sales-pl').hide();
        isLabor ? $form.find('.labor-pl').show() : $form.find('.labor-pl').hide();
        isMisc ? $form.find('.misc-pl').show() : $form.find('.misc-pl').hide();
        isSubRent ? $form.find('.subrental-pl').show() : $form.find('.subrental-pl').hide();
        isSubSale ? $form.find('.subsale-pl').show() : $form.find('.subsale-pl').hide();
        isSubLabor ? $form.find('.sublabor-pl').show() : $form.find('.sublabor-pl').hide();
        isSubMisc ? $form.find('.submisc-pl').show() : $form.find('.submisc-pl').hide();

        if (!isMisc && !isLabor && !isSubRent && !isSubSale && !isSubMisc && !isSubLabor) $scheduleDateFields.hide();

        //Click Event on tabs to load grids/browses
        $form.find('.tabGridsLoaded[data-type="tab"]').removeClass('tabGridsLoaded');
        $form.on('click', '[data-type="tab"][data-enabled!="false"]', e => {
            const $tab = jQuery(e.currentTarget);
            const tabname = $tab.attr('id');
            const lastIndexOfTab = tabname.lastIndexOf('tab');  // for cases where "tab" is included in the name of the tab
            const tabpage = `${tabname.substring(0, lastIndexOfTab)}tabpage${tabname.substring(lastIndexOfTab + 3)}`;
            if ($tab.hasClass('profitlosstab')) {
                this.loadSummary($form);
            }
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
            if (!$tab.hasClass('activitytab')) {
                $tab.addClass('tabGridsLoaded');
            }
        });

        const $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        $orderItemGridRental.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
        const $orderItemGridSales = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        $orderItemGridSales.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
        const $orderItemGridPart = $form.find('.partsgrid [data-name="OrderItemGrid"]');
        $orderItemGridPart.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
        const $orderItemGridLabor = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        $orderItemGridLabor.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
        const $orderItemGridMisc = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        $orderItemGridMisc.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
        const $orderItemGridSubRent = $form.find('.subrentalgrid [data-name="OrderItemGrid"]');
        $orderItemGridSubRent.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
        const $orderItemGridSubSales = $form.find('.subsalesgrid [data-name="OrderItemGrid"]');
        $orderItemGridSubSales.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
        const $orderItemGridSubLabor = $form.find('.sublaborgrid [data-name="OrderItemGrid"]');
        $orderItemGridSubLabor.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
        const $orderItemGridSubMisc = $form.find('.submiscgrid [data-name="OrderItemGrid"]');
        $orderItemGridSubMisc.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();

        //these are the "Search" options, which is now available in the PO header bar
        //$orderItemGridSubRent.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();
        //$orderItemGridSubSales.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();
        //$orderItemGridSubLabor.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();
        //$orderItemGridSubMisc.find('.submenu-btn[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"]').hide();

        $orderItemGridSubRent.find('.buttonbar').hide();
        $orderItemGridSubSales.find('.buttonbar').hide();
        $orderItemGridSubLabor.find('.buttonbar').hide();
        $orderItemGridSubMisc.find('.buttonbar').hide();

        // total type radio on sub-rental tab
        $form.find(`[data-datafield="totalTypeSubRental"]`).on('change', e => { // required in afterLoad since subrental grid is not allowed on NEW and fields are not visible yet in openForm
            const $target = jQuery(e.currentTarget);
            const gridType = 'subrental';
            const adjustmentsPeriod = $form.find(`.${gridType}AdjustmentsPeriod`);
            const adjustmentsWeekly = $form.find(`.${gridType}AdjustmentsWeekly`);
            const adjustmentsMonthly = $form.find(`.${gridType}AdjustmentsMonthly`);
            const totalTypeSubRental = FwFormField.getValueByDataField($form, 'totalTypeSubRental');
            switch (totalTypeSubRental) {
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
            const totals = $form.find(`.R[data-issubgrid="true"] [data-name="OrderItemGrid"]`).data('totals');
            this.calculateOrderItemGridTotals($form, gridType, totals);
        });
        this.disableCheckboxesOnLoad($form);

        let nodeVendorInvoice = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'Fq9aOe0yWfY');
        if (nodeVendorInvoice !== undefined && nodeVendorInvoice.properties.visible === 'T') {
            FwTabs.showTab($form.find('.vendorinvoicetab'));
            const $vendorInvoiceBrowse = this.openVendorInvoiceBrowse($form);
            $form.find('.vendorinvoice').empty().append($vendorInvoiceBrowse);

            //replace default click event on "New" button in Vendor Invoice sub-module to default PO
            $vendorInvoiceBrowse.find('div.btn[data-type="NewMenuBarButton"]').off().on('click', function () {
                if ($form.attr('data-mode') !== 'NEW') {
                    const $vendorInvoiceForm = VendorInvoiceController.openForm('NEW');
                    const poNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
                    const poId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
                    FwFormField.setValueByDataField($vendorInvoiceForm, 'PurchaseOrderId', poId, poNumber, true);
                    FwModule.openSubModuleTab($vendorInvoiceBrowse, $vendorInvoiceForm);
                } else {
                    FwNotification.renderNotification('WARNING', 'Save the record first.')
                }
            });
        }

        let nodeContract = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'Z8MlDQp7xOqu');
        if (nodeContract !== undefined && nodeContract.properties.visible === 'T') {
            FwTabs.showTab($form.find('.contracttab'));
            $form.find('.contractSubModule').empty().append(this.openContractBrowse($form));
        }

        const enableProjects = FwFormField.getValueByDataField($form, 'EnableProjects');
        enableProjects ? $form.find('.projecttab').show() : $form.find('.projecttab').hide();

        //hide/reset Currency change fields
        $form.find('[data-datafield="UpdateAllRatesToNewCurrency"], [data-datafield="ConfirmUpdateAllRatesToNewCurrency"]').hide();
        FwFormField.setValueByDataField($form, 'UpdateAllRatesToNewCurrency', false);
        FwFormField.setValueByDataField($form, 'ConfirmUpdateAllRatesToNewCurrency', '');

        this.renderScheduleDateAndTimeSection($form, response);
        this.applyTaxOptions($form, response);
        this.applyCurrencySymbolToTotalFields($form, response);
    };
    //----------------------------------------------------------------------------------------------
    applyCurrencySymbolToTotalFields($form: JQuery, response: any) {
        const $totalFields = $form.find('.totals[data-type="money"], .frame[data-type="money"]');

        $totalFields.each((index, element) => {
            let $fwformfield, currencySymbol;
            $fwformfield = jQuery(element);
            currencySymbol = response[$fwformfield.attr('data-currencysymbol')];
            if (typeof currencySymbol == 'undefined' || currencySymbol === '') {
                currencySymbol = '$';
            }

            $fwformfield.attr('data-currencysymboldisplay', currencySymbol);

            $fwformfield
                .find('.fwformfield-value')
                .inputmask('currency', {
                    prefix: currencySymbol + ' ',
                    placeholder: "0.00",
                    min: ((typeof $fwformfield.attr('data-minvalue') !== 'undefined') ? $fwformfield.attr('data-minvalue') : undefined),
                    max: ((typeof $fwformfield.attr('data-maxvalue') !== 'undefined') ? $fwformfield.attr('data-maxvalue') : undefined),
                    digits: ((typeof $fwformfield.attr('data-digits') !== 'undefined') ? $fwformfield.attr('data-digits') : 2),
                    radixPoint: '.',
                    groupSeparator: ','
                });
        });

        //add to grids
        const $grids = $form.find('[data-name="OrderItemGrid"]');

        $grids.each((index, element) => {
            let $grid, currencySymbol;
            $grid = jQuery(element);
            currencySymbol = response["CurrencySymbol"];
            if (typeof currencySymbol != 'undefined' && currencySymbol != '') {
                $grid.attr('data-currencysymboldisplay', currencySymbol);
            }
        });
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
        const rentalTab = $form.find('.rentaltab')
            , salesTab = $form.find('.salestab')
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
                    $form.find('.rental-pl').show();
                } else {
                    rentalTab.hide();
                    $form.find('.rental-pl').hide();
                }
            } else {
                if (jQuery(e.currentTarget).prop('checked')) {
                    rentalTab.show();
                    $form.find('.rental-pl').show();
                    FwFormField.disable($form.find('[data-datafield="RentalSale"]'));
                } else {
                    rentalTab.hide();
                    $form.find('.rental-pl').hide();
                    FwFormField.enable($form.find('[data-datafield="RentalSale"]'));
                }
            }
        });

        $form.find('[data-datafield="Sales"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                salesTab.show();
                $form.find('.sales-pl').show();
            } else {
                salesTab.hide();
                $form.find('.sales-pl').hide();
            }
        });
        $form.find('[data-datafield="Parts"] input').on('change', e => {
            jQuery(e.currentTarget).prop('checked') ? partsTab.show() : partsTab.hide();
        });
        $form.find('[data-datafield="Miscellaneous"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                miscTab.show();
                $form.find('.misc-pl').show();
                $scheduleDateFields.show();
            } else {
                miscTab.hide();
                $form.find('.misc-pl').hide();
            }
        });
        $form.find('[data-datafield="Labor"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                laborTab.show();
                $form.find('.labor-pl').show();
                $scheduleDateFields.show();
            }
            else {
                laborTab.hide();
                $form.find('.labor-pl').hide();
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
        if (FwFormField.getValueByDataField($form, 'HasRentalItem') && FwFormField.getValueByDataField($form, 'Rental')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Rental'));
        }
        if (FwFormField.getValueByDataField($form, 'HasSalesItem') && FwFormField.getValueByDataField($form, 'Sales')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Sales'));
        }
        if (FwFormField.getValueByDataField($form, 'HasMiscellaneousItem') && FwFormField.getValueByDataField($form, 'Miscellaneous')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Miscellaneous'));
        }
        if (FwFormField.getValueByDataField($form, 'HasLaborItem') && FwFormField.getValueByDataField($form, 'Labor')) {
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

                const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
                const $realConfirm = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);

                const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
                FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorder/void/${purchaseOrderId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Purchase Order Successfully Voided');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwModule.refreshForm($form);
                }, function onError(response) {
                    $yes.on('click', makeVoid);
                    $yes.text('Void');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                    FwModule.refreshForm($form);
                }, $realConfirm);
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
    calculateOrderItemGridTotals($form: any, gridType: string, totals?): void {
        let subTotal, discount, salesTax, salesTax2, grossTotal, total;
        const rateValue = $form.find(`.${gridType}grid .totalType input:checked`).val();

        //const extendedColumn: any = $form.find(`.${gridType}grid [data-browsedatafield="${rateType}Extended"]`);
        //const discountColumn: any = $form.find(`.${gridType}grid [data-browsedatafield="${rateType}DiscountAmount"]`);
        //const taxColumn: any = $form.find(`.${gridType}grid [data-browsedatafield="${rateType}Tax"]`);
        //const taxColumn2: any = $form.find(`.${gridType}grid [data-browsedatafield="${rateType}Tax2"]`);

        //for (let i = 1; i < extendedColumn.length; i++) {
        //    // Extended Column
        //    let inputValueFromExtended: any = +extendedColumn.eq(i).attr('data-originalvalue');
        //    extendedTotal = extendedTotal.plus(inputValueFromExtended);
        //    // DiscountAmount Column
        //    let inputValueFromDiscount: any = +discountColumn.eq(i).attr('data-originalvalue');
        //    discountTotal = discountTotal.plus(inputValueFromDiscount);
        //    // Tax Column
        //    let inputValueFromTax: any = +taxColumn.eq(i).attr('data-originalvalue');
        //    taxTotal = taxTotal.plus(inputValueFromTax);

        //    // Tax2 Column
        //    let inputValueFromTax2: any = +taxColumn2.eq(i).attr('data-originalvalue');
        //    taxTotal2 = taxTotal2.plus(inputValueFromTax2);
        //};

        //subTotal = extendedTotal.toFixed(2);
        //discount = discountTotal.toFixed(2);
        //salesTax = taxTotal.toFixed(2);
        //salesTax2 = taxTotal2.toFixed(2);
        //grossTotal = extendedTotal.plus(discountTotal).toFixed(2);
        //total = taxTotal.plus(extendedTotal).toFixed(2);
        switch (rateValue) {
            case 'W':
                subTotal = totals.WeeklyExtended;
                discount = totals.WeeklyDiscountAmount;
                salesTax = totals.WeeklyTax1;
                salesTax2 = totals.WeeklyTax2;
                grossTotal = totals.WeeklyExtendedNoDiscount;
                total = totals.WeeklyTotal;
                break;
            case 'P':
                subTotal = totals.PeriodExtended;
                discount = totals.PeriodDiscountAmount;
                salesTax = totals.PeriodTax1;
                salesTax2 = totals.PeriodTax2;
                grossTotal = totals.PeriodExtendedNoDiscount;
                total = totals.PeriodTotal;
                break;
            case 'M':
                subTotal = totals.MonthlyExtended;
                discount = totals.MonthlyDiscountAmount;
                salesTax = totals.MonthlyTax1;
                salesTax2 = totals.MonthlyTax2;
                grossTotal = totals.MonthlyExtendedNoDiscount;
                total = totals.MonthlyTotal;
                break;
            default:
                subTotal = totals.PeriodExtended;
                discount = totals.PeriodDiscountAmount;
                salesTax = totals.PeriodTax1;
                salesTax2 = totals.PeriodTax2;
                grossTotal = totals.PeriodExtendedNoDiscount;
                total = totals.PeriodTotal;
        }

        FwFormField.setValue2($form.find(`.${gridType}-totals [data-totalfield="SubTotal"]`), subTotal);
        FwFormField.setValue2($form.find(`.${gridType}-totals [data-totalfield="Discount"]`), discount);
        FwFormField.setValue2($form.find(`.${gridType}-totals [data-totalfield="Tax"]`), salesTax);
        FwFormField.setValue2($form.find(`.${gridType}-totals [data-totalfield="Tax2"]`), salesTax2);
        FwFormField.setValue2($form.find(`.${gridType}-totals [data-totalfield="GrossTotal"]`), grossTotal);
        FwFormField.setValue2($form.find(`.${gridType}-totals [data-totalfield="Total"]`), total);
    };
    //----------------------------------------------------------------------------------------------
    applyRateType($form: JQuery) {
        const subRentalDaysPerWeek = $form.find('div[data-datafield="SubRentalDaysPerWeek"]');
        const rateType = FwFormField.getValueByDataField($form, 'RateType');
        const weeklyType = $form.find('.togglebutton-item input[value="W"]').parent();
        const monthlyType = $form.find('.togglebutton-item input[value="M"]').parent();

        switch (rateType) {
            case 'DAILY':
                weeklyType.show();
                monthlyType.hide();
                subRentalDaysPerWeek.show();
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
                break;
            case '3WEEK':
                weeklyType.show();
                monthlyType.hide();
                subRentalDaysPerWeek.hide();
                break;
            case 'MONTHLY':
                weeklyType.hide();
                monthlyType.show();
                subRentalDaysPerWeek.hide();
                break;
            default:
                weeklyType.show();
                monthlyType.hide();
                subRentalDaysPerWeek.show();
                break;
        }
        //resets back to period summary frames
        //$form.find('.togglebutton-item input[value="P"]').click();
        FwFormField.setValueByDataField($form, 'totalTypeProfitLoss', 'P', '', false);
    }
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        $form.find('div[data-datafield="VendorId"]').data('onchange', $tr => {
            //FwFormField.setValue($form, 'div[data-datafield="RateType"]', $tr.find('.field[data-formdatafield="DefaultRate"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="DefaultRate"]').attr('data-originalvalue'));
            //FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', $tr.find('.field[data-browsedatafield="BillingCycleId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="BillingCycle"]').attr('data-originalvalue'));

            //justin hoffman 03/16/2020 - only change the RateType and BillingCycle when the PO is NEW
            if ($form.attr('data-mode') === 'NEW') {
                FwFormField.setValue($form, 'div[data-datafield="RateType"]', $tr.find('.field[data-formdatafield="DefaultRate"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="DefaultRate"]').attr('data-originalvalue'));
                FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', $tr.find('.field[data-browsedatafield="BillingCycleId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="BillingCycle"]').attr('data-originalvalue'));
            }

            const vendorId = FwFormField.getValueByDataField($form, 'VendorId');

            FwAppData.apiMethod(true, 'GET', `api/v1/vendor/${vendorId}`, null, FwServices.defaultTimeout, response => {
                FwFormField.setValueByDataField($form, 'RemitToAddress1', response.RemitAddress1);
                FwFormField.setValueByDataField($form, 'RemitToAddress2', response.RemitAddress2);
                FwFormField.setValueByDataField($form, 'RemitToCity', response.RemitCity);
                FwFormField.setValueByDataField($form, 'RemitToState', response.RemitState);
                FwFormField.setValueByDataField($form, 'RemitToZipCode', response.RemitZipCode);
                FwFormField.setValueByDataField($form, 'RemitToCountryId', response.RemitCountryId, response.RemitCountry);


                const office = JSON.parse(sessionStorage.getItem('location'));
                const currencyId = response.DefaultCurrencyId || office.defaultcurrencyid;
                const currencyCode = response.DefaultCurrencyCode || office.defaultcurrencycode;
                FwFormField.setValueByDataField($form, 'CurrencyId', currencyId, currencyCode);


                if ($form.attr('data-mode') === 'NEW') {
                    FwFormField.setValueByDataField($form, 'ReceiveDeliveryDeliveryType', response.DefaultOutgoingDeliveryType);
                    FwFormField.setValueByDataField($form, 'ReturnDeliveryDeliveryType', response.DefaultIncomingDeliveryType);
                    if (response.DefaultOutgoingDeliveryType === 'DELIVER' || response.DefaultOutgoingDeliveryType === 'SHIP') {
                        FwFormField.setValueByDataField($form, 'ReceiveDeliveryAddressType', 'DEAL');
                        this.fillDeliveryAddressFieldsforDeal($form, 'Receive', response);
                    }
                    else if (response.DefaultOutgoingDeliveryType === 'PICK UP') {
                        FwFormField.setValueByDataField($form, 'ReceiveDeliveryAddressType', 'WAREHOUSE');
                        this.getWarehouseAddress($form, 'Receive');
                    }

                    if (response.DefaultIncomingDeliveryType === 'DELIVER' || response.DefaultIncomingDeliveryType === 'SHIP') {
                        FwFormField.setValueByDataField($form, 'ReturnDeliveryAddressType', 'WAREHOUSE');
                        this.getWarehouseAddress($form, 'Return');
                    }
                    else if (response.DefaultIncomingDeliveryType === 'PICK UP') {
                        FwFormField.setValueByDataField($form, 'ReturnDeliveryAddressType', 'DEAL');
                        this.fillDeliveryAddressFieldsforDeal($form, 'Return', response);
                    }
                }
            }, null, null);
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

        // Out / In DeliveryType radio in Deliver tab
        $form.find('.delivery-type-radio').on('change', event => {
            this.deliveryTypeAddresses($form, event);
        });
        //summary toggle buttons
        $form.find('.profit-loss-total input').off('change').on('change', e => {
            this.loadSummary($form);
        });
        // Stores previous value for Receive / ReturnDeliveryDeliveryType
        $form.find('.delivery-delivery').on('click', event => {
            const $element = jQuery(event.currentTarget);
            if ($element.attr('data-datafield') === 'ReceiveDeliveryDeliveryType') {
                $element.data('prevValue', FwFormField.getValueByDataField($form, 'ReceiveDeliveryDeliveryType'))
            } else {
                $element.data('prevValue', FwFormField.getValueByDataField($form, 'ReturnDeliveryDeliveryType'))
            }
        });
        // Delivery type select field on Deliver tab
        $form.find('.delivery-delivery').on('change', event => {
            const $element = jQuery(event.currentTarget);
            const newValue = $element.find('.fwformfield-value').val();
            const prevValue = $element.data('prevValue');

            if ($element.attr('data-datafield') === 'ReceiveDeliveryDeliveryType') {
                if (newValue === 'DELIVER' && prevValue === 'PICK UP') {
                    FwFormField.setValueByDataField($form, 'ReceiveDeliveryAddressType', 'DEAL');
                }
                if (newValue === 'SHIP' && prevValue === 'PICK UP') {
                    FwFormField.setValueByDataField($form, 'ReceiveDeliveryAddressType', 'DEAL');
                }
                if (newValue === 'PICK UP') {
                    FwFormField.setValueByDataField($form, 'ReceiveDeliveryAddressType', 'WAREHOUSE');
                }
                $form.find('div[data-datafield="ReceiveDeliveryAddressType"]').change();
            }
            else if ($element.attr('data-datafield') === 'ReturnDeliveryDeliveryType') {
                if (newValue === 'DELIVER' && prevValue === 'PICK UP') {
                    FwFormField.setValueByDataField($form, 'ReturnDeliveryAddressType', 'WAREHOUSE');
                }
                if (newValue === 'SHIP' && prevValue === 'PICK UP') {
                    FwFormField.setValueByDataField($form, 'ReturnDeliveryAddressType', 'WAREHOUSE');
                }
                if (newValue === 'PICK UP') {
                    FwFormField.setValueByDataField($form, 'ReturnDeliveryAddressType', 'DEAL');
                }
                $form.find('div[data-datafield="ReturnDeliveryAddressType"]').change();
            }
        });

        //Track shipment-Receive
        $form.find('.track-shipment-return').on('click', e => {
            const trackingURL = FwFormField.getValueByDataField($form, 'ReceiveDeliveryFreightTrackingUrl');
            if (trackingURL !== '') {
                try {
                    window.open(trackingURL);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }
        });

        //Track shipment-Return
        $form.find('.track-shipment-return').on('click', e => {
            const trackingURL = FwFormField.getValueByDataField($form, 'ReturnDeliveryFreightTrackingUrl');
            if (trackingURL !== '') {
                try {
                    window.open(trackingURL);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }
        });
        $form.find('.addresscopy').on('click', e => {
            const $confirmation = FwConfirmation.renderConfirmation('Confirm Copy', '');
            const html: Array<string> = [];
            html.push('<div class="flexrow">Copy Receive Address into Return Address?</div>');
            FwConfirmation.addControls($confirmation, html.join(''));
            const $yes = FwConfirmation.addButton($confirmation, 'Copy', false);
            const $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', copyAddress);
            function copyAddress() {
                FwNotification.renderNotification('SUCCESS', 'Address Successfully Copied.');
                FwConfirmation.destroyConfirmation($confirmation);
                FwFormField.setValueByDataField($form, 'ReturnDeliveryToLocation', FwFormField.getValueByDataField($form, 'ReceiveDeliveryToLocation'));
                FwFormField.setValueByDataField($form, 'ReturnDeliveryToAttention', FwFormField.getValueByDataField($form, 'ReceiveDeliveryToAttention'));
                FwFormField.setValueByDataField($form, 'ReturnDeliveryToAddress1', FwFormField.getValueByDataField($form, 'ReceiveDeliveryToAddress1'));
                FwFormField.setValueByDataField($form, 'ReturnDeliveryToAddress2', FwFormField.getValueByDataField($form, 'ReceiveDeliveryToAddress2'));
                FwFormField.setValueByDataField($form, 'ReturnDeliveryToCity', FwFormField.getValueByDataField($form, 'ReceiveDeliveryToCity'));
                FwFormField.setValueByDataField($form, 'ReturnDeliveryToState', FwFormField.getValueByDataField($form, 'ReceiveDeliveryToState'));
                FwFormField.setValueByDataField($form, 'ReturnDeliveryToZipCode', FwFormField.getValueByDataField($form, 'ReceiveDeliveryToZipCode'));
                FwFormField.setValueByDataField($form, 'ReturnDeliveryToCountryId', FwFormField.getValueByDataField($form, 'ReceiveDeliveryToCountryId'), FwFormField.getTextByDataField($form, 'ReceiveDeliveryToCountryId'));
                FwFormField.setValueByDataField($form, 'ReturnDeliveryToCrossStreets', FwFormField.getValueByDataField($form, 'ReceiveDeliveryToCrossStreets'));
                $form.attr('data-modified', 'true');
                $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
            }
        });
        //Hide/Show summary buttons based on rate type
        $form.find('[data-datafield="RateType"]').data('onchange', e => {
            this.applyRateType($form);
        });

        //Activity Filters
        $form.on('change', '.activity-filters', e => {
            ActivityGridController.filterActivities($form);
        });
        // Prevent items tab view on 'NEW' records
        $form.find('[data-type="tab"][data-notOnNew="true"]').on('click', e => {
            if ($form.attr('data-mode') === 'NEW') {
                e.stopImmediatePropagation();
                FwNotification.renderNotification('WARNING', 'Save Record first.');
            }
        });

        //Toggle projects tab based on Department
        $form.find('[data-datafield="DepartmentId"]').data('onchange', $tr => {
            const enableProjects = FwBrowse.getValueByDataField($form, $tr, 'EnableProjects');
            enableProjects === 'true' ? $form.find('.projecttab').show() : $form.find('.projecttab').hide();
        });

        //Project validations
        $form.find('[data-datafield="ProjectId"]').data('onchange', $tr => {
            const validationName = $tr.closest('.fwbrowse').attr('data-name');
            const id = FwBrowse.getValueByDataField(null, $tr, 'ProjectId');
            let data: any = {};
            if (validationName === 'ProjectValidation') {
                data = {
                    field: 'ProjectNumber',
                    value: FwBrowse.getValueByDataField(null, $tr, 'ProjectNumber')
                };
            } else if (validationName === 'ProjectNumberValidation') {
                data = {
                    field: 'Project',
                    value: FwBrowse.getValueByDataField(null, $tr, 'Project')
                };
            }
            FwFormField.setValue2($form.find(`[data-validationname="${data.field}Validation"]`), id, data.value);
        });

        //Currency Change
        $form.find('[data-datafield="CurrencyId"]').data('onchange', $tr => {
            const mode = $form.attr('data-mode');
            if (mode !== 'NEW') {
                const originalVal = $form.find('[data-datafield="CurrencyId"]').attr('data-originalvalue');
                const newVal = FwFormField.getValue2($form.find('[data-datafield="CurrencyId"]'));
                const $updateRatesCheckbox = $form.find('[data-datafield="UpdateAllRatesToNewCurrency"]');
                if (originalVal !== '' && originalVal !== newVal) {
                    const currency = FwBrowse.getValueByDataField($form, $tr, 'Currency');
                    const currencyCode = FwBrowse.getValueByDataField($form, $tr, 'CurrencyCode');
                    const currencySymbol = FwBrowse.getValueByDataField($form, $tr, 'CurrencySymbol');
                    $updateRatesCheckbox.show().find('.checkbox-caption')
                        .text(`Update Rates for all items on this ${this.Module} to ${currency} (${currencyCode})?`)
                        .css('white-space', 'break-spaces');
                    FwFormField.setValueByDataField($form, 'CurrencySymbol', currencySymbol);
                } else {
                    $form.find('[data-datafield="UpdateAllRatesToNewCurrency"]').hide();
                }
                FwFormField.setValueByDataField($form, 'ConfirmUpdateAllRatesToNewCurrency', '');
                $form.find('[data-datafield="ConfirmUpdateAllRatesToNewCurrency"]').hide();
                FwFormField.setValueByDataField($form, 'UpdateAllRatesToNewCurrency', false);
            }
        });

        //Currency Change Text Confirmation
        $form.on('change', '[data-datafield="UpdateAllRatesToNewCurrency"]', e => {
            const updateAllRates = FwFormField.getValueByDataField($form, 'UpdateAllRatesToNewCurrency');
            const $updateRatesTextConfirmation = $form.find('[data-datafield="ConfirmUpdateAllRatesToNewCurrency"]');
            if (updateAllRates) {
                $updateRatesTextConfirmation.show().find('.fwformfield-caption')
                    .text(`Type 'UPDATE RATES' here to confirm this change.  All Item Rates will be altered when this Purchase Order is saved.`)
                    .css({ 'white-space': 'break-spaces', 'height': 'auto', 'font-size': '1em', 'color': 'red' });
            } else {
                FwFormField.setValueByDataField($form, 'ConfirmUpdateAllRatesToNewCurrency', '');
                $updateRatesTextConfirmation.hide();
            }
        });
    };
    //----------------------------------------------------------------------------------------------
    deliveryTypeAddresses($form: any, event: any): void {
        const $element = jQuery(event.currentTarget);
        if ($element.attr('data-datafield') === 'ReceiveDeliveryAddressType') {
            const value = FwFormField.getValueByDataField($form, 'ReceiveDeliveryAddressType');
            if (value === 'WAREHOUSE') {
                this.getWarehouseAddress($form, 'Receive');
            } else if (value === 'DEAL') {
                this.fillDeliveryAddressFieldsforDeal($form, 'Receive');
            }
        }
        else if ($element.attr('data-datafield') === 'ReturnDeliveryAddressType') {
            const value = FwFormField.getValueByDataField($form, 'ReturnDeliveryAddressType');
            if (value === 'WAREHOUSE') {
                this.getWarehouseAddress($form, 'Return');
            } else if (value === 'DEAL') {
                this.fillDeliveryAddressFieldsforDeal($form, 'Return');
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    loadSummary($form: any) {
        FwFormField.disable($form.find('.frame'));
        const period = FwFormField.getValueByDataField($form, 'totalTypeProfitLoss');
        let id = FwFormField.getValueByDataField($form, `${this.Module}Id`);
        if (id !== '') {
            if (typeof period !== 'undefined') {
                id = `${id}~${period}`
            }
            FwAppData.apiMethod(true, 'GET', `api/v1/ordersummary/${id}`, null, FwServices.defaultTimeout, response => {
                for (let key in response) {
                    if (response.hasOwnProperty(key)) {
                        FwFormField.setValue($form, `[data-framedatafield="${key}"]`, response[key]);
                    }
                }

                //const $profitFrames = $form.find('.profitframes .frame');  //no profiframes in PO
                //$profitFrames.each(function () {
                //    var profit = parseFloat(jQuery(this).attr('data-originalvalue'));
                //    if (profit > 0) {
                //        jQuery(this).find('input').css('background-color', '#A6D785');
                //    } else if (profit < 0) {
                //        jQuery(this).find('input').css('background-color', '#ff9999');
                //    }
                //});  

                const $totalFrames = $form.find('.totalColors input');
                $totalFrames.each(function () {
                    var total = jQuery(this).val();
                    if (total != 0) {
                        jQuery(this).css('background-color', '#ffffe5');
                    }
                })
            }, null, $form);
            //$form.find(".frame .add-on").children().hide(); // Jason H - 03/24/20 This looks like it doesn't do anything?
            //$form.find('.frame input').css('width', '100%');
        }
    };
    //----------------------------------------------------------------------------------------------
    getWarehouseAddress($form: any, prefix: string): void {
        //const warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid; - J.Pace :: changed from user warehouse to order warehouse at request of mgmt 12/31/19
        const warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');

        let WHresponse: any = {};

        if ($form.data('whAddress')) {
            WHresponse = $form.data('whAddress');
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToLocation`, WHresponse.Warehouse);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAttention`, WHresponse.Attention);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress1`, WHresponse.Address1);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress2`, WHresponse.Address2);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToCity`, WHresponse.City);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToState`, WHresponse.State);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToZipCode`, WHresponse.Zip);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToCountryId`, WHresponse.CountryId, WHresponse.Country);
        } else {
            if (warehouseId) {
                FwAppData.apiMethod(true, 'GET', `api/v1/warehouse/${warehouseId}`, null, FwServices.defaultTimeout, response => {
                    WHresponse = response;

                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToLocation`, WHresponse.Warehouse);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToAttention`, WHresponse.Attention);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress1`, WHresponse.Address1);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress2`, WHresponse.Address2);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToCity`, WHresponse.City);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToState`, WHresponse.State);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToZipCode`, WHresponse.Zip);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToCountryId`, WHresponse.CountryId, WHresponse.Country);
                    // Preventing unnecessary API calls once warehouse addresses have been requested once
                    $form.data('whAddress', {
                        'Warehouse': response.Warehouse,
                        'Attention': response.Attention,
                        'Address1': response.Address1,
                        'Address2': response.Address2,
                        'City': response.City,
                        'State': response.State,
                        'Zip': response.Zip,
                        'CountryId': response.CountryId,
                        'Country': response.Country
                    })
                }, null, null);
            } else {
                FwNotification.renderNotification('INFO', 'No Warehouse chosen on Purchase Order.');
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    fillDeliveryAddressFieldsforDeal($form: any, prefix: string, response?: any): void {
        if (!response) {
            const dealId = FwFormField.getValueByDataField($form, 'DealId');
            if (dealId !== '') {
                FwAppData.apiMethod(true, 'GET', `api/v1/deal/${dealId}`, null, FwServices.defaultTimeout, res => {
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToLocation`, res.Deal);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToAttention`, res.ShipAttention);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress1`, res.ShipAddress1);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress2`, res.ShipAddress2);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToCity`, res.ShipCity);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToState`, res.ShipState);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToZipCode`, res.ShipZipCode);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToCountryId`, res.ShipCountryId, res.ShipCountry);
                }, null, null);
            }
        } else {
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToLocation`, response.Deal);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAttention`, response.ShipAttention);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress1`, response.ShipAddress1);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress2`, response.ShipAddress2);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToCity`, response.ShipCity);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToState`, response.ShipState);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToZipCode`, response.ShipZipCode);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToCountryId`, response.ShipCountryId, response.ShipCountry);
        }
    }
    //----------------------------------------------------------------------------------------------
    assignBarCodes($form) {
        const mode = 'EDIT';
        const purchaseOrderInfo: any = {};
        purchaseOrderInfo.PurchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        purchaseOrderInfo.PurchaseOrderNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
        const $assignBarCodesForm = AssignBarCodesController.openForm(mode, purchaseOrderInfo);
        FwModule.openSubModuleTab($form, $assignBarCodesForm);
        const $tab = FwTabs.getTabByElement($assignBarCodesForm);
        $tab.find('.caption').html('Assign Bar Codes');
    }
    //----------------------------------------------------------------------------------------------
    returnToVendor($form) {
        const mode = 'EDIT';
        const purchaseOrderInfo: any = {};
        purchaseOrderInfo.PurchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        purchaseOrderInfo.PurchaseOrderNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
        const $returnToVendorForm = ReturnToVendorController.openForm(mode, purchaseOrderInfo);
        FwModule.openSubModuleTab($form, $returnToVendorForm);
        const $tab = FwTabs.getTabByElement($returnToVendorForm);
        $tab.find('.caption').html('Return To Vendor');
    }
    //----------------------------------------------------------------------------------------------
    search($form) {
        const orderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');

        if (orderId == "") {
            FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
        } else {
            let search = new SearchInterface();
            let $popup = search.renderSearchPopup($form, orderId, 'PurchaseOrder');
        }
    }
    //----------------------------------------------------------------------------------------------
    receiveFromVendor($form) {
        const mode = 'EDIT';
        const purchaseOrderInfo: any = {};
        purchaseOrderInfo.PurchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        purchaseOrderInfo.PurchaseOrderNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
        const $receiveFromVendorForm = ReceiveFromVendorController.openForm(mode, purchaseOrderInfo);
        FwModule.openSubModuleTab($form, $receiveFromVendorForm);
        const $tab = FwTabs.getTabByElement($receiveFromVendorForm);
        $tab.find('.caption').html('Receive From Vendor');
    }
    //---------------------------------------------------------------------------------
    purchaseOrderStatus($form: JQuery) {
        const mode = 'EDIT';
        const orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        orderInfo.OrderNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
        const $orderStatusForm = PurchaseOrderStatusController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $orderStatusForm);
        const $tab = FwTabs.getTabByElement($orderStatusForm);
        $tab.find('.caption').html('Purchase Order Status');
    }
    //----------------------------------------------------------------------------------------------	
    beforeValidate(datafield, request, $validationbrowse, $form, $tr) {
        switch (datafield) {
            case 'VendorId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatevendor`);
                break;
            case 'DepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedepartment`);
                break;
            case 'RateType':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validaterate`);
                break;
            case 'PoTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepotype`);
                break;
            case 'AgentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateagent`);
                break;
            case 'ProjectManagerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateprojectmanager`);
                break;
            case 'BillingCycleId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatebillingcycle`);
                break;
            case 'CurrencyId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecurrency`);
                break;
            case 'TaxOptionId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatetaxoption`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------	
    toggleClosePurchaseOrder($form) {
        let $confirmation, $yes;
        const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        const purchaseOrderNumber = FwFormField.getValueByDataField($form, 'PurchaseOrderNumber');
        const purchaseOrderStatus = FwFormField.getValueByDataField($form, 'Status');
        const confirmationText = purchaseOrderStatus === 'CLOSED' ? 'Re-Open Purchase Order' : 'Close Purchase Order';
        $confirmation = FwConfirmation.renderConfirmation(confirmationText, '<div style="white-space:pre;">\n' +
            confirmationText + ' ' + purchaseOrderNumber + '?</div>');
        $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
        FwConfirmation.addButton($confirmation, 'No');
        $yes.on('click', () => {
            FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorder/toggleclose/${purchaseOrderId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    FwNotification.renderNotification('SUCCESS', purchaseOrderStatus === 'CLOSED' ? 'Purchase Order Re-Opened' : 'Purchase Order Closed');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwModule.refreshForm($form);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, $form);
        });
    }
    //----------------------------------------------------------------------------------------------	
    renderScheduleDateAndTimeSection($form, response) {
        $form.find('.activity-dates').empty();
        const activityDatesAndTimes = response.ActivityDatesAndTimes;
        for (let i = 0; i < activityDatesAndTimes.length; i++) {
            const row = activityDatesAndTimes[i];
            let validationClass = '';
            if (row.ActivityType === 'PICK' || row.ActivityType === 'START' || row.ActivityType === 'STOP') {
                validationClass = 'pick-date-validation';
            }
            const $row = jQuery(`<div class="flexrow date-row">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="OrderTypeDateTypeId" style="display:none;"></div>
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield ${validationClass}" data-caption="${row.DescriptionDisplayTitleCase} Date" data-dateactivitytype="${row.ActivityType}" data-datafield="Date" data-enabled="true" style="flex:0 1 150px;"></div>
                              <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Time" data-timeactivitytype="${row.ActivityType}" data-datafield="Time" data-enabled="true" style="flex:0 1 120px;"></div>
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Day" data-datafield="DayOfWeek" data-enabled="false" style="flex:0 1 120px;"></div>                          
                              <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Production Activity" data-datafield="IsProductionActivity" style="display:none; flex:0 1 180px;"></div>                          
                              <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Milestone" data-datafield="IsMilestone" style="display:none; flex:0 1 110px;"></div>                          
                              </div>`);
            FwControl.renderRuntimeControls($row.find('.fwcontrol'));
            FwFormField.setValueByDataField($row, 'OrderTypeDateTypeId', row.OrderTypeDateTypeId);
            FwFormField.setValueByDataField($row, 'Date', row.Date);
            FwFormField.setValueByDataField($row, 'Time', row.Time);
            FwFormField.setValueByDataField($row, 'DayOfWeek', row.DayOfWeek);
            FwFormField.setValueByDataField($row, 'IsProductionActivity', row.IsProductionActivity);
            FwFormField.setValueByDataField($row, 'IsMilestone', row.IsMilestone);
            $form.find('.activity-dates').append($row);
        };

        const $showActivitiesAndMilestones = jQuery(`<div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Production Activities and Milestones" data-datafield="ShowActivitiesAndMilestones" style="flex:1 1 250px;"></div>`);
        FwControl.renderRuntimeControls($showActivitiesAndMilestones);
        $form.find('.activity-dates').append($showActivitiesAndMilestones);

        $showActivitiesAndMilestones.on('change', e => {
            const isChecked = jQuery(e.currentTarget).find('input').prop('checked');
            const $checkboxes = $form.find('[data-datafield="IsMilestone"], [data-datafield="IsProductionActivity"]');
            if (isChecked) {
                $checkboxes.show();
            } else {
                $checkboxes.hide();
            }
        });

        const scheduleFields = $form.find('.schedule-date-fields');
        scheduleFields.remove();
        const activityDateFields = $form.find('.activity-dates');
        activityDateFields.show();

        function addNewFieldsToDataObj($form, $newFieldsObj) {
            const $fieldsDataObj = $form.data('fields');
            const $newFields = $fieldsDataObj.add($newFieldsObj);
            $form.data('fields', $newFields);
        }

        const newDateFields = $form.find('.pick-date-validation');
        addNewFieldsToDataObj($form, newDateFields);

        $form.data('beforesave', request => {
            if ($form.find('.activity-dates:visible').length > 0) {
                const activityDatesAndTimes = [];
                const $rows = $form.find('.date-row');
                for (let i = 0; i < $rows.length; i++) {
                    const $row = jQuery($rows[i]);
                    activityDatesAndTimes.push({
                        OrderTypeDateTypeId: FwFormField.getValue2($row.find('[data-datafield="OrderTypeDateTypeId"]'))
                        , Date: FwFormField.getValue2($row.find('[data-datafield="Date"]'))
                        , Time: FwFormField.getValue2($row.find('[data-datafield="Time"]'))
                        , IsProductionActivity: FwFormField.getValue2($row.find('[data-datafield="IsProductionActivity"]'))
                        , IsMilestone: FwFormField.getValue2($row.find('[data-datafield="IsMilestone"]'))
                    });
                }
                request['ActivityDatesAndTimes'] = activityDatesAndTimes;
            }
        });

        //stops field event bubbling
        $form.off('change', '.fwformfield[data-enabled="true"][data-datafield!=""]:not(.find-field)');
    }
}
//----------------------------------------------------------------------------------------------

var PurchaseOrderController = new PurchaseOrder();