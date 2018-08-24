class RentalInventory extends InventoryBase {
    constructor() {
        super(...arguments);
        this.Module = 'RentalInventory';
        this.apiurl = 'api/v1/rentalinventory';
        this.caption = 'Rental Inventory';
        this.AvailableFor = "R";
    }
    openFormInventory($form) {
        FwFormField.loadItems($form.find('.lamps'), [
            { value: '0', text: '0' },
            { value: '1', text: '1' },
            { value: '2', text: '2' },
            { value: '3', text: '3' },
            { value: '4', text: '4' }
        ], true);
    }
    ;
    renderGrids($form) {
        var $itemLocationTaxGrid;
        var $itemLocationTaxGridControl;
        var $rentalInventoryWarehouseGrid;
        var $rentalInventoryWarehouseGridControl;
        var $inventoryAvailabilityGrid;
        var $inventoryAvailabilityGridControl;
        var $inventoryConsignmentGrid;
        var $inventoryConsignmentGridControl;
        var $inventoryCompleteKitGrid;
        var $inventoryCompleteKitGridControl;
        var $inventorySubstituteGrid;
        var $inventorySubstituteGridControl;
        var $inventoryCompatibilityGrid;
        var $inventoryCompatibilityGridControl;
        var $inventoryQcGrid;
        var $inventoryQcGridControl;
        var $inventoryAttributeValueGrid;
        var $inventoryAttributeValueGridControl;
        var $inventoryVendorGrid;
        var $inventoryVendorGridControl;
        var $inventoryPrepGrid;
        var $inventoryPrepGridControl;
        var $inventoryContainerGrid;
        var $inventoryContainerGridControl;
        var $inventoryCompleteGrid;
        var $inventoryCompleteGridControl;
        var $inventoryWarehouseStagingGrid;
        var $inventoryWarehouseStagingGridControl;
        var $inventoryKitGrid;
        var $inventoryKitGridControl;
        var $wardrobeInventoryColorGrid;
        var $wardrobeInventoryColorGridControl;
        var $wardrobeInventoryMaterialGrid;
        var $wardrobeInventoryMaterialGridControl;
        var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        $itemLocationTaxGrid = $form.find('div[data-grid="ItemLocationTaxGrid"]');
        $itemLocationTaxGridControl = jQuery(jQuery('#tmpl-grids-ItemLocationTaxGridBrowse').html());
        $itemLocationTaxGrid.empty().append($itemLocationTaxGridControl);
        $itemLocationTaxGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $itemLocationTaxGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        });
        FwBrowse.init($itemLocationTaxGridControl);
        FwBrowse.renderRuntimeHtml($itemLocationTaxGridControl);
        $rentalInventoryWarehouseGrid = $form.find('div[data-grid="RentalInventoryWarehouseGrid"]');
        $rentalInventoryWarehouseGridControl = jQuery(jQuery('#tmpl-grids-RentalInventoryWarehouseGridBrowse').html());
        $rentalInventoryWarehouseGrid.empty().append($rentalInventoryWarehouseGridControl);
        $rentalInventoryWarehouseGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $rentalInventoryWarehouseGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        });
        FwBrowse.init($rentalInventoryWarehouseGridControl);
        FwBrowse.renderRuntimeHtml($rentalInventoryWarehouseGridControl);
        $inventoryAvailabilityGrid = $form.find('div[data-grid="InventoryAvailabilityGrid"]');
        $inventoryAvailabilityGridControl = jQuery(jQuery('#tmpl-grids-InventoryAvailabilityGridBrowse').html());
        $inventoryAvailabilityGrid.empty().append($inventoryAvailabilityGridControl);
        $inventoryAvailabilityGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryAvailabilityGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        });
        FwBrowse.init($inventoryAvailabilityGridControl);
        FwBrowse.renderRuntimeHtml($inventoryAvailabilityGridControl);
        $inventoryConsignmentGrid = $form.find('div[data-grid="InventoryConsignmentGrid"]');
        $inventoryConsignmentGridControl = jQuery(jQuery('#tmpl-grids-InventoryConsignmentGridBrowse').html());
        $inventoryConsignmentGrid.empty().append($inventoryConsignmentGridControl);
        $inventoryConsignmentGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryConsignmentGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        });
        FwBrowse.init($inventoryConsignmentGridControl);
        FwBrowse.renderRuntimeHtml($inventoryConsignmentGridControl);
        $inventoryCompleteKitGrid = $form.find('div[data-grid="InventoryCompleteKitGrid"]');
        $inventoryCompleteKitGridControl = jQuery(jQuery('#tmpl-grids-InventoryCompleteKitGridBrowse').html());
        $inventoryCompleteKitGrid.empty().append($inventoryCompleteKitGridControl);
        $inventoryCompleteKitGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryCompleteKitGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        });
        FwBrowse.init($inventoryCompleteKitGridControl);
        FwBrowse.renderRuntimeHtml($inventoryCompleteKitGridControl);
        $inventorySubstituteGrid = $form.find('div[data-grid="InventorySubstituteGrid"]');
        $inventorySubstituteGridControl = jQuery(jQuery('#tmpl-grids-InventorySubstituteGridBrowse').html());
        $inventorySubstituteGrid.empty().append($inventorySubstituteGridControl);
        $inventorySubstituteGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(),
                WarehouseId: warehouse.warehouseid
            };
        });
        $inventorySubstituteGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        });
        FwBrowse.init($inventorySubstituteGridControl);
        FwBrowse.renderRuntimeHtml($inventorySubstituteGridControl);
        $inventoryCompatibilityGrid = $form.find('div[data-grid="InventoryCompatibilityGrid"]');
        $inventoryCompatibilityGridControl = jQuery(jQuery('#tmpl-grids-InventoryCompatibilityGridBrowse').html());
        $inventoryCompatibilityGrid.empty().append($inventoryCompatibilityGridControl);
        $inventoryCompatibilityGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryCompatibilityGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        });
        FwBrowse.init($inventoryCompatibilityGridControl);
        FwBrowse.renderRuntimeHtml($inventoryCompatibilityGridControl);
        $inventoryQcGrid = $form.find('div[data-grid="InventoryQcGrid"]');
        $inventoryQcGridControl = jQuery(jQuery('#tmpl-grids-InventoryQcGridBrowse').html());
        $inventoryQcGrid.empty().append($inventoryQcGridControl);
        $inventoryQcGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryQcGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        });
        FwBrowse.init($inventoryQcGridControl);
        FwBrowse.renderRuntimeHtml($inventoryQcGridControl);
        $inventoryAttributeValueGrid = $form.find('div[data-grid="InventoryAttributeValueGrid"]');
        $inventoryAttributeValueGridControl = jQuery(jQuery('#tmpl-grids-InventoryAttributeValueGridBrowse').html());
        $inventoryAttributeValueGrid.empty().append($inventoryAttributeValueGridControl);
        $inventoryAttributeValueGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryAttributeValueGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        });
        FwBrowse.init($inventoryAttributeValueGridControl);
        FwBrowse.renderRuntimeHtml($inventoryAttributeValueGridControl);
        $inventoryVendorGrid = $form.find('div[data-grid="InventoryVendorGrid"]');
        $inventoryVendorGridControl = jQuery(jQuery('#tmpl-grids-InventoryVendorGridBrowse').html());
        $inventoryVendorGrid.empty().append($inventoryVendorGridControl);
        $inventoryVendorGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryVendorGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        });
        FwBrowse.init($inventoryVendorGridControl);
        FwBrowse.renderRuntimeHtml($inventoryVendorGridControl);
        $inventoryPrepGrid = $form.find('div[data-grid="InventoryPrepGrid"]');
        $inventoryPrepGridControl = jQuery(jQuery('#tmpl-grids-InventoryPrepGridBrowse').html());
        $inventoryPrepGrid.empty().append($inventoryPrepGridControl);
        $inventoryPrepGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryPrepGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        });
        FwBrowse.init($inventoryPrepGridControl);
        FwBrowse.renderRuntimeHtml($inventoryPrepGridControl);
        $inventoryContainerGrid = $form.find('div[data-grid="InventoryContainerGrid"]');
        $inventoryContainerGridControl = jQuery(jQuery('#tmpl-grids-InventoryContainerGridBrowse').html());
        $inventoryContainerGrid.empty().append($inventoryContainerGridControl);
        $inventoryContainerGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                PackageId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryContainerGridControl.data('beforesave', function (request) {
            request.PackageId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
            request.ContainerId = $form.find('div.fwformfield[data-datafield="ContainerId"] input').val();
        });
        FwBrowse.init($inventoryContainerGridControl);
        FwBrowse.renderRuntimeHtml($inventoryContainerGridControl);
        $inventoryCompleteGrid = $form.find('div[data-grid="InventoryCompleteGrid"]');
        $inventoryCompleteGridControl = jQuery(jQuery('#tmpl-grids-InventoryCompleteGridBrowse').html());
        $inventoryCompleteGrid.empty().append($inventoryCompleteGridControl);
        $inventoryCompleteGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                PackageId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(),
                WarehouseId: warehouse.warehouseid
            };
        });
        $inventoryCompleteGridControl.data('beforesave', function (request) {
            request.PackageId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        });
        $inventoryCompleteGridControl.data('isfieldeditable', function ($field, dt, rowIndex) {
            var primaryRowIndex;
            if (primaryRowIndex === undefined) {
                var orderByIndex = dt.ColumnIndex.OrderBy;
                var inventoryIdIndex = dt.ColumnIndex.InventoryId;
                for (var i = 0; i < dt.Rows.length; i++) {
                    if (dt.Rows[i][orderByIndex] === 1 && dt.Rows[i][inventoryIdIndex] !== '') {
                        primaryRowIndex = i;
                    }
                }
            }
            if (rowIndex === primaryRowIndex) {
                return true;
            }
            else {
                return false;
            }
        });
        FwBrowse.init($inventoryCompleteGridControl);
        FwBrowse.renderRuntimeHtml($inventoryCompleteGridControl);
        $inventoryWarehouseStagingGrid = $form.find('div[data-grid="InventoryWarehouseStagingGrid"]');
        $inventoryWarehouseStagingGridControl = jQuery(jQuery('#tmpl-grids-InventoryWarehouseStagingGridBrowse').html());
        $inventoryWarehouseStagingGrid.empty().append($inventoryWarehouseStagingGridControl);
        $inventoryWarehouseStagingGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryWarehouseStagingGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        });
        FwBrowse.init($inventoryWarehouseStagingGridControl);
        FwBrowse.renderRuntimeHtml($inventoryWarehouseStagingGridControl);
        $inventoryKitGrid = $form.find('div[data-grid="InventoryKitGrid"]');
        $inventoryKitGridControl = jQuery(jQuery('#tmpl-grids-InventoryKitGridBrowse').html());
        $inventoryKitGrid.empty().append($inventoryKitGridControl);
        $inventoryKitGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                PackageId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryKitGridControl.data('beforesave', function (request) {
            request.PackageId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        });
        $inventoryKitGridControl.data('isfieldeditable', function ($field, dt, rowIndex) {
            var primaryRowIndex;
            if (primaryRowIndex === undefined) {
                var orderByIndex = dt.ColumnIndex.OrderBy;
                var inventoryIdIndex = dt.ColumnIndex.InventoryId;
                for (var i = 0; i < dt.Rows.length; i++) {
                    if (dt.Rows[i][orderByIndex] === 1 && dt.Rows[i][inventoryIdIndex] !== '') {
                        primaryRowIndex = i;
                    }
                }
            }
            if (rowIndex === primaryRowIndex) {
                return true;
            }
            else {
                return false;
            }
        });
        FwBrowse.init($inventoryKitGridControl);
        FwBrowse.renderRuntimeHtml($inventoryKitGridControl);
        $wardrobeInventoryColorGrid = $form.find('div[data-grid="WardrobeInventoryColorGrid"]');
        $wardrobeInventoryColorGridControl = jQuery(jQuery('#tmpl-grids-WardrobeInventoryColorGridBrowse').html());
        $wardrobeInventoryColorGrid.empty().append($wardrobeInventoryColorGridControl);
        $wardrobeInventoryColorGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $wardrobeInventoryColorGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        });
        FwBrowse.init($wardrobeInventoryColorGridControl);
        FwBrowse.renderRuntimeHtml($wardrobeInventoryColorGridControl);
        $wardrobeInventoryMaterialGrid = $form.find('div[data-grid="WardrobeInventoryMaterialGrid"]');
        $wardrobeInventoryMaterialGridControl = jQuery(jQuery('#tmpl-grids-WardrobeInventoryMaterialGridBrowse').html());
        $wardrobeInventoryMaterialGrid.empty().append($wardrobeInventoryMaterialGridControl);
        $wardrobeInventoryMaterialGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $wardrobeInventoryMaterialGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        });
        FwBrowse.init($wardrobeInventoryMaterialGridControl);
        FwBrowse.renderRuntimeHtml($wardrobeInventoryMaterialGridControl);
    }
    ;
    afterLoad($form) {
        var self = this;
        var $itemLocationTaxGrid;
        var $rentalInventoryWarehouseGrid;
        var $inventoryAvailabilityGrid;
        var $inventoryConsignmentGrid;
        var $inventoryCompleteKitGrid;
        var $inventorySubstituteGrid;
        var $inventoryCompatibilityGrid;
        var $inventoryQcGrid;
        var $inventoryAttributeValueGrid;
        var $inventoryVendorGrid;
        var $inventoryPrepGrid;
        var $inventoryContainerGrid;
        var $inventoryCompleteGrid;
        var $inventoryWarehouseStagingGrid;
        var $inventoryKitGrid;
        var $wardrobeInventoryColorGrid;
        var $wardrobeInventoryMaterialGrid;
        let $submoduleAssetBrowse, classificationValue, trackedByValue;
        classificationValue = FwFormField.getValueByDataField($form, 'Classification');
        trackedByValue = FwFormField.getValueByDataField($form, 'TrackedBy');
        $itemLocationTaxGrid = $form.find('[data-name="ItemLocationTaxGrid"]');
        FwBrowse.search($itemLocationTaxGrid);
        $rentalInventoryWarehouseGrid = $form.find('[data-name="RentalInventoryWarehouseGrid"]');
        FwBrowse.search($rentalInventoryWarehouseGrid);
        $inventoryAvailabilityGrid = $form.find('[data-name="InventoryAvailabilityGrid"]');
        FwBrowse.search($inventoryAvailabilityGrid);
        $inventoryConsignmentGrid = $form.find('[data-name="InventoryConsignmentGrid"]');
        FwBrowse.search($inventoryConsignmentGrid);
        $inventoryCompleteKitGrid = $form.find('[data-name="InventoryCompleteKitGrid"]');
        FwBrowse.search($inventoryCompleteKitGrid);
        $inventorySubstituteGrid = $form.find('[data-name="InventorySubstituteGrid"]');
        FwBrowse.search($inventorySubstituteGrid);
        $inventoryCompatibilityGrid = $form.find('[data-name="InventoryCompatibilityGrid"]');
        FwBrowse.search($inventoryCompatibilityGrid);
        $inventoryQcGrid = $form.find('[data-name="InventoryQcGrid"]');
        FwBrowse.search($inventoryQcGrid);
        $inventoryAttributeValueGrid = $form.find('[data-name="InventoryAttributeValueGrid"]');
        FwBrowse.search($inventoryAttributeValueGrid);
        $inventoryVendorGrid = $form.find('[data-name="InventoryVendorGrid"]');
        FwBrowse.search($inventoryVendorGrid);
        $inventoryPrepGrid = $form.find('[data-name="InventoryPrepGrid"]');
        FwBrowse.search($inventoryPrepGrid);
        $inventoryContainerGrid = $form.find('[data-name="InventoryContainerGrid"]');
        FwBrowse.search($inventoryContainerGrid);
        $inventoryCompleteGrid = $form.find('[data-name="InventoryCompleteGrid"]');
        FwBrowse.search($inventoryCompleteGrid);
        $inventoryWarehouseStagingGrid = $form.find('[data-name="InventoryWarehouseStagingGrid"]');
        FwBrowse.search($inventoryWarehouseStagingGrid);
        $inventoryKitGrid = $form.find('[data-name="InventoryKitGrid"]');
        FwBrowse.search($inventoryKitGrid);
        $wardrobeInventoryColorGrid = $form.find('[data-name="WardrobeInventoryColorGrid"]');
        FwBrowse.search($wardrobeInventoryColorGrid);
        $wardrobeInventoryMaterialGrid = $form.find('[data-name="WardrobeInventoryMaterialGrid"]');
        FwBrowse.search($wardrobeInventoryMaterialGrid);
        this.afterLoadSetClassification($form);
        if ($form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
        }
        else {
            FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
        }
        if ($form.find('[data-datafield="InventoryTypeIsWardrobe"] .fwformfield-value').prop('checked') === true) {
            $form.find('.wardrobetab').show();
        }
        ;
        if ($form.find('[data-datafield="SubCategoryCount"] .fwformfield-value').val() > 0) {
            FwFormField.enable($form.find('.subcategory'));
        }
        else {
            FwFormField.disable($form.find('.subcategory'));
        }
        this.addAssetTab($form);
    }
    ;
    addAssetTab($form) {
        let $submoduleAssetBrowse, classificationValue, trackedByValue;
        classificationValue = FwFormField.getValueByDataField($form, 'Classification');
        trackedByValue = FwFormField.getValueByDataField($form, 'TrackedBy');
        if (classificationValue === 'I' || classificationValue === 'A') {
            if (trackedByValue !== 'QUANTITY') {
                $form.find('.asset-submodule').show();
                $submoduleAssetBrowse = this.openAssetBrowse($form);
                $form.find('.asset-submodule-page').append($submoduleAssetBrowse);
                $submoduleAssetBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
                $submoduleAssetBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
                    var $assetForm, controller, $browse, assetFormData = {};
                    $browse = jQuery(this).closest('.fwbrowse');
                    controller = $browse.attr('data-controller');
                    assetFormData.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                    if (typeof window[controller] !== 'object')
                        throw 'Missing javascript module: ' + controller;
                    if (typeof window[controller]['openForm'] !== 'function')
                        throw 'Missing javascript function: ' + controller + '.openForm';
                    $assetForm = window[controller]['openForm']('NEW', assetFormData);
                    FwModule.openSubModuleTab($browse, $assetForm);
                });
            }
        }
    }
    ;
    openAssetBrowse($form) {
        let $browse;
        $browse = AssetController.openBrowse();
        $browse.data('ondatabind', request => {
            request.ActiveView = AssetController.ActiveView;
            request.uniqueids = {
                InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
            };
        });
        return $browse;
    }
    ;
    beforeValidate($browse, $grid, request) {
        var validationName = request.module;
        var InventoryTypeValue = jQuery($grid.find('[data-validationname="InventoryTypeValidation"] input')).val();
        var CategoryTypeId = jQuery($grid.find('[data-validationname="RentalCategoryValidation"] input')).val();
        switch (validationName) {
            case 'InventoryTypeValidation':
                request.uniqueids = {
                    Rental: true
                };
                break;
            case 'RentalCategoryValidation':
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
        }
        ;
    }
    ;
}
;
var RentalInventoryController = new RentalInventory();
//# sourceMappingURL=RentalInventory.js.map