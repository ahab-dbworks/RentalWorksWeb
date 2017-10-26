declare var FwModule: any;
declare var FwBrowse: any;
declare var FwSettings: any;

class CustomFields {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CustomFields';
        this.apiurl = 'api/v1/customfield';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Custom Fields', false, 'BROWSE', true);
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
        var $form
            , $moduleSelect;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'))
        } else {
            FwFormField.disable($form.find('.ifnew'))
        }

        var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '730C9659-B33B-493E-8280-76A060A07DCE');
        var modules = FwApplicationTree.getChildrenByType(node, 'Module');
        var allModules = [];

        for (var i = 0; i < modules.length; i++) {
            var moduleNav = modules[i].properties.controller.slice(0, -10);
            var moduleCaption = modules[i].properties.caption
            allModules.push({ value: moduleNav, text: moduleCaption });
        };
        console.log(allModules);

        $moduleSelect = $form.find('.modules');
        FwFormField.loadItems($moduleSelect, allModules);
 
        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CustomFieldId"] input').val(uniqueids.CustomFieldId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="CustomFieldId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {

    }
}

(<any>window).CustomFieldsController = new CustomFields();