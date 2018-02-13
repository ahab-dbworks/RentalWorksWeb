class FormDesign {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'FormDesign';
        this.apiurl = 'api/v1/formdesign';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Form Design', false, 'BROWSE', true);
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

        return $browse;
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="FormDesignId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {

    }
}

(<any>window).FormDesignController = new FormDesign();