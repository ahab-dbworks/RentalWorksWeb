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

        var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '0A5F2584-D239-480F-8312-7C2B552A30BA');
        var modules = FwApplicationTree.getChildrenByType(node, 'Module');
        var allModules = [];

        for (var i = 0; i < modules.length; i++) {
            var moduleNav = modules[i].properties.controller.slice(0, -10);
            var moduleCaption = modules[i].properties.caption
            allModules.push({ value: moduleNav, text: moduleCaption });

        };

        //Sort modules
        function compare(a, b) {
            if (a.text < b.text)
                return -1;
            if (a.text > b.text)
                return 1;
            return 0;
        }
        allModules.sort(compare);

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
        FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath });
        FwFormField.disable($form.find('.ifnew'));
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