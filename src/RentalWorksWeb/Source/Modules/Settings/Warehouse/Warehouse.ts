declare var FwModule: any;
declare var FwBrowse: any;

class Warehouse {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Warehouse';
        this.apiurl = 'api/v1/warehouse';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Warehouse', false, 'BROWSE', true);
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

        $form.find('[data-datafield="AssignBarCodesBy"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            $form.find('.singlerange').hide();            
            $form.find('.warehousedepartment').hide();
            $form.find('.warehouseinventorytype').hide();

            if ($this.val() === 'S') {
                $form.find('.singlerange').show();
            }
            if ($this.val() === 'D') {
                $form.find('.warehousedepartment').show();
            }
            if ($this.val() === 'C') {
                $form.find('.warehouseinventorytype').show();
            }
        });

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val(uniqueids.WarehouseId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: any) {
        var $warehouseDepartmentGrid: any;
        var $warehouseDepartmentGridControl: any;
        var $warehouseInventoryTypeGrid: any;
        var $warehouseInventoryTypeGridControl: any;

        $warehouseDepartmentGrid = $form.find('div[data-grid="WarehouseDepartmentGrid"]');
        $warehouseDepartmentGridControl = jQuery(jQuery('#tmpl-grids-WarehouseDepartmentGridBrowse').html());
        $warehouseDepartmentGrid.empty().append($warehouseDepartmentGridControl);
        $warehouseDepartmentGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                WarehouseId: $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val()
            };
        });
        $warehouseDepartmentGridControl.data('beforesave', function (request) {
            request.WarehouseId = $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val()
        });
        FwBrowse.init($warehouseDepartmentGridControl);
        FwBrowse.renderRuntimeHtml($warehouseDepartmentGridControl);

        $warehouseInventoryTypeGrid = $form.find('div[data-grid="WarehouseInventoryTypeGrid"]');
        $warehouseInventoryTypeGridControl = jQuery(jQuery('#tmpl-grids-WarehouseInventoryTypeGridBrowse').html());
        $warehouseInventoryTypeGrid.empty().append($warehouseInventoryTypeGridControl);
        $warehouseInventoryTypeGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                WarehouseId: $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val()
            };
        });
        $warehouseInventoryTypeGridControl.data('beforesave', function (request) {
            request.WarehouseId = $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val()
        });
        FwBrowse.init($warehouseInventoryTypeGridControl);
        FwBrowse.renderRuntimeHtml($warehouseInventoryTypeGridControl);
    }


    afterLoad($form: any) {
        var $warehouseDepartmentGrid: any;
        var $warehouseInventoryTypeGrid: any;

        $warehouseDepartmentGrid = $form.find('[data-name="WarehouseDepartmentGrid"]');
        FwBrowse.search($warehouseDepartmentGrid);

        $warehouseInventoryTypeGrid = $form.find('[data-name="WarehouseInventoryTypeGrid"]');
        FwBrowse.search($warehouseInventoryTypeGrid);

        if (FwFormField.getValue($form, 'div[data-datafield="AssignBarCodesBy"]') === 'S') {
            $form.find('.singlerange').show();
        }
        if (FwFormField.getValue($form, 'div[data-datafield="AssignBarCodesBy"]') === 'D') {
            $form.find('.warehousedepartment').show();
        }
        if (FwFormField.getValue($form, 'div[data-datafield="AssignBarCodesBy"]') === 'C') {
            $form.find('.warehouseinventorytype').show();
        }
    }
}

(window as any).WarehouseController = new Warehouse();