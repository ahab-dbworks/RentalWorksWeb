routes.push({ pattern: /^module\/partsinventory$/, action: function (match: RegExpExecArray) { return PartsInventoryController.getModuleScreen(); } });

//----------------------------------------------------------------------------------------------
class PartsInventory {
    Module: string = 'PartsInventory';
    apiurl: string = 'api/v1/partsinventory';
    ActiveView: string  = 'ALL';

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        let screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Parts Inventory', false, 'BROWSE', true);
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
        let self = this;
        let $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        this.ActiveView = 'ALL'; // Resets view to all when revisting module page

        $browse.data('ondatabind', function (request) {
            request.activeview = self.ActiveView;
        });
        FwBrowse.addLegend($browse, 'Item', '#ffffff');
        FwBrowse.addLegend($browse, 'Accessory', '#fffa00');
        FwBrowse.addLegend($browse, 'Complete', '#0080ff');
        FwBrowse.addLegend($browse, 'Kit', '#00c400');
        FwBrowse.addLegend($browse, 'Misc', '#ff0080');
        FwBrowse.addLegend($browse, 'Container', '#ff8040');

        return $browse;
    };

    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject: any) {
        let self = this;
        let $all: JQuery = FwMenu.generateDropDownViewBtn('All', true);
        let $item: JQuery = FwMenu.generateDropDownViewBtn('Item', true);
        let $accessory: JQuery = FwMenu.generateDropDownViewBtn('Accessory', false);
        let $complete: JQuery = FwMenu.generateDropDownViewBtn('Complete', false);
        let $kitset: JQuery = FwMenu.generateDropDownViewBtn('Kit', false);
        let $misc: JQuery = FwMenu.generateDropDownViewBtn('Misc', false);
        let $container: JQuery = FwMenu.generateDropDownViewBtn('Container', false);

        $all.on('click', function() {
            let $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ALL';
            FwBrowse.search($browse);
        });
        $item.on('click', function() {
            let $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ITEM';
            FwBrowse.search($browse);
        });
        $accessory.on('click', function() {
            let $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ACCESSORY';
            FwBrowse.search($browse);
        });
        $complete.on('click', function() {
            let $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'COMPLETE';
            FwBrowse.search($browse);
        });
        $kitset.on('click', function() {
            let $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'KITSET';
            FwBrowse.search($browse);
        });
        $misc.on('click', function() {
            let $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'MISC';
            FwBrowse.search($browse);
        });
        $container.on('click', function() {
            let $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CONTAINER';
            FwBrowse.search($browse);
        });

        FwMenu.addVerticleSeparator($menuObject);

        let viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all);
        viewSubitems.push($item);
        viewSubitems.push($accessory);
        viewSubitems.push($complete);
        viewSubitems.push($kitset);
        viewSubitems.push($misc);
        viewSubitems.push($container);
        let $view;
        $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);

        return $menuObject;
    };

    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        $form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').on('change', function () {
            let $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
            }
            else {
                FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
            }
        });

        if (mode === 'NEW') {
            FwFormField.enable($form.find('[data-datafield="Classification"]'));

            $form.find('div[data-datafield="Classification"] .fwformfield-value').on('change', function () {
                var $this = jQuery(this);

                $form.find('.completeskitstab').show();
                $form.find('.completetab').hide();
                $form.find('.kittab').hide();

                if ($this.prop('checked') === true && $this.val() === 'C') {
                    $form.find('.completetab').show();
                    $form.find('.completeskitstab').hide();
                }
                if ($this.prop('checked') === true && $this.val() === 'K') {
                    $form.find('.kittab').show();
                }
            })
        };

        $form.find('div[data-datafield="InventoryTypeId"]').data('onchange', $tr => {
            if ($tr.find('.field[data-browsedatafield="Wardrobe"]').attr('data-originalvalue') === 'true') {
                $form.find('.wardrobetab').show();
            } else {
                $form.find('.wardrobetab').hide();
            }
        });

        $form.find('div[data-datafield="CategoryId"]').data('onchange', $tr => {
            FwFormField.disable($form.find('.subcategory'));
            if ($tr.find('.field[data-browsedatafield="SubCategoryCount"]').attr('data-originalvalue') > 0) {
                FwFormField.enable($form.find('.subcategory'));
            } else {
                FwFormField.setValueByDataField($form, 'SubCategoryId', '')
            }
        });

        return $form;
    };

    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(uniqueids.InventoryId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };

    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };

    //----------------------------------------------------------------------------------------------
    loadAudit($form: any) {
        let uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };

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
        FwBrowse.search($itemLocationTaxGrid);
        $salesInventoryWarehouseGrid = $form.find('[data-name="SalesInventoryWarehouseGrid"]');
        FwBrowse.search($salesInventoryWarehouseGrid);
        $inventoryAvailabilityGrid = $form.find('[data-name="InventoryAvailabilityGrid"]');
        FwBrowse.search($inventoryAvailabilityGrid);
        $inventoryConsignmentGrid = $form.find('[data-name="InventoryConsignmentGrid"]');
        FwBrowse.search($inventoryConsignmentGrid);
        $inventoryCompleteKitGrid = $form.find('[data-name="InventoryCompleteKitGrid"]');
        FwBrowse.search($inventoryCompleteKitGrid);
        $partsinventorySubstituteGrid = $form.find('[data-name="PartsInventorySubstituteGrid"]');
        FwBrowse.search($partsinventorySubstituteGrid);
        $partsinventoryCompatibilityGrid = $form.find('[data-name="PartsInventoryCompatibilityGrid"]');
        FwBrowse.search($partsinventoryCompatibilityGrid);
        $inventoryQcGrid = $form.find('[data-name="InventoryQcGrid"]');
        FwBrowse.search($inventoryQcGrid);
        $inventoryAttributeValueGrid = $form.find('[data-name="InventoryAttributeValueGrid"]');
        FwBrowse.search($inventoryAttributeValueGrid);
        $inventoryVendorGrid = $form.find('[data-name="InventoryVendorGrid"]');
        FwBrowse.search($inventoryVendorGrid);
        $inventoryPrepGrid = $form.find('[data-name="InventoryPrepGrid"]');
        FwBrowse.search($inventoryPrepGrid);
        $wardrobeInventoryColorGrid = $form.find('[data-name="WardrobeInventoryColorGrid"]');
        FwBrowse.search($wardrobeInventoryColorGrid);
        $wardrobeInventoryMaterialGrid = $form.find('[data-name="WardrobeInventoryMaterialGrid"]');
        FwBrowse.search($wardrobeInventoryMaterialGrid);
        $inventoryCompleteGrid = $form.find('[data-name="InventoryCompleteGrid"]');
        FwBrowse.search($inventoryCompleteGrid);
        $inventoryKitGrid = $form.find('[data-name="InventoryKitGrid"]');
        FwBrowse.search($inventoryKitGrid);

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'I' || FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'A') {
            FwFormField.enable($form.find('[data-datafield="Classification"]'));
            $form.find('.completeradio').hide();
            $form.find('.kitradio').hide();
            $form.find('.containerradio').hide();
            $form.find('.miscradio').hide();
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'N') {
            $form.find('.kitradio').hide();
            $form.find('.completeradio').hide();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.miscradio').hide();
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'C') {
            $form.find('.completetab').show();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.containerradio').hide();
            $form.find('.miscradio').hide();
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'K') {
            $form.find('.kittab').show();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.containerradio').hide();
            $form.find('.miscradio').hide();
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'M') {
            $form.find('.completeradio').hide();
            $form.find('.kitradio').hide();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.containerradio').hide();

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
const PartsInventoryController = new PartsInventory();