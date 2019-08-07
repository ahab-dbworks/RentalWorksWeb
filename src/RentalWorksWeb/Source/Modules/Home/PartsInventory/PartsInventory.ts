routes.push({ pattern: /^module\/partsinventory$/, action: function (match: RegExpExecArray) { return PartsInventoryController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class PartsInventory extends InventoryBase {
    Module: string = 'PartsInventory';
    apiurl: string = 'api/v1/partsinventory';
    caption: string = Constants.Modules.Home.PartsInventory.caption;
	nav: string = Constants.Modules.Home.PartsInventory.nav;
	id: string = Constants.Modules.Home.PartsInventory.id;
    AvailableFor: string = "P";
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        // ----------
        const $inventoryLocationTaxGrid: any = $form.find('div[data-grid="InventoryLocationTaxGrid"]');
        const $inventoryLocationTaxGridControl: any = FwBrowse.loadGridFromTemplate('InventoryLocationTaxGrid');
        $inventoryLocationTaxGrid.empty().append($inventoryLocationTaxGridControl);
        $inventoryLocationTaxGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        FwBrowse.init($inventoryLocationTaxGridControl);
        FwBrowse.renderRuntimeHtml($inventoryLocationTaxGridControl);
        // ----------
        const $partsInventoryWarehouseGrid: any = $form.find('div[data-grid="PartsInventoryWarehouseGrid"]');
        const $partsInventoryWarehouseGridControl: any = FwBrowse.loadGridFromTemplate('PartsInventoryWarehouseGrid');
        $partsInventoryWarehouseGrid.empty().append($partsInventoryWarehouseGridControl);
        $partsInventoryWarehouseGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
            request.miscfields = {
                UserWarehouseId: warehouse.warehouseid
            };
            request.pagesize = 100;  //justin 04/01/2019 #359 show all active warehouses here
        });
        $partsInventoryWarehouseGridControl.data('beforesave', request => {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($partsInventoryWarehouseGridControl);
        FwBrowse.renderRuntimeHtml($partsInventoryWarehouseGridControl);
        // ----------
        const $partsInventoryWarehousePricingGrid: any = $form.find('div[data-grid="PartsInventoryWarehousePricingGrid"]');
        const $partsInventoryWarehouseGridPricingControl: any = FwBrowse.loadGridFromTemplate('PartsInventoryWarehousePricingGrid');
        $partsInventoryWarehousePricingGrid.empty().append($partsInventoryWarehouseGridPricingControl);
        $partsInventoryWarehouseGridPricingControl.data('ondatabind', request => {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
            request.miscfields = {
                UserWarehouseId: warehouse.warehouseid
            };
            request.pagesize = 100;  //justin 04/01/2019 #359 show all active warehouses here
        });
        $partsInventoryWarehouseGridPricingControl.data('beforesave', request => {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($partsInventoryWarehouseGridPricingControl);
        FwBrowse.renderRuntimeHtml($partsInventoryWarehouseGridPricingControl);
        // ----------
        const $inventoryCompleteKitGrid: any = $form.find('div[data-grid="InventoryCompleteKitGrid"]');
        const $inventoryCompleteKitGridControl: any = FwBrowse.loadGridFromTemplate('InventoryCompleteKitGrid');
        $inventoryCompleteKitGrid.empty().append($inventoryCompleteKitGridControl);
        $inventoryCompleteKitGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        FwBrowse.init($inventoryCompleteKitGridControl);
        FwBrowse.renderRuntimeHtml($inventoryCompleteKitGridControl);
        // ----------
        const $partsinventorySubstituteGrid: any = $form.find('div[data-grid="PartsInventorySubstituteGrid"]');
        const $partsinventorySubstituteGridControl: any = FwBrowse.loadGridFromTemplate('PartsInventorySubstituteGrid');
        $partsinventorySubstituteGrid.empty().append($partsinventorySubstituteGridControl);
        $partsinventorySubstituteGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $partsinventorySubstituteGridControl.data('beforesave', request => {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($partsinventorySubstituteGridControl);
        FwBrowse.renderRuntimeHtml($partsinventorySubstituteGridControl);
        // ----------
        const $partsinventoryCompatibilityGrid: any = $form.find('div[data-grid="PartsInventoryCompatibilityGrid"]');
        const $partsinventoryCompatibilityGridControl: any = FwBrowse.loadGridFromTemplate('PartsInventoryCompatibilityGrid');
        $partsinventoryCompatibilityGrid.empty().append($partsinventoryCompatibilityGridControl);
        $partsinventoryCompatibilityGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $partsinventoryCompatibilityGridControl.data('beforesave', request => {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($partsinventoryCompatibilityGridControl);
        FwBrowse.renderRuntimeHtml($partsinventoryCompatibilityGridControl);
        // ----------
        const $inventoryAttributeValueGrid: any = $form.find('div[data-grid="InventoryAttributeValueGrid"]');
        const $inventoryAttributeValueGridControl: any = FwBrowse.loadGridFromTemplate('InventoryAttributeValueGrid');
        $inventoryAttributeValueGrid.empty().append($inventoryAttributeValueGridControl);
        $inventoryAttributeValueGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryAttributeValueGridControl.data('beforesave', request => {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryAttributeValueGridControl);
        FwBrowse.renderRuntimeHtml($inventoryAttributeValueGridControl);
        // ----------
        const $inventoryPrepGrid: any = $form.find('div[data-grid="InventoryPrepGrid"]');
        const $inventoryPrepGridControl: any = FwBrowse.loadGridFromTemplate('InventoryPrepGrid');
        $inventoryPrepGrid.empty().append($inventoryPrepGridControl);
        $inventoryPrepGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryPrepGridControl.data('beforesave', request => {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryPrepGridControl);
        FwBrowse.renderRuntimeHtml($inventoryPrepGridControl);
        // ----------
        const $inventoryCompleteGrid: any = $form.find('div[data-grid="InventoryCompleteGrid"]');
        const $inventoryCompleteGridControl: any = FwBrowse.loadGridFromTemplate('InventoryCompleteGrid');
        $inventoryCompleteGridControl.find('div[data-datafield="InventoryId"]').attr('data-validationname', 'PartsInventoryValidation');
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
        //$inventoryCompleteGridControl.data('afterdatabindcallback', function ($control, dt) {
        //    var orderByIndex = dt.ColumnIndex.OrderBy;
        //    var inventoryIdIndex = dt.ColumnIndex.InventoryId
        //    for (var i = 0; i < dt.Rows.length; i++) {
        //        if (dt.Rows[i][orderByIndex] === 1 && dt.Rows[i][inventoryIdIndex] !== '') {
        //            primaryRowIndex = i
        //        }
        //    }

        //});
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
        const $inventoryKitGrid: any = $form.find('div[data-grid="InventoryKitGrid"]');
        const $inventoryKitGridControl: any = FwBrowse.loadGridFromTemplate('InventoryKitGrid');
        $inventoryKitGridControl.find('div[data-datafield="InventoryId"]').attr('data-validationname', 'PartsInventoryValidation');
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
        let $itemLocationTaxGrid: any;
        let $inventoryAvailabilityGrid: any;
        let $inventoryConsignmentGrid: any;
        let $inventoryCompleteKitGrid: any;
        let $partsinventorySubstituteGrid: any;
        let $partsinventoryCompatibilityGrid: any;
        let $inventoryQcGrid: any;
        let $inventoryAttributeValueGrid: any;
        let $inventoryVendorGrid: any;
        let $inventoryPrepGrid: any;
        let $wardrobeInventoryColorGrid: any;
        let $wardrobeInventoryMaterialGrid: any;
        let $inventoryCompleteGrid: any;
        let $inventoryKitGrid: any;

        const $partsInventoryWarehouseGrid = $form.find('[data-name="PartsInventoryWarehouseGrid"]');
        FwBrowse.search($partsInventoryWarehouseGrid);
        const $partsInventoryWarehousePricingGrid = $form.find('[data-name="PartsInventoryWarehousePricingGrid"]');
        FwBrowse.search($partsInventoryWarehousePricingGrid);

        $itemLocationTaxGrid = $form.find('[data-name="ItemLocationTaxGrid"]');
        //FwBrowse.search($itemLocationTaxGrid);
        $inventoryAvailabilityGrid = $form.find('[data-name="InventoryAvailabilityGrid"]');
        //FwBrowse.search($inventoryAvailabilityGrid);
        $inventoryConsignmentGrid = $form.find('[data-name="InventoryConsignmentGrid"]');
        //FwBrowse.search($inventoryConsignmentGrid);
        $inventoryCompleteKitGrid = $form.find('[data-name="InventoryCompleteKitGrid"]');
        //FwBrowse.search($inventoryCompleteKitGrid);
        $partsinventorySubstituteGrid = $form.find('[data-name="PartsInventorySubstituteGrid"]');
        //FwBrowse.search($partsinventorySubstituteGrid);
        $partsinventoryCompatibilityGrid = $form.find('[data-name="PartsInventoryCompatibilityGrid"]');
        //FwBrowse.search($partsinventoryCompatibilityGrid);
        $inventoryQcGrid = $form.find('[data-name="InventoryQcGrid"]');
        //FwBrowse.search($inventoryQcGrid);
        $inventoryAttributeValueGrid = $form.find('[data-name="InventoryAttributeValueGrid"]');
        //FwBrowse.search($inventoryAttributeValueGrid);
        $inventoryVendorGrid = $form.find('[data-name="InventoryVendorGrid"]');
        //FwBrowse.search($inventoryVendorGrid);
        $inventoryPrepGrid = $form.find('[data-name="InventoryPrepGrid"]');
        //FwBrowse.search($inventoryPrepGrid);
        $wardrobeInventoryColorGrid = $form.find('[data-name="WardrobeInventoryColorGrid"]');
        //FwBrowse.search($wardrobeInventoryColorGrid);
        $wardrobeInventoryMaterialGrid = $form.find('[data-name="WardrobeInventoryMaterialGrid"]');
        //FwBrowse.search($wardrobeInventoryMaterialGrid);
        $inventoryCompleteGrid = $form.find('[data-name="InventoryCompleteGrid"]');
        //FwBrowse.search($inventoryCompleteGrid);
        $inventoryKitGrid = $form.find('[data-name="InventoryKitGrid"]');
        //FwBrowse.search($inventoryKitGrid);

        this.afterLoadSetClassification($form);

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
    beforeValidate($browse, $grid, request) {
        const validationName = request.module;
        const InventoryTypeValue = jQuery($grid.find('[data-validationname="InventoryTypeValidation"] input')).val();
        const CategoryTypeId = jQuery($grid.find('[data-validationname="PartsCategoryValidation"] input')).val();

        switch (validationName) {
            case 'InventoryTypeValidation':
                request.uniqueids = {
                    Parts: true
                };
                break;
            case 'PartsCategoryValidation':
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
FwApplicationTree.clickEvents[Constants.Modules.Home.PartsInventory.form.menuItems.CreateComplete.id] = (e: JQuery.ClickEvent) => {
    try {
        const $form = jQuery(e.currentTarget).closest('.fwform');
        const inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
        FwAppData.apiMethod(true, 'POST', `api/v1/inventorycompletekit/createcomplete/${inventoryId}`, null, FwServices.defaultTimeout,
            // onsuccess
            (response: any) => {
                try {
                    const uniqueIds: any = {};
                    uniqueIds.InventoryId = response.PackageId;
                    const $completeForm = PartsInventoryController.loadForm(uniqueIds);
                    FwModule.openSubModuleTab($form, $completeForm);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, 
            // onfailure
            (ex: any) => {
                FwFunc.showError(ex);
            }, $form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
var PartsInventoryController = new PartsInventory();