var DuplicateRules = (function () {
    function DuplicateRules() {
        this.Module = 'DuplicateRules';
        this.apiurl = 'api/v1/duplicaterule';
    }
    DuplicateRules.prototype.getModuleScreen = function () {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Duplicate Rules', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    DuplicateRules.prototype.openBrowse = function () {
        var $browse;
        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);
        return $browse;
    };
    DuplicateRules.prototype.openForm = function (mode) {
        var $form, $moduleSelect;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'));
        }
        else {
            FwFormField.disable($form.find('.ifnew'));
        }
        var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '0A5F2584-D239-480F-8312-7C2B552A30BA');
        var modules = FwApplicationTree.getChildrenByType(node, 'Module');
        var allModules = [];
        for (var i = 0; i < modules.length; i++) {
            var moduleNav = modules[i].properties.controller.slice(0, -10);
            var moduleCaption = modules[i].properties.caption;
            var moduleController = modules[i].properties.controller;
            var moduleUrl = window[moduleController].apiurl;
            allModules.push({ value: moduleNav, text: moduleCaption, apiurl: moduleUrl });
        }
        ;
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
        this.events($form);
        return $form;
    };
    DuplicateRules.prototype.loadForm = function (uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DuplicateRuleId"] input').val(uniqueids.DuplicateRuleId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    DuplicateRules.prototype.saveForm = function ($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    };
    DuplicateRules.prototype.events = function ($form) {
        $form.find('div.modules').on("change", function () {
            var moduleName = jQuery(this).find(':selected').val();
            var request = {
                module: moduleName
            };
            var moduleUrl = jQuery(this).find(":selected").attr('data-apiurl');
            FwAppData.apiMethod(true, 'POST', moduleUrl + "/browse", request, FwServices.defaultTimeout, function onSuccess(response) {
                var columns = response.ColumnIndex;
                var fieldsHtml = [];
                var $fields = $form.find('.fields');
                for (var key in columns) {
                    var uniqueId = FwApplication.prototype.uniqueId(10);
                    fieldsHtml.push('<div data-control="FwFormField"');
                    fieldsHtml.push(' data-type="checkbox"');
                    fieldsHtml.push(' class="fwcontrol fwformfield"');
                    fieldsHtml.push(' data-caption="' + key + '"');
                    //fieldsHtml.push(' data-datafield="Fields"');
                    fieldsHtml.push(' data-value="' + key + '"');
                    fieldsHtml.push(' style="float:left;width:150px;"');
                    fieldsHtml.push('>');
                    fieldsHtml.push('<input id="' + uniqueId + '" class="fwformfield-control fwformfield-value" type="checkbox" name= "' + key + '"');
                    fieldsHtml.push(' />');
                    fieldsHtml.push('<label class="fwformfield-caption" for="' + uniqueId + '">' + key + '</label>');
                    fieldsHtml.push('</div>');
                }
                $fields.empty().append(fieldsHtml.join('')).html();
            });
        });
        var fieldsArray = [];
        $form.on('change', '[type="checkbox"]', function (e) {
            var field = jQuery(e.currentTarget).attr('name');
            if (fieldsArray.indexOf(field) === -1) {
                fieldsArray.push(field);
            }
            else {
                fieldsArray = fieldsArray.filter(function (item) { return item !== field; });
            }
            console.log(fieldsArray, "FIELDS");
        });
        //FwFormField.setValueByDataField($form, 'Fields', fieldsArray)
    };
    DuplicateRules.prototype.afterLoad = function ($form) {
        var moduleName = $form.find('.modules').attr('data-originalvalue');
        var moduleUrl = $form.find("select option[value='" + moduleName + "']").attr('data-apiurl');
        var request = {
            module: moduleName
        };
        FwAppData.apiMethod(true, 'POST', moduleUrl + "/browse", request, FwServices.defaultTimeout, function onSuccess(response) {
            var columns = response.ColumnIndex;
            var fieldsHtml = [];
            var $fields = $form.find('.fields');
            for (var key in columns) {
                var uniqueId = FwApplication.prototype.uniqueId(10);
                fieldsHtml.push('<div data-control="FwFormField"');
                fieldsHtml.push(' data-type="checkbox"');
                fieldsHtml.push(' class="fwcontrol fwformfield check"');
                fieldsHtml.push(' data-caption="' + key + '"');
                //fieldsHtml.push(' data-datafield="Fields"');
                fieldsHtml.push(' data-value="' + key + '"');
                fieldsHtml.push(' style="float:left;width:150px;"');
                fieldsHtml.push('>');
                fieldsHtml.push('<input id="' + uniqueId + '" class="fwformfield-control fwformfield-value" type="checkbox" name="' + key + '"');
                fieldsHtml.push(' />');
                fieldsHtml.push('<label class="fwformfield-caption" data-value="' + key + '" for="' + uniqueId + '">' + key + '</label>');
                fieldsHtml.push('</div>');
            }
            $fields.empty().append(fieldsHtml.join('')).html();
        });
    };
    return DuplicateRules;
}());
window.DuplicateRulesController = new DuplicateRules();
//# sourceMappingURL=DuplicateRules.js.map