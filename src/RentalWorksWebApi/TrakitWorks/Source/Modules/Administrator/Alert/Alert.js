routes.push({ pattern: /^module\/alert/, action: function (match) { return AlertController.getModuleScreen(); } });
class Alert {
    constructor() {
        this.Module = 'Alert';
        this.apiurl = 'api/v1/alert';
        this.caption = Constants.Modules.Administrator.children.Alert.caption;
        this.nav = Constants.Modules.Administrator.children.Alert.nav;
        this.id = Constants.Modules.Administrator.children.Alert.id;
        this.datafields = [];
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
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        return $browse;
    }
    openForm(mode) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'));
        }
        else {
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
    loadForm(uniqueids) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="AlertId"] input').val(uniqueids.AlertId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    renderGrids($form) {
        FwBrowse.renderGrid({
            nameGrid: 'AlertWebUsersGrid',
            gridSecurityId: 'REgcmntq4LWE',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request) => {
                request.uniqueids = {
                    AlertId: FwFormField.getValueByDataField($form, 'AlertId'),
                };
            },
            beforeSave: (request) => {
                request.AlertId = FwFormField.getValueByDataField($form, 'AlertId');
            }
        });
        FwBrowse.renderGrid({
            nameGrid: 'WebAlertLogGrid',
            gridSecurityId: 'x6SZhutIpRi2',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request) => {
                request.uniqueids = {
                    AlertId: FwFormField.getValueByDataField($form, 'AlertId'),
                };
            }
        });
    }
    loadModules($form) {
        const modules = FwApplicationTree.getAllModules(false, false, (modules, moduleCaption, moduleName, category, currentNode, nodeModule, hasView, hasNew, hasEdit, moduleController) => {
            if (moduleController.hasOwnProperty('apiurl')) {
                modules.push({ value: moduleName, text: moduleCaption, apiurl: moduleController.apiurl });
            }
        });
        const grids = FwApplicationTree.getAllGrids(false, (modules, moduleCaption, moduleName, category, currentNode, nodeModule, hasNew, hasEdit, moduleController) => {
            if (moduleController.hasOwnProperty('apiurl')) {
                modules.push({ value: moduleName, text: moduleCaption, apiurl: moduleController.apiurl });
            }
        });
        const allModules = modules.concat(grids);
        FwApplicationTree.sortModules(allModules);
        const $moduleSelect = $form.find('.modules');
        FwFormField.loadItems($moduleSelect, allModules);
        this.getFields($form);
    }
    events($form) {
        $form.on('change', '[data-datafield="FieldName2"]', e => {
            const $this = jQuery(e.currentTarget);
            const val = $this.find(':selected').attr('value');
            if (val == 'LITERALVALUE') {
                FwFormField.enable($this.siblings('[data-datafield="Value"]'));
            }
            else {
                FwFormField.disable($this.siblings('[data-datafield="Value"]'));
            }
        });
        const $conditionList = $form.find('.condition-list');
        $form.on('click', 'i.add-condition', e => {
            const $this = jQuery(e.currentTarget);
            $this.siblings('.delete-condition').show();
            $this.hide();
            let $conditionRow = this.renderConditionRow($form);
            $conditionRow.find('.delete-condition').hide();
            $conditionList.append($conditionRow);
        });
        $form.on('click', 'i.delete-condition', e => {
            const $this = jQuery(e.currentTarget);
            const $conditionRow = $this.closest('.condition-row');
            const id = FwFormField.getValue2($conditionRow.find('[data-datafield="AlertConditionId"]'));
            if (id != '') {
                FwAppData.apiMethod(true, 'DELETE', `api/v1/alertcondition/${id}`, null, FwServices.defaultTimeout, response => {
                    $form.find($conditionRow).remove();
                }, ex => FwFunc.showError(ex), $form);
            }
            else {
                $form.find($conditionRow).remove();
            }
        });
        $form.on('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])', e => {
            e.stopPropagation();
            const $tabpage = $form.parent();
            const $tab = jQuery('#' + $tabpage.attr('data-tabid'));
            $tab.find('.modified').html('*');
            $form.attr('data-modified', 'true');
            $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
            $form.find('.btn[data-type="RefreshMenuBarButton"]').addClass('disabled');
        });
        $form.on('click', '.field-name', e => {
            const textToInject = `[${jQuery(e.currentTarget).text()}]`;
            const $messageBody = $form.find('[data-datafield="AlertBody"] textarea');
            $messageBody.val(jQuery($messageBody).val() + textToInject);
        });
        $form.on('click', '.default-alert-body', e => {
            const alertBody = FwFormField.getValueByDataField($form, 'AlertBody');
            const actionNew = FwFormField.getValueByDataField($form, 'ActionNew');
            const actionEdit = FwFormField.getValueByDataField($form, 'ActionEdit');
            const actionDelete = FwFormField.getValueByDataField($form, 'ActionDelete');
            let defaultBody = [];
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
    </tr>`;
            }
            ;
            defaultBody = `<table>
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
                $form.find('.btn[data-type="RefreshMenuBarButton"]').addClass('disabled');
                $form.find('[data-datafield="AlertBody"] textarea').val(defaultBody);
            };
            if (alertBody.length > 0) {
                const $confirmation = FwConfirmation.renderConfirmation('Use Default Alert Template?', '<div style="white-space:pre;">\n' +
                    'Clear the Body field and use the default Alert template?</div>');
                const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
                FwConfirmation.addButton($confirmation, 'No');
                $yes.on('click', function () {
                    FwConfirmation.destroyConfirmation($confirmation);
                    useTemplate();
                });
            }
            else {
                useTemplate();
            }
        });
    }
    getFields($form) {
        $form.find('div.modules').on("change", e => {
            const moduleUrl = jQuery(e.currentTarget).find(':selected').attr('data-apiurl');
            FwAppData.apiMethod(true, 'GET', `${moduleUrl}/emptyobject`, null, FwServices.defaultTimeout, response => {
                let fieldsNoDupes = [];
                let fieldsList = response._Fields.filter(obj => { return obj.Name != 'DateStamp'; });
                fieldsNoDupes = fieldsList.map(obj => obj.Name);
                const oldVal = fieldsList.map(obj => {
                    return { 'value': `${obj.Name}___OldValue`, 'text': `${obj.Name} - Old Value`, 'datatype': obj.DataType };
                });
                const newVal = fieldsList
                    .map(obj => {
                    return { 'value': `${obj.Name}___NewValue`, 'text': `${obj.Name} - New Value`, 'datatype': obj.DataType };
                });
                if (response._Custom.length > 0) {
                    let customNoDupes = [];
                    customNoDupes = response._Custom.map(obj => obj.FieldName);
                    const customOldVal = response._Custom.map(obj => {
                        return { 'value': `${obj.FieldName}___OldValue`, 'text': `${obj.FieldName} - Old Value`, 'datatype': obj.FieldType };
                    });
                    const customNewVal = response._Custom.map(obj => {
                        return { 'value': `${obj.FieldName}___NewValue`, 'text': `${obj.FieldName} - New Value`, 'datatype': obj.FieldType };
                    });
                    fieldsList = oldVal.concat(newVal).concat(customOldVal).concat(customNewVal);
                    fieldsNoDupes = fieldsNoDupes.concat(customNoDupes);
                }
                else {
                    fieldsList = jQuery.merge(oldVal, newVal);
                }
                fieldsNoDupes = fieldsNoDupes.sort();
                $form.find('.field-list').data('fieldsNoDupes', fieldsNoDupes);
                fieldsList = fieldsList.sort(this.compare);
                fieldsList.unshift({ 'value': 'DATACHANGEDBYUSERNAME', 'text': 'Data Changed by User Name', 'datatype': 'Text' }, { 'value': 'DATACHANGEDATETIME', 'text': 'Data Change Date/Time', 'datatype': 'Date' });
                this.datafields = fieldsList;
                const $fieldListSection = $form.find('.field-list');
                $fieldListSection.empty();
                for (let i = 0; i < fieldsList.length; i++) {
                    $fieldListSection.append(`<div class="field-name" style="cursor:pointer">${fieldsList[i].text}</div>`);
                }
                this.addConditionRow($form);
            }, ex => FwFunc.showError(ex), $form);
        });
    }
    addConditionRow($form) {
        const $conditionsList = $form.find('.condition-list');
        $conditionsList.empty();
        const mode = $form.attr('data-mode');
        if (mode === 'EDIT') {
            this.loadConditionRows($form);
        }
        else {
            const $conditionRow = this.renderConditionRow($form);
            $conditionRow.find('.delete-condition').hide();
            $conditionsList.append($conditionRow);
        }
    }
    renderConditionRow($form) {
        let $conditionRow = jQuery(`
                            <div class="flexrow condition-row">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="AlertConditionId" style="display:none;"></div>
                              <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Data Field" data-datafield="FieldName1" data-allcaps="false" style="flex:1 1 0; max-width:400px;"></div>
                              <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Condition" data-datafield="Condition" style="flex:1 1 0; max-width:150px;"></div>
                              <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Data Field" data-datafield="FieldName2" data-allcaps="false" style="flex:1 1 0; max-width:400px;"></div>
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
    loadConditionRows($form) {
        const request = {};
        const $conditionList = $form.find('.condition-list');
        request.uniqueids = {
            AlertId: FwFormField.getValueByDataField($form, 'AlertId')
        };
        FwAppData.apiMethod(true, 'POST', `api/v1/alertcondition/browse`, request, FwServices.defaultTimeout, response => {
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
                }
            }
            const $addConditionRow = this.renderConditionRow($form);
            $addConditionRow.find('.delete-condition').hide();
            $conditionList.append($addConditionRow);
        }, ex => FwFunc.showError(ex), $form);
    }
    getConditionData($form) {
        const $conditions = $form.find('.condition-row');
        const alertId = FwFormField.getValueByDataField($form, 'AlertId');
        const conditionList = [];
        for (let i = 0; i < $conditions.length; i++) {
            const $this = jQuery($conditions[i]);
            const fieldName1 = FwFormField.getValue2($this.find('[data-datafield="FieldName1"]'));
            const fieldName2 = FwFormField.getValue2($this.find('[data-datafield="FieldName2"]'));
            const condition = FwFormField.getValue2($this.find('[data-datafield="Condition"]'));
            const value = FwFormField.getValue2($this.find('[data-datafield="Value"]'));
            const alertConditionId = FwFormField.getValue2($this.find('[data-datafield="AlertConditionId"]'));
            if (fieldName1 !== '' && fieldName2 !== '' && condition !== '') {
                conditionList.push({
                    AlertConditionId: alertConditionId,
                    AlertId: alertId,
                    FieldName1: fieldName1,
                    FieldName2: fieldName2,
                    Condition: condition,
                    Value: value
                });
            }
        }
        return conditionList;
    }
    afterSave($form) {
        FwFormField.disable($form.find('div[data-datafield="ModuleName"]'));
        const $tabpage = $form.parent();
        const $tab = jQuery('#' + $tabpage.attr('data-tabid'));
        $tab.find('.modified').html('');
        $form.attr('data-modified', 'false');
        $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
        $form.find('.btn[data-type="RefreshMenuBarButton"]').addClass('disabled');
    }
    afterLoad($form) {
        $form.find('div.modules').change();
        const $tabpage = $form.parent();
        const $tab = jQuery('#' + $tabpage.attr('data-tabid'));
        $tab.find('.modified').html('');
        $form.attr('data-modified', 'false');
        $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
        $form.find('.btn[data-type="RefreshMenuBarButton"]').addClass('disabled');
        const $alertWebUsersGrid = $form.find('[data-name="AlertWebUsersGrid"]');
        FwBrowse.search($alertWebUsersGrid);
        const $alertHistoryGrid = $form.find('[data-name="WebAlertLogGrid"]');
        FwBrowse.search($alertHistoryGrid);
    }
    compare(a, b) {
        if (a.value < b.value)
            return -1;
        if (a.value > b.value)
            return 1;
        return 0;
    }
}
var AlertController = new Alert();
//# sourceMappingURL=Alert.js.map