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
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const maxPageSize = 20;
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        // ----------
        //const $inventoryLocationTaxGrid: any = $form.find('div[data-grid="InventoryLocationTaxGrid"]');
        //const $inventoryLocationTaxGridControl: any = FwBrowse.loadGridFromTemplate('InventoryLocationTaxGrid');
        //$inventoryLocationTaxGrid.empty().append($inventoryLocationTaxGridControl);
        //$inventoryLocationTaxGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //$inventoryLocationTaxGridControl.data('beforesave', function (request) {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($inventoryLocationTaxGridControl);
        //FwBrowse.renderRuntimeHtml($inventoryLocationTaxGridControl);

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
        //const $rentalInventoryWarehouseGrid = $form.find('div[data-grid="RentalInventoryWarehouseGrid"]');
        //const $rentalInventoryWarehouseGridControl = FwBrowse.loadGridFromTemplate('RentalInventoryWarehouseGrid');
        //$rentalInventoryWarehouseGrid.empty().append($rentalInventoryWarehouseGridControl);
        //$rentalInventoryWarehouseGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //    request.miscfields = {
        //        UserWarehouseId: warehouse.warehouseid
        //    };
        //    request.pagesize = 100;  //justin 04/01/2019 #359 show all active warehouses here
        //});
        //$rentalInventoryWarehouseGridControl.data('beforesave', function (request) {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($rentalInventoryWarehouseGridControl);
        //FwBrowse.renderRuntimeHtml($rentalInventoryWarehouseGridControl);

        //Rental Inventory Warehouse Grid
        FwBrowse.renderGrid({
            nameGrid: 'RentalInventoryWarehouseGrid',
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
        //const containerWarehouseGrid = $form.find('div[data-grid="ContainerWarehouseGrid"]');
        //const containerWarehouseGridControl = FwBrowse.loadGridFromTemplate('ContainerWarehouseGrid');
        //containerWarehouseGrid.empty().append(containerWarehouseGridControl);
        //containerWarehouseGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //    request.miscfields = {
        //        UserWarehouseId: warehouse.warehouseid
        //    };
        //});
        //containerWarehouseGridControl.data('beforesave', function (request) {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init(containerWarehouseGridControl);
        //FwBrowse.renderRuntimeHtml(containerWarehouseGridControl);

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
        //const $rentalInventoryWarehousePricingGrid: any = $form.find('div[data-grid="RentalInventoryWarehousePricingGrid"]');
        //const $rentalInventoryWarehouseGridPricingControl: any = FwBrowse.loadGridFromTemplate('RentalInventoryWarehousePricingGrid');
        //$rentalInventoryWarehousePricingGrid.empty().append($rentalInventoryWarehouseGridPricingControl);
        //$rentalInventoryWarehouseGridPricingControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //    request.miscfields = {
        //        UserWarehouseId: warehouse.warehouseid
        //    };
        //    request.pagesize = 100;  //justin 04/01/2019 #359 show all active warehouses here
        //});
        //$rentalInventoryWarehouseGridPricingControl.data('beforesave', request => {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($rentalInventoryWarehouseGridPricingControl);
        //FwBrowse.renderRuntimeHtml($rentalInventoryWarehouseGridPricingControl);

        //Rental Inventory Warehouse Pricing Grid
        FwBrowse.renderGrid({
            nameGrid: 'RentalInventoryWarehousePricingGrid',
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
                request.miscfields = {
                    UserWarehouseId: warehouse.warehouseid
                };
                request.pagesize = 100;  //justin 04/01/2019 #359 show all active warehouses here
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
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
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
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
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            }
        });
        // ----------
        //const $inventoryAvailabilityGrid = $form.find('div[data-grid="InventoryAvailabilityGrid"]');
        //const $inventoryAvailabilityGridControl = FwBrowse.loadGridFromTemplate('InventoryAvailabilityGrid');
        //$inventoryAvailabilityGrid.empty().append($inventoryAvailabilityGridControl);
        //$inventoryAvailabilityGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //$inventoryAvailabilityGridControl.data('beforesave', function (request) {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($inventoryAvailabilityGridControl);
        //FwBrowse.renderRuntimeHtml($inventoryAvailabilityGridControl);

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
        //const $inventoryConsignmentGrid = $form.find('div[data-grid="InventoryConsignmentGrid"]');
        //const $inventoryConsignmentGridControl = FwBrowse.loadGridFromTemplate('InventoryConsignmentGrid');
        //$inventoryConsignmentGrid.empty().append($inventoryConsignmentGridControl);
        //$inventoryConsignmentGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //$inventoryConsignmentGridControl.data('beforesave', function (request) {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($inventoryConsignmentGridControl);
        //FwBrowse.renderRuntimeHtml($inventoryConsignmentGridControl);

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
        //const $inventoryCompleteKitGrid = $form.find('div[data-grid="InventoryCompleteKitGrid"]');
        //const $inventoryCompleteKitGridControl = FwBrowse.loadGridFromTemplate('InventoryCompleteKitGrid');
        //$inventoryCompleteKitGrid.empty().append($inventoryCompleteKitGridControl);
        //$inventoryCompleteKitGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //$inventoryCompleteKitGridControl.data('beforesave', function (request) {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($inventoryCompleteKitGridControl);
        //FwBrowse.renderRuntimeHtml($inventoryCompleteKitGridControl);

        //Inventory Complete/Kit Grid
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
        //const $inventorySubstituteGrid = $form.find('div[data-grid="InventorySubstituteGrid"]');
        //const $inventorySubstituteGridControl = FwBrowse.loadGridFromTemplate('InventorySubstituteGrid');
        //$inventorySubstituteGrid.empty().append($inventorySubstituteGridControl);
        //$inventorySubstituteGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(),
        //        WarehouseId: warehouse.warehouseid
        //    };
        //});
        //$inventorySubstituteGridControl.data('beforesave', function (request) {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($inventorySubstituteGridControl);
        //FwBrowse.renderRuntimeHtml($inventorySubstituteGridControl);

        //Inventory Substitute Grid
        FwBrowse.renderGrid({
            nameGrid: 'InventorySubstituteGrid',
            gridSecurityId: '5sN9zKtGzNTq',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId'),
                    WarehouseId: warehouse.warehouseid
                };
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            }
        });
        // ----------
        //const $inventoryCompatibilityGrid = $form.find('div[data-grid="InventoryCompatibilityGrid"]');
        //const $inventoryCompatibilityGridControl = FwBrowse.loadGridFromTemplate('InventoryCompatibilityGrid');
        //$inventoryCompatibilityGrid.empty().append($inventoryCompatibilityGridControl);
        //$inventoryCompatibilityGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //$inventoryCompatibilityGridControl.data('beforesave', function (request) {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($inventoryCompatibilityGridControl);
        //FwBrowse.renderRuntimeHtml($inventoryCompatibilityGridControl);

        //Inventory Compatibility Grid
        FwBrowse.renderGrid({
            nameGrid: 'InventoryCompatibilityGrid',
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
        //const $inventoryQcGrid = $form.find('div[data-grid="InventoryQcGrid"]');
        //const $inventoryQcGridControl = FwBrowse.loadGridFromTemplate('InventoryQcGrid');
        //$inventoryQcGrid.empty().append($inventoryQcGridControl);
        //$inventoryQcGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //$inventoryQcGridControl.data('beforesave', function (request) {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($inventoryQcGridControl);
        //FwBrowse.renderRuntimeHtml($inventoryQcGridControl);

        //Inventory QC Grid
        FwBrowse.renderGrid({
            nameGrid: 'InventoryQcGrid',
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
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            }
        });
        // ----------
        //const $inventoryAttributeValueGrid = $form.find('div[data-grid="InventoryAttributeValueGrid"]');
        //const $inventoryAttributeValueGridControl = FwBrowse.loadGridFromTemplate('InventoryAttributeValueGrid');
        //$inventoryAttributeValueGrid.empty().append($inventoryAttributeValueGridControl);
        //$inventoryAttributeValueGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //$inventoryAttributeValueGridControl.data('beforesave', function (request) {
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
        //const $inventoryVendorGrid = $form.find('div[data-grid="InventoryVendorGrid"]');
        //const $inventoryVendorGridControl = FwBrowse.loadGridFromTemplate('InventoryVendorGrid');
        //$inventoryVendorGrid.empty().append($inventoryVendorGridControl);
        //$inventoryVendorGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //$inventoryVendorGridControl.data('beforesave', function (request) {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($inventoryVendorGridControl);
        //FwBrowse.renderRuntimeHtml($inventoryVendorGridControl);

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
        //const $inventoryPrepGrid = $form.find('div[data-grid="InventoryPrepGrid"]');
        //const $inventoryPrepGridControl = FwBrowse.loadGridFromTemplate('InventoryPrepGrid');
        //$inventoryPrepGrid.empty().append($inventoryPrepGridControl);
        //$inventoryPrepGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //$inventoryPrepGridControl.data('beforesave', function (request) {
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
        //const $inventoryContainerItemGrid = $form.find('div[data-grid="InventoryContainerItemGrid"]');
        //const $inventoryContainerItemGridControl = FwBrowse.loadGridFromTemplate('InventoryContainerItemGrid');
        //$inventoryContainerItemGrid.empty().append($inventoryContainerItemGridControl);
        //$inventoryContainerItemGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        PackageId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //    request.pagesize = maxPageSize;
        //});
        //$inventoryContainerItemGridControl.data('beforesave', function (request) {
        //    request.PackageId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        //    request.ContainerId = $form.find('div.fwformfield[data-datafield="ContainerId"] input').val();
        //});
        //FwBrowse.init($inventoryContainerItemGridControl);
        //FwBrowse.renderRuntimeHtml($inventoryContainerItemGridControl);

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
        //const $inventoryCompleteGrid = $form.find('div[data-grid="InventoryCompleteGrid"]');
        //const $inventoryCompleteGridControl = FwBrowse.loadGridFromTemplate('InventoryCompleteGrid');
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
        //$inventoryCompleteGridControl.data('isfieldeditable', function ($field, dt, rowIndex) {
        //    let primaryRowIndex;
        //    if (primaryRowIndex === undefined) {
        //        const orderByIndex = dt.ColumnIndex.OrderBy;
        //        const inventoryIdIndex = dt.ColumnIndex.InventoryId
        //        for (let i = 0; i < dt.Rows.length; i++) {
        //            if (dt.Rows[i][orderByIndex] === 1 && dt.Rows[i][inventoryIdIndex] !== '') {
        //                primaryRowIndex = i
        //            }
        //        }
        //    }
        //    if (rowIndex === primaryRowIndex) {
        //        return true;
        //    } else {
        //        return false;
        //    }
        //});
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
                        this.quikSearch(e);
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
            }
        });

        $inventoryCompleteGrid.data('isfieldeditable', function ($field, dt, rowIndex) {
            let primaryRowIndex;
            if (primaryRowIndex === undefined) {
                const orderByIndex = dt.ColumnIndex.OrderBy;
                const inventoryIdIndex = dt.ColumnIndex.InventoryId
                for (let i = 0; i < dt.Rows.length; i++) {
                    if (dt.Rows[i][orderByIndex] === 1 && dt.Rows[i][inventoryIdIndex] !== '') {
                        primaryRowIndex = i
                    }
                }
            }
            if (rowIndex === primaryRowIndex) {
                return true;
            } else {
                return false;
            }
        });
        // ----------
        //const $inventoryWarehouseStagingGrid = $form.find('div[data-grid="InventoryWarehouseStagingGrid"]');
        //const $inventoryWarehouseStagingGridControl = FwBrowse.loadGridFromTemplate('InventoryWarehouseStagingGrid');
        //$inventoryWarehouseStagingGrid.empty().append($inventoryWarehouseStagingGridControl);
        //$inventoryWarehouseStagingGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //$inventoryWarehouseStagingGridControl.data('beforesave', function (request) {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($inventoryWarehouseStagingGridControl);
        //FwBrowse.renderRuntimeHtml($inventoryWarehouseStagingGridControl);

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
        //const $inventoryKitGrid = $form.find('div[data-grid="InventoryKitGrid"]');
        //const $inventoryKitGridControl = FwBrowse.loadGridFromTemplate('InventoryKitGrid');
        //$inventoryKitGrid.empty().append($inventoryKitGridControl);
        //$inventoryKitGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        PackageId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(),
        //        WarehouseId: warehouse.warehouseid
        //    };
        //});
        //$inventoryKitGridControl.data('beforesave', function (request) {
        //    request.PackageId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //$inventoryKitGridControl.data('isfieldeditable', function ($field, dt, rowIndex) {
        //    let primaryRowIndex;
        //    if (primaryRowIndex === undefined) {
        //        const orderByIndex = dt.ColumnIndex.OrderBy;
        //        const inventoryIdIndex = dt.ColumnIndex.InventoryId
        //        for (let i = 0; i < dt.Rows.length; i++) {
        //            if (dt.Rows[i][orderByIndex] === 1 && dt.Rows[i][inventoryIdIndex] !== '') {
        //                primaryRowIndex = i
        //            }
        //        }
        //    }
        //    if (rowIndex === primaryRowIndex) {
        //        return true;
        //    } else {
        //        return false;
        //    }
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
                        this.quikSearch(e);
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
            }
        });

        $inventoryKitGrid.data('isfieldeditable', function ($field, dt, rowIndex) {
            let primaryRowIndex;
            if (primaryRowIndex === undefined) {
                const orderByIndex = dt.ColumnIndex.OrderBy;
                const inventoryIdIndex = dt.ColumnIndex.InventoryId
                for (let i = 0; i < dt.Rows.length; i++) {
                    if (dt.Rows[i][orderByIndex] === 1 && dt.Rows[i][inventoryIdIndex] !== '') {
                        primaryRowIndex = i
                    }
                }
            }
            if (rowIndex === primaryRowIndex) {
                return true;
            } else {
                return false;
            }
        });
        // ----------
        //const $wardrobeInventoryColorGrid = $form.find('div[data-grid="WardrobeInventoryColorGrid"]');
        //const $wardrobeInventoryColorGridControl = FwBrowse.loadGridFromTemplate('WardrobeInventoryColorGrid');
        //$wardrobeInventoryColorGrid.empty().append($wardrobeInventoryColorGridControl);
        //$wardrobeInventoryColorGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //$wardrobeInventoryColorGridControl.data('beforesave', function (request) {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($wardrobeInventoryColorGridControl);
        //FwBrowse.renderRuntimeHtml($wardrobeInventoryColorGridControl);

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
        //const $wardrobeInventoryMaterialGrid = $form.find('div[data-grid="WardrobeInventoryMaterialGrid"]');
        //const $wardrobeInventoryMaterialGridControl = FwBrowse.loadGridFromTemplate('WardrobeInventoryMaterialGrid');
        //$wardrobeInventoryMaterialGrid.empty().append($wardrobeInventoryMaterialGridControl);
        //$wardrobeInventoryMaterialGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //$wardrobeInventoryMaterialGridControl.data('beforesave', function (request) {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($wardrobeInventoryMaterialGridControl);
        //FwBrowse.renderRuntimeHtml($wardrobeInventoryMaterialGridControl);

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
        //    , request.IsPrimary = false
        //});
        //FwBrowse.renderRuntimeHtml($akaGridControl);
        //FwBrowse.init($akaGridControl);

        //Alternative Description Grid
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
    //----------------------------------------------------------------------------------------------
    dynamicColumns($form: any): void {
        const threeWeekPricing = JSON.parse(sessionStorage.getItem('applicationOptions')).threeweekpricing;
        const $rentalInventoryWarehousePricingGrid = $form.find('div[data-name="RentalInventoryWarehousePricingGrid"]');

        !threeWeekPricing.enabled ? jQuery($rentalInventoryWarehousePricingGrid.find(`[data-mappedfield="Rate"]`)).parent().hide() : jQuery($rentalInventoryWarehousePricingGrid.find(`[data-mappedfield="Rate"]`)).parent().show()
    };
    //----------------------------------------------------------------------------------------------
    afterSave($form: any) {
        $form.find('[data-datafield="ConfirmTrackedBy"]').hide();
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        super.afterLoad($form);
        this.iCodeMask($form);

        const $rentalInventoryWarehouseGrid = $form.find('[data-name="RentalInventoryWarehouseGrid"]');
        FwBrowse.search($rentalInventoryWarehouseGrid);
        const $rentalInventoryWarehousePricingGrid = $form.find('[data-name="RentalInventoryWarehousePricingGrid"]');
        FwBrowse.search($rentalInventoryWarehousePricingGrid);


        this.afterLoadSetClassification($form);

        const classificationValue = FwFormField.getValueByDataField($form, 'Classification');
        const trackedByValue = FwFormField.getValueByDataField($form, 'TrackedBy');
        if (classificationValue === 'I' || classificationValue === 'A') {
            if (trackedByValue !== 'QUANTITY') {
                $form.find('.tab.asset').show();
                //$submoduleAssetBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
                //$submoduleAssetBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
                //   if ($form.attr('data-mode') !== 'NEW') {

                //    var $assetForm, controller, $browse, assetFormData: any = {};
                //    $browse = jQuery(this).closest('.fwbrowse');
                //    controller = $browse.attr('data-controller');
                //    assetFormData.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                //    if (typeof window[controller] !== 'object') throw `Missing javascript module: ${controller}`;
                //    if (typeof window[controller]['openForm'] !== 'function') throw `Missing javascript function: ${controller}.openForm`;
                //    $assetForm = window[controller]['openForm']('NEW', assetFormData);
                //    FwModule.openSubModuleTab($browse, $assetForm);
                // } else {
                //    FwNotification.renderNotification('WARNING', 'Save the record first.')
                //  }
                //});
            }
        }

        //Change the grid on primary to tab when classification is container
        const $containerWarehouseGrid = $form.find('[data-name="ContainerWarehouseGrid"]');
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

        this.dynamicColumns($form);

        const trackedBy = FwFormField.getValueByDataField($form, 'TrackedBy');
        let textToReplace: string = 'TRACKEDBYTYPE';
        $form.find('[data-datafield="TrackedBy"]').on('change', e => {
            let newTrackedBy = FwFormField.getValueByDataField($form, 'TrackedBy');
            const $confirmTrackedByField = $form.find('[data-datafield="ConfirmTrackedBy"]');
            if (trackedBy !== newTrackedBy) {
                let text = $confirmTrackedByField.find('.fwformfield-caption').text().replace(textToReplace, newTrackedBy);
                textToReplace = newTrackedBy;
                $confirmTrackedByField.find('.fwformfield-caption').text(text).css('color', 'red');
                $confirmTrackedByField.show();
            } else {
                $confirmTrackedByField.hide();
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    quikSearch(event) {
        const $form = jQuery(event.currentTarget).closest('.fwform');
        const controllerName = $form.attr('data-controller');

        let gridInventoryType: string;
        if (controllerName === 'RentalInventoryController') {
            gridInventoryType = 'Rental';
        } else if (controllerName === 'SalesInventoryController') {
            gridInventoryType = 'Sales';
        }

        if ($form.attr('data-mode') === 'NEW') {
            let isValid = FwModule.validateForm($form);
            if (isValid) {
                let activeTabId = jQuery($form.find('[data-type="tab"].active')).attr('id');
                (<any>window)[controllerName].saveForm($form, { closetab: false });
                $form.attr('data-opensearch', 'true');
                $form.attr('data-searchtype', gridInventoryType);
                $form.attr('data-activetabid', activeTabId);
            }
        }
        let type: string;
        const grid = jQuery(event.currentTarget).parents('[data-control="FwGrid"]').attr('data-grid');
        if (grid === 'InventoryKitGrid') {
            type = 'Kit'
        }
        if (grid === 'InventoryCompleteGrid') {
            type = 'Complete'
        }
        if (grid === 'InventoryContainerItemGrid') {
            type = 'Container'
        }
        const id = FwFormField.getValueByDataField($form, 'InventoryId');
        const search = new SearchInterface();
        search.renderSearchPopup($form, id, type, gridInventoryType);
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
        let $form = $grid.closest('.fwform');
        const ContainerId = FwFormField.getValueByDataField($form, 'ContainerId');

        request.uniqueids = {
            ContainerId: ContainerId,
            TrackedBy: "BARCODE"
        };
    };
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

        $form.find('[data-datafield="TrackSoftware"]').on('change', e => {
            if (FwFormField.getValue2(jQuery(e.currentTarget))) {
                FwFormField.enable($form.find('.track-software'));
            } else {
                FwFormField.disable($form.find('.track-software'));
            }
        });
    }
    //----------------------------------------------------------------------------------------------
}

//----------------------------------------------------------------------------------------------
var RentalInventoryController = new RentalInventory();