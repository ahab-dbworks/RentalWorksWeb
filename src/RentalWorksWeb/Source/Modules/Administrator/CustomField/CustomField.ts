class CustomField {
    Module: string = 'CustomField';
    apiurl: string = 'api/v1/customfield';
    caption: string = Constants.Modules.Administrator.CustomField.caption;
    nav: string = Constants.Modules.Administrator.CustomField.nav;
    id: string = Constants.Modules.Administrator.CustomField.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
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
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form
            , $moduleSelect;

        $form = FwModule.loadFormFromTemplate(this.Module);
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
        })

        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'))
        }

        var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '0A5F2584-D239-480F-8312-7C2B552A30BA');
        var modules = FwApplicationTree.getChildrenByType(node, 'Module');
        var settingsModules = FwApplicationTree.getChildrenByType(node, 'SettingsModule');
        var allModules = [];

        for (var i = 0; i < modules.length; i++) {
            const controller = modules[i].properties.controller;
            const moduleCaption = modules[i].properties.caption;
            if (typeof window[controller] !== 'undefined') {
                if (window[controller].hasOwnProperty('apiurl')) {
                    let moduleNav = window[controller].nav;
                    if (moduleNav) {
                        const sliceIndex = moduleNav.lastIndexOf('/');
                        moduleNav = moduleNav.slice(sliceIndex + 1);
                        if (moduleNav == 'customfield') {
                            break;
                        }
                        allModules.push({ value: moduleNav, text: moduleCaption });
                    }
                }
            }
        };
        for (var i = 0; i < settingsModules.length; i++) {
            var settingsModuleNav = settingsModules[i].properties.controller.slice(0, -10);
            var settingsModuleCaption = settingsModules[i].properties.caption
            allModules.push({ value: settingsModuleNav, text: settingsModuleCaption });
        };

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

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
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