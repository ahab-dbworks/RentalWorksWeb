routes.push({ pattern: /^module\/alert/, action: function (match: RegExpExecArray) { return AlertController.getModuleScreen(); } });

class Alert {
    Module: string = 'Alert';
    apiurl: string = 'api/v1/alert';
    caption: string = Constants.Modules.Administrator.Alert.caption;
    nav: string = Constants.Modules.Administrator.Alert.nav;
    id: string = Constants.Modules.Administrator.Alert.id;
    datafields: any = [];
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

        $form.off('change', '.fwformfield[data-enabled="true"][data-datafield!=""]:not(.find-field)');

        $form.data('beforesave', request => {
            request.AlertConditionList = this.getConditionData($form);
        });

        this.loadModules($form);
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
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        const $alertWebUserGrid = $form.find('div[data-grid="AlertWebUsersGrid"]');
        const $alertWebUserGridControl = FwBrowse.loadGridFromTemplate('AlertWebUsersGrid');
        $alertWebUserGrid.empty().append($alertWebUserGridControl);
        $alertWebUserGridControl.data('ondatabind', request => {
            request.uniqueids = {
                AlertId: FwFormField.getValueByDataField($form, 'AlertId')
            };
        });
        $alertWebUserGridControl.data('beforesave', request => {
            request.AlertId = FwFormField.getValueByDataField($form, 'AlertId')
        });
        FwBrowse.init($alertWebUserGridControl);
        FwBrowse.renderRuntimeHtml($alertWebUserGridControl);


        const $alertHistoryGrid = $form.find('div[data-grid="WebAlertLogGrid"]');
        const $alertHistoryGridControl = FwBrowse.loadGridFromTemplate('WebAlertLogGrid');
        $alertHistoryGrid.empty().append($alertHistoryGridControl);
        $alertHistoryGridControl.data('ondatabind', request => {
            request.uniqueids = {
                AlertId: FwFormField.getValueByDataField($form, 'AlertId')
            };
        });
        FwBrowse.init($alertHistoryGridControl);
        FwBrowse.renderRuntimeHtml($alertHistoryGridControl);
    }
    //----------------------------------------------------------------------------------------------
    loadModules($form) {
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
                                var moduleUrl = (<any>window)[moduleController].apiurl;
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
                            let moduleUrl = (<any>window)[moduleController].apiurl;
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
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        ////load condition types based on datafield's datatype
        //$form.on('change', '[data-datafield="FieldName"]', e => {
        //    const $this = jQuery(e.currentTarget);
        //    const $conditionTypeSelect = $this.closest('.conditions').find('[data-datafield="Condition"]');
        //    const dataType = $this.find(':selected').attr('data-datatype');

        //    if (dataType === 'Integer' || dataType === 'Decimal' || dataType === 'Date') {
        //        FwFormField.loadItems($conditionTypeSelect, [
        //            { value: 'CONTAINS', text: 'Contains', selected: 'F' },
        //            { value: 'CHANGEDBY', text: 'Changed By', selected: 'F' },
        //            { value: 'EQUALS', text: 'Equals', selected: 'T' },
        //            { value: 'DOESNOTEQUAL', text: 'Does Not Equal', selected: 'F' }
        //        ], true);
        //    } else {
        //        FwFormField.loadItems($conditionTypeSelect, [
        //            { value: 'CONTAINS', text: 'Contains', selected: 'T' },
        //            { value: 'STARTSWITH', text: 'Starts With', selected: 'F' },
        //            { value: 'ENDSWITH', text: 'Ends With', selected: 'F' },
        //            { value: 'EQUALS', text: 'Equals', selected: 'F' },
        //            { value: 'DOESNOTCONTAIN', text: 'Does Not Contain', selected: 'F' },
        //            { value: 'DOESNOTEQUAL', text: 'Does Not Equal', selected: 'F' }
        //        ], true);
        //    }
        //});

        //enable value field 
        $form.on('change', '[data-datafield="FieldName2"]', e => {
            const $this = jQuery(e.currentTarget);
            const val = $this.find(':selected').attr('value');
            if (val == 'LITERALVALUE') {
                FwFormField.enable($this.siblings('[data-datafield="Value"]'));
            } else {
                FwFormField.disable($this.siblings('[data-datafield="Value"]'));
            }
        });

        //$form.on('change', '[data-datafield="FieldName1"]', e => {
        //    const $this = jQuery(e.currentTarget);
        //    const val = $this.find(':selected').attr('value');
        //    if (val == 'DATACHANGEDBYUSER' || val == 'DATACHANGEDATETIME') {
        //        FwFormField.enable($this.siblings('[data-datafield="Value"]'));
        //        FwFormField.disable($this.siblings('[data-datafield="FieldName2"]'));
        //        FwFormField.disable($this.siblings('[data-datafield="Condition"]'));
        //    } else {
        //        const isLiteralVal = FwFormField.getValue2($this.siblings('[data-datafield="FieldName2"]'));
        //        if (isLiteralVal == 'LITERALVALUE') {
        //            FwFormField.disable($this.siblings('[data-datafield="Value"]'));
        //        }
        //        FwFormField.enable($this.siblings('[data-datafield="FieldName2"]'));
        //        FwFormField.enable($this.siblings('[data-datafield="Condition"]'));
        //    }
        //});

        //add condition fields
        const $conditionList = $form.find('.condition-list')
        $form.on('click', 'i.add-condition', e => {
            const $this = jQuery(e.currentTarget);
            $this.siblings('.delete-condition').show();
            $this.hide();
            let $conditionRow = this.renderConditionRow($form);
            $conditionRow.find('.delete-condition').hide();
            $conditionList.append($conditionRow);
        });
        //delete condition
        $form.on('click', 'i.delete-condition', e => {
            const $this = jQuery(e.currentTarget);
            const $conditionRow = $this.closest('.condition-row');
            const id = FwFormField.getValue2($conditionRow.find('[data-datafield="AlertConditionId"]'));
            if (id != '') {
                FwAppData.apiMethod(true, 'DELETE', `api/v1/alertcondition/${id}`, null, FwServices.defaultTimeout,
                    response => {
                        $form.find($conditionRow).remove();
                    },
                    ex => FwFunc.showError(ex), $form);
            } else {
                $form.find($conditionRow).remove();
            }
        });
        //enable save button when conditions are changed
        $form.on('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])', e => {
            e.stopPropagation();
            const $tabpage = $form.parent();
            const $tab = jQuery('#' + $tabpage.attr('data-tabid'));
            $tab.find('.modified').html('*');
            $form.attr('data-modified', 'true');
            $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
        });

        $form.on('click', '.field-name', e => {
            const textToInject = `[${jQuery(e.currentTarget).text()}]`;
            const $messageBody = $form.find('[data-datafield="AlertBody"] textarea');
            $messageBody.val(jQuery($messageBody).val() + textToInject);
        });

        //default alert body
        $form.on('click', '.default-alert-body', e => {
            const alertBody = FwFormField.getValueByDataField($form, 'AlertBody');
            const actionNew = FwFormField.getValueByDataField($form, 'ActionNew');
            const actionEdit = FwFormField.getValueByDataField($form, 'ActionEdit');
            const actionDelete = FwFormField.getValueByDataField($form, 'ActionDelete');
            let defaultBody: any = [];
            const alertName = FwFormField.getValueByDataField($form, 'AlertName');

            const fields = $form.find('.field-list').data('fieldsNoDupes');
            let html = '';
            for (let i = 0; i < fields.length; i++) {
                const field = fields[i];
                html += `
    <tr>
        <td>${field}</td>
        ${actionDelete || actionEdit ? `<td>[${fields[i]} - Old Value]</td>` : ''}
        ${actionNew || actionEdit ? `<td>[${fields[i]} - New Value]</td>` : ''}
    </tr>`};


            defaultBody =`<table>
    <tr style="font-weight:bold;">
        <td colspan="${actionEdit ? '3' : '2'}">
            ${alertName}
        </td>
    </tr>
    <tr style="font-weight:bold; background-color:#DCDCDC;">
        <td>Field Name</td>
        ${actionDelete || actionEdit ? '<td>Old Value</td>' : ''}
        ${actionNew || actionEdit ? '<td>New Value</td>' : ''}
    </tr>
    <tr>
        <td>
            Data Changed by User Name
        </td>
        <td>
            [Data Changed by User Name]
        </td>
    </tr>
    <tr>
        <td>
            Data Change Date/Time
        </td>
        <td>
            [Data Change Date/Time]
        </td>
    </tr>
    ${html}
</table>`;

            const useTemplate = () => {
                $form.attr('data-modified', true);
                $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
                $form.find('[data-datafield="AlertBody"] textarea').val(defaultBody);
            }

            if (alertBody.length > 0) {
                const $confirmation = FwConfirmation.renderConfirmation('Use Default Alert Template?', '<div style="white-space:pre;">\n' +
                    'Clear the Body field and use the default Alert template?</div>');
                const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
                FwConfirmation.addButton($confirmation, 'No');
                $yes.on('click', function () {
                    FwConfirmation.destroyConfirmation($confirmation);
                    useTemplate();
                });
            } else {
                useTemplate();
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    getFields($form: JQuery): void {
        $form.find('div.modules').on("change", e => {
            const moduleUrl = jQuery(e.currentTarget).find(':selected').attr('data-apiurl');
            FwAppData.apiMethod(true, 'GET', `${moduleUrl}/emptyobject`, null, FwServices.defaultTimeout,
                response => {
                    let fieldsNoDupes: any = [];
                    let fieldsList = response._Fields.filter(obj => { return obj.Name != 'DateStamp' });
                    fieldsNoDupes = fieldsList.map(obj => obj.Name);
                    const oldVal = fieldsList.map(obj => {
                        return { 'value': `${obj.Name}___OldValue`, 'text': `${obj.Name} - Old Value`, 'datatype': obj.DataType }
                    });
                    const newVal = fieldsList
                        .map(obj => {
                            return { 'value': `${obj.Name}___NewValue`, 'text': `${obj.Name} - New Value`, 'datatype': obj.DataType }
                        });
                    if (response._Custom.length > 0) {
                        let customNoDupes: any = [];
                        customNoDupes = response._Custom.map(obj => obj.FieldName);
                        const customOldVal = response._Custom.map(obj => {
                            return { 'value': `${obj.FieldName}___OldValue`, 'text': `${obj.FieldName} - Old Value`, 'datatype': obj.FieldType }
                        });
                        const customNewVal = response._Custom.map(obj => {
                            return { 'value': `${obj.FieldName}___NewValue`, 'text': `${obj.FieldName} - New Value`, 'datatype': obj.FieldType }
                        });
                        fieldsList = oldVal.concat(newVal).concat(customOldVal).concat(customNewVal);
                        fieldsNoDupes = fieldsNoDupes.concat(customNoDupes);
                    } else {
                        fieldsList = jQuery.merge(oldVal, newVal);
                    }

                    fieldsNoDupes = fieldsNoDupes.sort();
                    $form.find('.field-list').data('fieldsNoDupes', fieldsNoDupes);
                    
                    fieldsList = fieldsList.sort(this.compare);
                    fieldsList.unshift(
                        { 'value': 'DATACHANGEDBYUSERNAME', 'text': 'Data Changed by User Name', 'datatype': 'Text' }
                        , { 'value': 'DATACHANGEDATETIME', 'text': 'Data Change Date/Time', 'datatype': 'Date' }
                    )
                    this.datafields = fieldsList;
                    const $fieldListSection = $form.find('.field-list');
                    $fieldListSection.empty();
                    for (let i = 0; i < fieldsList.length; i++) {
                        $fieldListSection.append(`<div class="field-name" style="cursor:pointer">${fieldsList[i].text}</div>`);
                    }
                    this.addConditionRow($form);
                },
                ex => FwFunc.showError(ex), $form);
        });
    }
    //----------------------------------------------------------------------------------------------
    addConditionRow($form) {
        const $conditionsList = $form.find('.condition-list');
        $conditionsList.empty();

        const mode = $form.attr('data-mode');
        if (mode === 'EDIT') {
            this.loadConditionRows($form);
        } else {
            const $conditionRow = this.renderConditionRow($form);
            $conditionRow.find('.delete-condition').hide();
            $conditionsList.append($conditionRow);
        }
    }
    //----------------------------------------------------------------------------------------------
    renderConditionRow($form) {
        let $conditionRow = jQuery(`
                            <div class="flexrow condition-row">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="AlertConditionId" style="display:none;"></div>
                              <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Data Field" data-datafield="FieldName1" data-allcaps="false" style="flex:1 1 0; max-width:350px;"></div>
                              <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Condition" data-datafield="Condition" style="flex:1 1 0; max-width:250px;"></div>
                              <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Data Field" data-datafield="FieldName2" data-allcaps="false" style="flex:1 1 0; max-width:350px;"></div>
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="Value" data-allcaps="false" style="flex:1 1 0; max-width:250px;" data-enabled="false"></div>
                              <i class="material-icons delete-condition" style="max-width:25px; cursor:pointer; margin:23px 0px 0px 10px;">delete_outline</i>
                              <i class="material-icons add-condition" style="max-width:25px; cursor:pointer; margin:23px 0px 0px 10px;">add_circle_outline</i>
                            </div>`);
        FwControl.renderRuntimeControls($conditionRow.find('.fwcontrol'));
        const moduleName = FwFormField.getValueByDataField($form, 'ModuleName');
        if (moduleName != '') {
            const $datafield1 = $conditionRow.find('[data-datafield="FieldName1"]');
            FwFormField.loadItems($datafield1, this.datafields);
            jQuery($datafield1).find('select option:first-of-type')
                .after(`<option value="DATACHANGEDBYUSER">Data Changed by User Name</option>
                        <option value="DATACHANGEDATETIME">Data Change Date/Time</option>`);
            const $datafield2 = $conditionRow.find('[data-datafield="FieldName2"]');
            FwFormField.loadItems($datafield2, this.datafields);
            jQuery($datafield2).find('select option:first-of-type')
                .after(`<option value="LITERALVALUE">Literal Value</option>
                        <option value="DATACHANGEDBYUSER">Data Changed by User Name</option>
                        <option value="DATACHANGEDATETIME">Data Change Date/Time</option>`);

            const $conditionSelect = $conditionRow.find('[data-datafield="Condition"]');
            FwFormField.loadItems($conditionSelect, [
                { value: 'CONTAINS', text: 'Contains', selected: 'T' },
                { value: 'STARTSWITH', text: 'Starts With', selected: 'F' },
                { value: 'ENDSWITH', text: 'Ends With', selected: 'F' },
                { value: 'EQUALS', text: 'Equals', selected: 'F' },
                { value: 'DOESNOTCONTAIN', text: 'Does Not Contain', selected: 'F' },
                { value: 'DOESNOTEQUAL', text: 'Does Not Equal', selected: 'F' },
                { value: 'CHANGEDBY', text: 'Changed By', selected: 'F' }
            ]);
        }
        return $conditionRow;
    }
    //----------------------------------------------------------------------------------------------
    loadConditionRows($form) {
        const request: any = {};
        const $conditionList = $form.find('.condition-list');
        request.uniqueids = {
            AlertId: FwFormField.getValueByDataField($form, 'AlertId')
        }
        FwAppData.apiMethod(true, 'POST', `api/v1/alertcondition/browse`, request, FwServices.defaultTimeout,
            response => {
                if (response.Rows.length > 0) {
                    const alertConditionIdIndex = response.ColumnIndex.AlertConditionId;
                    const fieldName1Index = response.ColumnIndex.FieldName1;
                    const fieldName2Index = response.ColumnIndex.FieldName2;
                    const conditionIndex = response.ColumnIndex.Condition;
                    const valueIndex = response.ColumnIndex.Value;
                    for (let i = 0; i < response.Rows.length; i++) {
                        const alertConditionId = response.Rows[i][alertConditionIdIndex];
                        const fieldName1 = response.Rows[i][fieldName1Index];
                        const fieldName2 = response.Rows[i][fieldName2Index];
                        const condition = response.Rows[i][conditionIndex];
                        const value = response.Rows[i][valueIndex];
                        let $conditionRow = this.renderConditionRow($form);
                        $conditionRow.find('.add-condition').hide();
                        FwFormField.setValue2($conditionRow.find('[data-datafield="AlertConditionId"]'), alertConditionId);
                        FwFormField.setValue2($conditionRow.find('[data-datafield="FieldName1"]'), fieldName1);
                        FwFormField.setValue2($conditionRow.find('[data-datafield="FieldName2"]'), fieldName2);
                        FwFormField.setValue2($conditionRow.find('[data-datafield="Condition"]'), condition);
                        FwFormField.setValue2($conditionRow.find('[data-datafield="Value"]'), value);
                        $conditionList.append($conditionRow);

                        if (fieldName2 === 'LITERALVALUE') {
                            FwFormField.enable(jQuery($conditionRow).find('[data-datafield="Value"]'));
                        }

                        //if (fieldName1 === 'DATACHANGEDBYUSER' || fieldName1 === 'DATACHANGEDATETIME') {
                        //    FwFormField.enable(jQuery($conditionRow).find('[data-datafield="Value"]'));
                        //    FwFormField.disable(jQuery($conditionRow).find('[data-datafield="FieldName2"]'));
                        //    FwFormField.disable(jQuery($conditionRow).find('[data-datafield="Condition"]'));
                        //}
                    }
                }
                const $addConditionRow = this.renderConditionRow($form);
                $addConditionRow.find('.delete-condition').hide();
                $conditionList.append($addConditionRow);
            },
            ex => FwFunc.showError(ex),
            $form);
    }
    //----------------------------------------------------------------------------------------------
    getConditionData($form) {
        const $conditions = $form.find('.condition-row');
        const alertId = FwFormField.getValueByDataField($form, 'AlertId');
        const conditionList: any = [];
        for (let i = 0; i < $conditions.length; i++) {
            const $this = jQuery($conditions[i]);
            const fieldName1 = FwFormField.getValue2($this.find('[data-datafield="FieldName1"]'));
            const fieldName2 = FwFormField.getValue2($this.find('[data-datafield="FieldName2"]'));
            const condition = FwFormField.getValue2($this.find('[data-datafield="Condition"]'));
            const value = FwFormField.getValue2($this.find('[data-datafield="Value"]'));
            const alertConditionId = FwFormField.getValue2($this.find('[data-datafield="AlertConditionId"]'));
            if (fieldName1 !== '' && fieldName2 !== '' && condition !== '') {
                conditionList.push({
                    AlertConditionId: alertConditionId
                    , AlertId: alertId
                    , FieldName1: fieldName1
                    , FieldName2: fieldName2
                    , Condition: condition
                    , Value: value
                });
            }
        }
        return conditionList;
    }
    //----------------------------------------------------------------------------------------------
    afterSave($form: any): void {
        FwFormField.disable($form.find('div[data-datafield="ModuleName"]'));
        const $tabpage = $form.parent();
        const $tab = jQuery('#' + $tabpage.attr('data-tabid'));
        $tab.find('.modified').html('');
        $form.attr('data-modified', 'false');
        $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
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

        const $alertWebUsersGrid = $form.find('[data-name="AlertWebUsersGrid"]');
        FwBrowse.search($alertWebUsersGrid);

        const $alertHistoryGrid = $form.find('[data-name="WebAlertLogGrid"]');
        FwBrowse.search($alertHistoryGrid);
    }
    //----------------------------------------------------------------------------------------------
    compare(a, b) {
        if (a.value < b.value)
            return -1;
        if (a.value > b.value)
            return 1;
        return 0;
    }
}
//----------------------------------------------------------------------------------------------
var AlertController = new Alert();