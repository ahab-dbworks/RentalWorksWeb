declare var FwModule: any;
declare var FwBrowse: any;

class SalesInventory {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'SalesInventory';
        this.apiurl = 'api/v1/salesinventory';
    }

    getModuleScreen() {
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
    }

    openBrowse() {
        var $browse;

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        $form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'))
            }
            else {
                FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategoryId"]'))
            }
        });

        if (mode === 'NEW') {
            FwFormField.enable($form.find('[data-datafield="Classification"]'));
        };

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form, $rank;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(uniqueids.InventoryId);
        FwModule.loadForm(this.Module, $form);

        $rank = $form.find('.rank');
        FwFormField.loadItems($rank, [
            { value: 'A', text: 'A' },
            { value: 'B', text: 'B' },
            { value: 'C', text: 'C' },
            { value: 'D', text: 'D' }
        ]);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

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

        $rentalInventoryWarehouseGrid = $form.find('div[data-grid="RentalInventoryWarehouseGrid"]');
        $rentalInventoryWarehouseGridControl = jQuery(jQuery('#tmpl-grids-RentalInventoryWarehouseGridBrowse').html());
        $rentalInventoryWarehouseGrid.empty().append($rentalInventoryWarehouseGridControl);
        $rentalInventoryWarehouseGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
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
    }

    afterLoad($form: any) {
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

        if ($form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'))
        } else {
            FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategoryId"]'))
        }
    }
}

(<any>window).SalesInventoryController = new SalesInventory();