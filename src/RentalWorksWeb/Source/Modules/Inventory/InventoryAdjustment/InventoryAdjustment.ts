routes.push({ pattern: /^module\/inventoryadjustment$/, action: function (match: RegExpExecArray) { return InventoryAdjustmentController.getModuleScreen(); } });
class InventoryAdjustment {
    Module: string = 'InventoryAdjustment';
    apiurl: string = 'api/v1/inventoryadjustment';
    caption: string = Constants.Modules.Inventory.children.InventoryAdjustment.caption;
    nav: string = Constants.Modules.Inventory.children.InventoryAdjustment.nav;
    id: string = Constants.Modules.Inventory.children.InventoryAdjustment.id;
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
    openForm(mode, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        FwFormField.loadItems($form.find('div[data-datafield="AdjustmentType"]'), [
            { value: 'OH', caption: 'Quantity on Hand', checked: 'checked' },
            { value: 'OHAC', caption: 'Quantity on Hand - Adjust Average Cost' },
            { value: 'AC', caption: 'Average Cost' }
        ]);

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="InventoryAdjustmentId"] input').val(uniqueids.InventoryAdjustmentId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    afterSave($form: JQuery): void {
        //refresh submodule browse after saving
        const $tab = FwTabs.getTabByElement($form);
        const parentTabId = $tab.data('parenttabid');
        const $parentTab = jQuery(`#${parentTabId}`);
        const $parentTabPage = FwTabs.getTabPageByTab($parentTab);
        const $inventoryAdjustmentBrowse = $parentTabPage.find('.fwbrowse[data-name="InventoryAdjustment"]');
        if ($inventoryAdjustmentBrowse.length > 0) {
            FwBrowse.search($inventoryAdjustmentBrowse);
        }
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery): void {
        FwModule.setFormReadOnly($form);
    };
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        //Calculate New Quantity
        $form.on('change', '[data-datafield="OnHandAdjustment"]', e => {
            const oldQty = parseInt(FwFormField.getValueByDataField($form, 'OldQuantity'));
            const adjustment = parseInt(FwFormField.getValueByDataField($form, 'OnHandAdjustment'));
            const newQty = oldQty + adjustment;
            FwFormField.setValueByDataField($form, 'NewQuantity', newQty);
        });
        //Calculate New Value
        $form.on('change', '[data-datafield="AverageCostAdjustment"]', e => {
            const oldVal = parseFloat(FwFormField.getValueByDataField($form, 'OldUnitCost'));
            const adjustment = parseFloat(FwFormField.getValueByDataField($form, 'AverageCostAdjustment'));
            const newVal = oldVal + adjustment;
            FwFormField.setValueByDataField($form, 'NewUnitCost', newVal);
        });

        //Toggle Quantity on Hand / Average Cost sections
        $form.on('change', '[data-datafield="AdjustmentType"]', e => {
            const type = FwFormField.getValueByDataField($form, 'AdjustmentType');
            switch (type) {
                case 'OH':
                    FwFormField.enableDataField($form, 'OnHandAdjustment');
                    FwFormField.setValueByDataField($form, 'AverageCostAdjustment', 0, 0, true);
                    FwFormField.disableDataField($form, 'AverageCostAdjustment');
                    break;
                case 'AC':
                    FwFormField.enableDataField($form, 'AverageCostAdjustment');
                    FwFormField.disableDataField($form, 'OnHandAdjustment');
                    FwFormField.setValueByDataField($form, 'OnHandAdjustment', 0, 0, true);
                    break;
                case 'OHAC':
                    FwFormField.enableDataField($form, 'OnHandAdjustment');
                    FwFormField.enableDataField($form, 'AverageCostAdjustment');
                    break;
            }
        });
    };
   //----------------------------------------------------------------------------------------------

}
var InventoryAdjustmentController = new InventoryAdjustment();