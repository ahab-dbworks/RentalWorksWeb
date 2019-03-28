class InventoryRank {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryRank';
        this.apiurl = 'api/v1/inventoryrank';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Inventory Rank', false, 'BROWSE', true);
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

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    requiredFalse(): void {
        jQuery('.requiredfalse').attr('data-required', 'false');
    }

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode == 'NEW') {
            this.requiredFalse();
            FwFormField.setValueByDataField($form, 'AFromValue', 0);
            FwFormField.setValueByDataField($form, 'AToValue', 0);
            FwFormField.setValueByDataField($form, 'BFromValue', 0);
            FwFormField.setValueByDataField($form, 'BToValue', 0);
            FwFormField.setValueByDataField($form, 'CFromValue', 0);
            FwFormField.setValueByDataField($form, 'CToValue', 0);
            FwFormField.setValueByDataField($form, 'DFromValue', 0);
            FwFormField.setValueByDataField($form, 'DToValue', 0);
            FwFormField.setValueByDataField($form, 'EFromValue', 0);
            FwFormField.setValueByDataField($form, 'EToValue', 0);
            FwFormField.setValueByDataField($form, 'FFromValue', 0);
            FwFormField.setValueByDataField($form, 'FToValue', 0);
            FwFormField.setValueByDataField($form, 'GFromValue', 0);
            FwFormField.setValueByDataField($form, 'GToValue', 0);
        }

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="InventoryRankId"] input').val(uniqueids.InventoryRankId);
        FwModule.loadForm(this.Module, $form);
        this.requiredFalse();
        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="InventoryRankId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {
        this.requiredFalse();
    }
}

var InventoryRankController = new InventoryRank();