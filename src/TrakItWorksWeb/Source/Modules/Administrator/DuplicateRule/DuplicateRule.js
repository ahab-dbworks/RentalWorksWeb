routes.push({ pattern: /^module\/duplicaterule/, action: function (match) { return DuplicateRuleController.getModuleScreen(); } });
class DuplicateRule {
    constructor() {
        this.Module = 'DuplicateRule';
        this.apiurl = 'api/v1/duplicaterule';
        this.caption = 'Duplicate Rules';
        this.nav = 'module/duplicaterule';
        this.id = '2E0EA479-AC02-43B1-87FA-CCE2ABA6E934';
        this.getModuleScreen = () => {
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
            screen.unload = () => {
                FwBrowse.screenunload($browse);
            };
            return screen;
        };
    }
    openBrowse() {
        let $browse;
        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        FwBrowse.addLegend($browse, 'User Defined Rule', '#00FF00');
        return $browse;
    }
    openForm(mode) {
        let $form;
        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'));
        }
        else {
            FwFormField.disable($form.find('.ifnew'));
        }
        const node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '0A5F2584-D239-480F-8312-7C2B552A30BA');
        const mainModules = FwApplicationTree.getChildrenByType(node, 'Module');
        const settingsModules = FwApplicationTree.getChildrenByType(node, 'SettingsModule');
        const modules = mainModules.concat(settingsModules);
        let allModules = [];
        for (let i = 0; i < modules.length; i++) {
            let moduleChildren = modules[i].children;
            let browseNodePosition = moduleChildren.map(function (x) { return x.properties.nodetype; }).indexOf('Browse');
            if (browseNodePosition != -1) {
                let browseNodeChildren = moduleChildren[browseNodePosition].children;
                let menuBarNodePosition = browseNodeChildren.map(function (x) { return x.properties.nodetype; }).indexOf('MenuBar');
                if (menuBarNodePosition != -1) {
                    let menuBarChildren = browseNodeChildren[menuBarNodePosition].children;
                    let newMenuBarButtonPosition = menuBarChildren.map(function (x) { return x.properties.nodetype; }).indexOf('NewMenuBarButton');
                    let editMenuBarButtonPosition = menuBarChildren.map(function (x) { return x.properties.nodetype; }).indexOf('EditMenuBarButton');
                    if (newMenuBarButtonPosition != -1 || editMenuBarButtonPosition != -1) {
                        let moduleNav = modules[i].properties.controller.slice(0, -10), moduleCaption = modules[i].properties.caption, moduleController = modules[i].properties.controller;
                        if (typeof window[moduleController] !== 'undefined') {
                            if (window[moduleController].hasOwnProperty('apiurl')) {
                                var moduleUrl = window[moduleController].apiurl;
                                allModules.push({ value: moduleNav, text: `${moduleCaption}`, apiurl: moduleUrl });
                            }
                        }
                    }
                }
            }
        }
        ;
        const gridNode = FwApplicationTree.getNodeById(FwApplicationTree.tree, '43765919-4291-49DD-BE76-F69AA12B13E8');
        let gridModules = FwApplicationTree.getChildrenByType(gridNode, 'Grid');
        for (let i = 0; i < gridModules.length; i++) {
            let gridChildren = gridModules[i].children;
            let menuBarNodePosition = gridChildren.map(function (x) { return x.properties.nodetype; }).indexOf('MenuBar');
            if (menuBarNodePosition != -1) {
                let menuBarChildren = gridChildren[menuBarNodePosition].children;
                let newMenuBarButtonPosition = menuBarChildren.map(function (x) { return x.properties.nodetype; }).indexOf('NewMenuBarButton');
                let editMenuBarButtonPosition = menuBarChildren.map(function (x) { return x.properties.nodetype; }).indexOf('EditMenuBarButton');
                if (newMenuBarButtonPosition != -1 || editMenuBarButtonPosition != -1) {
                    let moduleNav = gridModules[i].properties.controller.slice(0, -14), moduleCaption = gridModules[i].properties.caption, moduleController = gridModules[i].properties.controller;
                    if (typeof window[moduleController] !== 'undefined') {
                        if (window[moduleController].hasOwnProperty('apiurl')) {
                            let moduleUrl = window[moduleController].apiurl;
                            allModules.push({ value: moduleNav, text: `${moduleCaption}`, apiurl: moduleUrl });
                        }
                    }
                }
            }
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
        const $moduleSelect = $form.find('.modules');
        FwFormField.loadItems($moduleSelect, allModules);
        this.getFields($form);
        $form.find('[data-datafield="SystemRule"]').attr('data-required', false);
        return $form;
    }
    loadForm(uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DuplicateRuleId"] input').val(uniqueids.DuplicateRuleId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    getFields($form) {
        let self = this;
        $form.find('div.modules').on("change", function () {
            let moduleUrl, request;
            moduleUrl = jQuery(this).find(':selected').attr('data-apiurl');
            FwAppData.apiMethod(true, 'GET', `${moduleUrl}/emptyobject`, null, FwServices.defaultTimeout, function onSuccess(response) {
                let fieldsList = response._Fields;
                let customFields;
                if (response._Custom.length > 0) {
                    customFields = response._Custom.map(obj => { return { 'Name': obj.FieldName, 'DataType': obj.FieldType }; });
                    jQuery.merge(fieldsList, customFields);
                }
                fieldsList = fieldsList.sort(self.compare);
                let fieldsHtml = [];
                let $fields = $form.find('.fields');
                for (var i = 0; i < fieldsList.length; i++) {
                    let field = fieldsList[i];
                    if (field.Name != 'DateStamp' && field.Name != 'RecordTitle' && field.Name != '_Custom' && field.Name != '_Fields') {
                        let uniqueId = FwApplication.prototype.uniqueId(10);
                        fieldsHtml.push(`<div data-control="FwFormField"
                                data-iscustomfield="${typeof customFields == "undefined" ? false : customFields.indexOf(fieldsList[i]) === -1 ? false : true}"
                                data-type="checkbox"
                                class="fwcontrol fwformfield check SystemRuleTRUE"
                                data-enabled="true"
                                data-caption="${field.Name}"
                                data-value="${field.Name}"
                                style="float:left;width:320px; padding: 10px 0px;">
                                <input id="${uniqueId}" class="fwformfield-control fwformfield-value" type="checkbox" data-fieldtype="${field.DataType}" name="${field.Name}"/>
                                <label class="fwformfield-caption" for="${uniqueId}">${field.Name}</label>
                                </div>
                       `);
                    }
                }
                $fields.empty().append(fieldsHtml.join('')).html();
                let fields = FwFormField.getValueByDataField($form, 'Fields');
                fields = fields.split(",");
                let fieldTypes = FwFormField.getValueByDataField($form, 'FieldTypes');
                fieldTypes = fieldTypes.split(",");
                jQuery.each(fields, function (i, val) {
                    jQuery(`input[name="${val}"]`).prop("checked", true);
                });
                $form.on('change', '[type="checkbox"]', e => {
                    let $this = jQuery(e.currentTarget);
                    let field = $this.attr('name');
                    let fieldType = $this.attr('data-fieldtype');
                    let index = fields.indexOf(field);
                    if (index === -1) {
                        fields.push(field);
                        fieldTypes.push(fieldType);
                    }
                    else {
                        fields.splice(index, 1);
                        fieldTypes.splice(index, 1);
                    }
                    FwFormField.setValueByDataField($form, 'Fields', fields.join(","));
                    FwFormField.setValueByDataField($form, 'FieldTypes', fieldTypes.join(","));
                });
            }, null, $form);
        });
    }
    afterSave($form) {
        FwFormField.disable($form.find('div[data-datafield="ModuleName"]'));
    }
    afterLoad($form) {
        $form.find('div.modules').change();
        if (FwFormField.getValueByDataField($form, 'SystemRule') === 'true') {
            FwFormField.toggle($form.find('.SystemRuleTRUE'), false);
        }
        const $tabpage = $form.parent();
        const $tab = jQuery('#' + $tabpage.attr('data-tabid'));
        $tab.find('.modified').html('');
        $form.attr('data-modified', 'false');
        $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
    }
    compare(a, b) {
        if (a.Name < b.Name)
            return -1;
        if (a.Name > b.Name)
            return 1;
        return 0;
    }
}
var DuplicateRuleController = new DuplicateRule();
//# sourceMappingURL=DuplicateRule.js.map