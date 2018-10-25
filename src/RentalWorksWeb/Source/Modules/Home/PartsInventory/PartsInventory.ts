routes.push({ pattern: /^module\/partsinventory$/, action: function (match: RegExpExecArray) { return PartsInventoryController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class PartsInventory extends InventoryBase {
    Module: string = 'PartsInventory';
    apiurl: string = 'api/v1/partsinventory';
    caption: string = 'Parts Inventory';
    nav: string = 'module/partsinventory';
    id: string = '351B8A09-7778-4F06-A6A2-ED0920A5C360';
    AvailableFor: string = "P";
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        let $itemLocationTaxGrid: any;
        let $itemLocationTaxGridControl: any;
        let $salesInventoryWarehouseGrid: any;
        let $salesInventoryWarehouseGridControl: any;
        let $inventoryAvailabilityGrid: any;
        let $inventoryAvailabilityGridControl: any;
        let $inventoryConsignmentGrid: any;
        let $inventoryConsignmentGridControl: any;
        let $inventoryCompleteKitGrid: any;
        let $inventoryCompleteKitGridControl: any;
        let $partsinventorySubstituteGrid: any;
        let $partsinventorySubstituteGridControl: any;
        let $partsinventoryCompatibilityGrid: any;
        let $partsinventoryCompatibilityGridControl: any;
        let $inventoryQcGrid: any;
        let $inventoryQcGridControl: any;
        let $inventoryAttributeValueGrid: any;
        let $inventoryAttributeValueGridControl: any;
        let $inventoryPrepGrid: any;
        let $inventoryPrepGridControl: any;
        let $inventoryCompleteGrid: any;
        let $inventoryCompleteGridControl: any;
        let $inventoryKitGrid: any;
        let $inventoryKitGridControl: any;

        let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        // load AttributeValue Grid
        $itemLocationTaxGrid = $form.find('div[data-grid="ItemLocationTaxGrid"]');
        $itemLocationTaxGridControl = jQuery(jQuery('#tmpl-grids-ItemLocationTaxGridBrowse').html());
        $itemLocationTaxGrid.empty().append($itemLocationTaxGridControl);
        $itemLocationTaxGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        FwBrowse.init($itemLocationTaxGridControl);
        FwBrowse.renderRuntimeHtml($itemLocationTaxGridControl);

        $salesInventoryWarehouseGrid = $form.find('div[data-grid="SalesInventoryWarehouseGrid"]');
        $salesInventoryWarehouseGridControl = jQuery(jQuery('#tmpl-grids-SalesInventoryWarehouseGridBrowse').html());
        $salesInventoryWarehouseGrid.empty().append($salesInventoryWarehouseGridControl);
        $salesInventoryWarehouseGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $salesInventoryWarehouseGridControl.data('beforesave', request => {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($salesInventoryWarehouseGridControl);
        FwBrowse.renderRuntimeHtml($salesInventoryWarehouseGridControl);

        $inventoryCompleteKitGrid = $form.find('div[data-grid="InventoryCompleteKitGrid"]');
        $inventoryCompleteKitGridControl = jQuery(jQuery('#tmpl-grids-InventoryCompleteKitGridBrowse').html());
        $inventoryCompleteKitGrid.empty().append($inventoryCompleteKitGridControl);
        $inventoryCompleteKitGridControl.data('ondatabind', request => {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        FwBrowse.init($inventoryCompleteKitGridControl);
        FwBrowse.renderRuntimeHtml($inventoryCompleteKitGridControl);

        $partsinventorySubstituteGrid = $form.find('div[data-grid="PartsInventorySubstituteGrid"]');
        $partsinventorySubstituteGridControl = jQuery(jQuery('#tmpl-grids-PartsInventorySubstituteGridBrowse').html());
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

        $partsinventoryCompatibilityGrid = $form.find('div[data-grid="PartsInventoryCompatibilityGrid"]');
        $partsinventoryCompatibilityGridControl = jQuery(jQuery('#tmpl-grids-PartsInventoryCompatibilityGridBrowse').html());
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

        $inventoryAttributeValueGrid = $form.find('div[data-grid="InventoryAttributeValueGrid"]');
        $inventoryAttributeValueGridControl = jQuery(jQuery('#tmpl-grids-InventoryAttributeValueGridBrowse').html());
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

        $inventoryPrepGrid = $form.find('div[data-grid="InventoryPrepGrid"]');
        $inventoryPrepGridControl = jQuery(jQuery('#tmpl-grids-InventoryPrepGridBrowse').html());
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

        $inventoryCompleteGrid = $form.find('div[data-grid="InventoryCompleteGrid"]');
        $inventoryCompleteGridControl = jQuery(jQuery('#tmpl-grids-InventoryCompleteGridBrowse').html());
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
            var primaryRowIndex;
            if (primaryRowIndex === undefined) {
                var orderByIndex = dt.ColumnIndex.OrderBy;
                var inventoryIdIndex = dt.ColumnIndex.InventoryId
                for (var i = 0; i < dt.Rows.length; i++) {
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

        $inventoryKitGrid = $form.find('div[data-grid="InventoryKitGrid"]');
        $inventoryKitGridControl = jQuery(jQuery('#tmpl-grids-InventoryKitGridBrowse').html());
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
            var primaryRowIndex;
            if (primaryRowIndex === undefined) {
                var orderByIndex = dt.ColumnIndex.OrderBy;
                var inventoryIdIndex = dt.ColumnIndex.InventoryId
                for (var i = 0; i < dt.Rows.length; i++) {
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
    };

    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        super.afterLoad($form);
        let $itemLocationTaxGrid: any;
        let $salesInventoryWarehouseGrid: any;
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

        $itemLocationTaxGrid = $form.find('[data-name="ItemLocationTaxGrid"]');
        //FwBrowse.search($itemLocationTaxGrid);
        $salesInventoryWarehouseGrid = $form.find('[data-name="SalesInventoryWarehouseGrid"]');
        FwBrowse.search($salesInventoryWarehouseGrid);
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
var PartsInventoryController = new PartsInventory();