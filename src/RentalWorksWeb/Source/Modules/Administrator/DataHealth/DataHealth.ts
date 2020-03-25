routes.push({ pattern: /^module\/datahealth$/, action: function (match: RegExpExecArray) { return DataHealthController.getModuleScreen(); } });

class DataHealth {
    Module:     string = 'DataHealth';
    apiurl:     string = 'api/v1/datahealth';
    caption:    string = Constants.Modules.Administrator.children.DataHealth.caption;
    nav:        string = Constants.Modules.Administrator.children.DataHealth.nav;
    id:         string = Constants.Modules.Administrator.children.DataHealth.id;
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
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //removes field propagation
        //$form.off('change', '.fwformfield[data-enabled="true"][data-datafield!=""]:not(.find-field)');

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DataHealthId"] input').val(uniqueids.DataHealthId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery) {
        //FwBrowse.renderGrid({
        //    nameGrid:         'CustomFormGroupGrid',
        //    gridSecurityId:   '11txpzVKVGi2',
        //    moduleSecurityId: this.id,
        //    $form:            $form,
        //    onDataBind: (request: any) => {
        //        request.uniqueids = {
        //            CustomFormId: FwFormField.getValueByDataField($form, 'CustomFormId')
        //        };
        //    },
        //    beforeSave: (request: any) => {
        //        request.CustomFormId = FwFormField.getValueByDataField($form, 'CustomFormId')
        //    }
        //});
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery) {
       
    }
    //----------------------------------------------------------------------------------------------a
};
//----------------------------------------------------------------------------------------------
var DataHealthController = new DataHealth();