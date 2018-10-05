class Control {
    Module: string = 'Control';
    apiurl: string = 'api/v1/control';
    reportImageId: string = '';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Control', false, 'BROWSE', true);
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
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'))
        } else {
            FwFormField.disable($form.find('.ifnew'))
        }

        let request = { "uniqueid1":"1", "uniqueid2":"", "uniqueid3":"", "description":"", "rectype":"F", "requestid":"a7e0e9d6-3703-47a1-bd4f-08286bf2c86a" }
        FwAppData.jsonPost(false, 'fwappimage.ashx?method=GetAppImages', request, FwServices.defaultTimeout, response => {
            this.reportImageId = response.images[0].appimageid
        }, null, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ControlId"] input').val(uniqueids.ControlId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);

        let request = { "uniqueid1": "1", "uniqueid2": "", "uniqueid3": "", "description": "", "rectype": "F", "requestid": "a7e0e9d6-3703-47a1-bd4f-08286bf2c86a" }
        FwAppData.jsonPost(false, 'fwappimage.ashx?method=GetAppImages', request, FwServices.defaultTimeout, response => {
            this.reportImageId = response.images[0].appimageid
        }, null, $form);

        FwAppData.apiMethod(true, 'GET', `api/v1/fwappimage.ashx?method=GetAppImage&appimageid=${this.reportImageId}`, null, FwServices.defaultTimeout, (response) => { console.log(response) }, null, null)

    }
    //----------------------------------------------------------------------------------------------
    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="ControlId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
    }
}
//----------------------------------------------------------------------------------------------
var ControlController = new Control();