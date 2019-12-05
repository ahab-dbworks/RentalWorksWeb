class SalesInventory extends InventoryBase {
    Module: string = 'SalesInventory';
    apiurl: string = 'api/v1/salesinventory';
    caption: string = Constants.Modules.Inventory.children.SalesInventory.caption;
    nav: string = Constants.Modules.Inventory.children.SalesInventory.nav;
    id: string = Constants.Modules.Inventory.children.SalesInventory.id;
    AvailableFor: string = "S";
    CreateCompleteId: string = '';

    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        // ----------
        //const $inventoryLocationTaxGrid = $form.find('div[data-grid="InventoryLocationTaxGrid"]');
        //const $inventoryLocationTaxGridControl = FwBrowse.loadGridFromTemplate('InventoryLocationTaxGrid');
        //$inventoryLocationTaxGrid.empty().append($inventoryLocationTaxGridControl);
        //$inventoryLocationTaxGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //FwBrowse.init($inventoryLocationTaxGridControl);
        //FwBrowse.renderRuntimeHtml($inventoryLocationTaxGridControl);

        //Inventory Location Tax Grid
        FwBrowse.renderGrid({
            nameGrid: 'InventoryLocationTaxGrid',
            gridSecurityId: 'dpDtvVrXRZrd',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
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
        //const $salesInventoryWarehouseGrid = $form.find('div[data-grid="SalesInventoryWarehouseGrid"]');
        //const $salesInventoryWarehouseGridControl = FwBrowse.loadGridFromTemplate('SalesInventoryWarehouseGrid');
        //$salesInventoryWarehouseGrid.empty().append($salesInventoryWarehouseGridControl);
        //$salesInventoryWarehouseGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //    request.miscfields = {
        //        UserWarehouseId: warehouse.warehouseid
        //    };
        //    request.pagesize = 100;  //justin 04/01/2019 #359 show all active warehouses here
        //});
        //$salesInventoryWarehouseGridControl.data('beforesave', function (request) {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($salesInventoryWarehouseGridControl);
        //FwBrowse.renderRuntimeHtml($salesInventoryWarehouseGridControl);

        //Sales Inventory Warehouse Grid
        FwBrowse.renderGrid({
            nameGrid: 'SalesInventoryWarehouseGrid',
            gridSecurityId: 'g8sCuKjUVrW1',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
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
        //const $salesInventoryWarehousePricingGrid: any = $form.find('div[data-grid="SalesInventoryWarehousePricingGrid"]');
        //const $salesInventoryWarehouseGridPricingControl: any = FwBrowse.loadGridFromTemplate('SalesInventoryWarehousePricingGrid');
        //$salesInventoryWarehousePricingGrid.empty().append($salesInventoryWarehouseGridPricingControl);
        //$salesInventoryWarehouseGridPricingControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //    request.miscfields = {
        //        UserWarehouseId: warehouse.warehouseid
        //    };
        //    request.pagesize = 100;  //justin 04/01/2019 #359 show all active warehouses here
        //});
        //$salesInventoryWarehouseGridPricingControl.data('beforesave', request => {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($salesInventoryWarehouseGridPricingControl);
        //FwBrowse.renderRuntimeHtml($salesInventoryWarehouseGridPricingControl);

        //Sales Inventory Warehouse Pricing Grid
        FwBrowse.renderGrid({
            nameGrid: 'SalesInventoryWarehousePricingGrid',
            gridSecurityId: 'g8sCuKjUVrW1',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
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
            gridSecurityId: 'BDwvPyfcT8iY9',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
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
        //const $inventoryConsignmentGrid = $form.find('div[data-grid="InventoryConsignmentGrid"]');
        //const $inventoryConsignmentGridControl = FwBrowse.loadGridFromTemplate('InventoryConsignmentGrid');
        //$inventoryConsignmentGrid.empty().append($inventoryConsignmentGridControl);
        //$inventoryConsignmentGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //FwBrowse.init($inventoryConsignmentGridControl);
        //FwBrowse.renderRuntimeHtml($inventoryConsignmentGridControl);

        //Inventory Consignment Grid
        FwBrowse.renderGrid({
            nameGrid: 'InventoryConsignmentGrid',
            gridSecurityId: 'JKfdyoLXFqu3',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
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
        //const $inventoryCompleteKitGrid = $form.find('div[data-grid="InventoryCompleteKitGrid"]');
        //const $inventoryCompleteKitGridControl = FwBrowse.loadGridFromTemplate('InventoryCompleteKitGrid');
        //$inventoryCompleteKitGrid.empty().append($inventoryCompleteKitGridControl);
        //$inventoryCompleteKitGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //FwBrowse.init($inventoryCompleteKitGridControl);
        //FwBrowse.renderRuntimeHtml($inventoryCompleteKitGridControl);
        //// ----------
        //const $inventorySubstituteGrid = $form.find('div[data-grid="SalesInventorySubstituteGrid"]');
        //const $inventorySubstituteGridControl = FwBrowse.loadGridFromTemplate('SalesInventorySubstituteGrid');
        //$inventorySubstituteGrid.empty().append($inventorySubstituteGridControl);
        //$inventorySubstituteGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //    };
        //});
        //$inventorySubstituteGridControl.data('beforesave', function (request) {
        //    request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        //});
        //FwBrowse.init($inventorySubstituteGridControl);
        //FwBrowse.renderRuntimeHtml($inventorySubstituteGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'SalesInventorySubstituteGrid',
            gridSecurityId: '5sN9zKtGzNTq',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
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


        //Inventory Complete/Kit Grid
        FwBrowse.renderGrid({
            nameGrid: 'InventoryCompleteKitGrid',
            gridSecurityId: 'gflkb5sQf7it',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasEdit = false;
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
        //const $inventoryCompatibilityGrid = $form.find('div[data-grid="SalesInventoryCompatibilityGrid"]');
        //const $inventoryCompatibilityGridControl = FwBrowse.loadGridFromTemplate('SalesInventoryCompatibilityGrid');
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
            nameGrid: 'SalesInventoryCompatibilityGrid',
            gridSecurityId: 'mlAKf5gRPNNI',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10, 
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
        //FwBrowse.init($inventoryQcGridControl);
        //FwBrowse.renderRuntimeHtml($inventoryQcGridControl);

        //Inventory QC Grid
        FwBrowse.renderGrid({
            nameGrid: 'InventoryQcGrid',
            gridSecurityId: 'g8sCuKjUVrW1',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10, 
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
            pageSize: 10,
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
            pageSize: 10,
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
            pageSize: 10,
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
            pageSize: 10,
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
            pageSize: 10,
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
        //const $inventoryCompleteGrid = $form.find('div[data-grid="InventoryCompleteGrid"]');
        //const $inventoryCompleteGridControl = FwBrowse.loadGridFromTemplate('InventoryCompleteGrid');
        //$inventoryCompleteGridControl.find('div[data-datafield="InventoryId"]').attr('data-validationname', 'SalesInventoryValidation');
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
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    PackageId  : FwFormField.getValueByDataField($form, 'InventoryId'),
                    WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
                };
            }, 
            beforeSave: (request: any) => {
                request.PackageId   = FwFormField.getValueByDataField($form, 'InventoryId'),
                request.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId')
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $browse.find('div[data-datafield="InventoryId"]').attr('data-validationname', 'SalesInventoryValidation');
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
        //const $inventoryKitGrid = $form.find('div[data-grid="InventoryKitGrid"]');
        //const $inventoryKitGridControl = FwBrowse.loadGridFromTemplate('InventoryKitGrid');
        //$inventoryKitGridControl.find('div[data-datafield="InventoryId"]').attr('data-validationname', 'SalesInventoryValidation');
        //$inventoryKitGrid.empty().append($inventoryKitGridControl);
        //$inventoryKitGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        PackageId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
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
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            }, 
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $browse.find('div[data-datafield="InventoryId"]').attr('data-validationname', 'SalesInventoryValidation');
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
        //// ----------
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

        FwBrowse.renderGrid({
            nameGrid: 'AlternativeDescriptionGrid',
            gridSecurityId: '2BkAgaVVrDD3',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
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
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                };
            }, 
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            }
        });
    };

    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        super.afterLoad($form);

        const $salesInventoryWarehouseGrid = $form.find('[data-name="SalesInventoryWarehouseGrid"]');
        FwBrowse.search($salesInventoryWarehouseGrid);
        const $salesInventoryWarehousePricingGrid = $form.find('[data-name="SalesInventoryWarehousePricingGrid"]');
        FwBrowse.search($salesInventoryWarehousePricingGrid);

        this.afterLoadSetClassification($form);

        const classificationValue = FwFormField.getValueByDataField($form, 'Classification');
        const trackedByValue = FwFormField.getValueByDataField($form, 'TrackedBy');

        if (classificationValue === 'I' || classificationValue === 'A') {
            if (trackedByValue !== 'QUANTITY') {
                $form.find('.tab.asset').show();
            }
        }

        if ($form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'))
        } else {
            FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategoryId"]'))
        }

        if ($form.find('[data-datafield="InventoryTypeIsWardrobe"] .fwformfield-value').prop('checked') === true) {
            $form.find('.wardrobetab').show();
        };

        if ($form.find('[data-datafield="SubCategoryCount"] .fwformfield-value').val() > 0) {
            FwFormField.enable($form.find('.subcategory'));
        } else {
            FwFormField.disable($form.find('.subcategory'));
        }

    };
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        //const validationName = request.module;
        //const InventoryTypeValue = jQuery($validationbrowse.find('[data-validationname="InventoryTypeValidation"] input')).val();
        //const CategoryTypeId = jQuery($validationbrowse.find('[data-validationname="SalesCategoryValidation"] input')).val();

        //switch (validationName) {
        //    case 'InventoryTypeValidation':
        //        request.uniqueids = {
        //            //Sales: true,
        //            RecType: "S",
        //            HasCategories: true
        //        };
        //        break;
        //    case 'SalesCategoryValidation':
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
                    Sales: true,
                    HasCategories: true,
                };
                break;
            case 'CategoryId':
                request.uniqueids = {
                    InventoryTypeId: inventoryTypeId,
                };
                break;
            case 'SubCategoryId':
                request.uniqueids = {
                    InventoryTypeId: inventoryTypeId,
                    CategoryId: categoryId,
                };
                break;
        }


    };
};
//----------------------------------------------------------------------------------------------
var SalesInventoryController = new SalesInventory();