routes.push({ pattern: /^module\/duplicaterule/, action: function (match: RegExpExecArray) { return DuplicateRuleController.getModuleScreen(); } });
class DuplicateRule {
    Module: string = 'DuplicateRule';
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
        for (let i = 0; i < modules.length; i++) { //Traverse security tree and only add modules with 'New' or 'Edit' options 
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
                        let moduleNav = modules[i].properties.controller.slice(0, -10)
                            , moduleCaption = modules[i].properties.caption
                            , moduleController = modules[i].properties.controller;
                        if (typeof window[moduleController] !== 'undefined') {
                            if (window[moduleController].hasOwnProperty('apiurl')) {
                                var moduleUrl = window[moduleController].apiurl;
                                allModules.push({ value: moduleNav, text: moduleCaption, apiurl: moduleUrl });
                            }
                        }
                    }
                }
            }
        };
        //load grids
        const gridNode = FwApplicationTree.getNodeById(FwApplicationTree.tree, '43765919-4291-49DD-BE76-F69AA12B13E8');
        let gridModules = FwApplicationTree.getChildrenByType(gridNode, 'Grid');
        for (let i = 0; i < gridModules.length; i++) { //Traverse security tree and only add grids with 'New' or 'Edit' options 
            let gridChildren = gridModules[i].children;
            let menuBarNodePosition = gridChildren.map(function (x) { return x.properties.nodetype; }).indexOf('MenuBar');
            if (menuBarNodePosition != -1) {
                let menuBarChildren = gridChildren[menuBarNodePosition].children;
                let newMenuBarButtonPosition = menuBarChildren.map(function (x) { return x.properties.nodetype; }).indexOf('NewMenuBarButton');
                let editMenuBarButtonPosition = menuBarChildren.map(function (x) { return x.properties.nodetype; }).indexOf('EditMenuBarButton');
                if (newMenuBarButtonPosition != -1 || editMenuBarButtonPosition != -1) {
                    let moduleNav = gridModules[i].properties.controller.slice(0, -14)
                        , moduleCaption = gridModules[i].properties.caption
                        , moduleController = gridModules[i].properties.controller;
                    if (typeof window[moduleController] !== 'undefined') {
                        if (window[moduleController].hasOwnProperty('apiurl')) {
                            let moduleUrl = window[moduleController].apiurl;
                            allModules.push({ value: moduleNav, text: moduleCaption, apiurl: moduleUrl });
                        }
                    }
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
        let self = this;
        $form.find('div.modules').on("change", function () {
            let moduleName, moduleUrl, request;
            moduleName = jQuery(this).find(':selected').val();
            moduleUrl = jQuery(this).find(':selected').attr('data-apiurl');
            request = {
                module: moduleName,
                top: 1
            };

            FwAppData.apiMethod(true, 'GET', `${moduleUrl}/emptyobject`, request, FwServices.defaultTimeout, function onSuccess(response) {
                let fieldColumns = Object.keys(response);
                let customFields = response._Custom.map(obj => obj.FieldName);
                for (let i = 0; i < customFields.length; i++) {
                    fieldColumns.push(`${customFields[i]}`);
                }
                fieldColumns = fieldColumns.sort(self.compare);

                let fieldsHtml = [];
                let $fields = $form.find('.fields');
                for (var i = 0; i < fieldColumns.length; i++) {
                    if (fieldColumns[i] != 'DateStamp' && fieldColumns[i] != 'RecordTitle' && fieldColumns[i] != '_Custom') {
                        let uniqueId = FwApplication.prototype.uniqueId(10);
                        fieldsHtml.push(`<div data-control="FwFormField"
                                data-iscustomfield=${customFields.indexOf(fieldColumns[i]) === -1 ? false : true}
                                data-type="checkbox"
                                class="fwcontrol fwformfield check SystemRuleTRUE"
                                data-enabled="true"
                                data-caption="${fieldColumns[i]}"
                                data-value="${fieldColumns[i]}"
                                style="float:left;width:320px; padding: 10px; 0px;">
                                <input id="${uniqueId}" class="fwformfield-control fwformfield-value" type="checkbox" name="${fieldColumns[i]}"/>
                                <label class="fwformfield-caption" for="${uniqueId}">${fieldColumns[i]}</label>
                                </div>
                       `);
                    }
                }
                $fields.empty().append(fieldsHtml.join('')).html();


                let fields = $form.find('[data-datafield="Fields"]').attr('data-originalvalue');
                let separateFields = fields.split(",");
                jQuery.each(separateFields, function (i, val) {
                    jQuery(`input[name="${val}"]`).prop("checked", true)
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
            }, null, $form);
        });
    }
    //----------------------------------------------------------------------------------------------
    afterSave($form: any): void {
        FwFormField.disable($form.find('div[data-datafield="ModuleName"]'));
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        if (FwFormField.getValueByDataField($form, 'SystemRule') === 'true') {
            FwFormField.toggle($form.find('.SystemRuleTRUE'), false);
        }
    }
    //----------------------------------------------------------------------------------------------
    compare(a, b) {
        if (a < b)
            return -1;
        if (a > b)
            return 1;
        return 0;
    }
}
//----------------------------------------------------------------------------------------------
var DuplicateRuleController = new DuplicateRule();