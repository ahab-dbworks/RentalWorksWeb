routes.push({ pattern: /^module\/duplicaterule/, action: function (match: RegExpExecArray) { return DuplicateRuleController.getModuleScreen(); } });

class DuplicateRule {
    Module:  string = 'DuplicateRule';
    apiurl:  string = 'api/v1/duplicaterule';
    caption: string = Constants.Modules.Administrator.children.DuplicateRule.caption;
    nav:     string = Constants.Modules.Administrator.children.DuplicateRule.nav;
    id:      string = Constants.Modules.Administrator.children.DuplicateRule.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
        const screen: any = {};
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
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        FwBrowse.addLegend($browse, 'User Defined Rule', '#00FF00');

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'));
        } else {
            FwFormField.disable($form.find('.ifnew'));
        }

        // Load Modules dropdown with sorted list of Modules and Grids
        const modules = FwApplicationTree.getAllModules(false, false, (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasView: boolean, hasNew: boolean, hasEdit: boolean, moduleController: any) => {
            if (moduleController.hasOwnProperty('apiurl')) {
                modules.push({ value: moduleName, text: moduleCaption, apiurl: moduleController.apiurl });
            }
        });
        const grids = FwApplicationTree.getAllGrids(false, (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasNew: boolean, hasEdit: boolean, moduleController: any) => {
            if (moduleController.hasOwnProperty('apiurl')) {
                modules.push({ value: moduleName, text: moduleCaption, apiurl: moduleController.apiurl });
            }
        });
        const allModules = modules.concat(grids);
        FwApplicationTree.sortModules(allModules);
        const $moduleSelect = $form.find('.modules');
        FwFormField.loadItems($moduleSelect, allModules);

        this.getFields($form);

        $form.find('[data-datafield="SystemRule"]').attr('data-required', 'false');

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form = this.openForm('EDIT');
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
            let moduleUrl;
            moduleUrl = jQuery(this).find(':selected').attr('data-apiurl');

            FwAppData.apiMethod(true, 'GET', `${moduleUrl}/emptyobject`, null, FwServices.defaultTimeout,
                response => {
                let fieldsList = response._Fields;
                let customFields;

                if (response._Custom.length > 0) {
                    customFields = response._Custom.map(obj => { return { 'Name': obj.FieldName, 'DataType': obj.FieldType } })
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
                                data-iscustomfield="${typeof customFields == "undefined" ? false : customFields.indexOf(fieldsList[i]) === -1 ? false : true }"
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
                    $form.find(`input[name="${val}"]`).prop("checked", true)
                });

                $form.on('change', '[type="checkbox"]', e => {
                    let $this = jQuery(e.currentTarget);
                    let field = $this.attr('name');
                    let fieldType = $this.attr('data-fieldtype');
                    let index = fields.indexOf(field);
                    if (index === -1) {
                        fields.push(field);
                        fieldTypes.push(fieldType);
                    } else {
                        fields.splice(index, 1);
                        fieldTypes.splice(index, 1);
                    }
                    FwFormField.setValueByDataField($form, 'Fields', fields.join(","));
                    FwFormField.setValueByDataField($form, 'FieldTypes', fieldTypes.join(","));
                });
                }, ex => FwFunc.showError(ex), $form);
        });
    }
    //----------------------------------------------------------------------------------------------
    afterSave($form: any): void {
        FwFormField.disable($form.find('div[data-datafield="ModuleName"]'));
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        $form.find('div.modules').change();
        if (FwFormField.getValueByDataField($form, 'SystemRule') === 'true') {
            FwFormField.toggle($form.find('.SystemRuleTRUE'), false);
        }

        //set form as unmodified after saving
        const $tabpage = $form.parent();
        const $tab = jQuery('#' + $tabpage.attr('data-tabid'));
        $tab.find('.modified').html('');
        $form.attr('data-modified', 'false');
        $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
    }
    //----------------------------------------------------------------------------------------------
    compare(a, b) {
        if (a.Name < b.Name)
            return -1;
        if (a.Name > b.Name)
            return 1;
        return 0;
    }
}
//----------------------------------------------------------------------------------------------
var DuplicateRuleController = new DuplicateRule();