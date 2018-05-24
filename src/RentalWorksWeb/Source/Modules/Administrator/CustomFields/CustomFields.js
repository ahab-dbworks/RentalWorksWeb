var CustomFields = (function () {
    function CustomFields() {
        this.Module = 'CustomFields';
        this.apiurl = 'api/v1/customfield';
    }
    CustomFields.prototype.getModuleScreen = function () {
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
    };
    CustomFields.prototype.openBrowse = function () {
        var $browse;
        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        return $browse;
    };
    CustomFields.prototype.openForm = function (mode) {
        var $form, $moduleSelect;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        $form.find('div[data-datafield="CustomTableName"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true && $this.val() === 'customvaluesnumeric') {
                $form.find('.float').show();
            }
        });
        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'));
        }
        var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '0A5F2584-D239-480F-8312-7C2B552A30BA');
        var modules = FwApplicationTree.getChildrenByType(node, 'Module');
        var settingsModules = FwApplicationTree.getChildrenByType(node, 'SettingsModule');
        var allModules = [];
        for (var i = 0; i < modules.length; i++) {
            var moduleNav = modules[i].properties.controller.slice(0, -10);
            var moduleCaption = modules[i].properties.caption;
            allModules.push({ value: moduleNav, text: moduleCaption });
        }
        ;
        for (var i = 0; i < settingsModules.length; i++) {
            var settingsModuleNav = settingsModules[i].properties.controller.slice(0, -10);
            var settingsModuleCaption = settingsModules[i].properties.caption;
            allModules.push({ value: settingsModuleNav, text: settingsModuleCaption });
        }
        ;
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
    };
    CustomFields.prototype.loadForm = function (uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CustomFieldId"] input').val(uniqueids.CustomFieldId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    CustomFields.prototype.saveForm = function ($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
        FwFormField.disable($form.find('.ifnew'));
    };
    CustomFields.prototype.loadAudit = function ($form) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="CustomFieldId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    CustomFields.prototype.afterLoad = function ($form) {
        if (FwFormField.getValueByDataField($form, 'CustomTableName') === 'customvaluesnumeric') {
            $form.find('.float').show();
        }
    };
    CustomFields.prototype.afterSave = function ($form) {
        FwAppData.apiMethod(true, 'GET', 'api/v1/custommodule/', null, FwServices.defaultTimeout, function onSuccess(response) {
            var customFields = [];
            for (var i = 0; i < response.length; i++) {
                customFields.push(response[i].ModuleName);
            }
            sessionStorage.setItem('customFields', JSON.stringify(customFields));
        }, null, null);
        return $form;
    };
    return CustomFields;
}());
var CustomFieldsController = new CustomFields();
//# sourceMappingURL=CustomFields.js.map