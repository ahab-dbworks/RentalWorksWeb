routes.push({ pattern: /^module\/alert/, action: function (match: RegExpExecArray) { return AlertController.getModuleScreen(); } });

class Alert {
    Module: string = 'Alert';
    apiurl: string = 'api/v1/alert';
    caption: string = Constants.Modules.Administrator.Alert.caption;
    nav: string = Constants.Modules.Administrator.Alert.nav;
    id: string = Constants.Modules.Administrator.Alert.id;
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
        let $browse;
        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form;
        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'));
        } else {
            FwFormField.disable($form.find('.ifnew'));
        }

        //load modules
        const node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '0A5F2584-D239-480F-8312-7C2B552A30BA');
        const mainModules = FwApplicationTree.getChildrenByType(node, 'Module');
        const settingsModules = FwApplicationTree.getChildrenByType(node, 'SettingsModule');
        const modules = mainModules.concat(settingsModules);
        let allModules = [];

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
                                allModules.push({ value: moduleNav, text: `${moduleCaption}`, apiurl: moduleUrl });
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
                            allModules.push({ value: moduleNav, text: `${moduleCaption}`, apiurl: moduleUrl });
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

        const $moduleSelect = $form.find('.modules');
        FwFormField.loadItems($moduleSelect, allModules);

        this.getFields($form);
        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="AlertId"] input').val(uniqueids.AlertId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);

        const $conditions = $form.find('.conditions');
        const alertId = FwFormField.getValueByDataField($form, 'AlertId')
        for (let i = 0; i < $conditions.length; i++) {
            let request: any = {};
            const $this = jQuery($conditions[i]);
            request = {
                AlertId: alertId
                , FieldName: FwFormField.getValue2($this.find('[data-datafield="FieldName"]'))
                , Condition: FwFormField.getValue2($this.find('[data-datafield="Condition"]'))
                , Value: FwFormField.getValue2($this.find('[data-datafield="Value"]'))
            };
            FwAppData.apiMethod(true, 'POST', `api/v1/alertcondition`, request, FwServices.defaultTimeout,
                response => { },
                ex => FwFunc.showError(ex),
                $form);
        }
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        //load condition types based on datafield's datatype
        $form.on('change', '.fields', e => {
            const $this = jQuery(e.currentTarget);
            const $conditionTypeSelect = $this.closest('.conditions').find('[data-datafield="Condition"]');
            const dataType = $this.find(':selected').attr('data-datatype');

            if (dataType === 'Integer' || dataType === 'Decimal' || dataType === 'Date') {
                FwFormField.loadItems($conditionTypeSelect, [
                    { value: 'CONTAINS', text: 'CONTAINS', selected: 'F' },
                    { value: 'CHANGEDBY', text: 'CHANGED BY', selected: 'T' }
                ], true);
            } else {
                FwFormField.loadItems($conditionTypeSelect, [
                    { value: 'CONTAINS', text: 'CONTAINS', selected: 'T' }
                ], true);
            }
        });

        //add condition fields
        const $conditionSection = $form.find('.conditions').parent();
        $form.on('click', 'i.add-condition', e => {
            let $conditionRow = jQuery(`
              <div class="flexrow conditions">
                <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield fields" data-caption="Data Field" data-datafield="FieldName" data-allcaps="false" style="flex:1 1 0; max-width:250px;"></div>
                <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Condition Type" data-datafield="ConditionType" style="flex:1 1 0; max-width:250px;"></div>
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="ConditionValue" data-allcaps="false" style="flex:1 1 0; max-width:250px;" data-required="true"></div>
                <div style="width:80px;">
                    <i class="material-icons delete-condition" style="cursor:pointer; margin:23px 0px 0px 10px;">delete_outline</i>
                    <i class="material-icons add-condition" style="cursor:pointer; margin:23px 0px 0px 10px;">add_circle_outline</i>
                </div>
              </div>`);
            FwControl.renderRuntimeControls($conditionRow.find('.fwcontrol'));
            $conditionSection.append($conditionRow);
        });
        //delete condition
        $form.on('click', 'i.delete-condition', e => {
            const $this = jQuery(e.currentTarget);
            const $conditionRow = $this.closest('.conditions');
            $form.find($conditionRow).remove();
        });
    }
    //----------------------------------------------------------------------------------------------
    getFields($form: JQuery): void {
        $form.find('div.modules').on("change", e => {
            const moduleUrl = jQuery(e.currentTarget).find(':selected').attr('data-apiurl');
            FwAppData.apiMethod(true, 'GET', `${moduleUrl}/emptyobject`, null, FwServices.defaultTimeout,
                response => {
                    let customFields;
                    let fieldsList = response._Fields;
                    const $datafield = $form.find('[data-datafield="FieldName"]');

                    fieldsList = fieldsList
                        .filter(obj => { return obj.Name != 'DateStamp' })
                        .map(obj => { return { 'value': obj.Name, 'text': obj.Name, 'datatype': obj.DataType } });

                    if (response._Custom.length > 0) {
                        customFields = response._Custom.map(obj => { return { 'value': obj.FieldName, 'text': obj.FieldName, 'datatype': obj.FieldType } })
                        jQuery.merge(fieldsList, customFields);
                    }
                    fieldsList = fieldsList.sort(this.compare);
                    FwFormField.loadItems($datafield, fieldsList);
                },
                ex => FwFunc.showError(ex), $form);
        });
    }
    //----------------------------------------------------------------------------------------------
    afterSave($form: any): void {
        FwFormField.disable($form.find('div[data-datafield="ModuleName"]'));
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        $form.find('div.modules').change();

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
var AlertController = new Alert();