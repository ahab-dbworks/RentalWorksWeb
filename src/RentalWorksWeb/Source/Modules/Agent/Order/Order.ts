class Order extends OrderBase {
    Module = 'Order';
    apiurl: string = 'api/v1/order';
    caption: string = Constants.Modules.Agent.children.Order.caption;
    nav: string = Constants.Modules.Agent.children.Order.nav;
    id: string = Constants.Modules.Agent.children.Order.id;
    lossDamageSessionId: string = '';
    successSoundFileName: string;
    errorSoundFileName: string;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasInactive = false;
        options.hasDelete = false;
        FwMenu.addBrowseMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel / Uncancel', 'dpH0uCuEp3E89', (e: JQuery.ClickEvent) => {
            try {
                this.browseCancelOption(options.$browse);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        // View DropDownMenu
        const $all = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $confirmed = FwMenu.generateDropDownViewBtn('Confirmed', false, "CONFIRMED");
        const $active = FwMenu.generateDropDownViewBtn('Active', false, "ACTIVE");
        const $hold = FwMenu.generateDropDownViewBtn('Hold', false, "HOLD");
        const $complete = FwMenu.generateDropDownViewBtn('Complete', false, "COMPLETE");
        const $cancelled = FwMenu.generateDropDownViewBtn('Cancelled', false, "CANCELLED");
        const $closed = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");

        const viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $confirmed, $active, $hold, $complete, $cancelled, $closed);
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
        const $myAgent = FwMenu.generateDropDownViewBtn('My Agent Orders', false, "AGENT");
        const $myProjectManager = FwMenu.generateDropDownViewBtn('My Project Manager Orders', false, "PROJECTMANAGER");

        const viewAgentItems: Array<JQuery> = [];
        viewAgentItems.push($allAgents, $myAgent, $myProjectManager);
        FwMenu.addViewBtn(options.$menu, 'My', viewAgentItems, true, "My");
    }
    //-----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Copy Order', 'S3zkxYNnBXzo', (e: JQuery.ClickEvent) => {
            try {
                this.copyOrderOrQuote(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Print Order', '1oEwl4qqLQym', (e: JQuery.ClickEvent) => {
            try {
                this.printQuoteOrder(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Print Manifest', '', (e: JQuery.ClickEvent) => {
            try {
                this.printManifest(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
            try {
                this.search(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel / Uncancel', 'cSxghAONeqcu', (e: JQuery.ClickEvent) => {
            try {
                this.cancelUncancelOrder(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        //no-security
        FwMenu.addSubMenuItem(options.$groupOptions, 'Create Pick List', '', (e: JQuery.ClickEvent) => {
            try {
                this.createPickList(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Create Snapshot', 'q7jExMRUzP6G', (e: JQuery.ClickEvent) => {
            try {
                this.createSnapshotOrder(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        FwMenu.addSubMenuItem(options.$groupOptions, 'On Hold', 'ChTLbGO95bgpJ', (e: JQuery.ClickEvent) => {
            try {
                this.OrderOnHold(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Order Status', 'C8Ycf0jvM2U9', (e: JQuery.ClickEvent) => {
            try {
                this.orderStatus(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Check Out', 'H0sf3MFhL0VK', (e: JQuery.ClickEvent) => {
            try {
                this.checkOut(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Check In', 'krnJWTUs4n5U', (e: JQuery.ClickEvent) => {
            try {
                this.checkIn(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Change Office Location', 'xFLAlAgAGKG07', (e: JQuery.ClickEvent) => {
            try {
                this.changeOfficeLocation(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //-----------------------------------------------------------------------------------------------
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
    openForm(mode: string, parentModuleInfo?: any) {
        let $form = super.openForm(mode, parentModuleInfo);

        //FwTabs.hideTab($form.find('.activitytab'));
        //FwTabs.hideTab($form.find('.picklisttab'));
        //FwTabs.hideTab($form.find('.contracttab'));
        //FwTabs.hideTab($form.find('.repairtab'));
        //FwTabs.hideTab($form.find('.purchaseordertab'));
        //FwTabs.hideTab($form.find('.invoicetab'));

        if (mode === 'NEW') {
            $form.find('[data-type="tab"][data-caption="Loss & Damage"]').hide();
            FwFormField.disable($form.find('[data-datafield="LossAndDamage"]'));
        }

        let nodeContract = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'Z8MlDQp7xOqu');
        if (nodeContract !== undefined && nodeContract.properties.visible === 'T') {
            FwTabs.showTab($form.find('.contracttab'));
            $form.find('.contract-submodule').append(this.openSubModuleBrowse($form, 'Contract'));
        }

        let nodePickList = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'bggVQOivrIgi');
        if (nodePickList !== undefined && nodePickList.properties.visible === 'T') {
            FwTabs.showTab($form.find('.picklisttab'));
            $form.find('.picklist-submodule').append(this.openSubModuleBrowse($form, 'PickList'));
        }

        let nodeRepair = FwApplicationTree.getNodeById(FwApplicationTree.tree, 't4gfyzLkSZhyc');
        if (nodeRepair !== undefined && nodeRepair.properties.visible === 'T') {
            FwTabs.showTab($form.find('.repairtab'));
            $form.find('.repair-submodule').append(this.openSubModuleBrowse($form, 'Repair'));
        }

        let nodePurchaseOrder = FwApplicationTree.getNodeById(FwApplicationTree.tree, '9a0xOMvBM7Uh9');
        if (nodePurchaseOrder !== undefined && nodePurchaseOrder.properties.visible === 'T') {
            FwTabs.showTab($form.find('.purchaseordertab'));
            $form.find('.purchaseorder-submodule').append(this.openSubModuleBrowse($form, 'PurchaseOrder'));
        }

        let nodeInvoice = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'cZ9Z8aGEiDDw');
        if (nodeInvoice !== undefined && nodeInvoice.properties.visible === 'T') {
            FwTabs.showTab($form.find('.invoicetab'));
            $form.find('.invoice-submodule').append(this.openSubModuleBrowse($form, 'Invoice'));
        }

        const $orderItemGridLossDamage = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');

        // Hides Add, Search, and Sub-Worksheet buttons on grid
        $orderItemGridLossDamage.find('.submenu-btn').filter('[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"], [data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        $orderItemGridLossDamage.find('.buttonbar').hide();


        //$form.find('div[data-datafield="BillingCycleId"]').data('onchange', () => {
        //    const worksheetTab = $form.find('[data-type="tab"][data-caption="Billing Worksheet"]');
        //    if (FwFormField.getTextByDataField($form, 'BillingCycleId') === 'ON DEMAND') {
        //        worksheetTab.show();
        //    } else {
        //        worksheetTab.hide();
        //    }
        //});

        //Toggle Buttons - Manifest tab - View Items
        FwFormField.loadItems($form.find('div[data-datafield="manifestItems"]'), [
            { value: 'SUMMARY', caption: 'Summary', checked: 'checked' },
            { value: 'DETAIL', caption: 'Detail' }
        ]);
        //Toggle Buttons - Manifest tab - Filter By
        FwFormField.loadItems($form.find('div[data-datafield="manifestFilter"]'), [
            { value: 'ALL', caption: 'All', checked: 'checked' },
            { value: 'OUT', caption: 'Out' },
            { value: 'IN', caption: 'In' },
            { value: 'SHORT', caption: 'Short' }
        ]);
        //Toggle Buttons - Manifest tab - Rental Valuation
        FwFormField.loadItems($form.find('div[data-datafield="rentalValueSelector"]'), [
            { value: 'UNIT VALUE', text: 'Unit Value', selected: 'checked' },
            { value: 'REPLACEMENT COST', text: 'Replacement Cost' }
        ], true);
        //Toggle Buttons - Manifest tab - Sales Valuation
        FwFormField.loadItems($form.find('div[data-datafield="salesValueSelector"]'), [
            { value: 'SELL PRICE', text: 'Sell Price', selected: 'checked' },
            { value: 'DEFAULT COST', text: 'Default Cost' },
            { value: 'AVERAGE COST', text: 'Average Cost' }
        ], true);
        //Toggle Buttons - Manifest tab - Weight Type
        FwFormField.loadItems($form.find('div[data-datafield="weightSelector"]'), [
            { value: 'IMPERIAL', caption: 'Imperial', checked: 'checked' },
            { value: 'METRIC', caption: 'Metric' }
        ]);

        $form
            .on('change', 'div[data-datafield="manifestItems"], div[data-datafield="manifestFilter"], div[data-datafield="rentalValueSelector"], div[data-datafield="salesValueSelector"]', event => {
                let $OrderManifestGrid = $form.find('div[data-name="OrderManifestGrid"]');

                FwBrowse.search($OrderManifestGrid);
            })
            .on('change', 'div[data-datafield="weightSelector"]', e => {
                if (FwFormField.getValueByDataField($form, 'weightSelector') === 'IMPERIAL') {
                    $form.find('div[data-datafield="ExtendedWeightTotalLbs"]').show();
                    $form.find('div[data-datafield="ExtendedWeightTotalOz"]').show();
                    $form.find('div[data-datafield="ExtendedWeightTotalKg"]').hide();
                    $form.find('div[data-datafield="ExtendedWeightTotalGm"]').hide();
                } else {
                    $form.find('div[data-datafield="ExtendedWeightTotalLbs"]').hide();
                    $form.find('div[data-datafield="ExtendedWeightTotalOz"]').hide();
                    $form.find('div[data-datafield="ExtendedWeightTotalKg"]').show();
                    $form.find('div[data-datafield="ExtendedWeightTotalGm"]').show();
                }
            })
            ;

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    openSubModuleBrowse($form, module: string) {
        let $browse = null;
        if (typeof window[`${module}Controller`] !== undefined && typeof window[`${module}Controller`].openBrowse === 'function') {
            $browse = (<any>window)[`${module}Controller`].openBrowse();
            $browse.data('ondatabind', request => {
                request.activeviewfields = (<any>window)[`${module}Controller`].ActiveViewFields;
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'OrderId')
                }
            });
        }
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids) {
        const $form = this.openForm('EDIT', uniqueids);
        $form.find('div.fwformfield[data-datafield="OrderId"] input').val(uniqueids.OrderId);

        let nodeActivity = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'hb52dbhX1mNLZ');
        if (nodeActivity !== undefined && nodeActivity.properties.visible === 'T') {
            FwTabs.showTab($form.find('.activitytab'));
        }

        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //---------------------------------------------------------------------------------------------
    renderGrids($form) {
        super.renderGrids($form);

        FwBrowse.renderGrid({
            nameGrid: 'OrderPickListGrid',
            gridSecurityId: 'bggVQOivrIgi',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'OrderId')
                };
            }
        });

        FwBrowse.renderGrid({
            nameGrid: 'OrderSnapshotGrid',
            gridSecurityId: 'YZQzEHG7tTUP',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1');
                FwMenu.addSubMenuItem($optionsgroup, 'View Snapshot', '', (e: JQuery.ClickEvent) => {
                    try {
                        const $form = jQuery(e.currentTarget).closest('.fwform');
                        OrderSnapshotGridController.viewSnapshotGrid($form);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                })
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'OrderId')
                };
            }
        });

        FwBrowse.renderGrid({
            nameGrid: 'OrderManifestGrid',
            gridSecurityId: '8uhwXXJ95d3o',
            moduleSecurityId: this.id,
            $form: $form,
            //getBaseApiUrl: () => `${this.apiurl}/manifest`,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                    RentalValue: FwFormField.getValueByDataField($form, 'rentalValueSelector'),
                    SalesValue: FwFormField.getValueByDataField($form, 'salesValueSelector'),
                    FilterBy: FwFormField.getValueByDataField($form, 'manifestFilter'),
                    Mode: FwFormField.getValueByDataField($form, 'manifestItems')
                };
                request.totalfields = ['OrderValueTotal', 'OrderReplacementTotal', 'OwnedValueTotal', 'OwnedReplacementTotal', 'SubValueTotal', 'SubReplacementTotal', 'ShippingContainerTotal', 'ShippingItemTotal', 'PieceCountTotal', 'StandAloneItemTotal', 'TotalExtendedWeightLbs', 'TotalExtendedWeightOz', 'TotalExtendedWeightKg', 'TotalExtendedWeightGr'];
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                var $barcodecells = $browse.find('div[data-browsedatafield="Barcode"]');
                for (var i = 0; i < $barcodecells.length; i++) {
                    let cell = jQuery($barcodecells[i]).parent();
                    if (FwFormField.getValueByDataField($form, 'manifestItems') == 'SUMMARY') {
                        jQuery(cell).hide();
                    } else {
                        jQuery(cell).show();
                    }
                }

                FwFormField.setValue($form, 'div[data-datafield="OrderValueTotal"]', dt.Totals.OrderValueTotal);
                FwFormField.setValue($form, 'div[data-datafield="OrderReplacementTotal"]', dt.Totals.OrderReplacementTotal);
                FwFormField.setValue($form, 'div[data-datafield="OwnedValueTotal"]', dt.Totals.OwnedValueTotal);
                FwFormField.setValue($form, 'div[data-datafield="OwnedReplacementTotal"]', dt.Totals.OwnedReplacementTotal);
                FwFormField.setValue($form, 'div[data-datafield="SubValueTotal"]', dt.Totals.SubValueTotal);
                FwFormField.setValue($form, 'div[data-datafield="SubReplacementTotal"]', dt.Totals.SubReplacementTotal);
                FwFormField.setValue($form, 'div[data-datafield="ShippingContainerTotal"]', dt.Totals.ShippingContainerTotal);
                FwFormField.setValue($form, 'div[data-datafield="ShippingItemTotal"]', dt.Totals.ShippingItemTotal);
                FwFormField.setValue($form, 'div[data-datafield="PieceCountTotal"]', dt.Totals.PieceCountTotal);
                FwFormField.setValue($form, 'div[data-datafield="StandAloneItemTotal"]', dt.Totals.StandAloneItemTotal);

                FwFormField.setValue($form, 'div[data-datafield="ExtendedWeightTotalLbs"]', dt.Totals.TotalExtendedWeightLbs);
                FwFormField.setValue($form, 'div[data-datafield="ExtendedWeightTotalOz"]', dt.Totals.TotalExtendedWeightOz);
                FwFormField.setValue($form, 'div[data-datafield="ExtendedWeightTotalKg"]', dt.Totals.TotalExtendedWeightKg);
                FwFormField.setValue($form, 'div[data-datafield="ExtendedWeightTotalGm"]', dt.Totals.TotalExtendedWeightGr);
            }
        });
        FwBrowse.addLegend($form.find('div[data-name="OrderManifestGrid"]'), 'Shipping Container', '#ffeb3b');
        FwBrowse.addLegend($form.find('div[data-name="OrderManifestGrid"]'), 'Stand-Alone Item', '#2196f3');

        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.lossdamagegrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                FwMenu.addSubMenuItem($optionsgroup, 'Add Loss and Damage Items', '', (e: JQuery.ClickEvent) => {
                    try {
                        this.addLossDamage(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Retire Loss and Damage Items', '', (e: JQuery.ClickEvent) => {
                    try {
                        this.retireLossDamage(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Lock / Unlock Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.lockUnlock(e);
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
                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid2')
                FwMenu.addSubMenuItem($viewgroup, 'Summary View', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.detailSummaryView(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
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
                    OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                    RecType: 'F'
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
                request.RecType = 'F';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('F');
                $fwgrid.find('div[data-datafield="InventoryId"]').attr('data-formreadonly', 'true');
                $fwgrid.find('div[data-datafield="Description"]').attr('data-formreadonly', 'true');
                $fwgrid.find('div[data-datafield="ItemId"]').attr('data-formreadonly', 'true');
                $fwgrid.find('div[data-datafield="Price"]').attr('data-digits', '3');
                $fwgrid.find('div[data-datafield="Price"]').attr('data-digitsoptional', 'false');
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateOrderItemGridTotals($form, 'lossdamage', dt.Totals);
                let lossDamageItems = $form.find('.lossdamagegrid tbody').children();
                lossDamageItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="LossAndDamage"]')) : FwFormField.enable($form.find('[data-datafield="LossAndDamage"]'));
            },
            onOverrideNotesTemplate: ($field, controlhtml, $confirmation, $browse, $tr, $ok) => {
                OrderItemGridController.addPrintNotes($field, controlhtml, $confirmation, $browse, $tr, $ok);
            },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderSubItemGrid',
            gridSelector: '.subrentalgrid div[data-grid="OrderSubItemGrid"]',
            gridSecurityId: 'kaFlpRnRQzIIz',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                options.hasEdit = false;
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                    RecType: 'R'
                };
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
                request.RecType = 'R';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('R');
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => { },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderSubItemGrid',
            gridSelector: '.subsalesgrid div[data-grid="OrderSubItemGrid"]',
            gridSecurityId: 'kaFlpRnRQzIIz',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                options.hasEdit = false;
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                    RecType: 'S'
                };
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
                request.RecType = 'S';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('S');
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => { },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderSubItemGrid',
            gridSelector: '.submiscgrid div[data-grid="OrderSubItemGrid"]',
            gridSecurityId: 'kaFlpRnRQzIIz',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                options.hasEdit = false;
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                    RecType: 'M'
                };
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
                request.RecType = 'M';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('M');
                $fwgrid.find('div[data-datatype="date"]').hide();
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => { },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderSubItemGrid',
            gridSelector: '.sublaborgrid div[data-grid="OrderSubItemGrid"]',
            gridSecurityId: 'kaFlpRnRQzIIz',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                options.hasEdit = false;
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                    RecType: 'L'
                };
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
                request.RecType = 'L';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('L');
                $fwgrid.find('div[data-datatype="date"]').hide();
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => { },
        });
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form, response) {
        super.afterLoad($form, response);
        const lossDamageTab = $form.find('[data-type="tab"][data-caption="Loss and Damage"]');

        const orderId = FwFormField.getValueByDataField($form, 'OrderId');
        this.checkMessages($form, 'order', orderId);
        if (!FwFormField.getValueByDataField($form, 'CombineActivity')) {
            // show / hide tabs
            if (!FwFormField.getValueByDataField($form, 'LossAndDamage')) { lossDamageTab.hide(), FwFormField.disable($form.find('[data-datafield="Rental"]')); }
        }

        if (FwFormField.getValueByDataField($form, 'HasLossAndDamageItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'LossAndDamage'));
        } else {
            FwFormField.enable(FwFormField.getDataField($form, 'LossAndDamage'));
        }

        if (!FwFormField.getValueByDataField($form, 'LossAndDamage')) {
            lossDamageTab.hide();
        } else {
            lossDamageTab.show();
        }

        if (!FwFormField.getValueByDataField($form, 'HasRepair')) {
            $form.find('[data-type="tab"][data-caption="Repair"]').hide();
        } else {
            $form.find('[data-type="tab"][data-caption="Repair"]').show();
        }
        //On Demand Billing Cycles require a worksheet tab
        const worksheetTab = $form.find('[data-type="tab"][data-caption="Billing Worksheet"]');
        if (FwFormField.getValueByDataField($form, 'BillingCycleType') === 'ONDEMAND') {
            worksheetTab.show();
            const billingWorksheetBrowse = this.openSubModuleBrowse($form, 'BillingWorksheet');
            $form.find('.worksheet-submodule').append(billingWorksheetBrowse);

            billingWorksheetBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
            billingWorksheetBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
                if ($form.attr('data-mode') !== 'NEW') {
                    const parentModuleInfo: any = {};
                    const $browse = jQuery(this).closest('.fwbrowse');
                    const controller = $browse.attr('data-controller');
                    parentModuleInfo.OrderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
                    parentModuleInfo.OrderDescription = FwFormField.getValueByDataField($form, 'Description');
                    parentModuleInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
                    parentModuleInfo.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
                    parentModuleInfo.OrderType = FwFormField.getTextByDataField($form, 'OrderTypeId');
                    parentModuleInfo.Deal = FwFormField.getTextByDataField($form, 'DealId');
                    parentModuleInfo.DealId = FwFormField.getValueByDataField($form, 'DealId');
                    parentModuleInfo.DealNumber = FwFormField.getValueByDataField($form, 'DealNumber');

                    if (typeof window[controller] !== 'object') throw `Missing javascript module: ${controller}`;
                    if (typeof window[controller]['openForm'] !== 'function') throw `Missing javascript function: ${controller}.openForm`;
                    const $billingWorksheetForm = window[controller]['openForm']('NEW', parentModuleInfo);
                    FwModule.openSubModuleTab($browse, $billingWorksheetForm);
                } else {
                    FwNotification.renderNotification('WARNING', 'Save the record first.')
                }
            });
        } else {
            worksheetTab.hide();
        }

        if (response.UnassignedSubs) {
            $form.find('.unassignedsubs').show();
        } else {
            $form.find('.unassignedsubs').hide();
        }

        // Documents Grid - Need to put this here, because renderGrids is called from openForm and uniqueid is not available yet on the form
        // Moved documents grid from loadForm to afterLoad so it loads on new records. - Jason H 04/20/20
        FwAppDocumentGrid.renderGrid({
            $form: $form,
            caption: 'Documents',
            nameGrid: 'OrderDocumentGrid',
            getBaseApiUrl: () => {
                return `${this.apiurl}/${orderId}/document`;
            },
            gridSecurityId: 'O9wP1M9xrgEY',
            moduleSecurityId: this.id,
            parentFormDataFields: 'OrderId',
            uniqueid1Name: 'OrderId',
            getUniqueid1Value: () => orderId,
            uniqueid2Name: '',
            getUniqueid2Value: () => ''
        });
    }
    //----------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
        <div data-name="Order" data-control="FwBrowse" data-type="Browse" id="OrderBrowse" class="fwcontrol fwbrowse" data-controller="OrderController">
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="OrderId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="OrderTypeId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="false" data-datafield="OfficeLocationId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="false" data-datafield="WarehouseId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="false" data-datafield="DepartmentId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="false" data-datafield="CustomerId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="false" data-datafield="DealId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Order No." data-datafield="OrderNumber" data-cellcolor="NumberColor" data-browsedatatype="text" data-sort="desc" data-sortsequence="2" data-searchfieldoperators="startswith"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Date" data-datafield="OrderDate" data-browsedatatype="date" data-sortsequence="1" data-sort="desc"></div>
          </div>
          <div class="column" data-width="350px" data-visible="true">
            <div class="field" data-caption="Description" data-datafield="Description" data-cellcolor="DescriptionColor" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="250px" data-visible="true">
            <div class="field" data-caption="Deal" data-datafield="Deal" data-cellcolor="UnassignedSubsColor" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Deal No." data-datafield="DealNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="0" data-visible="true">
            <div class="field" data-caption="Status" data-datafield="Status" data-cellcolor="StatusColor" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Status As Of" data-datafield="StatusDate" data-browsedatatype="date" data-sort="off"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="PO No." data-datafield="PoNumber" data-cellcolor="PoNumberColor" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Total" data-datafield="Total" data-cellcolor="CurrencyColor" data-browsedatatype="money" data-digits="2" data-formatnumeric="true" data-sort="off"></div>
          </div>
          <div class="column" data-width="180px" data-visible="true">
            <div class="field" data-caption="Agent" data-datafield="Agent" data-multiwordseparator="|" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="50px" data-visible="true">
            <div class="field" data-caption="Warehouse" data-datafield="WarehouseCode" data-cellcolor="WarehouseColor" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column spacer" data-width="auto" data-visible="true"></div>
        </div>`;
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="orderform" class="fwcontrol fwcontainer fwform orderform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Order" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="OrderController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield OrderId" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="OrderId"></div>
          <div id="orderform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="generaltab" class="tab generaltab" data-tabpageid="generaltabpage" data-caption="Order"></div>
              <div data-type="tab" id="rentaltab" class="tab rentaltab notcombinedtab" data-tabpageid="rentaltabpage" data-notOnNew="true" data-inventorytype="Rental" data-caption="Rental"></div>
              <div data-type="tab" id="subrentaltab" class="tab subrentaltab notcombinedtab" data-hasSubGrid="true" data-tabpageid="subrentaltabpage" data-notOnNew="true" data-caption="Sub-Rental"></div>
              <div data-type="tab" id="salestab" class="tab salestab notcombinedtab" data-tabpageid="salestabpage" data-notOnNew="true" data-inventorytype="Sales" data-caption="Sales"></div>
              <div data-type="tab" id="subsalestab" class="tab subsalestab notcombinedtab" data-hasSubGrid="true" data-tabpageid="subsalestabpage" data-notOnNew="true" data-caption="Sub-Sales"></div>
              <div data-type="tab" id="misctab" class="tab misctab notcombinedtab" data-tabpageid="misctabpage" data-notOnNew="true" data-inventorytype="Misc" data-caption="Miscellaneous"></div>
              <div data-type="tab" id="submisctab" class="tab submisctab notcombinedtab" data-hasSubGrid="true" data-tabpageid="submisctabpage" data-notOnNew="true" data-caption="Sub-Miscellaneous"></div>
              <div data-type="tab" id="labortab" class="tab labortab notcombinedtab" data-tabpageid="labortabpage" data-notOnNew="true" data-inventorytype="Labor" data-caption="Labor"></div>
              <div data-type="tab" id="sublabortab" class="tab sublabortab notcombinedtab" data-hasSubGrid="true" data-tabpageid="sublabortabpage" data-notOnNew="true" data-caption="Sub-Labor"></div>
              <div data-type="tab" id="usedsaletab" class="tab usedsaletab notcombinedtab" data-tabpageid="usedsaletabpage" data-notOnNew="true" data-caption="Used Sale"></div>
              <div data-type="tab" id="lossdamagetab" class="tab lossdamagetab" data-tabpageid="lossdamagetabpage" data-notOnNew="true" data-caption="Loss &amp; Damage"></div>
              <div data-type="tab" id="alltab" class="tab combinedtab" data-tabpageid="alltabpage" data-notOnNew="true" data-caption="Items"></div>
              <div data-type="tab" id="subpurchaseordertab" class="tab submodule subpurchaseordertab" data-tabpageid="subpurchaseordertabpage" data-caption="Sub POs"></div>
              <div data-type="tab" id="billingtab" class="tab billingtab" data-tabpageid="billingtabpage" data-caption="Billing"></div>
              <div data-type="tab" id="billingworksheettab" class="tab billingworksheettab" data-tabpageid="billingworksheettabpage" data-caption="Billing Worksheet" style="display:none;"></div>
              <div data-type="tab" id="summarytab" class="tab profitlosstab summarytab" data-tabpageid="profitlosstabpage" data-caption="Profit &amp; Loss"></div>
              <div data-type="tab" id="contactstab" class="tab contactstab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
              <div data-type="tab" id="activitytab" class="tab activitytab" data-tabpageid="activitytabpage" data-caption="Activities"></div>
              <div data-type="tab" id="picklisttab" class="tab submodule picklisttab" data-tabpageid="picklisttabpage" data-caption="Pick List"></div>
              <div data-type="tab" id="contracttab" class="tab submodule contracttab" data-tabpageid="contracttabpage" data-caption="Contracts"></div>
              <div data-type="tab" id="delivershiptab" class="tab delivershiptab" data-tabpageid="delivershiptabpage" data-caption="Deliver/Ship"></div>
              <div data-type="tab" id="manifesttab" class="tab manifesttab" data-tabpageid="manifesttabpage" data-caption="Manifest"></div>
              <div data-type="tab" id="invoicetab" class="tab submodule invoicetab" data-tabpageid="invoicetabpage" data-caption="Invoices"></div>
              <div data-type="tab" id="repairtab" class="tab submodule repairtab" data-tabpageid="repairtabpage" data-caption="Repair"></div>
              <div data-type="tab" id="documentstab" class="tab documentstab" data-tabpageid="documentstabpage" data-caption="Documents"></div>
              <div data-type="tab" id="notetab" class="tab notestab" data-tabpageid="notetabpage" data-caption="Notes"></div>
              <div data-type="tab" id="historytab" class="tab historytab" data-tabpageid="historytabpage" data-caption="History"></div>
              <div data-type="tab" id="emailhistorytab" class="tab emailhistorytab" data-tabpageid="emailhistorytabpage" data-caption="Email History"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
                <div class="formpage">
                  <!-- ORDER TAB -->
                  <div class="flexrow">
                    <!-- Customer section -->
                    <div class="flexcolumn" style="flex:1 1 350px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderNumber" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-required="true" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" data-required="true" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield dealnumber" data-caption="Deal No." data-datafield="DealNumber" data-enabled="false" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" style="flex:1 1 0;display:none;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-formbeforevalidate="beforeValidateDeal" data-required="true" style="flex:1 1 225px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield RateType" data-caption="Rate" data-datafield="RateType" data-displayfield="RateTypeDisplay" data-validationname="RateTypeValidation" data-validationpeek="false" data-required="true" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="OrderTypeId" data-displayfield="OrderType" data-validationname="OrderTypeValidation" data-required="true" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Reference No." data-datafield="ReferenceNumber" style="flex:1 0 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="Location"></div>
                        </div>
                        <div class="flexrow" style="display:none;">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Rental" data-datafield="DisableEditingRentalRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Sales" data-datafield="DisableEditingSalesRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Miscellaneous" data-datafield="DisableEditingMiscellaneousRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Labor" data-datafield="DisableEditingLaborRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Used Sale" data-datafield="DisableEditingUsedSaleRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Loss and Damage" data-datafield="DisableEditingLossAndDamageRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Is Manual Sort" data-datafield="IsManualSort"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Activity">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield activity" data-caption="Combine Activity" data-datafield="CombineActivity" style="display:none"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield anti-LD activity" data-caption="Rental" data-datafield="Rental" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield anti-LD activity" data-caption="Sales" data-datafield="Sales" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield activity" data-caption="Miscellaneous" data-datafield="Miscellaneous" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield activity" data-caption="Labor" data-datafield="Labor" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield anti-LD activity" data-caption="Used Sale" data-datafield="RentalSale" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield activity" data-caption="Loss &amp; Damage" data-datafield="LossAndDamage" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow" style="display:none;">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalItem" data-datafield="HasRentalItem" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasSalesItem" data-datafield="HasSalesItem" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasMiscellaneousItem" data-datafield="HasMiscellaneousItem" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasLaborItem" data-datafield="HasLaborItem" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalSaleItem" data-datafield="HasRentalSaleItem" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasLossAndDamageItem" data-datafield="HasLossAndDamageItem" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasNotes" data-datafield="HasNotes" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRepair" data-datafield="HasRepair" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="BillingCycleType" data-datafield="BillingCycleType" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Dates & Times -->
                    <div class="flexcolumn" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Schedule">
                        <div class="flexrow schedule-date-fields">
                          <!-- removed class date-field -->
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick-date-validation og-datetime" data-caption="Pick Date" data-dateactivitytype="PICK" data-datafield="PickDate" style="flex:1 1 115px;"></div>
                          <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield og-datetime" data-caption="Pick Time" data-timeactivitytype="PICK" data-datafield="PickTime" style="flex:1 1 84px;"></div>
                        </div>
                        <div class="flexrow schedule-date-fields">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick-date-validation og-datetime" data-caption="From Date" data-dateactivitytype="START" data-datafield="EstimatedStartDate" style="flex:1 1 115px;"></div>
                          <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield og-datetime" data-caption="From Time" data-timeactivitytype="START" data-datafield="EstimatedStartTime" style="flex:1 1 84px;"></div>
                        </div>
                        <div class="flexrow schedule-date-fields">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick-date-validation og-datetime" data-caption="To Date" data-dateactivitytype="STOP" data-datafield="EstimatedStopDate" style="flex:1 1 115px;"></div>
                          <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield og-datetime" data-caption="To Time" data-timeactivitytype="STOP" data-datafield="EstimatedStopTime" style="flex:1 1 84px;"></div>
                        </div>
                        <div class="activity-dates" style="display:none;"></div>
                        <!--<div class="activity-dates-toggle"></div>-->
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section itemsection" data-control="FwContainer" data-type="section" data-caption="Documents">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Cover Letter" data-datafield="CoverLetterId" data-displayfield="CoverLetter" data-enabled="true" data-validationname="CoverLetterValidation" style="flex:1 1 225px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Terms &#038; Conditions" data-datafield="TermsConditionsId" data-displayfield="TermsConditions" data-enabled="true" data-validationname="TermsConditionsValidation" style="flex:1 1 225px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Presentation Layer" data-datafield="PresentationLayerId" data-displayfield="PresentationLayer" data-enabled="true" data-validationname="PresentationLayerValidation" style="flex:1 1 225px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Market">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="MarketTypeId" data-displayfield="MarketType" data-validationpeek="true" data-validationname="MarketTypeValidation" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Segment" data-datafield="MarketSegmentId" data-displayfield="MarketSegment" data-validationpeek="true" data-formbeforevalidate="beforeValidateMarketSegment" data-validationname="MarketSegmentValidation" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Job" data-datafield="MarketSegmentJobId" data-displayfield="MarketSegmentJob" data-validationpeek="true" data-formbeforevalidate="beforeValidateMarketSegment" data-validationname="MarketSegmentJobValidation" style="flex:1 1 150px;"></div>
                        </div>
                      </div>
                    </div>

                    <!-- Status section -->
                    <div class="flexcolumn" style="flex:1 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="Status" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="As of" data-datafield="StatusDate" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="unassignedsubs">Unassigned Subs</div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="PO">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Pending" data-datafield="PendingPo" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Flat PO" data-datafield="FlatPo" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PoNumber" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="PO Amount" data-datafield="PoAmount" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Personnel">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="AgentId" data-displayfield="Agent" data-enabled="true" data-required="true" data-validationname="UserValidation" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Project Manager" data-datafield="ProjectManagerId" data-displayfield="ProjectManager" data-enabled="true" data-required="false" data-validationname="UserValidation" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Outside Sales Representative" data-datafield="OutsideSalesRepresentativeId" data-displayfield="OutsideSalesRepresentative" data-enabled="true" data-validationname="ContactValidation" style="flex:1 1 150px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Location">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-enabled="false" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="WarehouseCode" data-datafield="WarehouseCode" data-formreadonly="true" data-enabled="false" style="display:none"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- BILLING WORKSHEET TAB -->
              <div data-type="tabpage" id="billingworksheettabpage" class="tabpage worksheet-submodule rwSubModule" data-tabid="billingworksheettab"></div>
              <!-- P&L TAB -->
              <div data-type="tabpage" id="profitlosstabpage" class="profitlossgrid tabpage" data-tabid="profitlosstab" data-render="false">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Profit &amp; Loss Summary">
                  <div class="wideflexrow">
                    <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield profit-loss-total" data-caption="View" data-gridtype="profitloss" data-datafield="totalTypeProfitLoss" style="flex:0 1 275px;"></div>
                  </div>
                  <div class="wideflexrow">
                    <!-- Order Profitability -->
                    <div class="flexcolumn" style="flex:0 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Total" data-datafield="" data-framedatafield="TotalPrice"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Discount" data-datafield="" data-framedatafield="TotalDiscount"></div>
                        </div>
                        <div class="flexrow totalColors">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Sub-Total" data-datafield="" data-framedatafield="TotalTotal"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Cost" data-datafield="" data-framedatafield="TotalCost"></div>
                        </div>
                        <div class="flexrow profitframes">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Profit" data-datafield="" data-framedatafield="TotalProfit"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Markup" data-datafield="" data-framedatafield="TotalMarkup"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Margin" data-datafield="" data-framedatafield="TotalMargin"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Rental Profitability -->
                    <div class="flexcolumn rental-pl" style="flex:0 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rentals">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Total" data-datafield="" data-framedatafield="RentalPrice"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Discount" data-datafield="" data-framedatafield="RentalDiscount"></div>
                        </div>
                        <div class="flexrow totalColors">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Sub-Total" data-datafield="" data-framedatafield="RentalSubTotal"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Cost" data-datafield="" data-framedatafield="RentalCost"></div>
                        </div>
                        <div class="flexrow profitframes">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Profit" data-datafield="" data-framedatafield="RentalProfit"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Markup" data-datafield="" data-framedatafield="RentalMarkup"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Margin" data-datafield="" data-framedatafield="RentalMargin"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Sales Profitability -->
                    <div class="flexcolumn sales-pl" style="flex:0 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sales">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Total" data-datafield="" data-framedatafield="SalesPrice"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Discount" data-datafield="" data-framedatafield="SalesDiscount"></div>
                        </div>
                        <div class="flexrow totalColors">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Sub-Total" data-datafield="" data-framedatafield="SalesSubTotal"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Cost" data-datafield="" data-framedatafield="SalesCost"></div>
                        </div>
                        <div class="flexrow profitframes">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Profit" data-datafield="" data-framedatafield="SalesProfit"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Markup" data-datafield="" data-framedatafield="SalesMarkup"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Margin" data-datafield="" data-framedatafield="SalesMargin"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Labor Profitability -->
                    <div class="flexcolumn labor-pl" style="flex:0 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Labor">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Total" data-datafield="" data-framedatafield="LaborPrice"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Discount" data-datafield="" data-framedatafield="LaborDiscount"></div>
                        </div>
                        <div class="flexrow totalColors">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Sub-Total" data-datafield="" data-framedatafield="LaborSubTotal"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Cost" data-datafield="" data-framedatafield="LaborCost"></div>
                        </div>
                        <div class="flexrow profitframes">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Profit" data-datafield="" data-framedatafield="LaborProfit"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Markup" data-datafield="" data-framedatafield="LaborMarkup"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Margin" data-datafield="" data-framedatafield="LaborMargin"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Miscellaneous Profitability -->
                    <div class="flexcolumn misc-pl" style="flex:0 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Miscellaneous">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Total" data-datafield="" data-framedatafield="MiscPrice"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Discount" data-datafield="" data-framedatafield="MiscDiscount"></div>
                        </div>
                        <div class="flexrow totalColors">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Sub-Total" data-datafield="" data-framedatafield="MiscSubTotal"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Cost" data-datafield="" data-framedatafield="MiscCost"></div>
                        </div>
                        <div class="flexrow profitframes">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Profit" data-datafield="" data-framedatafield="MiscProfit"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Markup" data-datafield="" data-framedatafield="MiscMarkup"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Margin" data-datafield="" data-framedatafield="MiscMargin"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Used Sale Profitability -->
                    <div class="flexcolumn usedsale-pl" style="flex:0 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Used Sales">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Total" data-datafield="" data-framedatafield="RentalSalePrice"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Discount" data-datafield="" data-framedatafield="RentalSaleDiscount"></div>
                        </div>
                        <div class="flexrow totalColors">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Sub-Total" data-datafield="" data-framedatafield="RentalSaleSubTotal"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Cost" data-datafield="" data-framedatafield="RentalSaleCost"></div>
                        </div>
                        <div class="flexrow profitframes">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Profit" data-datafield="" data-framedatafield="RentalSaleProfit"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Markup" data-datafield="" data-framedatafield="RentalSaleMarkup"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Margin" data-datafield="" data-framedatafield="RentalSaleMargin"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Sales Tax -->
                    <div class="flexcolumn salestax-pl" style="flex:0 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sales Tax">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="" style="visibility:hidden;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="" style="visibility:hidden"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Total" data-datafield="" data-framedatafield="TotalTax"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Cost" data-datafield="" data-framedatafield="TaxCost"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Activity Summary">
                  <div class="flexrow">
                    <div class="rwGrid" data-control="FwGrid" data-grid="OrderActivitySummaryGrid" data-securitycaption="Activity Summary"></div>
                  </div>
                </div>
              </div>

              <!-- CONTACTS TAB -->
              <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
                <div class="flexrow">
                  <div class="rwGrid" data-control="FwGrid" data-grid="OrderContactGrid" data-securitycaption="Contacts"></div>
                </div>
              </div>

              <!-- ACTIVITY TAB -->
              <div data-type="tabpage" id="activitytabpage" class="tabpage" data-tabid="activitytab">
                <div class="wideflexrow">
                  <div class="flexcolumn">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Activities">
                      <div class="rwGrid" data-control="FwGrid" data-grid="ActivityGrid" data-securitycaption="Activity"></div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:0 0 0;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filter Activities">
                      <div data-control="FwFormField" data-type="date" class="activity-filters fwcontrol fwformfield" data-caption="From" data-datafield="ActivityFromDate"></div>
                      <div data-control="FwFormField" data-type="date" class="activity-filters fwcontrol fwformfield" data-caption="To" data-datafield="ActivityToDate"></div>
                      <div data-control="FwFormField" data-type="multiselectvalidation" class="activity-filters fwcontrol fwformfield" data-caption="Activity" data-datafield="ActivityTypeId" data-validationname="ActivityTypeValidation"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="activity-filters fwcontrol fwformfield" data-caption="Show Shipping Activities" data-datafield="ShowShipping"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="activity-filters fwcontrol fwformfield" data-caption="Show Sub-PO Activities" data-datafield="ShowSubPo"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="activity-filters fwcontrol fwformfield" data-caption="Show Complete Activities" data-datafield="ShowComplete"></div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- RENTAL TAB -->
              <div data-type="tabpage" id="rentaltabpage" class="rentalgrid notcombined tabpage" data-tabid="rentaltab" data-render="false">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:1 1 600px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Items">
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Rental Items"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:0 0 auto;">
                    <div class="flexcolumn summarySection" style="flex:0 1 200px;padding-right:10px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Totals">
                        <div class="flexrow rentaltotals">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield totals totalType" data-gridtype="rental" data-caption="View" data-datafield="totalTypeRental" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow rentaltotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow rentaltotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow rentaltotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow rentaltotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" data-taxtype="Rental" style="flex:1 1 175px;"></div>
                        </div>
                         <div class="flexrow rentaltotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax 2" data-datafield="" data-enabled="false" data-totalfield="Tax2" data-taxtype="Rental" style="flex:1 1 175px; display:none;"></div>
                        </div>
                        <div class="flexrow rentaltotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Adjustments">
                        <div class="flexrow rentalAdjustments">
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield totals RentalDaysPerWeek" data-caption="D/W" data-datafield="RentalDaysPerWeek" data-digits="3" data-digitsoptional="false" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow rentalAdjustments">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield bottom_line_discount" data-caption="Disc. %" data-rectype="R" data-datafield="RentalDiscountPercent" data-digits="2" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow rentalAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals rentalOrderItemTotal bottom_line_total_tax rentalAdjustmentsPeriod" data-caption="Total" data-rectype="R" data-datafield="PeriodRentalTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow rentalAdjustments">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield rentalTotalWithTax bottom_line_total_tax rentalAdjustmentsPeriod" data-caption="Include Tax in Total" data-rectype="R" data-datafield="PeriodRentalTotalIncludesTax" style="flex:1 1 175px;margin-top:10px;"></div>
                        </div>
                        <div class="flexrow rentalAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals rentalOrderItemTotal bottom_line_total_tax rentalAdjustmentsWeekly" data-caption="Total" data-rectype="R" data-datafield="WeeklyRentalTotal" style="flex:1 1 175px; display:none;"></div>
                        </div>
                        <div class="flexrow rentalAdjustments">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield rentalTotalWithTax bottom_line_total_tax rentalAdjustmentsWeekly" data-caption="Include Tax in Total" data-rectype="R" data-datafield="WeeklyRentalTotalIncludesTax" style="flex:1 1 175px;margin-top:10px; display:none;"></div>
                        </div>
                        <div class="flexrow rentalAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals rentalOrderItemTotal bottom_line_total_tax rentalAdjustmentsMonthly" data-caption="Total" data-rectype="R" data-datafield="MonthlyRentalTotal" style="flex:1 1 175px; display:none;"></div>
                        </div>
                        <div class="flexrow rentalAdjustments">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield rentalTotalWithTax bottom_line_total_tax rentalAdjustmentsMonthly" data-caption="Include Tax in Total" data-rectype="R" data-datafield="MonthlyRentalTotalIncludesTax" style="flex:1 1 175px;margin-top:10px; display:none;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- SUBRENTAL TAB -->
              <div data-type="tabpage" id="subrentaltabpage" class="subrentalgrid notcombined tabpage" data-tabid="subrentaltab" data-render="false">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:1 1 600px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sub-Rental Items">
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="OrderSubItemGrid" data-securitycaption="Sub-Rental Items"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- SALES TAB -->
              <div data-type="tabpage" id="salestabpage" class="salesgrid notcombined tabpage" data-tabid="salestab" data-render="false">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:1 1 600px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sales Items">
                      <div class="wide-flexrow">
                        <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Sales Items"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:0 0 auto;">
                    <div class="flexcolumn summarySection" style="flex:0 0 200px;padding-right:10px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sales Totals">
                        <div class="flexrow salestotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow salestotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow salestotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow salestotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" data-taxtype="Sales" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow salestotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax 2" data-datafield="" data-enabled="false" data-totalfield="Tax2" data-taxtype="Sales" style="flex:1 1 175px; display:none;"></div>
                        </div>
                        <div class="flexrow salestotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sales Adjustments">
                        <div class="flexrow salesAdjustments">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="S" data-datafield="SalesDiscountPercent" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow salesAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals salesOrderItemTotal bottom_line_total_tax" data-caption="Total" data-rectype="S" data-datafield="SalesTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow salesAdjustments">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield salesTotalWithTax bottom_line_total_tax" data-caption="Include Tax in Total" data-rectype="S" data-datafield="SalesTotalIncludesTax" style="flex:1 1 175px;margin-top:10px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- SUBSALES TAB -->
              <div data-type="tabpage" id="subsalestabpage" class="subsalesgrid notcombined tabpage" data-tabid="subsalestab" data-render="false">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:1 1 600px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sub-Sales Items">
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="OrderSubItemGrid" data-securitycaption="Sub-Sales Items"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- MISC TAB -->
              <div data-type="tabpage" id="misctabpage" class="miscgrid notcombined tabpage" data-tabid="misctab" data-render="false">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:1 1 600px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Misc Items">
                      <div class="wide-flexrow">
                        <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Miscellaneous Items"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:0 0 auto;">
                    <div class="flexcolumn summarySection" style="flex:0 0 200px;padding-right:10px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Misc Totals">
                        <div class="flexrow misctotals">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield totals totalType" data-gridtype="misc" data-caption="View" data-datafield="totalTypeMisc" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow misctotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow misctotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow misctotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow misctotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" data-taxtype="Sales" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow misctotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax 2" data-datafield="" data-enabled="false" data-totalfield="Tax2" data-taxtype="Sales" style="flex:1 1 175px; display:none;"></div>
                        </div>
                        <div class="flexrow misctotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Misc Adjustments">
                        <div class="flexrow miscAdjustments">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="M" data-datafield="MiscDiscountPercent" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow miscAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals miscOrderItemTotal bottom_line_total_tax miscAdjustmentsPeriod" data-caption="Total" data-rectype="M" data-datafield="PeriodMiscTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow miscAdjustments">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield miscTotalWithTax bottom_line_total_tax miscAdjustmentsPeriod" data-caption="Include Tax in Total" data-rectype="M" data-datafield="PeriodMiscTotalIncludesTax" style="flex:1 1 175px;margin-top:10px;"></div>
                        </div>
                        <div class="flexrow miscAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals miscOrderItemTotal bottom_line_total_tax miscAdjustmentsWeekly" data-caption="Total" data-rectype="M" data-datafield="WeeklyMiscTotal" style="flex:1 1 175px; display:none;"></div>
                        </div>
                        <div class="flexrow miscAdjustments">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield miscTotalWithTax bottom_line_total_tax miscAdjustmentsWeekly" data-caption="Include Tax in Total" data-rectype="M" data-datafield="WeeklyMiscTotalIncludesTax" style="flex:1 1 175px;margin-top:10px; display:none;"></div>
                        </div>
                        <div class="flexrow miscAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals miscOrderItemTotal bottom_line_total_tax miscAdjustmentsMonthly" data-caption="Total" data-rectype="M" data-datafield="MonthlyMiscTotal" style="flex:1 1 175px; display:none;"></div>
                        </div>
                        <div class="flexrow miscAdjustments">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield miscTotalWithTax bottom_line_total_tax miscAdjustmentsMonthly" data-caption="Include Tax in Total" data-rectype="M" data-datafield="MonthlyMiscTotalIncludesTax" style="flex:1 1 175px;margin-top:10px; display:none;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- SUBMISC TAB -->
              <div data-type="tabpage" id="submisctabpage" class="submiscgrid notcombined tabpage" data-tabid="submisctab" data-render="false">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:1 1 600px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sub-Miscellaneous Items">
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="OrderSubItemGrid" data-securitycaption="Sub-Miscellaneous Items"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- LABOR TAB -->
              <div data-type="tabpage" id="labortabpage" class="laborgrid notcombined tabpage" data-tabid="labortab" data-render="false">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:1 1 600px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Labor Items">
                      <div class="wide-flexrow">
                        <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Labor Items"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:0 0 auto;">
                    <div class="flexcolumn labortotals laboradjustments summarySection" style="flex:0 0 200px;padding-right:10px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Labor Totals">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield totals totalType" data-gridtype="labor" data-caption="View" data-datafield="totalTypeLabor" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow labortotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow labortotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow labortotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow labortotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" data-taxtype="Labor" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow labortotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax 2" data-datafield="" data-enabled="false" data-totalfield="Tax2" data-taxtype="Labor" style="flex:1 1 175px; display:none;"></div>
                        </div>
                        <div class="flexrow labortotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Labor Adjustments">
                        <div class="flexrow laborAdjustments">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="L" data-datafield="LaborDiscountPercent" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow laborAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals laborOrderItemTotal bottom_line_total_tax laborAdjustmentsPeriod" data-caption="Total" data-rectype="L" data-datafield="PeriodLaborTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow laborAdjustments">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield laborTotalWithTax bottom_line_total_tax laborAdjustmentsPeriod" data-caption="Include Tax in Total" data-rectype="L" data-datafield="PeriodLaborTotalIncludesTax" style="flex:1 1 175px;margin-top:10px;"></div>
                        </div>
                        <div class="flexrow laborAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals laborOrderItemTotal bottom_line_total_tax laborAdjustmentsWeekly" data-caption="Total" data-rectype="L" data-datafield="WeeklyLaborTotal" style="flex:1 1 175px; display:none;"></div>
                        </div>
                        <div class="flexrow laborAdjustments">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield laborTotalWithTax bottom_line_total_tax laborAdjustmentsWeekly" data-caption="Include Tax in Total" data-rectype="L" data-datafield="WeeklyLaborTotalIncludesTax" style="flex:1 1 175px;margin-top:10px; display:none;"></div>
                        </div>
                        <div class="flexrow laborAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals laborOrderItemTotal bottom_line_total_tax laborAdjustmentsMonthly" data-caption="Total" data-rectype="L" data-datafield="MonthlyLaborTotal" style="flex:1 1 175px; display:none;"></div>
                        </div>
                        <div class="flexrow laborAdjustments">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield laborTotalWithTax bottom_line_total_tax laborAdjustmentsMonthly" data-caption="Include Tax in Total" data-rectype="L" data-datafield="MonthlyLaborTotalIncludesTax" style="flex:1 1 175px;margin-top:10px; display:none;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- SUBLABOR TAB -->
              <div data-type="tabpage" id="sublabortabpage" class="sublaborgrid notcombined tabpage" data-tabid="sublabortab" data-render="false">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:1 1 600px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sub-Labor Items">
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="OrderSubItemGrid" data-securitycaption="Sub-Labor Items"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- USED SALE TAB -->
              <div data-type="tabpage" id="usedsaletabpage" class="usedsalegrid notcombined tabpage" data-tabid="usedsaletab" data-render="false">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:1 1 600px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Used Sale Items">
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Used Sale Items"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:0 0 auto;">
                    <div class="flexcolumn usedsaletotals usedsaleadjustments summarySection" style="flex:0 0 200px;padding-right:10px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Used Sales Totals">
                        <div class="flexrow usedsaletotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:2 1 175px;"></div>
                        </div>
                        <div class="flexrow usedsaletotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow usedsaletotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:2 1 175px;"></div>
                        </div>
                        <div class="flexrow usedsaletotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" data-taxtype="Sales" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow usedsaletotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax 2" data-datafield="" data-enabled="false" data-totalfield="Tax2" data-taxtype="Sales" style="flex:1 1 175px; display:none;"></div>
                        </div>
                        <div class="flexrow usedsaletotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:2 1 175px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Used Sales Adjustments">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="RS" data-datafield="UsedSaleDiscountPercent" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals bottom_line_total_tax" data-caption="Total" data-rectype="RS" data-datafield="UsedSaleTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield bottom_line_total_tax" data-caption="Include Tax in Total" data-rectype="RS" data-datafield="UsedSaleTotalIncludesTax" style="flex:1 1 175px;margin-top:10px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- LOSS AND DAMAGE TAB -->
              <div data-type="tabpage" id="lossdamagetabpage" class="lossdamagegrid tabpage" data-tabid="lossdamagetab">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:1 1 600px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Loss &amp; Damage Items">
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Loss Damage Items"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:0 0 auto;">
                    <div class="flexcolumn summarySection" style="flex:0 0 200px;padding-right:10px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="L&D Totals">
                        <div class="flexrow lossdamagetotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow lossdamagetotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow lossdamagetotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow lossdamagetotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" data-taxtype="Sales" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow lossdamagetotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax 2" data-datafield="" data-enabled="false" data-totalfield="Tax2" data-taxtype="Sales" style="flex:1 1 175px; display:none;"></div>
                        </div>
                        <div class="flexrow lossdamagetotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="L&D Adjustments">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="F" data-datafield="LossAndDamageDiscountPercent" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals lossOrderItemTotal bottom_line_total_tax" data-caption="Total" data-rectype="F" data-datafield="LossAndDamageTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield lossTotalWithTax bottom_line_total_tax" data-caption="Include Tax in Total" data-rectype="F" data-datafield="LossAndDamageTotalIncludesTax" style="flex:1 1 175px;margin-top:10px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- ALL TAB -->
              <div data-type="tabpage" id="alltabpage" class="combinedgrid combined tabpage" data-tabid="alltab" data-render="false">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:1 1 600px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Items">
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Order Items"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:0 0 auto;">
                    <div class="flexcolumn summarySection" style="flex:0 0 200px;padding-right:10px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Totals">
                        <div class="flexrow combinedAdjustments">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield totals totalType" data-gridtype="combined" data-caption="View" data-datafield="totalTypeAll" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow combinedAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow combinedAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow combinedAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow combinedAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" data-taxtype="Sales" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow combinedAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax 2" data-datafield="" data-enabled="false" data-totalfield="Tax2" data-taxtype="Sales" style="flex:1 1 175px; display:none;"></div>
                        </div>
                        <div class="flexrow combinedAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Adjustments">
                        <div class="flexrow combinedtotals">
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield combineddw" data-caption="D/W" data-datafield="CombinedDaysPerWeek" data-digits="3" data-digitsoptional="false" style="flex:1 1 175px;display:none;" data-enabled="true"></div>
                        </div>
                        <div class="flexrow combinedtotals">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="" data-datafield="CombinedDiscountPercent" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow combinedtotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals combinedOrderItemTotal bottom_line_total_tax combinedAdjustmentsPeriod" data-caption="Total" data-rectype="" data-datafield="PeriodCombinedTotal" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow combinedtotals">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield combinedTotalWithTax bottom_line_total_tax combinedAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="" data-datafield="PeriodCombinedTotalIncludesTax" style="flex:1 1 175px;margin-top:10px;"></div>
                        </div>
                        <div class="flexrow combinedtotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals combinedOrderItemTotal bottom_line_total_tax combinedAdjustmentsWeekly" data-caption="Total" data-rectype="" data-datafield="WeeklyCombinedTotal" style="flex:1 1 175px; display:none;"></div>
                        </div>
                        <div class="flexrow combinedtotals">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield combinedTotalWithTax bottom_line_total_tax combinedAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="" data-datafield="WeeklyCombinedTotalIncludesTax" style="flex:1 1 175px;margin-top:10px; display:none;"></div>
                        </div>
                        <div class="flexrow combinedtotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals combinedOrderItemTotal bottom_line_total_tax combinedAdjustmentsMonthly" data-caption="Total" data-rectype="" data-datafield="MonthlyCombinedTotal" style="flex:1 1 175px; display:none;"></div>
                        </div>
                        <div class="flexrow combinedtotals">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield combinedTotalWithTax bottom_line_total_tax combinedAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="" data-datafield="MonthlyCombinedTotalIncludesTax" style="flex:1 1 175px;margin-top:10px; display:none;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- BILLING TAB PAGE -->
              <div data-type="tabpage" id="billingtabpage" class="tabpage" data-tabid="billingtab">
                <div class="flexrow">
                  <!-- Left column -->
                  <div class="flexcolumn" style="flex:1 1 325px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield billing_start_date date-types-disable" data-caption="Start" data-datafield="BillingStartDate" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield billing_end_date date-types-disable" data-caption="Stop" data-datafield="BillingEndDate" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield BillingWeeks week_or_month_field date-types-disable" data-caption="Weeks" data-datafield="BillingWeeks" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield BillingMonths week_or_month_field date-types-disable" data-caption="Months" data-datafield="BillingMonths" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Delay Billing Search Until" data-datafield="DelayBillingSearchUntil" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Lock Billing Dates" data-datafield="LockBillingDates" style="flex:1 1 150px;margin-top:10px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Specify Billing Dates by Type" data-datafield="SpecifyBillingDatesByType"></div>
                      </div>
                      <div class="flexrow date-types" style="display:none;">
                        <div class="flexcolumn">
                          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Billing Period">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Start" data-datafield="RentalBillingStartDate"></div>
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Stop" data-datafield="RentalBillingEndDate"></div>
                            </div>
                          </div>
                          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Facilities Billing Period">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Start" data-datafield="FacilitiesBillingStartDate"></div>
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Stop" data-datafield="FacilitiesBillingEndDate"></div>
                            </div>
                          </div>
                          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Vehicle Billing Period">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Start" data-datafield="VehicleBillingStartDate"></div>
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Stop" data-datafield="VehicleBillingEndDate"></div>
                            </div>
                          </div>
                          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Crew Billing Period">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Start" data-datafield="LaborBillingStartDate"></div>
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Stop" data-datafield="LaborBillingEndDate"></div>
                            </div>
                          </div>
                          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Misc Billing Period">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Start" data-datafield="MiscellaneousBillingStartDate"></div>
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Stop" data-datafield="MiscellaneousBillingEndDate"></div>
                            </div>
                          </div>
                        </div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="BillingCycleValidation" class="fwcontrol fwformfield" data-caption="Billing Cycle" data-datafield="BillingCycleId" data-displayfield="BillingCycle" style="flex:1 1 250px;" data-required="true"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="PaymentTermsValidation" class="fwcontrol fwformfield" data-caption="Payment Terms" data-datafield="PaymentTermsId" data-displayfield="PaymentTerms" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="PaymentTypeValidation" class="fwcontrol fwformfield" data-caption="Pay Type" data-datafield="PaymentTypeId" data-displayfield="PaymentType" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="CurrencyValidation" class="fwcontrol fwformfield" data-caption="Currency Code" data-datafield="CurrencyId" data-displayfield="CurrencyCode" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="TaxOptionValidation" class="fwcontrol fwformfield" data-caption="Tax Option" data-datafield="TaxOptionId" data-displayfield="TaxOption" style="flex:1 1 250px"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Rental Tax" data-datafield="RentalTaxRate1" data-enabled="false" style="flex:1 1 75px;"></div>
                        <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Sales Tax" data-datafield="SalesTaxRate1" data-enabled="false" style="flex:1 1 75px;"></div>
                        <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Labor Tax" data-datafield="LaborTaxRate1" data-enabled="false" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Bill Quantities From" data-datafield="DetermineQuantitiesToBillBasedOn"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield totals" data-caption="Add Prep Fees" data-datafield="IncludePrepFeesInRentalRate" style="flex:1 1 150px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Use Hiatus Schedule From" data-datafield="HiatusDiscountFrom"></div>
                      </div>
                    </div>
                  </div>
                  <!-- Center column -->
                  <div class="flexcolumn" style="flex:1 1 325px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Issued To">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Issue To" data-datafield="PrintIssuedToAddressFrom"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="IssuedToName" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="IssuedToAttention" style="flex:1 1 250px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention 2" data-datafield="IssuedToAttention2" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="IssuedToAddress1" style="flex:1 1 250px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="IssuedToAddress2" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="IssuedToCity" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <!--<div data-control="FwFormField" data-type="validation" data-validationname="StateValidation" class="fwcontrol fwformfield" data-caption="State/Province" data-datafield="IssuedToState" style="flex:1 1 100px;"></div>-->
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="IssuedToState" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="IssuedToZipCode" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="CountryValidation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="IssuedToCountryId" data-displayfield="IssuedToCountry" style="flex:1 1 250px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Options">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Check-Out items again without increasing Order quantity" data-datafield="RoundTripRentals"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Require Contact Confirmation" data-datafield="RequireContactConfirmation" style="flex:1 1 125px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Discount Reason" data-datafield="DiscountReasonId" data-displayfield="DiscountReason" data-validationname="DiscountReasonValidation" style="flex:1 1 250px; float:left;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Add to Group" data-datafield="InGroup" style="flex:1 1 150px;margin-top:10px;"></div>
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Group No" data-datafield="GroupNumber" style="flex:1 1 100px;"></div>
                      </div>
                    </div>
                  </div>
                  <!-- Right Column -->
                  <div class="flexcolumn" style="flex:1 1 275px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Bill To">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Use Other Billing Address" data-datafield="BillToAddressDifferentFromIssuedToAddress" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Name" data-datafield="BillToName" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Attention" data-datafield="BillToAttention" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Attention 2" data-datafield="BillToAttention2" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Address" data-datafield="BillToAddress1" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Address 2" data-datafield="BillToAddress2" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="City" data-datafield="BillToCity" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <!--<div data-control="FwFormField" data-type="validation" data-validationname="StateValidation" class="differentaddress fwcontrol fwformfield" data-caption="State/Province" data-datafield="BillToState" data-enabled="false" style="flex:1 1 100px;"></div>-->
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="State" data-datafield="BillToState" data-enabled="false" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="BillToZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="CountryValidation" class="differentaddress fwcontrol fwformfield" data-caption="Country" data-datafield="BillToCountryId" data-displayfield="BillToCountry" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="No Charge Order">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="No Charge Order" data-datafield="NoCharge" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="No Charge Reason" data-datafield="NoChargeReason" style="flex:1 1 300px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="flexrow">
                  <div class="flexcolumn">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Hiatus Schedule">
                      <div class="rwGrid" data-control="FwGrid" data-grid="OrderHiatusDiscountGrid" style="display:none;"></div>
                      <div class="rwGrid" data-control="FwGrid" data-grid="DealHiatusDiscountGrid"></div>
                    </div>
                  </div>
                </div>
              </div>

              <!--  PICK LIST TAB  -->
              <div data-type="tabpage" id="picklisttabpage" class="tabpage submodule picklist-submodule rwSubModule" data-tabid="picklisttab"></div>

              <!--  CONTRACT TAB  -->
              <div data-type="tabpage" id="contracttabpage" class="tabpage submodule contract-submodule rwSubModule" data-tabid="contracttab"></div>

              <!-- DELIVER/SHIP TAB -->
              <div data-type="tabpage" id="delivershiptabpage" class="tabpage" data-tabid="delivershiptab">
                <div class="flexpage">
                  <div class="flexrow">
                    <!-- Outgoing section -->
                    <div class="flexcolumn" style="flex:1 1 425px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Outgoing">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield delivery-delivery" data-caption="Type" data-datafield="OutDeliveryDeliveryType" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On Date" data-datafield="OutDeliveryTargetShipDate" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="On Time" data-datafield="OutDeliveryTargetShipTime" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Required By" data-datafield="OutDeliveryRequiredDate" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Required Time" data-datafield="OutDeliveryRequiredTime" style="flex:1 1 75px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contact" data-datafield="OutDeliveryToContact" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="OutDeliveryToContactPhone" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" data-validationname="VendorValidation" class="fwcontrol fwformfield" data-caption="Carrier" data-datafield="OutDeliveryCarrierId" data-displayfield="OutDeliveryCarrier" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="validation" data-validationname="ShipViaValidation" class="fwcontrol fwformfield" data-caption="Ship Via" data-datafield="OutDeliveryShipViaId" data-displayfield="OutDeliveryShipVia" style="flex:1 1 200px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Tracking URL" data-datafield="OutDeliveryFreightTrackingUrl" data-allcaps="false" style="display:none;flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Tracking Number" data-datafield="OutDeliveryFreightTrackingNumber" data-allcaps="false" style="flex:1 1 200px;"></div>
                          <div class="fwformcontrol track-shipment-out" data-type="button" data-enabled="false" style="flex:1 1 150px;margin:16px 10px 0px 5px;text-align:center;">Track Shipment</div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Outgoing Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield delivery-type-radio" data-caption="" data-datafield="OutDeliveryAddressType"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="OutDeliveryToLocation"></div>
                          <div data-control="FwFormField" data-type="validation" data-validationname="VenueValidation" class="fwcontrol fwformfield" data-caption="Venue" data-datafield="OutDeliveryToVenueId" data-displayfield="OutDeliveryToVenue" style="flex:1 1 200px;display:none;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="OutDeliveryToAttention"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="OutDeliveryToAddress1"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="OutDeliveryToAddress2"></div>
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
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Online Order">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order No" data-datafield="OutDeliveryOnlineOrderNumber"></div>
                          <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield online" data-caption="Status" data-datafield="OutDeliveryOnlineOrderStatus"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Incoming section -->
                    <div class="flexcolumn" style="flex:1 1 425px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Incoming">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield delivery-delivery" data-caption="Type" data-datafield="InDeliveryDeliveryType" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On Date" data-datafield="InDeliveryTargetShipDate" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="On Time" data-datafield="InDeliveryTargetShipTime" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Required By" data-datafield="InDeliveryRequiredDate" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Required Time" data-datafield="InDeliveryRequiredTime" style="flex:1 1 75px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contact" data-datafield="InDeliveryToContact" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="InDeliveryToContactPhone" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" data-validationname="VendorValidation" class="fwcontrol fwformfield" data-caption="Carrier" data-datafield="InDeliveryCarrierId" data-displayfield="InDeliveryCarrier" data-formbeforevalidate="beforeValidateCarrier" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="validation" data-validationname="ShipViaValidation" class="fwcontrol fwformfield" data-caption="Ship Via" data-datafield="InDeliveryShipViaId" data-displayfield="InDeliveryShipVia" data-formbeforevalidate="beforeValidateInShipVia" style="flex:1 1 200px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Tracking URL" data-datafield="InDeliveryFreightTrackingUrl" data-allcaps="false" style="display:none;flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Tracking Number" data-datafield="InDeliveryFreightTrackingNumber" data-allcaps="false" style="flex:1 1 200px;"></div>
                          <div class="fwformcontrol track-shipment-in" data-type="button" data-enabled="false" style="flex:1 1 150px;margin:16px 10px 0px 5px;text-align:center;">Track Shipment</div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Incoming Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield delivery-type-radio" data-caption="" data-datafield="InDeliveryAddressType"></div>
                          <div class="addresscopy fwformcontrol" data-type="button" style="flex:0 1 40px;margin:16px 5px 0px 15px;text-align:center;">Copy</div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="InDeliveryToLocation"></div>
                          <div data-control="FwFormField" data-type="validation" data-validationname="VenueValidation" class="fwcontrol fwformfield" data-caption="Venue" data-datafield="InDeliveryToVenueId" data-displayfield="InDeliveryToVenue" style="flex:1 1 200px;display:none;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="InDeliveryToAttention"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="InDeliveryToAddress1"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="InDeliveryToAddress2"></div>
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

              <!-- MANIFEST TAB -->
              <div data-type="tabpage" id="manifesttabpage" class="tabpage" data-tabid="manifesttab" data-render="false">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:0 1 175px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Total">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Value" data-datafield="OrderValueTotal" data-enabled="false"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Replacement Cost" data-datafield="OrderReplacementTotal" data-enabled="false"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Owned Total">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Value" data-datafield="OwnedValueTotal" data-enabled="false"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Replacement Cost" data-datafield="OwnedReplacementTotal" data-enabled="false"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sub-Rental Total">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Value" data-datafield="SubValueTotal" data-enabled="false"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Replacement Cost" data-datafield="SubReplacementTotal" data-enabled="false"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Valuation">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="rentalValueSelector"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="salesValueSelector"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:1 1 450px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Manifest Items">
                      <div class="wideflexrow">
                        <div class="flexcolumn">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="View Items" data-datafield="manifestItems"></div>
                          </div>
                        </div>
                        <div class="flexcolumn">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Filter By" data-datafield="manifestFilter"></div>
                          </div>
                        </div>
                      </div>
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="OrderManifestGrid" data-securitycaption=""></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:0 1 200px;padding-right:10px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Piece Count">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Shipping Containers" data-datafield="ShippingContainerTotal" data-enabled="false"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Stand-Alone Items" data-datafield="StandAloneItemTotal" data-enabled="false"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Piece Count" data-datafield="PieceCountTotal" data-enabled="false"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Total Items">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Shipping Items" data-datafield="ShippingItemTotal" data-enabled="false"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Weight">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="" data-datafield="weightSelector"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Pounds" data-datafield="ExtendedWeightTotalLbs" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Kilograms" data-datafield="ExtendedWeightTotalKg" data-enabled="false" style="display:none;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Ounces" data-datafield="ExtendedWeightTotalOz" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Grams" data-datafield="ExtendedWeightTotalGm" data-enabled="false" style="display:none;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- SUB PURCHASE ORDER TAB -->
              <div data-type="tabpage" id="subpurchaseordertabpage" class="tabpage purchaseorder-submodule rwSubModule" data-tabid="subpurchaseordertab"></div>

              <!-- INVOICE tab -->
              <div data-type="tabpage" id="invoicetabpage" class="tabpage invoice-submodule rwSubModule" data-tabid="invoicetab"></div>

              <!-- REPAIR TAB -->
              <div data-type="tabpage" id="repairtabpage" class="tabpage submodule repair-submodule rwSubModule" data-tabid="repairtab"></div>

              <!-- DOCUMENTS TAB -->
              <div data-type="tabpage" id="documentstabpage" class="tabpage" data-tabid="documentstab">
                <div class="wideflexrow">
                  <div class="rwGrid" data-control="FwGrid" data-grid="OrderDocumentGrid"></div>
                </div>
              </div>

              <!-- NOTES TAB -->
              <div data-type="tabpage" id="notetabpage" class="tabpage" data-tabid="notetab">
                <div class="wideflexrow">
                  <div class="rwGrid" data-control="FwGrid" data-grid="OrderNoteGrid" data-securitycaption="Notes"></div>
                </div>
              </div>

              <!-- HISTORY TAB -->
              <div data-type="tabpage" id="historytabpage" class="tabpage" data-tabid="historytab">
                <div class="flexrow">
                  <div class="rwGrid" data-control="FwGrid" data-grid="OrderStatusHistoryGrid" data-securitycaption="Order Status History"></div>
                </div>
                <div class="flexrow">
                  <div class="rwGrid" data-control="FwGrid" data-grid="OrderSnapshotGrid" data-securitycaption="Order Snapshot"></div>
                </div>
              </div>

              <!-- EMAIL HISTORY TAB -->
              <div data-type="tabpage" id="emailhistorytabpage" class="tabpage submodule emailhistory-page rwSubModule" data-tabid="emailhistorytab"></div>

            </div>
          </div>
        </div>
        `;
    }
    //----------------------------------------------------------------------------------------------
    cancelPickList(pickListId, pickListNumber, $form) {
        var $confirmation, $yes, $no;
        var orderId = FwFormField.getValueByDataField($form, 'OrderId');
        $confirmation = FwConfirmation.renderConfirmation('Cancel Pick List', '<div style="white-space:pre;">\n' +
            'Cancel Pick List ' + pickListNumber + '?</div>');
        $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
        $no = FwConfirmation.addButton($confirmation, 'No');
        $yes.on('click', function () {
            FwAppData.apiMethod(true, 'DELETE', 'api/v1/picklist/' + pickListId, {}, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    FwNotification.renderNotification('SUCCESS', 'Pick List Cancelled');
                    FwConfirmation.destroyConfirmation($confirmation);
                    var $pickListGridControl = $form.find('[data-name="OrderPickListGrid"]');
                    $pickListGridControl.data('ondatabind', function (request) {
                        request.uniqueids = {
                            OrderId: orderId
                        };
                    });
                    FwBrowse.search($pickListGridControl);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, $form);
        });
    };
    //----------------------------------------------------------------------------------------------
    // Form menu item -- corresponding grid menu item function in OrderSnapshotGrid controller        -- j.pace removed at request of justin 12.23.19 #1521
    //viewSnapshotOrder(event: any) {
    //    let $orderForm, $selectedCheckBoxes, $orderSnapshotGrid, snapshotId, orderNumber;

    //    $orderSnapshotGrid = $form.find(`[data-name="OrderSnapshotGrid"]`);
    //    $selectedCheckBoxes = $orderSnapshotGrid.find('tbody .cbselectrow:checked');

    //    try {
    //        if ($selectedCheckBoxes.length !== 0) {
    //            for (let i = 0; i < $selectedCheckBoxes.length; i++) {
    //                snapshotId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="SnapshotId"]').attr('data-originalvalue');
    //                orderNumber = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderNumber"]').attr('data-originalvalue');
    //                var orderInfo: any = {};
    //                orderInfo.OrderId = snapshotId;
    //                $orderForm = OrderController.openForm('EDIT', orderInfo);
    //                FwModule.openSubModuleTab($form, $orderForm);
    //                jQuery('.tab.submodule.active').find('.caption').html(`Snapshot for Order ${orderNumber}`);
    //            }
    //        } else {
    //            FwNotification.renderNotification('WARNING', 'Select rows in Order Snapshot Grid in order to perform this function.');
    //        }
    //    }
    //    catch (ex) {
    //        FwFunc.showError(ex);
    //    }
    //};
    //----------------------------------------------------------------------------------------------
    addLossDamage(event: any): void {
        const $form = jQuery(event.currentTarget).closest('.fwform');
        if ($form.attr('data-mode') !== 'NEW') {
            let sessionId, $lossAndDamageItemGridControl;
            const userWarehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
            const userLocationId = JSON.parse(sessionStorage.getItem('location')).locationid;
            const dealId = FwFormField.getValueByDataField($form, 'DealId');
            const HTML: Array<string> = [];
            HTML.push(
                `<div class="fwcontrol fwcontainer fwform popup" data-control="FwContainer" data-type="form" data-caption="Loss and Damage">
              <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div class="tabpages">
                  <div class="formpage">
                      <div class="formrow">
                        <div class="formcolumn" style="width:100%;margin-top:50px;">
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div class="fwform-section-title" style="margin-bottom:20px;">Loss and Damage</div>
                            <div class="formrow error-msg"></div>
                            <div class="formrow sub-header" style="margin-left:8px;font-size:16px;"><span>Select one or more Orders with Lost or Damaged items, then click Continue</span></div>
                            <div data-control="FwGrid" class="container"></div>
                          </div>
                        </div>
                      </div>
                      <div class="formrow add-button">
                        <div class="select-items fwformcontrol" data-type="button" style="float:right;">Continue</div>
                      </div>
                      <div class="formrow session-buttons" style="display:none;">
                        <div class="options-button fwformcontrol" data-type="button" style="float:left">Options &#8675;</div>
                        <div class="selectall fwformcontrol" data-type="button" style="float:left; margin-left:10px;">Select All</div>
                        <div class="selectnone fwformcontrol" data-type="button" style="float:left; margin-left:10px;">Select None</div>
                        <div class="complete-session fwformcontrol" data-type="button" style="float:right;">Add To Order</div>
                      </div>
                      <div class="formrow option-list" style="display:none;">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Placeholder..." data-datafield=""></div>
                      </div>
                    </div>
                  </div>
                </div>
            </div>`
            );

            const addOrderBrowse = () => {
                let $browse = jQuery(this.getBrowseTemplate());
                $browse.attr('data-hasmultirowselect', 'true');
                $browse.attr('data-type', 'Browse');
                $browse.attr('data-showsearch', 'false');
                FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
                    if (dt.Rows[rowIndex][dt.ColumnIndex['Status']] === 'CANCELLED') {
                        $tr.css('color', '#aaaaaa');
                    }
                });

                $browse = FwModule.openBrowse($browse);
                $browse.find('.fwbrowse-menu').hide();

                $browse.data('ondatabind', function (request) {
                    request.ActiveViewFields = { Status: ["ALL"], LocationId: [userLocationId] };
                    request.pagesize = 16;
                    request.orderby = 'OrderDate desc';
                    request.miscfields = {
                        LossAndDamage: true,
                        LossAndDamageWarehouseId: userWarehouseId,
                        LossAndDamageDealId: dealId
                    }
                });
                return $browse;
            };

            const startLDSession = (): void => {
                let $browse = jQuery($popup).children().find('.fwbrowse');
                let orderId, $selectedCheckBoxes: any, orderIds: string = '';
                $selectedCheckBoxes = $browse.find('tbody .cbselectrow:checked');
                if ($selectedCheckBoxes.length !== 0) {
                    for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                        orderId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');
                        if (orderId) {
                            orderIds = orderIds.concat(', ', orderId);
                        }
                    }
                    orderIds = orderIds.substring(2);

                    const request: any = {};
                    request.OrderIds = orderIds;
                    request.DealId = dealId;
                    request.WarehouseId = userWarehouseId;
                    FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/startsession`, request, FwServices.defaultTimeout, response => {
                        sessionId = response.SessionId
                        this.lossDamageSessionId = sessionId;
                        if (sessionId) {
                            $popup.find('.container').html('<div class="formrow"><div data-control="FwGrid" data-grid="LossAndDamageItemGrid" data-securitycaption=""></div></div>');
                            $popup.find('.add-button').hide();
                            $popup.find('.sub-header').hide();
                            $popup.find('.session-buttons').show();
                            const $lossAndDamageItemGrid = $popup.find('div[data-grid="LossAndDamageItemGrid"]');
                            $lossAndDamageItemGridControl = FwBrowse.loadGridFromTemplate('LossAndDamageItemGrid');
                            $lossAndDamageItemGrid.data('sessionId', sessionId);
                            $lossAndDamageItemGrid.data('orderId', orderId);
                            $lossAndDamageItemGrid.empty().append($lossAndDamageItemGridControl);
                            $lossAndDamageItemGridControl.data('ondatabind', request => {
                                request.uniqueids = {
                                    SessionId: sessionId
                                };
                            });
                            FwBrowse.init($lossAndDamageItemGridControl);
                            FwBrowse.renderRuntimeHtml($lossAndDamageItemGridControl);
                            FwBrowse.search($lossAndDamageItemGridControl);
                        }
                    }, null, $browse);
                } else {
                    FwNotification.renderNotification('WARNING', 'Select rows in order to perform this function.');
                }
            };
            const events = () => {
                let $orderItemGridLossDamage = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');
                // Starts LD session
                $popup.find('.select-items').on('click', event => {
                    startLDSession();
                });
                // Complete Session
                $popup.find('.complete-session').on('click', event => {
                    let $lossAndDamageItemGrid = $popup.find('div[data-grid="LossAndDamageItemGrid"]');
                    $lossAndDamageItemGrid = jQuery($lossAndDamageItemGrid);
                    let request: any = {};
                    request.SourceOrderId = FwFormField.getValueByDataField($form, 'OrderId');
                    request.SessionId = this.lossDamageSessionId
                    FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/completesession`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        if (response.success === true) {
                            FwPopup.destroyPopup($popup);
                            FwBrowse.search($orderItemGridLossDamage);
                        } else {
                            FwNotification.renderNotification('ERROR', response.msg);
                        }
                    }, null, $lossAndDamageItemGrid)
                });
                // Select All
                $popup.find('.selectall').on('click', e => {
                    let $lossAndDamageItemGrid = $popup.find('div[data-grid="LossAndDamageItemGrid"]');
                    $lossAndDamageItemGrid = jQuery($lossAndDamageItemGrid);

                    const request: any = {};
                    request.SessionId = this.lossDamageSessionId;
                    FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/selectall`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        $popup.find('.error-msg').html('');
                        if (response.success === false) {
                            FwFunc.playErrorSound();
                            $popup.find('div.error-msg').html(`<div><span>${response.msg}</span></div>`);
                        } else {
                            FwFunc.playSuccessSound();
                            FwBrowse.search($lossAndDamageItemGridControl);
                        }
                    }, function onError(response) {
                        FwFunc.showError(response);
                    }, $lossAndDamageItemGrid);
                });
                // Select None
                $popup.find('.selectnone').on('click', e => {
                    let $lossAndDamageItemGrid = $popup.find('div[data-grid="LossAndDamageItemGrid"]');
                    $lossAndDamageItemGrid = jQuery($lossAndDamageItemGrid);

                    const request: any = {};
                    request.SessionId = this.lossDamageSessionId;
                    FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/selectnone`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        $popup.find('.error-msg').html('');
                        if (response.success === false) {
                            FwFunc.playErrorSound();
                            FwBrowse.search($lossAndDamageItemGridControl);
                            $popup.find('div.error-msg').html(`<div><span">${response.msg}</span></div>`);
                        } else {
                            FwFunc.playSuccessSound();
                            FwBrowse.search($lossAndDamageItemGridControl);
                        }
                    }, function onError(response) {
                        FwFunc.showError(response);
                    }, $lossAndDamageItemGrid);
                });
                //Options button
                $popup.find('.options-button').on('click', e => {
                    $popup.find('div.formrow.option-list').toggle();
                });
            }
            const $popupHtml = HTML.join('');
            const $popup = FwPopup.renderPopup(jQuery($popupHtml), { ismodal: true }, 'Loss and Damage');
            FwPopup.showPopup($popup);
            const $orderBrowse = addOrderBrowse();
            $popup.find('.container').append($orderBrowse);
            FwBrowse.search($orderBrowse);
            events();
        } else {
            FwNotification.renderNotification('WARNING', 'Save the record before performing this function.');
        }
    }
    //----------------------------------------------------------------------------------------------
    retireLossDamage(event: any): void {
        const $form = jQuery(event.currentTarget).closest('.fwform');
        if ($form.attr('data-mode') !== 'NEW') {
            const $confirmation = FwConfirmation.renderConfirmation('Confirm?', '');
            $confirmation.find('.fwconfirmationbox').css('width', '450px');

            const html: Array<string> = [];;
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div>Create a Lost Contract and Retire the Items?</div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));
            const $yes = FwConfirmation.addButton($confirmation, 'Retire', false);
            const $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', retireLD);

            function retireLD() {
                const orderId = FwFormField.getValueByDataField($form, 'OrderId');
                const request: any = {}
                request.OrderId = orderId;
                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                FwFormField.disable($no);
                $yes.text('Retiring...');
                $yes.off('click');
                const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
                const $realConfirm = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);

                FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/retire`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.success === true) {
                        const uniqueids: any = {};
                        uniqueids.ContractId = response.ContractId;
                        FwNotification.renderNotification('SUCCESS', 'Order Successfully Retired');
                        FwConfirmation.destroyConfirmation($confirmation);
                        const $contractForm = ContractController.loadForm(uniqueids);
                        FwModule.openModuleTab($contractForm, "", true, 'FORM', true)
                        FwModule.refreshForm($form);
                    }
                }, function onError(response) {
                    $yes.on('click', retireLD);
                    $yes.text('Retire');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                    FwModule.refreshForm($form);
                }, $realConfirm);
            }
        } else {
            FwNotification.renderNotification('WARNING', 'Save the record before performing this function.');
        }
    }
    //----------------------------------------------------------------------------------------------
    createSnapshotOrder($form: JQuery): void {
        let orderNumber, orderId, $orderSnapshotGrid;
        orderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        $orderSnapshotGrid = $form.find('[data-name="OrderSnapshotGrid"]');

        let $confirmation, $yes, $no;

        $confirmation = FwConfirmation.renderConfirmation('Create Snapshot', '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');
        let html = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push(`    <div>Create a Snapshot for Order ${orderNumber}?</div>`);
        html.push('  </div>');
        html.push('</div>');

        FwConfirmation.addControls($confirmation, html.join(''));

        $yes = FwConfirmation.addButton($confirmation, 'Create Snapshot', false);
        $no = FwConfirmation.addButton($confirmation, 'Cancel');

        $yes.on('click', createSnapshot);
        let $confirmationbox = jQuery('.fwconfirmationbox');
        function createSnapshot() {
            FwAppData.apiMethod(true, 'POST', `api/v1/order/createsnapshot/${orderId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Snapshot Successfully Created.');
                FwConfirmation.destroyConfirmation($confirmation);

                $orderSnapshotGrid.data('ondatabind', request => {
                    request.uniqueids = {
                        OrderId: orderId,
                    }
                    request.pagesize = 10;
                    request.orderby = "OrderDate"
                });

                $orderSnapshotGrid.data('beforesave', request => {
                    request.OrderId = orderId;
                });

                FwBrowse.search($orderSnapshotGrid);
            }, null, $confirmationbox);
        }
    };
    //----------------------------------------------------------------------------------------------
    OrderOnHold($form: JQuery): void {
        const orderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
        const orderId = FwFormField.getValueByDataField($form, 'OrderId');
        const status = FwFormField.getValueByDataField($form, 'Status');
        let $confirmation, $yes, $no, html = [];
        if (status === 'HOLD') {
            $confirmation = FwConfirmation.renderConfirmation('Order Hold', '');
            $confirmation.find('.fwconfirmationbox').css('width', '450px');
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push(`    <div>Remove Hold for Order ${orderNumber}?</div>`);
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));

            $yes = FwConfirmation.addButton($confirmation, 'Continue', false);
            $no = FwConfirmation.addButton($confirmation, 'Cancel');
        } else {
            $confirmation = FwConfirmation.renderConfirmation('Order Hold', '');
            $confirmation.find('.fwconfirmationbox').css('width', '450px');
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push(`    <div>Put Order ${orderNumber} On Hold?</div>`);
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));

            $yes = FwConfirmation.addButton($confirmation, 'Continue', false);
            $no = FwConfirmation.addButton($confirmation, 'Cancel');
        }

        $yes.on('click', putOnRemoveHold);
        let $confirmationbox = jQuery('.fwconfirmationbox');
        function putOnRemoveHold() {
            FwConfirmation.destroyConfirmation($confirmation);
            FwAppData.apiMethod(true, 'POST', `api/v1/order/onhold/${orderId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success === true) {
                    if (status === 'HOLD') {
                        FwNotification.renderNotification('SUCCESS', 'Hold Removed From Order');
                    } else {
                        FwNotification.renderNotification('SUCCESS', 'Order Is Now On Hold');
                    }
                    FwModule.refreshForm($form)
                } else if (response.success === false) {
                    FwNotification.renderNotification('ERROR', `${response.msg}`);
                }
            }, null, $form);
        }
    }
    //----------------------------------------------------------------------------------------------
    cancelUncancel($browse: JQuery) {
        let $confirmation, $yes, $no;
        let orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
        let orderStatus = $browse.find('.selected [data-formdatafield="Status"]').attr('data-originalvalue');
        if (orderId != null) {
            if (orderStatus === "CANCELLED") {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                let html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to un-cancel this Order?</div>');
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Un-Cancel Order', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', uncancelOrder);
            }
            else {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                let html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to cancel this Order?</div>');
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Cancel Order', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', cancelOrder);
            }

            function cancelOrder() {
                let request: any = {};

                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Canceling...');
                $yes.off('click');

                FwAppData.apiMethod(true, 'POST', `api/v1/order/cancel/${orderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Order Successfully Cancelled');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwBrowse.databind($browse);
                }, function onError(response) {
                    $yes.on('click', cancelOrder);
                    $yes.text('Cancel');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                    FwBrowse.databind($browse);
                }, $browse);
            };

            function uncancelOrder() {
                let request: any = {};

                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Retrieving...');
                $yes.off('click');

                FwAppData.apiMethod(true, 'POST', `api/v1/order/uncancel/${orderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Order Successfully Retrieved');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwBrowse.databind($browse);
                }, function onError(response) {
                    $yes.on('click', uncancelOrder);
                    $yes.text('Cancel');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                    FwBrowse.databind($browse);
                }, $browse);
            };
        } else {
            FwNotification.renderNotification('WARNING', 'Select an Order to perform this action.');
        }
    }
    //---------------------------------------------------------------------------------
    search($form: JQuery) {
        let orderId = FwFormField.getValueByDataField($form, 'OrderId');
        if ($form.attr('data-mode') === 'NEW') {
            OrderController.saveForm($form, { closetab: false });
            return;
        }
        if (orderId == "") {
            FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
        } else {
            let search = new SearchInterface();
            search.renderSearchPopup($form, orderId, 'Order');
        }
    }
    //---------------------------------------------------------------------------------
    createPickList($form: JQuery) {
        const mode = 'EDIT';
        const orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        const $pickListForm = CreatePickListController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $pickListForm);
        const $tab = FwTabs.getTabByElement($pickListForm);
        $tab.find('.caption').html('New Pick List');
        const $pickListUtilityGrid = $pickListForm.find('[data-name="PickListUtilityGrid"]');
        FwBrowse.search($pickListUtilityGrid);
    }
    //---------------------------------------------------------------------------------
    orderStatus($form: JQuery) {
        const mode = 'EDIT';
        const orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        orderInfo.OrderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
        const $orderStatusForm = OrderStatusController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $orderStatusForm);
        const $tab = FwTabs.getTabByElement($orderStatusForm);
        $tab.find('.caption').html('Order Status');
    }
    //----------------------------------------------------------------------------------------------
    checkIn($form: JQuery) {
        const mode = 'EDIT';
        const orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        orderInfo.OrderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
        const $checkinForm = CheckInController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $checkinForm);
        const $tab = FwTabs.getTabByElement($checkinForm);
        $tab.find('.caption').html('Check-In');
    }
    //----------------------------------------------------------------------------------------------
    checkOut($form: JQuery) {
        const mode = 'EDIT';
        const orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        orderInfo.OrderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
        orderInfo.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
        orderInfo.Warehouse = $form.find('div[data-datafield="WarehouseId"] input.fwformfield-text').val();
        orderInfo.DealId = FwFormField.getValueByDataField($form, 'DealId');
        orderInfo.Deal = $form.find('div[data-datafield="DealId"] input.fwformfield-text').val();
        const $stagingCheckoutForm = StagingCheckoutController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $stagingCheckoutForm);
        const $tab = FwTabs.getTabByElement($stagingCheckoutForm);
        $tab.find('.caption').html('Staging / Check-Out');
    }
    //----------------------------------------------------------------------------------------------
    browseCancelOption($browse: JQuery) {
        try {
            let $confirmation, $yes, $no;
            let orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
            let orderStatus = $browse.find('.selected [data-formdatafield="Status"]').attr('data-originalvalue');
            if (orderId != null) {
                if (orderStatus === "CANCELLED") {
                    $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                    $confirmation.find('.fwconfirmationbox').css('width', '450px');
                    let html = [];
                    html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                    html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                    html.push('    <div>Would you like to un-cancel this Order?</div>');
                    html.push('  </div>');
                    html.push('</div>');

                    FwConfirmation.addControls($confirmation, html.join(''));
                    $yes = FwConfirmation.addButton($confirmation, 'Un-Cancel Order', false);
                    $no = FwConfirmation.addButton($confirmation, 'Cancel');

                    $yes.on('click', uncancelOrder);
                }
                else {
                    $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                    $confirmation.find('.fwconfirmationbox').css('width', '450px');
                    let html = [];
                    html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                    html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                    html.push('    <div>Would you like to cancel this Order?</div>');
                    html.push('  </div>');
                    html.push('</div>');

                    FwConfirmation.addControls($confirmation, html.join(''));
                    $yes = FwConfirmation.addButton($confirmation, 'Cancel Order', false);
                    $no = FwConfirmation.addButton($confirmation, 'Cancel');

                    $yes.on('click', cancelOrder);
                }

                function cancelOrder() {
                    let request: any = {};

                    FwFormField.disable($confirmation.find('.fwformfield'));
                    FwFormField.disable($yes);
                    $yes.text('Canceling...');
                    $yes.off('click');

                    FwAppData.apiMethod(true, 'POST', `api/v1/order/cancel/${orderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        FwNotification.renderNotification('SUCCESS', 'Order Successfully Cancelled');
                        FwConfirmation.destroyConfirmation($confirmation);
                        FwBrowse.databind($browse);
                    }, function onError(response) {
                        $yes.on('click', cancelOrder);
                        $yes.text('Cancel');
                        FwFunc.showError(response);
                        FwFormField.enable($confirmation.find('.fwformfield'));
                        FwFormField.enable($yes);
                        FwBrowse.databind($browse);
                    }, $browse);
                };

                function uncancelOrder() {
                    let request: any = {};

                    FwFormField.disable($confirmation.find('.fwformfield'));
                    FwFormField.disable($yes);
                    $yes.text('Retrieving...');
                    $yes.off('click');

                    FwAppData.apiMethod(true, 'POST', `api/v1/order/uncancel/${orderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        FwNotification.renderNotification('SUCCESS', 'Order Successfully Retrieved');
                        FwConfirmation.destroyConfirmation($confirmation);
                        FwBrowse.databind($browse);
                    }, function onError(response) {
                        $yes.on('click', uncancelOrder);
                        $yes.text('Cancel');
                        FwFunc.showError(response);
                        FwFormField.enable($confirmation.find('.fwformfield'));
                        FwFormField.enable($yes);
                        FwBrowse.databind($browse);
                    }, $browse);
                };
            } else {
                FwNotification.renderNotification('WARNING', 'Select an Order to perform this action.');
            }
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
}
//----------------------------------------------------------------------------------------------
var OrderController = new Order();