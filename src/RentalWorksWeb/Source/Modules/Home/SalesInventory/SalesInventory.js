var SalesInventory = (function () {
    function SalesInventory() {
        this.Module = 'SalesInventory';
        this.apiurl = 'api/v1/salesinventory';
        this.ActiveView = 'ALL';
    }
    SalesInventory.prototype.getModuleScreen = function () {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Sales Inventory', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    ;
    SalesInventory.prototype.openBrowse = function () {
        var self = this;
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        this.ActiveView = 'ALL';
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
    ;
    SalesInventory.prototype.addBrowseMenuItems = function ($menuObject) {
        var self = this;
        var $all = FwMenu.generateDropDownViewBtn('All Items', true);
        var $accessory = FwMenu.generateDropDownViewBtn('Accessory', false);
        var $complete = FwMenu.generateDropDownViewBtn('Complete', false);
        var $kitset = FwMenu.generateDropDownViewBtn('Kit', false);
        var $misc = FwMenu.generateDropDownViewBtn('Misc', false);
        var $container = FwMenu.generateDropDownViewBtn('Container', false);
        $all.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ALL';
            FwBrowse.search($browse);
        });
        $accessory.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ACCESSORY';
            FwBrowse.search($browse);
        });
        $complete.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'COMPLETE';
            FwBrowse.search($browse);
        });
        $kitset.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'KITSET';
            FwBrowse.search($browse);
        });
        $misc.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'MISC';
            FwBrowse.search($browse);
        });
        $container.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CONTAINER';
            FwBrowse.search($browse);
        });
        FwMenu.addVerticleSeparator($menuObject);
        var viewSubitems = [];
        viewSubitems.push($all);
        viewSubitems.push($accessory);
        viewSubitems.push($complete);
        viewSubitems.push($kitset);
        viewSubitems.push($misc);
        viewSubitems.push($container);
        var $view;
        $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);
        return $menuObject;
    };
    ;
    SalesInventory.prototype.openForm = function (mode) {
        var $form;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        $form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
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
            });
        }
        ;
        $form.find('div[data-datafield="InventoryTypeId"]').data('onchange', function ($tr) {
            if ($tr.find('.field[data-browsedatafield="Wardrobe"]').attr('data-originalvalue') === 'true') {
                $form.find('.wardrobetab').show();
            }
            else {
                $form.find('.wardrobetab').hide();
            }
        });
        $form.find('div[data-datafield="CategoryId"]').data('onchange', function ($tr) {
            FwFormField.disable($form.find('.subcategory'));
            if ($tr.find('.field[data-browsedatafield="SubCategoryCount"]').attr('data-originalvalue') > 0) {
                FwFormField.enable($form.find('.subcategory'));
            }
            else {
                FwFormField.setValueByDataField($form, 'SubCategoryId', '');
            }
        });
        return $form;
    };
    ;
    SalesInventory.prototype.loadForm = function (uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(uniqueids.InventoryId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    ;
    SalesInventory.prototype.saveForm = function ($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    ;
    SalesInventory.prototype.loadAudit = function ($form) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    ;
    SalesInventory.prototype.renderGrids = function ($form) {
        var $itemLocationTaxGrid;
        var $itemLocationTaxGridControl;
        var $salesInventoryWarehouseGrid;
        var $salesInventoryWarehouseGridControl;
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
        var $wardrobeInventoryColorGrid;
        var $wardrobeInventoryColorGridControl;
        var $wardrobeInventoryMaterialGrid;
        var $wardrobeInventoryMaterialGridControl;
        var $inventoryCompleteGrid;
        var $inventoryCompleteGridControl;
        var $inventoryKitGrid;
        var $inventoryKitGridControl;
        var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
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
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
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
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
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
    };
    ;
    SalesInventory.prototype.afterLoad = function ($form) {
        var $itemLocationTaxGrid;
        var $salesInventoryWarehouseGrid;
        var $inventoryAvailabilityGrid;
        var $inventoryConsignmentGrid;
        var $inventoryCompleteKitGrid;
        var $inventorySubstituteGrid;
        var $inventoryCompatibilityGrid;
        var $inventoryQcGrid;
        var $inventoryAttributeValueGrid;
        var $inventoryVendorGrid;
        var $inventoryPrepGrid;
        var $wardrobeInventoryColorGrid;
        var $wardrobeInventoryMaterialGrid;
        var $inventoryCompleteGrid;
        var $inventoryKitGrid;
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
        $inventorySubstituteGrid = $form.find('[data-name="SalesInventorySubstituteGrid"]');
        FwBrowse.search($inventorySubstituteGrid);
        $inventoryCompatibilityGrid = $form.find('[data-name="SalesInventoryCompatibilityGrid"]');
        FwBrowse.search($inventoryCompatibilityGrid);
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
    };
    ;
    SalesInventory.prototype.beforeValidate = function ($browse, $grid, request) {
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
        }
        ;
    };
    ;
    return SalesInventory;
}());
;
var SalesInventoryController = new SalesInventory();
//# sourceMappingURL=SalesInventory.js.map