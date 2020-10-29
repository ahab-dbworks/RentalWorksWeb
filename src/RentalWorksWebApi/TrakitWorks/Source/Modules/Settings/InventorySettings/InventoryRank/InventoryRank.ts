class InventoryRank {
    Module: string = 'InventoryRank';
    apiurl: string = 'api/v1/inventoryrank';
    caption: string = Constants.Modules.Settings.children.InventorySettings.children.InventoryRank.caption;
    nav:     string = Constants.Modules.Settings.children.InventorySettings.children.InventoryRank.nav;
    id:      string = Constants.Modules.Settings.children.InventorySettings.children.InventoryRank.id;


    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

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

    afterLoad($form: any) {
        this.requiredFalse();
    }
    //-------------------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'WarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                break;
            case 'InventoryTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorytype`);
                break;
        }
    }
}

var InventoryRankController = new InventoryRank();