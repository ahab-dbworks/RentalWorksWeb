routes.push({ pattern: /^module\/partsinventory$/, action: function (match: RegExpExecArray) { return PartsInventoryController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class PartsInventory extends InventoryBase {
    Module: string = 'PartsInventory';
    apiurl: string = 'api/v1/partsinventory';
    caption: string = Constants.Modules.Inventory.children.PartsInventory.caption;
    nav: string = Constants.Modules.Inventory.children.PartsInventory.nav;
    id: string = Constants.Modules.Inventory.children.PartsInventory.id;
    AvailableFor: string = "P";
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        // ----------

        //Inventory Location Tax grid
        FwBrowse.renderGrid({
            nameGrid: 'InventoryLocationTaxGrid',
            gridSecurityId: 'dpDtvVrXRZrd',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            // getBaseApiUrl: (): string => { return `${this.apiurl}/${FwFormField.getValueByDataField($form, 'InventoryId')}/aka`; },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            }
        });
        // ----------
        //const $partsInventoryWarehouseGrid: any = $form.find('div[data-grid="PartsInventoryWarehouseGrid"]');
        //const $partsInventoryWarehouseGridControl: any = FwBrowse.loadGridFromTemplate('PartsInventoryWarehouseGrid');
        //$partsInventoryWarehouseGrid.empty().append($partsInventoryWarehouseGridControl);
        //$partsInventoryWarehouseGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //    request.miscfields = {
        //        UserWarehouseId: warehouse.warehouseid
        //    };
        //    request.pagesize = 100;  //justin 04/01/2019 #359 show all active warehouses here
        //});
        //$partsInventoryWarehouseGridControl.data('beforesave', request => {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($partsInventoryWarehouseGridControl);
        //FwBrowse.renderRuntimeHtml($partsInventoryWarehouseGridControl);

        //Parts Inventory Warehouse Grid
        FwBrowse.renderGrid({
            nameGrid: 'PartsInventoryWarehouseGrid',
            gridSecurityId: 'g8sCuKjUVrW1',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
                options.hasEdit = true;
            },
            // getBaseApiUrl: (): string => { return `${this.apiurl}/${FwFormField.getValueByDataField($form, 'InventoryId')}/aka`; },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
                request.miscfields = {
                    UserWarehouseId: warehouse.warehouseid
                };
                request.pagesize = 100;
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            }
        });
        // ----------
        //const $partsInventoryWarehousePricingGrid: any = $form.find('div[data-grid="PartsInventoryWarehousePricingGrid"]');
        //const $partsInventoryWarehouseGridPricingControl: any = FwBrowse.loadGridFromTemplate('PartsInventoryWarehousePricingGrid');
        //$partsInventoryWarehousePricingGrid.empty().append($partsInventoryWarehouseGridPricingControl);
        //$partsInventoryWarehouseGridPricingControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //    request.miscfields = {
        //        UserWarehouseId: warehouse.warehouseid
        //    };
        //    request.pagesize = 100;  //justin 04/01/2019 #359 show all active warehouses here
        //});
        //$partsInventoryWarehouseGridPricingControl.data('beforesave', request => {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($partsInventoryWarehouseGridPricingControl);
        //FwBrowse.renderRuntimeHtml($partsInventoryWarehouseGridPricingControl);

        //Parts Inventory Warehouse Pricing Grid
        FwBrowse.renderGrid({
            nameGrid: 'PartsInventoryWarehousePricingGrid',
            gridSecurityId: 'g8sCuKjUVrW1',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
                options.hasEdit = true;
                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid1');
                FwMenu.addSubMenuItem($viewgroup, 'View Rates in local Currencies', '', (e: JQuery.ClickEvent) => {
                    try {
                        this.currencyViewForPricingGrids(e, 'local');
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'View Rates in a specific Currency', '', (e: JQuery.ClickEvent) => {
                    try {
                        this.currencyViewForPricingGrids(e, 'specific');
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'View Rates in All Currencies', '', (e: JQuery.ClickEvent) => {
                    try {
                        this.currencyViewForPricingGrids(e, 'all');
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            // getBaseApiUrl: (): string => { return `${this.apiurl}/${FwFormField.getValueByDataField($form, 'InventoryId')}/aka`; },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
                request.miscfields = {
                    UserWarehouseId: warehouse.warehouseid
                };
                request.pagesize = 100;  //justin 04/01/2019 #359 show all active warehouses here
            },
            beforeSave: (request: any, $browse, $tr) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                request.CurrencyId = $tr.find('.field[data-browsedatafield="CurrencyId"]').attr('data-originalvalue');
            }
        });

        FwBrowse.renderGrid({
            nameGrid: 'InventoryWarehouseCompletePricingGrid',
            gridSecurityId: 'g8sCuKjUVrW1',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid1');
                FwMenu.addSubMenuItem($viewgroup, 'View Rates in local Currencies', '', (e: JQuery.ClickEvent) => {
                    try {
                        this.currencyViewForPricingGrids(e, 'local');
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'View Rates in a specific Currency', '', (e: JQuery.ClickEvent) => {
                    try {
                        this.currencyViewForPricingGrids(e, 'specific');
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'View Rates in All Currencies', '', (e: JQuery.ClickEvent) => {
                    try {
                        this.currencyViewForPricingGrids(e, 'all');
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
                request.miscfields = {
                    UserWarehouseId: warehouse.warehouseid
                };
                request.pagesize = 100;  //justin 04/01/2019 #359 show all active warehouses here
            },
            beforeSave: (request: any, $browse, $tr) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                request.CurrencyId = $tr.find('.field[data-browsedatafield="CurrencyId"]').attr('data-originalvalue');
            }
        });

        FwBrowse.renderGrid({
            nameGrid: 'InventoryWarehouseKitPricingGrid',
            gridSecurityId: 'g8sCuKjUVrW1',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid1');
                FwMenu.addSubMenuItem($viewgroup, 'View Rates in local Currencies', '', (e: JQuery.ClickEvent) => {
                    try {
                        this.currencyViewForPricingGrids(e, 'local');
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'View Rates in a specific Currency', '', (e: JQuery.ClickEvent) => {
                    try {
                        this.currencyViewForPricingGrids(e, 'specific');
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'View Rates in All Currencies', '', (e: JQuery.ClickEvent) => {
                    try {
                        this.currencyViewForPricingGrids(e, 'all');
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
                request.miscfields = {
                    UserWarehouseId: warehouse.warehouseid
                };
                request.pagesize = 100;  //justin 04/01/2019 #359 show all active warehouses here
            },
            beforeSave: (request: any, $browse, $tr) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                request.CurrencyId = $tr.find('.field[data-browsedatafield="CurrencyId"]').attr('data-originalvalue');
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'InventoryCompleteKitGrid',
            gridSecurityId: 'gflkb5sQf7it',
            moduleSecurityId: this.id,
            $form: $form,
            // getBaseApiUrl: (): string => { return `${this.apiurl}/${FwFormField.getValueByDataField($form, 'InventoryId')}/aka`; }, 
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'PartsInventorySubstituteGrid',
            gridSecurityId: '5sN9zKtGzNTq',
            moduleSecurityId: this.id,
            $form: $form,
            // getBaseApiUrl: (): string => { return `${this.apiurl}/${FwFormField.getValueByDataField($form, 'InventoryId')}/aka`; },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            }
        });
        // ----------
        //const $partsinventoryCompatibilityGrid: any = $form.find('div[data-grid="PartsInventoryCompatibilityGrid"]');
        //const $partsinventoryCompatibilityGridControl: any = FwBrowse.loadGridFromTemplate('PartsInventoryCompatibilityGrid');
        //$partsinventoryCompatibilityGrid.empty().append($partsinventoryCompatibilityGridControl);
        //$partsinventoryCompatibilityGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //$partsinventoryCompatibilityGridControl.data('beforesave', request => {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($partsinventoryCompatibilityGridControl);
        //FwBrowse.renderRuntimeHtml($partsinventoryCompatibilityGridControl);


        //Parts Inventory Compatibility Grid
        FwBrowse.renderGrid({
            nameGrid: 'PartsInventoryCompatibilityGrid',
            gridSecurityId: 'mlAKf5gRPNNI',
            moduleSecurityId: this.id,
            $form: $form,
            // getBaseApiUrl: (): string => { return `${this.apiurl}/${FwFormField.getValueByDataField($form, 'InventoryId')}/aka`; }, 
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            }
        });
        // ----------
        //const $inventoryAttributeValueGrid: any = $form.find('div[data-grid="InventoryAttributeValueGrid"]');
        //const $inventoryAttributeValueGridControl: any = FwBrowse.loadGridFromTemplate('InventoryAttributeValueGrid');
        //$inventoryAttributeValueGrid.empty().append($inventoryAttributeValueGridControl);
        //$inventoryAttributeValueGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //$inventoryAttributeValueGridControl.data('beforesave', request => {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($inventoryAttributeValueGridControl);
        //FwBrowse.renderRuntimeHtml($inventoryAttributeValueGridControl);

        //Inventory Attribute Value Grid
        FwBrowse.renderGrid({
            nameGrid: 'InventoryAttributeValueGrid',
            gridSecurityId: 'CntxgVXDQtQ7',
            moduleSecurityId: this.id,
            $form: $form,
            // getBaseApiUrl: (): string => { return `${this.apiurl}/${FwFormField.getValueByDataField($form, 'InventoryId')}/aka`; },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            }
        });
        // ----------
        //const $inventoryPrepGrid: any = $form.find('div[data-grid="InventoryPrepGrid"]');
        //const $inventoryPrepGridControl: any = FwBrowse.loadGridFromTemplate('InventoryPrepGrid');
        //$inventoryPrepGrid.empty().append($inventoryPrepGridControl);
        //$inventoryPrepGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //$inventoryPrepGridControl.data('beforesave', request => {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($inventoryPrepGridControl);
        //FwBrowse.renderRuntimeHtml($inventoryPrepGridControl);

        //Inventory Prep Grid
        FwBrowse.renderGrid({
            nameGrid: 'InventoryPrepGrid',
            gridSecurityId: 'CzNh6kOVsRO4',
            moduleSecurityId: this.id,
            $form: $form,
            // getBaseApiUrl: (): string => { return `${this.apiurl}/${FwFormField.getValueByDataField($form, 'InventoryId')}/aka`; },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            }
        });
        // ----------
        //const $inventoryCompleteGrid: any = $form.find('div[data-grid="InventoryCompleteGrid"]');
        //const $inventoryCompleteGridControl: any = FwBrowse.loadGridFromTemplate('InventoryCompleteGrid');
        //$inventoryCompleteGridControl.find('div[data-datafield="InventoryId"]').attr('data-validationname', 'PartsInventoryValidation');
        //$inventoryCompleteGrid.empty().append($inventoryCompleteGridControl);
        //$inventoryCompleteGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        PackageId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(),
        //        WarehouseId: warehouse.warehouseid
        //    };
        //});
        //$inventoryCompleteGridControl.data('beforesave', function (request) {
        //    request.PackageId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        ////$inventoryCompleteGridControl.data('afterdatabindcallback', function ($control, dt) {
        ////    var orderByIndex = dt.ColumnIndex.OrderBy;
        ////    var inventoryIdIndex = dt.ColumnIndex.InventoryId
        ////    for (var i = 0; i < dt.Rows.length; i++) {
        ////        if (dt.Rows[i][orderByIndex] === 1 && dt.Rows[i][inventoryIdIndex] !== '') {
        ////            primaryRowIndex = i
        ////        }
        ////    }

        ////});

        //FwBrowse.init($inventoryCompleteGridControl);
        //FwBrowse.renderRuntimeHtml($inventoryCompleteGridControl);

        //Inventory Complete Grid
        const $inventoryCompleteGrid = FwBrowse.renderGrid({
            nameGrid: 'InventoryCompleteGrid',
            gridSecurityId: 'ABL0XJQpsQQo',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        RentalInventoryController.quikSearch(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            // getBaseApiUrl: (): string => { return `${this.apiurl}/${FwFormField.getValueByDataField($form, 'InventoryId')}/aka`; }, 
            onDataBind: (request: any) => {
                request.uniqueids = {
                    PackageId: FwFormField.getValueByDataField($form, 'InventoryId'),
                    WarehouseId: warehouse.warehouseid
                };
            },
            beforeSave: (request: any) => {
                request.PackageId = FwFormField.getValueByDataField($form, 'InventoryId')
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $browse.find('div[data-datafield="InventoryId"]').attr('data-validationname', 'PartsInventoryValidation');
            }
        });


        // ----------
        //const $inventoryKitGrid: any = $form.find('div[data-grid="InventoryKitGrid"]');
        //const $inventoryKitGridControl: any = FwBrowse.loadGridFromTemplate('InventoryKitGrid');
        //$inventoryKitGridControl.find('div[data-datafield="InventoryId"]').attr('data-validationname', 'PartsInventoryValidation');
        //$inventoryKitGrid.empty().append($inventoryKitGridControl);
        //$inventoryKitGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        PackageId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //$inventoryKitGridControl.data('beforesave', function (request) {
        //    request.PackageId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($inventoryKitGridControl);
        //FwBrowse.renderRuntimeHtml($inventoryKitGridControl);


        //Inventory Kit Grid
        const $inventoryKitGrid = FwBrowse.renderGrid({
            nameGrid: 'InventoryKitGrid',
            gridSecurityId: 'ABL0XJQpsQQo',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        RentalInventoryController.quikSearch(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            // getBaseApiUrl: (): string => { return `${this.apiurl}/${FwFormField.getValueByDataField($form, 'InventoryId')}/aka`; }, 
            onDataBind: (request: any) => {
                request.uniqueids = {
                    PackageId: FwFormField.getValueByDataField($form, 'InventoryId'),
                    WarehouseId: warehouse.warehouseid
                };
            },
            beforeSave: (request: any) => {
                request.PackageId = FwFormField.getValueByDataField($form, 'InventoryId');
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $browse.find('div[data-datafield="InventoryId"]').attr('data-validationname', 'PartsInventoryValidation');
            }
        });

        // ----------
        //const $purchaseVendorGrid = $form.find('div[data-grid="PurchaseVendorGrid"]');
        //const $purchaseVendorGridControl = FwBrowse.loadGridFromTemplate('PurchaseVendorGrid');
        //$purchaseVendorGrid.empty().append($purchaseVendorGridControl);
        //$purchaseVendorGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //FwBrowse.init($purchaseVendorGridControl);
        //FwBrowse.renderRuntimeHtml($purchaseVendorGridControl);

        //Purchase Vendor Grid
        FwBrowse.renderGrid({
            nameGrid: 'PurchaseVendorGrid',
            gridSecurityId: '15yjeHiHe1x99',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
                options.hasEdit = false;
            },
            // getBaseApiUrl: (): string => { return `${this.apiurl}/${FwFormField.getValueByDataField($form, 'InventoryId')}/aka`; }, 
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            }
        });

        // ----------
        //const $akaGrid = $form.find('div[data-grid="AlternativeDescriptionGrid"]');
        //const $akaGridControl = FwBrowse.loadGridFromTemplate('AlternativeDescriptionGrid');
        //$akaGrid.empty().append($akaGridControl);
        //$akaGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
        //    };
        //});
        //$akaGridControl.data('beforesave', function (request) {
        //    request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId')
        //        , request.IsPrimary = false
        //});
        //FwBrowse.init($akaGridControl);
        //FwBrowse.renderRuntimeHtml($akaGridControl);

        //Alternative Description Grid
        FwBrowse.renderGrid({
            nameGrid: 'AlternativeDescriptionGrid',
            gridSecurityId: '2BkAgaVVrDD3',
            moduleSecurityId: this.id,
            $form: $form,
            // getBaseApiUrl: (): string => { return `${this.apiurl}/${FwFormField.getValueByDataField($form, 'InventoryId')}/aka`; },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            }
        });


        //hide columns
        $form.find('[data-grid="InventoryWarehouseCompletePricingGrid"] div[data-browsedatafield="DailyRate"]').parent('td').hide();
        $form.find('[data-grid="InventoryWarehouseKitPricingGrid"] div[data-browsedatafield="DailyRate"]').parent('td').hide();
        $form.find('[data-grid="InventoryWarehouseCompletePricingGrid"] div[data-browsedatafield="WeeklyRate"]').parent('td').hide();
        $form.find('[data-grid="InventoryWarehouseKitPricingGrid"] div[data-browsedatafield="WeeklyRate"]').parent('td').hide();
        $form.find('[data-grid="InventoryWarehouseCompletePricingGrid"] div[data-browsedatafield="MonthlyRate"]').parent('td').hide();
        $form.find('[data-grid="InventoryWarehouseKitPricingGrid"] div[data-browsedatafield="MonthlyRate"]').parent('td').hide();
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        super.afterLoad($form);

        const $partsInventoryWarehouseGrid = $form.find('[data-name="PartsInventoryWarehouseGrid"]');
        FwBrowse.search($partsInventoryWarehouseGrid);
        const $partsInventoryWarehousePricingGrid = $form.find('[data-name="PartsInventoryWarehousePricingGrid"]');
        FwBrowse.search($partsInventoryWarehousePricingGrid);

        const inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
        FwAppDocumentGrid.renderGrid({
            $form: $form,
            caption: 'Documents',
            nameGrid: 'PartsInventoryDocumentGrid',
            getBaseApiUrl: () => {
                return `${this.apiurl}/${inventoryId}/document`;
            },
            gridSecurityId: 'lwYABhj5zknM',
            moduleSecurityId: this.id,
            parentFormDataFields: 'InventoryId',
            uniqueid1Name: 'InventoryId',
            getUniqueid1Value: () => inventoryId,
            uniqueid2Name: '',
            getUniqueid2Value: () => ''
        });
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        let inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
        let categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
        switch (datafield) {
            case 'InventoryTypeId':
                request.uniqueids = {
                    Parts: true,
                    HasCategories: true,
                };

                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorytype`);
                break;
            case 'CategoryId':
                request.uniqueids = {
                    InventoryTypeId: inventoryTypeId,
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecategory`);
                break;
            case 'SubCategoryId':
                request.uniqueids = {
                    InventoryTypeId: inventoryTypeId,
                    CategoryId: categoryId,
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubcategory`);
                break;
            case 'UnitId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateunit`);
                break;
            case 'Rank':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validaterank`);
                break;
            case 'ManufacturerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatemanufacturer`);
                break;
            case 'AssetAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateassetaccount`);
                break;
            case 'IncomeAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateincomeaccount`);
                break;
            case 'CostOfGoodsSoldExpenseAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecostofgoodssoldexpenseaccount`);
                break;
            case 'ProfitAndLossCategoryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateprofitandloss`);
                break;
            case 'WarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                break;
            case 'CountryOfOriginId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecountryoforigin`);
        }
    }
};
//----------------------------------------------------------------------------------------------
var PartsInventoryController = new PartsInventory();