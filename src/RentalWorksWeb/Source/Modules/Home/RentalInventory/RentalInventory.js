var RentalInventory = (function () {
    function RentalInventory() {
        this.Module = 'RentalInventory';
        this.apiurl = 'api/v1/rentalinventory';
        this.ActiveView = 'ALL';
    }
    RentalInventory.prototype.getModuleScreen = function () {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Rental Inventory', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    RentalInventory.prototype.openBrowse = function () {
        var self = this;
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        $browse.data('ondatabind', function (request) {
            request.activeview = self.ActiveView;
        });
        FwBrowse.addLegend($browse, 'Item', '#ffffff');
        FwBrowse.addLegend($browse, 'Accessory', '#fffa00');
        FwBrowse.addLegend($browse, 'Complete', '#0080ff');
        FwBrowse.addLegend($browse, 'Kit/Set', '#00c400');
        FwBrowse.addLegend($browse, 'Misc', '#ff0080');
        FwBrowse.addLegend($browse, 'Container', '#ff8040');
        return $browse;
    };
    RentalInventory.prototype.addBrowseMenuItems = function ($menuObject) {
        var self = this;
        var $all = FwMenu.generateDropDownViewBtn('All Items', true);
        var $accessory = FwMenu.generateDropDownViewBtn('Accessory', false);
        var $complete = FwMenu.generateDropDownViewBtn('Complete', false);
        var $kitset = FwMenu.generateDropDownViewBtn('Kit/Set', false);
        var $misc = FwMenu.generateDropDownViewBtn('Misc', false);
        var $container = FwMenu.generateDropDownViewBtn('Container', false);
        $all.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ALL';
            FwBrowse.databind($browse);
        });
        $accessory.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ACCESSORY';
            FwBrowse.databind($browse);
        });
        $complete.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'COMPLETE';
            FwBrowse.databind($browse);
        });
        $kitset.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'KITSET';
            FwBrowse.databind($browse);
        });
        $misc.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'MISC';
            FwBrowse.databind($browse);
        });
        $container.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CONTAINER';
            FwBrowse.databind($browse);
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
    RentalInventory.prototype.openForm = function (mode) {
        var $form, $rank;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        if (mode === 'NEW') {
            FwFormField.enable($form.find('[data-datafield="Classification"]'));
            $form.find('div[data-datafield="Classification"] .fwformfield-value').on('change', function () {
                var $this = jQuery(this);
                $form.find('.completeskitstab').show();
                $form.find('.containertab').hide();
                $form.find('.completetab').hide();
                $form.find('.kittab').hide();
                if ($this.prop('checked') === true && $this.val() === 'N') {
                    $form.find('.containertab').show();
                    $form.find('.completeskitstab').hide();
                }
                if ($this.prop('checked') === true && $this.val() === 'C') {
                    $form.find('.completetab').show();
                    $form.find('.completeskitstab').hide();
                }
                if ($this.prop('checked') === true && $this.val() === 'K') {
                    $form.find('.kittab').show();
                }
            });
        }
        $form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
            }
            else {
                FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
            }
        });
        $rank = $form.find('.rank');
        FwFormField.loadItems($rank, [
            { value: 'A', text: 'A' },
            { value: 'B', text: 'B' },
            { value: 'C', text: 'C' },
            { value: 'D', text: 'D' }
        ]);
        FwFormField.loadItems($form.find('.lamps'), [
            { value: '0', text: '0' },
            { value: '1', text: '1' },
            { value: '2', text: '2' },
            { value: '3', text: '3' },
            { value: '4', text: '4' }
        ], true);
        return $form;
    };
    RentalInventory.prototype.loadForm = function (uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(uniqueids.InventoryId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    RentalInventory.prototype.saveForm = function ($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    };
    RentalInventory.prototype.loadAudit = function ($form) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    RentalInventory.prototype.renderGrids = function ($form) {
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
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
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
                PackageId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryCompleteGridControl.data('beforesave', function (request) {
            request.PackageId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
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
        FwBrowse.init($inventoryKitGridControl);
        FwBrowse.renderRuntimeHtml($inventoryKitGridControl);
    };
    RentalInventory.prototype.afterLoad = function ($form) {
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
        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'N') {
            $form.find('.containertab').show();
            $form.find('.completeskitstab').hide();
        }
        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'C') {
            $form.find('.completetab').show();
            $form.find('.completeskitstab').hide();
        }
        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'K') {
            $form.find('.kittab').show();
        }
        if ($form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
        }
        else {
            FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
        }
    };
    return RentalInventory;
}());
window.RentalInventoryController = new RentalInventory();
//# sourceMappingURL=RentalInventory.js.map