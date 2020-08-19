class CustomField {
    constructor() {
        this.Module = 'CustomField';
        this.apiurl = 'api/v1/customfield';
        this.caption = Constants.Modules.Administrator.children.CustomField.caption;
        this.nav = Constants.Modules.Administrator.children.CustomField.nav;
        this.id = Constants.Modules.Administrator.children.CustomField.id;
    }
    getModuleScreen() {
        const screen = {};
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
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        return $browse;
    }
    openForm(mode) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        $form.find('div[data-datafield="CustomTableName"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true && $this.val() === 'customvaluesnumeric') {
                $form.find('.float').show();
            }
            else {
                $form.find('.float').hide();
            }
        });
        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'));
        }
        const modules = FwApplicationTree.getAllModules(false, false, (modules, moduleCaption, moduleName, category, currentNode, nodeModule, hasView, hasNew, hasEdit, moduleController) => {
            if (moduleController.hasOwnProperty('apiurl')) {
                modules.push({ value: moduleName.toLowerCase(), text: moduleCaption, apiurl: moduleController.apiurl });
            }
        });
        const grids = FwApplicationTree.getAllGrids(false, (modules, moduleCaption, moduleName, category, currentNode, nodeModule, hasNew, hasEdit, moduleController) => {
            if (moduleController.hasOwnProperty('apiurl')) {
                modules.push({ value: moduleName.toLowerCase(), text: moduleCaption, apiurl: moduleController.apiurl });
            }
        });
        const allModules = modules.concat(grids);
        FwApplicationTree.sortModules(allModules);
        let $moduleSelect = $form.find('.modules');
        FwFormField.loadItems($moduleSelect, allModules);
        return $form;
    }
    loadForm(uniqueids) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CustomFieldId"] input').val(uniqueids.CustomFieldId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
        FwFormField.disable($form.find('.ifnew'));
    }
    afterLoad($form) {
        if (FwFormField.getValueByDataField($form, 'CustomTableName') === 'customvaluesnumeric') {
            $form.find('.float').show();
        }
    }
    afterSave($form) {
        FwAppData.apiMethod(true, 'GET', 'api/v1/custommodule/', null, FwServices.defaultTimeout, function onSuccess(response) {
            var customFields = [];
            for (var i = 0; i < response.length; i++) {
                customFields.push(response[i].ModuleName);
            }
            sessionStorage.setItem('customFields', JSON.stringify(customFields));
        }, null, null);
        return $form;
    }
}
var CustomFieldController = new CustomField();
//# sourceMappingURL=CustomField.js.map