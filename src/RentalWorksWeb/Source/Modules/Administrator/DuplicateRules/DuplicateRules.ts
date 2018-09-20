class DuplicateRules {
    Module: string = 'DuplicateRules';
    apiurl: string = 'api/v1/duplicaterule';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
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
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        FwBrowse.addLegend($browse, 'User Defined Rule', '#00FF00');

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form
            , $moduleSelect
            , node
            , mainModules
            , settingsModules
            , modules
            , allModules;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'));
        } else {
            FwFormField.disable($form.find('.ifnew'));
        }

        //load modules
        node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '0A5F2584-D239-480F-8312-7C2B552A30BA');
        mainModules = FwApplicationTree.getChildrenByType(node, 'Module');
        settingsModules = FwApplicationTree.getChildrenByType(node, 'SettingsModule');
        modules = mainModules.concat(settingsModules);
        allModules = [];
        for (let i = 0; i < modules.length; i++) {
            let moduleNav = modules[i].properties.controller.slice(0, -10)
                , moduleCaption = modules[i].properties.caption
                , moduleController = modules[i].properties.controller;
            if (typeof window[moduleController] !== 'undefined') {
                if (window[moduleController].hasOwnProperty('apiurl')) {
                    var moduleUrl = window[moduleController].apiurl;
                    allModules.push({ value: moduleNav, text: moduleCaption, apiurl: moduleUrl });
                }
            }
        };
        //load grids
        const gridNode = FwApplicationTree.getNodeById(FwApplicationTree.tree, '43765919-4291-49DD-BE76-F69AA12B13E8');
        let gridModules = FwApplicationTree.getChildrenByType(gridNode, 'Grid');

        for (let i = 0; i < gridModules.length; i++) {
            let moduleNav = gridModules[i].properties.controller.slice(0, -14)
                , moduleCaption = gridModules[i].properties.caption
                , moduleController = gridModules[i].properties.controller;
            if (typeof window[moduleController] !== 'undefined') {
                if (window[moduleController].hasOwnProperty('apiurl')) {
                    let moduleUrl = window[moduleController].apiurl;
                    allModules.push({ value: moduleNav, text: moduleCaption, apiurl: moduleUrl });
                }
            }
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

        this.getFields($form);

        $form.find('[data-datafield="SystemRule"]').attr('data-required', false);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DuplicateRuleId"] input').val(uniqueids.DuplicateRuleId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    getFields($form: JQuery): void {
        $form.find('div.modules').on("change", function () {
            let moduleName, moduleUrl, request;
            moduleName = jQuery(this).find(':selected').val();
            moduleUrl = jQuery(this).find(':selected').attr('data-apiurl');
            request = {
                module: moduleName,
                top: 1
            };

            FwAppData.apiMethod(true, 'POST', `${moduleUrl}/browse`, request, FwServices.defaultTimeout, function onSuccess(response) {
                var fieldColumns = response.Columns;

                var filteredColumns = fieldColumns.filter(function (obj) {
                    return obj.DataField !== 'DateStamp';
                });

                var fieldNamesOnly = filteredColumns.map(a => a.DataField).sort();
                var sortedFields = fieldNamesOnly.sort(function (a, b) {
                    return a.toLowerCase().localeCompare(b.toLowerCase());
                });

                var fieldsHtml = [];
                var $fields = $form.find('.fields');
                for (var i = 0; i < sortedFields.length; i++) {
                    var uniqueId = FwApplication.prototype.uniqueId(10);
                    fieldsHtml.push('<div data-control="FwFormField"');
                    fieldsHtml.push(' data-type="checkbox"');
                    fieldsHtml.push(' class="fwcontrol fwformfield"');
                    fieldsHtml.push(' data-enabled="true"');
                    fieldsHtml.push(` data-caption="${sortedFields[i]}"`);
                    fieldsHtml.push(` data-value="${sortedFields[i]}"`);
                    fieldsHtml.push(' style="float:left;width:300px; padding: 10px; 0px;"');
                    fieldsHtml.push('>');
                    fieldsHtml.push(`<input id="${uniqueId}" class="fwformfield-control fwformfield-value" type="checkbox" name= "${sortedFields[i]}"`);
                    fieldsHtml.push(' />');
                    fieldsHtml.push(`<label class="fwformfield-caption" for="${uniqueId}">${sortedFields[i]}</label>`);
                    fieldsHtml.push('</div>');
                }
                $fields.empty().append(fieldsHtml.join('')).html();

                var fields = $form.find('[data-datafield="Fields"]').attr('data-originalvalue');
                var separateFields = fields.split(",");
                jQuery.each(separateFields, function (i, val) {
                    jQuery(`input[name="${val}"]`).prop("checked", true)
                });

                $form.on('change', '[type="checkbox"]', e => {
                    var field = jQuery(e.currentTarget).attr('name');
                    if (separateFields.indexOf(field) === -1) {
                        separateFields.push(field);
                    } else {
                        separateFields = separateFields.filter(item => { return item !== field })
                    }
                    //if (separateFields.length !== 1) {
                    //    throw 'Expected 1 element, but got: ' + separateFields.length;
                    //}
                    var joinFields = separateFields.join(',');
                    FwFormField.setValueByDataField($form, 'Fields', joinFields);
                });
            }, null, $form);
        });
    }
    //----------------------------------------------------------------------------------------------
    afterSave($form: any): void {
        FwFormField.disable($form.find('div[data-datafield="ModuleName"]'));
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        var moduleName = $form.find('.modules').attr('data-originalvalue');
        var moduleUrl = $form.find(`select option[value="${moduleName}"]`).attr('data-apiurl');
        var request = {
            module: moduleName
        };

        FwAppData.apiMethod(true, 'POST', `${moduleUrl}/browse`, request, FwServices.defaultTimeout, function onSuccess(response) {
            var fieldColumns = response.Columns;

            var filteredColumns = fieldColumns.filter(function (obj) {
                return obj.DataField !== 'DateStamp';
            });

            var fieldNamesOnly = filteredColumns.map(a => a.DataField).sort();
            var sortedFields = fieldNamesOnly.sort(function (a, b) {
                return a.toLowerCase().localeCompare(b.toLowerCase());
            });

            var fieldsHtml = [];
            var $fields = $form.find('.fields');

            for (var i = 0; i < sortedFields.length; i++) {
                let uniqueId = FwApplication.prototype.uniqueId(10);
                fieldsHtml.push(`<div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield check SystemRuleTRUE" data-enabled="true" data-datafield="${sortedFields[i]}" data-value="${sortedFields[i]}" data-caption="${sortedFields[i]}" style="float:left;width:300px;padding:10px;0px;">`);
                fieldsHtml.push(`<input id="${uniqueId}" class="fwformfield-control fwformfield-value" type="checkbox" name="${sortedFields[i]}"/>`);
                fieldsHtml.push(`<label class="fwformfield-caption" data-value="${sortedFields[i]}" for="${uniqueId}">${sortedFields[i]}</label></div>`);
            }

            $fields.empty().append(fieldsHtml.join(''));

            let fields = $form.find('[data-datafield="Fields"]').attr('data-originalvalue');
            var separateFields = fields.split(",");
            //for (let i = 0; i < separateFields.length; i++) {
            //    FwFormField.setValue($form, `div[data-datafield=${separateFields[i]}]`, 'true');
            //}
            jQuery.each(separateFields, function (i, val) {
                jQuery(`input[name="${val}"]`).prop("checked", true);
            });

            $form.on('change', '[type="checkbox"]', e => {
                let field = jQuery(e.currentTarget).attr('name');
                if (separateFields.indexOf(field) === -1) {
                    separateFields.push(field);
                } else {
                    separateFields = separateFields.filter(item => { return item !== field })
                }
                let joinFields = separateFields.join(',');
                FwFormField.setValueByDataField($form, 'Fields', joinFields);
            });

            if (FwFormField.getValueByDataField($form, 'SystemRule') === 'true') { FwFormField.toggle($form.find('.SystemRuleTRUE'), false); }
        }, null, $form);
    }
    //----------------------------------------------------------------------------------------------
}
//----------------------------------------------------------------------------------------------
var DuplicateRulesController = new DuplicateRules();