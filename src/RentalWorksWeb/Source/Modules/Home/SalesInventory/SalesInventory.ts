class SalesInventory extends InventoryBase {
    Module: string = 'SalesInventory';
    apiurl: string = 'api/v1/salesinventory';
    caption: string = Constants.Modules.Home.SalesInventory.caption;
	nav: string = Constants.Modules.Home.SalesInventory.nav;
	id: string = Constants.Modules.Home.SalesInventory.id;
    AvailableFor: string = "S";
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        // ----------
        const $inventoryLocationTaxGrid = $form.find('div[data-grid="InventoryLocationTaxGrid"]');
        const $inventoryLocationTaxGridControl = FwBrowse.loadGridFromTemplate('InventoryLocationTaxGrid');
        $inventoryLocationTaxGrid.empty().append($inventoryLocationTaxGridControl);
        $inventoryLocationTaxGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        FwBrowse.init($inventoryLocationTaxGridControl);
        FwBrowse.renderRuntimeHtml($inventoryLocationTaxGridControl);
        // ----------
        const $salesInventoryWarehouseGrid = $form.find('div[data-grid="SalesInventoryWarehouseGrid"]');
        const $salesInventoryWarehouseGridControl = FwBrowse.loadGridFromTemplate('SalesInventoryWarehouseGrid');
        $salesInventoryWarehouseGrid.empty().append($salesInventoryWarehouseGridControl);
        $salesInventoryWarehouseGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
            request.pagesize = 100;  //justin 04/01/2019 #359 show all active warehouses here
        });
        $salesInventoryWarehouseGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($salesInventoryWarehouseGridControl);
        FwBrowse.renderRuntimeHtml($salesInventoryWarehouseGridControl);
        // ----------
        const $inventoryAvailabilityGrid = $form.find('div[data-grid="InventoryAvailabilityGrid"]');
        const $inventoryAvailabilityGridControl = FwBrowse.loadGridFromTemplate('InventoryAvailabilityGrid');
        $inventoryAvailabilityGrid.empty().append($inventoryAvailabilityGridControl);
        $inventoryAvailabilityGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryAvailabilityGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryAvailabilityGridControl);
        FwBrowse.renderRuntimeHtml($inventoryAvailabilityGridControl);
        // ----------
        const $inventoryConsignmentGrid = $form.find('div[data-grid="InventoryConsignmentGrid"]');
        const $inventoryConsignmentGridControl = FwBrowse.loadGridFromTemplate('InventoryConsignmentGrid');
        $inventoryConsignmentGrid.empty().append($inventoryConsignmentGridControl);
        $inventoryConsignmentGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        FwBrowse.init($inventoryConsignmentGridControl);
        FwBrowse.renderRuntimeHtml($inventoryConsignmentGridControl);
        // ----------
        const $inventoryCompleteKitGrid = $form.find('div[data-grid="InventoryCompleteKitGrid"]');
        const $inventoryCompleteKitGridControl = FwBrowse.loadGridFromTemplate('InventoryCompleteKitGrid');
        $inventoryCompleteKitGrid.empty().append($inventoryCompleteKitGridControl);
        $inventoryCompleteKitGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        FwBrowse.init($inventoryCompleteKitGridControl);
        FwBrowse.renderRuntimeHtml($inventoryCompleteKitGridControl);
        // ----------
        const $inventorySubstituteGrid = $form.find('div[data-grid="SalesInventorySubstituteGrid"]');
        const $inventorySubstituteGridControl = FwBrowse.loadGridFromTemplate('SalesInventorySubstituteGrid');
        $inventorySubstituteGrid.empty().append($inventorySubstituteGridControl);
        $inventorySubstituteGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventorySubstituteGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventorySubstituteGridControl);
        FwBrowse.renderRuntimeHtml($inventorySubstituteGridControl);
        // ----------
        const $inventoryCompatibilityGrid = $form.find('div[data-grid="SalesInventoryCompatibilityGrid"]');
        const $inventoryCompatibilityGridControl = FwBrowse.loadGridFromTemplate('SalesInventoryCompatibilityGrid');
        $inventoryCompatibilityGrid.empty().append($inventoryCompatibilityGridControl);
        $inventoryCompatibilityGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryCompatibilityGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryCompatibilityGridControl);
        FwBrowse.renderRuntimeHtml($inventoryCompatibilityGridControl);
        // ----------
        const $inventoryQcGrid = $form.find('div[data-grid="InventoryQcGrid"]');
        const $inventoryQcGridControl = FwBrowse.loadGridFromTemplate('InventoryQcGrid');
        $inventoryQcGrid.empty().append($inventoryQcGridControl);
        $inventoryQcGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        FwBrowse.init($inventoryQcGridControl);
        FwBrowse.renderRuntimeHtml($inventoryQcGridControl);
        // ----------
        const $inventoryAttributeValueGrid = $form.find('div[data-grid="InventoryAttributeValueGrid"]');
        const $inventoryAttributeValueGridControl = FwBrowse.loadGridFromTemplate('InventoryAttributeValueGrid');
        $inventoryAttributeValueGrid.empty().append($inventoryAttributeValueGridControl);
        $inventoryAttributeValueGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryAttributeValueGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryAttributeValueGridControl);
        FwBrowse.renderRuntimeHtml($inventoryAttributeValueGridControl);
        // ----------
        const $inventoryVendorGrid = $form.find('div[data-grid="InventoryVendorGrid"]');
        const $inventoryVendorGridControl = FwBrowse.loadGridFromTemplate('InventoryVendorGrid');
        $inventoryVendorGrid.empty().append($inventoryVendorGridControl);
        $inventoryVendorGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryVendorGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryVendorGridControl);
        FwBrowse.renderRuntimeHtml($inventoryVendorGridControl);
        // ----------
        const $inventoryPrepGrid = $form.find('div[data-grid="InventoryPrepGrid"]');
        const $inventoryPrepGridControl = FwBrowse.loadGridFromTemplate('InventoryPrepGrid');
        $inventoryPrepGrid.empty().append($inventoryPrepGridControl);
        $inventoryPrepGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryPrepGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryPrepGridControl);
        FwBrowse.renderRuntimeHtml($inventoryPrepGridControl);
        // ----------
        const $wardrobeInventoryColorGrid = $form.find('div[data-grid="WardrobeInventoryColorGrid"]');
        const $wardrobeInventoryColorGridControl = FwBrowse.loadGridFromTemplate('WardrobeInventoryColorGrid');
        $wardrobeInventoryColorGrid.empty().append($wardrobeInventoryColorGridControl);
        $wardrobeInventoryColorGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $wardrobeInventoryColorGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($wardrobeInventoryColorGridControl);
        FwBrowse.renderRuntimeHtml($wardrobeInventoryColorGridControl);
        // ----------
        const $wardrobeInventoryMaterialGrid = $form.find('div[data-grid="WardrobeInventoryMaterialGrid"]');
        const $wardrobeInventoryMaterialGridControl = FwBrowse.loadGridFromTemplate('WardrobeInventoryMaterialGrid');
        $wardrobeInventoryMaterialGrid.empty().append($wardrobeInventoryMaterialGridControl);
        $wardrobeInventoryMaterialGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $wardrobeInventoryMaterialGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($wardrobeInventoryMaterialGridControl);
        FwBrowse.renderRuntimeHtml($wardrobeInventoryMaterialGridControl);
        // ----------
        const $inventoryCompleteGrid = $form.find('div[data-grid="InventoryCompleteGrid"]');
        const $inventoryCompleteGridControl = FwBrowse.loadGridFromTemplate('InventoryCompleteGrid');
        $inventoryCompleteGridControl.find('div[data-datafield="InventoryId"]').attr('data-validationname', 'SalesInventoryValidation');
        $inventoryCompleteGrid.empty().append($inventoryCompleteGridControl);
        $inventoryCompleteGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                PackageId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(),
                WarehouseId: warehouse.warehouseid
            };
        });
        $inventoryCompleteGridControl.data('beforesave', function (request) {
            request.PackageId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        $inventoryCompleteGridControl.data('isfieldeditable', function ($field, dt, rowIndex) {
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
        FwBrowse.init($inventoryCompleteGridControl);
        FwBrowse.renderRuntimeHtml($inventoryCompleteGridControl);
        // ----------
        const $inventoryKitGrid = $form.find('div[data-grid="InventoryKitGrid"]');
        const $inventoryKitGridControl = FwBrowse.loadGridFromTemplate('InventoryKitGrid');
        $inventoryKitGridControl.find('div[data-datafield="InventoryId"]').attr('data-validationname', 'SalesInventoryValidation');
        $inventoryKitGrid.empty().append($inventoryKitGridControl);
        $inventoryKitGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                PackageId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryKitGridControl.data('beforesave', function (request) {
            request.PackageId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        $inventoryKitGridControl.data('isfieldeditable', function ($field, dt, rowIndex) {
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
        FwBrowse.init($inventoryKitGridControl);
        FwBrowse.renderRuntimeHtml($inventoryKitGridControl);
        // ----------
    };

    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        super.afterLoad($form);

        let $salesInventoryWarehouseGrid = $form.find('[data-name="SalesInventoryWarehouseGrid"]');
        FwBrowse.search($salesInventoryWarehouseGrid);

        this.afterLoadSetClassification($form);
        this.addAssetTab($form);

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
    addAssetTab($form: any): void {
        const classificationValue = FwFormField.getValueByDataField($form, 'Classification');
        const trackedByValue = FwFormField.getValueByDataField($form, 'TrackedBy');

        if (classificationValue === 'I' || classificationValue === 'A') {
            if (trackedByValue !== 'QUANTITY') {
                $form.find('.tab.asset').show();
                const $submoduleAssetBrowse = this.openAssetBrowse($form);
                $form.find('.tabpage.asset').html($submoduleAssetBrowse);
            }
        }
    };
    //----------------------------------------------------------------------------------------------
    openAssetBrowse($form: any) {
        const $browse = AssetController.openBrowse();
        const inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');

        $browse.data('ondatabind', request => {
            request.activiewfields = AssetController.ActiveViewFields;
            request.uniqueids = {
                InventoryId: inventoryId
            }
        });

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $grid, request) {
        const validationName = request.module;
        const InventoryTypeValue = jQuery($grid.find('[data-validationname="InventoryTypeValidation"] input')).val();
        const CategoryTypeId = jQuery($grid.find('[data-validationname="SalesCategoryValidation"] input')).val();

        switch (validationName) {
            case 'InventoryTypeValidation':
                request.uniqueids = {
                    Sales: true
                };
                break;
            case 'SalesCategoryValidation':
                request.uniqueids = {
                    InventoryTypeId: InventoryTypeValue
                };
                break;
            case 'SubCategoryValidation':
                request.uniqueids = {
                    TypeId: InventoryTypeValue,
                    CategoryId: CategoryTypeId
                };
                break;
        };
    };
};

//----------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents[Constants.Modules.Home.SalesInventory.form.menuItems.CreateComplete.id] = (e:JQuery.ClickEvent) => {
    try {
        const $form = jQuery(e.currentTarget).closest('.fwform');
        const inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
        FwAppData.apiMethod(true, 'POST', `api/v1/inventorycompletekit/createcomplete/${inventoryId}`, null, FwServices.defaultTimeout,
            response => {
                const uniqueIds: any = {};
                uniqueIds.InventoryId = response.PackageId;
                const $completeForm = SalesInventoryController.loadForm(uniqueIds);
                FwModule.openSubModuleTab($form, $completeForm);
            }, ex => {
                FwFunc.showError(ex);
            }, $form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
var SalesInventoryController = new SalesInventory();