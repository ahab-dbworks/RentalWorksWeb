declare var FwModule: any;
declare var FwBrowse: any;

class RwInventoryGroup {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryGroup';
        this.apiurl = 'api/v1/inventorygroup';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Inventory Group', false, 'BROWSE', true);
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

    renderGrids($form: any) {
        var $inventoryGroupInvGrid: any;
        var $inventoryGroupInvGridControl: any;

        $inventoryGroupInvGrid = $form.find('div[data-grid="InventoryGroupInvGrid"]');
        $inventoryGroupInvGridControl = jQuery(jQuery('#tmpl-grids-InventoryGroupInvGridBrowse').html());
        $inventoryGroupInvGrid.empty().append($inventoryGroupInvGridControl);
        $inventoryGroupInvGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryGroupId: $form.find('div.fwformfield[data-datafield="InventoryGroupId"] input').val()
            };
        })

        $inventoryGroupInvGridControl.data('beforesave', function (request) {
            request.InventoryGroupId = FwFormField.getValueByDataField($form, 'InventoryGroupId');
        });

        $form.find('[data-datafield="RecType"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.val() === "S") {
                $form.find('div.field[data-formdatafield="ICode"]').attr("data-formvalidationname", "SalesInventoryValidation");
            }
            else {
                $form.find('div.field[data-formdatafield="ICode"]').attr("data-formvalidationname", "RentalInventoryValidation");
            }
        });


        FwBrowse.init($inventoryGroupInvGridControl);
        FwBrowse.renderRuntimeHtml($inventoryGroupInvGridControl);

    }
     
    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="InventoryGroupId"] input').val(uniqueids.InventoryGroupId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="InventoryGroupId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }


    afterLoad($form: any) {
        var $inventoryGroupInvGrid: any;

        $inventoryGroupInvGrid = $form.find('[data-name="InventoryGroupInvGrid"]');
        FwBrowse.search($inventoryGroupInvGrid);

    }

 
}

(<any>window).InventoryGroupController = new RwInventoryGroup();