class InventoryItem {
    Module: string = 'InventoryItem';
    apiurl: string = 'api/v1/rentalinventory';
    caption: string = 'Inventory Item';
    nav: string = 'module/inventoryitem';
    id: string = '803A2616-4DB6-4BAC-8845-ECAD34C369A8';
    ActiveView: string = 'ALL';

    getModuleScreen() {
        var me: InventoryItem = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $browse: JQuery = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, me.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    };

    //----------------------------------------------------------------------------------------------
    openBrowse() {
        var self = this;
        //var $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        //var $form;
        //$form = FwModule.loadFormFromTemplate(this.Module);
        let $form = jQuery(this.getFormTemplate());

        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');
            const today = FwFunc.getDate();

            FwFormField.setValueByDataField($form, 'ActiveDate', today);

            // Disable / Enable Inactive Date
            $form.find('[data-datafield="Inactive"] .fwformfield-value').on('change', function () {
                var $this = jQuery(this);
                if ($this.prop('checked') === true) {
                    FwFormField.enable($form.find('div[data-datafield="InactiveDate"]'));
                    FwFormField.setValueByDataField($form, 'InactiveDate', today);
                }
                else {
                    FwFormField.disable($form.find('div[data-datafield="InactiveDate"]'));
                    FwFormField.setValueByDataField($form, 'InactiveDate', "");
                }
            });
        }

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form: JQuery = this.openForm('EDIT');

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(uniqueids.InventoryId);

        FwModule.loadForm(this.Module, $form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        var $itemLocationTaxGrid: any;
        var $itemLocationTaxGridControl: any;
        var $rentalInventoryWarehouseGrid: any;
        var $rentalInventoryWarehouseGridControl: any;
        var $inventoryAvailabilityGrid: any;
        var $inventoryAvailabilityGridControl: any;
        var $inventoryConsignmentGrid: any;
        var $inventoryConsignmentGridControl: any;
        var $inventoryCompleteKitGrid: any;
        var $inventoryCompleteKitGridControl: any;
        var $inventorySubstituteGrid: any;
        var $inventorySubstituteGridControl: any;
        var $inventoryCompatibilityGrid: any;
        var $inventoryCompatibilityGridControl: any;
        var $inventoryQcGrid: any;
        var $inventoryQcGridControl: any;
        var $inventoryAttributeValueGrid: any;
        var $inventoryAttributeValueGridControl: any;
        var $inventoryVendorGrid: any;
        var $inventoryVendorGridControl: any;
        var $inventoryPrepGrid: any;
        var $inventoryPrepGridControl: any;
        var $inventoryContainerGrid: any;
        var $inventoryContainerGridControl: any;
        var $inventoryCompleteGrid: any;
        var $inventoryCompleteGridControl: any;
        var $inventoryWarehouseStagingGrid: any;
        var $inventoryWarehouseStagingGridControl: any;
        var $inventoryKitGrid: any;
        var $inventoryKitGridControl: any;
        var $wardrobeInventoryColorGrid: any;
        var $wardrobeInventoryColorGridControl: any;
        var $wardrobeInventoryMaterialGrid: any;
        var $wardrobeInventoryMaterialGridControl: any;
        let max = 9999;

        var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        // load AttributeValue Grid
        $itemLocationTaxGrid = $form.find('div[data-grid="ItemLocationTaxGrid"]');
        $itemLocationTaxGridControl = jQuery(jQuery('#tmpl-grids-ItemLocationTaxGridBrowse').html());
        $itemLocationTaxGrid.empty().append($itemLocationTaxGridControl);
        $itemLocationTaxGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $itemLocationTaxGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($itemLocationTaxGridControl);
        FwBrowse.renderRuntimeHtml($itemLocationTaxGridControl);
        // ----------
        $rentalInventoryWarehouseGrid = $form.find('div[data-grid="RentalInventoryWarehouseGrid"]');
        $rentalInventoryWarehouseGridControl = jQuery(jQuery('#tmpl-grids-RentalInventoryWarehouseGridBrowse').html());
        $rentalInventoryWarehouseGrid.empty().append($rentalInventoryWarehouseGridControl);
        $rentalInventoryWarehouseGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $rentalInventoryWarehouseGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($rentalInventoryWarehouseGridControl);
        FwBrowse.renderRuntimeHtml($rentalInventoryWarehouseGridControl);
        // ----------
        let containerWarehouseGrid: any;
        let containerWarehouseGridControl: any;
        containerWarehouseGrid = $form.find('div[data-grid="ContainerWarehouseGrid"]');
        containerWarehouseGridControl = jQuery(jQuery('#tmpl-grids-ContainerWarehouseGridBrowse').html());
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
        // ----------
        $inventoryConsignmentGrid = $form.find('div[data-grid="InventoryConsignmentGrid"]');
        $inventoryConsignmentGridControl = jQuery(jQuery('#tmpl-grids-InventoryConsignmentGridBrowse').html());
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
        $inventoryCompleteKitGrid = $form.find('div[data-grid="InventoryCompleteKitGrid"]');
        $inventoryCompleteKitGridControl = jQuery(jQuery('#tmpl-grids-InventoryCompleteKitGridBrowse').html());
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
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventorySubstituteGridControl);
        FwBrowse.renderRuntimeHtml($inventorySubstituteGridControl);
        // ----------
        $inventoryCompatibilityGrid = $form.find('div[data-grid="InventoryCompatibilityGrid"]');
        $inventoryCompatibilityGridControl = jQuery(jQuery('#tmpl-grids-InventoryCompatibilityGridBrowse').html());
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
        $inventoryQcGrid = $form.find('div[data-grid="InventoryQcGrid"]');
        $inventoryQcGridControl = jQuery(jQuery('#tmpl-grids-InventoryQcGridBrowse').html());
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
        // ----------
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
        // ----------
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
        // ----------
        $inventoryContainerGrid = $form.find('div[data-grid="InventoryContainerGrid"]');
        $inventoryContainerGridControl = jQuery(jQuery('#tmpl-grids-InventoryContainerGridBrowse').html());
        $inventoryContainerGrid.empty().append($inventoryContainerGridControl);
        $inventoryContainerGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                PackageId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
            request.pagesize = max;
        });
        $inventoryContainerGridControl.data('beforesave', function (request) {
            request.PackageId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
            request.ContainerId = $form.find('div.fwformfield[data-datafield="ContainerId"] input').val();
        });
        FwBrowse.init($inventoryContainerGridControl);
        FwBrowse.renderRuntimeHtml($inventoryContainerGridControl);
        // ----------
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
            request.PackageId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        $inventoryCompleteGridControl.data('isfieldeditable', function ($field, dt, rowIndex) {
            var primaryRowIndex;
            if (primaryRowIndex === undefined) {
                let orderByIndex = dt.ColumnIndex.OrderBy;
                let inventoryIdIndex = dt.ColumnIndex.InventoryId
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
        $inventoryWarehouseStagingGrid = $form.find('div[data-grid="InventoryWarehouseStagingGrid"]');
        $inventoryWarehouseStagingGridControl = jQuery(jQuery('#tmpl-grids-InventoryWarehouseStagingGridBrowse').html());
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
        $inventoryKitGrid = $form.find('div[data-grid="InventoryKitGrid"]');
        $inventoryKitGridControl = jQuery(jQuery('#tmpl-grids-InventoryKitGridBrowse').html());
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
                let orderByIndex = dt.ColumnIndex.OrderBy;
                let inventoryIdIndex = dt.ColumnIndex.InventoryId
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
        // ----------
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
        // ----------
    };

    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        this.afterLoad($form); // changed super.afterLoad($form); to this.afterLoad($form);

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
        var $inventoryContainerGrid: any;
        var $inventoryCompleteGrid: any;
        var $inventoryWarehouseStagingGrid: any;
        var $inventoryKitGrid: any;
        var $wardrobeInventoryColorGrid: any;
        var $wardrobeInventoryMaterialGrid: any;
        let $containerWarehouseGrid: any;

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
        $inventoryContainerGrid = $form.find('[data-name="InventoryContainerGrid"]');
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

        //this.afterLoadSetClassification($form);       tg 3/5/2019
        

        this.addAssetTab($form);

        let classificationType = FwFormField.getValueByDataField($form, 'Classification');
        //Change the grid on primary to tab when classification is container
        if (classificationType == 'N') {
            $form.find('[data-grid="RentalInventoryWarehouseGrid"]').hide();
            $form.find('[data-grid="ContainerWarehouseGrid"]').show();
            FwBrowse.search($containerWarehouseGrid);

            //Open Container module as submodule
            let $containerBrowse;
            $containerBrowse = this.openContainerBrowse($form);
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

    };

    //----------------------------------------------------------------------------------------------
    openContainerBrowse($form: any) {
        let $browse, containerId;
        $browse = ContainerController.openBrowse();
        containerId = FwFormField.getValueByDataField($form, 'ContainerId');

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
        let $submoduleAssetBrowse, classificationValue, trackedByValue;
        classificationValue = FwFormField.getValueByDataField($form, 'Classification');
        trackedByValue = FwFormField.getValueByDataField($form, 'TrackedBy');

        if (classificationValue === 'I' || classificationValue === 'A') {
            if (trackedByValue !== 'QUANTITY') {
                $form.find('.tab.asset').show();
                $submoduleAssetBrowse = this.openAssetBrowse($form);
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
        let $browse, inventoryId;
        $browse = AssetController.openBrowse();
        inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');

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

    //--------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
        <div data-name="InventoryItem" data-control="FwBrowse" data-type="Browse" id="InventoryBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="InventoryItemController" data-hasinactive="true">
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="InventoryId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-datafield="Inactive" data-browsedatatype="text" data-visible="false"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="Description" data-datafield="Description" data-browsedatatype="text" data-cellcolor="ClassificationColor" data-sort="asc"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="I-Code" data-datafield="ICode" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="Inventory Type" data-datafield="InventoryType" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="Category" data-datafield="Category" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="Sub-Category" data-datafield="SubCategory" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="Manufacturer Part Number" data-datafield="ManufacturerPartNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="Classification" data-datafield="ClassificationDescription" data-cellcolor="ClassificationColor" data-browsedatatype="text" data-sort="asc"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="Tracked By" data-datafield="TrackedBy" data-browsedatatype="text" data-sort="off"></div>
          </div>
        </div>
        `;
    }

    //--------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="rentalinventoryform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Inventory Item" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="InventoryItemController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="InventoryId"></div>
          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="SubCategoryCount" style="display:none;"></div>
          <div id="rentalinventoryform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="rentalitemtab" class="tab" data-tabpageid="rentalitemtabpage" data-caption="Inventory"></div>
              <div data-type="tab" id="containerpackinglisttab" class="containertab tab" data-tabpageid="containerpackinglisttabpage" data-caption="Container" style="display:none;"></div>
              <div data-type="tab" id="containerassetstab" class="containerassetstab tab" data-tabpageid="containerassetstabpage" data-caption="Container Assets" style="display:none;"></div>
              <div data-type="tab" id="completetab" class="completetab tab" data-tabpageid="completetabpage" data-caption="Complete" style="display:none;"></div>
              <div data-type="tab" id="kittab" class="kittab tab" data-tabpageid="kittabpage" data-caption="Kit" style="display:none;"></div>

              <div data-type="tab" id="availabilitycalendartab" class="tab" data-tabpageid="availabilitycalendartabpage" data-caption="Availability Calendar"></div>

                    <div data-type="tab" id="settingstab" class="settingstab tab" data-tabpageid="settingstabpage" data-caption="Settings" style="display:none;"></div>
                    <div data-type="tab" id="assettab" class="tab asset submodule" data-tabpageid="assettabpage" data-caption="Asset" style="display:none;"></div>
              <div data-type="tab" id="consignmenttab" class="tab" data-tabpageid="consignmenttabpage" data-caption="Consignment"></div>
                    <div data-type="tab" id="wardrobetab" class="wardrobetab tab" data-tabpageid="wardrobetabpage" data-caption="Wardrobe" style="display:none;"></div>
              <div data-type="tab" id="availabilitytab" class="tab" data-tabpageid="availabilitytabpage" data-caption="Availability"></div>
              <div data-type="tab" id="completeskitstab" class="completeskitstab tab" data-tabpageid="completeskitstabpage" data-caption="Completes / Kits"></div>
              <div data-type="tab" id="substitutestab" class="tab" data-tabpageid="substitutestabpage" data-caption="Substitutes"></div>
              <div data-type="tab" id="compatibilitytab" class="tab" data-tabpageid="compatibilitytabpage" data-caption="Compatibility"></div>
              <div data-type="tab" id="subrentalvendortab" class="tab" data-tabpageid="subrentalvendortabpage" data-caption="Sub-Rental Vendor"></div>
              <div data-type="tab" id="weightsdimensionstab" class="tab" data-tabpageid="weightsdimensionstabpage" data-caption="Weights &amp; Dimensions"></div>
              <div data-type="tab" id="qctab" class="tab" data-tabpageid="qctabpage" data-caption="QC"></div>
              <div data-type="tab" id="attributesusagetab" class="tab" data-tabpageid="attributesusagetabpage" data-caption="Attribute / Usage"></div>
              <div data-type="tab" id="repairordertab" class="tab submodule repairTab" data-tabpageid="repairordertabpage" data-caption="Repair Orders"></div>
              <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
            </div>
            <div class="tabpages">
              <!-- INVENTORY TAB -->
              <div data-type="tabpage" id="rentalitemtabpage" class="tabpage" data-tabid="rentalitemtab">
                <div class="flexpage">
                  <!-- Rental Inventory -->
                  <div class="wideflexrow">
                    <div class="flexcolumn" style="flex:1 1 500px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Item">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICode" data-required="true" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-required="true" style="flex:1 1 375px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="ContainerId" style="display:none;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType" data-validationname="InventoryTypeValidation" data-formbeforevalidate="beforeValidate" data-validationpeek="true" data-required="true" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Category" data-datafield="CategoryId" data-displayfield="Category" data-validationname="RentalCategoryValidation" data-required="true" data-formbeforevalidate="beforeValidate" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield subcategory" data-caption="Sub-Category" data-datafield="SubCategoryId" data-displayfield="SubCategory" data-validationname="SubCategoryValidation" data-validationpeek="true" data-formbeforevalidate="beforeValidate" style="flex:1 1 200px;"></div>
                        </div>
                        <div class="flexrow">
                          
                          <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield rank" data-caption="Classification" data-datafield="Rank" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield rank" data-caption="Tracked By" data-datafield="Rank" style="flex:1 1 150px;"></div>
                          
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Unit" data-datafield="UnitId" data-displayfield="Unit" data-validationname="UnitValidation" data-required="true" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Rank" data-datafield="Rank" data-displayfield="Rank" data-validationname="RankValidation" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Manufacturer Part Number" data-datafield="ManufacturerPartNumber" style="flex:2 1 150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="" data-datafield="InventoryTypeIsWardrobe" style="display:none;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Inactive" data-datafield="Inactive" style="flex:0 1 75px;margin-top:10px;margin-left:15px;"></div>
                        </div>
                      </div>
                      <div class="flexrow">
                        <div class="flexcolumn" style="flex:0 1 250px;">
                          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Classification">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield class-tracked-radio" data-caption="" data-datafield="Classification" data-enabled="false" style="flex:0 1 200px;">
                                <div data-value="I" class="itemradio" data-caption="Item"></div>
                                <div data-value="A" class="accessoryradio" data-caption="Accessory"></div>
                                <div data-value="C" class="completeradio" data-caption="Complete"></div>
                                <div data-value="K" class="kitradio" data-caption="Kit"></div>
                                <div data-value="N" class="containerradio" data-caption="Container"></div>
                                <div data-value="M" class="miscradio" data-caption="Misc."></div>
                              </div>
                            </div>
                          </div>
                        </div>
                        <div class="flexcolumn tracked-by-column" style="flex:0 1 250px;">
                          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tracked By">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield class-tracked-radio" data-caption="" data-datafield="TrackedBy" style="flex:0 1 200px;">
                                <div data-value="BARCODE" data-caption="Bar Code"></div>
                                <div data-value="SERIALNO" data-caption="Serial No."></div>
                                <div data-value="RFID" data-caption="RFID"></div>
                                <div data-value="QUANTITY" data-caption="Quantity"></div>
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <!-- Image -->
                    <div class="flexcolumn" style="flex:1 1 350px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Image">
                        <div class="flexrow">
                          <div data-control="FwAppImage" data-type="" class="fwcontrol fwappimage contactphoto" data-caption="" data-uniqueid1field="InventoryId" data-description="" data-rectype="F" style="overflow:hidden;"></div>
                        </div>
                      </div>
                    </div>
                  </div>

                  <!-- Warehouse grid -->
                  <div class="wideflexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Warehouse Inventory">
                      <div data-control="FwGrid" data-grid="RentalInventoryWarehouseGrid" data-securitycaption="Rental Inventory Warehouse"></div>
                      <div data-control="FwGrid" data-grid="ContainerWarehouseGrid" data-securitycaption="Container Warehouse" style="display:none;"></div>
                    </div>
                  </div>
                </div>
              </div>











              <!-- Container Assets tab -->
              <div data-type="tabpage" id="containerassetstabpage" class="containerassetstabpage tabpage" data-tabid="containerassetstab">
              </div>


              <!-- Repair Order tab -->
              <div data-type="tabpage" id="repairordertabpage" class="tabpage repairOrderSubModule" data-tabid="repairordertab">
              </div>



              <!--- SETTINGS TAB -->
              <div data-type="tabpage" id="settingstabpage" class="tabpage" data-tabid="settingstab">
                <div class="flexpage">
                  <div class="flexrow" style="max-width:650px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Scannable Item">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Scannable Item" data-datafield="ContainerScannableInventoryId" data-displayfield="ContainerScannableICode" data-validationname="RentalInventoryValidation" data-enabled="false" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="ContainerScannableDescription" data-enabled="false" style="flex:1 1 400px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="max-width:700px">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Auto-Rebuild">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Automatically rebuild Container when Scannable Item is checked-in from an Order" data-datafield="AutomaticallyRebuildContainerAtCheckIn"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Automatically rebuild Container when Scannable Item is transferred-in from a Transfer Order" data-datafield="AutomaticallyRebuildContainerAtTransferIn"></div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="flexcolumn">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Availability">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Exclude Contained Items from Availability" data-datafield="ExcludeContainedItemsFromAvailability"></div>
                      </div>
                    </div>
                    <div class="flexcolumn">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Use Container No." data-datafield="UseContainerNumber"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow" style="max-width:550px">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="When more items are In Container than requested on Order">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="ContainerStagingRule">
                        <div data-value="AUTOADD" data-caption="Automatically add extra items to the Order"></div>
                        <div data-value="WARN" data-caption="Warn the user about the extra items, but do not add them to the Order"></div>
                        <div data-value="NOWARN" data-caption="Do not warn, and do not add them to the Order"></div>
                        <div data-value="ERROR" data-caption="Do not stage the Container"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow" style="max-width:550px">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="When Filling a Container">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="ContainerPackingListBehavior">
                        <div data-value="AUTOPRINT" data-caption="Automatically print a Container Packing List"></div>
                        <div data-value="PROMPT" data-caption="Prompt user to print a Packing List"></div>
                        <div data-value="NONE" data-caption="Do nothing"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>







              <!-- Order History tab -->
              <div data-type="tabpage" id="orderhistorytabpage" class="tabpage" data-tabid="orderhistorytab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order History">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="orderhistory" data-securitycaption="" style="min-height:350px;border:1px solid silver;">
                          <p>Order History grid</p>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="formcolumn" style="width:275px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Grid">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Mode" data-datafield="vendor.type" style="width:250px;">
                            <div data-value="Summary" data-caption="Summary"></div>
                            <div data-value="Detail" data-caption="Detail"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:200px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All Warehouses" data-datafield="vendor.modifiedby" style="width:175px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="wardrobetabpage" class="tabpage" data-tabid="wardrobetab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Wardrobe">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Pattern" data-datafield="PatternId" data-displayfield="Pattern" data-validationname="WardrobePatternValidation" style="float:left;width:300px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Period" data-datafield="PeriodId" data-displayfield="Period" data-validationname="WardrobePeriodValidation" style="float:left;width:300px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Gender" data-datafield="GenderId" data-displayfield="Gender" data-validationname="WardrobeGenderValidation" style="float:left;width:300px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Cleaning Fee" data-datafield="CleaningFeeAmount" style="float:left;width:175px;"></div>
                      </div>
                      <div class="formrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Label" data-datafield="LabelId" data-displayfield="Label" data-validationname="WardrobeLabelValidation" style="float:left;width:300px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Size" data-datafield="WardrobeSize" style="float:left;width:200px;"></div>
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Piece Count" data-datafield="WardrobePieceCount" style="float:left;width:100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Dyed" data-datafield="Dyed" style="float:left;width:150px;margin-top:12px;"></div>
                      </div>
                      <div class="formrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Care" data-datafield="WardrobeCareId" data-displayfield="WardrobeCare" data-validationname="WardrobeCareValidation" style="float:left;width:300px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Source" data-datafield="WardrobeSourceId" data-displayfield="WardrobeSource" data-validationname="WardrobeSourceValidation" style="float:left;width:300px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="formcolumn" style="width:450px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Detailed Description">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="" data-datafield="WardrobeDetailedDescription"></div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:450px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Color">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwGrid" data-grid="WardrobeInventoryColorGrid" data-securitycaption="Wardrobe Inventory Color"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:450px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Material">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwGrid" data-grid="WardrobeInventoryMaterialGrid" data-securitycaption="Wardrobe Inventory Material"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>




    











              <!-- Transfer History tab -->
              <div data-type="tabpage" id="transferhistorytabpage" class="tabpage" data-tabid="transferhistorytab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Transfer History">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="orderhistory" data-securitycaption="" style="min-height:350px;border:1px solid silver;">
                          <p>Transfer History grid</p>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="formcolumn" style="width:275px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Grid">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Mode" data-datafield="vendor.type" style="width:250px;">
                            <div data-value="Summary" data-caption="Summary"></div>
                            <div data-value="Detail" data-caption="Detail"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:200px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All Warehouses" data-datafield="vendor.modifiedby" style="width:175px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- AKAs tab -->
              <div data-type="tabpage" id="akastabpage" class="tabpage" data-tabid="akastab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="AKAs" style="width:750px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="CustomerNotes" data-securitycaption="" style="min-height:350px;border:1px solid silver;">
                          <p>AKAs grid</p>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="width:150px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All" data-datafield="Inactive" style="width:125px;margin-top:0px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- Character tab -->
              <div data-type="tabpage" id="charactertabpage" class="tabpage" data-tabid="charactertab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Character" style="width:750px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="CustomerNotes" data-securitycaption="" style="min-height:350px;border:1px solid silver;">
                          <p>Character grid</p>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="width:415px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Filter by Deal" data-datafield="department" style="float:left;width:115px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="" data-datafield="vendor.primarycontactname" style="float:left;width:275px;"></div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All" data-datafield="Inactive" style="width:175px;margin-top:0px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>



              <!-- Inventory tab -->
              <div data-type="tabpage" id="inventorytabpage" class="tabpage" data-tabid="inventorytab">
                <div class="formpage">
                  <div class="formrow" style="width:850px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="" data-securitycaption="" style="min-height:200px;border:1px solid silver;">
                          <p style="font-size:10pt;">Warehouse | Total | Consigned | In | In Container | QC Required | Staged | Out | In Repair | On PO | Aisle | Shelf</p>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All Warehouses" data-datafield="Inactive" style="float:left;width:175px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow" style="width:875px;">
                    <div class="formcolumn" style="width:550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="width:550px;">
                        <div class="formcolumn" style="width:200px;">
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Fixed Asset" data-datafield="" style="width:175px;margin-top:0px;"></div>
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Hazardous Materials" data-datafield="" style="width:175px;margin-top:0px;"></div>
                          </div>
                        </div>
                        <div class="formcolumn" style="width:325px;">
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Automatically Swap Items from the Same Deal" data-datafield="" style="width:300px;margin-top:0px;"></div>
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Exclude from .NET Return on Asset Report" data-datafield="" style="width:300px;margin-top:0px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:300px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Original Show">
                        <div class="formcolumn" style="width:200px;">
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Description" data-datafield="" style="width:275px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ASSET TAB -->
              <div data-type="tabpage" id="assettabpage" class="tabpage asset submodule" data-tabid="assettab"></div>
              <!-- Items tab -->
              <div data-type="tabpage" id="itemstabpage" class="tabpage" data-tabid="itemstab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Bar Coded Items">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="CustomerNotes" data-securitycaption="Customer Notes" style="min-height:350px;border:1px solid silver;">
                          <p>Bar Coded Items grid</p>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="width:450px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Filter by Department" data-datafield="department" style="float:left;width:155px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="" data-datafield="vendor.primarycontactname" style="float:left;width:275px;"></div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All" data-datafield="Inactive" style="float:left;width:155px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All Warehouses" data-datafield="Inactive" style="float:left;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- Availability Calendar tab -->
              <div data-type="tabpage" id="availabilitycalendartabpage" class="tabpage" data-tabid="availabilitycalendartab">
                <div class="formpage" style="width:auto;">
                  <div data-control="FwScheduler" class="fwcontrol fwscheduler calendar"></div>
<!--                  
<div data-control="FwSchedulerDetailed" class="fwcontrol fwscheduler realscheduler"></div>
-->
                </div>
              </div>
              <!-- Out Items tab -->
              <div data-type="tabpage" id="outitemstabpage" class="tabpage" data-tabid="outitemstab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Out Items">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="CustomerNotes" data-securitycaption="" style="min-height:350px;border:1px solid silver;">
                          <p>Out Items grid</p>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="formcolumn" style="width:450px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                        <div class="formrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Filter by Warehouse" data-datafield="department" style="float:left;width:155px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="" data-datafield="vendor.primarycontactname" style="float:left;width:275px;"></div>
                        </div>
                        <div class="formrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All Warehouses" data-datafield="Inactive" style="float:left;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:700px;margin-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Active">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="In" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:85px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Staged" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:85px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="In Repair" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:85px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Out" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:85px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="In Transit" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:85px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="On Truck" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:85px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="In Container" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:85px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Total" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:85px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- Purchase History tab -->
              <div data-type="tabpage" id="purchasehistorytabpage" class="tabpage" data-tabid="purchasehistorytab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Purchase History">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="CustomerNotes" data-securitycaption="Customer Notes" style="min-height:350px;border:1px solid silver;">
                          <p>Purchase History grid</p>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="formcolumn" style="width:275px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Grid">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Mode" data-datafield="vendor.type" style="width:250px;">
                            <div data-value="Summary" data-caption="Summary"></div>
                            <div data-value="Detail" data-caption="Detail"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:200px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All Warehouses" data-datafield="vendor.modifiedby" style="width:175px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:275px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Cost Total">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Amount" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:125px;"></div>
                          <button class="button theme calculate" style="float:left;margin-top:15px;margin-left:5px;line-height:12px;width:115px;font-size:10pt;">Calculate</button>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- Sub-Rental History tab -->
              <div data-type="tabpage" id="subrentalhistorytabpage" class="tabpage" data-tabid="subrentalhistorytab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sub-Rental History" style="width:750px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="" data-securitycaption="" style="min-height:350px;border:1px solid silver;">
                          <p>Sub-Rental History grid</p>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="width:200px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All Warehouses" data-datafield="Inactive" style="width:175px;margin-top:0px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- Repair History tab -->
              <div data-type="tabpage" id="repairhistorytabpage" class="tabpage" data-tabid="repairhistorytab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Repair History">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="CustomerNotes" data-securitycaption="Customer Notes" style="min-height:350px;border:1px solid silver;">
                          <p>Repair History grid</p>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="formcolumn" style="width:200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All Warehouses" data-datafield="vendor.modifiedby" style="width:175px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:275px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Total">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Amount" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:125px;"></div>
                          <button class="button theme calculate" style="float:left;margin-top:15px;margin-left:5px;line-height:12px;width:115px;font-size:10pt;">Calculate</button>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- Physical Inventory History tab -->
              <div data-type="tabpage" id="physicalinventoryhistorytabpage" class="tabpage" data-tabid="physicalinventoryhistorytab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Physical Inventory History" style="width:750px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="CustomerNotes" data-securitycaption="" style="min-height:350px;border:1px solid silver;">
                          <p>Physical Inventory History grid</p>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="width:450px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Filter by Warehouse" data-datafield="department" style="float:left;width:155px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="" data-datafield="vendor.primarycontactname" style="float:left;width:275px;"></div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All Warehouses" data-datafield="Inactive" style="width:175px;margin-top:0px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- Retired Items tab -->
              <div data-type="tabpage" id="retireditemstabpage" class="tabpage" data-tabid="retireditemstab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Retired Items">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="CustomerNotes" data-securitycaption="Customer Notes" style="min-height:350px;border:1px solid silver;">
                          <p>Retired Items grid</p>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="formcolumn" style="width:450px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                        <div class="formrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Filter by Warehouse" data-datafield="department" style="float:left;width:155px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="" data-datafield="vendor.primarycontactname" style="float:left;width:275px;"></div>
                        </div>
                        <div class="formrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All Warehouses" data-datafield="Inactive" style="float:left;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:700px;margin-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Retired">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sold" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:85px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Lost" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:85px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Traded" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:85px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Donated" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:85px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Stolen" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:85px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Inventory" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:85px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Other" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:85px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Total" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:85px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- Documents tab -->
              <div data-type="tabpage" id="documentstabpage" class="tabpage" data-tabid="documentstab">
                <div class="formpage">
                  <div classs="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Documents">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="ProjectDocuments" data-securitycaption="Customer Notes" style="min-height:150px;border:1px solid silver;">##### ADD MISSING DOCUMENTS GRID #####</div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <!-- Options section -->
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="width:150px;">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All" data-datafield="Inactive" style="width:125px;"></div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- Consignment tab -->
              <div data-type="tabpage" id="consignmenttabpage" class="tabpage" data-tabid="consignmenttab">
                <div class="flexpage">
                  <div class="wideflexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Consigned Items">
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="InventoryConsignmentGrid" data-securitycaption="Inventory Consignment"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- AVAILABILITY TAB -->
              <div data-type="tabpage" id="availabilitytabpage" class="tabpage" data-tabid="availabilitytab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Availability">
                        <div class="flexrow">
                          <div data-control="FwGrid" data-grid="InventoryAvailabilityGrid" data-securitycaption="Inventory Availability"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Add to Order at Staging">
                        <div class="flexrow">
                          <div data-control="FwGrid" data-grid="InventoryWarehouseStagingGrid" data-securitycaption="Inventory Warehouse Staging"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="width:225px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Do Not Calculate Availability" data-datafield="NoAvailabilityCheck" style="width:200px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Send Availability Alert" data-datafield="SendAvailabilityAlert" style="width:200px;margin-top:0px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Manually Resolve Conflicts" data-datafield="AvailabilityManuallyResolveConflicts" style="width:200px;margin-top:0px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- COMPLETES / KITS TAB -->
              <div data-type="tabpage" id="completeskitstabpage" class="tabpage" data-tabid="completeskitstab">
                <div class="flexpage">
                  <div class="wideflexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Completes / Kits">
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="InventoryCompleteKitGrid" data-securitycaption="Inventory Complete Kit"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="flex:0 1 425px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Print and Display in Summary mode when Rate is $0.00" data-datafield="DisplayInSummaryModeWhenRateIsZero"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- SUBSTITUTES TAB -->
              <div data-type="tabpage" id="substitutestabpage" class="tabpage" data-tabid="substitutestab">
                <div class="flexpage">
                  <div class="wideflexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Substitute Items" style="flex:1 1 950px">
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="InventorySubstituteGrid" data-securitycaption="Inventory Substitute"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- COMPATABILITY TAB -->
              <div data-type="tabpage" id="compatibilitytabpage" class="tabpage" data-tabid="compatibilitytab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 525px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Compatibility">
                        <div class="flexrow">
                          <div data-control="FwGrid" data-grid="InventoryCompatibilityGrid" data-securitycaption="Inventory Compatibility"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 525px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Image">
                        <div class="flexrow">
                          <div class="fwcontrol fwappimage" data-control="FwAppImage" data-type="" data-uniqueid1field="InventoryId" data-description="" data-rectype="F" style="min-height:425px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- Manufacturer tab -->
              <div data-type="tabpage" id="manufacturertabpage" class="tabpage" data-tabid="manufacturertab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Manufacturer" style="width:750px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="CustomerNotes" data-securitycaption="Customer Notes" style="min-height:250px;border:1px solid silver;">
                          <p style="font-size:10pt;">Manufacturer | Part Number | Model | Warranty Period | Country of Origin | Link URL | PDF Link URL</p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- Purchase Vendor tab -->
              <div data-type="tabpage" id="purchasevendortabpage" class="tabpage" data-tabid="purchasevendortab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Purchase Vendor" style="width:750px">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="CustomerNotes" data-securitycaption="Customer Notes" style="min-height:200px;border:1px solid silver;">
                          <p style="font-size:10pt;">Vendor | Date | Part No. | Cost | City | St./Pr. | Phone</p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- SUB RENTAL VENDOR TAB -->
              <div data-type="tabpage" id="subrentalvendortabpage" class="tabpage" data-tabid="subrentalvendortab">
                <div class="flexpage">
                  <div class="wideflexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sub-Rental Vendor" style="flex:1 1 750px;">
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="InventoryVendorGrid" data-securitycaption="Inventory Vendor"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- Weights & Dimensions tab -->
              <div data-type="tabpage" id="weightsdimensionstabpage" class="tabpage" data-tabid="weightsdimensionstab">
                <div class="formpage">
                  <!-- Primary Weights &amp; Dimensions -->
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Primary Weights &amp; Dimensions" style="width:950px;">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Primary Description" data-datafield="PrimaryDimensionDescription" style="width:925px;"></div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="formcolumn" style="width:175px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Item">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="lbs." data-datafield="PrimaryDimensionShipWeightLbs" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="oz." data-datafield="PrimaryDimensionShipWeightOz" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:200px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="In Shipping Case">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="lbs." data-datafield="PrimaryDimensionWeightInCaseLbs" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="oz." data-datafield="PrimaryDimensionWeightInCaseOz" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:175px;margin-left:175px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Item">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="kg." data-datafield="PrimaryDimensionShipWeightKg" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="g." data-datafield="PrimaryDimensionShipWeightG" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:200px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="In Shipping Case">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="kg." data-datafield="PrimaryDimensionWeightInCaseKg" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="g." data-datafield="PrimaryDimensionWeightInCaseG" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="formcolumn" style="width:150px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Length">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ft." data-datafield="PrimaryDimensionLengthFt" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="in." data-datafield="PrimaryDimensionLengthIn" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:150px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Width">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ft." data-datafield="PrimaryDimensionWidthFt" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="in." data-datafield="PrimaryDimensionWidthIn" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:150px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Height">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ft." data-datafield="PrimaryDimensionHeightFt" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="in." data-datafield="PrimaryDimensionHeightIn" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:150px;padding-left:1px;margin-left:25px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Length">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="m." data-datafield="PrimaryDimensionLengthM" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="cm." data-datafield="PrimaryDimensionLengthCm" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:150px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Width">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="m." data-datafield="PrimaryDimensionWidthM" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="cm." data-datafield="PrimaryDimensionWidthCm" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:150px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Height">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="m." data-datafield="PrimaryDimensionHeightM" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="cm." data-datafield="PrimaryDimensionHeightCm" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                  </div>
                  <!-- Secondary Weights &amp; Dimensions -->
                  <div class="formrow" style="margin-top:50px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Secondary Weights &amp; Dimensions" style="width:950px;">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Secondary Description" data-datafield="SecondaryDimensionDescription" style="width:925px;"></div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="formcolumn" style="width:175px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Item">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="lbs." data-datafield="SecondaryDimensionShipWeightLbs" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="oz." data-datafield="SecondaryDimensionShipWeightOz" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:200px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="In Shipping Case">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="lbs." data-datafield="SecondaryDimensionWeightInCaseLbs" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="oz." data-datafield="SecondaryDimensionWeightInCaseOz" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:175px;margin-left:175px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Item">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="kg." data-datafield="SecondaryDimensionShipWeightKg" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="g." data-datafield="SecondaryDimensionShipWeightG" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:200px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="In Shipping Case">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="kg." data-datafield="SecondaryDimensionWeightInCaseKg" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="g." data-datafield="SecondaryDimensionWeightInCaseG" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="formcolumn" style="width:150px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Length">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ft." data-datafield="SecondaryDimensionLengthFt" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="in." data-datafield="SecondaryDimensionLengthIn" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:150px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Width">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ft." data-datafield="SecondaryDimensionWidthFt" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="in." data-datafield="SecondaryDimensionWidthIn" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:150px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Height">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ft." data-datafield="SecondaryDimensionHeightFt" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="in." data-datafield="SecondaryDimensionHeightIn" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:150px;padding-left:1px;margin-left:25px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Length">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="m." data-datafield="SecondaryDimensionLengthM" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="cm." data-datafield="SecondaryDimensionLengthCm" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:150px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Width">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="m." data-datafield="SecondaryDimensionWidthM" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="cm." data-datafield="SecondaryDimensionWidthCm" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:150px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Height">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="m." data-datafield="SecondaryDimensionHeightM" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="cm." data-datafield="SecondaryDimensionHeightCm" style="float:left;width:50px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- QC TAB -->
              <div data-type="tabpage" id="qctabpage" class="tabpage" data-tabid="qctab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="QC">
                      <div class="flexrow">
                        <div data-control="FwGrid" data-grid="InventoryQcGrid" data-securitycaption="Inventory Qc"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="QC Options">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="QC Time" data-datafield="QcTime" style="flex:1 1 75px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Required by All Warehouses" data-datafield="QcRequired" style="flex:1 1 225px;margin-left:25px;"></div>
                      </div>
                    </div>
                    
                  </div>
                </div>
              </div>

              <!-- ATTRIBUTES / USAGE TAB -->
              <div data-type="tabpage" id="attributesusagetabpage" class="tabpage" data-tabid="attributesusagetab">
                <div class="formpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 650px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Attributes">
                        <div class="flexrow">
                          <div data-control="FwGrid" data-grid="InventoryAttributeValueGrid" data-securitycaption="Inventory Attribute Value"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Attributes to Quote/Order as a Note" data-datafield="CopyAttributesAsNote" style="width:275px;margin-top:5px;margin-left:10px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 400px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Track Usage">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Asset Usage" data-datafield="" style="flex:1 1 150px;margin-top:0px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Lamp Usage" data-datafield="" style="flex:1 1 150px;margin-top:0px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Lamp Strikes" data-datafield="" style="flex:1 1 150px;margin-top:0px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Foot-Candles" data-datafield="" style="flex:1 1 150px;margin-top:0px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield lamps" data-caption="Number of Lamps" data-datafield="LampCount" style="flex:1 1 110px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Min. Foot Candles" data-datafield="MinimumFootCandles" style="flex:1 1 125px;margin-left:40px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Track Software">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Enable Software Tracking" data-datafield="TrackSoftware" style="flex:1 1 200px;margin-top:10px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Version" data-datafield="SoftwareVersion" style="flex:1 1 150px;"></div>
                          <!--<div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Effective Date" data-datafield="SoftwareEffectiveDate" style="float:left;width:120px;"></div>-->
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- TAX TAB -->
              <div data-type="tabpage" id="taxtabpage" class="tabpage" data-tabid="taxtab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax" style="flex:0 1 750px;">
                      <div class="flexrow">
                        <div data-control="FwGrid" data-grid="ItemLocationTaxGrid" data-securitycaption="Rate Location Tax"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- Container Packing List tab -->
              <div data-type="tabpage" id="containerpackinglisttabpage" class="tabpage" data-tabid="containerpackinglisttab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Container">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="InventoryContainerGrid" data-securitycaption="Inventory Container"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- COMPLETES TAB -->
              <div data-type="tabpage" id="completetabpage" class="tabpage" data-tabid="completetab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Complete">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="InventoryCompleteGrid" data-securitycaption="Inventory Complete"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- KITS TAB -->
              <div data-type="tabpage" id="kittabpage" class="tabpage" data-tabid="kittab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Kit">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="InventoryKitGrid" data-securitycaption="Inventory Kit"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- NOTES TAB -->
              <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Extended Note" style="flex:0 1 1000px;">
                      <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Notes" data-datafield="Note"></div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Print On" style="flex:0 1 1000px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Quote" data-datafield="PrintNoteOnQuote" style="flex:0 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Order" data-datafield="PrintNoteOnOrder" style="flex:0 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Pick List" data-datafield="PrintNoteOnPickList" style="flex:0 1 150px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Out Contract" data-datafield="PrintNoteOnOutContract" style="flex:0 1 150px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Return List" data-datafield="PrintNoteOnReturnList" style="flex:0 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="In Contract" data-datafield="PrintNoteOnInContract" style="flex:0 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Invoice" data-datafield="PrintNoteOnInvoice" style="flex:0 1 125px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="PO" data-datafield="PrintNoteOnPO" style="flex:0 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Receive List" data-datafield="PrintNoteOnPoReceiveList" style="flex:0 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Receive Contract" data-datafield="PrintNoteOnReceiveContract" style="flex:0 1 150px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Return List" data-datafield="PrintNoteOnPoReturnList" style="flex:0 1 150px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Return Contract" data-datafield="PrintNoteOnReturnContract" style="flex:0 1 125px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Copy Notes" style="flex:0 1 450px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Automatically Copy Inventory Notes to Quote/Order" data-datafield="AutoCopyNotesToQuoteOrder" style="flex:1 1 350px;margin-top:0px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- Access tab -->
              <div data-type="tabpage" id="accesstabpage" class="tabpage" data-tabid="accesstab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="formcolumn" style="width:350px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Department Access">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwGrid" data-grid="CustomerNotes" data-securitycaption="Customer Notes" style="min-height:500px;border:1px solid silver;">
                            <p style="font-size:10pt;">Company Department</p>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:350px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Department Access Override">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwGrid" data-grid="CustomerNotes" data-securitycaption="" style="min-height:500px;border:1px solid silver;">
                            <p style="font-size:10pt;">Company Department Override</p>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- RentalWorks.NET tab -->
              <div data-type="tabpage" id="rentalworksnettabpage" class="tabpage" data-tabid="rentalworksnettab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Web Description" style="width:775px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="" data-datafield="" style="float:left;width:750px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- Audit tab -->
              <!--<div data-type="tabpage" id="audittabpage" class="tabpage" data-tabid="audittab">
            <div class="formpage">
              <div class="formrow">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Update" style="width:500px;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Opened" data-datafield="" data-enabled="false" style="float:left;width:120px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="By" data-datafield="vendor.modifieddate" data-enabled="false" style="float:left;width:350px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Modified Last" data-datafield="" data-enabled="false" style="float:left;width:120px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="By" data-datafield="vendor.modifieddate" data-enabled="false" style="float:left;width:350px;"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>-->
            </div>
          </div>
        </div>
        `;
    }
}

var InventoryItemController = new InventoryItem();