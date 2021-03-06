class RentalInventory extends InventoryBase {
    Module: string = 'RentalInventory';
    apiurl: string = 'api/v1/rentalinventory';
    caption: string = Constants.Modules.Inventory.children.RentalInventory.caption;
    nav: string = Constants.Modules.Inventory.children.RentalInventory.nav;
    id: string = Constants.Modules.Inventory.children.RentalInventory.id;
    AvailableFor: string = "R";
    //----------------------------------------------------------------------------------------------
    openFormInventory($form: any) {
        FwFormField.loadItems($form.find('.lamps'), [
            { value: '0', text: '0' },
            { value: '1', text: '1' },
            { value: '2', text: '2' },
            { value: '3', text: '3' },
            { value: '4', text: '4' }
        ], true);

        $form.find('div[data-datafield="ContainerScannableInventoryId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="ContainerScannableDescription"]', $tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });

        const enableConsignment = JSON.parse(sessionStorage.getItem('controldefaults')).enableconsignment;
        if (!enableConsignment) {
            this.hideTab($form, 'consignmenttab');
        }

        const multiWarehouse = JSON.parse(sessionStorage.getItem('controldefaults')).multiwarehouse;
        if (!multiWarehouse) {
            $form.find('.warehousespecific').hide();
        }

    };
    //----------------------------------------------------------------------------------------------
    setupNewMode($form: any) {
        super.setupNewMode($form);
        const controlDefaults = JSON.parse(sessionStorage.getItem('controldefaults'));
        FwFormField.setValueByDataField($form, 'CostCalculation', controlDefaults.defaultrentalquantityinventorycostcalculation);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        super.renderGrids($form, 'Rental');
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        // ----------
        //Container Warehouse Grid
        FwBrowse.renderGrid({
            nameGrid: 'ContainerWarehouseGrid',
            gridSecurityId: '4gsBzepUJdWm',
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
                request.miscfields = {
                    UserWarehouseId: warehouse.warehouseid
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            }
        });
        // ----------
        //Inventory Availability Grid
        FwBrowse.renderGrid({
            nameGrid: 'InventoryAvailabilityGrid',
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
                };
            },
            //beforeSave: (request: any) => {
            //    request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            //}
        });
        // ----------
        //Inventory Consignment Grid
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
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            }
        });
        // ----------
        //Inventory QC Grid
        FwBrowse.renderGrid({
            nameGrid: 'InventoryQcGrid',
            gridSecurityId: 'g8sCuKjUVrW1',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1');
                FwMenu.addSubMenuItem($optionsgroup, 'QC Required for all Warehouses', '', (e: JQuery.ClickEvent) => {
                    try {
                        const $form = jQuery(e.currentTarget).closest('.fwform');
                        RentalInventoryController.QCRequiredWarehouse($form, true);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'QC Not Required for all Warehouses', '', (e: JQuery.ClickEvent) => {
                    try {
                        const $form = jQuery(e.currentTarget).closest('.fwform');
                        RentalInventoryController.QCRequiredWarehouse($form, false);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
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
        //Inventory Vendor Grid
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
        //Inventory Container Item Grid
        FwBrowse.renderGrid({
            nameGrid: 'InventoryContainerItemGrid',
            gridSecurityId: '6ELSTtE6IqSb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        this.quikSearch(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            // getBaseApiUrl: (): string => { return `${this.apiurl}/${FwFormField.getValueByDataField($form, 'InventoryId')}/aka`; }, 
            onDataBind: (request: any) => {
                request.uniqueids = {
                    PackageId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            },
            beforeSave: (request: any) => {
                request.PackageId = FwFormField.getValueByDataField($form, 'InventoryId');
                request.ContainerId = FwFormField.getValueByDataField($form, 'ContainerId');
            }
        });
        // ----------
        // InventoryWarehouseSpecificGrid for Completes
        FwBrowse.renderGrid({
            nameGrid: 'InventoryWarehouseSpecificGrid',
            gridSelector: 'div[data-grid="InventoryWarehouseSpecificGrid"].complete',
            gridSecurityId: 'HUmVUurETwRho',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = true;
                options.hasEdit = false;
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
        //Inventory Warehouse Staging Grid
        FwBrowse.renderGrid({
            nameGrid: 'InventoryWarehouseStagingGrid',
            gridSecurityId: 'g8sCuKjUVrW1',
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
        // InventoryWarehouseSpecificGrid for Kits
        FwBrowse.renderGrid({
            nameGrid: 'InventoryWarehouseSpecificGrid',
            gridSelector: 'div[data-grid="InventoryWarehouseSpecificGrid"].kit',
            gridSecurityId: 'HUmVUurETwRho',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = true;
                options.hasEdit = false;
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
        //Wardrobe Inventory Color Grid
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
        //Wardrobe Inventory Material Grid
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
        // ----------
        //hide columns
        $form.find('[data-grid="InventoryWarehouseCompletePricingGrid"] div[data-browsedatafield="Price"]').parent('td').hide();
        $form.find('[data-grid="InventoryWarehouseKitPricingGrid"] div[data-browsedatafield="Price"]').parent('td').hide();
        $form.find('[data-grid="InventoryWarehouseCompletePricingGrid"] div[data-browsedatafield="Retail"]').parent('td').hide();
        $form.find('[data-grid="InventoryWarehouseKitPricingGrid"] div[data-browsedatafield="Retail"]').parent('td').hide();
        $form.find('[data-grid="InventoryWarehouseCompletePricingGrid"] div[data-browsedatafield="AverageCost"]').parent('td').hide();
        $form.find('[data-grid="InventoryWarehouseKitPricingGrid"] div[data-browsedatafield="AverageCost"]').parent('td').hide();
        $form.find('[data-grid="InventoryWarehouseCompletePricingGrid"] div[data-browsedatafield="RestockingFee"]').parent('td').hide();
        $form.find('[data-grid="InventoryWarehouseKitPricingGrid"] div[data-browsedatafield="RestockingFee"]').parent('td').hide();
    };
    //-----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {

        super.addFormMenuItems(options);

        //FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Inventory Summary', '', (e: JQuery.ClickEvent) => {
            try {
                this.openInventorySummary(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    dynamicColumns($form: any): void {
        //const threeWeekPricing = JSON.parse(sessionStorage.getItem('applicationOptions')).threeweekpricing;
        const threeWeekPricing = JSON.parse(sessionStorage.getItem('controldefaults')).enable3weekpricing;

        const $rentalInventoryWarehousePricingGrid = $form.find('div[data-name="RentalInventoryWarehousePricingGrid"]');
        //!threeWeekPricing.enabled ? jQuery($rentalInventoryWarehousePricingGrid.find(`[data-mappedfield="Rate"]`)).parent().hide() : jQuery($rentalInventoryWarehousePricingGrid.find(`[data-mappedfield="Rate"]`)).parent().show()
        threeWeekPricing ? jQuery($rentalInventoryWarehousePricingGrid.find(`[data-mappedfield="Rate"]`)).parent().show() : jQuery($rentalInventoryWarehousePricingGrid.find(`[data-mappedfield="Rate"]`)).parent().hide()
        const $inventoryWarehouseCompletePricingGrid = $form.find('div[data-name="InventoryWarehouseCompletePricingGrid"]');
        //!threeWeekPricing.enabled ? jQuery($inventoryWarehouseCompletePricingGrid.find(`[data-mappedfield="Rate"]`)).parent().hide() : jQuery($inventoryWarehouseCompletePricingGrid.find(`[data-mappedfield="Rate"]`)).parent().show()
        threeWeekPricing ? jQuery($inventoryWarehouseCompletePricingGrid.find(`[data-mappedfield="Rate"]`)).parent().show() : jQuery($inventoryWarehouseCompletePricingGrid.find(`[data-mappedfield="Rate"]`)).parent().hide()
        const $inventoryWarehouseKitPricingGrid = $form.find('div[data-name="InventoryWarehouseKitPricingGrid"]');
        //!threeWeekPricing.enabled ? jQuery($inventoryWarehouseKitPricingGrid.find(`[data-mappedfield="Rate"]`)).parent().hide() : jQuery($inventoryWarehouseKitPricingGrid.find(`[data-mappedfield="Rate"]`)).parent().show()
        threeWeekPricing ? jQuery($inventoryWarehouseKitPricingGrid.find(`[data-mappedfield="Rate"]`)).parent().show() : jQuery($inventoryWarehouseKitPricingGrid.find(`[data-mappedfield="Rate"]`)).parent().hide()
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        super.afterLoad($form);
        this.iCodeMask($form);

        //originalTrackedBy value used in "TrackedBy" evt listener in InventoryBase
        $form.data('originalTrackedBy', FwFormField.getValueByDataField($form, 'TrackedBy'));

        const $rentalInventoryWarehouseGrid = $form.find('[data-name="RentalInventoryWarehouseGrid"]');
        const enableConsignment = JSON.parse(sessionStorage.getItem('controldefaults')).enableconsignment;
        if (!enableConsignment) {
            jQuery($rentalInventoryWarehouseGrid.find(`[data-browsedatafield="QtyConsigned"]`)).parent().hide();
        }
        FwBrowse.search($rentalInventoryWarehouseGrid);
        const $rentalInventoryWarehousePricingGrid = $form.find('[data-name="RentalInventoryWarehousePricingGrid"]');
        FwBrowse.search($rentalInventoryWarehousePricingGrid);

        //Change the grid on primary to tab when classification is container
        const $containerWarehouseGrid = $form.find('[data-name="ContainerWarehouseGrid"]');
        const classificationValue = FwFormField.getValueByDataField($form, 'Classification');

        if (classificationValue == 'N') {
            $form.find('[data-grid="RentalInventoryWarehouseGrid"]').hide();
            $form.find('[data-grid="ContainerWarehouseGrid"]').show();
            FwBrowse.search($containerWarehouseGrid);

            //Open Container module as submodule
            const $containerBrowse = this.openContainerBrowse($form);
            $form.find('.containerassetstabpage').html($containerBrowse);
            $form.find('.containerassetstab').show();

            //Show settings tab
            $form.find('.settingstab').show();
        }

        //enable version and effective date fields upon loading if "Enable Software Tracking" is checked
        if (FwFormField.getValueByDataField($form, 'TrackSoftware')) FwFormField.enable($form.find('.track-software'));
        if (FwFormField.getValueByDataField($form, 'ManifestShippingContainer')) FwFormField.disable($form.find('div[data-datafield="ManifestStandAloneItem"]'));
        if (FwFormField.getValueByDataField($form, 'ManifestStandAloneItem')) FwFormField.disable($form.find('div[data-datafield="ManifestShippingContainer"]'));

        this.dynamicColumns($form);

        const inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
        FwAppDocumentGrid.renderGrid({
            $form: $form,
            caption: 'Documents',
            nameGrid: 'RentalInventoryDocumentGrid',
            getBaseApiUrl: () => {
                return `${this.apiurl}/${inventoryId}/document`;
            },
            gridSecurityId: 'DCfBWlHhvSDH',
            moduleSecurityId: this.id,
            parentFormDataFields: 'InventoryId',
            uniqueid1Name: 'InventoryId',
            getUniqueid1Value: () => inventoryId,
            uniqueid2Name: '',
            getUniqueid2Value: () => ''
        });

        this.classificationSetFixedAsset($form);
        this.disableInventoryWarehouseSpecificGrid($form);
    }
    //----------------------------------------------------------------------------------------------
    classificationSetFixedAsset($form) {
        const classification = FwFormField.getValueByDataField($form, 'Classification');

        if (classification !== 'I' && classification !== 'A') {
            FwFormField.disable($form.find('div[data-datafield="IsFixedAsset"]'));
            FwFormField.setValueByDataField($form, 'IsFixedAsset', false);
        } else {
            FwFormField.enable($form.find('div[data-datafield="IsFixedAsset"]'));
        }
    }
    //----------------------------------------------------------------------------------------------
    QCRequiredWarehouse($form: JQuery, QcRequired: boolean) {
        const request: any = {
            InventoryId: FwFormField.getValueByDataField($form, 'InventoryId'),
            QcRequired: QcRequired,
        }

        FwAppData.apiMethod(true, 'POST', 'api/v1/rentalinventory/qcrequiredallwarehouses', request, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                if (response.success === true) {
                    const $grid = $form.find('div[data-name="InventoryQcGrid"]');
                    FwBrowse.search($grid);
                } else {
                    FwNotification.renderNotification('ERROR', `${response.msg}`)
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, $form);
    }
    //----------------------------------------------------------------------------------------------
    openContainerBrowse($form: any) {
        const $browse = ContainerController.openBrowse();
        const containerId = FwFormField.getValueByDataField($form, 'ContainerId');

        $browse.data('ondatabind', function (request) {
            request.activeview = 'ALL'
            request.uniqueids = {
                ContainerId: containerId
            }
        });
        FwBrowse.databind($browse);
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        //const validationName = request.module;
        //const InventoryTypeValue = jQuery($validationbrowse.find('[data-validationname="InventoryTypeValidation"] input')).val();
        //const CategoryTypeId = jQuery($validationbrowse.find('[data-validationname="RentalCategoryValidation"] input')).val();
        //
        //switch (validationName) {
        //    case 'InventoryTypeValidation':
        //        request.uniqueids = {
        //            //Rental: true,
        //            RecType: "R",
        //            HasCategories: true
        //        };
        //        break;
        //    case 'RentalCategoryValidation':
        //        request.uniqueids = {
        //            InventoryTypeId: InventoryTypeValue
        //        };
        //        break;
        //    case 'SubCategoryValidation':
        //        request.uniqueids = {
        //            TypeId: InventoryTypeValue,
        //            CategoryId: CategoryTypeId
        //        };
        //        break;
        //};

        let inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
        let categoryId = FwFormField.getValueByDataField($form, 'CategoryId');

        switch (datafield) {
            case 'InventoryTypeId':
                request.uniqueids = {
                    Rental: true,
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
            case 'SubIncomeAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubincomeaccount`);
                break;
            case 'EquipmentSaleIncomeAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateequipmentsaleincomeaccount`);
                break;
            case 'LdIncomeAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateldincomeaccount`);
                break;
            case 'CostOfGoodsSoldExpenseAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecostofgoodssoldexpenseaccount`);
                break;
            case 'CostOfGoodsRentedExpenseAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecostofgoodsrentedexpenseaccount`);
                break;
            case 'ProfitAndLossCategoryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateprofitandlosscategory`);
                break;
            case 'CountryOfOriginId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecountryoforigin`);
                break;
            case 'WarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateScannableICode($browse, $grid, request) {
        const $form = $grid.closest('.fwform');
        const ContainerId = FwFormField.getValueByDataField($form, 'ContainerId');

        request.uniqueids = {
            ContainerId: ContainerId,
            TrackedBy: "BARCODE"
        };
    };
    //----------------------------------------------------------------------------------------------
    openInventorySummary($form: any) {
        try {
            const mode = 'EDIT';
            const summaryInfo: any = {};
            summaryInfo.ICode = FwFormField.getValueByDataField($form, `ICode`);
            summaryInfo.InventoryId = FwFormField.getValueByDataField($form, `InventoryId`);
            summaryInfo.Description = FwFormField.getValueByDataField($form, `Description`);


            const $inventorySummary = InventorySummaryController.openForm(mode, summaryInfo);
            FwModule.openSubModuleTab($form, $inventorySummary);
            const $tab = FwTabs.getTabByElement($inventorySummary);
            $tab.find('.caption').html('Inventory Summary');
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    iCodeMask($form) {
        let inputmask = JSON.parse(sessionStorage.getItem('controldefaults')).defaulticodemask;
        let inputmasksplit = inputmask.split('');
        for (var i = 0; i < inputmasksplit.length; i++) {
            if (inputmasksplit[i] !== '-') {
                inputmasksplit[i] = '*'
            }
        }
        //let wildcardMask = inputmasksplit.join('');
        //let wildcardMask = '[' + inputmasksplit.join('') + ']';  //justin 04/16/2019 optional digits are converted to blanks instead of '_' on blur

        //justin 10/18/2019 #1155
        let wildcardMask = inputmasksplit.join('');
        while (wildcardMask.length < 12) {
            wildcardMask += '*';
        }
        wildcardMask = '[' + wildcardMask + ']'
        $form.find('[data-datafield="ICode"] input').inputmask({ mask: wildcardMask });
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        super.events($form);

        $form
            .on('change', '[data-datafield="TrackSoftware"]', e => {
                if (FwFormField.getValue2(jQuery(e.currentTarget))) {
                    FwFormField.enable($form.find('.track-software'));
                } else {
                    FwFormField.disable($form.find('.track-software'));
                }
            })
            .on('change', 'div[data-datafield="ManifestShippingContainer"]', e => {
                if (FwFormField.getValue2(jQuery(e.currentTarget))) {
                    FwFormField.disable($form.find('div[data-datafield="ManifestStandAloneItem"]'));
                } else {
                    FwFormField.enable($form.find('div[data-datafield="ManifestStandAloneItem"]'));
                }
            })
            .on('change', 'div[data-datafield="ManifestStandAloneItem"]', e => {
                if (FwFormField.getValue2(jQuery(e.currentTarget))) {
                    FwFormField.disable($form.find('div[data-datafield="ManifestShippingContainer"]'));
                } else {
                    FwFormField.enable($form.find('div[data-datafield="ManifestShippingContainer"]'));
                }
            });

        $form.find('div[data-datafield="Classification"] .fwformfield-value').on('change', e => {
            this.classificationSetFixedAsset($form);
        });
        // WarehouseSpecificPackage checkbox
        $form.find('div[data-datafield="WarehouseSpecificPackage"]').on('change', e => {
            this.disableInventoryWarehouseSpecificGrid($form);
        });
        const $inventoryWarehouseSpecificGrid = $form.find('[data-name="InventoryWarehouseSpecificGrid"]');
        // double click added for single row grid
        $inventoryWarehouseSpecificGrid.data('onrowdblclick', evt => {
            const $tr = jQuery(evt.currentTarget);
            const $control = $tr.closest('div[data-type="Grid"]');
            $inventoryWarehouseSpecificGrid.data('onselectedrowchanged')($control, $tr);
        });

        // evt for InventoryWarehouseSpecificGrid
        $inventoryWarehouseSpecificGrid.data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
            try {
                if ($control.attr('data-enabled') !== 'false') {
                    let $grid;
                    if ($control.hasClass('complete')) {
                        $grid = $form.find('[data-name="InventoryCompleteGrid"]');
                    } else if ($control.hasClass('kit')) {
                        $grid = $form.find('[data-name="InventoryKitGrid"]');
                    }
                    const warehouseId = $tr.find('.field[data-whkey="true"]').attr('data-originalvalue');
                    const warehouse = $tr.find('.field[data-validationname="WarehouseValidation"]').attr('data-originaltext') || '';
                    $grid.find('.gridmenu .menucaption').text(`Items ${warehouse ? '(' + warehouse + ')' : 'Items'}`);
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
        // ----------
        $inventoryWarehouseSpecificGrid.data('afterdelete', ($control: JQuery, $tr: JQuery) => {
            const classification = FwFormField.getValueByDataField($form, 'Classification');
            let $grid;
            if (classification === 'C') {
                $grid = $form.find('[data-name="InventoryCompleteGrid"]');
            } else if (classification === 'K') {
                $grid = $form.find('[data-name="InventoryKitGrid"]');
            }
            $grid.find('.gridmenu .menucaption').text(`Items`);
            $grid.data('ondatabind', request => {
                request.uniqueids = {
                    PackageId: FwFormField.getValueByDataField($form, 'InventoryId'),
                };
            });
            $grid.data('beforesave', request => {
                request.PackageId = FwFormField.getValueByDataField($form, 'InventoryId');
            });

            FwBrowse.search($grid);
        })
        // ----------
        // click evt for resetting complete or kit grid back to all warehouses
        $form.find('.standard-definition').on('click', e => {
            const $this = jQuery(e.currentTarget);
            let $grid, tag;
            if ($this.hasClass('complete')) {
                $grid = $form.find('[data-name="InventoryCompleteGrid"]');
                tag = 'complete';
            } else if ($this.hasClass('kit')) {
                $grid = $form.find('[data-name="InventoryKitGrid"]');
                tag = 'kit';
            }
            $grid.find('.gridmenu .menucaption').text(`Items`);
            $grid.data('ondatabind', request => {
                request.uniqueids = {
                    PackageId: FwFormField.getValueByDataField($form, 'InventoryId'),
                };
            });
            $grid.data('beforesave', request => {
                request.PackageId = FwFormField.getValueByDataField($form, 'InventoryId');
            });

            FwBrowse.search($grid);
            FwBrowse.search($form.find(`[data-name="InventoryWarehouseSpecificGrid"].${tag}`));
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

        if ((classification === 'C') || (classification === 'K')) {
            const warehouseSpecificPackage = FwFormField.getValue($form, `div[data-datafield="WarehouseSpecificPackage"].${tag}`);
            if (warehouseSpecificPackage) {
                FwFormField.enable($grid);
            } else {
                FwFormField.disable($grid);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
}

//----------------------------------------------------------------------------------------------
var RentalInventoryController = new RentalInventory();