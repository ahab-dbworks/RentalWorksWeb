class SalesInventory extends InventoryBase {
    Module: string = 'SalesInventory';
    apiurl: string = 'api/v1/salesinventory';
    caption: string = 'Sales Inventory';
    AvailableFor: string = "S";
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
        let $inventorySubstituteGrid: any;
        let $inventorySubstituteGridControl: any;
        let $inventoryCompatibilityGrid: any;
        let $inventoryCompatibilityGridControl: any;
        let $inventoryQcGrid: any;
        let $inventoryQcGridControl: any;
        let $inventoryAttributeValueGrid: any;
        let $inventoryAttributeValueGridControl: any;
        let $inventoryVendorGrid: any;
        let $inventoryVendorGridControl: any;
        let $inventoryPrepGrid: any;
        let $inventoryPrepGridControl: any;
        let $wardrobeInventoryColorGrid: any;
        let $wardrobeInventoryColorGridControl: any;
        let $wardrobeInventoryMaterialGrid: any;
        let $wardrobeInventoryMaterialGridControl: any;
        let $inventoryCompleteGrid: any;
        let $inventoryCompleteGridControl: any;
        let $inventoryKitGrid: any;
        let $inventoryKitGridControl: any;

        let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        // load AttributeValue Grid
        $itemLocationTaxGrid = $form.find('div[data-grid="ItemLocationTaxGrid"]');
        $itemLocationTaxGridControl = jQuery(jQuery('#tmpl-grids-ItemLocationTaxGridBrowse').html());
        $itemLocationTaxGrid.empty().append($itemLocationTaxGridControl);
        $itemLocationTaxGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        FwBrowse.init($itemLocationTaxGridControl);
        FwBrowse.renderRuntimeHtml($itemLocationTaxGridControl);

        $salesInventoryWarehouseGrid = $form.find('div[data-grid="SalesInventoryWarehouseGrid"]');
        $salesInventoryWarehouseGridControl = jQuery(jQuery('#tmpl-grids-SalesInventoryWarehouseGridBrowse').html());
        $salesInventoryWarehouseGrid.empty().append($salesInventoryWarehouseGridControl);
        $salesInventoryWarehouseGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $salesInventoryWarehouseGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($salesInventoryWarehouseGridControl);
        FwBrowse.renderRuntimeHtml($salesInventoryWarehouseGridControl);

        $inventoryAvailabilityGrid = $form.find('div[data-grid="InventoryAvailabilityGrid"]');
        $inventoryAvailabilityGridControl = jQuery(jQuery('#tmpl-grids-InventoryAvailabilityGridBrowse').html());
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

        $inventoryConsignmentGrid = $form.find('div[data-grid="InventoryConsignmentGrid"]');
        $inventoryConsignmentGridControl = jQuery(jQuery('#tmpl-grids-InventoryConsignmentGridBrowse').html());
        $inventoryConsignmentGrid.empty().append($inventoryConsignmentGridControl);
        $inventoryConsignmentGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
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
        FwBrowse.init($inventoryCompleteKitGridControl);
        FwBrowse.renderRuntimeHtml($inventoryCompleteKitGridControl);

        $inventorySubstituteGrid = $form.find('div[data-grid="SalesInventorySubstituteGrid"]');
        $inventorySubstituteGridControl = jQuery(jQuery('#tmpl-grids-SalesInventorySubstituteGridBrowse').html());
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

        $inventoryCompatibilityGrid = $form.find('div[data-grid="SalesInventoryCompatibilityGrid"]');
        $inventoryCompatibilityGridControl = jQuery(jQuery('#tmpl-grids-SalesInventoryCompatibilityGridBrowse').html());
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

        $inventoryQcGrid = $form.find('div[data-grid="InventoryQcGrid"]');
        $inventoryQcGridControl = jQuery(jQuery('#tmpl-grids-InventoryQcGridBrowse').html());
        $inventoryQcGrid.empty().append($inventoryQcGridControl);
        $inventoryQcGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
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
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
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
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
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
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryPrepGridControl);
        FwBrowse.renderRuntimeHtml($inventoryPrepGridControl);

        $wardrobeInventoryColorGrid = $form.find('div[data-grid="WardrobeInventoryColorGrid"]');
        $wardrobeInventoryColorGridControl = jQuery(jQuery('#tmpl-grids-WardrobeInventoryColorGridBrowse').html());
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

        $wardrobeInventoryMaterialGrid = $form.find('div[data-grid="WardrobeInventoryMaterialGrid"]');
        $wardrobeInventoryMaterialGridControl = jQuery(jQuery('#tmpl-grids-WardrobeInventoryMaterialGridBrowse').html());
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

        $inventoryCompleteGrid = $form.find('div[data-grid="InventoryCompleteGrid"]');
        $inventoryCompleteGridControl = jQuery(jQuery('#tmpl-grids-InventoryCompleteGridBrowse').html());
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
        let $inventorySubstituteGrid: any;
        let $inventoryCompatibilityGrid: any;
        let $inventoryQcGrid: any;
        let $inventoryAttributeValueGrid: any;
        let $inventoryVendorGrid: any;
        let $inventoryPrepGrid: any;
        let $wardrobeInventoryColorGrid: any;
        let $wardrobeInventoryMaterialGrid: any;
        let $inventoryCompleteGrid: any;
        let $inventoryKitGrid: any;
        let $assetBrowse;

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
        $inventorySubstituteGrid = $form.find('[data-name="SalesInventorySubstituteGrid"]');
        //FwBrowse.search($inventorySubstituteGrid);
        $inventoryCompatibilityGrid = $form.find('[data-name="SalesInventoryCompatibilityGrid"]');
        //FwBrowse.search($inventoryCompatibilityGrid);
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
        this.addAssetTab($form);
        $assetBrowse = $form.find('#AssetBrowse');
        setTimeout(() => { FwBrowse.search($assetBrowse); }, 0);
    };
    //----------------------------------------------------------------------------------------------
    addAssetTab($form: any): void {
        let $submoduleAssetBrowse, classificationValue, trackedByValue;
        classificationValue = FwFormField.getValueByDataField($form, 'Classification');
        trackedByValue = FwFormField.getValueByDataField($form, 'TrackedBy');

        if (classificationValue === 'I' || classificationValue === 'A') {
            if (trackedByValue !== 'QUANTITY') {
                $form.find('.asset-submodule').show();
                $submoduleAssetBrowse = this.openAssetBrowse($form);
                $form.find('.asset-submodule-page').append($submoduleAssetBrowse);

                //$submoduleAssetBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
                //$submoduleAssetBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
                //    var $assetForm, controller, $browse, assetFormData: any = {};
                //    $browse = jQuery(this).closest('.fwbrowse');
                //    controller = $browse.attr('data-controller');
                //    assetFormData.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                //    if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
                //    if (typeof window[controller]['openForm'] !== 'function') throw 'Missing javascript function: ' + controller + '.openForm';
                //    $assetForm = window[controller]['openForm']('NEW', assetFormData);
                //    FwModule.openSubModuleTab($browse, $assetForm);
                //});
            } else {
                $form.find('.asset-submodule').hide();
            }
        } else {
            $form.find('.asset-submodule').hide();
        }
    };
    //----------------------------------------------------------------------------------------------
    openAssetBrowse($form: any) {
        let $browse;
        $browse = AssetController.openBrowse();

        $browse.data('ondatabind', request => {
            request.ActiveView = AssetController.ActiveView;
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            }
        });

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $grid, request) {
        var validationName = request.module;
        var InventoryTypeValue = jQuery($grid.find('[data-validationname="InventoryTypeValidation"] input')).val();
        var CategoryTypeId = jQuery($grid.find('[data-validationname="SalesCategoryValidation"] input')).val();

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
var SalesInventoryController = new SalesInventory();