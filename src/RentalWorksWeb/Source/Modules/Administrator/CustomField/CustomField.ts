class CustomField {
    Module:  string = 'CustomField';
    apiurl:  string = 'api/v1/customfield';
    caption: string = Constants.Modules.Administrator.children.CustomField.caption;
    nav:     string = Constants.Modules.Administrator.children.CustomField.nav;
    id:      string = Constants.Modules.Administrator.children.CustomField.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
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
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //$form.find('div[data-datafield="ShowInBrowse"] input').on('change', function () {
        //    var $this = jQuery(this);

        //    if ($this.prop('checked') === true) {
        //        $form.find('.browselength').show();
        //    } else {
        //        $form.find('.browselength').hide();
        //    }
        //})

        $form.find('div[data-datafield="CustomTableName"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);

            if ($this.prop('checked') === true && $this.val() === 'customvaluesnumeric') {
                $form.find('.float').show();
            } else {
                $form.find('.float').hide();
            }
        });

        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'))
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
         let $moduleSelect = $form.find('.modules');
        FwFormField.loadItems($moduleSelect, allModules);
        

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CustomFieldId"] input').val(uniqueids.CustomFieldId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
        FwFormField.disable($form.find('.ifnew'));
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        if (FwFormField.getValueByDataField($form, 'CustomTableName') === 'customvaluesnumeric') {
            $form.find('.float').show();
        }

        //if (FwFormField.getValueByDataField($form, 'ShowInBrowse')) {
        //    $form.find('.browselength').show();
        //}
    }
    //----------------------------------------------------------------------------------------------
    afterSave($form: any) {
        FwAppData.apiMethod(true, 'GET', 'api/v1/custommodule/', null, FwServices.defaultTimeout, function onSuccess(response) {
            var customFields = [];
            for (var i = 0; i < response.length; i++) {
                customFields.push(response[i].ModuleName);
            }
            sessionStorage.setItem('customFields', JSON.stringify(customFields));
        }, null, null)

        return $form;
    }
}
//----------------------------------------------------------------------------------------------
var CustomFieldController = new CustomField();