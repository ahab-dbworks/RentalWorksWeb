class SalesInventory extends InventoryBase {
    Module: string = 'SalesInventory';
    apiurl: string = 'api/v1/salesinventory';
    caption: string = Constants.Modules.Inventory.children.SalesInventory.caption;
    nav: string = Constants.Modules.Inventory.children.SalesInventory.nav;
    id: string = Constants.Modules.Inventory.children.SalesInventory.id;
    AvailableFor: string = "S";
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'InventoryLocationTaxGrid',
            gridSecurityId: 'dpDtvVrXRZrd',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'SalesInventoryWarehouseGrid',
            gridSecurityId: 'g8sCuKjUVrW1',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                }
                request.miscfields = {
                    UserWarehouseId: warehouse.warehouseid
                };
                request.pagesize = 100;  //justin 04/01/2019 #359 show all active warehouses here                
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'SalesInventoryWarehousePricingGrid',
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
        // ----------
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
            nameGrid: 'InventoryAvailabilityGrid',
            gridSecurityId: 'g8sCuKjUVrW1',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            },
            //beforeSave: (request: any) => {
            //    request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            //}
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'InventoryConsignmentGrid',
            gridSecurityId: 'JKfdyoLXFqu3',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasEdit = false;
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'SalesInventorySubstituteGrid',
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
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'SalesInventoryCompatibilityGrid',
            gridSecurityId: 'mlAKf5gRPNNI',
            moduleSecurityId: this.id,
            $form: $form,
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
        FwBrowse.renderGrid({
            nameGrid: 'InventoryQcGrid',
            gridSecurityId: 'g8sCuKjUVrW1',
            moduleSecurityId: this.id,
            $form: $form,
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
        FwBrowse.renderGrid({
            nameGrid: 'InventoryVendorGrid',
            gridSecurityId: 's9vdtBqItIEi',
            moduleSecurityId: this.id,
            $form: $form,
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
        FwBrowse.renderGrid({
            nameGrid: 'InventoryPrepGrid',
            gridSecurityId: 'CzNh6kOVsRO4',
            moduleSecurityId: this.id,
            $form: $form,
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
        FwBrowse.renderGrid({
            nameGrid: 'WardrobeInventoryColorGrid',
            gridSecurityId: 'gJN4HKmkowSD',
            moduleSecurityId: this.id,
            $form: $form,
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
        FwBrowse.renderGrid({
            nameGrid: 'WardrobeInventoryMaterialGrid',
            gridSecurityId: 'l35woZUn3E5M',
            moduleSecurityId: this.id,
            $form: $form,
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
                $browse.find('div[data-datafield="InventoryId"]').attr('data-validationname', 'SalesInventoryValidation');
            }
        });
        // InventoryWarehouseSpecificGrid for Completes
        FwBrowse.renderGrid({
            nameGrid: 'InventoryWarehouseSpecificGrid',
            gridSelector: 'div[data-grid="InventoryWarehouseSpecificGrid"].complete',
            gridSecurityId: 'HUmVUurETwRho',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = true;
                options.hasEdit = true;
                options.hasDelete = true;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $browse.addClass('complete');
            },
        });
        // ----------
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
                $browse.find('div[data-datafield="InventoryId"]').attr('data-validationname', 'SalesInventoryValidation');
            }
        });
        // InventoryWarehouseSpecificGrid for Kits
        FwBrowse.renderGrid({
            nameGrid: 'InventoryWarehouseSpecificGrid',
            gridSelector: 'div[data-grid="InventoryWarehouseSpecificGrid"].kit',
            gridSecurityId: 'HUmVUurETwRho',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = true;
                options.hasEdit = true;
                options.hasDelete = true;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $browse.addClass('kit');
            },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'AlternativeDescriptionGrid',
            gridSecurityId: '2BkAgaVVrDD3',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                request.IsPrimary = false;
            }
        });

        //Purchase Vendor Grid
        FwBrowse.renderGrid({
            nameGrid: 'PurchaseVendorGrid',
            gridSecurityId: '15yjeHiHe1x99',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            }
        });

        FwBrowse.renderGrid({
            nameGrid: 'ContractHistoryGrid',
            gridSecurityId: 'fY1Au6CjXlodD',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
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
    afterSave($form: any) {
        let $confirmTrackedByField = $form.find('[data-datafield="ConfirmTrackedBy"]');
        $confirmTrackedByField.hide();
        FwFormField.setValue2($confirmTrackedByField, '');

        if ($form.attr('data-opensearch') == 'true') {
            RentalInventoryController.quikSearch($form.data('opensearch'));
        }
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        super.afterLoad($form);

        const $salesInventoryWarehouseGrid = $form.find('[data-name="SalesInventoryWarehouseGrid"]');
        FwBrowse.search($salesInventoryWarehouseGrid);
        const $salesInventoryWarehousePricingGrid = $form.find('[data-name="SalesInventoryWarehousePricingGrid"]');
        FwBrowse.search($salesInventoryWarehousePricingGrid);

        let trackedBy = FwFormField.getValueByDataField($form, 'TrackedBy');
        let textToReplace: string = 'TRACKEDBYTYPE';
        $form.find('[data-datafield="TrackedBy"]').on('change', e => {
            let newTrackedBy = FwFormField.getValueByDataField($form, 'TrackedBy');
            const $confirmTrackedByField = $form.find('[data-datafield="ConfirmTrackedBy"]');
            if (trackedBy !== newTrackedBy) {
                let text = $confirmTrackedByField.find('.fwformfield-caption').text().replace(textToReplace, newTrackedBy);
                textToReplace = newTrackedBy;
                $confirmTrackedByField.find('.fwformfield-caption').text(text).css('color', 'red');
                $confirmTrackedByField.show();
                trackedBy = newTrackedBy;
            } else {
                $confirmTrackedByField.hide();
                FwFormField.setValue2($confirmTrackedByField, '');
            }
        });

        const inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
        FwAppDocumentGrid.renderGrid({
            $form: $form,
            caption: 'Documents',
            nameGrid: 'SalesInventoryDocumentGrid',
            getBaseApiUrl: () => {
                return `${this.apiurl}/${inventoryId}/document`;
            },
            gridSecurityId: 'Fpb2CAabL83x',
            moduleSecurityId: this.id,
            parentFormDataFields: 'InventoryId',
            uniqueid1Name: 'InventoryId',
            getUniqueid1Value: () => inventoryId,
            uniqueid2Name: '',
            getUniqueid2Value: () => ''
        });

        this.disableInventoryWarehouseSpecificGrid($form);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        let inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
        let categoryId = FwFormField.getValueByDataField($form, 'CategoryId');

        switch (datafield) {
            case 'InventoryTypeId':
                request.uniqueids = {
                    Sales: true,
                    HasCategories: true,
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorytype`);
                break;
            case 'CategoryId':
                request.uniqueids = {
                    InventoryTypeId: inventoryTypeId,
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecategoryid`);
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
            case 'CountryOfOriginId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecountryoforigin`);
                break;
            case 'ProfitAndLossCategoryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateprofitandlosscategory`);
                break;
            case 'AssetAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateassetaccount`);
                break;
            case 'IncomeAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateincomeaccount`);
                break;
            case 'SubIncomeAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubincomeaccount`);
                break;
            case 'CostOfGoodsSoldExpenseAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecostofgoodssoldexpenseaccount`);
                break;
            case 'WarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                break;
            case 'ManufacturerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatemanufacturer`);
                break;
        };
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        super.events($form);
        //populate inventory adjustment form fields
        $form.on('click', '[data-name="InventoryAdjustment"] [data-type="NewMenuBarButton"]', e => {
            const iCode = FwFormField.getValueByDataField($form, 'ICode');
            const description = FwFormField.getValueByDataField($form, 'Description');
            const inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            const warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
            const $tab = FwTabs.getTabByElement($form);
            const subTabIds = $tab.data('subtabids');
            const subTabLength = subTabIds.length;
            if (subTabLength > 0) {
                try {
                    FwAppData.apiMethod(true, 'GET', `api/v1/inventorywarehouse/${inventoryId}~${warehouseId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                        const subTabId = subTabIds[subTabLength - 1];
                        const $subTab = jQuery(`#${subTabId}`);
                        const $subTabPage = FwTabs.getTabPageByTab($subTab);
                        const today = FwFunc.getDate();
                        const time = FwFunc.getTime(false);
                        const userId = JSON.parse(sessionStorage.getItem('userid'));
                        FwFormField.setValueByDataField($subTabPage, 'AdjustmentDate', today);
                        FwFormField.setValueByDataField($subTabPage, 'AdjustmentTime', time);
                        FwFormField.setValueByDataField($subTabPage, 'ModifiedByUserId', userId.usersid, userId.name);
                        FwFormField.setValueByDataField($subTabPage, 'InventoryId', inventoryId, iCode);
                        FwFormField.setValueByDataField($subTabPage, 'Description', description);
                        FwFormField.setValueByDataField($subTabPage, 'OldUnitCost', response.AverageCost);
                        FwFormField.setValueByDataField($subTabPage, 'OldQuantity', response.Qty);
                        FwFormField.setValueByDataField($subTabPage, 'UnitCost', response.AverageCost);
                        FwFormField.setValueByDataField($subTabPage, 'WarehouseId', warehouseId);
                    }, function onError(response) {
                        FwFunc.showError(response);
                    }, $form)
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }
        });
        // WarehouseSpecificPackage checkbox
        $form.find('div[data-datafield="WarehouseSpecificPackage"]').on('change', e => {
            this.disableInventoryWarehouseSpecificGrid($form);
        });
        // evt for InventoryWarehouseSpecificGrid
        $form.find('[data-name="InventoryWarehouseSpecificGrid"]').data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
            try {
                if ($control.attr('data-enabled') !== 'false') {
                    let $grid;
                    if ($control.hasClass('complete')) {
                        $grid = $form.find('[data-name="InventoryCompleteGrid"]');
                    } else if ($control.hasClass('kit')) {
                        $grid = $form.find('[data-name="InventoryKitGrid"]');
                    }
                    const warehouseId = $tr.find('.field[data-browsedatatype="key"]').attr('data-originalvalue');

                    $grid.data('ondatabind', request => {
                        request.uniqueids = {
                            PackageId: FwFormField.getValueByDataField($form, 'InventoryId'),
                            WarehouseId: warehouseId,
                        };
                    });
                    $grid.data('beforesave', request => {
                        request.PackageId = FwFormField.getValueByDataField($form, 'InventoryId');
                        request.WarehouseId = warehouseId;
                    });
                    FwBrowse.search($grid);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        // click evt for resetting complete or kit grid back to all warehouses
        $form.find('.standard-definition').on('click', e => {
            const $this = jQuery(e.currentTarget);
            let $grid;
            if ($this.hasClass('complete')) {
                $grid = $form.find('[data-name="InventoryCompleteGrid"]');
            } else if ($this.hasClass('kit')) {
                $grid = $form.find('[data-name="InventoryKitGrid"]');
            }
            $grid.data('ondatabind', request => {
                request.uniqueids = {
                    PackageId: FwFormField.getValueByDataField($form, 'InventoryId'),
                };
            });
            $grid.data('beforesave', request => {
                request.PackageId = FwFormField.getValueByDataField($form, 'InventoryId');
            });

            FwBrowse.search($grid);
        });
    }
    //----------------------------------------------------------------------------------------------
    disableInventoryWarehouseSpecificGrid($form) {
        let $grid, tag;
        const classification = FwFormField.getValueByDataField($form, 'Classification');
        if (classification === 'C') {
            $grid = $form.find('[data-name="InventoryWarehouseSpecificGrid"].complete');
            tag = 'complete';
        } else if (classification === 'K') {
            $grid = $form.find('[data-name="InventoryWarehouseSpecificGrid"].kit');
            tag = 'kit';
        }

        const warehouseSpecificPackage = FwFormField.getValue($form, `div[data-datafield="WarehouseSpecificPackage"].${tag}`);
        if (warehouseSpecificPackage) {
            FwFormField.enable($grid);
        } else {
            FwFormField.disable($grid);
        }
    }
    //----------------------------------------------------------------------------------------------
};
//----------------------------------------------------------------------------------------------
var SalesInventoryController = new SalesInventory();