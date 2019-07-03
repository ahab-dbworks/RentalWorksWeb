class RentalInventory extends InventoryBase {
    Module: string = 'RentalInventory';
    apiurl: string = 'api/v1/rentalinventory';
    caption: string = Constants.Modules.Home.RentalInventory.caption;
    nav: string = Constants.Modules.Home.RentalInventory.nav;
    id: string = Constants.Modules.Home.RentalInventory.id;
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

        const $calendar = $form.find('.calendar');
        const $realscheduler = $form.find('.realscheduler');
        $form.on('change', '.warehousefilter', e => {
            FwScheduler.refresh($calendar);
            FwSchedulerDetailed.refresh($realscheduler);
        });
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const maxPageSize = 20;
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        // ----------
        const $inventoryLocationTaxGrid: any = $form.find('div[data-grid="InventoryLocationTaxGrid"]');
        const $inventoryLocationTaxGridControl: any = FwBrowse.loadGridFromTemplate('InventoryLocationTaxGrid');
        $inventoryLocationTaxGrid.empty().append($inventoryLocationTaxGridControl);
        $inventoryLocationTaxGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryLocationTaxGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryLocationTaxGridControl);
        FwBrowse.renderRuntimeHtml($inventoryLocationTaxGridControl);
        // ----------
        const $rentalInventoryWarehouseGrid = $form.find('div[data-grid="RentalInventoryWarehouseGrid"]');
        const $rentalInventoryWarehouseGridControl = FwBrowse.loadGridFromTemplate('RentalInventoryWarehouseGrid');
        $rentalInventoryWarehouseGrid.empty().append($rentalInventoryWarehouseGridControl);
        $rentalInventoryWarehouseGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
            request.miscfields = {
                UserWarehouseId: warehouse.warehouseid
            };
            request.pagesize = 100;  //justin 04/01/2019 #359 show all active warehouses here
        });
        $rentalInventoryWarehouseGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($rentalInventoryWarehouseGridControl);
        FwBrowse.renderRuntimeHtml($rentalInventoryWarehouseGridControl);
        // ----------
        const containerWarehouseGrid = $form.find('div[data-grid="ContainerWarehouseGrid"]');
        const containerWarehouseGridControl = FwBrowse.loadGridFromTemplate('ContainerWarehouseGrid');
        containerWarehouseGrid.empty().append(containerWarehouseGridControl);
        containerWarehouseGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        containerWarehouseGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init(containerWarehouseGridControl);
        FwBrowse.renderRuntimeHtml(containerWarehouseGridControl);
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
        $inventoryConsignmentGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
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
        $inventoryCompleteKitGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryCompleteKitGridControl);
        FwBrowse.renderRuntimeHtml($inventoryCompleteKitGridControl);
        // ----------
        const $inventorySubstituteGrid = $form.find('div[data-grid="InventorySubstituteGrid"]');
        const $inventorySubstituteGridControl = FwBrowse.loadGridFromTemplate('InventorySubstituteGrid');
        $inventorySubstituteGrid.empty().append($inventorySubstituteGridControl);
        $inventorySubstituteGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(),
                WarehouseId: warehouse.warehouseid
            };
        });
        $inventorySubstituteGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventorySubstituteGridControl);
        FwBrowse.renderRuntimeHtml($inventorySubstituteGridControl);
        // ----------
        const $inventoryCompatibilityGrid = $form.find('div[data-grid="InventoryCompatibilityGrid"]');
        const $inventoryCompatibilityGridControl = FwBrowse.loadGridFromTemplate('InventoryCompatibilityGrid');
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
        $inventoryQcGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
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
        const $inventoryContainerItemGrid = $form.find('div[data-grid="InventoryContainerItemGrid"]');
        const $inventoryContainerItemGridControl = FwBrowse.loadGridFromTemplate('InventoryContainerItemGrid');
        $inventoryContainerItemGrid.empty().append($inventoryContainerItemGridControl);
        $inventoryContainerItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                PackageId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
            request.pagesize = maxPageSize;
        });
        $inventoryContainerItemGridControl.data('beforesave', function (request) {
            request.PackageId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
            request.ContainerId = $form.find('div.fwformfield[data-datafield="ContainerId"] input').val();
        });
        FwBrowse.init($inventoryContainerItemGridControl);
        FwBrowse.renderRuntimeHtml($inventoryContainerItemGridControl);
        // ----------
        const $inventoryCompleteGrid = $form.find('div[data-grid="InventoryCompleteGrid"]');
        const $inventoryCompleteGridControl = FwBrowse.loadGridFromTemplate('InventoryCompleteGrid');
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
        const $inventoryWarehouseStagingGrid = $form.find('div[data-grid="InventoryWarehouseStagingGrid"]');
        const $inventoryWarehouseStagingGridControl = FwBrowse.loadGridFromTemplate('InventoryWarehouseStagingGrid');
        $inventoryWarehouseStagingGrid.empty().append($inventoryWarehouseStagingGridControl);
        $inventoryWarehouseStagingGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryWarehouseStagingGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryWarehouseStagingGridControl);
        FwBrowse.renderRuntimeHtml($inventoryWarehouseStagingGridControl);
        // ----------
        const $inventoryKitGrid = $form.find('div[data-grid="InventoryKitGrid"]');
        const $inventoryKitGridControl = FwBrowse.loadGridFromTemplate('InventoryKitGrid');
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
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        super.afterLoad($form);

        var $itemLocationTaxGrid: any;
        var $rentalInventoryWarehouseGrid: any;
        var $inventoryAvailabilityGrid: any;
        var $inventoryConsignmentGrid: any;
        var $inventoryCompleteKitGrid: any;
        var $inventorySubstituteGrid: any;
        var $inventoryCompatibilityGrid: any;
        var $inventoryQcGrid: any;
        var $inventoryAttributeValueGrid: any;
        var $inventoryVendorGrid: any;
        var $inventoryPrepGrid: any;
        var $inventoryContainerItemGrid: any;
        var $inventoryCompleteGrid: any;
        var $inventoryWarehouseStagingGrid: any;
        var $inventoryKitGrid: any;
        var $wardrobeInventoryColorGrid: any;
        var $wardrobeInventoryMaterialGrid: any;
        let $containerWarehouseGrid: any;

        this.iCodeMask($form);

        $containerWarehouseGrid = $form.find('[data-name="ContainerWarehouseGrid"]');
        $rentalInventoryWarehouseGrid = $form.find('[data-name="RentalInventoryWarehouseGrid"]');
        $itemLocationTaxGrid = $form.find('[data-name="ItemLocationTaxGrid"]');
        //FwBrowse.search($itemLocationTaxGrid);
        $inventoryAvailabilityGrid = $form.find('[data-name="InventoryAvailabilityGrid"]');
        //FwBrowse.search($inventoryAvailabilityGrid);
        $inventoryConsignmentGrid = $form.find('[data-name="InventoryConsignmentGrid"]');
        //FwBrowse.search($inventoryConsignmentGrid);
        $inventoryCompleteKitGrid = $form.find('[data-name="InventoryCompleteKitGrid"]');
        //FwBrowse.search($inventoryCompleteKitGrid);
        $inventorySubstituteGrid = $form.find('[data-name="InventorySubstituteGrid"]');
        //FwBrowse.search($inventorySubstituteGrid);
        $inventoryCompatibilityGrid = $form.find('[data-name="InventoryCompatibilityGrid"]');
        //FwBrowse.search($inventoryCompatibilityGrid);
        $inventoryQcGrid = $form.find('[data-name="InventoryQcGrid"]');
        //FwBrowse.search($inventoryQcGrid);
        $inventoryAttributeValueGrid = $form.find('[data-name="InventoryAttributeValueGrid"]');
        //FwBrowse.search($inventoryAttributeValueGrid);
        $inventoryVendorGrid = $form.find('[data-name="InventoryVendorGrid"]');
        //FwBrowse.search($inventoryVendorGrid);
        $inventoryPrepGrid = $form.find('[data-name="InventoryPrepGrid"]');
        //FwBrowse.search($inventoryPrepGrid);
        $inventoryContainerItemGrid = $form.find('[data-name="InventoryContainerItemGrid"]');
        //FwBrowse.search($inventoryContainerGrid);
        $inventoryCompleteGrid = $form.find('[data-name="InventoryCompleteGrid"]');
        //FwBrowse.search($inventoryCompleteGrid);
        $inventoryWarehouseStagingGrid = $form.find('[data-name="InventoryWarehouseStagingGrid"]');
        //FwBrowse.search($inventoryWarehouseStagingGrid);
        $inventoryKitGrid = $form.find('[data-name="InventoryKitGrid"]');
        //FwBrowse.search($inventoryKitGrid);
        $wardrobeInventoryColorGrid = $form.find('[data-name="WardrobeInventoryColorGrid"]');
        //FwBrowse.search($wardrobeInventoryColorGrid);
        $wardrobeInventoryMaterialGrid = $form.find('[data-name="WardrobeInventoryMaterialGrid"]');
        //FwBrowse.search($wardrobeInventoryMaterialGrid);

        this.afterLoadSetClassification($form);
        this.addAssetTab($form);

        let classificationType = FwFormField.getValueByDataField($form, 'Classification');
        //Change the grid on primary to tab when classification is container
        if (classificationType == 'N') {
            $form.find('[data-grid="RentalInventoryWarehouseGrid"]').hide();
            $form.find('[data-grid="ContainerWarehouseGrid"]').show();
            FwBrowse.search($containerWarehouseGrid);

            //Open Container module as submodule
            const $containerBrowse = this.openContainerBrowse($form);
            $form.find('.containerassetstabpage').html($containerBrowse);
            $form.find('.containerassetstab').show();

            //Show settings tab
            $form.find('.settingstab').show();
        } else {
            FwBrowse.search($rentalInventoryWarehouseGrid);
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

        //enable version and effective date fields upon loading if "Enable Software Tracking" is checked
        if (FwFormField.getValueByDataField($form, 'TrackSoftware')) FwFormField.enable($form.find('.track-software'));
    };
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
    addAssetTab($form: any): void {
        const classificationValue = FwFormField.getValueByDataField($form, 'Classification');
        const trackedByValue = FwFormField.getValueByDataField($form, 'TrackedBy');

        if (classificationValue === 'I' || classificationValue === 'A') {
            if (trackedByValue !== 'QUANTITY') {
                $form.find('.tab.asset').show();
                const $submoduleAssetBrowse = this.openAssetBrowse($form);
                $form.find('.tabpage.asset').html($submoduleAssetBrowse);
                $submoduleAssetBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
                $submoduleAssetBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
                    var $assetForm, controller, $browse, assetFormData: any = {};
                    $browse = jQuery(this).closest('.fwbrowse');
                    controller = $browse.attr('data-controller');
                    assetFormData.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                    if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
                    if (typeof window[controller]['openForm'] !== 'function') throw 'Missing javascript function: ' + controller + '.openForm';
                    $assetForm = window[controller]['openForm']('NEW', assetFormData);
                    FwModule.openSubModuleTab($browse, $assetForm);
                });
            }
        }
    };
    //----------------------------------------------------------------------------------------------
    openAssetBrowse($form: any) {
        const $browse = AssetController.openBrowse();
        const inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');

        $browse.data('ondatabind', request => {
            request.activeviewfields = AssetController.ActiveViewFields;
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
        const CategoryTypeId = jQuery($grid.find('[data-validationname="RentalCategoryValidation"] input')).val();

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
        };
    };
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
        let wildcardMask = '[' + inputmasksplit.join('') + ']';  //justin 04/16/2019 optional digits are converted to blanks instead of '_' on blur
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
};
//----------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents[Constants.Modules.Home.RentalInventory.form.menuItems.CreateComplete.id] = (e: JQuery.ClickEvent) => {
    try {
        const $form = jQuery(e.currentTarget).closest('.fwform');
        const inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
        FwAppData.apiMethod(true, 'POST', `api/v1/inventorycompletekit/createcomplete/${inventoryId}`, null, FwServices.defaultTimeout,
            response => {
                const uniqueIds: any = {};
                uniqueIds.InventoryId = response.PackageId;
                const $completeForm = RentalInventoryController.loadForm(uniqueIds);
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
var RentalInventoryController = new RentalInventory();