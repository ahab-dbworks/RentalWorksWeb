class RwInventoryGroup {
    Module: string = 'InventoryGroup';
    apiurl: string = 'api/v1/inventorygroup';
    caption: string = 'Inventory Group';
    nav: string = 'module/inventorygroup';
    id: string = '43AF2FBA-69FB-46A8-8E5A-2712486B66F3';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const $inventoryGroupInvGrid = $form.find('div[data-grid="InventoryGroupInvGrid"]');
        const $inventoryGroupInvGridControl = FwBrowse.loadGridFromTemplate('InventoryGroupInvGrid');
        $inventoryGroupInvGrid.empty().append($inventoryGroupInvGridControl);
        $inventoryGroupInvGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryGroupId: $form.find('div.fwformfield[data-datafield="InventoryGroupId"] input').val()
            };
        })

        $inventoryGroupInvGridControl.data('beforesave', function (request) {
            request.InventoryGroupId = FwFormField.getValueByDataField($form, 'InventoryGroupId');
        });
        FwBrowse.init($inventoryGroupInvGridControl);
        FwBrowse.renderRuntimeHtml($inventoryGroupInvGridControl);
        // ----------
 
        this.events($form); // Here for a grid specific event
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="InventoryGroupId"] input').val(uniqueids.InventoryGroupId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $inventoryGroupInvGrid: JQuery = $form.find('[data-name="InventoryGroupInvGrid"]');
        FwBrowse.search($inventoryGroupInvGrid);
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        $form.find('[data-datafield="RecType"] .fwformfield-value').on('change', function () {
            const value = FwFormField.getValueByDataField($form, "RecType");
            value === "S" ? $form.find('div.field[data-formdatafield="InventoryId"]').attr("data-formvalidationname", "SalesInventoryValidation") : $form.find('div.field[data-formdatafield="InventoryId"]').attr("data-formvalidationname", "RentalInventoryValidation");
        });
    }
}

var InventoryGroupController = new RwInventoryGroup();