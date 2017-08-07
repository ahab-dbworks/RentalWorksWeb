declare var FwModule: any;
declare var FwBrowse: any;

class OrganizationType {
Module: string;
apiurl: string;

constructor() {
    this.Module = 'OrganizationType';
    this.apiurl = 'api/v1/organizationtype';
}

getModuleScreen() {
    var screen, $browse;

    screen = {};
    screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
    screen.viewModel = {};
    screen.properties = {};

    $browse = this.openBrowse();

    screen.load = function () {
        FwModule.openModuleTab($browse, 'OrganizationType', false, 'BROWSE', true);
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

    return $form;
}

loadForm(uniqueids: any) {
    var $form;

    $form = this.openForm('EDIT');
    $form.find('div.fwformfield[data-datafield="OrganizationTypeId"] input').val(uniqueids.OrganizationTypeId);
    FwModule.loadForm(this.Module, $form);

        return $form;
    }

saveForm($form: any, closetab: boolean, navigationpath: string)
    {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

loadAudit($form: any)
    {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="OrganizationTypeId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

afterLoad($form: any)
    {

    }
}

(<any>window).OrganizationTypeController = new OrganizationType();