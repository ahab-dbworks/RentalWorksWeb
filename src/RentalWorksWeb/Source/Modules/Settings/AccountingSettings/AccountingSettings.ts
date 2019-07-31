class AccountingSettings {
    Module: string = 'AccountingSettings';
    apiurl: string = 'api/v1/accountingsettings';

    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Accounting Settings', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        return $form;
    }

    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ControlId"] input').val(uniqueids.ControlId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    renderGrids($form: any) {
        //----------
        //const $orderTypeCoverLetterGrid = $form.find('div[data-grid="OrderTypeCoverLetterGrid"]');
        //const $orderTypeCoverLetterGridControl = FwBrowse.loadGridFromTemplate('OrderTypeCoverLetterGrid');
        //$orderTypeCoverLetterGrid.empty().append($orderTypeCoverLetterGridControl);
        //$orderTypeCoverLetterGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderTypeId: FwFormField.getValueByDataField($form, 'PoTypeId')
        //    };
        //})
        //$orderTypeCoverLetterGridControl.data('beforesave', request => {
        //    request.OrderTypeId = FwFormField.getValueByDataField($form, 'PoTypeId');
        //});
        //FwBrowse.init($orderTypeCoverLetterGridControl);
        //FwBrowse.renderRuntimeHtml($orderTypeCoverLetterGridControl);
        //----------
    }

    afterLoad($form: any) {

    }
}

var AccountingSettingsController = new AccountingSettings();